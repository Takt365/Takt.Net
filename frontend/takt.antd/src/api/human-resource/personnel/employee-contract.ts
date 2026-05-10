// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-contract.ts
// 功能描述：EmployeeContract API，对应后端 Takt.WebApi.Controllers.HumanResource.Personnel.TaktEmployeeContracts
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeeContract,
  EmployeeContractQuery,
  EmployeeContractCreate,
  EmployeeContractUpdate
} from '@/types/human-resource/personnel/employee-contract'

// ========================================
// EmployeeContract相关 API（按后端控制器顺序）
// ========================================
const employeeContractUrl = '/api/TaktEmployeeContracts';

/**
 * 获取EmployeeContract列表（分页）
 * 对应后端：GetEmployeeContractListAsync
 */
export function getEmployeeContractList(params: EmployeeContractQuery): Promise<TaktPagedResult<EmployeeContract>> {
  return request({
    url: `${employeeContractUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeeContract
 * 对应后端：GetEmployeeContractByIdAsync
 */
export function getEmployeeContractById(id: string): Promise<EmployeeContract> {
  return request({
    url: `${employeeContractUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeeContract选项列表（用于下拉框等）
 * 对应后端：GetEmployeeContractOptionsAsync
 */
export function getEmployeeContractOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeContractUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeeContract
 * 对应后端：CreateEmployeeContractAsync
 */
export function createEmployeeContract(data: EmployeeContractCreate): Promise<EmployeeContract> {
  return request({
    url: employeeContractUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeeContract
 * 对应后端：UpdateEmployeeContractAsync
 */
export function updateEmployeeContract(id: string, data: EmployeeContractUpdate): Promise<EmployeeContract> {
  return request({
    url: `${employeeContractUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeeContract（单条）
 * 对应后端：DeleteEmployeeContractByIdAsync
 */
export function deleteEmployeeContractById(id: string): Promise<void> {
  return request({
    url: `${employeeContractUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeeContract
 * 对应后端：DeleteEmployeeContractBatchAsync
 */
export function deleteEmployeeContractBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeContractUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeContractTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeContractTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeContractUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeeContract
 * 对应后端：ImportEmployeeContractAsync
 */
export function importEmployeeContractData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeContractUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeeContract
 * 对应后端：ExportEmployeeContractAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeContractData(query: EmployeeContractQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeContractUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
