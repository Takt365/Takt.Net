// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：purchase-price.ts
// 功能描述：PurchasePrice API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPurchasePrices
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PurchasePrice,
  PurchasePriceQuery,
  PurchasePriceCreate,
  PurchasePriceUpdate
} from '@/types/logistics/materials/purchase-price'

// ========================================
// PurchasePrice相关 API（按后端控制器顺序）
// ========================================
const purchasePriceUrl = '/api/TaktPurchasePrices';

/**
 * 获取PurchasePrice列表（分页）
 * 对应后端：GetPurchasePriceListAsync
 */
export function getPurchasePriceList(params: PurchasePriceQuery): Promise<TaktPagedResult<PurchasePrice>> {
  return request({
    url: `${purchasePriceUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PurchasePrice
 * 对应后端：GetPurchasePriceByIdAsync
 */
export function getPurchasePriceById(id: string): Promise<PurchasePrice> {
  return request({
    url: `${purchasePriceUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PurchasePrice选项列表（用于下拉框等）
 * 对应后端：GetPurchasePriceOptionsAsync
 */
export function getPurchasePriceOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${purchasePriceUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PurchasePrice
 * 对应后端：CreatePurchasePriceAsync
 */
export function createPurchasePrice(data: PurchasePriceCreate): Promise<PurchasePrice> {
  return request({
    url: purchasePriceUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PurchasePrice
 * 对应后端：UpdatePurchasePriceAsync
 */
export function updatePurchasePrice(id: string, data: PurchasePriceUpdate): Promise<PurchasePrice> {
  return request({
    url: `${purchasePriceUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PurchasePrice（单条）
 * 对应后端：DeletePurchasePriceByIdAsync
 */
export function deletePurchasePriceById(id: string): Promise<void> {
  return request({
    url: `${purchasePriceUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PurchasePrice
 * 对应后端：DeletePurchasePriceBatchAsync
 */
export function deletePurchasePriceBatch(ids: string[]): Promise<void> {
  return request({
    url: `${purchasePriceUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPurchasePriceTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPurchasePriceTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchasePriceUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PurchasePrice
 * 对应后端：ImportPurchasePriceAsync
 */
export function importPurchasePriceData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${purchasePriceUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PurchasePrice
 * 对应后端：ExportPurchasePriceAsync；fileName 仅传名称不含后缀
 */
export function exportPurchasePriceData(query: PurchasePriceQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchasePriceUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
