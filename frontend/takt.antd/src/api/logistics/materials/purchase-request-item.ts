// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：purchase-request-item.ts
// 功能描述：PurchaseRequestItem API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPurchaseRequestItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PurchaseRequestItem,
  PurchaseRequestItemQuery,
  PurchaseRequestItemCreate,
  PurchaseRequestItemUpdate
} from '@/types/logistics/materials/purchase-request-item'

// ========================================
// PurchaseRequestItem相关 API（按后端控制器顺序）
// ========================================
const purchaseRequestItemUrl = '/api/TaktPurchaseRequestItems';

/**
 * 获取PurchaseRequestItem列表（分页）
 * 对应后端：GetPurchaseRequestItemListAsync
 */
export function getPurchaseRequestItemList(params: PurchaseRequestItemQuery): Promise<TaktPagedResult<PurchaseRequestItem>> {
  return request({
    url: `${purchaseRequestItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PurchaseRequestItem
 * 对应后端：GetPurchaseRequestItemByIdAsync
 */
export function getPurchaseRequestItemById(id: string): Promise<PurchaseRequestItem> {
  return request({
    url: `${purchaseRequestItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PurchaseRequestItem选项列表（用于下拉框等）
 * 对应后端：GetPurchaseRequestItemOptionsAsync
 */
export function getPurchaseRequestItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${purchaseRequestItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PurchaseRequestItem
 * 对应后端：CreatePurchaseRequestItemAsync
 */
export function createPurchaseRequestItem(data: PurchaseRequestItemCreate): Promise<PurchaseRequestItem> {
  return request({
    url: purchaseRequestItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PurchaseRequestItem
 * 对应后端：UpdatePurchaseRequestItemAsync
 */
export function updatePurchaseRequestItem(id: string, data: PurchaseRequestItemUpdate): Promise<PurchaseRequestItem> {
  return request({
    url: `${purchaseRequestItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PurchaseRequestItem（单条）
 * 对应后端：DeletePurchaseRequestItemByIdAsync
 */
export function deletePurchaseRequestItemById(id: string): Promise<void> {
  return request({
    url: `${purchaseRequestItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PurchaseRequestItem
 * 对应后端：DeletePurchaseRequestItemBatchAsync
 */
export function deletePurchaseRequestItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${purchaseRequestItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPurchaseRequestItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPurchaseRequestItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchaseRequestItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PurchaseRequestItem
 * 对应后端：ImportPurchaseRequestItemAsync
 */
export function importPurchaseRequestItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${purchaseRequestItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PurchaseRequestItem
 * 对应后端：ExportPurchaseRequestItemAsync；fileName 仅传名称不含后缀
 */
export function exportPurchaseRequestItemData(query: PurchaseRequestItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchaseRequestItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
