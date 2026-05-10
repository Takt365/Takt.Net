// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/performance
// 文件名称：performance-indicator.ts
// 功能描述：PerformanceIndicator API，对应后端 Takt.WebApi.Controllers.HumanResource.Performance.TaktPerformanceIndicators
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PerformanceIndicator,
  PerformanceIndicatorQuery,
  PerformanceIndicatorCreate,
  PerformanceIndicatorUpdate,
  PerformanceIndicatorStatus,
  PerformanceIndicatorSort
} from '@/types/human-resource/performance/performance-indicator'

// ========================================
// PerformanceIndicator相关 API（按后端控制器顺序）
// ========================================
const performanceIndicatorUrl = '/api/TaktPerformanceIndicators';

/**
 * 获取PerformanceIndicator列表（分页）
 * 对应后端：GetPerformanceIndicatorListAsync
 */
export function getPerformanceIndicatorList(params: PerformanceIndicatorQuery): Promise<TaktPagedResult<PerformanceIndicator>> {
  return request({
    url: `${performanceIndicatorUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PerformanceIndicator
 * 对应后端：GetPerformanceIndicatorByIdAsync
 */
export function getPerformanceIndicatorById(id: string): Promise<PerformanceIndicator> {
  return request({
    url: `${performanceIndicatorUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PerformanceIndicator选项列表（用于下拉框等）
 * 对应后端：GetPerformanceIndicatorOptionsAsync
 */
export function getPerformanceIndicatorOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${performanceIndicatorUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PerformanceIndicator
 * 对应后端：CreatePerformanceIndicatorAsync
 */
export function createPerformanceIndicator(data: PerformanceIndicatorCreate): Promise<PerformanceIndicator> {
  return request({
    url: performanceIndicatorUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PerformanceIndicator
 * 对应后端：UpdatePerformanceIndicatorAsync
 */
export function updatePerformanceIndicator(id: string, data: PerformanceIndicatorUpdate): Promise<PerformanceIndicator> {
  return request({
    url: `${performanceIndicatorUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PerformanceIndicator（单条）
 * 对应后端：DeletePerformanceIndicatorByIdAsync
 */
export function deletePerformanceIndicatorById(id: string): Promise<void> {
  return request({
    url: `${performanceIndicatorUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PerformanceIndicator
 * 对应后端：DeletePerformanceIndicatorBatchAsync
 */
export function deletePerformanceIndicatorBatch(ids: string[]): Promise<void> {
  return request({
    url: `${performanceIndicatorUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新PerformanceIndicator状态
 * 对应后端：UpdatePerformanceIndicatorStatusAsync
 */
export function updatePerformanceIndicatorStatus(data: PerformanceIndicatorStatus): Promise<PerformanceIndicatorStatus> {
  return request({
    url: `${performanceIndicatorUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新PerformanceIndicator排序
 * 对应后端：UpdatePerformanceIndicatorSortAsync
 */
export function updatePerformanceIndicatorSort(data: PerformanceIndicatorSort): Promise<PerformanceIndicatorSort> {
  return request({
    url: `${performanceIndicatorUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPerformanceIndicatorTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPerformanceIndicatorTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${performanceIndicatorUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PerformanceIndicator
 * 对应后端：ImportPerformanceIndicatorAsync
 */
export function importPerformanceIndicatorData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${performanceIndicatorUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PerformanceIndicator
 * 对应后端：ExportPerformanceIndicatorAsync；fileName 仅传名称不含后缀
 */
export function exportPerformanceIndicatorData(query: PerformanceIndicatorQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${performanceIndicatorUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
