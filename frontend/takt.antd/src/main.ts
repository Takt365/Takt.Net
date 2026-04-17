// Vue 核心
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { watchEffect } from 'vue'
import { message } from 'ant-design-vue'

// 应用入口和路由
import App from './App.vue'
import router from './router'
import { logger } from '@/utils/logger'
import { DateTimeHelper, formatDateTime, formatDate, formatTime, formatDateTimeShort, formatDateCN, formatDateTimeCN, formatRelative, formatFriendly, getTimestamp, getTimestampSeconds, isToday, isYesterday, isThisWeek, isThisYear, diffDateTime, addDateTime, startOfDay, endOfDay } from '@/utils/datetime'
import { MaskHelper } from '@/utils/mask'
import { TaktPasswordHelper } from '@/utils/password'
import { RegexHelper, RegexPatterns } from '@/utils/regex'
import { UploadHelper } from '@/utils/upload'
import { getDefaultEntityColumns, mergeDefaultColumns } from '@/utils/table-columns'
import { applySettings, watchSettings, notifySettingsChanged } from '@/utils/apply-settings'
import { TaktSignalRManager, signalRManager } from '@/utils/signalr'
import { useDictDataStore } from '@/stores/routine/dict/dictdata'
import { useTranslationStore } from '@/stores/routine/localization/translation'

// 插件配置
import { setupI18n } from './locales'
import i18n from './locales'
// Ant Design Vue 全局注册（保留以兼容性更好）
import Antd from 'ant-design-vue'
import 'ant-design-vue/dist/reset.css'
// FormCreate 表单设计器（fc-designer + form-create 渲染器）
import FcDesigner from '@form-create/antd-designer'
// Font Awesome 免费版
import '@fortawesome/fontawesome-free/css/all.css'
// Flag Icons
import 'flag-icons/css/flag-icons.min.css'
// LogicFlow 全局注册插件：Control、BpmnElement、DndPanel、SelectionSelect（拖拽添加节点、框选）
import LogicFlow from '@logicflow/core'
import { Control, BpmnElement, DndPanel, SelectionSelect } from '@logicflow/extension'
import '@logicflow/extension/lib/style/index.css'
LogicFlow.use(Control)
LogicFlow.use(BpmnElement)
LogicFlow.use(DndPanel)
LogicFlow.use(SelectionSelect)
import '@/assets/styles/index.less'

const app = createApp(App)

// 配置 Vue 警告过滤器：忽略来自 Ant Design Vue Tooltip 的插槽警告
// 这个警告来自库内部实现，不影响功能
// 注意：这个警告是 Ant Design Vue 库的问题，在异步操作中调用插槽函数
if (import.meta.env.DEV) {
  const originalWarn = console.warn
  console.warn = (...args: unknown[]) => {
    const msg = args[0] && typeof args[0] === 'string' ? args[0] : ''
    // 过滤掉 Tooltip 相关的插槽警告
    if (msg.includes('Slot') && msg.includes('invoked outside of the render function')) {
      const stack = new Error().stack || ''
      if (stack.includes('Tooltip') || 
          stack.includes('Trigger') || 
          stack.includes('Popup') || 
          stack.includes('PopupInner') ||
          stack.includes('Align')) {
        return
      }
    }
    // 过滤 LogicFlow 锚点回退提示（加载方案时边引用的锚点可能不存在，已使用默认锚点，不影响功能）
    if (msg.includes('未在节点上找到指定的') && msg.includes('已使用默认锚点')) {
      return
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

// 2. Ant Design Vue（UI 组件库）
app.use(Antd)

// 3. FormCreate 表单设计器（fc-designer + form-create，依赖 Antd）
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

// 动态更新页面标题
watchEffect(() => {
  // 使用类型断言，因为在实际运行时 locale 总是 WritableComputedRef
  // 访问 locale.value 以触发响应式更新
  const localeRef = i18n.global.locale as { value: string }
  void localeRef.value
  // 使用类型断言修复 t 方法的类型问题
  const htmlTitle = String(i18n.global.t('common.app.htmlTitle'))
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
 */
window.addEventListener('unhandledrejection', (event: PromiseRejectionEvent) => {
  // 记录错误日志
  logger.error('[Unhandled Promise Rejection] 未处理的 Promise 拒绝:', {
    reason: event.reason,
    promise: event.promise,
    stack: event.reason instanceof Error ? event.reason.stack : undefined
  })

  // 显示用户友好的错误提示
  const errorMessage = event.reason instanceof Error 
    ? event.reason.message 
    : String(event.reason || '未知错误')
  
  message.error({
    content: `操作失败: ${errorMessage}`,
    duration: 5
  })

  // 阻止默认行为（在控制台显示错误）
  // 注意：开发环境保留默认行为以便调试
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
