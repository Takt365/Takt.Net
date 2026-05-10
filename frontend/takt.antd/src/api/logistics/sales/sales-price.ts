// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/sales
// 文件名称：sales-price.ts
// 功能描述：SalesPrice API，对应后端 Takt.WebApi.Controllers.Logistics.Sales.TaktSalesPrices
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SalesPrice,
  SalesPriceQuery,
  SalesPriceCreate,
  SalesPriceUpdate
} from '@/types/logistics/sales/sales-price'

// ========================================
// SalesPrice相关 API（按后端控制器顺序）
// ========================================
const salesPriceUrl = '/api/TaktSalesPrices';

/**
 * 获取SalesPrice列表（分页）
 * 对应后端：GetSalesPriceListAsync
 */
export function getSalesPriceList(params: SalesPriceQuery): Promise<TaktPagedResult<SalesPrice>> {
  return request({
    url: `${salesPriceUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SalesPrice
 * 对应后端：GetSalesPriceByIdAsync
 */
export function getSalesPriceById(id: string): Promise<SalesPrice> {
  return request({
    url: `${salesPriceUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SalesPrice选项列表（用于下拉框等）
 * 对应后端：GetSalesPriceOptionsAsync
 */
export function getSalesPriceOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${salesPriceUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SalesPrice
 * 对应后端：CreateSalesPriceAsync
 */
export function createSalesPrice(data: SalesPriceCreate): Promise<SalesPrice> {
  return request({
    url: salesPriceUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SalesPrice
 * 对应后端：UpdateSalesPriceAsync
 */
export function updateSalesPrice(id: string, data: SalesPriceUpdate): Promise<SalesPrice> {
  return request({
    url: `${salesPriceUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SalesPrice（单条）
 * 对应后端：DeleteSalesPriceByIdAsync
 */
export function deleteSalesPriceById(id: string): Promise<void> {
  return request({
    url: `${salesPriceUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SalesPrice
 * 对应后端：DeleteSalesPriceBatchAsync
 */
export function deleteSalesPriceBatch(ids: string[]): Promise<void> {
  return request({
    url: `${salesPriceUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSalesPriceTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSalesPriceTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${salesPriceUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SalesPrice
 * 对应后端：ImportSalesPriceAsync
 */
export function importSalesPriceData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${salesPriceUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SalesPrice
 * 对应后端：ExportSalesPriceAsync；fileName 仅传名称不含后缀
 */
export function exportSalesPriceData(query: SalesPriceQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${salesPriceUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
