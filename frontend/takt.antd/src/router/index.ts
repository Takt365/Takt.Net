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
import type { RouteRecordRaw } from 'vue-router'
import { nextTick } from 'vue'
import type { UserInfo } from '@/types/identity/auth'
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'
import i18n from '@/locales'

/** 路由守卫上下文，由 main 注入（router 不依赖 stores） */
export interface GuardContext {
  hasToken(): boolean
  hasUserInfo(): boolean
  loadUserInfo(): Promise<UserInfo | null>
  getUserInfo(): UserInfo | null
  setPermissions(permissions: string[]): void
  hasPermission(permission: string): boolean
  loadBackendTranslations(): Promise<void>
  loadDictData(): Promise<void>
  connectSignalR(): Promise<void>
  generateRoutes(): Promise<RouteRecordRaw[]>
  getIsRoutesLoaded(): boolean
  getHasDynamicRoutes(): boolean
  getRoutes(): RouteRecordRaw[]
  getMenuListLength(): number
  syncRoutesStateFromRouter(): void
  resetMenuAndRegenerateRoutes(): Promise<void>
  logout(): Promise<void>
}

let guardContext: GuardContext | null = null
export function setGuardContext(ctx: GuardContext): void {
  guardContext = ctx
}

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
    path: '/400',
    name: 'BadRequest',
    component: () => import('@/views/error/bad-request/index.vue'),
    meta: { title: '400', requiresAuth: false }
  },
  {
    path: '/401',
    name: 'Unauthorized',
    component: () => import('@/views/error/unauthorized/index.vue'),
    meta: { title: '401', requiresAuth: false }
  },
  {
    path: '/403',
    name: 'Forbidden',
    component: () => import('@/views/error/forbidden/index.vue'),
    meta: { title: '403', requiresAuth: false }
  },
  {
    path: '/500',
    name: 'InternalServerError',
    component: () => import('@/views/error/internal-server-error/index.vue'),
    meta: { title: '500', requiresAuth: false }
  },
  {
    path: '/502',
    name: 'BadGateway',
    component: () => import('@/views/error/bad-gateway/index.vue'),
    meta: { title: '502', requiresAuth: false }
  },
  {
    path: '/503',
    name: 'ServiceUnavailable',
    component: () => import('@/views/error/service-unavailable/index.vue'),
    meta: { title: '503', requiresAuth: false }
  },
  {
    path: '/504',
    name: 'GatewayTimeout',
    component: () => import('@/views/error/gateway-timeout/index.vue'),
    meta: { title: '504', requiresAuth: false }
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: () => import('@/views/error/not-found/index.vue'),
    meta: { title: '404' }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

// 辅助函数：获取根路由
const getRootRoute = () => {
  return router.getRoutes().find(r => r.path === '/' && (r.name === 'Root' || !r.name))
}

/** 处理根路径重定向（Vue Router 4 推荐 return 替代 next） */
const handleRootRedirect = (): string => {
  NProgress.done()
  return '/dashboard/workspace'
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
    router.addRoute({
      ...currentRootRoute,
      children: newChildren
    })
    
    return true
  }
  return false
}

/** 跳转到登录页（Vue Router 4 推荐 return 替代 next） */
const redirectToLogin = (redirectPath?: string): { path: string; query?: { redirect: string } } => {
  NProgress.done()
  return {
    path: '/login',
    query: redirectPath ? { redirect: redirectPath } : undefined
  }
}

