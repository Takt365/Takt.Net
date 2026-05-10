// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/performance
// 文件名称：performance-plan.ts
// 功能描述：PerformancePlan API，对应后端 Takt.WebApi.Controllers.HumanResource.Performance.TaktPerformancePlans
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PerformancePlan,
  PerformancePlanQuery,
  PerformancePlanCreate,
  PerformancePlanUpdate,
  PerformancePlanStatus
} from '@/types/human-resource/performance/performance-plan'

// ========================================
// PerformancePlan相关 API（按后端控制器顺序）
// ========================================
const performancePlanUrl = '/api/TaktPerformancePlans';

/**
 * 获取PerformancePlan列表（分页）
 * 对应后端：GetPerformancePlanListAsync
 */
export function getPerformancePlanList(params: PerformancePlanQuery): Promise<TaktPagedResult<PerformancePlan>> {
  return request({
    url: `${performancePlanUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PerformancePlan
 * 对应后端：GetPerformancePlanByIdAsync
 */
export function getPerformancePlanById(id: string): Promise<PerformancePlan> {
  return request({
    url: `${performancePlanUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PerformancePlan选项列表（用于下拉框等）
 * 对应后端：GetPerformancePlanOptionsAsync
 */
export function getPerformancePlanOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${performancePlanUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PerformancePlan
 * 对应后端：CreatePerformancePlanAsync
 */
export function createPerformancePlan(data: PerformancePlanCreate): Promise<PerformancePlan> {
  return request({
    url: performancePlanUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PerformancePlan
 * 对应后端：UpdatePerformancePlanAsync
 */
export function updatePerformancePlan(id: string, data: PerformancePlanUpdate): Promise<PerformancePlan> {
  return request({
    url: `${performancePlanUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PerformancePlan（单条）
 * 对应后端：DeletePerformancePlanByIdAsync
 */
export function deletePerformancePlanById(id: string): Promise<void> {
  return request({
    url: `${performancePlanUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PerformancePlan
 * 对应后端：DeletePerformancePlanBatchAsync
 */
export function deletePerformancePlanBatch(ids: string[]): Promise<void> {
  return request({
    url: `${performancePlanUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新PerformancePlan状态
 * 对应后端：UpdatePerformancePlanStatusAsync
 */
export function updatePerformancePlanStatus(data: PerformancePlanStatus): Promise<PerformancePlanStatus> {
  return request({
    url: `${performancePlanUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPerformancePlanTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPerformancePlanTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${performancePlanUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PerformancePlan
 * 对应后端：ImportPerformancePlanAsync
 */
export function importPerformancePlanData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${performancePlanUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PerformancePlan
 * 对应后端：ExportPerformancePlanAsync；fileName 仅传名称不含后缀
 */
export function exportPerformancePlanData(query: PerformancePlanQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${performancePlanUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
