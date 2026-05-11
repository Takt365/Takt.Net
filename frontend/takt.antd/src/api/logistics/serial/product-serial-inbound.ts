// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/serial
// 文件名称：product-serial-inbound.ts
// 功能描述：ProductSerialInbound API，对应后端 Takt.WebApi.Controllers.Logistics.Serial.TaktProductSerialInbounds
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ProductSerialInbound,
  ProductSerialInboundQuery,
  ProductSerialInboundCreate,
  ProductSerialInboundUpdate
} from '@/types/logistics/serial/product-serial-inbound'

// ========================================
// ProductSerialInbound相关 API（按后端控制器顺序）
// ========================================
const productSerialInboundUrl = '/api/TaktProductSerialInbounds';

/**
 * 获取ProductSerialInbound列表（分页）
 * 对应后端：GetProductSerialInboundListAsync
 */
export function getProductSerialInboundList(params: ProductSerialInboundQuery): Promise<TaktPagedResult<ProductSerialInbound>> {
  return request({
    url: `${productSerialInboundUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ProductSerialInbound
 * 对应后端：GetProductSerialInboundByIdAsync
 */
export function getProductSerialInboundById(id: string): Promise<ProductSerialInbound> {
  return request({
    url: `${productSerialInboundUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ProductSerialInbound选项列表（用于下拉框等）
 * 对应后端：GetProductSerialInboundOptionsAsync
 */
export function getProductSerialInboundOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${productSerialInboundUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ProductSerialInbound
 * 对应后端：CreateProductSerialInboundAsync
 */
export function createProductSerialInbound(data: ProductSerialInboundCreate): Promise<ProductSerialInbound> {
  return request({
    url: productSerialInboundUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ProductSerialInbound
 * 对应后端：UpdateProductSerialInboundAsync
 */
export function updateProductSerialInbound(id: string, data: ProductSerialInboundUpdate): Promise<ProductSerialInbound> {
  return request({
    url: `${productSerialInboundUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ProductSerialInbound（单条）
 * 对应后端：DeleteProductSerialInboundByIdAsync
 */
export function deleteProductSerialInboundById(id: string): Promise<void> {
  return request({
    url: `${productSerialInboundUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ProductSerialInbound
 * 对应后端：DeleteProductSerialInboundBatchAsync
 */
export function deleteProductSerialInboundBatch(ids: string[]): Promise<void> {
  return request({
    url: `${productSerialInboundUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetProductSerialInboundTemplateAsync；fileName 仅传名称不含后缀
 */
export function getProductSerialInboundTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${productSerialInboundUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ProductSerialInbound
 * 对应后端：ImportProductSerialInboundAsync
 */
export function importProductSerialInboundData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${productSerialInboundUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ProductSerialInbound
 * 对应后端：ExportProductSerialInboundAsync；fileName 仅传名称不含后缀
 */
export function exportProductSerialInboundData(query: ProductSerialInboundQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${productSerialInboundUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
