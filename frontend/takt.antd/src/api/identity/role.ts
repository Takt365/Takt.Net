// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/identity/role
// 文件名称：role.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：角色相关 API，对应后端 TaktRolesController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Role,
  RoleQuery,
  RoleCreate,
  RoleUpdate,
  RoleStatus
} from '@/types/identity/role'
import type { RoleMenu } from '@/types/identity/role-menu'
import type { RoleDept } from '@/types/humanresource/organization/role-dept'

// ========================================
// 角色相关 API（按后端控制器顺序）
// ========================================

/**
 * 获取角色列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: RoleQuery): Promise<TaktPagedResult<Role>> {
  return request({
    url: '/api/TaktRoles/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取角色
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<Role> {
  return request({
    url: `/api/TaktRoles/${id}`,
    method: 'get'
  })
}

/**
 * 获取角色选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktRoles/options',
    method: 'get'
  })
}

/**
 * 创建角色
 * 对应后端：CreateAsync
 */
export function create(data: RoleCreate): Promise<Role> {
  return request({
    url: '/api/TaktRoles',
    method: 'post',
    data
  })
}

/**
 * 更新角色
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: RoleUpdate): Promise<Role> {
  return request({
    url: `/api/TaktRoles/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除角色
 * 对应后端：DeleteAsync
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktRoles/${id}`,
    method: 'delete'
  })
}

/**
 * 更新角色状态
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: RoleStatus): Promise<Role> {
  return request({
    url: '/api/TaktRoles/status',
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync
 */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktRoles/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入角色
 * 对应后端：ImportAsync
 */
export function importRoles(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktRoles/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出角色
 * 对应后端：ExportAsync
 */
export function exportRoles(query: RoleQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktRoles/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 获取角色菜单列表
 * 对应后端：GetRoleMenuIdsAsync
 */
export function getRoleMenuIds(roleId: string): Promise<RoleMenu[]> {
  return request({
    url: `/api/TaktRoles/${roleId}/menus`,
    method: 'get'
  })
}

/**
 * 获取角色部门列表
 * 对应后端：GetRoleDeptIdsAsync
 */
export function getRoleDeptIds(roleId: string): Promise<RoleDept[]> {
  return request({
    url: `/api/TaktRoles/${roleId}/depts`,
    method: 'get'
  })
}

/**
 * 分配角色菜单
 * 对应后端：AssignRoleMenusAsync
 */
export function assignRoleMenus(roleId: string, menuIds: string[]): Promise<{ success: boolean }> {
  return request({
    url: `/api/TaktRoles/${roleId}/menus`,
    method: 'put',
    data: menuIds
  })
}

/**
 * 分配角色部门
 * 对应后端：AssignRoleDeptsAsync
 */
export function assignRoleDepts(roleId: string, deptIds: string[]): Promise<{ success: boolean }> {
  return request({
    url: `/api/TaktRoles/${roleId}/depts`,
    method: 'put',
    data: deptIds
  })
}

/**
 * 分配角色用户
 * 对应后端：AssignRoleUsersAsync
 */
export function assignRoleUsers(roleId: string, userIds: string[]): Promise<{ success: boolean }> {
  return request({
    url: `/api/TaktRoles/${roleId}/users`,
    method: 'put',
    data: userIds
  })
}
