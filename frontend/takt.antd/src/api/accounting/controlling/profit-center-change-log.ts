// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling
// 文件名称：profit-center-change-log.ts
// 功能描述：ProfitCenterChangeLog API，对应后端 Takt.WebApi.Controllers.Accounting.Controlling.TaktProfitCenterChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ProfitCenterChangeLog,
  ProfitCenterChangeLogQuery,
  ProfitCenterChangeLogCreate,
  ProfitCenterChangeLogUpdate
} from '@/types/accounting/controlling/profit-center-change-log'

// ========================================
// ProfitCenterChangeLog相关 API（按后端控制器顺序）
// ========================================
const profitCenterChangeLogUrl = '/api/TaktProfitCenterChangeLogs';

/**
 * 获取ProfitCenterChangeLog列表（分页）
 * 对应后端：GetProfitCenterChangeLogListAsync
 */
export function getProfitCenterChangeLogList(params: ProfitCenterChangeLogQuery): Promise<TaktPagedResult<ProfitCenterChangeLog>> {
  return request({
    url: `${profitCenterChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ProfitCenterChangeLog
 * 对应后端：GetProfitCenterChangeLogByIdAsync
 */
export function getProfitCenterChangeLogById(id: string): Promise<ProfitCenterChangeLog> {
  return request({
    url: `${profitCenterChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ProfitCenterChangeLog选项列表（用于下拉框等）
 * 对应后端：GetProfitCenterChangeLogOptionsAsync
 */
export function getProfitCenterChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${profitCenterChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ProfitCenterChangeLog
 * 对应后端：CreateProfitCenterChangeLogAsync
 */
export function createProfitCenterChangeLog(data: ProfitCenterChangeLogCreate): Promise<ProfitCenterChangeLog> {
  return request({
    url: profitCenterChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ProfitCenterChangeLog
 * 对应后端：UpdateProfitCenterChangeLogAsync
 */
export function updateProfitCenterChangeLog(id: string, data: ProfitCenterChangeLogUpdate): Promise<ProfitCenterChangeLog> {
  return request({
    url: `${profitCenterChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ProfitCenterChangeLog（单条）
 * 对应后端：DeleteProfitCenterChangeLogByIdAsync
 */
export function deleteProfitCenterChangeLogById(id: string): Promise<void> {
  return request({
    url: `${profitCenterChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ProfitCenterChangeLog
 * 对应后端：DeleteProfitCenterChangeLogBatchAsync
 */
export function deleteProfitCenterChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${profitCenterChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetProfitCenterChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getProfitCenterChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${profitCenterChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ProfitCenterChangeLog
 * 对应后端：ImportProfitCenterChangeLogAsync
 */
export function importProfitCenterChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${profitCenterChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ProfitCenterChangeLog
 * 对应后端：ExportProfitCenterChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportProfitCenterChangeLogData(query: ProfitCenterChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${profitCenterChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
