// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/identity/tenant
// 文件名称：tenant.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：租户相关 API，对应后端 TaktTenantsController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Tenant,
  TenantQuery,
  TenantCreate,
  TenantUpdate,
  TenantStatus
} from '@/types/identity/tenant'
import type { UserTenant } from '@/types/identity/user-tenant'

// ========================================
// 租户相关 API（按后端控制器顺序）
// ========================================

/**
 * 获取租户列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: TenantQuery): Promise<TaktPagedResult<Tenant>> {
  return request({
    url: '/api/TaktTenants/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取租户
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<Tenant> {
  return request({
    url: `/api/TaktTenants/${id}`,
    method: 'get'
  })
}

/**
 * 获取租户选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktTenants/options',
    method: 'get'
  })
}

/**
 * 创建租户
 * 对应后端：CreateAsync
 */
export function create(data: TenantCreate): Promise<Tenant> {
  return request({
    url: '/api/TaktTenants',
    method: 'post',
    data
  })
}

/**
 * 更新租户
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: TenantUpdate): Promise<Tenant> {
  return request({
    url: `/api/TaktTenants/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除租户
 * 对应后端：DeleteAsync
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktTenants/${id}`,
    method: 'delete'
  })
}

/**
 * 更新租户状态
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: TenantStatus): Promise<Tenant> {
  return request({
    url: '/api/TaktTenants/status',
    method: 'put',
    data
  })
}

/**
 * 获取租户用户列表
 * 对应后端：GetUserTenantIdsAsync
 */
export function getUserTenantIds(tenantId: string): Promise<UserTenant[]> {
  return request({
    url: `/api/TaktTenants/${tenantId}/users`,
    method: 'get'
  })
}

/**
 * 分配租户用户
 * 对应后端：AssignUserTenantsAsync
 */
export function assignUserTenants(tenantId: string, userIds: string[]): Promise<{ success: boolean }> {
  return request({
    url: `/api/TaktTenants/${tenantId}/users`,
    method: 'put',
    data: userIds
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync
 */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktTenants/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入租户
 * 对应后端：ImportAsync
 */
export function importTenants(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktTenants/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出租户
 * 对应后端：ExportAsync
 */
export function exportTenants(query: TenantQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktTenants/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
