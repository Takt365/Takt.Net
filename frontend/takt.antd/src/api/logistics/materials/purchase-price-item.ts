// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：purchase-price-item.ts
// 功能描述：PurchasePriceItem API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPurchasePriceItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PurchasePriceItem,
  PurchasePriceItemQuery,
  PurchasePriceItemCreate,
  PurchasePriceItemUpdate,
  PurchasePriceItemSort
} from '@/types/logistics/materials/purchase-price-item'

// ========================================
// PurchasePriceItem相关 API（按后端控制器顺序）
// ========================================
const purchasePriceItemUrl = '/api/TaktPurchasePriceItems';

/**
 * 获取PurchasePriceItem列表（分页）
 * 对应后端：GetPurchasePriceItemListAsync
 */
export function getPurchasePriceItemList(params: PurchasePriceItemQuery): Promise<TaktPagedResult<PurchasePriceItem>> {
  return request({
    url: `${purchasePriceItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PurchasePriceItem
 * 对应后端：GetPurchasePriceItemByIdAsync
 */
export function getPurchasePriceItemById(id: string): Promise<PurchasePriceItem> {
  return request({
    url: `${purchasePriceItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PurchasePriceItem选项列表（用于下拉框等）
 * 对应后端：GetPurchasePriceItemOptionsAsync
 */
export function getPurchasePriceItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${purchasePriceItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PurchasePriceItem
 * 对应后端：CreatePurchasePriceItemAsync
 */
export function createPurchasePriceItem(data: PurchasePriceItemCreate): Promise<PurchasePriceItem> {
  return request({
    url: purchasePriceItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PurchasePriceItem
 * 对应后端：UpdatePurchasePriceItemAsync
 */
export function updatePurchasePriceItem(id: string, data: PurchasePriceItemUpdate): Promise<PurchasePriceItem> {
  return request({
    url: `${purchasePriceItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PurchasePriceItem（单条）
 * 对应后端：DeletePurchasePriceItemByIdAsync
 */
export function deletePurchasePriceItemById(id: string): Promise<void> {
  return request({
    url: `${purchasePriceItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PurchasePriceItem
 * 对应后端：DeletePurchasePriceItemBatchAsync
 */
export function deletePurchasePriceItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${purchasePriceItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新PurchasePriceItem排序
 * 对应后端：UpdatePurchasePriceItemSortAsync
 */
export function updatePurchasePriceItemSort(data: PurchasePriceItemSort): Promise<PurchasePriceItemSort> {
  return request({
    url: `${purchasePriceItemUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPurchasePriceItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPurchasePriceItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchasePriceItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PurchasePriceItem
 * 对应后端：ImportPurchasePriceItemAsync
 */
export function importPurchasePriceItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${purchasePriceItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PurchasePriceItem
 * 对应后端：ExportPurchasePriceItemAsync；fileName 仅传名称不含后缀
 */
export function exportPurchasePriceItemData(query: PurchasePriceItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchasePriceItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
