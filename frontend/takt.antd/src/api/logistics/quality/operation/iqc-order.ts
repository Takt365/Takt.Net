// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：iqc-order.ts
// 功能描述：IqcOrder API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktIqcOrders
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  IqcOrder,
  IqcOrderQuery,
  IqcOrderCreate,
  IqcOrderUpdate
} from '@/types/logistics/quality/operation/iqc-order'

// ========================================
// IqcOrder相关 API（按后端控制器顺序）
// ========================================
const iqcOrderUrl = '/api/TaktIqcOrders';

/**
 * 获取IqcOrder列表（分页）
 * 对应后端：GetIqcOrderListAsync
 */
export function getIqcOrderList(params: IqcOrderQuery): Promise<TaktPagedResult<IqcOrder>> {
  return request({
    url: `${iqcOrderUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取IqcOrder
 * 对应后端：GetIqcOrderByIdAsync
 */
export function getIqcOrderById(id: string): Promise<IqcOrder> {
  return request({
    url: `${iqcOrderUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取IqcOrder选项列表（用于下拉框等）
 * 对应后端：GetIqcOrderOptionsAsync
 */
export function getIqcOrderOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${iqcOrderUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建IqcOrder
 * 对应后端：CreateIqcOrderAsync
 */
export function createIqcOrder(data: IqcOrderCreate): Promise<IqcOrder> {
  return request({
    url: iqcOrderUrl,
    method: 'post',
    data
  })
}

/**
 * 更新IqcOrder
 * 对应后端：UpdateIqcOrderAsync
 */
export function updateIqcOrder(id: string, data: IqcOrderUpdate): Promise<IqcOrder> {
  return request({
    url: `${iqcOrderUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除IqcOrder（单条）
 * 对应后端：DeleteIqcOrderByIdAsync
 */
export function deleteIqcOrderById(id: string): Promise<void> {
  return request({
    url: `${iqcOrderUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除IqcOrder
 * 对应后端：DeleteIqcOrderBatchAsync
 */
export function deleteIqcOrderBatch(ids: string[]): Promise<void> {
  return request({
    url: `${iqcOrderUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetIqcOrderTemplateAsync；fileName 仅传名称不含后缀
 */
export function getIqcOrderTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${iqcOrderUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入IqcOrder
 * 对应后端：ImportIqcOrderAsync
 */
export function importIqcOrderData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${iqcOrderUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出IqcOrder
 * 对应后端：ExportIqcOrderAsync；fileName 仅传名称不含后缀
 */
export function exportIqcOrderData(query: IqcOrderQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${iqcOrderUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
