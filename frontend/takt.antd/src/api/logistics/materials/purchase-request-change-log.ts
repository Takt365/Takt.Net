// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：purchase-request-change-log.ts
// 功能描述：PurchaseRequestChangeLog API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPurchaseRequestChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PurchaseRequestChangeLog,
  PurchaseRequestChangeLogQuery,
  PurchaseRequestChangeLogCreate,
  PurchaseRequestChangeLogUpdate
} from '@/types/logistics/materials/purchase-request-change-log'

// ========================================
// PurchaseRequestChangeLog相关 API（按后端控制器顺序）
// ========================================
const purchaseRequestChangeLogUrl = '/api/TaktPurchaseRequestChangeLogs';

/**
 * 获取PurchaseRequestChangeLog列表（分页）
 * 对应后端：GetPurchaseRequestChangeLogListAsync
 */
export function getPurchaseRequestChangeLogList(params: PurchaseRequestChangeLogQuery): Promise<TaktPagedResult<PurchaseRequestChangeLog>> {
  return request({
    url: `${purchaseRequestChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PurchaseRequestChangeLog
 * 对应后端：GetPurchaseRequestChangeLogByIdAsync
 */
export function getPurchaseRequestChangeLogById(id: string): Promise<PurchaseRequestChangeLog> {
  return request({
    url: `${purchaseRequestChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PurchaseRequestChangeLog选项列表（用于下拉框等）
 * 对应后端：GetPurchaseRequestChangeLogOptionsAsync
 */
export function getPurchaseRequestChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${purchaseRequestChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PurchaseRequestChangeLog
 * 对应后端：CreatePurchaseRequestChangeLogAsync
 */
export function createPurchaseRequestChangeLog(data: PurchaseRequestChangeLogCreate): Promise<PurchaseRequestChangeLog> {
  return request({
    url: purchaseRequestChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PurchaseRequestChangeLog
 * 对应后端：UpdatePurchaseRequestChangeLogAsync
 */
export function updatePurchaseRequestChangeLog(id: string, data: PurchaseRequestChangeLogUpdate): Promise<PurchaseRequestChangeLog> {
  return request({
    url: `${purchaseRequestChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PurchaseRequestChangeLog（单条）
 * 对应后端：DeletePurchaseRequestChangeLogByIdAsync
 */
export function deletePurchaseRequestChangeLogById(id: string): Promise<void> {
  return request({
    url: `${purchaseRequestChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PurchaseRequestChangeLog
 * 对应后端：DeletePurchaseRequestChangeLogBatchAsync
 */
export function deletePurchaseRequestChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${purchaseRequestChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPurchaseRequestChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPurchaseRequestChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchaseRequestChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PurchaseRequestChangeLog
 * 对应后端：ImportPurchaseRequestChangeLogAsync
 */
export function importPurchaseRequestChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${purchaseRequestChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PurchaseRequestChangeLog
 * 对应后端：ExportPurchaseRequestChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportPurchaseRequestChangeLogData(query: PurchaseRequestChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchaseRequestChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
