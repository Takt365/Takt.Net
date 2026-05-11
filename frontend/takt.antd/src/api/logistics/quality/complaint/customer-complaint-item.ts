// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/complaint
// 文件名称：customer-complaint-item.ts
// 功能描述：CustomerComplaintItem API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Complaint.TaktCustomerComplaintItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CustomerComplaintItem,
  CustomerComplaintItemQuery,
  CustomerComplaintItemCreate,
  CustomerComplaintItemUpdate,
  CustomerComplaintItemSort
} from '@/types/logistics/quality/complaint/customer-complaint-item'

// ========================================
// CustomerComplaintItem相关 API（按后端控制器顺序）
// ========================================
const customerComplaintItemUrl = '/api/TaktCustomerComplaintItems';

/**
 * 获取CustomerComplaintItem列表（分页）
 * 对应后端：GetCustomerComplaintItemListAsync
 */
export function getCustomerComplaintItemList(params: CustomerComplaintItemQuery): Promise<TaktPagedResult<CustomerComplaintItem>> {
  return request({
    url: `${customerComplaintItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CustomerComplaintItem
 * 对应后端：GetCustomerComplaintItemByIdAsync
 */
export function getCustomerComplaintItemById(id: string): Promise<CustomerComplaintItem> {
  return request({
    url: `${customerComplaintItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CustomerComplaintItem选项列表（用于下拉框等）
 * 对应后端：GetCustomerComplaintItemOptionsAsync
 */
export function getCustomerComplaintItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${customerComplaintItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建CustomerComplaintItem
 * 对应后端：CreateCustomerComplaintItemAsync
 */
export function createCustomerComplaintItem(data: CustomerComplaintItemCreate): Promise<CustomerComplaintItem> {
  return request({
    url: customerComplaintItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CustomerComplaintItem
 * 对应后端：UpdateCustomerComplaintItemAsync
 */
export function updateCustomerComplaintItem(id: string, data: CustomerComplaintItemUpdate): Promise<CustomerComplaintItem> {
  return request({
    url: `${customerComplaintItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CustomerComplaintItem（单条）
 * 对应后端：DeleteCustomerComplaintItemByIdAsync
 */
export function deleteCustomerComplaintItemById(id: string): Promise<void> {
  return request({
    url: `${customerComplaintItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CustomerComplaintItem
 * 对应后端：DeleteCustomerComplaintItemBatchAsync
 */
export function deleteCustomerComplaintItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${customerComplaintItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新CustomerComplaintItem排序
 * 对应后端：UpdateCustomerComplaintItemSortAsync
 */
export function updateCustomerComplaintItemSort(data: CustomerComplaintItemSort): Promise<CustomerComplaintItemSort> {
  return request({
    url: `${customerComplaintItemUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCustomerComplaintItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCustomerComplaintItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${customerComplaintItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CustomerComplaintItem
 * 对应后端：ImportCustomerComplaintItemAsync
 */
export function importCustomerComplaintItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${customerComplaintItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CustomerComplaintItem
 * 对应后端：ExportCustomerComplaintItemAsync；fileName 仅传名称不含后缀
 */
export function exportCustomerComplaintItemData(query: CustomerComplaintItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${customerComplaintItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
