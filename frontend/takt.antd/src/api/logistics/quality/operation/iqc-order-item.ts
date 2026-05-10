// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：iqc-order-item.ts
// 功能描述：IqcOrderItem API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktIqcOrderItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  IqcOrderItem,
  IqcOrderItemQuery,
  IqcOrderItemCreate,
  IqcOrderItemUpdate
} from '@/types/logistics/quality/operation/iqc-order-item'

// ========================================
// IqcOrderItem相关 API（按后端控制器顺序）
// ========================================
const iqcOrderItemUrl = '/api/TaktIqcOrderItems';

/**
 * 获取IqcOrderItem列表（分页）
 * 对应后端：GetIqcOrderItemListAsync
 */
export function getIqcOrderItemList(params: IqcOrderItemQuery): Promise<TaktPagedResult<IqcOrderItem>> {
  return request({
    url: `${iqcOrderItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取IqcOrderItem
 * 对应后端：GetIqcOrderItemByIdAsync
 */
export function getIqcOrderItemById(id: string): Promise<IqcOrderItem> {
  return request({
    url: `${iqcOrderItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取IqcOrderItem选项列表（用于下拉框等）
 * 对应后端：GetIqcOrderItemOptionsAsync
 */
export function getIqcOrderItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${iqcOrderItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建IqcOrderItem
 * 对应后端：CreateIqcOrderItemAsync
 */
export function createIqcOrderItem(data: IqcOrderItemCreate): Promise<IqcOrderItem> {
  return request({
    url: iqcOrderItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新IqcOrderItem
 * 对应后端：UpdateIqcOrderItemAsync
 */
export function updateIqcOrderItem(id: string, data: IqcOrderItemUpdate): Promise<IqcOrderItem> {
  return request({
    url: `${iqcOrderItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除IqcOrderItem（单条）
 * 对应后端：DeleteIqcOrderItemByIdAsync
 */
export function deleteIqcOrderItemById(id: string): Promise<void> {
  return request({
    url: `${iqcOrderItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除IqcOrderItem
 * 对应后端：DeleteIqcOrderItemBatchAsync
 */
export function deleteIqcOrderItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${iqcOrderItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetIqcOrderItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getIqcOrderItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${iqcOrderItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入IqcOrderItem
 * 对应后端：ImportIqcOrderItemAsync
 */
export function importIqcOrderItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${iqcOrderItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出IqcOrderItem
 * 对应后端：ExportIqcOrderItemAsync；fileName 仅传名称不含后缀
 */
export function exportIqcOrderItemData(query: IqcOrderItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${iqcOrderItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
