// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity
// 文件名称：role.ts
// 功能描述：Role API，对应后端 Takt.WebApi.Controllers.Identity.TaktRoles
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Role,
  RoleQuery,
  RoleCreate,
  RoleUpdate,
  RoleStatus,
  RoleSort
} from '@/types/identity/role'

// ========================================
// Role相关 API（按后端控制器顺序）
// ========================================
const roleUrl = '/api/TaktRoles';

/**
 * 获取Role列表（分页）
 * 对应后端：GetRoleListAsync
 */
export function getRoleList(params: RoleQuery): Promise<TaktPagedResult<Role>> {
  return request({
    url: `${roleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Role
 * 对应后端：GetRoleByIdAsync
 */
export function getRoleById(id: string): Promise<Role> {
  return request({
    url: `${roleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Role选项列表（用于下拉框等）
 * 对应后端：GetRoleOptionsAsync
 */
export function getRoleOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${roleUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Role
 * 对应后端：CreateRoleAsync
 */
export function createRole(data: RoleCreate): Promise<Role> {
  return request({
    url: roleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Role
 * 对应后端：UpdateRoleAsync
 */
export function updateRole(id: string, data: RoleUpdate): Promise<Role> {
  return request({
    url: `${roleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Role（单条）
 * 对应后端：DeleteRoleByIdAsync
 */
export function deleteRoleById(id: string): Promise<void> {
  return request({
    url: `${roleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Role
 * 对应后端：DeleteRoleBatchAsync
 */
export function deleteRoleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${roleUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Role状态
 * 对应后端：UpdateRoleStatusAsync
 */
export function updateRoleStatus(data: RoleStatus): Promise<RoleStatus> {
  return request({
    url: `${roleUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新Role排序
 * 对应后端：UpdateRoleSortAsync
 */
export function updateRoleSort(data: RoleSort): Promise<RoleSort> {
  return request({
    url: `${roleUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetRoleTemplateAsync；fileName 仅传名称不含后缀
 */
export function getRoleTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${roleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Role
 * 对应后端：ImportRoleAsync
 */
export function importRoleData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${roleUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Role
 * 对应后端：ExportRoleAsync；fileName 仅传名称不含后缀
 */
export function exportRoleData(query: RoleQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${roleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
