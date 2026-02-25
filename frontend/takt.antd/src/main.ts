// Vue 核心
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { watchEffect } from 'vue'
import { message } from 'ant-design-vue'

// 应用入口和路由
import App from './App.vue'
import router, { clearDynamicRoutes, setGuardContext, registerDynamicRoutes } from './router'
import type { GuardContext } from './router'
import type { RouteRecordRaw } from 'vue-router'
import { unref } from 'vue'
import { logger } from '@/utils/logger'
import { eventBus, AuthEvents } from '@/utils/eventBus'
import { DateTimeHelper, formatDateTime, formatDate, formatTime, formatDateTimeShort, formatDateCN, formatDateTimeCN, formatRelative, formatFriendly, getTimestamp, getTimestampSeconds, isToday, isYesterday, isThisWeek, isThisYear, diffDateTime, addDateTime, startOfDay, endOfDay } from '@/utils/datetime'
import { MaskHelper } from '@/utils/mask'
import { TaktPasswordHelper } from '@/utils/password'
import { RegexHelper, RegexPatterns } from '@/utils/regex'
import { UploadHelper } from '@/utils/upload'
import { getDefaultEntityColumns, mergeDefaultColumns } from '@/utils/table-columns'
import { applySettings, watchSettings, notifySettingsChanged } from '@/utils/apply-settings'
import { TaktSignalRManager, signalRManager } from '@/utils/signalr'
import { setTokenGetter, setRefreshTokenGetter, setTokenSetter, setExpiresAtGetter, startTokenRefreshTimer } from '@/api/request'
import { useUserStore } from '@/stores/identity/user'
import { usePermissionStore } from '@/stores/identity/permission'
import { useMenuStore } from '@/stores/identity/menu'
import { useDictDataStore } from '@/stores/routine/dict/dictdata'
import { useTranslationStore } from '@/stores/routine/localization/translation'
import { useLocaleStore } from '@/stores/routine/localization/locale'
import { useSignalRStore } from '@/stores/identity/signalr'
import { loadTranslationsFromBackend } from '@/locales'

// 插件配置
import { setupI18n } from './locales'
import i18n from './locales'
// Ant Design Vue 全局注册（保留以兼容性更好）
import Antd, { notification } from 'ant-design-vue'
import 'ant-design-vue/dist/reset.css'
// Font Awesome 免费版
import '@fortawesome/fontawesome-free/css/all.css'
// Flag Icons
import 'flag-icons/css/flag-icons.min.css'
import '@/assets/styles/index.less'

const app = createApp(App)

// 配置 Vue 警告过滤器：忽略来自 Ant Design Vue Tooltip 的插槽警告
// 这个警告来自库内部实现，不影响功能
// 注意：这个警告是 Ant Design Vue 库的问题，在异步操作中调用插槽函数
if (import.meta.env.DEV) {
  const originalWarn = console.warn
  console.warn = (...args: any[]) => {
    // 过滤掉 Tooltip 相关的插槽警告
    if (args[0] && typeof args[0] === 'string' && 
        args[0].includes('Slot') && 
        args[0].includes('invoked outside of the render function')) {
      // 检查堆栈跟踪是否来自 Tooltip 相关组件
      const stack = new Error().stack || ''
      if (stack.includes('Tooltip') || 
          stack.includes('Trigger') || 
          stack.includes('Popup') || 
          stack.includes('PopupInner') ||
          stack.includes('Align')) {
        return // 忽略这个警告
      }
    }
    originalWarn.apply(console, args)
  }
}

// ========================================
// 插件注册（按依赖顺序）
// ========================================

// 1. Pinia（状态管理，其他插件可能依赖）
const pinia = createPinia()
app.use(pinia)

