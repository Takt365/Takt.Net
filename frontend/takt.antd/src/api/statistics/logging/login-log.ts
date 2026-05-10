// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/statistics/logging
// 文件名称：login-log.ts
// 功能描述：LoginLog API，对应后端 Takt.WebApi.Controllers.Statistics.Logging.TaktLoginLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  LoginLog,
  LoginLogQuery,
  LoginLogCreate,
  LoginLogUpdate
} from '@/types/statistics/logging/login-log'

// ========================================
// LoginLog相关 API（按后端控制器顺序）
// ========================================
const loginLogUrl = '/api/TaktLoginLogs';

/**
 * 获取LoginLog列表（分页）
 * 对应后端：GetLoginLogListAsync
 */
export function getLoginLogList(params: LoginLogQuery): Promise<TaktPagedResult<LoginLog>> {
  return request({
    url: `${loginLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取LoginLog
 * 对应后端：GetLoginLogByIdAsync
 */
export function getLoginLogById(id: string): Promise<LoginLog> {
  return request({
    url: `${loginLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取LoginLog选项列表（用于下拉框等）
 * 对应后端：GetLoginLogOptionsAsync
 */
export function getLoginLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${loginLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建LoginLog
 * 对应后端：CreateLoginLogAsync
 */
export function createLoginLog(data: LoginLogCreate): Promise<LoginLog> {
  return request({
    url: loginLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新LoginLog
 * 对应后端：UpdateLoginLogAsync
 */
export function updateLoginLog(id: string, data: LoginLogUpdate): Promise<LoginLog> {
  return request({
    url: `${loginLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除LoginLog（单条）
 * 对应后端：DeleteLoginLogByIdAsync
 */
export function deleteLoginLogById(id: string): Promise<void> {
  return request({
    url: `${loginLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除LoginLog
 * 对应后端：DeleteLoginLogBatchAsync
 */
export function deleteLoginLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${loginLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetLoginLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getLoginLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${loginLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入LoginLog
 * 对应后端：ImportLoginLogAsync
 */
export function importLoginLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${loginLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出LoginLog
 * 对应后端：ExportLoginLogAsync；fileName 仅传名称不含后缀
 */
export function exportLoginLogData(query: LoginLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${loginLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
