// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：purchase-order-item.ts
// 功能描述：PurchaseOrderItem API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPurchaseOrderItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PurchaseOrderItem,
  PurchaseOrderItemQuery,
  PurchaseOrderItemCreate,
  PurchaseOrderItemUpdate
} from '@/types/logistics/materials/purchase-order-item'

// ========================================
// PurchaseOrderItem相关 API（按后端控制器顺序）
// ========================================
const purchaseOrderItemUrl = '/api/TaktPurchaseOrderItems';

/**
 * 获取PurchaseOrderItem列表（分页）
 * 对应后端：GetPurchaseOrderItemListAsync
 */
export function getPurchaseOrderItemList(params: PurchaseOrderItemQuery): Promise<TaktPagedResult<PurchaseOrderItem>> {
  return request({
    url: `${purchaseOrderItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PurchaseOrderItem
 * 对应后端：GetPurchaseOrderItemByIdAsync
 */
export function getPurchaseOrderItemById(id: string): Promise<PurchaseOrderItem> {
  return request({
    url: `${purchaseOrderItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PurchaseOrderItem选项列表（用于下拉框等）
 * 对应后端：GetPurchaseOrderItemOptionsAsync
 */
export function getPurchaseOrderItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${purchaseOrderItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PurchaseOrderItem
 * 对应后端：CreatePurchaseOrderItemAsync
 */
export function createPurchaseOrderItem(data: PurchaseOrderItemCreate): Promise<PurchaseOrderItem> {
  return request({
    url: purchaseOrderItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PurchaseOrderItem
 * 对应后端：UpdatePurchaseOrderItemAsync
 */
export function updatePurchaseOrderItem(id: string, data: PurchaseOrderItemUpdate): Promise<PurchaseOrderItem> {
  return request({
    url: `${purchaseOrderItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PurchaseOrderItem（单条）
 * 对应后端：DeletePurchaseOrderItemByIdAsync
 */
export function deletePurchaseOrderItemById(id: string): Promise<void> {
  return request({
    url: `${purchaseOrderItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PurchaseOrderItem
 * 对应后端：DeletePurchaseOrderItemBatchAsync
 */
export function deletePurchaseOrderItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${purchaseOrderItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPurchaseOrderItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPurchaseOrderItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchaseOrderItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PurchaseOrderItem
 * 对应后端：ImportPurchaseOrderItemAsync
 */
export function importPurchaseOrderItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${purchaseOrderItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PurchaseOrderItem
 * 对应后端：ExportPurchaseOrderItemAsync；fileName 仅传名称不含后缀
 */
export function exportPurchaseOrderItemData(query: PurchaseOrderItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchaseOrderItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
