// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/performance
// 文件名称：review-cycle.ts
// 功能描述：ReviewCycle API，对应后端 Takt.WebApi.Controllers.HumanResource.Performance.TaktReviewCycles
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ReviewCycle,
  ReviewCycleQuery,
  ReviewCycleCreate,
  ReviewCycleUpdate,
  ReviewCycleStatus
} from '@/types/human-resource/performance/review-cycle'

// ========================================
// ReviewCycle相关 API（按后端控制器顺序）
// ========================================
const reviewCycleUrl = '/api/TaktReviewCycles';

/**
 * 获取ReviewCycle列表（分页）
 * 对应后端：GetReviewCycleListAsync
 */
export function getReviewCycleList(params: ReviewCycleQuery): Promise<TaktPagedResult<ReviewCycle>> {
  return request({
    url: `${reviewCycleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ReviewCycle
 * 对应后端：GetReviewCycleByIdAsync
 */
export function getReviewCycleById(id: string): Promise<ReviewCycle> {
  return request({
    url: `${reviewCycleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ReviewCycle选项列表（用于下拉框等）
 * 对应后端：GetReviewCycleOptionsAsync
 */
export function getReviewCycleOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${reviewCycleUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ReviewCycle
 * 对应后端：CreateReviewCycleAsync
 */
export function createReviewCycle(data: ReviewCycleCreate): Promise<ReviewCycle> {
  return request({
    url: reviewCycleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ReviewCycle
 * 对应后端：UpdateReviewCycleAsync
 */
export function updateReviewCycle(id: string, data: ReviewCycleUpdate): Promise<ReviewCycle> {
  return request({
    url: `${reviewCycleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ReviewCycle（单条）
 * 对应后端：DeleteReviewCycleByIdAsync
 */
export function deleteReviewCycleById(id: string): Promise<void> {
  return request({
    url: `${reviewCycleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ReviewCycle
 * 对应后端：DeleteReviewCycleBatchAsync
 */
export function deleteReviewCycleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${reviewCycleUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新ReviewCycle状态
 * 对应后端：UpdateReviewCycleStatusAsync
 */
export function updateReviewCycleStatus(data: ReviewCycleStatus): Promise<ReviewCycleStatus> {
  return request({
    url: `${reviewCycleUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetReviewCycleTemplateAsync；fileName 仅传名称不含后缀
 */
export function getReviewCycleTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${reviewCycleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ReviewCycle
 * 对应后端：ImportReviewCycleAsync
 */
export function importReviewCycleData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${reviewCycleUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ReviewCycle
 * 对应后端：ExportReviewCycleAsync；fileName 仅传名称不含后缀
 */
export function exportReviewCycleData(query: ReviewCycleQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${reviewCycleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
