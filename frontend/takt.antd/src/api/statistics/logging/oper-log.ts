// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/statistics/logging
// 文件名称：oper-log.ts
// 功能描述：OperLog API，对应后端 Takt.WebApi.Controllers.Statistics.Logging.TaktOperLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  OperLog,
  OperLogQuery,
  OperLogCreate,
  OperLogUpdate
} from '@/types/statistics/logging/oper-log'

// ========================================
// OperLog相关 API（按后端控制器顺序）
// ========================================
const operLogUrl = '/api/TaktOperLogs';

/**
 * 获取OperLog列表（分页）
 * 对应后端：GetOperLogListAsync
 */
export function getOperLogList(params: OperLogQuery): Promise<TaktPagedResult<OperLog>> {
  return request({
    url: `${operLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取OperLog
 * 对应后端：GetOperLogByIdAsync
 */
export function getOperLogById(id: string): Promise<OperLog> {
  return request({
    url: `${operLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取OperLog选项列表（用于下拉框等）
 * 对应后端：GetOperLogOptionsAsync
 */
export function getOperLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${operLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建OperLog
 * 对应后端：CreateOperLogAsync
 */
export function createOperLog(data: OperLogCreate): Promise<OperLog> {
  return request({
    url: operLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新OperLog
 * 对应后端：UpdateOperLogAsync
 */
export function updateOperLog(id: string, data: OperLogUpdate): Promise<OperLog> {
  return request({
    url: `${operLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除OperLog（单条）
 * 对应后端：DeleteOperLogByIdAsync
 */
export function deleteOperLogById(id: string): Promise<void> {
  return request({
    url: `${operLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除OperLog
 * 对应后端：DeleteOperLogBatchAsync
 */
export function deleteOperLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${operLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetOperLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getOperLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${operLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入OperLog
 * 对应后端：ImportOperLogAsync
 */
export function importOperLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${operLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出OperLog
 * 对应后端：ExportOperLogAsync；fileName 仅传名称不含后缀
 */
export function exportOperLogData(query: OperLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${operLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
