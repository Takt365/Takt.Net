// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/statistics/report
// 文件名称：report-execution-log.ts
// 功能描述：ReportExecutionLog API，对应后端 Takt.WebApi.Controllers.Statistics.Report.TaktReportExecutionLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ReportExecutionLog,
  ReportExecutionLogQuery,
  ReportExecutionLogCreate,
  ReportExecutionLogUpdate
} from '@/types/statistics/report/report-execution-log'

// ========================================
// ReportExecutionLog相关 API（按后端控制器顺序）
// ========================================
const reportExecutionLogUrl = '/api/TaktReportExecutionLogs';

/**
 * 获取ReportExecutionLog列表（分页）
 * 对应后端：GetReportExecutionLogListAsync
 */
export function getReportExecutionLogList(params: ReportExecutionLogQuery): Promise<TaktPagedResult<ReportExecutionLog>> {
  return request({
    url: `${reportExecutionLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ReportExecutionLog
 * 对应后端：GetReportExecutionLogByIdAsync
 */
export function getReportExecutionLogById(id: string): Promise<ReportExecutionLog> {
  return request({
    url: `${reportExecutionLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ReportExecutionLog选项列表（用于下拉框等）
 * 对应后端：GetReportExecutionLogOptionsAsync
 */
export function getReportExecutionLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${reportExecutionLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ReportExecutionLog
 * 对应后端：CreateReportExecutionLogAsync
 */
export function createReportExecutionLog(data: ReportExecutionLogCreate): Promise<ReportExecutionLog> {
  return request({
    url: reportExecutionLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ReportExecutionLog
 * 对应后端：UpdateReportExecutionLogAsync
 */
export function updateReportExecutionLog(id: string, data: ReportExecutionLogUpdate): Promise<ReportExecutionLog> {
  return request({
    url: `${reportExecutionLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ReportExecutionLog（单条）
 * 对应后端：DeleteReportExecutionLogByIdAsync
 */
export function deleteReportExecutionLogById(id: string): Promise<void> {
  return request({
    url: `${reportExecutionLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ReportExecutionLog
 * 对应后端：DeleteReportExecutionLogBatchAsync
 */
export function deleteReportExecutionLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${reportExecutionLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetReportExecutionLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getReportExecutionLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${reportExecutionLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ReportExecutionLog
 * 对应后端：ImportReportExecutionLogAsync
 */
export function importReportExecutionLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${reportExecutionLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ReportExecutionLog
 * 对应后端：ExportReportExecutionLogAsync；fileName 仅传名称不含后缀
 */
export function exportReportExecutionLogData(query: ReportExecutionLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${reportExecutionLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
