// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：overtime.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：加班 API，对应后端 TaktOvertimesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  Overtime,
  OvertimeCreate,
  OvertimeQuery,
  OvertimeUpdate
} from '@/types/human-resource/attendance-leave/overtime'

const overtimeUrl = '/api/TaktOvertimes'

/**
 * 获取加班分页列表
 */
export function getOvertimeList(params: OvertimeQuery): Promise<TaktPagedResult<Overtime>> {
  return request({ url: `${overtimeUrl}/list`, method: 'get', params })
}

/**
 * 根据 ID 获取加班详情
 */
export function getOvertimeById(id: string): Promise<Overtime> {
  return request({ url: `${overtimeUrl}/${id}`, method: 'get' })
}

/**
 * 创建加班记录
 */
export function createOvertime(data: OvertimeCreate): Promise<Overtime> {
  return request({ url: overtimeUrl, method: 'post', data })
}

/**
 * 更新加班记录
 */
export function updateOvertime(id: string, data: OvertimeUpdate): Promise<Overtime> {
  return request({ url: `${overtimeUrl}/${id}`, method: 'put', data })
}

/**
 * 删除加班记录（单条）
 */
export function deleteOvertimeById(id: string): Promise<void> {
  return request({ url: `${overtimeUrl}/${id}`, method: 'delete' })
}

/**
 * 批量删除加班记录
 */
export function deleteOvertimeBatch(ids: string[]): Promise<void> {
  return request({ url: `${overtimeUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/**
 * 获取加班导入模板
 */
export function getOvertimeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${overtimeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入加班数据
 */
export function importOvertimeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${overtimeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出加班数据
 */
export function exportOvertimeData(
  query: OvertimeQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${overtimeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
