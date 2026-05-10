// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：fqc-order-change-log.ts
// 功能描述：FqcOrderChangeLog API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktFqcOrderChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  FqcOrderChangeLog,
  FqcOrderChangeLogQuery,
  FqcOrderChangeLogCreate,
  FqcOrderChangeLogUpdate
} from '@/types/logistics/quality/operation/fqc-order-change-log'

// ========================================
// FqcOrderChangeLog相关 API（按后端控制器顺序）
// ========================================
const fqcOrderChangeLogUrl = '/api/TaktFqcOrderChangeLogs';

/**
 * 获取FqcOrderChangeLog列表（分页）
 * 对应后端：GetFqcOrderChangeLogListAsync
 */
export function getFqcOrderChangeLogList(params: FqcOrderChangeLogQuery): Promise<TaktPagedResult<FqcOrderChangeLog>> {
  return request({
    url: `${fqcOrderChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取FqcOrderChangeLog
 * 对应后端：GetFqcOrderChangeLogByIdAsync
 */
export function getFqcOrderChangeLogById(id: string): Promise<FqcOrderChangeLog> {
  return request({
    url: `${fqcOrderChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取FqcOrderChangeLog选项列表（用于下拉框等）
 * 对应后端：GetFqcOrderChangeLogOptionsAsync
 */
export function getFqcOrderChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${fqcOrderChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建FqcOrderChangeLog
 * 对应后端：CreateFqcOrderChangeLogAsync
 */
export function createFqcOrderChangeLog(data: FqcOrderChangeLogCreate): Promise<FqcOrderChangeLog> {
  return request({
    url: fqcOrderChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新FqcOrderChangeLog
 * 对应后端：UpdateFqcOrderChangeLogAsync
 */
export function updateFqcOrderChangeLog(id: string, data: FqcOrderChangeLogUpdate): Promise<FqcOrderChangeLog> {
  return request({
    url: `${fqcOrderChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除FqcOrderChangeLog（单条）
 * 对应后端：DeleteFqcOrderChangeLogByIdAsync
 */
export function deleteFqcOrderChangeLogById(id: string): Promise<void> {
  return request({
    url: `${fqcOrderChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除FqcOrderChangeLog
 * 对应后端：DeleteFqcOrderChangeLogBatchAsync
 */
export function deleteFqcOrderChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${fqcOrderChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetFqcOrderChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getFqcOrderChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${fqcOrderChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入FqcOrderChangeLog
 * 对应后端：ImportFqcOrderChangeLogAsync
 */
export function importFqcOrderChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${fqcOrderChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出FqcOrderChangeLog
 * 对应后端：ExportFqcOrderChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportFqcOrderChangeLogData(query: FqcOrderChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${fqcOrderChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
