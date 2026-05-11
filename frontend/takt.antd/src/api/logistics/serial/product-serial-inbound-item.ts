// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/serial
// 文件名称：product-serial-inbound-item.ts
// 功能描述：ProductSerialInboundItem API，对应后端 Takt.WebApi.Controllers.Logistics.Serial.TaktProductSerialInboundItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ProductSerialInboundItem,
  ProductSerialInboundItemQuery,
  ProductSerialInboundItemCreate,
  ProductSerialInboundItemUpdate
} from '@/types/logistics/serial/product-serial-inbound-item'

// ========================================
// ProductSerialInboundItem相关 API（按后端控制器顺序）
// ========================================
const productSerialInboundItemUrl = '/api/TaktProductSerialInboundItems';

/**
 * 获取ProductSerialInboundItem列表（分页）
 * 对应后端：GetProductSerialInboundItemListAsync
 */
export function getProductSerialInboundItemList(params: ProductSerialInboundItemQuery): Promise<TaktPagedResult<ProductSerialInboundItem>> {
  return request({
    url: `${productSerialInboundItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ProductSerialInboundItem
 * 对应后端：GetProductSerialInboundItemByIdAsync
 */
export function getProductSerialInboundItemById(id: string): Promise<ProductSerialInboundItem> {
  return request({
    url: `${productSerialInboundItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ProductSerialInboundItem选项列表（用于下拉框等）
 * 对应后端：GetProductSerialInboundItemOptionsAsync
 */
export function getProductSerialInboundItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${productSerialInboundItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ProductSerialInboundItem
 * 对应后端：CreateProductSerialInboundItemAsync
 */
export function createProductSerialInboundItem(data: ProductSerialInboundItemCreate): Promise<ProductSerialInboundItem> {
  return request({
    url: productSerialInboundItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ProductSerialInboundItem
 * 对应后端：UpdateProductSerialInboundItemAsync
 */
export function updateProductSerialInboundItem(id: string, data: ProductSerialInboundItemUpdate): Promise<ProductSerialInboundItem> {
  return request({
    url: `${productSerialInboundItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ProductSerialInboundItem（单条）
 * 对应后端：DeleteProductSerialInboundItemByIdAsync
 */
export function deleteProductSerialInboundItemById(id: string): Promise<void> {
  return request({
    url: `${productSerialInboundItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ProductSerialInboundItem
 * 对应后端：DeleteProductSerialInboundItemBatchAsync
 */
export function deleteProductSerialInboundItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${productSerialInboundItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetProductSerialInboundItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getProductSerialInboundItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${productSerialInboundItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ProductSerialInboundItem
 * 对应后端：ImportProductSerialInboundItemAsync
 */
export function importProductSerialInboundItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${productSerialInboundItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ProductSerialInboundItem
 * 对应后端：ExportProductSerialInboundItemAsync；fileName 仅传名称不含后缀
 */
export function exportProductSerialInboundItemData(query: ProductSerialInboundItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${productSerialInboundItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
