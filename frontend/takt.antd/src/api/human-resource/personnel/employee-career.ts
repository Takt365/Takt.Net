// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-career.ts
// 功能描述：员工职业信息 API，对应后端 HumanResource/Personnel TaktEmployeeCareersController
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  EmployeeCareer,
  EmployeeCareerQuery,
  EmployeeCareerCreate,
  EmployeeCareerUpdate
} from '@/types/human-resource/personnel/employee-career'

// ========================================
// 员工职业信息相关 API（按后端控制器顺序）
// ========================================

const careerUrl = '/api/TaktEmployeeCareers'

/**
 * 获取员工职业信息分页列表
 * 对应后端：GetListAsync
 */
export function getEmployeeCareerList(params: EmployeeCareerQuery): Promise<TaktPagedResult<EmployeeCareer>> {
  return request({
    url: `${careerUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取员工职业信息详情
 * 对应后端：GetByIdAsync
 */
export function getEmployeeCareerById(id: string): Promise<EmployeeCareer> {
  return request({
    url: `${careerUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 创建员工职业信息
 * 对应后端：CreateAsync
 */
export function createEmployeeCareer(data: EmployeeCareerCreate): Promise<EmployeeCareer> {
  return request({
    url: careerUrl,
    method: 'post',
    data
  })
}

/**
 * 更新员工职业信息
 * 对应后端：UpdateAsync
 */
export function updateEmployeeCareer(id: string, data: EmployeeCareerUpdate): Promise<EmployeeCareer> {
  return request({
    url: `${careerUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除员工职业信息（单条）
 * 对应后端：DeleteAsync
 */
export function deleteEmployeeCareerById(id: string): Promise<void> {
  return request({
    url: `${careerUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除员工职业信息
 * 对应后端：DeleteBatchAsync
 */
export function deleteEmployeeCareerBatch(ids: string[]): Promise<void> {
  return request({
    url: `${careerUrl}/batch`,
    method: 'delete',
    data: ids.map((id) => Number(id))
  })
}

/**
 * 获取员工职业信息导入模板
 * 对应后端：GetTemplateAsync
 */
export function getEmployeeCareerTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${careerUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入员工职业信息数据
 * 对应后端：ImportAsync
 */
export function importEmployeeCareerData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${careerUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出员工职业信息数据
 * 对应后端：ExportAsync
 */
export function exportEmployeeCareerData(
  query: EmployeeCareerQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${careerUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
