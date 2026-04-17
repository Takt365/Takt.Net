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
import { nextTick } from 'vue'
import { useUserStore } from '@/stores/identity/user'
import { usePermissionStore } from '@/stores/identity/permission'
import { useMenuStore } from '@/stores/identity/menu'
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'
import i18n from '@/locales'

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
    path: '/403',
    name: 'Forbidden',
    component: () => import('@/views/error/403.vue'),
    meta: {
      title: '403',
      requiresAuth: false
    }
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: () => import('@/views/error/404.vue'),
    meta: {
      title: '404'
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
  const target: RouteLocationRaw = redirectPath
    ? { path: '/login', query: { redirect: redirectPath } }
    : { path: '/login' }
  next(target)
  NProgress.done()
}

// 辅助函数：加载后端翻译数据
const loadBackendTranslations = async () => {
  try {
    const { loadTranslationsFromBackend } = await import('@/locales')
    const { unref } = await import('vue')
    const i18n = await import('@/locales')
    const currentLocale = unref(i18n.default.global.locale)
    await loadTranslationsFromBackend(currentLocale, 'Frontend')
    logger.info('[Router Guard] 后端翻译数据加载成功')
  } catch (error) {
    logger.error('[Router Guard] 加载后端翻译数据失败:', error)
    // 翻译数据加载失败不影响路由跳转，继续执行
  }
}

// 辅助函数：加载字典数据
const loadDictData = async () => {
  try {
    const { useDictDataStore } = await import('@/stores/routine/dict/dictdata')
    const dictDataStore = useDictDataStore()
    await dictDataStore.loadAllDictData()
    logger.info('[Router Guard] 字典数据加载成功')
  } catch (error) {
    logger.error('[Router Guard] 加载字典数据失败:', error)
  }
}

// 辅助函数：连接 SignalR
const connectSignalR = async () => {
  try {
    const { useSignalRStore } = await import('@/stores/identity/signalr')
    const signalRStore = useSignalRStore()
    if (!signalRStore.isConnected) {
      await signalRStore.connect()
      logger.info('[Router Guard] SignalR 连接成功')
    }
  } catch (error: unknown) {
    const message = error instanceof Error ? error.message : String(error)
    logger.error('[Router Guard] SignalR 连接失败:', message)
  }
}

// 路由守卫
router.beforeEach(async (to, _from, next) => {
  NProgress.start()

  const userStore = useUserStore()
  const permissionStore = usePermissionStore()
  const menuStore = useMenuStore()

  // 检查是否需要登录
  if (to.meta.requiresAuth !== false) {
    if (!userStore.token) {
      redirectToLogin(next, to.fullPath)
      return
    }

    // 检查是否已加载用户信息
    if (!userStore.userInfo) {
      try {
        const userInfo = await userStore.getUserInfo()
        // 获取用户信息后，设置权限列表
        if (userInfo?.permissions) {
          permissionStore.permissions = userInfo.permissions
        }
        
        // 获取用户信息后，优先加载翻译和字典数据
        await Promise.all([
          loadBackendTranslations(),
          loadDictData()
        ])
        logger.info('[Router Guard] 翻译和字典数据加载完成')
        
        // 连接 SignalR（登录成功后）
        await connectSignalR()
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
      const { unref } = await import('vue')
      const i18n = await import('@/locales')
      const currentLocale = unref(i18n.default.global.locale)
      const { useTranslationStore } = await import('@/stores/routine/localization/translation')
      const translationStore = useTranslationStore()
      const { useDictDataStore } = await import('@/stores/routine/dict/dictdata')
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
      try {
        await menuStore.generateRoutes()
        logger.info('[Router Guard] 权限路由加载成功，路由数量:', menuStore.routes.length)
        
        // 将动态路由添加到根路由的 children 中
        const registered = registerDynamicRoutes(menuStore.routes)
        if (!registered) {
          // 如果找不到根路由，直接添加到根路由下
          menuStore.routes.forEach((route) => {
            router.addRoute('/', route)
          })
        }
        
        // 等待 Vue Router 更新路由表（确保路由已完全注册）
        await nextTick()
        // 再等一帧，确保 Pinia 菜单状态已写入、布局侧栏能拿到 menuList（避免首次登录左侧菜单不显示需刷新的问题）
        await nextTick()
        
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
      return
    }
  }

  next()
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

export default router
