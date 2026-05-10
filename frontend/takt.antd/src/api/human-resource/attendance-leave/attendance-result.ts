// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-result.ts
// 功能描述：AttendanceResult API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktAttendanceResults
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AttendanceResult,
  AttendanceResultQuery,
  AttendanceResultCreate,
  AttendanceResultUpdate
} from '@/types/human-resource/attendance-leave/attendance-result'

// ========================================
// AttendanceResult相关 API（按后端控制器顺序）
// ========================================
const attendanceResultUrl = '/api/TaktAttendanceResults';

/**
 * 获取AttendanceResult列表（分页）
 * 对应后端：GetAttendanceResultListAsync
 */
export function getAttendanceResultList(params: AttendanceResultQuery): Promise<TaktPagedResult<AttendanceResult>> {
  return request({
    url: `${attendanceResultUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AttendanceResult
 * 对应后端：GetAttendanceResultByIdAsync
 */
export function getAttendanceResultById(id: string): Promise<AttendanceResult> {
  return request({
    url: `${attendanceResultUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AttendanceResult选项列表（用于下拉框等）
 * 对应后端：GetAttendanceResultOptionsAsync
 */
export function getAttendanceResultOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${attendanceResultUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AttendanceResult
 * 对应后端：CreateAttendanceResultAsync
 */
export function createAttendanceResult(data: AttendanceResultCreate): Promise<AttendanceResult> {
  return request({
    url: attendanceResultUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AttendanceResult
 * 对应后端：UpdateAttendanceResultAsync
 */
export function updateAttendanceResult(id: string, data: AttendanceResultUpdate): Promise<AttendanceResult> {
  return request({
    url: `${attendanceResultUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AttendanceResult（单条）
 * 对应后端：DeleteAttendanceResultByIdAsync
 */
export function deleteAttendanceResultById(id: string): Promise<void> {
  return request({
    url: `${attendanceResultUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AttendanceResult
 * 对应后端：DeleteAttendanceResultBatchAsync
 */
export function deleteAttendanceResultBatch(ids: string[]): Promise<void> {
  return request({
    url: `${attendanceResultUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAttendanceResultTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAttendanceResultTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${attendanceResultUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AttendanceResult
 * 对应后端：ImportAttendanceResultAsync
 */
export function importAttendanceResultData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${attendanceResultUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AttendanceResult
 * 对应后端：ExportAttendanceResultAsync；fileName 仅传名称不含后缀
 */
export function exportAttendanceResultData(query: AttendanceResultQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${attendanceResultUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
