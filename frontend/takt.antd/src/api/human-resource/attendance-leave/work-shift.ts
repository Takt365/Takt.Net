// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：work-shift.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：班次 API，对应后端 TaktWorkShiftsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  WorkShift,
  WorkShiftCreate,
  WorkShiftQuery,
  WorkShiftUpdate
} from '@/types/human-resource/attendance-leave/work-shift'

const workShiftUrl = '/api/TaktWorkShifts'

/**
 * 获取班次分页列表
 */
export function getWorkShiftList(params: WorkShiftQuery): Promise<TaktPagedResult<WorkShift>> {
  return request({ url: `${workShiftUrl}/list`, method: 'get', params })
}

/**
 * 根据 ID 获取班次详情
 */
export function getWorkShiftById(id: string): Promise<WorkShift> {
  return request({ url: `${workShiftUrl}/${id}`, method: 'get' })
}

/**
 * 获取班次下拉选项（GET /options）
 */
export function getWorkShiftOptions(): Promise<TaktSelectOption[]> {
  return request({ url: `${workShiftUrl}/options`, method: 'get' })
}

/**
 * 创建班次
 */
export function createWorkShift(data: WorkShiftCreate): Promise<WorkShift> {
  return request({ url: workShiftUrl, method: 'post', data })
}

/**
 * 更新班次
 */
export function updateWorkShift(id: string, data: WorkShiftUpdate): Promise<WorkShift> {
  return request({ url: `${workShiftUrl}/${id}`, method: 'put', data })
}

/**
 * 删除班次（单条）
 */
export function deleteWorkShiftById(id: string): Promise<void> {
  return request({ url: `${workShiftUrl}/${id}`, method: 'delete' })
}

/**
 * 批量删除班次
 */
export function deleteWorkShiftBatch(ids: string[]): Promise<void> {
  return request({ url: `${workShiftUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/**
 * 获取班次导入模板
 */
export function getWorkShiftTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${workShiftUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入班次数据
 */
export function importWorkShiftData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${workShiftUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出班次数据
 */
export function exportWorkShiftData(
  query: WorkShiftQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${workShiftUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
