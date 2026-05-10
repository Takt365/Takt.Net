// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/performance
// 文件名称：performance-review.ts
// 功能描述：PerformanceReview API，对应后端 Takt.WebApi.Controllers.HumanResource.Performance.TaktPerformanceReviews
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PerformanceReview,
  PerformanceReviewQuery,
  PerformanceReviewCreate,
  PerformanceReviewUpdate,
  PerformanceReviewStatus
} from '@/types/human-resource/performance/performance-review'

// ========================================
// PerformanceReview相关 API（按后端控制器顺序）
// ========================================
const performanceReviewUrl = '/api/TaktPerformanceReviews';

/**
 * 获取PerformanceReview列表（分页）
 * 对应后端：GetPerformanceReviewListAsync
 */
export function getPerformanceReviewList(params: PerformanceReviewQuery): Promise<TaktPagedResult<PerformanceReview>> {
  return request({
    url: `${performanceReviewUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PerformanceReview
 * 对应后端：GetPerformanceReviewByIdAsync
 */
export function getPerformanceReviewById(id: string): Promise<PerformanceReview> {
  return request({
    url: `${performanceReviewUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PerformanceReview选项列表（用于下拉框等）
 * 对应后端：GetPerformanceReviewOptionsAsync
 */
export function getPerformanceReviewOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${performanceReviewUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PerformanceReview
 * 对应后端：CreatePerformanceReviewAsync
 */
export function createPerformanceReview(data: PerformanceReviewCreate): Promise<PerformanceReview> {
  return request({
    url: performanceReviewUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PerformanceReview
 * 对应后端：UpdatePerformanceReviewAsync
 */
export function updatePerformanceReview(id: string, data: PerformanceReviewUpdate): Promise<PerformanceReview> {
  return request({
    url: `${performanceReviewUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PerformanceReview（单条）
 * 对应后端：DeletePerformanceReviewByIdAsync
 */
export function deletePerformanceReviewById(id: string): Promise<void> {
  return request({
    url: `${performanceReviewUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PerformanceReview
 * 对应后端：DeletePerformanceReviewBatchAsync
 */
export function deletePerformanceReviewBatch(ids: string[]): Promise<void> {
  return request({
    url: `${performanceReviewUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新PerformanceReview状态
 * 对应后端：UpdatePerformanceReviewStatusAsync
 */
export function updatePerformanceReviewStatus(data: PerformanceReviewStatus): Promise<PerformanceReviewStatus> {
  return request({
    url: `${performanceReviewUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPerformanceReviewTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPerformanceReviewTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${performanceReviewUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PerformanceReview
 * 对应后端：ImportPerformanceReviewAsync
 */
export function importPerformanceReviewData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${performanceReviewUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PerformanceReview
 * 对应后端：ExportPerformanceReviewAsync；fileName 仅传名称不含后缀
 */
export function exportPerformanceReviewData(query: PerformanceReviewQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${performanceReviewUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
