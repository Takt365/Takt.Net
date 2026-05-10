// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling
// 文件名称：cost-center-change-log.ts
// 功能描述：CostCenterChangeLog API，对应后端 Takt.WebApi.Controllers.Accounting.Controlling.TaktCostCenterChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CostCenterChangeLog,
  CostCenterChangeLogQuery,
  CostCenterChangeLogCreate,
  CostCenterChangeLogUpdate
} from '@/types/accounting/controlling/cost-center-change-log'

// ========================================
// CostCenterChangeLog相关 API（按后端控制器顺序）
// ========================================
const costCenterChangeLogUrl = '/api/TaktCostCenterChangeLogs';

/**
 * 获取CostCenterChangeLog列表（分页）
 * 对应后端：GetCostCenterChangeLogListAsync
 */
export function getCostCenterChangeLogList(params: CostCenterChangeLogQuery): Promise<TaktPagedResult<CostCenterChangeLog>> {
  return request({
    url: `${costCenterChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CostCenterChangeLog
 * 对应后端：GetCostCenterChangeLogByIdAsync
 */
export function getCostCenterChangeLogById(id: string): Promise<CostCenterChangeLog> {
  return request({
    url: `${costCenterChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CostCenterChangeLog选项列表（用于下拉框等）
 * 对应后端：GetCostCenterChangeLogOptionsAsync
 */
export function getCostCenterChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${costCenterChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建CostCenterChangeLog
 * 对应后端：CreateCostCenterChangeLogAsync
 */
export function createCostCenterChangeLog(data: CostCenterChangeLogCreate): Promise<CostCenterChangeLog> {
  return request({
    url: costCenterChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CostCenterChangeLog
 * 对应后端：UpdateCostCenterChangeLogAsync
 */
export function updateCostCenterChangeLog(id: string, data: CostCenterChangeLogUpdate): Promise<CostCenterChangeLog> {
  return request({
    url: `${costCenterChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CostCenterChangeLog（单条）
 * 对应后端：DeleteCostCenterChangeLogByIdAsync
 */
export function deleteCostCenterChangeLogById(id: string): Promise<void> {
  return request({
    url: `${costCenterChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CostCenterChangeLog
 * 对应后端：DeleteCostCenterChangeLogBatchAsync
 */
export function deleteCostCenterChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${costCenterChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCostCenterChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCostCenterChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${costCenterChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CostCenterChangeLog
 * 对应后端：ImportCostCenterChangeLogAsync
 */
export function importCostCenterChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${costCenterChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CostCenterChangeLog
 * 对应后端：ExportCostCenterChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportCostCenterChangeLogData(query: CostCenterChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${costCenterChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
