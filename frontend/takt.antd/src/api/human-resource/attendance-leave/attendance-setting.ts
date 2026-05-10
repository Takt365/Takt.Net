// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-setting.ts
// 功能描述：AttendanceSetting API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktAttendanceSettings
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AttendanceSetting,
  AttendanceSettingQuery,
  AttendanceSettingCreate,
  AttendanceSettingUpdate,
  AttendanceSettingSort
} from '@/types/human-resource/attendance-leave/attendance-setting'

// ========================================
// AttendanceSetting相关 API（按后端控制器顺序）
// ========================================
const attendanceSettingUrl = '/api/TaktAttendanceSettings';

/**
 * 获取AttendanceSetting列表（分页）
 * 对应后端：GetAttendanceSettingListAsync
 */
export function getAttendanceSettingList(params: AttendanceSettingQuery): Promise<TaktPagedResult<AttendanceSetting>> {
  return request({
    url: `${attendanceSettingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AttendanceSetting
 * 对应后端：GetAttendanceSettingByIdAsync
 */
export function getAttendanceSettingById(id: string): Promise<AttendanceSetting> {
  return request({
    url: `${attendanceSettingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AttendanceSetting选项列表（用于下拉框等）
 * 对应后端：GetAttendanceSettingOptionsAsync
 */
export function getAttendanceSettingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${attendanceSettingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AttendanceSetting
 * 对应后端：CreateAttendanceSettingAsync
 */
export function createAttendanceSetting(data: AttendanceSettingCreate): Promise<AttendanceSetting> {
  return request({
    url: attendanceSettingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AttendanceSetting
 * 对应后端：UpdateAttendanceSettingAsync
 */
export function updateAttendanceSetting(id: string, data: AttendanceSettingUpdate): Promise<AttendanceSetting> {
  return request({
    url: `${attendanceSettingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AttendanceSetting（单条）
 * 对应后端：DeleteAttendanceSettingByIdAsync
 */
export function deleteAttendanceSettingById(id: string): Promise<void> {
  return request({
    url: `${attendanceSettingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AttendanceSetting
 * 对应后端：DeleteAttendanceSettingBatchAsync
 */
export function deleteAttendanceSettingBatch(ids: string[]): Promise<void> {
  return request({
    url: `${attendanceSettingUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新AttendanceSetting排序
 * 对应后端：UpdateAttendanceSettingSortAsync
 */
export function updateAttendanceSettingSort(data: AttendanceSettingSort): Promise<AttendanceSettingSort> {
  return request({
    url: `${attendanceSettingUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAttendanceSettingTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAttendanceSettingTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${attendanceSettingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AttendanceSetting
 * 对应后端：ImportAttendanceSettingAsync
 */
export function importAttendanceSettingData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${attendanceSettingUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AttendanceSetting
 * 对应后端：ExportAttendanceSettingAsync；fileName 仅传名称不含后缀
 */
export function exportAttendanceSettingData(query: AttendanceSettingQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${attendanceSettingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
