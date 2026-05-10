// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：holiday.ts
// 功能描述：Holiday API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktHolidays
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Holiday,
  HolidayQuery,
  HolidayCreate,
  HolidayUpdate
} from '@/types/human-resource/attendance-leave/holiday'

// ========================================
// Holiday相关 API（按后端控制器顺序）
// ========================================
const holidayUrl = '/api/TaktHolidays';

/**
 * 获取Holiday列表（分页）
 * 对应后端：GetHolidayListAsync
 */
export function getHolidayList(params: HolidayQuery): Promise<TaktPagedResult<Holiday>> {
  return request({
    url: `${holidayUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Holiday
 * 对应后端：GetHolidayByIdAsync
 */
export function getHolidayById(id: string): Promise<Holiday> {
  return request({
    url: `${holidayUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Holiday选项列表（用于下拉框等）
 * 对应后端：GetHolidayOptionsAsync
 */
export function getHolidayOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${holidayUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Holiday
 * 对应后端：CreateHolidayAsync
 */
export function createHoliday(data: HolidayCreate): Promise<Holiday> {
  return request({
    url: holidayUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Holiday
 * 对应后端：UpdateHolidayAsync
 */
export function updateHoliday(id: string, data: HolidayUpdate): Promise<Holiday> {
  return request({
    url: `${holidayUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Holiday（单条）
 * 对应后端：DeleteHolidayByIdAsync
 */
export function deleteHolidayById(id: string): Promise<void> {
  return request({
    url: `${holidayUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Holiday
 * 对应后端：DeleteHolidayBatchAsync
 */
export function deleteHolidayBatch(ids: string[]): Promise<void> {
  return request({
    url: `${holidayUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetHolidayTemplateAsync；fileName 仅传名称不含后缀
 */
export function getHolidayTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${holidayUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Holiday
 * 对应后端：ImportHolidayAsync
 */
export function importHolidayData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${holidayUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Holiday
 * 对应后端：ExportHolidayAsync；fileName 仅传名称不含后缀
 */
export function exportHolidayData(query: HolidayQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${holidayUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
