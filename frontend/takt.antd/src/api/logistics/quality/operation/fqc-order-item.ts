// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：fqc-order-item.ts
// 功能描述：FqcOrderItem API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktFqcOrderItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  FqcOrderItem,
  FqcOrderItemQuery,
  FqcOrderItemCreate,
  FqcOrderItemUpdate
} from '@/types/logistics/quality/operation/fqc-order-item'

// ========================================
// FqcOrderItem相关 API（按后端控制器顺序）
// ========================================
const fqcOrderItemUrl = '/api/TaktFqcOrderItems';

/**
 * 获取FqcOrderItem列表（分页）
 * 对应后端：GetFqcOrderItemListAsync
 */
export function getFqcOrderItemList(params: FqcOrderItemQuery): Promise<TaktPagedResult<FqcOrderItem>> {
  return request({
    url: `${fqcOrderItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取FqcOrderItem
 * 对应后端：GetFqcOrderItemByIdAsync
 */
export function getFqcOrderItemById(id: string): Promise<FqcOrderItem> {
  return request({
    url: `${fqcOrderItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取FqcOrderItem选项列表（用于下拉框等）
 * 对应后端：GetFqcOrderItemOptionsAsync
 */
export function getFqcOrderItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${fqcOrderItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建FqcOrderItem
 * 对应后端：CreateFqcOrderItemAsync
 */
export function createFqcOrderItem(data: FqcOrderItemCreate): Promise<FqcOrderItem> {
  return request({
    url: fqcOrderItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新FqcOrderItem
 * 对应后端：UpdateFqcOrderItemAsync
 */
export function updateFqcOrderItem(id: string, data: FqcOrderItemUpdate): Promise<FqcOrderItem> {
  return request({
    url: `${fqcOrderItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除FqcOrderItem（单条）
 * 对应后端：DeleteFqcOrderItemByIdAsync
 */
export function deleteFqcOrderItemById(id: string): Promise<void> {
  return request({
    url: `${fqcOrderItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除FqcOrderItem
 * 对应后端：DeleteFqcOrderItemBatchAsync
 */
export function deleteFqcOrderItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${fqcOrderItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetFqcOrderItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getFqcOrderItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${fqcOrderItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入FqcOrderItem
 * 对应后端：ImportFqcOrderItemAsync
 */
export function importFqcOrderItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${fqcOrderItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出FqcOrderItem
 * 对应后端：ExportFqcOrderItemAsync；fileName 仅传名称不含后缀
 */
export function exportFqcOrderItemData(query: FqcOrderItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${fqcOrderItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
