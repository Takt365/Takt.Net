// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee.ts
// 功能描述：Employee API，对应后端 Takt.WebApi.Controllers.HumanResource.Personnel.TaktEmployees
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Employee,
  EmployeeQuery,
  EmployeeCreate,
  EmployeeUpdate,
  EmployeeStatus
} from '@/types/human-resource/personnel/employee'

// ========================================
// Employee相关 API（按后端控制器顺序）
// ========================================
const employeeUrl = '/api/TaktEmployees';

/**
 * 获取Employee列表（分页）
 * 对应后端：GetEmployeeListAsync
 */
export function getEmployeeList(params: EmployeeQuery): Promise<TaktPagedResult<Employee>> {
  return request({
    url: `${employeeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Employee
 * 对应后端：GetEmployeeByIdAsync
 */
export function getEmployeeById(id: string): Promise<Employee> {
  return request({
    url: `${employeeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Employee选项列表（用于下拉框等）
 * 对应后端：GetEmployeeOptionsAsync
 */
export function getEmployeeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Employee
 * 对应后端：CreateEmployeeAsync
 */
export function createEmployee(data: EmployeeCreate): Promise<Employee> {
  return request({
    url: employeeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Employee
 * 对应后端：UpdateEmployeeAsync
 */
export function updateEmployee(id: string, data: EmployeeUpdate): Promise<Employee> {
  return request({
    url: `${employeeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Employee（单条）
 * 对应后端：DeleteEmployeeByIdAsync
 */
export function deleteEmployeeById(id: string): Promise<void> {
  return request({
    url: `${employeeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Employee
 * 对应后端：DeleteEmployeeBatchAsync
 */
export function deleteEmployeeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Employee状态
 * 对应后端：UpdateEmployeeStatusAsync
 */
export function updateEmployeeStatus(data: EmployeeStatus): Promise<EmployeeStatus> {
  return request({
    url: `${employeeUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Employee
 * 对应后端：ImportEmployeeAsync
 */
export function importEmployeeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Employee
 * 对应后端：ExportEmployeeAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeData(query: EmployeeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