// 路由守卫（使用 main 注入的 guardContext；通过 return 返回值，不再使用 next()）
router.beforeEach(async (to, _from) => {
  NProgress.start()
  if (!guardContext) {
    if (to.meta.requiresAuth !== false) return redirectToLogin(to.fullPath)
    return
  }

  if (to.meta.requiresAuth !== false) {
    if (!guardContext.hasToken()) return redirectToLogin(to.fullPath)
    if (!guardContext.hasUserInfo()) {
      try {
        const userInfo = await guardContext.loadUserInfo()
        if (userInfo?.permissions) guardContext.setPermissions(userInfo.permissions)
        await Promise.all([guardContext.loadBackendTranslations(), guardContext.loadDictData()])
        await guardContext.connectSignalR()
      } catch (error) {
        await guardContext.logout()
        return redirectToLogin(to.fullPath)
      }
    } else {
      const userInfo = guardContext.getUserInfo()
      if (userInfo?.permissions) guardContext.setPermissions(userInfo.permissions)
      await guardContext.loadBackendTranslations()
      await guardContext.loadDictData()
      await guardContext.connectSignalR()
    }

    const hasDynamicRoutes = guardContext.getHasDynamicRoutes()

    if (!guardContext.getIsRoutesLoaded() && !hasDynamicRoutes) {
      try {
        await guardContext.generateRoutes()
        const routes = guardContext.getRoutes()
        const registered = registerDynamicRoutes(routes)
        if (!registered) routes.forEach((route) => router.addRoute('/', route))
        await nextTick()
        if (to.path === '/') return handleRootRedirect()
        NProgress.done()
        return { path: to.fullPath, replace: true }
      } catch (error) {
        logger.error('[Router Guard] 加载权限路由失败:', error)
        return redirectToLogin()
      }
    }

    if (hasDynamicRoutes && !guardContext.getIsRoutesLoaded()) {
      guardContext.syncRoutesStateFromRouter()
    } else if (hasDynamicRoutes && guardContext.getIsRoutesLoaded() && guardContext.getMenuListLength() === 0) {
      try {
        await guardContext.resetMenuAndRegenerateRoutes()
        await guardContext.loadBackendTranslations()
        await nextTick()
        if (to.path === '/') return handleRootRedirect()
        NProgress.done()
        return { path: to.fullPath, replace: true }
      } catch (error) {
        logger.error('[Router Guard] 再次登录：重新加载路由和菜单失败:', error)
        return redirectToLogin()
      }
    }

    if (to.path === '/') return handleRootRedirect()
  }

  const hasMenuType = to.matched.some(record => record.meta.menuType !== undefined)
  if (to.meta.permission && !hasMenuType) {
    if (!guardContext.hasPermission(to.meta.permission as string)) {
      logger.warn('[Router Guard] 权限检查失败:', { path: to.path, permission: to.meta.permission })
      NProgress.done()
      return '/403'
    }
  }
})

// 处理路由错误（包括动态导入失败）
// 使用 sessionStorage 记录重试次数，避免无限循环
const MAX_RETRY_COUNT = 3
const RETRY_KEY = 'router_retry_count'
const LAST_ERROR_KEY = 'router_last_error'

router.onError((error) => {
  logger.error('[Router Error] 路由错误:', error)
  
  // 如果是动态导入失败，尝试重新加载页面
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
      // 显示错误提示
      if (import.meta.env.DEV) {
        alert((i18n.global.t as (key: string) => string)('layouts.route.loadFail'))
      }
      return
    }
    
    // 在开发环境下，延迟后重试
    if (import.meta.env.DEV) {
      const newRetryCount = lastError === currentError ? retryCount + 1 : 1
      sessionStorage.setItem(RETRY_KEY, String(newRetryCount))
      sessionStorage.setItem(LAST_ERROR_KEY, currentError)
      
      logger.warn(`[Router Error] 动态导入模块失败，可能是开发服务器未完全启动，尝试重新加载... (${newRetryCount}/${MAX_RETRY_COUNT})`)
      
      setTimeout(() => {
        window.location.reload()
      }, 1000)
    } else {
      // 生产环境不自动重试，直接显示错误
      logger.error('[Router Error] 动态导入模块失败，请检查文件是否存在')
    }
  }
})

router.afterEach(() => {
  NProgress.done()
  // 路由成功加载后，清除重试计数器
  sessionStorage.removeItem(RETRY_KEY)
  sessionStorage.removeItem(LAST_ERROR_KEY)
})

// 导出清除动态路由的函数（供退出登录时调用）
export { clearDynamicRoutes, registerDynamicRoutes }

export default router
