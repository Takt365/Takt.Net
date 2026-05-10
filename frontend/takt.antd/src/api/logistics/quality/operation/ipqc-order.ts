// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：ipqc-order.ts
// 功能描述：IpqcOrder API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktIpqcOrders
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  IpqcOrder,
  IpqcOrderQuery,
  IpqcOrderCreate,
  IpqcOrderUpdate
} from '@/types/logistics/quality/operation/ipqc-order'

// ========================================
// IpqcOrder相关 API（按后端控制器顺序）
// ========================================
const ipqcOrderUrl = '/api/TaktIpqcOrders';

/**
 * 获取IpqcOrder列表（分页）
 * 对应后端：GetIpqcOrderListAsync
 */
export function getIpqcOrderList(params: IpqcOrderQuery): Promise<TaktPagedResult<IpqcOrder>> {
  return request({
    url: `${ipqcOrderUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取IpqcOrder
 * 对应后端：GetIpqcOrderByIdAsync
 */
export function getIpqcOrderById(id: string): Promise<IpqcOrder> {
  return request({
    url: `${ipqcOrderUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取IpqcOrder选项列表（用于下拉框等）
 * 对应后端：GetIpqcOrderOptionsAsync
 */
export function getIpqcOrderOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ipqcOrderUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建IpqcOrder
 * 对应后端：CreateIpqcOrderAsync
 */
export function createIpqcOrder(data: IpqcOrderCreate): Promise<IpqcOrder> {
  return request({
    url: ipqcOrderUrl,
    method: 'post',
    data
  })
}

/**
 * 更新IpqcOrder
 * 对应后端：UpdateIpqcOrderAsync
 */
export function updateIpqcOrder(id: string, data: IpqcOrderUpdate): Promise<IpqcOrder> {
  return request({
    url: `${ipqcOrderUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除IpqcOrder（单条）
 * 对应后端：DeleteIpqcOrderByIdAsync
 */
export function deleteIpqcOrderById(id: string): Promise<void> {
  return request({
    url: `${ipqcOrderUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除IpqcOrder
 * 对应后端：DeleteIpqcOrderBatchAsync
 */
export function deleteIpqcOrderBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ipqcOrderUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetIpqcOrderTemplateAsync；fileName 仅传名称不含后缀
 */
export function getIpqcOrderTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ipqcOrderUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入IpqcOrder
 * 对应后端：ImportIpqcOrderAsync
 */
export function importIpqcOrderData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ipqcOrderUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出IpqcOrder
 * 对应后端：ExportIpqcOrderAsync；fileName 仅传名称不含后缀
 */
export function exportIpqcOrderData(query: IpqcOrderQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ipqcOrderUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
