// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-exception.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤异常 API，对应后端 TaktAttendanceExceptionsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  AttendanceException,
  AttendanceExceptionCreate,
  AttendanceExceptionQuery,
  AttendanceExceptionUpdate
} from '@/types/human-resource/attendance-leave/attendance-exception'

const exceptionUrl = '/api/TaktAttendanceExceptions'

/**
 * 获取考勤异常分页列表
 */
export function getAttendanceExceptionList(
  params: AttendanceExceptionQuery
): Promise<TaktPagedResult<AttendanceException>> {
  return request({ url: `${exceptionUrl}/list`, method: 'get', params })
}

/**
 * 根据 ID 获取考勤异常详情
 */
export function getAttendanceExceptionById(id: string): Promise<AttendanceException> {
  return request({ url: `${exceptionUrl}/${id}`, method: 'get' })
}

/**
 * 创建考勤异常
 */
export function createAttendanceException(data: AttendanceExceptionCreate): Promise<AttendanceException> {
  return request({ url: exceptionUrl, method: 'post', data })
}

/**
 * 更新考勤异常
 */
export function updateAttendanceException(
  id: string,
  data: AttendanceExceptionUpdate
): Promise<AttendanceException> {
  return request({ url: `${exceptionUrl}/${id}`, method: 'put', data })
}

/**
 * 删除考勤异常（单条）
 */
export function deleteAttendanceExceptionById(id: string): Promise<void> {
  return request({ url: `${exceptionUrl}/${id}`, method: 'delete' })
}

/**
 * 批量删除考勤异常
 */
export function deleteAttendanceExceptionBatch(ids: string[]): Promise<void> {
  return request({ url: `${exceptionUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/**
 * 获取考勤异常导入模板
 */
export function getAttendanceExceptionTemplate(
  sheetName?: string,
  fileName?: string
): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${exceptionUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入考勤异常数据
 */
export function importAttendanceExceptionData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${exceptionUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出考勤异常数据
 */
export function exportAttendanceExceptionData(
  query: AttendanceExceptionQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${exceptionUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