// 1.1 认证解耦：注入 token / refreshToken / 守卫上下文，订阅 eventBus（request、router、stores 互不依赖，由此处串联）
;(() => {
  // 请求拦截器在 axios 调用时同步执行、不在 Vue 上下文，故从 localStorage 读 token（由 user store 写入的持久化备份）
  const getToken = (): string | null => {
    if (typeof localStorage === 'undefined') return null
    const t = localStorage.getItem('token')
    return typeof t === 'string' && t ? t : null
  }
  const getRefreshToken = (): string | null => {
    if (typeof localStorage === 'undefined') return null
    const r = localStorage.getItem('refreshToken')
    return typeof r === 'string' && r ? r : null
  }
  setTokenGetter(getToken)
  setRefreshTokenGetter(getRefreshToken)
  setTokenSetter((token: string, refreshToken?: string, expiresIn?: number) => {
    useUserStore().setAuth(token, { refreshToken, expiresIn })
  })
  setExpiresAtGetter(() => {
    const s = typeof localStorage !== 'undefined' ? localStorage.getItem('tokenExpiresAt') : null
    if (!s) return null
    const n = parseInt(s, 10)
    return Number.isNaN(n) ? null : n
  })

  const getRootRoute = () => router.getRoutes().find((r: any) => r.path === '/' && (r.name === 'Root' || !r.name))
  const ctx: GuardContext = {
    hasToken: () => !!useUserStore().token,
    hasUserInfo: () => !!useUserStore().userInfo,
    loadUserInfo: () => useUserStore().getUserInfo(),
    getUserInfo: () => useUserStore().userInfo ?? null,
    setPermissions: (p: string[]) => usePermissionStore().setPermissions(p),
    hasPermission: (perm: string) => usePermissionStore().hasPermission(perm),
    loadBackendTranslations: async () => {
      const locale = unref(i18n.global.locale as any) || 'zh-CN'
      await loadTranslationsFromBackend(locale, 'Frontend')
    },
    loadDictData: () => useDictDataStore().loadAllDictData(),
    connectSignalR: async () => {
      const s = useSignalRStore()
      if (!s.isConnected) await s.connect()
    },
    generateRoutes: async () => {
      await useMenuStore().generateRoutes()
      return useMenuStore().routes
    },
    getIsRoutesLoaded: () => useMenuStore().isRoutesLoaded,
    getHasDynamicRoutes: () => !!(getRootRoute()?.children?.length),
    getRoutes: () => useMenuStore().routes,
    getMenuListLength: () => useMenuStore().menuList?.length ?? 0,
    syncRoutesStateFromRouter: () => {
      const root = getRootRoute()
      const menuStore = useMenuStore()
      menuStore.isRoutesLoaded = true
      if (root?.children) menuStore.routes = root.children as RouteRecordRaw[]
    },
    resetMenuAndRegenerateRoutes: async () => {
      const root = getRootRoute()
      if (root?.name) {
        router.removeRoute(root.name)
        router.addRoute({
          path: '/',
          name: 'Root',
          component: () => import('@/layouts/index.vue'),
          meta: { requiresAuth: true },
          children: []
        })
      }
      const menuStore = useMenuStore()
      menuStore.isRoutesLoaded = false
      await menuStore.generateRoutes()
      registerDynamicRoutes(menuStore.routes)
    },
    logout: () => useUserStore().logout()
  }
  setGuardContext(ctx)

  eventBus.$on(AuthEvents.RedirectToLogin, () => {
    useUserStore().logout().then(() => router.replace('/login')).catch(() => router.replace('/login'))
  })
  eventBus.$on(AuthEvents.DidLogout, () => clearDynamicRoutes())
  eventBus.$on(AuthEvents.LoginSuccess, () => startTokenRefreshTimer())
  eventBus.$on(AuthEvents.TokenRefreshed, () => {
    setTimeout(async () => {
      try {
        const s = useSignalRStore()
        if (s.isConnected) {
          await s.disconnect().catch(() => {})
          await new Promise(r => setTimeout(r, 500))
          await s.connect().catch(() => {})
        }
      } catch { /* ignore */ }
    }, 100)
  })
})()

// 2. Ant Design Vue（UI 组件库）
app.use(Antd)
// 静态 Notification 尽早配置，保证 request 拦截器里 notification.error 能正确展示（placement 等）
notification.config({ placement: 'topRight' })

// 2.1 表单设计器 FcDesigner（@form-create/antd-designer 开源版，与 form-create.com/v3/antd/designer 一致；挂载顺序：FcDesigner → formCreate）
import FcDesigner from '@form-create/antd-designer'
app.use(FcDesigner)
app.use(FcDesigner.formCreate)

// ========================================
// 全局工具类和函数注册
// ========================================
Object.assign(app.config.globalProperties, {
  // 工具类
  DateTimeHelper, // 类本身（静态方法可通过类访问）
  MaskHelper,
  TaktPasswordHelper,
  RegexHelper,
  RegexPatterns,
  UploadHelper,
  TaktSignalRManager,
  signalRManager,
  // DateTimeHelper 便捷函数（已绑定上下文，推荐使用）
  formatDateTime,
  formatDate,
  formatTime,
  formatDateTimeShort,
  formatDateCN,
  formatDateTimeCN,
  formatRelative,
  formatFriendly,
  getTimestamp,
  getTimestampSeconds,
  isToday,
  isYesterday,
  isThisWeek,
  isThisYear,
  diffDateTime,
  addDateTime,
  startOfDay,
  endOfDay,
  // 工具函数
  getDefaultEntityColumns,
  mergeDefaultColumns,
  applySettings,
  watchSettings,
  notifySettingsChanged,
  // Store（必须在 Pinia 初始化后）
  useDictDataStore,
  useTranslationStore
})

// i18n
setupI18n(app)

// 挂载前将 localeStore 与 i18n 同步，避免首屏 t() 使用错误 locale（刷新才正常的问题）
const localeStore = useLocaleStore()
const i18nLocaleRef = i18n.global.locale as { value: string }
i18nLocaleRef.value = localeStore.locale

