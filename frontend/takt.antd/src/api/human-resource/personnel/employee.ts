// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 功能描述：员工相关 API，对应后端 HumanResource/Personnel TaktEmployeesController
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Employee,
  EmployeeQuery,
  EmployeeCreate,
  EmployeeUpdate
} from '@/types/human-resource/personnel/employee'

const employeeUrl = '/api/TaktEmployees'

/**
 * 获取员工分页列表
 * 对应后端：GetListAsync
 */
export function getEmployeeList(params: EmployeeQuery): Promise<TaktPagedResult<Employee>> {
  return request({
    url: `${employeeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取员工详情
 * 对应后端：GetByIdAsync
 */
export function getEmployeeById(id: string): Promise<Employee> {
  return request({
    url: `${employeeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 创建员工
 * 对应后端：CreateAsync
 */
export function createEmployee(data: EmployeeCreate): Promise<Employee> {
  return request({
    url: employeeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新员工
 * 对应后端：UpdateAsync
 */
export function updateEmployee(id: string, data: EmployeeUpdate): Promise<Employee> {
  return request({
    url: `${employeeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除员工（单条）
 * 对应后端：DeleteAsync
 */
export function deleteEmployeeById(id: string): Promise<void> {
  return request({
    url: `${employeeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除员工
 * 对应后端：DeleteBatchAsync（POST delete）
 */
export function deleteEmployeeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeUrl}/delete`,
    method: 'post',
    data: ids.map((id) => Number(id))
  })
}

/**
 * 获取员工导入模板
 * 对应后端：GetTemplateAsync
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
 * 导入员工（Excel，列与 TaktEmployeeImportDto / 下载模板一致）
 * 对应后端：ImportAsync
 */
export function importEmployeeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: `${employeeUrl}/import`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出员工数据
 * 对应后端：ExportAsync；返回含 Content-Disposition 元数据便于本地保存名与 zip/xlsx 一致
 */
export function exportEmployeeData(
  query: EmployeeQuery,
  sheetName?: string,
  fileName?: string
): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 获取员工选项列表（用于下拉框等，仅在职员工）
 * 对应后端：GetOptionsAsync
 */
export function getEmployeeOptions(excludeBoundToUser: boolean = false): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeUrl}/options`,
    method: 'get',
    params: { excludeBoundToUser }
  })
}
