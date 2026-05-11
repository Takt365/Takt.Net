// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/serial
// 文件名称：product-serial-outbound.ts
// 功能描述：ProductSerialOutbound API，对应后端 Takt.WebApi.Controllers.Logistics.Serial.TaktProductSerialOutbounds
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ProductSerialOutbound,
  ProductSerialOutboundQuery,
  ProductSerialOutboundCreate,
  ProductSerialOutboundUpdate
} from '@/types/logistics/serial/product-serial-outbound'

// ========================================
// ProductSerialOutbound相关 API（按后端控制器顺序）
// ========================================
const productSerialOutboundUrl = '/api/TaktProductSerialOutbounds';

/**
 * 获取ProductSerialOutbound列表（分页）
 * 对应后端：GetProductSerialOutboundListAsync
 */
export function getProductSerialOutboundList(params: ProductSerialOutboundQuery): Promise<TaktPagedResult<ProductSerialOutbound>> {
  return request({
    url: `${productSerialOutboundUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ProductSerialOutbound
 * 对应后端：GetProductSerialOutboundByIdAsync
 */
export function getProductSerialOutboundById(id: string): Promise<ProductSerialOutbound> {
  return request({
    url: `${productSerialOutboundUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ProductSerialOutbound选项列表（用于下拉框等）
 * 对应后端：GetProductSerialOutboundOptionsAsync
 */
export function getProductSerialOutboundOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${productSerialOutboundUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ProductSerialOutbound
 * 对应后端：CreateProductSerialOutboundAsync
 */
export function createProductSerialOutbound(data: ProductSerialOutboundCreate): Promise<ProductSerialOutbound> {
  return request({
    url: productSerialOutboundUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ProductSerialOutbound
 * 对应后端：UpdateProductSerialOutboundAsync
 */
export function updateProductSerialOutbound(id: string, data: ProductSerialOutboundUpdate): Promise<ProductSerialOutbound> {
  return request({
    url: `${productSerialOutboundUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ProductSerialOutbound（单条）
 * 对应后端：DeleteProductSerialOutboundByIdAsync
 */
export function deleteProductSerialOutboundById(id: string): Promise<void> {
  return request({
    url: `${productSerialOutboundUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ProductSerialOutbound
 * 对应后端：DeleteProductSerialOutboundBatchAsync
 */
export function deleteProductSerialOutboundBatch(ids: string[]): Promise<void> {
  return request({
    url: `${productSerialOutboundUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetProductSerialOutboundTemplateAsync；fileName 仅传名称不含后缀
 */
export function getProductSerialOutboundTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${productSerialOutboundUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ProductSerialOutbound
 * 对应后端：ImportProductSerialOutboundAsync
 */
export function importProductSerialOutboundData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${productSerialOutboundUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ProductSerialOutbound
 * 对应后端：ExportProductSerialOutboundAsync；fileName 仅传名称不含后缀
 */
export function exportProductSerialOutboundData(query: ProductSerialOutboundQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${productSerialOutboundUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
