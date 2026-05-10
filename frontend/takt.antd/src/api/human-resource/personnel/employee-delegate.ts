// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-delegate.ts
// 功能描述：EmployeeDelegate API，对应后端 Takt.WebApi.Controllers.HumanResource.Personnel.TaktEmployeeDelegates
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeeDelegate,
  EmployeeDelegateQuery,
  EmployeeDelegateCreate,
  EmployeeDelegateUpdate,
  EmployeeDelegateSort
} from '@/types/human-resource/personnel/employee-delegate'

// ========================================
// EmployeeDelegate相关 API（按后端控制器顺序）
// ========================================
const employeeDelegateUrl = '/api/TaktEmployeeDelegates';

/**
 * 获取EmployeeDelegate列表（分页）
 * 对应后端：GetEmployeeDelegateListAsync
 */
export function getEmployeeDelegateList(params: EmployeeDelegateQuery): Promise<TaktPagedResult<EmployeeDelegate>> {
  return request({
    url: `${employeeDelegateUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeeDelegate
 * 对应后端：GetEmployeeDelegateByIdAsync
 */
export function getEmployeeDelegateById(id: string): Promise<EmployeeDelegate> {
  return request({
    url: `${employeeDelegateUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeeDelegate选项列表（用于下拉框等）
 * 对应后端：GetEmployeeDelegateOptionsAsync
 */
export function getEmployeeDelegateOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeDelegateUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeeDelegate
 * 对应后端：CreateEmployeeDelegateAsync
 */
export function createEmployeeDelegate(data: EmployeeDelegateCreate): Promise<EmployeeDelegate> {
  return request({
    url: employeeDelegateUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeeDelegate
 * 对应后端：UpdateEmployeeDelegateAsync
 */
export function updateEmployeeDelegate(id: string, data: EmployeeDelegateUpdate): Promise<EmployeeDelegate> {
  return request({
    url: `${employeeDelegateUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeeDelegate（单条）
 * 对应后端：DeleteEmployeeDelegateByIdAsync
 */
export function deleteEmployeeDelegateById(id: string): Promise<void> {
  return request({
    url: `${employeeDelegateUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeeDelegate
 * 对应后端：DeleteEmployeeDelegateBatchAsync
 */
export function deleteEmployeeDelegateBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeDelegateUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新EmployeeDelegate排序
 * 对应后端：UpdateEmployeeDelegateSortAsync
 */
export function updateEmployeeDelegateSort(data: EmployeeDelegateSort): Promise<EmployeeDelegateSort> {
  return request({
    url: `${employeeDelegateUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeDelegateTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeDelegateTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeDelegateUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeeDelegate
 * 对应后端：ImportEmployeeDelegateAsync
 */
export function importEmployeeDelegateData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeDelegateUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeeDelegate
 * 对应后端：ExportEmployeeDelegateAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeDelegateData(query: EmployeeDelegateQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeDelegateUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
