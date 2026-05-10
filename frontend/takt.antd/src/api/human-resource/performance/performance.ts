// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/performance
// 文件名称：performance.ts
// 功能描述：Performance API，对应后端 Takt.WebApi.Controllers.HumanResource.Performance.TaktPerformances
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Performance,
  PerformanceQuery,
  PerformanceCreate,
  PerformanceUpdate,
  PerformanceStatus
} from '@/types/human-resource/performance/performance'

// ========================================
// Performance相关 API（按后端控制器顺序）
// ========================================
const performanceUrl = '/api/TaktPerformances';

/**
 * 获取Performance列表（分页）
 * 对应后端：GetPerformanceListAsync
 */
export function getPerformanceList(params: PerformanceQuery): Promise<TaktPagedResult<Performance>> {
  return request({
    url: `${performanceUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Performance
 * 对应后端：GetPerformanceByIdAsync
 */
export function getPerformanceById(id: string): Promise<Performance> {
  return request({
    url: `${performanceUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Performance选项列表（用于下拉框等）
 * 对应后端：GetPerformanceOptionsAsync
 */
export function getPerformanceOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${performanceUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Performance
 * 对应后端：CreatePerformanceAsync
 */
export function createPerformance(data: PerformanceCreate): Promise<Performance> {
  return request({
    url: performanceUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Performance
 * 对应后端：UpdatePerformanceAsync
 */
export function updatePerformance(id: string, data: PerformanceUpdate): Promise<Performance> {
  return request({
    url: `${performanceUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Performance（单条）
 * 对应后端：DeletePerformanceByIdAsync
 */
export function deletePerformanceById(id: string): Promise<void> {
  return request({
    url: `${performanceUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Performance
 * 对应后端：DeletePerformanceBatchAsync
 */
export function deletePerformanceBatch(ids: string[]): Promise<void> {
  return request({
    url: `${performanceUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Performance状态
 * 对应后端：UpdatePerformanceStatusAsync
 */
export function updatePerformanceStatus(data: PerformanceStatus): Promise<PerformanceStatus> {
  return request({
    url: `${performanceUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPerformanceTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPerformanceTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${performanceUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Performance
 * 对应后端：ImportPerformanceAsync
 */
export function importPerformanceData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${performanceUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Performance
 * 对应后端：ExportPerformanceAsync；fileName 仅传名称不含后缀
 */
export function exportPerformanceData(query: PerformanceQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${performanceUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
