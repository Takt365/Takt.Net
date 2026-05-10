// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/organization
// 文件名称：role-dept.ts
// 功能描述：RoleDept API，对应后端 Takt.WebApi.Controllers.HumanResource.Organization.TaktRoleDepts
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  RoleDept,
  RoleDeptQuery,
  RoleDeptCreate,
  RoleDeptUpdate
} from '@/types/human-resource/organization/role-dept'

// ========================================
// RoleDept相关 API（按后端控制器顺序）
// ========================================
const roleDeptUrl = '/api/TaktRoleDepts';

/**
 * 获取RoleDept列表（分页）
 * 对应后端：GetRoleDeptListAsync
 */
export function getRoleDeptList(params: RoleDeptQuery): Promise<TaktPagedResult<RoleDept>> {
  return request({
    url: `${roleDeptUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取RoleDept
 * 对应后端：GetRoleDeptByIdAsync
 */
export function getRoleDeptById(id: string): Promise<RoleDept> {
  return request({
    url: `${roleDeptUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取RoleDept选项列表（用于下拉框等）
 * 对应后端：GetRoleDeptOptionsAsync
 */
export function getRoleDeptOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${roleDeptUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建RoleDept
 * 对应后端：CreateRoleDeptAsync
 */
export function createRoleDept(data: RoleDeptCreate): Promise<RoleDept> {
  return request({
    url: roleDeptUrl,
    method: 'post',
    data
  })
}

/**
 * 更新RoleDept
 * 对应后端：UpdateRoleDeptAsync
 */
export function updateRoleDept(id: string, data: RoleDeptUpdate): Promise<RoleDept> {
  return request({
    url: `${roleDeptUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除RoleDept（单条）
 * 对应后端：DeleteRoleDeptByIdAsync
 */
export function deleteRoleDeptById(id: string): Promise<void> {
  return request({
    url: `${roleDeptUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除RoleDept
 * 对应后端：DeleteRoleDeptBatchAsync
 */
export function deleteRoleDeptBatch(ids: string[]): Promise<void> {
  return request({
    url: `${roleDeptUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetRoleDeptTemplateAsync；fileName 仅传名称不含后缀
 */
export function getRoleDeptTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${roleDeptUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入RoleDept
 * 对应后端：ImportRoleDeptAsync
 */
export function importRoleDeptData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${roleDeptUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出RoleDept
 * 对应后端：ExportRoleDeptAsync；fileName 仅传名称不含后缀
 */
export function exportRoleDeptData(query: RoleDeptQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${roleDeptUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
