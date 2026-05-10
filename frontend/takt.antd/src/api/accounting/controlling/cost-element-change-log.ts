// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling
// 文件名称：cost-element-change-log.ts
// 功能描述：CostElementChangeLog API，对应后端 Takt.WebApi.Controllers.Accounting.Controlling.TaktCostElementChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CostElementChangeLog,
  CostElementChangeLogQuery,
  CostElementChangeLogCreate,
  CostElementChangeLogUpdate
} from '@/types/accounting/controlling/cost-element-change-log'

// ========================================
// CostElementChangeLog相关 API（按后端控制器顺序）
// ========================================
const costElementChangeLogUrl = '/api/TaktCostElementChangeLogs';

/**
 * 获取CostElementChangeLog列表（分页）
 * 对应后端：GetCostElementChangeLogListAsync
 */
export function getCostElementChangeLogList(params: CostElementChangeLogQuery): Promise<TaktPagedResult<CostElementChangeLog>> {
  return request({
    url: `${costElementChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CostElementChangeLog
 * 对应后端：GetCostElementChangeLogByIdAsync
 */
export function getCostElementChangeLogById(id: string): Promise<CostElementChangeLog> {
  return request({
    url: `${costElementChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CostElementChangeLog选项列表（用于下拉框等）
 * 对应后端：GetCostElementChangeLogOptionsAsync
 */
export function getCostElementChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${costElementChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建CostElementChangeLog
 * 对应后端：CreateCostElementChangeLogAsync
 */
export function createCostElementChangeLog(data: CostElementChangeLogCreate): Promise<CostElementChangeLog> {
  return request({
    url: costElementChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CostElementChangeLog
 * 对应后端：UpdateCostElementChangeLogAsync
 */
export function updateCostElementChangeLog(id: string, data: CostElementChangeLogUpdate): Promise<CostElementChangeLog> {
  return request({
    url: `${costElementChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CostElementChangeLog（单条）
 * 对应后端：DeleteCostElementChangeLogByIdAsync
 */
export function deleteCostElementChangeLogById(id: string): Promise<void> {
  return request({
    url: `${costElementChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CostElementChangeLog
 * 对应后端：DeleteCostElementChangeLogBatchAsync
 */
export function deleteCostElementChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${costElementChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCostElementChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCostElementChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${costElementChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CostElementChangeLog
 * 对应后端：ImportCostElementChangeLogAsync
 */
export function importCostElementChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${costElementChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CostElementChangeLog
 * 对应后端：ExportCostElementChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportCostElementChangeLogData(query: CostElementChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${costElementChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
