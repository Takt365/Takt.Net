// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/statistics/logging
// 文件名称：quartz-log.ts
// 功能描述：QuartzLog API，对应后端 Takt.WebApi.Controllers.Statistics.Logging.TaktQuartzLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QuartzLog,
  QuartzLogQuery,
  QuartzLogCreate,
  QuartzLogUpdate
} from '@/types/statistics/logging/quartz-log'

// ========================================
// QuartzLog相关 API（按后端控制器顺序）
// ========================================
const quartzLogUrl = '/api/TaktQuartzLogs';

/**
 * 获取QuartzLog列表（分页）
 * 对应后端：GetQuartzLogListAsync
 */
export function getQuartzLogList(params: QuartzLogQuery): Promise<TaktPagedResult<QuartzLog>> {
  return request({
    url: `${quartzLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QuartzLog
 * 对应后端：GetQuartzLogByIdAsync
 */
export function getQuartzLogById(id: string): Promise<QuartzLog> {
  return request({
    url: `${quartzLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QuartzLog选项列表（用于下拉框等）
 * 对应后端：GetQuartzLogOptionsAsync
 */
export function getQuartzLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${quartzLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QuartzLog
 * 对应后端：CreateQuartzLogAsync
 */
export function createQuartzLog(data: QuartzLogCreate): Promise<QuartzLog> {
  return request({
    url: quartzLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QuartzLog
 * 对应后端：UpdateQuartzLogAsync
 */
export function updateQuartzLog(id: string, data: QuartzLogUpdate): Promise<QuartzLog> {
  return request({
    url: `${quartzLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QuartzLog（单条）
 * 对应后端：DeleteQuartzLogByIdAsync
 */
export function deleteQuartzLogById(id: string): Promise<void> {
  return request({
    url: `${quartzLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QuartzLog
 * 对应后端：DeleteQuartzLogBatchAsync
 */
export function deleteQuartzLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${quartzLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQuartzLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQuartzLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${quartzLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QuartzLog
 * 对应后端：ImportQuartzLogAsync
 */
export function importQuartzLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${quartzLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QuartzLog
 * 对应后端：ExportQuartzLogAsync；fileName 仅传名称不含后缀
 */
export function exportQuartzLogData(query: QuartzLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${quartzLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
