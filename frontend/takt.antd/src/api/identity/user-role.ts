// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity
// 文件名称：user-role.ts
// 功能描述：UserRole API，对应后端 Takt.WebApi.Controllers.Identity.TaktUserRoles
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  UserRole,
  UserRoleQuery,
  UserRoleCreate,
  UserRoleUpdate
} from '@/types/identity/user-role'

// ========================================
// UserRole相关 API（按后端控制器顺序）
// ========================================
const userRoleUrl = '/api/TaktUserRoles';

/**
 * 获取UserRole列表（分页）
 * 对应后端：GetUserRoleListAsync
 */
export function getUserRoleList(params: UserRoleQuery): Promise<TaktPagedResult<UserRole>> {
  return request({
    url: `${userRoleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取UserRole
 * 对应后端：GetUserRoleByIdAsync
 */
export function getUserRoleById(id: string): Promise<UserRole> {
  return request({
    url: `${userRoleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取UserRole选项列表（用于下拉框等）
 * 对应后端：GetUserRoleOptionsAsync
 */
export function getUserRoleOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${userRoleUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建UserRole
 * 对应后端：CreateUserRoleAsync
 */
export function createUserRole(data: UserRoleCreate): Promise<UserRole> {
  return request({
    url: userRoleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新UserRole
 * 对应后端：UpdateUserRoleAsync
 */
export function updateUserRole(id: string, data: UserRoleUpdate): Promise<UserRole> {
  return request({
    url: `${userRoleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除UserRole（单条）
 * 对应后端：DeleteUserRoleByIdAsync
 */
export function deleteUserRoleById(id: string): Promise<void> {
  return request({
    url: `${userRoleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除UserRole
 * 对应后端：DeleteUserRoleBatchAsync
 */
export function deleteUserRoleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${userRoleUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetUserRoleTemplateAsync；fileName 仅传名称不含后缀
 */
export function getUserRoleTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${userRoleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入UserRole
 * 对应后端：ImportUserRoleAsync
 */
export function importUserRoleData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${userRoleUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出UserRole
 * 对应后端：ExportUserRoleAsync；fileName 仅传名称不含后缀
 */
export function exportUserRoleData(query: UserRoleQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${userRoleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
