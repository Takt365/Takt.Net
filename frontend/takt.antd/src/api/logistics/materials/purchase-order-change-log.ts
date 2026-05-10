// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：purchase-order-change-log.ts
// 功能描述：PurchaseOrderChangeLog API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPurchaseOrderChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PurchaseOrderChangeLog,
  PurchaseOrderChangeLogQuery,
  PurchaseOrderChangeLogCreate,
  PurchaseOrderChangeLogUpdate
} from '@/types/logistics/materials/purchase-order-change-log'

// ========================================
// PurchaseOrderChangeLog相关 API（按后端控制器顺序）
// ========================================
const purchaseOrderChangeLogUrl = '/api/TaktPurchaseOrderChangeLogs';

/**
 * 获取PurchaseOrderChangeLog列表（分页）
 * 对应后端：GetPurchaseOrderChangeLogListAsync
 */
export function getPurchaseOrderChangeLogList(params: PurchaseOrderChangeLogQuery): Promise<TaktPagedResult<PurchaseOrderChangeLog>> {
  return request({
    url: `${purchaseOrderChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PurchaseOrderChangeLog
 * 对应后端：GetPurchaseOrderChangeLogByIdAsync
 */
export function getPurchaseOrderChangeLogById(id: string): Promise<PurchaseOrderChangeLog> {
  return request({
    url: `${purchaseOrderChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PurchaseOrderChangeLog选项列表（用于下拉框等）
 * 对应后端：GetPurchaseOrderChangeLogOptionsAsync
 */
export function getPurchaseOrderChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${purchaseOrderChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PurchaseOrderChangeLog
 * 对应后端：CreatePurchaseOrderChangeLogAsync
 */
export function createPurchaseOrderChangeLog(data: PurchaseOrderChangeLogCreate): Promise<PurchaseOrderChangeLog> {
  return request({
    url: purchaseOrderChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PurchaseOrderChangeLog
 * 对应后端：UpdatePurchaseOrderChangeLogAsync
 */
export function updatePurchaseOrderChangeLog(id: string, data: PurchaseOrderChangeLogUpdate): Promise<PurchaseOrderChangeLog> {
  return request({
    url: `${purchaseOrderChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PurchaseOrderChangeLog（单条）
 * 对应后端：DeletePurchaseOrderChangeLogByIdAsync
 */
export function deletePurchaseOrderChangeLogById(id: string): Promise<void> {
  return request({
    url: `${purchaseOrderChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PurchaseOrderChangeLog
 * 对应后端：DeletePurchaseOrderChangeLogBatchAsync
 */
export function deletePurchaseOrderChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${purchaseOrderChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPurchaseOrderChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPurchaseOrderChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchaseOrderChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PurchaseOrderChangeLog
 * 对应后端：ImportPurchaseOrderChangeLogAsync
 */
export function importPurchaseOrderChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${purchaseOrderChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PurchaseOrderChangeLog
 * 对应后端：ExportPurchaseOrderChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportPurchaseOrderChangeLogData(query: PurchaseOrderChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchaseOrderChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
