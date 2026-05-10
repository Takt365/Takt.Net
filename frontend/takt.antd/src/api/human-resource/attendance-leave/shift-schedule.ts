// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：shift-schedule.ts
// 功能描述：ShiftSchedule API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktShiftSchedules
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ShiftSchedule,
  ShiftScheduleQuery,
  ShiftScheduleCreate,
  ShiftScheduleUpdate
} from '@/types/human-resource/attendance-leave/shift-schedule'

// ========================================
// ShiftSchedule相关 API（按后端控制器顺序）
// ========================================
const shiftScheduleUrl = '/api/TaktShiftSchedules';

/**
 * 获取ShiftSchedule列表（分页）
 * 对应后端：GetShiftScheduleListAsync
 */
export function getShiftScheduleList(params: ShiftScheduleQuery): Promise<TaktPagedResult<ShiftSchedule>> {
  return request({
    url: `${shiftScheduleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ShiftSchedule
 * 对应后端：GetShiftScheduleByIdAsync
 */
export function getShiftScheduleById(id: string): Promise<ShiftSchedule> {
  return request({
    url: `${shiftScheduleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ShiftSchedule选项列表（用于下拉框等）
 * 对应后端：GetShiftScheduleOptionsAsync
 */
export function getShiftScheduleOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${shiftScheduleUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ShiftSchedule
 * 对应后端：CreateShiftScheduleAsync
 */
export function createShiftSchedule(data: ShiftScheduleCreate): Promise<ShiftSchedule> {
  return request({
    url: shiftScheduleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ShiftSchedule
 * 对应后端：UpdateShiftScheduleAsync
 */
export function updateShiftSchedule(id: string, data: ShiftScheduleUpdate): Promise<ShiftSchedule> {
  return request({
    url: `${shiftScheduleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ShiftSchedule（单条）
 * 对应后端：DeleteShiftScheduleByIdAsync
 */
export function deleteShiftScheduleById(id: string): Promise<void> {
  return request({
    url: `${shiftScheduleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ShiftSchedule
 * 对应后端：DeleteShiftScheduleBatchAsync
 */
export function deleteShiftScheduleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${shiftScheduleUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetShiftScheduleTemplateAsync；fileName 仅传名称不含后缀
 */
export function getShiftScheduleTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${shiftScheduleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ShiftSchedule
 * 对应后端：ImportShiftScheduleAsync
 */
export function importShiftScheduleData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${shiftScheduleUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ShiftSchedule
 * 对应后端：ExportShiftScheduleAsync；fileName 仅传名称不含后缀
 */
export function exportShiftScheduleData(query: ShiftScheduleQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${shiftScheduleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