// 动态更新页面标题
watchEffect(() => {
  // 使用类型断言，因为在实际运行时 locale 总是 WritableComputedRef
  // 访问 locale.value 以触发响应式更新
  const localeRef = i18n.global.locale as { value: string }
  void localeRef.value
  // 使用类型断言修复 t 方法的类型问题
  const htmlTitle = (i18n.global.t as any)('common.app.htmlTitle') as string
  if (htmlTitle && htmlTitle !== 'common.app.htmlTitle') {
    document.title = htmlTitle
  }
})

// Router
app.use(router)
// 注意：权限检查逻辑已在 router/index.ts 的 beforeEach 中实现

// ========================================
// 全局错误处理
// ========================================

/**
 * Vue 全局错误处理器
 * 捕获组件渲染错误、生命周期钩子错误等
 */
app.config.errorHandler = (err: unknown, instance, info) => {
  // 记录错误日志
  logger.error('[Vue Error Handler] 组件错误:', {
    error: err,
    component: instance?.$?.type?.__name || instance?.$?.type?.name || 'Unknown',
    info,
    stack: err instanceof Error ? err.stack : undefined
  })

  // 显示用户友好的错误提示
  const errorMessage = err instanceof Error ? err.message : String(err)
  message.error({
    content: `应用错误: ${errorMessage}`,
    duration: 5
  })
}

/**
 * 全局未捕获的 Promise 拒绝处理器
 * 捕获未处理的 Promise 拒绝（async/await 未捕获的错误）
 * 注意：请求层网络错误（无响应）已在 request 拦截器用 Notification 提示并跳转登录，此处不再用 Message 重复提示
 */
window.addEventListener('unhandledrejection', (event: PromiseRejectionEvent) => {
  const reason = event.reason
  // 请求层已处理的网络错误：已用 Notification 提示并跳转登录，不再用 Message 重复
  const isAxiosNetworkError =
    reason?.isAxiosError === true && reason?.request != null && reason?.response == null
  if (isAxiosNetworkError) {
    event.preventDefault()
    return
  }

  // 记录错误日志
  logger.error('[Unhandled Promise Rejection] 未处理的 Promise 拒绝:', {
    reason,
    promise: event.promise,
    stack: reason instanceof Error ? reason.stack : undefined
  })

  // 显示用户友好的错误提示
  const errorMessage = reason instanceof Error ? reason.message : String(reason || '未知错误')
  message.error({
    content: `操作失败: ${errorMessage}`,
    duration: 5
  })

  if (!import.meta.env.DEV) {
    event.preventDefault()
  }
})

/**
 * 全局 JavaScript 错误处理器
 * 捕获运行时错误、语法错误等
 */
window.addEventListener('error', (event: ErrorEvent) => {
  // 忽略资源加载错误（如图片、脚本、样式表加载失败）
  // 资源加载错误时，event.target 是 HTMLElement（如 img、script、link 等）
  if (event.target && event.target !== window && event.target !== document) {
    const target = event.target as HTMLElement
    // 如果是 HTML 元素（有 tagName），说明是资源加载错误，忽略
    if (target.tagName) {
      // 开发环境记录资源加载错误（用于调试）
      if (import.meta.env.DEV) {
        let resourceUrl = 'N/A'
        if (target instanceof HTMLImageElement || target instanceof HTMLScriptElement) {
          resourceUrl = target.src || 'N/A'
        } else if (target instanceof HTMLLinkElement) {
          resourceUrl = target.href || 'N/A'
        }
        logger.warn('[Resource Load Error] 资源加载失败:', {
          tagName: target.tagName,
          url: resourceUrl,
          message: event.message
        })
      }
      return
    }
  }

  // 忽略 ResizeObserver 相关的警告（这是浏览器的已知问题，不是真正的错误）
  // ResizeObserver loop completed with undelivered notifications
  // 这通常来自 Ant Design Vue 组件内部使用 ResizeObserver 时触发的浏览器警告
  if (event.message && event.message.includes('ResizeObserver')) {
    // 开发环境记录警告（用于调试），但不显示给用户
    if (import.meta.env.DEV) {
      logger.debug('[ResizeObserver Warning] ResizeObserver 警告（可忽略）:', {
        message: event.message,
        filename: event.filename
      })
    }
    // 阻止默认行为（不在控制台显示错误）
    event.preventDefault()
    return
  }

  // 记录错误日志
  logger.error('[Global Error Handler] 全局错误:', {
    message: event.message,
    filename: event.filename,
    lineno: event.lineno,
    colno: event.colno,
    error: event.error,
    stack: event.error?.stack
  })

  // 显示用户友好的错误提示
  const errorMessage = event.message || '发生了未知错误'
  message.error({
    content: `系统错误: ${errorMessage}`,
    duration: 5
  })
})

// 等待路由准备就绪后再挂载应用，避免动态导入模块失败
router.isReady().then(() => {
  app.mount('#app')
}).catch((error) => {
  logger.error('[Router] 路由初始化失败:', error)
  // 即使路由初始化失败，也尝试挂载应用，让错误页面可以显示
  app.mount('#app')
})
