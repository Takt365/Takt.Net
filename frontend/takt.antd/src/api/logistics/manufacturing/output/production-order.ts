// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/output
// 文件名称：production-order.ts
// 功能描述：ProductionOrder API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Output.TaktProductionOrders
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ProductionOrder,
  ProductionOrderQuery,
  ProductionOrderCreate,
  ProductionOrderUpdate,
  ProductionOrderStatus
} from '@/types/logistics/manufacturing/output/production-order'

// ========================================
// ProductionOrder相关 API（按后端控制器顺序）
// ========================================
const productionOrderUrl = '/api/TaktProductionOrders';

/**
 * 获取ProductionOrder列表（分页）
 * 对应后端：GetProductionOrderListAsync
 */
export function getProductionOrderList(params: ProductionOrderQuery): Promise<TaktPagedResult<ProductionOrder>> {
  return request({
    url: `${productionOrderUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ProductionOrder
 * 对应后端：GetProductionOrderByIdAsync
 */
export function getProductionOrderById(id: string): Promise<ProductionOrder> {
  return request({
    url: `${productionOrderUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ProductionOrder选项列表（用于下拉框等）
 * 对应后端：GetProductionOrderOptionsAsync
 */
export function getProductionOrderOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${productionOrderUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ProductionOrder
 * 对应后端：CreateProductionOrderAsync
 */
export function createProductionOrder(data: ProductionOrderCreate): Promise<ProductionOrder> {
  return request({
    url: productionOrderUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ProductionOrder
 * 对应后端：UpdateProductionOrderAsync
 */
export function updateProductionOrder(id: string, data: ProductionOrderUpdate): Promise<ProductionOrder> {
  return request({
    url: `${productionOrderUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ProductionOrder（单条）
 * 对应后端：DeleteProductionOrderByIdAsync
 */
export function deleteProductionOrderById(id: string): Promise<void> {
  return request({
    url: `${productionOrderUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ProductionOrder
 * 对应后端：DeleteProductionOrderBatchAsync
 */
export function deleteProductionOrderBatch(ids: string[]): Promise<void> {
  return request({
    url: `${productionOrderUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新ProductionOrder状态
 * 对应后端：UpdateProductionOrderStatusAsync
 */
export function updateProductionOrderStatus(data: ProductionOrderStatus): Promise<ProductionOrderStatus> {
  return request({
    url: `${productionOrderUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetProductionOrderTemplateAsync；fileName 仅传名称不含后缀
 */
export function getProductionOrderTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${productionOrderUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ProductionOrder
 * 对应后端：ImportProductionOrderAsync
 */
export function importProductionOrderData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${productionOrderUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ProductionOrder
 * 对应后端：ExportProductionOrderAsync；fileName 仅传名称不含后缀
 */
export function exportProductionOrderData(query: ProductionOrderQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${productionOrderUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
