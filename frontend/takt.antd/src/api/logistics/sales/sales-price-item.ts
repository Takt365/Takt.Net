// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/sales
// 文件名称：sales-price-item.ts
// 功能描述：SalesPriceItem API，对应后端 Takt.WebApi.Controllers.Logistics.Sales.TaktSalesPriceItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SalesPriceItem,
  SalesPriceItemQuery,
  SalesPriceItemCreate,
  SalesPriceItemUpdate,
  SalesPriceItemSort
} from '@/types/logistics/sales/sales-price-item'

// ========================================
// SalesPriceItem相关 API（按后端控制器顺序）
// ========================================
const salesPriceItemUrl = '/api/TaktSalesPriceItems';

/**
 * 获取SalesPriceItem列表（分页）
 * 对应后端：GetSalesPriceItemListAsync
 */
export function getSalesPriceItemList(params: SalesPriceItemQuery): Promise<TaktPagedResult<SalesPriceItem>> {
  return request({
    url: `${salesPriceItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SalesPriceItem
 * 对应后端：GetSalesPriceItemByIdAsync
 */
export function getSalesPriceItemById(id: string): Promise<SalesPriceItem> {
  return request({
    url: `${salesPriceItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SalesPriceItem选项列表（用于下拉框等）
 * 对应后端：GetSalesPriceItemOptionsAsync
 */
export function getSalesPriceItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${salesPriceItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SalesPriceItem
 * 对应后端：CreateSalesPriceItemAsync
 */
export function createSalesPriceItem(data: SalesPriceItemCreate): Promise<SalesPriceItem> {
  return request({
    url: salesPriceItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SalesPriceItem
 * 对应后端：UpdateSalesPriceItemAsync
 */
export function updateSalesPriceItem(id: string, data: SalesPriceItemUpdate): Promise<SalesPriceItem> {
  return request({
    url: `${salesPriceItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SalesPriceItem（单条）
 * 对应后端：DeleteSalesPriceItemByIdAsync
 */
export function deleteSalesPriceItemById(id: string): Promise<void> {
  return request({
    url: `${salesPriceItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SalesPriceItem
 * 对应后端：DeleteSalesPriceItemBatchAsync
 */
export function deleteSalesPriceItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${salesPriceItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新SalesPriceItem排序
 * 对应后端：UpdateSalesPriceItemSortAsync
 */
export function updateSalesPriceItemSort(data: SalesPriceItemSort): Promise<SalesPriceItemSort> {
  return request({
    url: `${salesPriceItemUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSalesPriceItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSalesPriceItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${salesPriceItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SalesPriceItem
 * 对应后端：ImportSalesPriceItemAsync
 */
export function importSalesPriceItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${salesPriceItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SalesPriceItem
 * 对应后端：ExportSalesPriceItemAsync；fileName 仅传名称不含后缀
 */
export function exportSalesPriceItemData(query: SalesPriceItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${salesPriceItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
