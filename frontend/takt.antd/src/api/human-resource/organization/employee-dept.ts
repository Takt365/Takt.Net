// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/organization
// 文件名称：employee-dept.ts
// 功能描述：EmployeeDept API，对应后端 Takt.WebApi.Controllers.HumanResource.Organization.TaktEmployeeDepts
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeeDept,
  EmployeeDeptQuery,
  EmployeeDeptCreate,
  EmployeeDeptUpdate
} from '@/types/human-resource/organization/employee-dept'

// ========================================
// EmployeeDept相关 API（按后端控制器顺序）
// ========================================
const employeeDeptUrl = '/api/TaktEmployeeDepts';

/**
 * 获取EmployeeDept列表（分页）
 * 对应后端：GetEmployeeDeptListAsync
 */
export function getEmployeeDeptList(params: EmployeeDeptQuery): Promise<TaktPagedResult<EmployeeDept>> {
  return request({
    url: `${employeeDeptUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeeDept
 * 对应后端：GetEmployeeDeptByIdAsync
 */
export function getEmployeeDeptById(id: string): Promise<EmployeeDept> {
  return request({
    url: `${employeeDeptUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeeDept选项列表（用于下拉框等）
 * 对应后端：GetEmployeeDeptOptionsAsync
 */
export function getEmployeeDeptOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeDeptUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeeDept
 * 对应后端：CreateEmployeeDeptAsync
 */
export function createEmployeeDept(data: EmployeeDeptCreate): Promise<EmployeeDept> {
  return request({
    url: employeeDeptUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeeDept
 * 对应后端：UpdateEmployeeDeptAsync
 */
export function updateEmployeeDept(id: string, data: EmployeeDeptUpdate): Promise<EmployeeDept> {
  return request({
    url: `${employeeDeptUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeeDept（单条）
 * 对应后端：DeleteEmployeeDeptByIdAsync
 */
export function deleteEmployeeDeptById(id: string): Promise<void> {
  return request({
    url: `${employeeDeptUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeeDept
 * 对应后端：DeleteEmployeeDeptBatchAsync
 */
export function deleteEmployeeDeptBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeDeptUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeDeptTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeDeptTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeDeptUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeeDept
 * 对应后端：ImportEmployeeDeptAsync
 */
export function importEmployeeDeptData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeDeptUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeeDept
 * 对应后端：ExportEmployeeDeptAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeDeptData(query: EmployeeDeptQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeDeptUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
