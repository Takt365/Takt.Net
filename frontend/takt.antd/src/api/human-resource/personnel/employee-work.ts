// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-work.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工工作经历 API，对应后端 TaktEmployeeWorksController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type { EmployeeWork, EmployeeWorkCreate, EmployeeWorkQuery, EmployeeWorkUpdate } from '@/types/human-resource/personnel/employee-work'

const workUrl = '/api/TaktEmployeeWorks'

/** 获取员工工作经历分页列表 */
export function getEmployeeWorkList(params: EmployeeWorkQuery): Promise<TaktPagedResult<EmployeeWork>> {
  return request({ url: `${workUrl}/list`, method: 'get', params })
}

/** 根据ID获取员工工作经历详情 */
export function getEmployeeWorkById(id: string): Promise<EmployeeWork> {
  return request({ url: `${workUrl}/${id}`, method: 'get' })
}

/** 创建员工工作经历 */
export function createEmployeeWork(data: EmployeeWorkCreate): Promise<EmployeeWork> {
  return request({ url: workUrl, method: 'post', data })
}

/** 更新员工工作经历 */
export function updateEmployeeWork(id: string, data: EmployeeWorkUpdate): Promise<EmployeeWork> {
  return request({ url: `${workUrl}/${id}`, method: 'put', data })
}

/** 删除员工工作经历（单条） */
export function deleteEmployeeWorkById(id: string): Promise<void> {
  return request({ url: `${workUrl}/${id}`, method: 'delete' })
}

/** 批量删除员工工作经历 */
export function deleteEmployeeWorkBatch(ids: string[]): Promise<void> {
  return request({ url: `${workUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/** 获取员工工作经历导入模板 */
export function getEmployeeWorkTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${workUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/** 导入员工工作经历数据 */
export function importEmployeeWorkData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${workUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/** 导出员工工作经历数据 */
export function exportEmployeeWorkData(query: EmployeeWorkQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${workUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
