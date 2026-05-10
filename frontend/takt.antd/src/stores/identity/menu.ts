// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/stores/identity/menu
// 文件名称：menu.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：菜单和路由管理 Store，管理菜单树、路由生成、权限提取等
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import { defineStore } from 'pinia'
import type { Component, VNode } from 'vue'
import type { RouteRecordRaw } from 'vue-router'
import { getMenuTree } from '@/api/identity/menu'
import type { MenuTree } from '@/types/identity/menu'
import type { MenuProps } from 'ant-design-vue'
import i18n from '@/locales'
import { logger } from '@/utils/logger'
type I18nT = (key: string, values?: object, options?: { locale?: string }) => string
type MenuItemEntry = NonNullable<MenuProps['items']>[number]
type MenuItemNode = {
  key: string
  title: string
  label: string
  titleKey?: string
  icon?: (item: { key: string }) => VNode
  children?: MenuItemEntry[]
}
type I18nReactiveValue = { value: unknown }
const toOptionalString = (value: unknown): string | undefined =>
  typeof value === 'string' && value.length > 0 ? value : undefined

export const useMenuStore = defineStore('menu', () => {
  const routes = ref<RouteRecordRaw[]>([])
  const isRoutesLoaded = ref(false)
  const menuList = ref<MenuTree[]>([])

  // 从菜单树中收集所有用到的图标名（递归）
  const collectIconNames = (menus: MenuTree[]): Set<string> => {
    const names = new Set<string>()
    const walk = (items: MenuTree[]) => {
      if (!items?.length) return
      items.forEach((menu) => {
        const node = menu
        const icon = node.menuIcon
        if (icon && typeof icon === 'string') names.add(icon)
        if (node.children?.length) walk(node.children)
      })
    }
    walk(menus)
    return names
  }

  // 预加载图标模块并填充缓存（与 getMenuTree 并行，减少首屏图标延迟）
  const iconModulePromise = import('@remixicon/vue')

  // 生成路由
  const generateRoutes = async () => {
    const menuStart = performance.now()
    logger.info('[Perf] ===== 菜单路由生成开始 =====')
    
    try {
      // Step 1: 并行请求菜单树和图标模块
      const step1Start = performance.now()
      logger.debug('[Perf] [Menu Step 1] 开始加载菜单树和图标模块...')
      
      const [menus, iconModule] = await Promise.all([
        getMenuTree(),
        iconModulePromise
      ])
      
      const step1Duration = performance.now() - step1Start
      logger.info(`[Perf] [Menu Step 1] 菜单树和图标模块加载完成，耗时: ${step1Duration.toFixed(2)}ms`)
      logger.debug(`[Perf] [Menu Step 1] 菜单数量: ${menus?.length || 0}`)
      
      // 输出日志验证菜单数据结构（仅在开发环境）
      // if (import.meta.env.DEV) {
      //   logger.debug('[Menu Data] 原始菜单数据:', JSON.stringify(menus, null, 2))
      //   logger.debug('[Menu Data] 菜单数据类型:', typeof menus, Array.isArray(menus))
      //   logger.debug('[Menu Data] 菜单数量:', menus?.length || 0)
      // }
      
      // 检查数据是否有效
      if (!menus || !Array.isArray(menus)) {
        logger.error('[Menu Data] 菜单数据格式错误，期望数组，实际:', typeof menus, menus)
        throw new Error('菜单数据格式错误：期望数组')
      }
      
      if (menus.length === 0) {
        logger.warn('[Menu Data] 菜单数据为空，没有可用的菜单')
      }

      
      // Step 2: 收集图标名称并填充缓存
      const step2Start = performance.now()
      logger.debug('[Perf] [Menu Step 2] 开始收集图标名称并填充缓存...')
      
      const iconNames = collectIconNames(menus)
      const mod = iconModule as Record<string, Component>
      iconNames.forEach((name) => {
        if (mod[name]) {
          iconCache.value[name] = markRaw(mod[name])
        }
      })
      
      const step2Duration = performance.now() - step2Start
      logger.info(`[Perf] [Menu Step 2] 图标缓存填充完成，图标数量: ${iconNames.size}，耗时: ${step2Duration.toFixed(2)}ms`)

      // if (menus && menus.length > 0 && import.meta.env.DEV) {
      //   const firstMenu = menus[0] as any
      //   logger.debug('[Menu Data] 第一个菜单项结构（camelCase 格式）:', {
      //     dictLabel: firstMenu.dictLabel,
      //     dictValue: firstMenu.dictValue,
      //     extLabel: firstMenu.extLabel,
      //     extValue: firstMenu.extValue,
      //     transKey: firstMenu.transKey,
      //     menuIcon: firstMenu.menuIcon,
      //     menuType: firstMenu.menuType,
      //     menuStatus: firstMenu.menuStatus,
      //     isVisible: firstMenu.isVisible,
      //     permission: firstMenu.permission,
      //     children: firstMenu.children?.length || 0,
      //     component: firstMenu.component,
      //     menuName: firstMenu.menuName,
      //     menuCode: firstMenu.menuCode,
      //     path: firstMenu.path,
      //     menuL10nKey: firstMenu.menuL10nKey
      //   })
      // }
      
      menuList.value = menus

      // Step 3: 提取权限并更新权限 store
      const step3Start = performance.now()
      logger.debug('[Perf] [Menu Step 3] 开始提取权限...')
      
      const { usePermissionStore } = await import('./permission')
      const permissionStore = usePermissionStore()
      const extractedPermissions = permissionStore.extractPermissionsFromMenuTree(menus)
      
      // 获取用户信息中的权限（如果有）
      const { useUserStore } = await import('./user')
      const userStore = useUserStore()
      const rawUserPermissions = userStore.userInfo?.permissions
      const userPermissions = Array.isArray(rawUserPermissions)
        ? rawUserPermissions.filter((item): item is string => typeof item === 'string')
        : []
      permissionStore.setPermissions(extractedPermissions, userPermissions)
      
      const step3Duration = performance.now() - step3Start
      logger.info(`[Perf] [Menu Step 3] 权限提取完成，菜单权限: ${extractedPermissions.length}，用户权限: ${userPermissions.length}，耗时: ${step3Duration.toFixed(2)}ms`)

      // Step 4: 将菜单树转换为 Vue Router 路由
      const step4Start = performance.now()
      logger.debug('[Perf] [Menu Step 4] 开始生成路由...')
      
      const generatedRoutes = generateRoutesFromMenuTree(menus)
      
      const step4Duration = performance.now() - step4Start
      logger.info(`[Perf] [Menu Step 4] 路由生成完成，路由数量: ${generatedRoutes.length}，耗时: ${step4Duration.toFixed(2)}ms`)
      
      routes.value = generatedRoutes

      isRoutesLoaded.value = true
      
      const totalDuration = performance.now() - menuStart
      logger.info('[Perf] ===== 菜单路由生成完成 =====')
      logger.info(`[Perf] [Menu] 总耗时: ${totalDuration.toFixed(2)}ms`)
      logger.info('[Perf] [Menu] 各步骤耗时:', {
        'Step1_菜单树加载': step1Duration.toFixed(2) + 'ms',
        'Step2_图标缓存': step2Duration.toFixed(2) + 'ms',
        'Step3_权限提取': step3Duration.toFixed(2) + 'ms',
        'Step4_路由生成': step4Duration.toFixed(2) + 'ms'
      })
    } catch (error) {
      const totalDuration = performance.now() - menuStart
      logger.error('[Perf] [Menu] 菜单路由生成失败，总耗时:', totalDuration.toFixed(2), 'ms')
      logger.error('[Menu Store] 生成路由失败:', error)
      throw error
    }
  }

  // 辅助函数：将绝对路径转换为相对路径（相对于父路径）
  // 注意：作为根路由（/）的子路由时，路径必须是绝对路径（带前导斜杠）
  // 这样 Vue Router 才能正确匹配路由
  const convertToRelativePath = (absolutePath: string, parentPath?: string): string => {
    if (parentPath) {
      // 有父路径：计算相对路径（移除父路径部分）
      const pathParts = absolutePath.split('/').filter((p: string) => p)
      const parentParts = parentPath.split('/').filter((p: string) => p)
      const relativePath = pathParts.slice(parentParts.length).join('/')
      return relativePath || absolutePath
    }
    // 没有父路径（顶级路由，作为根路由 / 的子路由）：保持绝对路径（带前导斜杠）
    // 这样 Vue Router 才能正确匹配，例如 /dashboard/workspace 匹配 /dashboard/workspace
    return absolutePath.startsWith('/') ? absolutePath : `/${absolutePath}`
  }

  // 辅助函数：将子路由的绝对路径转换为相对于父路由的相对路径
  const normalizeChildRoutes = (childRoutes: RouteRecordRaw[], parentAbsolutePath: string): RouteRecordRaw[] => {
    return childRoutes.map(child => {
      if (child.path.startsWith('/')) {
        const childParts = child.path.split('/').filter((p: string) => p)
        const parentParts = parentAbsolutePath.split('/').filter((p: string) => p)
        const relativePath = childParts.slice(parentParts.length).join('/')
        return {
          ...child,
          path: relativePath || child.path
        }
      }
      return child
    })
  }

  // 辅助函数：创建路由的 meta 信息
  const createRouteMeta = (
    menuL10nKey: string | undefined,
    menuName: string,
    menuIcon: string | undefined,
    permission: string | undefined,
    menuType: number
  ) => ({
    title: menuL10nKey || menuName,
    titleKey: menuL10nKey,
    icon: menuIcon,
    permission: permission,
    menuType: menuType
  })

  // 辅助函数：从菜单对象提取字段
  const extractMenuFields = (menu: MenuTree) => {
    const node = menu
    return {
      menuId: node.menuId,
      menuName: node.menuName,
      menuCode: node.menuCode,
      menuL10nKey: node.menuL10nKey,
      menuIcon: node.menuIcon,
      path: node.path,
      component: node.component,
      menuType: node.menuType ?? 0,
      menuStatus: node.menuStatus ?? 1,
      isVisible: node.isVisible ?? 1,
      permission: node.permission,
      children: node.children
    }
  }

  // 从菜单树生成路由
  // 规范：
  // 1. Path 必须为绝对路径（如 /dashboard, /identity/user）
  // 2. Component 必须使用别名相对路径（如 identity/user/index，不包含 @/views/ 前缀）
  // 3. 目录类型（MenuType=0）：Path 是绝对路径，Component 为 null
  // 4. 菜单类型（MenuType=1）：Path 是绝对路径，Component 是相对路径
  const generateRoutesFromMenuTree = (menus: MenuTree[], parentPath?: string): RouteRecordRaw[] => {
    const result: RouteRecordRaw[] = []

    // 按 sortOrder 排序（后端已统一转换为 camelCase）
    const sortedMenus = [...menus].sort((a, b) => {
      const aOrder = a.sortOrder ?? 0
      const bOrder = b.sortOrder ?? 0
      return aOrder - bOrder
    })

    sortedMenus.forEach((menu, index) => {
      // 提取菜单字段
      const {
        menuId,
        menuName,
        menuCode,
        menuL10nKey,
        menuIcon,
        path,
        component,
        menuType,
        menuStatus,
        isVisible,
        permission,
        children
      } = extractMenuFields(menu)
      
      // 输出每个菜单项的详细日志（仅在开发环境）
      // if (import.meta.env.DEV) {
      //   logger.debug(`[Menu Processing] 处理第 ${index + 1} 个菜单项:`, {
      //     原始数据: menu,
      //     menuName,
      //     menuCode,
      //     menuL10nKey,
      //     menuIcon,
      //     path,
      //     component,
      //     menuType,
      //     menuStatus,
      //     isVisible,
      //     permission,
      //     childrenCount: children?.length || 0,
      //     parentPath
      //   })
      // }
      
      // 只处理目录（0）和菜单（1），忽略按钮（2）
      if (menuType === 2) {
        // if (import.meta.env.DEV) {
        //   logger.debug(`[Menu Processing] 跳过按钮类型菜单: ${menuName || menuCode}`)
        // }
        return
      }

      // 只处理启用的菜单（MenuStatus = 1 表示启用）
      if (menuStatus !== undefined && menuStatus !== 1) {
        // const statusText = menuStatus === 1 ? '禁用' : menuStatus === 3 ? '锁定' : `未知状态(${menuStatus})`
        // if (import.meta.env.DEV) {
        //   logger.debug(`[Menu Processing] 跳过${statusText}菜单: ${menuName || menuCode}, status: ${menuStatus}`)
        // }
        return
      }

      // 只处理可见的菜单（IsVisible = 1 表示可见）
      if (isVisible !== undefined && isVisible === 0) {
        // if (import.meta.env.DEV) {
        //   logger.debug(`[Menu Processing] 跳过不可见菜单: ${menuName || menuCode}, isVisible: ${isVisible}`)
        // }
        return
      }

      // 判断菜单类型
      if (menuType === 0) {
        // 目录类型（MenuType = 0）：Path 必须为绝对路径，Component 为 null
        // 后端已规范化，所有目录都有 Path
        if (!path) {
          // if (import.meta.env.DEV) {
          //   logger.warn(`[Menu Processing] 目录类型菜单缺少 Path: ${menuName || menuCode}`)
          // }
          return
        }

        // 确保 path 是绝对路径（用于递归处理子菜单）
        const absolutePath = path.startsWith('/') ? path : `/${path}`
        
        // 先递归处理子菜单（使用绝对路径作为父路径）
        const childRoutes = children && children.length > 0
          ? generateRoutesFromMenuTree(children, absolutePath)
          : []

        // 目录必须有子路由才能创建路由（空目录不应该创建路由）
        if (childRoutes.length > 0) {
          const normalizedChildRoutes = normalizeChildRoutes(childRoutes, absolutePath)
          const routePath = convertToRelativePath(absolutePath, parentPath)
          const [firstChild] = normalizedChildRoutes
          
          const route: RouteRecordRaw = {
            path: routePath,
            name: menuCode || `menu_${menuId || index}`,
            component: () => import('@/layouts/router-view.vue'),
            meta: createRouteMeta(menuL10nKey, menuName, menuIcon, permission, menuType),
            children: normalizedChildRoutes,
            ...(firstChild?.path ? { redirect: firstChild.path } : {})
          }
          // if (import.meta.env.DEV) {
          //   logger.debug(`[Menu Processing] 创建目录路由: ${routePath}, 子路由数量: ${normalizedChildRoutes.length}, 原始path: ${absolutePath}`)
          // }
          result.push(route)
        } else {
          // if (import.meta.env.DEV) {
          //   logger.debug(`[Menu Processing] 跳过无子菜单的目录: ${menuName || menuCode}`)
          // }
        }
      } else if (menuType === 1) {
        // 菜单类型（MenuType = 1）：必须有 path 和 component
        if (!path || !component) {
          // if (import.meta.env.DEV) {
          //   logger.debug(`[Menu Processing] 跳过无路径或无组件的菜单: ${menuName || menuCode}, path: ${path}, component: ${component}`)
          // }
          return
        }
        
        // 确保 path 是绝对路径
        const routePath = path.startsWith('/') ? path : `/${path}`
        
        // 先递归处理子菜单（如果有）
        const childRoutes = children && children.length > 0
          ? generateRoutesFromMenuTree(children, routePath)
          : []
        
        // 如果有子路由，创建父路由（菜单也可以有子菜单）
        if (childRoutes.length > 0) {
          const normalizedChildRoutes = normalizeChildRoutes(childRoutes, routePath)
          const finalPath = convertToRelativePath(routePath, parentPath)
          const [firstChild] = normalizedChildRoutes
          
          const route: RouteRecordRaw = {
            path: finalPath,
            name: menuCode || `menu_${menuId || index}`,
            component: () => import('@/layouts/router-view.vue'),
            meta: createRouteMeta(menuL10nKey, menuName, menuIcon, permission, menuType),
            children: normalizedChildRoutes,
            ...(firstChild?.path ? { redirect: firstChild.path } : {})
          }
          // if (import.meta.env.DEV) {
          //   logger.debug(`[Menu Processing] 创建菜单父路由: ${finalPath}, 子路由数量: ${normalizedChildRoutes.length}, 原始path: ${routePath}`)
          // }
          result.push(route)
        } else {
          // 叶子节点（无子路由），创建路由
          const finalPath = convertToRelativePath(routePath, parentPath)
          
          const route: RouteRecordRaw = {
            path: finalPath,
            name: menuCode || `menu_${menuId || index}`,
            component: loadComponent(component),
            meta: createRouteMeta(menuL10nKey, menuName, menuIcon, permission, menuType)
          }
          // if (import.meta.env.DEV) {
          //   logger.debug(`[Menu Processing] 创建菜单路由: ${finalPath}, 组件: ${component}, 原始path: ${routePath}`)
          // }
          result.push(route)
        }
      }
    })

    return result
  }

  // 预加载所有视图组件（使用 import.meta.glob）
  // 这样 Vite 可以在构建时识别所有组件，避免运行时解析错误
  // import.meta.glob 返回的键是完整路径（如：@/views/home/index.vue）
  const componentModules = import.meta.glob('@/views/**/*.vue', { eager: false })
  
  // 动态加载组件
  // 统一使用 @/views/... 别名格式的相对路径
  const loadComponent = (componentPath: string) => {
    // 将后端返回的组件路径转换为 Vue 组件的动态导入
    // 例如：identity/user/index -> () => import('@/views/identity/user/index.vue')
    try {
      // 规范化路径：移除 .vue 后缀（如果有），移除前导斜杠（如果有）
      let normalizedPath = componentPath.replace(/\.vue$/, '').replace(/^\//, '')
      
      // 如果路径以 @/ 开头，移除 @/views/ 或 @/ 前缀，统一处理
      if (normalizedPath.startsWith('@/')) {
        normalizedPath = normalizedPath.replace(/^@\/views\//, '').replace(/^@\//, '')
      }
      
      // 统一构建为 @/views/... 格式（使用别名相对路径）
      const importPath = `@/views/${normalizedPath}.vue`
      
      // 调试日志
      // if (import.meta.env.DEV) {
      //   logger.debug(`[Component Loader] 组件路径转换: ${componentPath} -> ${importPath}`)
      // }
      
      // 从预加载的模块中获取组件
      // import.meta.glob 返回的键可能是 @/views/... 或 /src/views/... 格式
      // 尝试多种可能的键格式进行匹配
      const possibleKeys = [
        importPath, // @/views/...（别名格式）
        importPath.replace('@/views/', '/src/views/'), // /src/views/...（实际路径格式）
      ]
      
      for (const key of possibleKeys) {
        if (componentModules[key]) {
          // if (import.meta.env.DEV) {
          //   logger.debug(`[Component Loader] 找到组件: ${key}`)
          // }
          return componentModules[key]
        }
      }
      
      // 如果预加载的模块中没有找到组件，说明文件不存在
      // import.meta.glob 只会包含实际存在的文件，所以如果找不到，说明文件确实不存在
      // 直接返回 404 错误组件，避免动态导入失败导致 Vue 渲染 undefined
      // if (import.meta.env.DEV) {
      //   logger.warn(`[Component Loader] 组件未在预加载模块中找到: ${importPath}，文件可能不存在，使用 404 组件`)
      // }
      // 返回 404 组件作为降级方案
      return () => import('@/views/error/404.vue')
    } catch (error) {
      logger.error(`[Menu Store] 加载组件失败: ${componentPath}`, error)
      // 返回一个默认的错误组件
      return () => import('@/views/error/404.vue')
    }
  }

  // 根据 menuL10nKey 获取翻译后的菜单标题
  const getTranslatedLabelForMenu = (menu: MenuTree): string => {
    const node = menu
    const menuName = node.menuName
    const menuL10nKey = node.menuL10nKey
    if (!menuL10nKey) return menuName
    const t = i18n.global.t as I18nT
    const translated = String(t(menuL10nKey))
    if (translated && translated !== menuL10nKey) return translated
    return menuName
  }

  // 扁平化菜单树，仅保留 menuType=1（叶子菜单），供 header-query 下拉与工作台快捷入口使用
  const flattenLeafMenus = (menus: MenuTree[]): { path: string; title: string; iconName?: string }[] => {
    if (!menus || !Array.isArray(menus)) return []
    const result: { path: string; title: string; iconName?: string }[] = []
    const walk = (items: MenuTree[]) => {
      items.forEach((menu) => {
        const node = menu
        const menuType = node.menuType ?? 0
        const menuStatus = node.menuStatus ?? 1
        const isVisible = node.isVisible ?? 1
        if (menuType !== 2 && menuStatus === 1 && isVisible === 1) {
          if (menuType === 1) {
            const path = node.path
            if (path) {
              const iconName = toOptionalString(node.menuIcon)
              result.push({
                path: path.startsWith('/') ? path : `/${path}`,
                title: getTranslatedLabelForMenu(node),
                ...(iconName ? { iconName } : {})
              })
            }
          }
          if (node.children && node.children.length > 0) {
            walk(node.children)
          }
        }
      })
    }
    walk(menus)
    return result
  }

  // 图标缓存：使用响应式对象，确保图标加载完成后触发更新
  const iconCache = ref<Record<string, Component>>({})

  // 预加载图标到缓存（使用 @remixicon/vue 组件名，如 RiHomeLine）
  const preloadIcon = (iconName: string) => {
    if (iconCache.value[iconName]) {
      return
    }
    import('@remixicon/vue').then((module) => {
      const iconModule = module as Record<string, Component>
      const IconComponent = iconModule[iconName]
      if (IconComponent) {
        iconCache.value[iconName] = markRaw(IconComponent)
      }
    }).catch(() => {
      // 加载失败，忽略
    })
  }

  // 获取图标渲染函数
  // 注意：Ant Design Vue 的菜单组件在使用 :items 时，icon 应该是渲染函数
  // 渲染函数会在菜单组件的上下文中执行，确保可以正确获取菜单上下文和 props
  const getIconRenderer = (iconName: string | undefined): ((item: { key: string }) => VNode) | undefined => {
    if (!iconName) {
      return undefined
    }

    // 立即开始预加载
    preloadIcon(iconName)

    // 返回渲染函数，在菜单组件上下文中执行
    return () => {
      const IconComponent = iconCache.value[iconName]
      if (IconComponent) {
        // 使用 h() 函数渲染图标组件，确保 props 正确传递
        return h(IconComponent)
      }
      // 如果图标还没加载完成，返回空占位 VNode
      return h('span')
    }
  }

  // 将菜单树转换为 Ant Design Vue 的菜单项格式
  const formatMenuItems = (menus: MenuTree[]): MenuProps['items'] => {
    // 如果菜单列表为空或不是数组，返回空数组
    if (!menus || !Array.isArray(menus) || menus.length === 0) {
      return []
    }
    return menus
      .filter((menu) => {
        const node = menu
        // 过滤掉不可见和禁用的菜单（后端已统一转换为 camelCase）
        const isVisible = node.isVisible ?? 1
        const menuStatus = node.menuStatus ?? 1
        const menuType = node.menuType ?? 0
        return menuType !== 2 && menuStatus === 1 && isVisible === 1
      })
      .map((menu): MenuItemEntry => {
        const node = menu
        const menuL10nKey = node.menuL10nKey
        const menuIcon = node.menuIcon
        const path = node.path
        const children = node.children
        const translatedLabel = getTranslatedLabelForMenu(node)

        const item: MenuItemNode = {
          key: path ?? '',
          title: translatedLabel,
          label: translatedLabel
        }
        if (menuL10nKey) item.titleKey = menuL10nKey

        // 图标处理：使用渲染函数
        // 注意：Ant Design Vue 的菜单组件在使用 :items 时，icon 应该是渲染函数
        if (menuIcon) {
          const iconRenderer = getIconRenderer(menuIcon)
          if (iconRenderer) {
            item.icon = iconRenderer
          }
        }

        // 如果有子菜单，递归处理
        if (children && children.length > 0) {
          const childItems = formatMenuItems(children)
          // 只有当子菜单项不为空时才设置 children
          if (childItems && childItems.length > 0) {
            item.children = childItems.filter((child): child is MenuItemEntry => child != null)
          }
        }

        return item as unknown as MenuItemEntry
      })
  }

  // 计算属性：处理好的菜单项（供布局组件使用）
  // 依赖 iconCache 和 i18n.locale 确保图标加载完成后菜单会更新，语言切换时也会更新
  const menuItems = computed(() => {
    // 访问 iconCache 和 i18n.locale 以建立响应式依赖
    // 同时访问 messages 确保翻译数据更新后菜单也会更新
    void iconCache.value
    void (i18n.global.locale as I18nReactiveValue).value
    void (i18n.global.messages as I18nReactiveValue).value
    return formatMenuItems(menuList.value)
  })

  // 仅 menuType=1 的扁平菜单列表（供 header-query 下拉与工作台快捷入口使用）
  const leafMenuItems = computed(() => {
    void iconCache.value
    void (i18n.global.locale as I18nReactiveValue).value
    void (i18n.global.messages as I18nReactiveValue).value
    return flattenLeafMenus(menuList.value)
  })

  // 重置（退出登录时调用）
  const reset = async () => {
    // 清除 store 状态
    routes.value = []
    isRoutesLoaded.value = false
    menuList.value = []
    
    // 清除 Vue Router 中的动态路由
    // 注意：需要在 router 模块中调用，这里只清除 store 状态
    // 路由清除逻辑在 router/index.ts 中处理
  }

  return {
    routes,
    isRoutesLoaded,
    menuList,
    menuItems,
    leafMenuItems,
    getIconRenderer,
    generateRoutes,
    reset
  }
})
