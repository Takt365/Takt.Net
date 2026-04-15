// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：shift-schedule.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：排班计划 API，对应后端 TaktShiftSchedulesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  ShiftSchedule,
  ShiftScheduleCreate,
  ShiftScheduleQuery,
  ShiftScheduleUpdate
} from '@/types/human-resource/attendance-leave/shift-schedule'

const shiftScheduleUrl = '/api/TaktShiftSchedules'

/**
 * 获取排班计划分页列表
 */
export function getShiftScheduleList(
  params: ShiftScheduleQuery
): Promise<TaktPagedResult<ShiftSchedule>> {
  return request({ url: `${shiftScheduleUrl}/list`, method: 'get', params })
}

/**
 * 根据 ID 获取排班计划详情
 */
export function getShiftScheduleById(id: string): Promise<ShiftSchedule> {
  return request({ url: `${shiftScheduleUrl}/${id}`, method: 'get' })
}

/**
 * 创建排班计划
 */
export function createShiftSchedule(data: ShiftScheduleCreate): Promise<ShiftSchedule> {
  return request({ url: shiftScheduleUrl, method: 'post', data })
}

/**
 * 更新排班计划
 */
export function updateShiftSchedule(id: string, data: ShiftScheduleUpdate): Promise<ShiftSchedule> {
  return request({ url: `${shiftScheduleUrl}/${id}`, method: 'put', data })
}

/**
 * 删除排班计划（单条）
 */
export function deleteShiftScheduleById(id: string): Promise<void> {
  return request({ url: `${shiftScheduleUrl}/${id}`, method: 'delete' })
}

/**
 * 批量删除排班计划
 */
export function deleteShiftScheduleBatch(ids: string[]): Promise<void> {
  return request({ url: `${shiftScheduleUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/**
 * 获取排班计划导入模板
 */
export function getShiftScheduleTemplate(
  sheetName?: string,
  fileName?: string
): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${shiftScheduleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入排班计划数据
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
 * 导出排班计划数据
 */
export function exportShiftScheduleData(
  query: ShiftScheduleQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${shiftScheduleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
