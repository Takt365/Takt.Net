// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-result.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤日结结果 API，对应后端 TaktAttendanceResultsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  AttendanceResult,
  AttendanceResultCreate,
  AttendanceResultQuery,
  AttendanceResultUpdate
} from '@/types/human-resource/attendance-leave/attendance-result'

const resultUrl = '/api/TaktAttendanceResults'

/**
 * 获取考勤日结结果分页列表
 */
export function getAttendanceResultList(
  params: AttendanceResultQuery
): Promise<TaktPagedResult<AttendanceResult>> {
  return request({ url: `${resultUrl}/list`, method: 'get', params })
}

/**
 * 根据 ID 获取考勤日结结果详情
 */
export function getAttendanceResultById(id: string): Promise<AttendanceResult> {
  return request({ url: `${resultUrl}/${id}`, method: 'get' })
}

/**
 * 创建考勤日结结果
 */
export function createAttendanceResult(data: AttendanceResultCreate): Promise<AttendanceResult> {
  return request({ url: resultUrl, method: 'post', data })
}

/**
 * 更新考勤日结结果
 */
export function updateAttendanceResult(
  id: string,
  data: AttendanceResultUpdate
): Promise<AttendanceResult> {
  return request({ url: `${resultUrl}/${id}`, method: 'put', data })
}

/**
 * 删除考勤日结结果（单条）
 */
export function deleteAttendanceResultById(id: string): Promise<void> {
  return request({ url: `${resultUrl}/${id}`, method: 'delete' })
}

/**
 * 批量删除考勤日结结果
 */
export function deleteAttendanceResultBatch(ids: string[]): Promise<void> {
  return request({ url: `${resultUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/**
 * 获取考勤日结结果导入模板
 */
export function getAttendanceResultTemplate(
  sheetName?: string,
  fileName?: string
): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${resultUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入考勤日结结果数据
 */
export function importAttendanceResultData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${resultUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出考勤日结结果数据
 */
export function exportAttendanceResultData(
  query: AttendanceResultQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${resultUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
