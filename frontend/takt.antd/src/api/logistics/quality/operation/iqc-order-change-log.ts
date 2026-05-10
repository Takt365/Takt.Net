// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：iqc-order-change-log.ts
// 功能描述：IqcOrderChangeLog API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktIqcOrderChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  IqcOrderChangeLog,
  IqcOrderChangeLogQuery,
  IqcOrderChangeLogCreate,
  IqcOrderChangeLogUpdate
} from '@/types/logistics/quality/operation/iqc-order-change-log'

// ========================================
// IqcOrderChangeLog相关 API（按后端控制器顺序）
// ========================================
const iqcOrderChangeLogUrl = '/api/TaktIqcOrderChangeLogs';

/**
 * 获取IqcOrderChangeLog列表（分页）
 * 对应后端：GetIqcOrderChangeLogListAsync
 */
export function getIqcOrderChangeLogList(params: IqcOrderChangeLogQuery): Promise<TaktPagedResult<IqcOrderChangeLog>> {
  return request({
    url: `${iqcOrderChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取IqcOrderChangeLog
 * 对应后端：GetIqcOrderChangeLogByIdAsync
 */
export function getIqcOrderChangeLogById(id: string): Promise<IqcOrderChangeLog> {
  return request({
    url: `${iqcOrderChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取IqcOrderChangeLog选项列表（用于下拉框等）
 * 对应后端：GetIqcOrderChangeLogOptionsAsync
 */
export function getIqcOrderChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${iqcOrderChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建IqcOrderChangeLog
 * 对应后端：CreateIqcOrderChangeLogAsync
 */
export function createIqcOrderChangeLog(data: IqcOrderChangeLogCreate): Promise<IqcOrderChangeLog> {
  return request({
    url: iqcOrderChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新IqcOrderChangeLog
 * 对应后端：UpdateIqcOrderChangeLogAsync
 */
export function updateIqcOrderChangeLog(id: string, data: IqcOrderChangeLogUpdate): Promise<IqcOrderChangeLog> {
  return request({
    url: `${iqcOrderChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除IqcOrderChangeLog（单条）
 * 对应后端：DeleteIqcOrderChangeLogByIdAsync
 */
export function deleteIqcOrderChangeLogById(id: string): Promise<void> {
  return request({
    url: `${iqcOrderChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除IqcOrderChangeLog
 * 对应后端：DeleteIqcOrderChangeLogBatchAsync
 */
export function deleteIqcOrderChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${iqcOrderChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetIqcOrderChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getIqcOrderChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${iqcOrderChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入IqcOrderChangeLog
 * 对应后端：ImportIqcOrderChangeLogAsync
 */
export function importIqcOrderChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${iqcOrderChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出IqcOrderChangeLog
 * 对应后端：ExportIqcOrderChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportIqcOrderChangeLogData(query: IqcOrderChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${iqcOrderChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
