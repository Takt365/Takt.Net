// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/sales
// 文件名称：sales-order-change-log.ts
// 功能描述：SalesOrderChangeLog API，对应后端 Takt.WebApi.Controllers.Logistics.Sales.TaktSalesOrderChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SalesOrderChangeLog,
  SalesOrderChangeLogQuery,
  SalesOrderChangeLogCreate,
  SalesOrderChangeLogUpdate
} from '@/types/logistics/sales/sales-order-change-log'

// ========================================
// SalesOrderChangeLog相关 API（按后端控制器顺序）
// ========================================
const salesOrderChangeLogUrl = '/api/TaktSalesOrderChangeLogs';

/**
 * 获取SalesOrderChangeLog列表（分页）
 * 对应后端：GetSalesOrderChangeLogListAsync
 */
export function getSalesOrderChangeLogList(params: SalesOrderChangeLogQuery): Promise<TaktPagedResult<SalesOrderChangeLog>> {
  return request({
    url: `${salesOrderChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SalesOrderChangeLog
 * 对应后端：GetSalesOrderChangeLogByIdAsync
 */
export function getSalesOrderChangeLogById(id: string): Promise<SalesOrderChangeLog> {
  return request({
    url: `${salesOrderChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SalesOrderChangeLog选项列表（用于下拉框等）
 * 对应后端：GetSalesOrderChangeLogOptionsAsync
 */
export function getSalesOrderChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${salesOrderChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SalesOrderChangeLog
 * 对应后端：CreateSalesOrderChangeLogAsync
 */
export function createSalesOrderChangeLog(data: SalesOrderChangeLogCreate): Promise<SalesOrderChangeLog> {
  return request({
    url: salesOrderChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SalesOrderChangeLog
 * 对应后端：UpdateSalesOrderChangeLogAsync
 */
export function updateSalesOrderChangeLog(id: string, data: SalesOrderChangeLogUpdate): Promise<SalesOrderChangeLog> {
  return request({
    url: `${salesOrderChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SalesOrderChangeLog（单条）
 * 对应后端：DeleteSalesOrderChangeLogByIdAsync
 */
export function deleteSalesOrderChangeLogById(id: string): Promise<void> {
  return request({
    url: `${salesOrderChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SalesOrderChangeLog
 * 对应后端：DeleteSalesOrderChangeLogBatchAsync
 */
export function deleteSalesOrderChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${salesOrderChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSalesOrderChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSalesOrderChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${salesOrderChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SalesOrderChangeLog
 * 对应后端：ImportSalesOrderChangeLogAsync
 */
export function importSalesOrderChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${salesOrderChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SalesOrderChangeLog
 * 对应后端：ExportSalesOrderChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportSalesOrderChangeLogData(query: SalesOrderChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${salesOrderChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
