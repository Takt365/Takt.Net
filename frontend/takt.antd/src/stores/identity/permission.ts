import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { MenuTree } from '@/types/identity/menu'
type MenuLike = MenuTree & {
  dictValue?: string | number
  dictLabel?: string
  transKey?: string
  extValue?: string
}

export const usePermissionStore = defineStore('permission', () => {
  const permissions = ref<string[]>([])

  // 从菜单树中提取所有权限（递归提取，包括按钮类型的菜单）
  const extractPermissionsFromMenuTree = (menus: MenuTree[]): string[] => {
    const result: string[] = []
    
    const traverse = (menuList: MenuTree[]) => {
      menuList.forEach((menu: MenuTree) => {
        const { permission, children, menuType } = extractMenuFields(menu)
        
        // 如果菜单有权限标识，添加到结果中（包括按钮类型）
        if (permission && typeof permission === 'string' && permission.trim()) {
          result.push(permission.trim())
          // if (import.meta.env.DEV) {
          //   logger.debug(`[Permission Extract] 提取权限: ${permission.trim()}, MenuType: ${menuType}, MenuName: ${menuName || menuCode}`)
          // }
        } else if (import.meta.env.DEV && menuType === 2) {
          // 按钮类型但没有权限标识，记录警告
          // logger.warn(`[Permission Extract] 按钮类型菜单没有权限标识: MenuName=${menuName || menuCode}, MenuType=${menuType}`)
        }
        
        // 递归处理子菜单
        if (children && Array.isArray(children) && children.length > 0) {
          traverse(children)
        }
      })
    }
    
    traverse(menus)
    return result
  }

  // 辅助函数：从菜单对象提取字段（后端已统一转换为 camelCase）
  const extractMenuFields = (menu: MenuTree) => {
    const menuLike = menu as MenuLike
    return {
    menuId: menu.menuId || menu.dictValue,
    menuName: menu.menuName || menuLike.dictLabel || '',
    menuCode: menu.menuCode || menu.extLabel || '',
    menuL10nKey: menu.menuL10nKey || menuLike.transKey,
    menuIcon: menu.menuIcon,
    path: menu.path || menuLike.extValue || '',
    component: menu.component,
    menuType: menu.menuType ?? 0,
    menuStatus: menu.menuStatus ?? 1,
    isVisible: menu.isVisible ?? 1,
    permission: menu.permission,
    children: menu.children
    }
  }

  // 设置权限列表（从菜单树或用户信息中提取）
  const setPermissions = (menuPermissions: string[], userPermissions: string[] = []) => {
    // 合并从菜单提取的权限和用户信息中的权限（去重）
    const allPermissions = [...new Set([...userPermissions, ...menuPermissions])]
    permissions.value = allPermissions
    // if (import.meta.env.DEV) {
    //   logger.debug('[Permission Store] 用户信息中的权限:', userPermissions)
    //   logger.debug('[Permission Store] 从菜单树提取的权限:', menuPermissions)
    //   logger.debug('[Permission Store] 合并后的权限列表:', allPermissions)
    //   logger.debug('[Permission Store] 权限数量:', allPermissions.length)
    // }
  }

  // 检查权限
  const hasPermission = (permission: string): boolean => {
    if (!permission) {
      return true
    }
    const hasPerm = permissions.value.includes(permission)
    // if (import.meta.env.DEV && !hasPerm) {
    //   logger.debug('[Permission Store] 权限检查失败:', {
    //     permission,
    //     permissionsList: permissions.value,
    //     permissionsCount: permissions.value.length,
    //     hasPermission: hasPerm
    //   })
    // }
    return hasPerm
  }

  // 重置（退出登录时调用）
  const reset = async () => {
    // 清除权限状态
    permissions.value = []
  }

  return {
    permissions,
    extractPermissionsFromMenuTree,
    setPermissions,
    hasPermission,
    reset
  }
})
