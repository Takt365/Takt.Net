// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/complaint
// 文件名称：customer-complaint-handling.ts
// 功能描述：CustomerComplaintHandling API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Complaint.TaktCustomerComplaintHandlings
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CustomerComplaintHandling,
  CustomerComplaintHandlingQuery,
  CustomerComplaintHandlingCreate,
  CustomerComplaintHandlingUpdate
} from '@/types/logistics/quality/complaint/customer-complaint-handling'

// ========================================
// CustomerComplaintHandling相关 API（按后端控制器顺序）
// ========================================
const customerComplaintHandlingUrl = '/api/TaktCustomerComplaintHandlings';

/**
 * 获取CustomerComplaintHandling列表（分页）
 * 对应后端：GetCustomerComplaintHandlingListAsync
 */
export function getCustomerComplaintHandlingList(params: CustomerComplaintHandlingQuery): Promise<TaktPagedResult<CustomerComplaintHandling>> {
  return request({
    url: `${customerComplaintHandlingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CustomerComplaintHandling
 * 对应后端：GetCustomerComplaintHandlingByIdAsync
 */
export function getCustomerComplaintHandlingById(id: string): Promise<CustomerComplaintHandling> {
  return request({
    url: `${customerComplaintHandlingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CustomerComplaintHandling选项列表（用于下拉框等）
 * 对应后端：GetCustomerComplaintHandlingOptionsAsync
 */
export function getCustomerComplaintHandlingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${customerComplaintHandlingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建CustomerComplaintHandling
 * 对应后端：CreateCustomerComplaintHandlingAsync
 */
export function createCustomerComplaintHandling(data: CustomerComplaintHandlingCreate): Promise<CustomerComplaintHandling> {
  return request({
    url: customerComplaintHandlingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CustomerComplaintHandling
 * 对应后端：UpdateCustomerComplaintHandlingAsync
 */
export function updateCustomerComplaintHandling(id: string, data: CustomerComplaintHandlingUpdate): Promise<CustomerComplaintHandling> {
  return request({
    url: `${customerComplaintHandlingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CustomerComplaintHandling（单条）
 * 对应后端：DeleteCustomerComplaintHandlingByIdAsync
 */
export function deleteCustomerComplaintHandlingById(id: string): Promise<void> {
  return request({
    url: `${customerComplaintHandlingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CustomerComplaintHandling
 * 对应后端：DeleteCustomerComplaintHandlingBatchAsync
 */
export function deleteCustomerComplaintHandlingBatch(ids: string[]): Promise<void> {
  return request({
    url: `${customerComplaintHandlingUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCustomerComplaintHandlingTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCustomerComplaintHandlingTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${customerComplaintHandlingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CustomerComplaintHandling
 * 对应后端：ImportCustomerComplaintHandlingAsync
 */
export function importCustomerComplaintHandlingData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${customerComplaintHandlingUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CustomerComplaintHandling
 * 对应后端：ExportCustomerComplaintHandlingAsync；fileName 仅传名称不含后缀
 */
export function exportCustomerComplaintHandlingData(query: CustomerComplaintHandlingQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${customerComplaintHandlingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
