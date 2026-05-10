// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-transfer.ts
// 功能描述：EmployeeTransfer API，对应后端 Takt.WebApi.Controllers.HumanResource.Personnel.TaktEmployeeTransfers
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeeTransfer,
  EmployeeTransferQuery,
  EmployeeTransferCreate,
  EmployeeTransferUpdate
} from '@/types/human-resource/personnel/employee-transfer'

// ========================================
// EmployeeTransfer相关 API（按后端控制器顺序）
// ========================================
const employeeTransferUrl = '/api/TaktEmployeeTransfers';

/**
 * 获取EmployeeTransfer列表（分页）
 * 对应后端：GetEmployeeTransferListAsync
 */
export function getEmployeeTransferList(params: EmployeeTransferQuery): Promise<TaktPagedResult<EmployeeTransfer>> {
  return request({
    url: `${employeeTransferUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeeTransfer
 * 对应后端：GetEmployeeTransferByIdAsync
 */
export function getEmployeeTransferById(id: string): Promise<EmployeeTransfer> {
  return request({
    url: `${employeeTransferUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeeTransfer选项列表（用于下拉框等）
 * 对应后端：GetEmployeeTransferOptionsAsync
 */
export function getEmployeeTransferOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeTransferUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeeTransfer
 * 对应后端：CreateEmployeeTransferAsync
 */
export function createEmployeeTransfer(data: EmployeeTransferCreate): Promise<EmployeeTransfer> {
  return request({
    url: employeeTransferUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeeTransfer
 * 对应后端：UpdateEmployeeTransferAsync
 */
export function updateEmployeeTransfer(id: string, data: EmployeeTransferUpdate): Promise<EmployeeTransfer> {
  return request({
    url: `${employeeTransferUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeeTransfer（单条）
 * 对应后端：DeleteEmployeeTransferByIdAsync
 */
export function deleteEmployeeTransferById(id: string): Promise<void> {
  return request({
    url: `${employeeTransferUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeeTransfer
 * 对应后端：DeleteEmployeeTransferBatchAsync
 */
export function deleteEmployeeTransferBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeTransferUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeTransferTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeTransferTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeTransferUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeeTransfer
 * 对应后端：ImportEmployeeTransferAsync
 */
export function importEmployeeTransferData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeTransferUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeeTransfer
 * 对应后端：ExportEmployeeTransferAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeTransferData(query: EmployeeTransferQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeTransferUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
