// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：purchase-price-change-log.ts
// 功能描述：PurchasePriceChangeLog API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPurchasePriceChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PurchasePriceChangeLog,
  PurchasePriceChangeLogQuery,
  PurchasePriceChangeLogCreate,
  PurchasePriceChangeLogUpdate
} from '@/types/logistics/materials/purchase-price-change-log'

// ========================================
// PurchasePriceChangeLog相关 API（按后端控制器顺序）
// ========================================
const purchasePriceChangeLogUrl = '/api/TaktPurchasePriceChangeLogs';

/**
 * 获取PurchasePriceChangeLog列表（分页）
 * 对应后端：GetPurchasePriceChangeLogListAsync
 */
export function getPurchasePriceChangeLogList(params: PurchasePriceChangeLogQuery): Promise<TaktPagedResult<PurchasePriceChangeLog>> {
  return request({
    url: `${purchasePriceChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PurchasePriceChangeLog
 * 对应后端：GetPurchasePriceChangeLogByIdAsync
 */
export function getPurchasePriceChangeLogById(id: string): Promise<PurchasePriceChangeLog> {
  return request({
    url: `${purchasePriceChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PurchasePriceChangeLog选项列表（用于下拉框等）
 * 对应后端：GetPurchasePriceChangeLogOptionsAsync
 */
export function getPurchasePriceChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${purchasePriceChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PurchasePriceChangeLog
 * 对应后端：CreatePurchasePriceChangeLogAsync
 */
export function createPurchasePriceChangeLog(data: PurchasePriceChangeLogCreate): Promise<PurchasePriceChangeLog> {
  return request({
    url: purchasePriceChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PurchasePriceChangeLog
 * 对应后端：UpdatePurchasePriceChangeLogAsync
 */
export function updatePurchasePriceChangeLog(id: string, data: PurchasePriceChangeLogUpdate): Promise<PurchasePriceChangeLog> {
  return request({
    url: `${purchasePriceChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PurchasePriceChangeLog（单条）
 * 对应后端：DeletePurchasePriceChangeLogByIdAsync
 */
export function deletePurchasePriceChangeLogById(id: string): Promise<void> {
  return request({
    url: `${purchasePriceChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PurchasePriceChangeLog
 * 对应后端：DeletePurchasePriceChangeLogBatchAsync
 */
export function deletePurchasePriceChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${purchasePriceChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPurchasePriceChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPurchasePriceChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchasePriceChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PurchasePriceChangeLog
 * 对应后端：ImportPurchasePriceChangeLogAsync
 */
export function importPurchasePriceChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${purchasePriceChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PurchasePriceChangeLog
 * 对应后端：ExportPurchasePriceChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportPurchasePriceChangeLogData(query: PurchasePriceChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchasePriceChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
