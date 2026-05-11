// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/complaint
// 文件名称：customer-complaint.ts
// 功能描述：CustomerComplaint API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Complaint.TaktCustomerComplaints
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CustomerComplaint,
  CustomerComplaintQuery,
  CustomerComplaintCreate,
  CustomerComplaintUpdate,
  CustomerComplaintSort
} from '@/types/logistics/quality/complaint/customer-complaint'

// ========================================
// CustomerComplaint相关 API（按后端控制器顺序）
// ========================================
const customerComplaintUrl = '/api/TaktCustomerComplaints';

/**
 * 获取CustomerComplaint列表（分页）
 * 对应后端：GetCustomerComplaintListAsync
 */
export function getCustomerComplaintList(params: CustomerComplaintQuery): Promise<TaktPagedResult<CustomerComplaint>> {
  return request({
    url: `${customerComplaintUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CustomerComplaint
 * 对应后端：GetCustomerComplaintByIdAsync
 */
export function getCustomerComplaintById(id: string): Promise<CustomerComplaint> {
  return request({
    url: `${customerComplaintUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CustomerComplaint选项列表（用于下拉框等）
 * 对应后端：GetCustomerComplaintOptionsAsync
 */
export function getCustomerComplaintOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${customerComplaintUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建CustomerComplaint
 * 对应后端：CreateCustomerComplaintAsync
 */
export function createCustomerComplaint(data: CustomerComplaintCreate): Promise<CustomerComplaint> {
  return request({
    url: customerComplaintUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CustomerComplaint
 * 对应后端：UpdateCustomerComplaintAsync
 */
export function updateCustomerComplaint(id: string, data: CustomerComplaintUpdate): Promise<CustomerComplaint> {
  return request({
    url: `${customerComplaintUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CustomerComplaint（单条）
 * 对应后端：DeleteCustomerComplaintByIdAsync
 */
export function deleteCustomerComplaintById(id: string): Promise<void> {
  return request({
    url: `${customerComplaintUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CustomerComplaint
 * 对应后端：DeleteCustomerComplaintBatchAsync
 */
export function deleteCustomerComplaintBatch(ids: string[]): Promise<void> {
  return request({
    url: `${customerComplaintUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新CustomerComplaint排序
 * 对应后端：UpdateCustomerComplaintSortAsync
 */
export function updateCustomerComplaintSort(data: CustomerComplaintSort): Promise<CustomerComplaintSort> {
  return request({
    url: `${customerComplaintUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCustomerComplaintTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCustomerComplaintTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${customerComplaintUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CustomerComplaint
 * 对应后端：ImportCustomerComplaintAsync
 */
export function importCustomerComplaintData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${customerComplaintUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CustomerComplaint
 * 对应后端：ExportCustomerComplaintAsync；fileName 仅传名称不含后缀
 */
export function exportCustomerComplaintData(query: CustomerComplaintQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${customerComplaintUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
