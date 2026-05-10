// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-punch.ts
// 功能描述：AttendancePunch API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktAttendancePunchs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AttendancePunch,
  AttendancePunchQuery,
  AttendancePunchCreate,
  AttendancePunchUpdate
} from '@/types/human-resource/attendance-leave/attendance-punch'

// ========================================
// AttendancePunch相关 API（按后端控制器顺序）
// ========================================
const attendancePunchUrl = '/api/TaktAttendancePunchs';

/**
 * 获取AttendancePunch列表（分页）
 * 对应后端：GetAttendancePunchListAsync
 */
export function getAttendancePunchList(params: AttendancePunchQuery): Promise<TaktPagedResult<AttendancePunch>> {
  return request({
    url: `${attendancePunchUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AttendancePunch
 * 对应后端：GetAttendancePunchByIdAsync
 */
export function getAttendancePunchById(id: string): Promise<AttendancePunch> {
  return request({
    url: `${attendancePunchUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AttendancePunch选项列表（用于下拉框等）
 * 对应后端：GetAttendancePunchOptionsAsync
 */
export function getAttendancePunchOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${attendancePunchUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AttendancePunch
 * 对应后端：CreateAttendancePunchAsync
 */
export function createAttendancePunch(data: AttendancePunchCreate): Promise<AttendancePunch> {
  return request({
    url: attendancePunchUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AttendancePunch
 * 对应后端：UpdateAttendancePunchAsync
 */
export function updateAttendancePunch(id: string, data: AttendancePunchUpdate): Promise<AttendancePunch> {
  return request({
    url: `${attendancePunchUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AttendancePunch（单条）
 * 对应后端：DeleteAttendancePunchByIdAsync
 */
export function deleteAttendancePunchById(id: string): Promise<void> {
  return request({
    url: `${attendancePunchUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AttendancePunch
 * 对应后端：DeleteAttendancePunchBatchAsync
 */
export function deleteAttendancePunchBatch(ids: string[]): Promise<void> {
  return request({
    url: `${attendancePunchUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAttendancePunchTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAttendancePunchTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${attendancePunchUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AttendancePunch
 * 对应后端：ImportAttendancePunchAsync
 */
export function importAttendancePunchData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${attendancePunchUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AttendancePunch
 * 对应后端：ExportAttendancePunchAsync；fileName 仅传名称不含后缀
 */
export function exportAttendancePunchData(query: AttendancePunchQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${attendancePunchUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
