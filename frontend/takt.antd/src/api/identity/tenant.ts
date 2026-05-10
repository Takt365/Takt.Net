// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity
// 文件名称：tenant.ts
// 功能描述：Tenant API，对应后端 Takt.WebApi.Controllers.Identity.TaktTenants
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Tenant,
  TenantQuery,
  TenantCreate,
  TenantUpdate,
  TenantStatus
} from '@/types/identity/tenant'

// ========================================
// Tenant相关 API（按后端控制器顺序）
// ========================================
const tenantUrl = '/api/TaktTenants';

/**
 * 获取Tenant列表（分页）
 * 对应后端：GetTenantListAsync
 */
export function getTenantList(params: TenantQuery): Promise<TaktPagedResult<Tenant>> {
  return request({
    url: `${tenantUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Tenant
 * 对应后端：GetTenantByIdAsync
 */
export function getTenantById(id: string): Promise<Tenant> {
  return request({
    url: `${tenantUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Tenant选项列表（用于下拉框等）
 * 对应后端：GetTenantOptionsAsync
 */
export function getTenantOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${tenantUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Tenant
 * 对应后端：CreateTenantAsync
 */
export function createTenant(data: TenantCreate): Promise<Tenant> {
  return request({
    url: tenantUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Tenant
 * 对应后端：UpdateTenantAsync
 */
export function updateTenant(id: string, data: TenantUpdate): Promise<Tenant> {
  return request({
    url: `${tenantUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Tenant（单条）
 * 对应后端：DeleteTenantByIdAsync
 */
export function deleteTenantById(id: string): Promise<void> {
  return request({
    url: `${tenantUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Tenant
 * 对应后端：DeleteTenantBatchAsync
 */
export function deleteTenantBatch(ids: string[]): Promise<void> {
  return request({
    url: `${tenantUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Tenant状态
 * 对应后端：UpdateTenantStatusAsync
 */
export function updateTenantStatus(data: TenantStatus): Promise<TenantStatus> {
  return request({
    url: `${tenantUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTenantTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTenantTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${tenantUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Tenant
 * 对应后端：ImportTenantAsync
 */
export function importTenantData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${tenantUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Tenant
 * 对应后端：ExportTenantAsync；fileName 仅传名称不含后缀
 */
export function exportTenantData(query: TenantQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${tenantUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
