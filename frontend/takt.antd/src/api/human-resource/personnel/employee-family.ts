// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-family.ts
// 功能描述：EmployeeFamily API，对应后端 Takt.WebApi.Controllers.HumanResource.Personnel.TaktEmployeeFamilys
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeeFamily,
  EmployeeFamilyQuery,
  EmployeeFamilyCreate,
  EmployeeFamilyUpdate
} from '@/types/human-resource/personnel/employee-family'

// ========================================
// EmployeeFamily相关 API（按后端控制器顺序）
// ========================================
const employeeFamilyUrl = '/api/TaktEmployeeFamilys';

/**
 * 获取EmployeeFamily列表（分页）
 * 对应后端：GetEmployeeFamilyListAsync
 */
export function getEmployeeFamilyList(params: EmployeeFamilyQuery): Promise<TaktPagedResult<EmployeeFamily>> {
  return request({
    url: `${employeeFamilyUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeeFamily
 * 对应后端：GetEmployeeFamilyByIdAsync
 */
export function getEmployeeFamilyById(id: string): Promise<EmployeeFamily> {
  return request({
    url: `${employeeFamilyUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeeFamily选项列表（用于下拉框等）
 * 对应后端：GetEmployeeFamilyOptionsAsync
 */
export function getEmployeeFamilyOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeFamilyUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeeFamily
 * 对应后端：CreateEmployeeFamilyAsync
 */
export function createEmployeeFamily(data: EmployeeFamilyCreate): Promise<EmployeeFamily> {
  return request({
    url: employeeFamilyUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeeFamily
 * 对应后端：UpdateEmployeeFamilyAsync
 */
export function updateEmployeeFamily(id: string, data: EmployeeFamilyUpdate): Promise<EmployeeFamily> {
  return request({
    url: `${employeeFamilyUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeeFamily（单条）
 * 对应后端：DeleteEmployeeFamilyByIdAsync
 */
export function deleteEmployeeFamilyById(id: string): Promise<void> {
  return request({
    url: `${employeeFamilyUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeeFamily
 * 对应后端：DeleteEmployeeFamilyBatchAsync
 */
export function deleteEmployeeFamilyBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeFamilyUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeFamilyTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeFamilyTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeFamilyUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeeFamily
 * 对应后端：ImportEmployeeFamilyAsync
 */
export function importEmployeeFamilyData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeFamilyUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeeFamily
 * 对应后端：ExportEmployeeFamilyAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeFamilyData(query: EmployeeFamilyQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeFamilyUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
