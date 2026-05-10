// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-career.ts
// 功能描述：EmployeeCareer API，对应后端 Takt.WebApi.Controllers.HumanResource.Personnel.TaktEmployeeCareers
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeeCareer,
  EmployeeCareerQuery,
  EmployeeCareerCreate,
  EmployeeCareerUpdate
} from '@/types/human-resource/personnel/employee-career'

// ========================================
// EmployeeCareer相关 API（按后端控制器顺序）
// ========================================
const employeeCareerUrl = '/api/TaktEmployeeCareers';

/**
 * 获取EmployeeCareer列表（分页）
 * 对应后端：GetEmployeeCareerListAsync
 */
export function getEmployeeCareerList(params: EmployeeCareerQuery): Promise<TaktPagedResult<EmployeeCareer>> {
  return request({
    url: `${employeeCareerUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeeCareer
 * 对应后端：GetEmployeeCareerByIdAsync
 */
export function getEmployeeCareerById(id: string): Promise<EmployeeCareer> {
  return request({
    url: `${employeeCareerUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeeCareer选项列表（用于下拉框等）
 * 对应后端：GetEmployeeCareerOptionsAsync
 */
export function getEmployeeCareerOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeCareerUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeeCareer
 * 对应后端：CreateEmployeeCareerAsync
 */
export function createEmployeeCareer(data: EmployeeCareerCreate): Promise<EmployeeCareer> {
  return request({
    url: employeeCareerUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeeCareer
 * 对应后端：UpdateEmployeeCareerAsync
 */
export function updateEmployeeCareer(id: string, data: EmployeeCareerUpdate): Promise<EmployeeCareer> {
  return request({
    url: `${employeeCareerUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeeCareer（单条）
 * 对应后端：DeleteEmployeeCareerByIdAsync
 */
export function deleteEmployeeCareerById(id: string): Promise<void> {
  return request({
    url: `${employeeCareerUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeeCareer
 * 对应后端：DeleteEmployeeCareerBatchAsync
 */
export function deleteEmployeeCareerBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeCareerUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeCareerTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeCareerTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeCareerUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeeCareer
 * 对应后端：ImportEmployeeCareerAsync
 */
export function importEmployeeCareerData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeCareerUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeeCareer
 * 对应后端：ExportEmployeeCareerAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeCareerData(query: EmployeeCareerQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeCareerUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
