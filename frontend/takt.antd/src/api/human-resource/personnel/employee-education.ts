// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-education.ts
// 功能描述：EmployeeEducation API，对应后端 Takt.WebApi.Controllers.HumanResource.Personnel.TaktEmployeeEducations
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeeEducation,
  EmployeeEducationQuery,
  EmployeeEducationCreate,
  EmployeeEducationUpdate
} from '@/types/human-resource/personnel/employee-education'

// ========================================
// EmployeeEducation相关 API（按后端控制器顺序）
// ========================================
const employeeEducationUrl = '/api/TaktEmployeeEducations';

/**
 * 获取EmployeeEducation列表（分页）
 * 对应后端：GetEmployeeEducationListAsync
 */
export function getEmployeeEducationList(params: EmployeeEducationQuery): Promise<TaktPagedResult<EmployeeEducation>> {
  return request({
    url: `${employeeEducationUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeeEducation
 * 对应后端：GetEmployeeEducationByIdAsync
 */
export function getEmployeeEducationById(id: string): Promise<EmployeeEducation> {
  return request({
    url: `${employeeEducationUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeeEducation选项列表（用于下拉框等）
 * 对应后端：GetEmployeeEducationOptionsAsync
 */
export function getEmployeeEducationOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeEducationUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeeEducation
 * 对应后端：CreateEmployeeEducationAsync
 */
export function createEmployeeEducation(data: EmployeeEducationCreate): Promise<EmployeeEducation> {
  return request({
    url: employeeEducationUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeeEducation
 * 对应后端：UpdateEmployeeEducationAsync
 */
export function updateEmployeeEducation(id: string, data: EmployeeEducationUpdate): Promise<EmployeeEducation> {
  return request({
    url: `${employeeEducationUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeeEducation（单条）
 * 对应后端：DeleteEmployeeEducationByIdAsync
 */
export function deleteEmployeeEducationById(id: string): Promise<void> {
  return request({
    url: `${employeeEducationUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeeEducation
 * 对应后端：DeleteEmployeeEducationBatchAsync
 */
export function deleteEmployeeEducationBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeEducationUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeEducationTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeEducationTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeEducationUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeeEducation
 * 对应后端：ImportEmployeeEducationAsync
 */
export function importEmployeeEducationData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeEducationUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeeEducation
 * 对应后端：ExportEmployeeEducationAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeEducationData(query: EmployeeEducationQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeEducationUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
