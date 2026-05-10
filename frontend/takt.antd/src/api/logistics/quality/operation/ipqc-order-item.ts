// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：ipqc-order-item.ts
// 功能描述：IpqcOrderItem API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktIpqcOrderItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  IpqcOrderItem,
  IpqcOrderItemQuery,
  IpqcOrderItemCreate,
  IpqcOrderItemUpdate
} from '@/types/logistics/quality/operation/ipqc-order-item'

// ========================================
// IpqcOrderItem相关 API（按后端控制器顺序）
// ========================================
const ipqcOrderItemUrl = '/api/TaktIpqcOrderItems';

/**
 * 获取IpqcOrderItem列表（分页）
 * 对应后端：GetIpqcOrderItemListAsync
 */
export function getIpqcOrderItemList(params: IpqcOrderItemQuery): Promise<TaktPagedResult<IpqcOrderItem>> {
  return request({
    url: `${ipqcOrderItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取IpqcOrderItem
 * 对应后端：GetIpqcOrderItemByIdAsync
 */
export function getIpqcOrderItemById(id: string): Promise<IpqcOrderItem> {
  return request({
    url: `${ipqcOrderItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取IpqcOrderItem选项列表（用于下拉框等）
 * 对应后端：GetIpqcOrderItemOptionsAsync
 */
export function getIpqcOrderItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ipqcOrderItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建IpqcOrderItem
 * 对应后端：CreateIpqcOrderItemAsync
 */
export function createIpqcOrderItem(data: IpqcOrderItemCreate): Promise<IpqcOrderItem> {
  return request({
    url: ipqcOrderItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新IpqcOrderItem
 * 对应后端：UpdateIpqcOrderItemAsync
 */
export function updateIpqcOrderItem(id: string, data: IpqcOrderItemUpdate): Promise<IpqcOrderItem> {
  return request({
    url: `${ipqcOrderItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除IpqcOrderItem（单条）
 * 对应后端：DeleteIpqcOrderItemByIdAsync
 */
export function deleteIpqcOrderItemById(id: string): Promise<void> {
  return request({
    url: `${ipqcOrderItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除IpqcOrderItem
 * 对应后端：DeleteIpqcOrderItemBatchAsync
 */
export function deleteIpqcOrderItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ipqcOrderItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetIpqcOrderItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getIpqcOrderItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ipqcOrderItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入IpqcOrderItem
 * 对应后端：ImportIpqcOrderItemAsync
 */
export function importIpqcOrderItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ipqcOrderItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出IpqcOrderItem
 * 对应后端：ExportIpqcOrderItemAsync；fileName 仅传名称不含后缀
 */
export function exportIpqcOrderItemData(query: IpqcOrderItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ipqcOrderItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
