// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity
// 文件名称：user-tenant.ts
// 功能描述：UserTenant API，对应后端 Takt.WebApi.Controllers.Identity.TaktUserTenants
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  UserTenant,
  UserTenantQuery,
  UserTenantCreate,
  UserTenantUpdate
} from '@/types/identity/user-tenant'

// ========================================
// UserTenant相关 API（按后端控制器顺序）
// ========================================
const userTenantUrl = '/api/TaktUserTenants';

/**
 * 获取UserTenant列表（分页）
 * 对应后端：GetUserTenantListAsync
 */
export function getUserTenantList(params: UserTenantQuery): Promise<TaktPagedResult<UserTenant>> {
  return request({
    url: `${userTenantUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取UserTenant
 * 对应后端：GetUserTenantByIdAsync
 */
export function getUserTenantById(id: string): Promise<UserTenant> {
  return request({
    url: `${userTenantUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取UserTenant选项列表（用于下拉框等）
 * 对应后端：GetUserTenantOptionsAsync
 */
export function getUserTenantOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${userTenantUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建UserTenant
 * 对应后端：CreateUserTenantAsync
 */
export function createUserTenant(data: UserTenantCreate): Promise<UserTenant> {
  return request({
    url: userTenantUrl,
    method: 'post',
    data
  })
}

/**
 * 更新UserTenant
 * 对应后端：UpdateUserTenantAsync
 */
export function updateUserTenant(id: string, data: UserTenantUpdate): Promise<UserTenant> {
  return request({
    url: `${userTenantUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除UserTenant（单条）
 * 对应后端：DeleteUserTenantByIdAsync
 */
export function deleteUserTenantById(id: string): Promise<void> {
  return request({
    url: `${userTenantUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除UserTenant
 * 对应后端：DeleteUserTenantBatchAsync
 */
export function deleteUserTenantBatch(ids: string[]): Promise<void> {
  return request({
    url: `${userTenantUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetUserTenantTemplateAsync；fileName 仅传名称不含后缀
 */
export function getUserTenantTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${userTenantUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入UserTenant
 * 对应后端：ImportUserTenantAsync
 */
export function importUserTenantData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${userTenantUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出UserTenant
 * 对应后端：ExportUserTenantAsync；fileName 仅传名称不含后缀
 */
export function exportUserTenantData(query: UserTenantQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${userTenantUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
