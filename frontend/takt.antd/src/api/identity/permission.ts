// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity/permission
// 文件名称：permission.ts
// 功能描述：权限相关 API，对应后端 TaktPermissionsController
// ========================================

import request from '../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Permission,
  PermissionQuery,
  PermissionCreate,
  PermissionUpdate,
  PermissionStatus
} from '@/types/identity/permission'

/**
 * 获取权限列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: PermissionQuery): Promise<TaktPagedResult<Permission>> {
  return request({
    url: '/api/TaktPermissions/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取权限
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<Permission> {
  return request({
    url: `/api/TaktPermissions/${id}`,
    method: 'get'
  })
}

/**
 * 获取权限选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktPermissions/options',
    method: 'get'
  })
}

/**
 * 创建权限
 * 对应后端：CreateAsync
 */
export function create(data: PermissionCreate): Promise<Permission> {
  return request({
    url: '/api/TaktPermissions',
    method: 'post',
    data
  })
}

/**
 * 更新权限
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: PermissionUpdate): Promise<Permission> {
  return request({
    url: `/api/TaktPermissions/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除权限
 * 对应后端：DeleteAsync
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktPermissions/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除权限
 * 对应后端：DeleteAsync(ids)
 */
export function removeBatch(ids: string[]): Promise<void> {
  return request({
    url: '/api/TaktPermissions/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 更新权限状态
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: PermissionStatus): Promise<Permission> {
  return request({
    url: '/api/TaktPermissions/status',
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
    url: '/api/TaktPermissions/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入权限
 * 对应后端：ImportAsync
 */
export function importPermissions(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktPermissions/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出权限
 * 对应后端：ExportAsync
 */
export function exportPermissions(
  query: PermissionQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktPermissions/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
