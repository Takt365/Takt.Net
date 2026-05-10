// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-attachment.ts
// 功能描述：EmployeeAttachment API，对应后端 Takt.WebApi.Controllers.HumanResource.Personnel.TaktEmployeeAttachments
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeeAttachment,
  EmployeeAttachmentQuery,
  EmployeeAttachmentCreate,
  EmployeeAttachmentUpdate,
  EmployeeAttachmentSort
} from '@/types/human-resource/personnel/employee-attachment'

// ========================================
// EmployeeAttachment相关 API（按后端控制器顺序）
// ========================================
const employeeAttachmentUrl = '/api/TaktEmployeeAttachments';

/**
 * 获取EmployeeAttachment列表（分页）
 * 对应后端：GetEmployeeAttachmentListAsync
 */
export function getEmployeeAttachmentList(params: EmployeeAttachmentQuery): Promise<TaktPagedResult<EmployeeAttachment>> {
  return request({
    url: `${employeeAttachmentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeeAttachment
 * 对应后端：GetEmployeeAttachmentByIdAsync
 */
export function getEmployeeAttachmentById(id: string): Promise<EmployeeAttachment> {
  return request({
    url: `${employeeAttachmentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeeAttachment选项列表（用于下拉框等）
 * 对应后端：GetEmployeeAttachmentOptionsAsync
 */
export function getEmployeeAttachmentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeAttachmentUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeeAttachment
 * 对应后端：CreateEmployeeAttachmentAsync
 */
export function createEmployeeAttachment(data: EmployeeAttachmentCreate): Promise<EmployeeAttachment> {
  return request({
    url: employeeAttachmentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeeAttachment
 * 对应后端：UpdateEmployeeAttachmentAsync
 */
export function updateEmployeeAttachment(id: string, data: EmployeeAttachmentUpdate): Promise<EmployeeAttachment> {
  return request({
    url: `${employeeAttachmentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeeAttachment（单条）
 * 对应后端：DeleteEmployeeAttachmentByIdAsync
 */
export function deleteEmployeeAttachmentById(id: string): Promise<void> {
  return request({
    url: `${employeeAttachmentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeeAttachment
 * 对应后端：DeleteEmployeeAttachmentBatchAsync
 */
export function deleteEmployeeAttachmentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeAttachmentUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新EmployeeAttachment排序
 * 对应后端：UpdateEmployeeAttachmentSortAsync
 */
export function updateEmployeeAttachmentSort(data: EmployeeAttachmentSort): Promise<EmployeeAttachmentSort> {
  return request({
    url: `${employeeAttachmentUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeAttachmentTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeAttachmentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeAttachmentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeeAttachment
 * 对应后端：ImportEmployeeAttachmentAsync
 */
export function importEmployeeAttachmentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeAttachmentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeeAttachment
 * 对应后端：ExportEmployeeAttachmentAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeAttachmentData(query: EmployeeAttachmentQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeAttachmentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
