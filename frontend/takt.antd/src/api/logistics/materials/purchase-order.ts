// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：purchase-order.ts
// 功能描述：PurchaseOrder API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPurchaseOrders
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PurchaseOrder,
  PurchaseOrderQuery,
  PurchaseOrderCreate,
  PurchaseOrderUpdate
} from '@/types/logistics/materials/purchase-order'

// ========================================
// PurchaseOrder相关 API（按后端控制器顺序）
// ========================================
const purchaseOrderUrl = '/api/TaktPurchaseOrders';

/**
 * 获取PurchaseOrder列表（分页）
 * 对应后端：GetPurchaseOrderListAsync
 */
export function getPurchaseOrderList(params: PurchaseOrderQuery): Promise<TaktPagedResult<PurchaseOrder>> {
  return request({
    url: `${purchaseOrderUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PurchaseOrder
 * 对应后端：GetPurchaseOrderByIdAsync
 */
export function getPurchaseOrderById(id: string): Promise<PurchaseOrder> {
  return request({
    url: `${purchaseOrderUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PurchaseOrder选项列表（用于下拉框等）
 * 对应后端：GetPurchaseOrderOptionsAsync
 */
export function getPurchaseOrderOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${purchaseOrderUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PurchaseOrder
 * 对应后端：CreatePurchaseOrderAsync
 */
export function createPurchaseOrder(data: PurchaseOrderCreate): Promise<PurchaseOrder> {
  return request({
    url: purchaseOrderUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PurchaseOrder
 * 对应后端：UpdatePurchaseOrderAsync
 */
export function updatePurchaseOrder(id: string, data: PurchaseOrderUpdate): Promise<PurchaseOrder> {
  return request({
    url: `${purchaseOrderUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PurchaseOrder（单条）
 * 对应后端：DeletePurchaseOrderByIdAsync
 */
export function deletePurchaseOrderById(id: string): Promise<void> {
  return request({
    url: `${purchaseOrderUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PurchaseOrder
 * 对应后端：DeletePurchaseOrderBatchAsync
 */
export function deletePurchaseOrderBatch(ids: string[]): Promise<void> {
  return request({
    url: `${purchaseOrderUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPurchaseOrderTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPurchaseOrderTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchaseOrderUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PurchaseOrder
 * 对应后端：ImportPurchaseOrderAsync
 */
export function importPurchaseOrderData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${purchaseOrderUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PurchaseOrder
 * 对应后端：ExportPurchaseOrderAsync；fileName 仅传名称不含后缀
 */
export function exportPurchaseOrderData(query: PurchaseOrderQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchaseOrderUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
