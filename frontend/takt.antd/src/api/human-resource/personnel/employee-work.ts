// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-work.ts
// 功能描述：EmployeeWork API，对应后端 Takt.WebApi.Controllers.HumanResource.Personnel.TaktEmployeeWorks
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeeWork,
  EmployeeWorkQuery,
  EmployeeWorkCreate,
  EmployeeWorkUpdate
} from '@/types/human-resource/personnel/employee-work'

// ========================================
// EmployeeWork相关 API（按后端控制器顺序）
// ========================================
const employeeWorkUrl = '/api/TaktEmployeeWorks';

/**
 * 获取EmployeeWork列表（分页）
 * 对应后端：GetEmployeeWorkListAsync
 */
export function getEmployeeWorkList(params: EmployeeWorkQuery): Promise<TaktPagedResult<EmployeeWork>> {
  return request({
    url: `${employeeWorkUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeeWork
 * 对应后端：GetEmployeeWorkByIdAsync
 */
export function getEmployeeWorkById(id: string): Promise<EmployeeWork> {
  return request({
    url: `${employeeWorkUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeeWork选项列表（用于下拉框等）
 * 对应后端：GetEmployeeWorkOptionsAsync
 */
export function getEmployeeWorkOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeWorkUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeeWork
 * 对应后端：CreateEmployeeWorkAsync
 */
export function createEmployeeWork(data: EmployeeWorkCreate): Promise<EmployeeWork> {
  return request({
    url: employeeWorkUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeeWork
 * 对应后端：UpdateEmployeeWorkAsync
 */
export function updateEmployeeWork(id: string, data: EmployeeWorkUpdate): Promise<EmployeeWork> {
  return request({
    url: `${employeeWorkUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeeWork（单条）
 * 对应后端：DeleteEmployeeWorkByIdAsync
 */
export function deleteEmployeeWorkById(id: string): Promise<void> {
  return request({
    url: `${employeeWorkUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeeWork
 * 对应后端：DeleteEmployeeWorkBatchAsync
 */
export function deleteEmployeeWorkBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeWorkUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeWorkTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeWorkTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeWorkUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeeWork
 * 对应后端：ImportEmployeeWorkAsync
 */
export function importEmployeeWorkData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeWorkUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeeWork
 * 对应后端：ExportEmployeeWorkAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeWorkData(query: EmployeeWorkQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeWorkUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
