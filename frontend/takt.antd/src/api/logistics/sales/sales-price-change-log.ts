// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/sales
// 文件名称：sales-price-change-log.ts
// 功能描述：SalesPriceChangeLog API，对应后端 Takt.WebApi.Controllers.Logistics.Sales.TaktSalesPriceChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SalesPriceChangeLog,
  SalesPriceChangeLogQuery,
  SalesPriceChangeLogCreate,
  SalesPriceChangeLogUpdate
} from '@/types/logistics/sales/sales-price-change-log'

// ========================================
// SalesPriceChangeLog相关 API（按后端控制器顺序）
// ========================================
const salesPriceChangeLogUrl = '/api/TaktSalesPriceChangeLogs';

/**
 * 获取SalesPriceChangeLog列表（分页）
 * 对应后端：GetSalesPriceChangeLogListAsync
 */
export function getSalesPriceChangeLogList(params: SalesPriceChangeLogQuery): Promise<TaktPagedResult<SalesPriceChangeLog>> {
  return request({
    url: `${salesPriceChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SalesPriceChangeLog
 * 对应后端：GetSalesPriceChangeLogByIdAsync
 */
export function getSalesPriceChangeLogById(id: string): Promise<SalesPriceChangeLog> {
  return request({
    url: `${salesPriceChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SalesPriceChangeLog选项列表（用于下拉框等）
 * 对应后端：GetSalesPriceChangeLogOptionsAsync
 */
export function getSalesPriceChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${salesPriceChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SalesPriceChangeLog
 * 对应后端：CreateSalesPriceChangeLogAsync
 */
export function createSalesPriceChangeLog(data: SalesPriceChangeLogCreate): Promise<SalesPriceChangeLog> {
  return request({
    url: salesPriceChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SalesPriceChangeLog
 * 对应后端：UpdateSalesPriceChangeLogAsync
 */
export function updateSalesPriceChangeLog(id: string, data: SalesPriceChangeLogUpdate): Promise<SalesPriceChangeLog> {
  return request({
    url: `${salesPriceChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SalesPriceChangeLog（单条）
 * 对应后端：DeleteSalesPriceChangeLogByIdAsync
 */
export function deleteSalesPriceChangeLogById(id: string): Promise<void> {
  return request({
    url: `${salesPriceChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SalesPriceChangeLog
 * 对应后端：DeleteSalesPriceChangeLogBatchAsync
 */
export function deleteSalesPriceChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${salesPriceChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSalesPriceChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSalesPriceChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${salesPriceChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SalesPriceChangeLog
 * 对应后端：ImportSalesPriceChangeLogAsync
 */
export function importSalesPriceChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${salesPriceChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SalesPriceChangeLog
 * 对应后端：ExportSalesPriceChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportSalesPriceChangeLogData(query: SalesPriceChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${salesPriceChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
