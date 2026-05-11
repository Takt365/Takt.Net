// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/complaint
// 文件名称：customer-satisfaction-survey-item.ts
// 功能描述：CustomerSatisfactionSurveyItem API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CustomerSatisfactionSurveyItem,
  CustomerSatisfactionSurveyItemQuery,
  CustomerSatisfactionSurveyItemCreate,
  CustomerSatisfactionSurveyItemUpdate,
  CustomerSatisfactionSurveyItemSort
} from '@/types/logistics/quality/complaint/customer-satisfaction-survey-item'

// ========================================
// CustomerSatisfactionSurveyItem相关 API（按后端控制器顺序）
// ========================================
const customerSatisfactionSurveyItemUrl = '/api/TaktCustomerSatisfactionSurveyItems';

/**
 * 获取CustomerSatisfactionSurveyItem列表（分页）
 * 对应后端：GetCustomerSatisfactionSurveyItemListAsync
 */
export function getCustomerSatisfactionSurveyItemList(params: CustomerSatisfactionSurveyItemQuery): Promise<TaktPagedResult<CustomerSatisfactionSurveyItem>> {
  return request({
    url: `${customerSatisfactionSurveyItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CustomerSatisfactionSurveyItem
 * 对应后端：GetCustomerSatisfactionSurveyItemByIdAsync
 */
export function getCustomerSatisfactionSurveyItemById(id: string): Promise<CustomerSatisfactionSurveyItem> {
  return request({
    url: `${customerSatisfactionSurveyItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CustomerSatisfactionSurveyItem选项列表（用于下拉框等）
 * 对应后端：GetCustomerSatisfactionSurveyItemOptionsAsync
 */
export function getCustomerSatisfactionSurveyItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${customerSatisfactionSurveyItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建CustomerSatisfactionSurveyItem
 * 对应后端：CreateCustomerSatisfactionSurveyItemAsync
 */
export function createCustomerSatisfactionSurveyItem(data: CustomerSatisfactionSurveyItemCreate): Promise<CustomerSatisfactionSurveyItem> {
  return request({
    url: customerSatisfactionSurveyItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CustomerSatisfactionSurveyItem
 * 对应后端：UpdateCustomerSatisfactionSurveyItemAsync
 */
export function updateCustomerSatisfactionSurveyItem(id: string, data: CustomerSatisfactionSurveyItemUpdate): Promise<CustomerSatisfactionSurveyItem> {
  return request({
    url: `${customerSatisfactionSurveyItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CustomerSatisfactionSurveyItem（单条）
 * 对应后端：DeleteCustomerSatisfactionSurveyItemByIdAsync
 */
export function deleteCustomerSatisfactionSurveyItemById(id: string): Promise<void> {
  return request({
    url: `${customerSatisfactionSurveyItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CustomerSatisfactionSurveyItem
 * 对应后端：DeleteCustomerSatisfactionSurveyItemBatchAsync
 */
export function deleteCustomerSatisfactionSurveyItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${customerSatisfactionSurveyItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新CustomerSatisfactionSurveyItem排序
 * 对应后端：UpdateCustomerSatisfactionSurveyItemSortAsync
 */
export function updateCustomerSatisfactionSurveyItemSort(data: CustomerSatisfactionSurveyItemSort): Promise<CustomerSatisfactionSurveyItemSort> {
  return request({
    url: `${customerSatisfactionSurveyItemUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCustomerSatisfactionSurveyItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCustomerSatisfactionSurveyItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${customerSatisfactionSurveyItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CustomerSatisfactionSurveyItem
 * 对应后端：ImportCustomerSatisfactionSurveyItemAsync
 */
export function importCustomerSatisfactionSurveyItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${customerSatisfactionSurveyItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CustomerSatisfactionSurveyItem
 * 对应后端：ExportCustomerSatisfactionSurveyItemAsync；fileName 仅传名称不含后缀
 */
export function exportCustomerSatisfactionSurveyItemData(query: CustomerSatisfactionSurveyItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${customerSatisfactionSurveyItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
