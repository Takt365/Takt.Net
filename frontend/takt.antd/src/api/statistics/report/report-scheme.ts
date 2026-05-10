// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/statistics/report
// 文件名称：report-scheme.ts
// 功能描述：ReportScheme API，对应后端 Takt.WebApi.Controllers.Statistics.Report.TaktReportSchemes
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ReportScheme,
  ReportSchemeQuery,
  ReportSchemeCreate,
  ReportSchemeUpdate,
  ReportSchemeStatus,
  ReportSchemeSort
} from '@/types/statistics/report/report-scheme'

// ========================================
// ReportScheme相关 API（按后端控制器顺序）
// ========================================
const reportSchemeUrl = '/api/TaktReportSchemes';

/**
 * 获取ReportScheme列表（分页）
 * 对应后端：GetReportSchemeListAsync
 */
export function getReportSchemeList(params: ReportSchemeQuery): Promise<TaktPagedResult<ReportScheme>> {
  return request({
    url: `${reportSchemeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ReportScheme
 * 对应后端：GetReportSchemeByIdAsync
 */
export function getReportSchemeById(id: string): Promise<ReportScheme> {
  return request({
    url: `${reportSchemeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ReportScheme选项列表（用于下拉框等）
 * 对应后端：GetReportSchemeOptionsAsync
 */
export function getReportSchemeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${reportSchemeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ReportScheme
 * 对应后端：CreateReportSchemeAsync
 */
export function createReportScheme(data: ReportSchemeCreate): Promise<ReportScheme> {
  return request({
    url: reportSchemeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ReportScheme
 * 对应后端：UpdateReportSchemeAsync
 */
export function updateReportScheme(id: string, data: ReportSchemeUpdate): Promise<ReportScheme> {
  return request({
    url: `${reportSchemeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ReportScheme（单条）
 * 对应后端：DeleteReportSchemeByIdAsync
 */
export function deleteReportSchemeById(id: string): Promise<void> {
  return request({
    url: `${reportSchemeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ReportScheme
 * 对应后端：DeleteReportSchemeBatchAsync
 */
export function deleteReportSchemeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${reportSchemeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新ReportScheme状态
 * 对应后端：UpdateReportSchemeStatusAsync
 */
export function updateReportSchemeStatus(data: ReportSchemeStatus): Promise<ReportSchemeStatus> {
  return request({
    url: `${reportSchemeUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新ReportScheme排序
 * 对应后端：UpdateReportSchemeSortAsync
 */
export function updateReportSchemeSort(data: ReportSchemeSort): Promise<ReportSchemeSort> {
  return request({
    url: `${reportSchemeUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetReportSchemeTemplateAsync；fileName 仅传名称不含后缀
 */
export function getReportSchemeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${reportSchemeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ReportScheme
 * 对应后端：ImportReportSchemeAsync
 */
export function importReportSchemeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${reportSchemeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ReportScheme
 * 对应后端：ExportReportSchemeAsync；fileName 仅传名称不含后缀
 */
export function exportReportSchemeData(query: ReportSchemeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${reportSchemeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
