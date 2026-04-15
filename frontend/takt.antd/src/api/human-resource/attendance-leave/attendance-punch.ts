// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-punch.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：打卡记录 API，对应后端 TaktAttendancePunchesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  AttendancePunch,
  AttendancePunchCreate,
  AttendancePunchQuery,
  AttendancePunchUpdate
} from '@/types/human-resource/attendance-leave/attendance-punch'

const punchUrl = '/api/TaktAttendancePunches'

/**
 * 获取打卡记录分页列表
 */
export function getAttendancePunchList(
  params: AttendancePunchQuery
): Promise<TaktPagedResult<AttendancePunch>> {
  return request({ url: `${punchUrl}/list`, method: 'get', params })
}

/**
 * 根据 ID 获取打卡记录详情
 */
export function getAttendancePunchById(id: string): Promise<AttendancePunch> {
  return request({ url: `${punchUrl}/${id}`, method: 'get' })
}

/**
 * 创建打卡记录
 */
export function createAttendancePunch(data: AttendancePunchCreate): Promise<AttendancePunch> {
  return request({ url: punchUrl, method: 'post', data })
}

/**
 * 更新打卡记录
 */
export function updateAttendancePunch(
  id: string,
  data: AttendancePunchUpdate
): Promise<AttendancePunch> {
  return request({ url: `${punchUrl}/${id}`, method: 'put', data })
}

/**
 * 删除打卡记录（单条）
 */
export function deleteAttendancePunchById(id: string): Promise<void> {
  return request({ url: `${punchUrl}/${id}`, method: 'delete' })
}

/**
 * 批量删除打卡记录
 */
export function deleteAttendancePunchBatch(ids: string[]): Promise<void> {
  return request({ url: `${punchUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/**
 * 获取打卡记录导入模板
 */
export function getAttendancePunchTemplate(
  sheetName?: string,
  fileName?: string
): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${punchUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入打卡记录数据
 */
export function importAttendancePunchData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${punchUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出打卡记录数据
 */
export function exportAttendancePunchData(
  query: AttendancePunchQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${punchUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
