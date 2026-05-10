// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：ipqc-order-change-log.ts
// 功能描述：IpqcOrderChangeLog API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktIpqcOrderChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  IpqcOrderChangeLog,
  IpqcOrderChangeLogQuery,
  IpqcOrderChangeLogCreate,
  IpqcOrderChangeLogUpdate
} from '@/types/logistics/quality/operation/ipqc-order-change-log'

// ========================================
// IpqcOrderChangeLog相关 API（按后端控制器顺序）
// ========================================
const ipqcOrderChangeLogUrl = '/api/TaktIpqcOrderChangeLogs';

/**
 * 获取IpqcOrderChangeLog列表（分页）
 * 对应后端：GetIpqcOrderChangeLogListAsync
 */
export function getIpqcOrderChangeLogList(params: IpqcOrderChangeLogQuery): Promise<TaktPagedResult<IpqcOrderChangeLog>> {
  return request({
    url: `${ipqcOrderChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取IpqcOrderChangeLog
 * 对应后端：GetIpqcOrderChangeLogByIdAsync
 */
export function getIpqcOrderChangeLogById(id: string): Promise<IpqcOrderChangeLog> {
  return request({
    url: `${ipqcOrderChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取IpqcOrderChangeLog选项列表（用于下拉框等）
 * 对应后端：GetIpqcOrderChangeLogOptionsAsync
 */
export function getIpqcOrderChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ipqcOrderChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建IpqcOrderChangeLog
 * 对应后端：CreateIpqcOrderChangeLogAsync
 */
export function createIpqcOrderChangeLog(data: IpqcOrderChangeLogCreate): Promise<IpqcOrderChangeLog> {
  return request({
    url: ipqcOrderChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新IpqcOrderChangeLog
 * 对应后端：UpdateIpqcOrderChangeLogAsync
 */
export function updateIpqcOrderChangeLog(id: string, data: IpqcOrderChangeLogUpdate): Promise<IpqcOrderChangeLog> {
  return request({
    url: `${ipqcOrderChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除IpqcOrderChangeLog（单条）
 * 对应后端：DeleteIpqcOrderChangeLogByIdAsync
 */
export function deleteIpqcOrderChangeLogById(id: string): Promise<void> {
  return request({
    url: `${ipqcOrderChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除IpqcOrderChangeLog
 * 对应后端：DeleteIpqcOrderChangeLogBatchAsync
 */
export function deleteIpqcOrderChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ipqcOrderChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetIpqcOrderChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getIpqcOrderChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ipqcOrderChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入IpqcOrderChangeLog
 * 对应后端：ImportIpqcOrderChangeLogAsync
 */
export function importIpqcOrderChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ipqcOrderChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出IpqcOrderChangeLog
 * 对应后端：ExportIpqcOrderChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportIpqcOrderChangeLogData(query: IpqcOrderChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ipqcOrderChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
