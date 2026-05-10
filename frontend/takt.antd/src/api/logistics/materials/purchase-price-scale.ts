// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：purchase-price-scale.ts
// 功能描述：PurchasePriceScale API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPurchasePriceScales
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PurchasePriceScale,
  PurchasePriceScaleQuery,
  PurchasePriceScaleCreate,
  PurchasePriceScaleUpdate,
  PurchasePriceScaleSort
} from '@/types/logistics/materials/purchase-price-scale'

// ========================================
// PurchasePriceScale相关 API（按后端控制器顺序）
// ========================================
const purchasePriceScaleUrl = '/api/TaktPurchasePriceScales';

/**
 * 获取PurchasePriceScale列表（分页）
 * 对应后端：GetPurchasePriceScaleListAsync
 */
export function getPurchasePriceScaleList(params: PurchasePriceScaleQuery): Promise<TaktPagedResult<PurchasePriceScale>> {
  return request({
    url: `${purchasePriceScaleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PurchasePriceScale
 * 对应后端：GetPurchasePriceScaleByIdAsync
 */
export function getPurchasePriceScaleById(id: string): Promise<PurchasePriceScale> {
  return request({
    url: `${purchasePriceScaleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PurchasePriceScale选项列表（用于下拉框等）
 * 对应后端：GetPurchasePriceScaleOptionsAsync
 */
export function getPurchasePriceScaleOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${purchasePriceScaleUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PurchasePriceScale
 * 对应后端：CreatePurchasePriceScaleAsync
 */
export function createPurchasePriceScale(data: PurchasePriceScaleCreate): Promise<PurchasePriceScale> {
  return request({
    url: purchasePriceScaleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PurchasePriceScale
 * 对应后端：UpdatePurchasePriceScaleAsync
 */
export function updatePurchasePriceScale(id: string, data: PurchasePriceScaleUpdate): Promise<PurchasePriceScale> {
  return request({
    url: `${purchasePriceScaleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PurchasePriceScale（单条）
 * 对应后端：DeletePurchasePriceScaleByIdAsync
 */
export function deletePurchasePriceScaleById(id: string): Promise<void> {
  return request({
    url: `${purchasePriceScaleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PurchasePriceScale
 * 对应后端：DeletePurchasePriceScaleBatchAsync
 */
export function deletePurchasePriceScaleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${purchasePriceScaleUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新PurchasePriceScale排序
 * 对应后端：UpdatePurchasePriceScaleSortAsync
 */
export function updatePurchasePriceScaleSort(data: PurchasePriceScaleSort): Promise<PurchasePriceScaleSort> {
  return request({
    url: `${purchasePriceScaleUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPurchasePriceScaleTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPurchasePriceScaleTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchasePriceScaleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PurchasePriceScale
 * 对应后端：ImportPurchasePriceScaleAsync
 */
export function importPurchasePriceScaleData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${purchasePriceScaleUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PurchasePriceScale
 * 对应后端：ExportPurchasePriceScaleAsync；fileName 仅传名称不含后缀
 */
export function exportPurchasePriceScaleData(query: PurchasePriceScaleQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchasePriceScaleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
