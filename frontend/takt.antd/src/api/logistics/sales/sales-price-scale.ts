// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/sales
// 文件名称：sales-price-scale.ts
// 功能描述：SalesPriceScale API，对应后端 Takt.WebApi.Controllers.Logistics.Sales.TaktSalesPriceScales
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SalesPriceScale,
  SalesPriceScaleQuery,
  SalesPriceScaleCreate,
  SalesPriceScaleUpdate,
  SalesPriceScaleSort
} from '@/types/logistics/sales/sales-price-scale'

// ========================================
// SalesPriceScale相关 API（按后端控制器顺序）
// ========================================
const salesPriceScaleUrl = '/api/TaktSalesPriceScales';

/**
 * 获取SalesPriceScale列表（分页）
 * 对应后端：GetSalesPriceScaleListAsync
 */
export function getSalesPriceScaleList(params: SalesPriceScaleQuery): Promise<TaktPagedResult<SalesPriceScale>> {
  return request({
    url: `${salesPriceScaleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SalesPriceScale
 * 对应后端：GetSalesPriceScaleByIdAsync
 */
export function getSalesPriceScaleById(id: string): Promise<SalesPriceScale> {
  return request({
    url: `${salesPriceScaleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SalesPriceScale选项列表（用于下拉框等）
 * 对应后端：GetSalesPriceScaleOptionsAsync
 */
export function getSalesPriceScaleOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${salesPriceScaleUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SalesPriceScale
 * 对应后端：CreateSalesPriceScaleAsync
 */
export function createSalesPriceScale(data: SalesPriceScaleCreate): Promise<SalesPriceScale> {
  return request({
    url: salesPriceScaleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SalesPriceScale
 * 对应后端：UpdateSalesPriceScaleAsync
 */
export function updateSalesPriceScale(id: string, data: SalesPriceScaleUpdate): Promise<SalesPriceScale> {
  return request({
    url: `${salesPriceScaleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SalesPriceScale（单条）
 * 对应后端：DeleteSalesPriceScaleByIdAsync
 */
export function deleteSalesPriceScaleById(id: string): Promise<void> {
  return request({
    url: `${salesPriceScaleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SalesPriceScale
 * 对应后端：DeleteSalesPriceScaleBatchAsync
 */
export function deleteSalesPriceScaleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${salesPriceScaleUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新SalesPriceScale排序
 * 对应后端：UpdateSalesPriceScaleSortAsync
 */
export function updateSalesPriceScaleSort(data: SalesPriceScaleSort): Promise<SalesPriceScaleSort> {
  return request({
    url: `${salesPriceScaleUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSalesPriceScaleTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSalesPriceScaleTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${salesPriceScaleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SalesPriceScale
 * 对应后端：ImportSalesPriceScaleAsync
 */
export function importSalesPriceScaleData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${salesPriceScaleUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SalesPriceScale
 * 对应后端：ExportSalesPriceScaleAsync；fileName 仅传名称不含后缀
 */
export function exportSalesPriceScaleData(query: SalesPriceScaleQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${salesPriceScaleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
