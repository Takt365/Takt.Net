// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/router
// 文件名称：index.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Vue Router 路由配置，包含路由守卫、动态路由加载、权限控制等
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw, NavigationGuardNext, RouteLocationRaw } from 'vue-router'
import { nextTick, unref } from 'vue'
import { useUserStore } from '@/stores/identity/user'
import { usePermissionStore } from '@/stores/identity/permission'
import { useMenuStore } from '@/stores/identity/menu'
import { useSignalRStore } from '@/stores/identity/signalr'
import { useTranslationStore } from '@/stores/routine/localization/translation'
import { useDictDataStore } from '@/stores/routine/dict/dict-data'
import { loadTranslationsFromBackend } from '@/locales'
import { eventBus, AuthEvents } from '@/utils/eventBus'
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'
import i18n from '@/locales'
import { message } from 'ant-design-vue'

// 配置 NProgress
NProgress.configure({ showSpinner: false })

// 关于页静态子路由（隐私政策、服务条款），与动态菜单一起挂载到根布局下，关于页直接引用
const ABOUT_STATIC_CHILDREN: RouteRecordRaw[] = [
  {
    path: '/privacy',
    name: 'AboutPrivacy',
    component: () => import('@/views/about/privacy/index.vue'),
    meta: { title: '隐私政策', requiresAuth: true }
  },
  {
    path: '/terms',
    name: 'AboutTerms',
    component: () => import('@/views/about/terms/index.vue'),
    meta: { title: '服务条款', requiresAuth: true }
  }
]

