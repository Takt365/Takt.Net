// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：overtime.ts
// 功能描述：Overtime API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktOvertimes
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Overtime,
  OvertimeQuery,
  OvertimeCreate,
  OvertimeUpdate,
  OvertimeStatus
} from '@/types/human-resource/attendance-leave/overtime'

// ========================================
// Overtime相关 API（按后端控制器顺序）
// ========================================
const overtimeUrl = '/api/TaktOvertimes';

/**
 * 获取Overtime列表（分页）
 * 对应后端：GetOvertimeListAsync
 */
export function getOvertimeList(params: OvertimeQuery): Promise<TaktPagedResult<Overtime>> {
  return request({
    url: `${overtimeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Overtime
 * 对应后端：GetOvertimeByIdAsync
 */
export function getOvertimeById(id: string): Promise<Overtime> {
  return request({
    url: `${overtimeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Overtime选项列表（用于下拉框等）
 * 对应后端：GetOvertimeOptionsAsync
 */
export function getOvertimeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${overtimeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Overtime
 * 对应后端：CreateOvertimeAsync
 */
export function createOvertime(data: OvertimeCreate): Promise<Overtime> {
  return request({
    url: overtimeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Overtime
 * 对应后端：UpdateOvertimeAsync
 */
export function updateOvertime(id: string, data: OvertimeUpdate): Promise<Overtime> {
  return request({
    url: `${overtimeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Overtime（单条）
 * 对应后端：DeleteOvertimeByIdAsync
 */
export function deleteOvertimeById(id: string): Promise<void> {
  return request({
    url: `${overtimeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Overtime
 * 对应后端：DeleteOvertimeBatchAsync
 */
export function deleteOvertimeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${overtimeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Overtime状态
 * 对应后端：UpdateOvertimeStatusAsync
 */
export function updateOvertimeStatus(data: OvertimeStatus): Promise<OvertimeStatus> {
  return request({
    url: `${overtimeUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetOvertimeTemplateAsync；fileName 仅传名称不含后缀
 */
export function getOvertimeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${overtimeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Overtime
 * 对应后端：ImportOvertimeAsync
 */
export function importOvertimeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${overtimeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Overtime
 * 对应后端：ExportOvertimeAsync；fileName 仅传名称不含后缀
 */
export function exportOvertimeData(query: OvertimeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${overtimeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
