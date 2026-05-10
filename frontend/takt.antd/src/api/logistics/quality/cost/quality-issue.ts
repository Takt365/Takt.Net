// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-issue.ts
// 功能描述：QualityIssue API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityIssues
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityIssue,
  QualityIssueQuery,
  QualityIssueCreate,
  QualityIssueUpdate
} from '@/types/logistics/quality/cost/quality-issue'

// ========================================
// QualityIssue相关 API（按后端控制器顺序）
// ========================================
const qualityIssueUrl = '/api/TaktQualityIssues';

/**
 * 获取QualityIssue列表（分页）
 * 对应后端：GetQualityIssueListAsync
 */
export function getQualityIssueList(params: QualityIssueQuery): Promise<TaktPagedResult<QualityIssue>> {
  return request({
    url: `${qualityIssueUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityIssue
 * 对应后端：GetQualityIssueByIdAsync
 */
export function getQualityIssueById(id: string): Promise<QualityIssue> {
  return request({
    url: `${qualityIssueUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityIssue选项列表（用于下拉框等）
 * 对应后端：GetQualityIssueOptionsAsync
 */
export function getQualityIssueOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityIssueUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityIssue
 * 对应后端：CreateQualityIssueAsync
 */
export function createQualityIssue(data: QualityIssueCreate): Promise<QualityIssue> {
  return request({
    url: qualityIssueUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityIssue
 * 对应后端：UpdateQualityIssueAsync
 */
export function updateQualityIssue(id: string, data: QualityIssueUpdate): Promise<QualityIssue> {
  return request({
    url: `${qualityIssueUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityIssue（单条）
 * 对应后端：DeleteQualityIssueByIdAsync
 */
export function deleteQualityIssueById(id: string): Promise<void> {
  return request({
    url: `${qualityIssueUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityIssue
 * 对应后端：DeleteQualityIssueBatchAsync
 */
export function deleteQualityIssueBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityIssueUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityIssueTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityIssueTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityIssueUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityIssue
 * 对应后端：ImportQualityIssueAsync
 */
export function importQualityIssueData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityIssueUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityIssue
 * 对应后端：ExportQualityIssueAsync；fileName 仅传名称不含后缀
 */
export function exportQualityIssueData(query: QualityIssueQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityIssueUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