// 基础路由
const routes: RouteRecordRaw[] = [
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/login/index.vue'),
    meta: {
      title: 'layouts.route.loginTitle',
      requiresAuth: false
    }
  },
  {
    path: '/',
    name: 'Root',
    component: () => import('@/layouts/index.vue'),
    // 不在这里设置 redirect，而是在路由守卫中根据动态路由加载情况处理
    meta: {
      requiresAuth: true
    },
    children: []
  },
  {
    path: '/401',
    name: 'Unauthorized',
    component: () => import('@/views/error/401.vue'),
    meta: {
      title: 'error.unauthorized.title',
      requiresAuth: false
    }
  },
  {
    path: '/403',
    name: 'Forbidden',
    component: () => import('@/views/error/403.vue'),
    meta: {
      title: 'error.forbidden.title',
      requiresAuth: false
    }
  },
  {
    path: '/500',
    name: 'ServerError',
    component: () => import('@/views/error/500.vue'),
    meta: {
      title: 'error.servererror.title',
      requiresAuth: false
    }
  },
  {
    path: '/503',
    name: 'ServiceUnavailable',
    component: () => import('@/views/error/503.vue'),
    meta: {
      title: 'error.serviceunavailable.title',
      requiresAuth: false
    }
  },
  {
    path: '/404',
    name: 'NotFoundPage',
    component: () => import('@/views/error/404.vue'),
    meta: {
      title: 'error.notfound.title',
      requiresAuth: false
    }
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: () => import('@/views/error/404.vue'),
    meta: {
      title: 'error.notfound.title',
      requiresAuth: false
    }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

/**
 * 移除值为 `undefined` 的键，满足 `exactOptionalPropertyTypes: true` 下 `RouteRecordRaw` 的要求：
 * 可选属性只能「省略」，不能显式赋 `undefined`（展开 `RouteRecordNormalized` 时常带 `redirect: undefined` 等）。
 */
function dropUndefinedRecordKeys(record: Record<string, unknown>): Record<string, unknown> {
  const next: Record<string, unknown> = { ...record }
  for (const key of Object.keys(next)) {
    if (next[key] === undefined) {
      delete next[key]
    }
  }
  return next
}

// 辅助函数：获取根路由
const getRootRoute = () => {
  return router.getRoutes().find(r => r.path === '/' && (r.name === 'Root' || !r.name))
}

// 辅助函数：处理根路径重定向
const handleRootRedirect = (next: NavigationGuardNext) => {
  next('/dashboard/workspace')
  NProgress.done()
}

// 辅助函数：清除动态路由（退出登录时调用）
const clearDynamicRoutes = () => {
  const rootRoute = getRootRoute()
  if (rootRoute && rootRoute.name) {
    // 移除旧的根路由
    router.removeRoute(rootRoute.name)
    // 重新添加空的根路由
    router.addRoute({
      path: '/',
      name: 'Root',
      component: () => import('@/layouts/index.vue'),
      meta: { requiresAuth: true },
      children: []
    })
  }
}

// 辅助函数：注册动态路由到根路由
const registerDynamicRoutes = (routes: RouteRecordRaw[]) => {
  const currentRootRoute = getRootRoute()
  if (currentRootRoute && currentRootRoute.name) {
    const currentChildren = currentRootRoute.children || []
    const existingNames = new Set(currentChildren.map(r => r.name).filter(Boolean))
    const newRoutes = routes.filter(r => !existingNames.has(r.name))
    const newChildren = [...ABOUT_STATIC_CHILDREN, ...currentChildren, ...newRoutes]
    
    // 移除旧的根路由
    router.removeRoute(currentRootRoute.name)
    
    // 添加新的根路由（包含所有 children）
    const mergedRoot = {
      ...(currentRootRoute as unknown as Record<string, unknown>),
      children: newChildren
    }
    router.addRoute(dropUndefinedRecordKeys(mergedRoot) as unknown as RouteRecordRaw)
    
    return true
  }
  return false
}

// 辅助函数：跳转到登录页
const redirectToLogin = (next: NavigationGuardNext, redirectPath?: string) => {
  // 如果已经在登录页，不再重复跳转（避免 redirect 参数无限叠加）
  if (redirectPath && redirectPath.startsWith('/login')) {
    redirectPath = undefined
  }
  
  const target: RouteLocationRaw = redirectPath
    ? { path: '/login', query: { redirect: redirectPath } }
    : { path: '/login' }
  next(target)
  NProgress.done()
}

// 辅助函数：加载后端翻译数据
const loadBackendTranslations = async () => {
  const startTime = performance.now()
  logger.debug('[Perf] 开始加载后端翻译数据...')
  try {
    const currentLocale = unref(i18n.global.locale)
    await loadTranslationsFromBackend(currentLocale, 'Frontend')
    const duration = performance.now() - startTime
    logger.info(`[Perf] 后端翻译数据加载完成，耗时: ${duration.toFixed(2)}ms`)
  } catch (error) {
    const duration = performance.now() - startTime
    logger.error(`[Perf] 加载后端翻译数据失败，耗时: ${duration.toFixed(2)}ms`, error)
    // 翻译数据加载失败不影响路由跳转，继续执行
  }
}

// 辅助函数：加载字典数据
const loadDictData = async () => {
  const startTime = performance.now()
  logger.debug('[Perf] 开始加载字典数据...')
  try {
    const dictDataStore = useDictDataStore()
    await dictDataStore.loadAllDictData()
    const duration = performance.now() - startTime
    logger.info(`[Perf] 字典数据加载完成，耗时: ${duration.toFixed(2)}ms`)
  } catch (error) {
    const duration = performance.now() - startTime
    logger.error(`[Perf] 加载字典数据失败，耗时: ${duration.toFixed(2)}ms`, error)
  }
}

// 辅助函数：连接 SignalR
const connectSignalR = async () => {
  const startTime = performance.now()
  logger.debug('[Perf] 开始连接 SignalR...')
  try {
    const signalRStore = useSignalRStore()
    if (!signalRStore.isConnected) {
      await signalRStore.connect()
      const duration = performance.now() - startTime
      logger.info(`[Perf] SignalR 连接成功，耗时: ${duration.toFixed(2)}ms`)
    } else {
      const duration = performance.now() - startTime
      logger.debug(`[Perf] SignalR 已连接，跳过，耗时: ${duration.toFixed(2)}ms`)
    }
  } catch (error: unknown) {
    const duration = performance.now() - startTime
    const message = error instanceof Error ? error.message : String(error)
    logger.error(`[Perf] SignalR 连接失败，耗时: ${duration.toFixed(2)}ms`, message)
  }
}

// 路由守卫
router.beforeEach(async (to, _from, next) => {
  const guardStartTime = performance.now()
  logger.info(`[Perf] ===== 路由守卫开始: ${to.path} =====`)
  NProgress.start()

  const userStore = useUserStore()
  const permissionStore = usePermissionStore()
  const menuStore = useMenuStore()

  // 调试日志：输出 Token 状态
  if (import.meta.env.DEV) {
    logger.debug('[Router Guard] 检查认证状态:', {
      hasToken: !!userStore.token,
      tokenLength: userStore.token?.length ?? 0,
      hasUserInfo: !!userStore.userInfo,
      routePath: to.path
    })
  }

  // 检查是否需要登录
  if (to.meta.requiresAuth !== false) {
    if (!userStore.token) {
      logger.warn('[Router Guard] 未找到 Token，重定向到登录页')
      redirectToLogin(next, to.fullPath)
      return
    }

    // 检查是否已加载用户信息
    if (!userStore.userInfo) {
      const phase1Start = performance.now()
      logger.info('[Perf] [阶段1] 开始加载翻译与字典数据...')
      try {
        // 优化：并行加载翻译和字典数据，而不是串行
        const [translationPromise, dictPromise] = [
          loadBackendTranslations(),
          loadDictData()
        ]
        
        // 等待翻译和字典数据加载完成
        await Promise.all([translationPromise, dictPromise])
        const phase1Duration = performance.now() - phase1Start
        logger.info(`[Perf] [阶段1] 翻译和字典数据加载完成，耗时: ${phase1Duration.toFixed(2)}ms`)
        
        // 并行加载用户信息和SignalR连接
        const phase2Start = performance.now()
        logger.info('[Perf] [阶段2] 开始加载用户信息和SignalR...')
        const [userInfoPromise, signalRPromise] = [
          userStore.getUserInfo(),
          connectSignalR()
        ]
        
        const userInfo = await userInfoPromise
        const phase2Duration = performance.now() - phase2Start
        logger.info(`[Perf] [阶段2] 用户信息加载完成，耗时: ${phase2Duration.toFixed(2)}ms`)
        
        if (userInfo?.permissions) {
          permissionStore.permissions = userInfo.permissions
        }
        
        // SignalR连接在后台继续，不阻塞页面渲染
        signalRPromise.catch(error => {
          logger.error('[Router Guard] SignalR 连接失败:', error)
        })
      } catch {
        userStore.logout()
        redirectToLogin(next, to.fullPath)
        return
      }
    } else {
      // 如果用户信息已存在但权限列表为空，则设置权限
      const userInfo = userStore.userInfo
      if (userInfo?.permissions && permissionStore.permissions.length === 0) {
        permissionStore.permissions = userInfo.permissions
      } else if (!userInfo?.permissions && import.meta.env.DEV) {
        logger.warn('[Router Guard] 用户信息已存在但权限列表为空或未定义')
      }
      
      // 检查翻译数据是否已加载（如果未加载则加载）
      const currentLocale = unref(i18n.global.locale)
      const translationStore = useTranslationStore()
      const dictDataStore = useDictDataStore()
      
      const promises: Promise<void>[] = []
      
      if (!translationStore.isLoaded(currentLocale, 'Frontend')) {
        promises.push(loadBackendTranslations())
      }
      
      if (!dictDataStore.isLoaded) {
        promises.push(loadDictData())
      }
      
      if (promises.length > 0) {
        await Promise.all(promises)
        logger.info('[Router Guard] 翻译和字典数据加载完成')
      }
      
      // 检查并连接 SignalR（刷新页面时，如果已登录但未连接）
      await connectSignalR()
    }

    // 检查是否已加载权限路由
    // 注意：刷新页面后 Pinia store 状态会丢失，但动态路由已注册到 router 中
    // 所以需要检查根路由是否已有子路由，而不仅仅依赖 isRoutesLoaded 标志
    const rootRoute = getRootRoute()
    const hasDynamicRoutes = rootRoute?.children && rootRoute.children.length > 0
    
    // 如果路由未加载且根路由没有动态子路由，则需要加载
    if (!menuStore.isRoutesLoaded && !hasDynamicRoutes) {
      const routeLoadStart = performance.now()
      logger.info('[Perf] [阶段3] 开始加载权限路由...')
      try {
        await menuStore.generateRoutes()
        const routeGenDuration = performance.now() - routeLoadStart
        logger.info(`[Perf] [阶段3] 权限路由生成完成，路由数量: ${menuStore.routes.length}，耗时: ${routeGenDuration.toFixed(2)}ms`)
        
        // 将动态路由添加到根路由的 children 中
        const registerStart = performance.now()
        const registered = registerDynamicRoutes(menuStore.routes)
        if (!registered) {
          // 如果找不到根路由，直接添加到根路由下
          menuStore.routes.forEach((route) => {
            router.addRoute('/', route)
          })
        }
        const registerDuration = performance.now() - registerStart
        logger.debug(`[Perf] [阶段3] 路由注册完成，耗时: ${registerDuration.toFixed(2)}ms`)
        
        // 等待 Vue Router 更新路由表（确保路由已完全注册）
        const nextTickStart = performance.now()
        await nextTick()
        // 再等一帧，确保 Pinia 菜单状态已写入、布局侧栏能拿到 menuList（避免首次登录左侧菜单不显示需刷新的问题）
        await nextTick()
        const nextTickDuration = performance.now() - nextTickStart
        logger.debug(`[Perf] [阶段3] nextTick 完成，耗时: ${nextTickDuration.toFixed(2)}ms`)
        
        // 如果访问的是根路径，重定向到 /dashboard/workspace（由后端菜单数据生成）
        if (to.path === '/') {
          handleRootRedirect(next)
          return
        }
        
        // 路由注册后，使用 next(to.fullPath) 强制重新导航以确保路由匹配
        // 这样可以确保 Vue Router 重新匹配路由表，识别新注册的动态路由
        // 使用 replace: true 避免在历史记录中留下中间状态
        next({ path: to.fullPath, replace: true })
        NProgress.done()
        return
      } catch (error) {
        logger.error('[Router Guard] 加载权限路由失败:', error)
        redirectToLogin(next)
        return
      }
    } else {
      // 路由已加载或已注册的情况
      
      // 场景1：刷新页面 - 路由已注册但 store 状态丢失
      if (hasDynamicRoutes && !menuStore.isRoutesLoaded) {
        // 刷新页面后，动态路由已注册但 store 状态丢失
        // 需要恢复 store 状态，但不需要重新加载路由
        menuStore.isRoutesLoaded = true
        // 从已注册的路由中恢复 routes（用于保持状态一致性）
        if (rootRoute?.children) {
          menuStore.routes = rootRoute.children
        }
      }
      // 场景2：退出后再次登录 - 路由已注册但菜单数据为空
      else if (hasDynamicRoutes && menuStore.isRoutesLoaded && (!menuStore.menuList || menuStore.menuList.length === 0)) {
        // 退出登录后再次登录时，路由可能还在但菜单数据被清空
        // 需要清除旧路由并重新加载
        try {
          // 清除旧路由
          if (rootRoute && rootRoute.name) {
            router.removeRoute(rootRoute.name)
            // 重新添加空的根路由
            router.addRoute({
              path: '/',
              name: 'Root',
              component: () => import('@/layouts/index.vue'),
              meta: { requiresAuth: true },
              children: []
            })
          }
          // 重置状态
          menuStore.isRoutesLoaded = false
          // 重新加载路由和菜单
          await menuStore.generateRoutes()
          logger.info('[Router Guard] 再次登录：路由和菜单重新加载成功', {
            routesCount: menuStore.routes.length,
            menuCount: menuStore.menuList.length
          })
          
          // 注册新路由
          const registered = registerDynamicRoutes(menuStore.routes)
          if (!registered) {
            menuStore.routes.forEach((route) => {
              router.addRoute('/', route)
            })
          }
          
          // 加载后端翻译数据
          await loadBackendTranslations()
          
          // 等待路由更新
          await nextTick()
          
          // 如果访问的是根路径，重定向到 /dashboard/workspace
          if (to.path === '/') {
            handleRootRedirect(next)
            return
          }
          
          // 重新导航以确保路由匹配
          next({ path: to.fullPath, replace: true })
          NProgress.done()
          return
        } catch (error) {
          logger.error('[Router Guard] 再次登录：重新加载路由和菜单失败:', error)
          redirectToLogin(next)
          return
        }
      }
      // 场景3：正常情况 - 路由和菜单都已加载
      
      // 如果访问的是根路径，重定向到 /dashboard/workspace（由后端菜单数据生成）
      if (to.path === '/') {
        handleRootRedirect(next)
        return
      }
    }
  }

  // 刷新页面时，动态路由尚未加载，所有非静态路径都会匹配 catch-all (NotFound)
  // catch-all 的 requiresAuth: false 导致上方认证块被跳过，直接渲染 404
  // 此处补载：若用户已登录且命中 NotFound，先加载动态路由再重新导航
  if (to.name === 'NotFound' && userStore.token) {
    if (!userStore.userInfo) {
      try {
        await Promise.all([loadBackendTranslations(), loadDictData()])
        logger.info('[Router Guard] 翻译和字典数据加载完成')
        const userInfo = await userStore.getUserInfo()
        if (userInfo?.permissions) {
          permissionStore.permissions = userInfo.permissions
        }
        await connectSignalR()
      } catch {
        userStore.logout()
        redirectToLogin(next, to.fullPath)
        return
      }
    }

    const rootRoute = getRootRoute()
    const hasDynamicRoutes = rootRoute?.children && rootRoute.children.length > 0

    if (!menuStore.isRoutesLoaded && !hasDynamicRoutes) {
      try {
        await menuStore.generateRoutes()
        logger.info('[Router Guard] 权限路由加载成功，路由数量:', menuStore.routes.length)

        const registered = registerDynamicRoutes(menuStore.routes)
        if (!registered) {
          menuStore.routes.forEach((route) => {
            router.addRoute('/', route)
          })
        }

        await nextTick()
        await nextTick()

        next({ path: to.fullPath, replace: true })
        NProgress.done()
        return
      } catch (error) {
        logger.error('[Router Guard] 加载权限路由失败:', error)
        redirectToLogin(next)
        return
      }
    }
  }

  // 检查权限
  // 注意：如果路由是从菜单生成的（路由链中有 menuType），则不需要检查权限
  // 因为菜单已经根据用户权限过滤过了，能显示的菜单用户肯定有权限
  // 只有静态路由（路由链中没有 menuType）且有 permission 时才检查权限
  const hasMenuType = to.matched.some(record => record.meta.menuType !== undefined)
  
  // 只有静态路由（没有 menuType）且有 permission 时才检查权限
  if (to.meta.permission && !hasMenuType) {
    if (!permissionStore.hasPermission(to.meta.permission as string)) {
      logger.warn('[Router Guard] 权限检查失败:', {
        path: to.path,
        permission: to.meta.permission,
        userPermissions: permissionStore.permissions
      })
      next('/403')
      NProgress.done()
      const totalDuration = performance.now() - guardStartTime
      logger.info(`[Perf] ===== 路由守卫结束 (403): ${to.path}，总耗时: ${totalDuration.toFixed(2)}ms =====`)
      return
    }
  }

  next()
  const totalDuration = performance.now() - guardStartTime
  logger.info(`[Perf] ===== 路由守卫结束: ${to.path}，总耗时: ${totalDuration.toFixed(2)}ms =====`)
})

// 处理路由错误（包括动态导入失败）
// 使用 sessionStorage 记录重试次数，避免无限循环
const MAX_RETRY_COUNT = 3
const RETRY_KEY = 'router_retry_count'
const LAST_ERROR_KEY = 'router_last_error'

router.onError((error) => {
  logger.error('[Router Error] 路由错误:', error)
  
  // 如果是动态导入失败，不要自动刷新页面，而是显示错误并停留在当前页
  if (error.message && error.message.includes('Failed to fetch dynamically imported module')) {
    // 获取当前重试次数
    const retryCount = parseInt(sessionStorage.getItem(RETRY_KEY) || '0', 10)
    const lastError = sessionStorage.getItem(LAST_ERROR_KEY)
    
    // 检查是否是相同的错误（避免重复处理）
    const currentError = error.message
    if (lastError === currentError && retryCount >= MAX_RETRY_COUNT) {
      logger.error('[Router Error] 重试次数已达上限，停止重试。请检查文件是否存在或服务器是否正常运行。')
      sessionStorage.removeItem(RETRY_KEY)
      sessionStorage.removeItem(LAST_ERROR_KEY)
      
      // 显示错误提示，但不刷新页面
      message.error({
        content: (i18n.global.t as (key: string) => string)('layouts.route.loadFail') || '页面加载失败，请稍后重试',
        duration: 5
      })
      return
    }
    
    // 记录错误但不自动刷新
    if (import.meta.env.DEV) {
      const newRetryCount = lastError === currentError ? retryCount + 1 : 1
      sessionStorage.setItem(RETRY_KEY, String(newRetryCount))
      sessionStorage.setItem(LAST_ERROR_KEY, currentError)
      
      logger.warn(`[Router Error] 动态导入模块失败 (${newRetryCount}/${MAX_RETRY_COUNT})，请检查开发服务器状态`)
      
      // 显示错误提示，让用户手动决定是否刷新
      message.warning({
        content: `页面加载失败 (${newRetryCount}/${MAX_RETRY_COUNT})，请检查网络连接或稍后重试`,
        duration: 5
      })
    } else {
      // 生产环境显示友好错误提示
      logger.error('[Router Error] 动态导入模块失败，请检查文件是否存在')
      message.error({
        content: '页面加载失败，请刷新页面重试',
        duration: 5
      })
    }
  } else {
    // 其他路由错误，显示错误提示但不刷新页面
    logger.error('[Router Error] 路由导航错误:', error.message)
    message.error({
      content: '页面导航失败，请重试',
      duration: 5
    })
  }
})

router.afterEach(() => {
  NProgress.done()
  // 路由成功加载后，清除重试计数器
  sessionStorage.removeItem(RETRY_KEY)
  sessionStorage.removeItem(LAST_ERROR_KEY)
})

// 导出清除动态路由的函数（供退出登录时调用）
export { clearDynamicRoutes }

/**
 * 确保菜单与动态路由已加载（供布局挂载时补救：首次登录若菜单为空则主动拉取，避免左侧菜单不显示）。
 * 若已加载则直接返回。
 */
export async function ensureMenuAndRoutesLoaded() {
  const menuStore = useMenuStore()
  const rootRoute = getRootRoute()
  const hasDynamicRoutes = rootRoute?.children && rootRoute.children.length > 0
  if (menuStore.isRoutesLoaded && menuStore.menuList.length > 0 && hasDynamicRoutes) {
    return
  }
  await menuStore.generateRoutes()
  const registered = registerDynamicRoutes(menuStore.routes)
  if (!registered) {
    menuStore.routes.forEach((route) => {
      router.addRoute('/', route)
    })
  }
  await nextTick()
}

// ========== 事件总线订阅 ==========

/**
 * 监听跳转登录页事件
 */
eventBus.$on(AuthEvents.RedirectToLogin, () => {
  const currentPath = router.currentRoute.value.fullPath
  // 跳转到登录页，并记住来源页面（用于登录后重定向）
  // 注意：检查是否已经在登录页（包括带 redirect 参数的情况），避免无限叠加 redirect
  if (!currentPath.startsWith('/login')) {
    router.push({
      path: '/login',
      query: { redirect: currentPath }
    })
  }
})

export default router
