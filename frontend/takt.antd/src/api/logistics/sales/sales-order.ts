// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/sales
// 文件名称：sales-order.ts
// 功能描述：SalesOrder API，对应后端 Takt.WebApi.Controllers.Logistics.Sales.TaktSalesOrders
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SalesOrder,
  SalesOrderQuery,
  SalesOrderCreate,
  SalesOrderUpdate
} from '@/types/logistics/sales/sales-order'

// ========================================
// SalesOrder相关 API（按后端控制器顺序）
// ========================================
const salesOrderUrl = '/api/TaktSalesOrders';

/**
 * 获取SalesOrder列表（分页）
 * 对应后端：GetSalesOrderListAsync
 */
export function getSalesOrderList(params: SalesOrderQuery): Promise<TaktPagedResult<SalesOrder>> {
  return request({
    url: `${salesOrderUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SalesOrder
 * 对应后端：GetSalesOrderByIdAsync
 */
export function getSalesOrderById(id: string): Promise<SalesOrder> {
  return request({
    url: `${salesOrderUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SalesOrder选项列表（用于下拉框等）
 * 对应后端：GetSalesOrderOptionsAsync
 */
export function getSalesOrderOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${salesOrderUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SalesOrder
 * 对应后端：CreateSalesOrderAsync
 */
export function createSalesOrder(data: SalesOrderCreate): Promise<SalesOrder> {
  return request({
    url: salesOrderUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SalesOrder
 * 对应后端：UpdateSalesOrderAsync
 */
export function updateSalesOrder(id: string, data: SalesOrderUpdate): Promise<SalesOrder> {
  return request({
    url: `${salesOrderUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SalesOrder（单条）
 * 对应后端：DeleteSalesOrderByIdAsync
 */
export function deleteSalesOrderById(id: string): Promise<void> {
  return request({
    url: `${salesOrderUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SalesOrder
 * 对应后端：DeleteSalesOrderBatchAsync
 */
export function deleteSalesOrderBatch(ids: string[]): Promise<void> {
  return request({
    url: `${salesOrderUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSalesOrderTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSalesOrderTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${salesOrderUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SalesOrder
 * 对应后端：ImportSalesOrderAsync
 */
export function importSalesOrderData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${salesOrderUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SalesOrder
 * 对应后端：ExportSalesOrderAsync；fileName 仅传名称不含后缀
 */
export function exportSalesOrderData(query: SalesOrderQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${salesOrderUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
