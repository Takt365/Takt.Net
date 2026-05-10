// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-correction.ts
// 功能描述：AttendanceCorrection API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktAttendanceCorrections
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AttendanceCorrection,
  AttendanceCorrectionQuery,
  AttendanceCorrectionCreate,
  AttendanceCorrectionUpdate
} from '@/types/human-resource/attendance-leave/attendance-correction'

// ========================================
// AttendanceCorrection相关 API（按后端控制器顺序）
// ========================================
const attendanceCorrectionUrl = '/api/TaktAttendanceCorrections';

/**
 * 获取AttendanceCorrection列表（分页）
 * 对应后端：GetAttendanceCorrectionListAsync
 */
export function getAttendanceCorrectionList(params: AttendanceCorrectionQuery): Promise<TaktPagedResult<AttendanceCorrection>> {
  return request({
    url: `${attendanceCorrectionUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AttendanceCorrection
 * 对应后端：GetAttendanceCorrectionByIdAsync
 */
export function getAttendanceCorrectionById(id: string): Promise<AttendanceCorrection> {
  return request({
    url: `${attendanceCorrectionUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AttendanceCorrection选项列表（用于下拉框等）
 * 对应后端：GetAttendanceCorrectionOptionsAsync
 */
export function getAttendanceCorrectionOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${attendanceCorrectionUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AttendanceCorrection
 * 对应后端：CreateAttendanceCorrectionAsync
 */
export function createAttendanceCorrection(data: AttendanceCorrectionCreate): Promise<AttendanceCorrection> {
  return request({
    url: attendanceCorrectionUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AttendanceCorrection
 * 对应后端：UpdateAttendanceCorrectionAsync
 */
export function updateAttendanceCorrection(id: string, data: AttendanceCorrectionUpdate): Promise<AttendanceCorrection> {
  return request({
    url: `${attendanceCorrectionUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AttendanceCorrection（单条）
 * 对应后端：DeleteAttendanceCorrectionByIdAsync
 */
export function deleteAttendanceCorrectionById(id: string): Promise<void> {
  return request({
    url: `${attendanceCorrectionUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AttendanceCorrection
 * 对应后端：DeleteAttendanceCorrectionBatchAsync
 */
export function deleteAttendanceCorrectionBatch(ids: string[]): Promise<void> {
  return request({
    url: `${attendanceCorrectionUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAttendanceCorrectionTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAttendanceCorrectionTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${attendanceCorrectionUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AttendanceCorrection
 * 对应后端：ImportAttendanceCorrectionAsync
 */
export function importAttendanceCorrectionData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${attendanceCorrectionUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AttendanceCorrection
 * 对应后端：ExportAttendanceCorrectionAsync；fileName 仅传名称不含后缀
 */
export function exportAttendanceCorrectionData(query: AttendanceCorrectionQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${attendanceCorrectionUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
