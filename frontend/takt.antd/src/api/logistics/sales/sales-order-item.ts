// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/sales
// 文件名称：sales-order-item.ts
// 功能描述：SalesOrderItem API，对应后端 Takt.WebApi.Controllers.Logistics.Sales.TaktSalesOrderItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SalesOrderItem,
  SalesOrderItemQuery,
  SalesOrderItemCreate,
  SalesOrderItemUpdate
} from '@/types/logistics/sales/sales-order-item'

// ========================================
// SalesOrderItem相关 API（按后端控制器顺序）
// ========================================
const salesOrderItemUrl = '/api/TaktSalesOrderItems';

/**
 * 获取SalesOrderItem列表（分页）
 * 对应后端：GetSalesOrderItemListAsync
 */
export function getSalesOrderItemList(params: SalesOrderItemQuery): Promise<TaktPagedResult<SalesOrderItem>> {
  return request({
    url: `${salesOrderItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SalesOrderItem
 * 对应后端：GetSalesOrderItemByIdAsync
 */
export function getSalesOrderItemById(id: string): Promise<SalesOrderItem> {
  return request({
    url: `${salesOrderItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SalesOrderItem选项列表（用于下拉框等）
 * 对应后端：GetSalesOrderItemOptionsAsync
 */
export function getSalesOrderItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${salesOrderItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SalesOrderItem
 * 对应后端：CreateSalesOrderItemAsync
 */
export function createSalesOrderItem(data: SalesOrderItemCreate): Promise<SalesOrderItem> {
  return request({
    url: salesOrderItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SalesOrderItem
 * 对应后端：UpdateSalesOrderItemAsync
 */
export function updateSalesOrderItem(id: string, data: SalesOrderItemUpdate): Promise<SalesOrderItem> {
  return request({
    url: `${salesOrderItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SalesOrderItem（单条）
 * 对应后端：DeleteSalesOrderItemByIdAsync
 */
export function deleteSalesOrderItemById(id: string): Promise<void> {
  return request({
    url: `${salesOrderItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SalesOrderItem
 * 对应后端：DeleteSalesOrderItemBatchAsync
 */
export function deleteSalesOrderItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${salesOrderItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSalesOrderItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSalesOrderItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${salesOrderItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SalesOrderItem
 * 对应后端：ImportSalesOrderItemAsync
 */
export function importSalesOrderItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${salesOrderItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SalesOrderItem
 * 对应后端：ExportSalesOrderItemAsync；fileName 仅传名称不含后缀
 */
export function exportSalesOrderItemData(query: SalesOrderItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${salesOrderItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
