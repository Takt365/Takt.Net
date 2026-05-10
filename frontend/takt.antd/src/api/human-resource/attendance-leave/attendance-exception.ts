// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-exception.ts
// 功能描述：AttendanceException API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktAttendanceExceptions
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AttendanceException,
  AttendanceExceptionQuery,
  AttendanceExceptionCreate,
  AttendanceExceptionUpdate
} from '@/types/human-resource/attendance-leave/attendance-exception'

// ========================================
// AttendanceException相关 API（按后端控制器顺序）
// ========================================
const attendanceExceptionUrl = '/api/TaktAttendanceExceptions';

/**
 * 获取AttendanceException列表（分页）
 * 对应后端：GetAttendanceExceptionListAsync
 */
export function getAttendanceExceptionList(params: AttendanceExceptionQuery): Promise<TaktPagedResult<AttendanceException>> {
  return request({
    url: `${attendanceExceptionUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AttendanceException
 * 对应后端：GetAttendanceExceptionByIdAsync
 */
export function getAttendanceExceptionById(id: string): Promise<AttendanceException> {
  return request({
    url: `${attendanceExceptionUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AttendanceException选项列表（用于下拉框等）
 * 对应后端：GetAttendanceExceptionOptionsAsync
 */
export function getAttendanceExceptionOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${attendanceExceptionUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AttendanceException
 * 对应后端：CreateAttendanceExceptionAsync
 */
export function createAttendanceException(data: AttendanceExceptionCreate): Promise<AttendanceException> {
  return request({
    url: attendanceExceptionUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AttendanceException
 * 对应后端：UpdateAttendanceExceptionAsync
 */
export function updateAttendanceException(id: string, data: AttendanceExceptionUpdate): Promise<AttendanceException> {
  return request({
    url: `${attendanceExceptionUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AttendanceException（单条）
 * 对应后端：DeleteAttendanceExceptionByIdAsync
 */
export function deleteAttendanceExceptionById(id: string): Promise<void> {
  return request({
    url: `${attendanceExceptionUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AttendanceException
 * 对应后端：DeleteAttendanceExceptionBatchAsync
 */
export function deleteAttendanceExceptionBatch(ids: string[]): Promise<void> {
  return request({
    url: `${attendanceExceptionUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAttendanceExceptionTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAttendanceExceptionTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${attendanceExceptionUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AttendanceException
 * 对应后端：ImportAttendanceExceptionAsync
 */
export function importAttendanceExceptionData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${attendanceExceptionUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AttendanceException
 * 对应后端：ExportAttendanceExceptionAsync；fileName 仅传名称不含后缀
 */
export function exportAttendanceExceptionData(query: AttendanceExceptionQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${attendanceExceptionUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
