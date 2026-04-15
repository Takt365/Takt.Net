// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-source.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤源记录（设备原始打卡行）API，对应后端 TaktAttendanceSourcesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  AttendanceSource,
  AttendanceSourceCreate,
  AttendanceSourceQuery,
  AttendanceSourceUpdate
} from '@/types/human-resource/attendance-leave/attendance-source'

const sourceUrl = '/api/TaktAttendanceSources'

/**
 * 获取考勤源记录分页列表
 */
export function getAttendanceSourceList(
  params: AttendanceSourceQuery
): Promise<TaktPagedResult<AttendanceSource>> {
  return request({ url: `${sourceUrl}/list`, method: 'get', params })
}

/**
 * 根据 ID 获取考勤源记录详情
 */
export function getAttendanceSourceById(id: string): Promise<AttendanceSource> {
  return request({ url: `${sourceUrl}/${id}`, method: 'get' })
}

/**
 * 创建考勤源记录
 */
export function createAttendanceSource(data: AttendanceSourceCreate): Promise<AttendanceSource> {
  return request({ url: sourceUrl, method: 'post', data })
}

/**
 * 更新考勤源记录
 */
export function updateAttendanceSource(
  id: string,
  data: AttendanceSourceUpdate
): Promise<AttendanceSource> {
  return request({ url: `${sourceUrl}/${id}`, method: 'put', data })
}

/**
 * 删除考勤源记录（单条）
 */
export function deleteAttendanceSourceById(id: string): Promise<void> {
  return request({ url: `${sourceUrl}/${id}`, method: 'delete' })
}

/**
 * 批量删除考勤源记录
 */
export function deleteAttendanceSourceBatch(ids: string[]): Promise<void> {
  return request({ url: `${sourceUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/**
 * 获取考勤源记录导入模板
 */
export function getAttendanceSourceTemplate(
  sheetName?: string,
  fileName?: string
): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${sourceUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入考勤源记录数据
 */
export function importAttendanceSourceData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${sourceUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出考勤源记录数据
 */
export function exportAttendanceSourceData(
  query: AttendanceSourceQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${sourceUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
