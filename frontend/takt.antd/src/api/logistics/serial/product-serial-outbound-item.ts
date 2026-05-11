// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/serial
// 文件名称：product-serial-outbound-item.ts
// 功能描述：ProductSerialOutboundItem API，对应后端 Takt.WebApi.Controllers.Logistics.Serial.TaktProductSerialOutboundItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ProductSerialOutboundItem,
  ProductSerialOutboundItemQuery,
  ProductSerialOutboundItemCreate,
  ProductSerialOutboundItemUpdate
} from '@/types/logistics/serial/product-serial-outbound-item'

// ========================================
// ProductSerialOutboundItem相关 API（按后端控制器顺序）
// ========================================
const productSerialOutboundItemUrl = '/api/TaktProductSerialOutboundItems';

/**
 * 获取ProductSerialOutboundItem列表（分页）
 * 对应后端：GetProductSerialOutboundItemListAsync
 */
export function getProductSerialOutboundItemList(params: ProductSerialOutboundItemQuery): Promise<TaktPagedResult<ProductSerialOutboundItem>> {
  return request({
    url: `${productSerialOutboundItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ProductSerialOutboundItem
 * 对应后端：GetProductSerialOutboundItemByIdAsync
 */
export function getProductSerialOutboundItemById(id: string): Promise<ProductSerialOutboundItem> {
  return request({
    url: `${productSerialOutboundItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ProductSerialOutboundItem选项列表（用于下拉框等）
 * 对应后端：GetProductSerialOutboundItemOptionsAsync
 */
export function getProductSerialOutboundItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${productSerialOutboundItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ProductSerialOutboundItem
 * 对应后端：CreateProductSerialOutboundItemAsync
 */
export function createProductSerialOutboundItem(data: ProductSerialOutboundItemCreate): Promise<ProductSerialOutboundItem> {
  return request({
    url: productSerialOutboundItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ProductSerialOutboundItem
 * 对应后端：UpdateProductSerialOutboundItemAsync
 */
export function updateProductSerialOutboundItem(id: string, data: ProductSerialOutboundItemUpdate): Promise<ProductSerialOutboundItem> {
  return request({
    url: `${productSerialOutboundItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ProductSerialOutboundItem（单条）
 * 对应后端：DeleteProductSerialOutboundItemByIdAsync
 */
export function deleteProductSerialOutboundItemById(id: string): Promise<void> {
  return request({
    url: `${productSerialOutboundItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ProductSerialOutboundItem
 * 对应后端：DeleteProductSerialOutboundItemBatchAsync
 */
export function deleteProductSerialOutboundItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${productSerialOutboundItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetProductSerialOutboundItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getProductSerialOutboundItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${productSerialOutboundItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ProductSerialOutboundItem
 * 对应后端：ImportProductSerialOutboundItemAsync
 */
export function importProductSerialOutboundItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${productSerialOutboundItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ProductSerialOutboundItem
 * 对应后端：ExportProductSerialOutboundItemAsync；fileName 仅传名称不含后缀
 */
export function exportProductSerialOutboundItemData(query: ProductSerialOutboundItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${productSerialOutboundItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
