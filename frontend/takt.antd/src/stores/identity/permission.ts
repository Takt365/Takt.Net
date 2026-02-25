import { defineStore } from 'pinia'
import { ref } from 'vue'

/**
 * 权限 Store
 * 权限来源：后端根据用户角色与 TaktRolePermission + TaktPermission 返回的权限标识列表（登录/拉取用户信息时写入），
 * 不再从菜单树（TaktMenu）提取。TaktMenu 仅用于路由与侧栏展示。
 */
export const usePermissionStore = defineStore('permission', () => {
  const permissions = ref<string[]>([])

  /** 设置权限列表（仅由后端下发的用户权限列表，来自 TaktRole -> TaktRolePermission -> TaktPermission） */
  const setPermissions = (list: string[]) => {
    permissions.value = Array.isArray(list) ? [...list] : []
  }

  /** 检查是否拥有某权限标识 */
  const hasPermission = (permission: string): boolean => {
    if (!permission) return true
    return permissions.value.includes(permission)
  }

  /** 重置（退出登录时调用） */
  const reset = async () => {
    permissions.value = []
  }

  return {
    permissions,
    setPermissions,
    hasPermission,
    reset
  }
})
