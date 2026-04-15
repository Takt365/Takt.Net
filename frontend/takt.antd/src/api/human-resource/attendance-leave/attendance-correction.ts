// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-correction.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：补卡管理 API，对应后端 TaktAttendanceCorrectionsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  AttendanceCorrection,
  AttendanceCorrectionCreate,
  AttendanceCorrectionQuery,
  AttendanceCorrectionUpdate
} from '@/types/human-resource/attendance-leave/attendance-correction'

const correctionUrl = '/api/TaktAttendanceCorrections'

/**
 * 获取补卡管理分页列表
 */
export function getAttendanceCorrectionList(
  params: AttendanceCorrectionQuery
): Promise<TaktPagedResult<AttendanceCorrection>> {
  return request({ url: `${correctionUrl}/list`, method: 'get', params })
}

/**
 * 根据 ID 获取补卡记录详情
 */
export function getAttendanceCorrectionById(id: string): Promise<AttendanceCorrection> {
  return request({ url: `${correctionUrl}/${id}`, method: 'get' })
}

/**
 * 创建补卡记录
 */
export function createAttendanceCorrection(data: AttendanceCorrectionCreate): Promise<AttendanceCorrection> {
  return request({ url: correctionUrl, method: 'post', data })
}

/**
 * 更新补卡记录
 */
export function updateAttendanceCorrection(
  id: string,
  data: AttendanceCorrectionUpdate
): Promise<AttendanceCorrection> {
  return request({ url: `${correctionUrl}/${id}`, method: 'put', data })
}

/**
 * 删除补卡记录（单条）
 */
export function deleteAttendanceCorrectionById(id: string): Promise<void> {
  return request({ url: `${correctionUrl}/${id}`, method: 'delete' })
}

/**
 * 批量删除补卡记录
 */
export function deleteAttendanceCorrectionBatch(ids: string[]): Promise<void> {
  return request({ url: `${correctionUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/**
 * 获取补卡记录导入模板
 */
export function getAttendanceCorrectionTemplate(
  sheetName?: string,
  fileName?: string
): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${correctionUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入补卡记录数据
 */
export function importAttendanceCorrectionData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${correctionUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出补卡记录数据
 */
export function exportAttendanceCorrectionData(
  query: AttendanceCorrectionQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${correctionUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
