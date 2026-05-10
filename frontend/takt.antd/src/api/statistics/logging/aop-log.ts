// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/statistics/logging
// 文件名称：aop-log.ts
// 功能描述：AopLog API，对应后端 Takt.WebApi.Controllers.Statistics.Logging.TaktAopLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AopLog,
  AopLogQuery,
  AopLogCreate,
  AopLogUpdate
} from '@/types/statistics/logging/aop-log'

// ========================================
// AopLog相关 API（按后端控制器顺序）
// ========================================
const aopLogUrl = '/api/TaktAopLogs';

/**
 * 获取AopLog列表（分页）
 * 对应后端：GetAopLogListAsync
 */
export function getAopLogList(params: AopLogQuery): Promise<TaktPagedResult<AopLog>> {
  return request({
    url: `${aopLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AopLog
 * 对应后端：GetAopLogByIdAsync
 */
export function getAopLogById(id: string): Promise<AopLog> {
  return request({
    url: `${aopLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AopLog选项列表（用于下拉框等）
 * 对应后端：GetAopLogOptionsAsync
 */
export function getAopLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${aopLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AopLog
 * 对应后端：CreateAopLogAsync
 */
export function createAopLog(data: AopLogCreate): Promise<AopLog> {
  return request({
    url: aopLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AopLog
 * 对应后端：UpdateAopLogAsync
 */
export function updateAopLog(id: string, data: AopLogUpdate): Promise<AopLog> {
  return request({
    url: `${aopLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AopLog（单条）
 * 对应后端：DeleteAopLogByIdAsync
 */
export function deleteAopLogById(id: string): Promise<void> {
  return request({
    url: `${aopLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AopLog
 * 对应后端：DeleteAopLogBatchAsync
 */
export function deleteAopLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${aopLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAopLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAopLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${aopLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AopLog
 * 对应后端：ImportAopLogAsync
 */
export function importAopLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${aopLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AopLog
 * 对应后端：ExportAopLogAsync；fileName 仅传名称不含后缀
 */
export function exportAopLogData(query: AopLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${aopLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
