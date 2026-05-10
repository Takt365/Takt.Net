// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：fqc-order.ts
// 功能描述：FqcOrder API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktFqcOrders
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  FqcOrder,
  FqcOrderQuery,
  FqcOrderCreate,
  FqcOrderUpdate
} from '@/types/logistics/quality/operation/fqc-order'

// ========================================
// FqcOrder相关 API（按后端控制器顺序）
// ========================================
const fqcOrderUrl = '/api/TaktFqcOrders';

/**
 * 获取FqcOrder列表（分页）
 * 对应后端：GetFqcOrderListAsync
 */
export function getFqcOrderList(params: FqcOrderQuery): Promise<TaktPagedResult<FqcOrder>> {
  return request({
    url: `${fqcOrderUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取FqcOrder
 * 对应后端：GetFqcOrderByIdAsync
 */
export function getFqcOrderById(id: string): Promise<FqcOrder> {
  return request({
    url: `${fqcOrderUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取FqcOrder选项列表（用于下拉框等）
 * 对应后端：GetFqcOrderOptionsAsync
 */
export function getFqcOrderOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${fqcOrderUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建FqcOrder
 * 对应后端：CreateFqcOrderAsync
 */
export function createFqcOrder(data: FqcOrderCreate): Promise<FqcOrder> {
  return request({
    url: fqcOrderUrl,
    method: 'post',
    data
  })
}

/**
 * 更新FqcOrder
 * 对应后端：UpdateFqcOrderAsync
 */
export function updateFqcOrder(id: string, data: FqcOrderUpdate): Promise<FqcOrder> {
  return request({
    url: `${fqcOrderUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除FqcOrder（单条）
 * 对应后端：DeleteFqcOrderByIdAsync
 */
export function deleteFqcOrderById(id: string): Promise<void> {
  return request({
    url: `${fqcOrderUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除FqcOrder
 * 对应后端：DeleteFqcOrderBatchAsync
 */
export function deleteFqcOrderBatch(ids: string[]): Promise<void> {
  return request({
    url: `${fqcOrderUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetFqcOrderTemplateAsync；fileName 仅传名称不含后缀
 */
export function getFqcOrderTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${fqcOrderUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入FqcOrder
 * 对应后端：ImportFqcOrderAsync
 */
export function importFqcOrderData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${fqcOrderUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出FqcOrder
 * 对应后端：ExportFqcOrderAsync；fileName 仅传名称不含后缀
 */
export function exportFqcOrderData(query: FqcOrderQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${fqcOrderUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
