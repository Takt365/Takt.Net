// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-education.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工教育经历 API，对应后端 TaktEmployeeEducationsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  EmployeeEducation,
  EmployeeEducationCreate,
  EmployeeEducationQuery,
  EmployeeEducationUpdate
} from '@/types/human-resource/personnel/employee-education'

const educationUrl = '/api/TaktEmployeeEducations'

/**
 * 获取员工教育经历分页列表
 */
export function getEmployeeEducationList(params: EmployeeEducationQuery): Promise<TaktPagedResult<EmployeeEducation>> {
  return request({ url: `${educationUrl}/list`, method: 'get', params })
}

/**
 * 根据ID获取员工教育经历详情
 */
export function getEmployeeEducationById(id: string): Promise<EmployeeEducation> {
  return request({ url: `${educationUrl}/${id}`, method: 'get' })
}

/**
 * 创建员工教育经历
 */
export function createEmployeeEducation(data: EmployeeEducationCreate): Promise<EmployeeEducation> {
  return request({ url: educationUrl, method: 'post', data })
}

/**
 * 更新员工教育经历
 */
export function updateEmployeeEducation(id: string, data: EmployeeEducationUpdate): Promise<EmployeeEducation> {
  return request({ url: `${educationUrl}/${id}`, method: 'put', data })
}

/**
 * 删除员工教育经历（单条）
 */
export function deleteEmployeeEducationById(id: string): Promise<void> {
  return request({ url: `${educationUrl}/${id}`, method: 'delete' })
}

/**
 * 批量删除员工教育经历
 */
export function deleteEmployeeEducationBatch(ids: string[]): Promise<void> {
  return request({ url: `${educationUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/**
 * 获取员工教育经历导入模板
 */
export function getEmployeeEducationTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${educationUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入员工教育经历数据
 */
export function importEmployeeEducationData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${educationUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出员工教育经历数据
 */
export function exportEmployeeEducationData(
  query: EmployeeEducationQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${educationUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
