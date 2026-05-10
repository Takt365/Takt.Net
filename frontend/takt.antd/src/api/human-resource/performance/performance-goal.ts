// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/performance
// 文件名称：performance-goal.ts
// 功能描述：PerformanceGoal API，对应后端 Takt.WebApi.Controllers.HumanResource.Performance.TaktPerformanceGoals
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PerformanceGoal,
  PerformanceGoalQuery,
  PerformanceGoalCreate,
  PerformanceGoalUpdate,
  PerformanceGoalStatus
} from '@/types/human-resource/performance/performance-goal'

// ========================================
// PerformanceGoal相关 API（按后端控制器顺序）
// ========================================
const performanceGoalUrl = '/api/TaktPerformanceGoals';

/**
 * 获取PerformanceGoal列表（分页）
 * 对应后端：GetPerformanceGoalListAsync
 */
export function getPerformanceGoalList(params: PerformanceGoalQuery): Promise<TaktPagedResult<PerformanceGoal>> {
  return request({
    url: `${performanceGoalUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PerformanceGoal
 * 对应后端：GetPerformanceGoalByIdAsync
 */
export function getPerformanceGoalById(id: string): Promise<PerformanceGoal> {
  return request({
    url: `${performanceGoalUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PerformanceGoal选项列表（用于下拉框等）
 * 对应后端：GetPerformanceGoalOptionsAsync
 */
export function getPerformanceGoalOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${performanceGoalUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PerformanceGoal
 * 对应后端：CreatePerformanceGoalAsync
 */
export function createPerformanceGoal(data: PerformanceGoalCreate): Promise<PerformanceGoal> {
  return request({
    url: performanceGoalUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PerformanceGoal
 * 对应后端：UpdatePerformanceGoalAsync
 */
export function updatePerformanceGoal(id: string, data: PerformanceGoalUpdate): Promise<PerformanceGoal> {
  return request({
    url: `${performanceGoalUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PerformanceGoal（单条）
 * 对应后端：DeletePerformanceGoalByIdAsync
 */
export function deletePerformanceGoalById(id: string): Promise<void> {
  return request({
    url: `${performanceGoalUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PerformanceGoal
 * 对应后端：DeletePerformanceGoalBatchAsync
 */
export function deletePerformanceGoalBatch(ids: string[]): Promise<void> {
  return request({
    url: `${performanceGoalUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新PerformanceGoal状态
 * 对应后端：UpdatePerformanceGoalStatusAsync
 */
export function updatePerformanceGoalStatus(data: PerformanceGoalStatus): Promise<PerformanceGoalStatus> {
  return request({
    url: `${performanceGoalUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPerformanceGoalTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPerformanceGoalTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${performanceGoalUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PerformanceGoal
 * 对应后端：ImportPerformanceGoalAsync
 */
export function importPerformanceGoalData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${performanceGoalUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PerformanceGoal
 * 对应后端：ExportPerformanceGoalAsync；fileName 仅传名称不含后缀
 */
export function exportPerformanceGoalData(query: PerformanceGoalQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${performanceGoalUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
