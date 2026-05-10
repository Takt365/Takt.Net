// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-source.ts
// 功能描述：AttendanceSource API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktAttendanceSources
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AttendanceSource,
  AttendanceSourceQuery,
  AttendanceSourceCreate,
  AttendanceSourceUpdate
} from '@/types/human-resource/attendance-leave/attendance-source'

// ========================================
// AttendanceSource相关 API（按后端控制器顺序）
// ========================================
const attendanceSourceUrl = '/api/TaktAttendanceSources';

/**
 * 获取AttendanceSource列表（分页）
 * 对应后端：GetAttendanceSourceListAsync
 */
export function getAttendanceSourceList(params: AttendanceSourceQuery): Promise<TaktPagedResult<AttendanceSource>> {
  return request({
    url: `${attendanceSourceUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AttendanceSource
 * 对应后端：GetAttendanceSourceByIdAsync
 */
export function getAttendanceSourceById(id: string): Promise<AttendanceSource> {
  return request({
    url: `${attendanceSourceUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AttendanceSource选项列表（用于下拉框等）
 * 对应后端：GetAttendanceSourceOptionsAsync
 */
export function getAttendanceSourceOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${attendanceSourceUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AttendanceSource
 * 对应后端：CreateAttendanceSourceAsync
 */
export function createAttendanceSource(data: AttendanceSourceCreate): Promise<AttendanceSource> {
  return request({
    url: attendanceSourceUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AttendanceSource
 * 对应后端：UpdateAttendanceSourceAsync
 */
export function updateAttendanceSource(id: string, data: AttendanceSourceUpdate): Promise<AttendanceSource> {
  return request({
    url: `${attendanceSourceUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AttendanceSource（单条）
 * 对应后端：DeleteAttendanceSourceByIdAsync
 */
export function deleteAttendanceSourceById(id: string): Promise<void> {
  return request({
    url: `${attendanceSourceUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AttendanceSource
 * 对应后端：DeleteAttendanceSourceBatchAsync
 */
export function deleteAttendanceSourceBatch(ids: string[]): Promise<void> {
  return request({
    url: `${attendanceSourceUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAttendanceSourceTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAttendanceSourceTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${attendanceSourceUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AttendanceSource
 * 对应后端：ImportAttendanceSourceAsync
 */
export function importAttendanceSourceData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${attendanceSourceUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AttendanceSource
 * 对应后端：ExportAttendanceSourceAsync；fileName 仅传名称不含后缀
 */
export function exportAttendanceSourceData(query: AttendanceSourceQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${attendanceSourceUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
