// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/bom
// 文件名称：bill-of-material-change-log.ts
// 功能描述：BillOfMaterialChangeLog API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Bom.TaktBillOfMaterialChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  BillOfMaterialChangeLog,
  BillOfMaterialChangeLogQuery,
  BillOfMaterialChangeLogCreate,
  BillOfMaterialChangeLogUpdate
} from '@/types/logistics/manufacturing/bom/bill-of-material-change-log'

// ========================================
// BillOfMaterialChangeLog相关 API（按后端控制器顺序）
// ========================================
const billOfMaterialChangeLogUrl = '/api/TaktBillOfMaterialChangeLogs';

/**
 * 获取BillOfMaterialChangeLog列表（分页）
 * 对应后端：GetBillOfMaterialChangeLogListAsync
 */
export function getBillOfMaterialChangeLogList(params: BillOfMaterialChangeLogQuery): Promise<TaktPagedResult<BillOfMaterialChangeLog>> {
  return request({
    url: `${billOfMaterialChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取BillOfMaterialChangeLog
 * 对应后端：GetBillOfMaterialChangeLogByIdAsync
 */
export function getBillOfMaterialChangeLogById(id: string): Promise<BillOfMaterialChangeLog> {
  return request({
    url: `${billOfMaterialChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取BillOfMaterialChangeLog选项列表（用于下拉框等）
 * 对应后端：GetBillOfMaterialChangeLogOptionsAsync
 */
export function getBillOfMaterialChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${billOfMaterialChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建BillOfMaterialChangeLog
 * 对应后端：CreateBillOfMaterialChangeLogAsync
 */
export function createBillOfMaterialChangeLog(data: BillOfMaterialChangeLogCreate): Promise<BillOfMaterialChangeLog> {
  return request({
    url: billOfMaterialChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新BillOfMaterialChangeLog
 * 对应后端：UpdateBillOfMaterialChangeLogAsync
 */
export function updateBillOfMaterialChangeLog(id: string, data: BillOfMaterialChangeLogUpdate): Promise<BillOfMaterialChangeLog> {
  return request({
    url: `${billOfMaterialChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除BillOfMaterialChangeLog（单条）
 * 对应后端：DeleteBillOfMaterialChangeLogByIdAsync
 */
export function deleteBillOfMaterialChangeLogById(id: string): Promise<void> {
  return request({
    url: `${billOfMaterialChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除BillOfMaterialChangeLog
 * 对应后端：DeleteBillOfMaterialChangeLogBatchAsync
 */
export function deleteBillOfMaterialChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${billOfMaterialChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetBillOfMaterialChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getBillOfMaterialChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${billOfMaterialChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入BillOfMaterialChangeLog
 * 对应后端：ImportBillOfMaterialChangeLogAsync
 */
export function importBillOfMaterialChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${billOfMaterialChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出BillOfMaterialChangeLog
 * 对应后端：ExportBillOfMaterialChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportBillOfMaterialChangeLogData(query: BillOfMaterialChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${billOfMaterialChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
