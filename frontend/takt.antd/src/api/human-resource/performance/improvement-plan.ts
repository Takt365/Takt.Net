// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/performance
// 文件名称：improvement-plan.ts
// 功能描述：ImprovementPlan API，对应后端 Takt.WebApi.Controllers.HumanResource.Performance.TaktImprovementPlans
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ImprovementPlan,
  ImprovementPlanQuery,
  ImprovementPlanCreate,
  ImprovementPlanUpdate,
  ImprovementPlanStatus
} from '@/types/human-resource/performance/improvement-plan'

// ========================================
// ImprovementPlan相关 API（按后端控制器顺序）
// ========================================
const improvementPlanUrl = '/api/TaktImprovementPlans';

/**
 * 获取ImprovementPlan列表（分页）
 * 对应后端：GetImprovementPlanListAsync
 */
export function getImprovementPlanList(params: ImprovementPlanQuery): Promise<TaktPagedResult<ImprovementPlan>> {
  return request({
    url: `${improvementPlanUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ImprovementPlan
 * 对应后端：GetImprovementPlanByIdAsync
 */
export function getImprovementPlanById(id: string): Promise<ImprovementPlan> {
  return request({
    url: `${improvementPlanUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ImprovementPlan选项列表（用于下拉框等）
 * 对应后端：GetImprovementPlanOptionsAsync
 */
export function getImprovementPlanOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${improvementPlanUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ImprovementPlan
 * 对应后端：CreateImprovementPlanAsync
 */
export function createImprovementPlan(data: ImprovementPlanCreate): Promise<ImprovementPlan> {
  return request({
    url: improvementPlanUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ImprovementPlan
 * 对应后端：UpdateImprovementPlanAsync
 */
export function updateImprovementPlan(id: string, data: ImprovementPlanUpdate): Promise<ImprovementPlan> {
  return request({
    url: `${improvementPlanUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ImprovementPlan（单条）
 * 对应后端：DeleteImprovementPlanByIdAsync
 */
export function deleteImprovementPlanById(id: string): Promise<void> {
  return request({
    url: `${improvementPlanUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ImprovementPlan
 * 对应后端：DeleteImprovementPlanBatchAsync
 */
export function deleteImprovementPlanBatch(ids: string[]): Promise<void> {
  return request({
    url: `${improvementPlanUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新ImprovementPlan状态
 * 对应后端：UpdateImprovementPlanStatusAsync
 */
export function updateImprovementPlanStatus(data: ImprovementPlanStatus): Promise<ImprovementPlanStatus> {
  return request({
    url: `${improvementPlanUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetImprovementPlanTemplateAsync；fileName 仅传名称不含后缀
 */
export function getImprovementPlanTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${improvementPlanUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ImprovementPlan
 * 对应后端：ImportImprovementPlanAsync
 */
export function importImprovementPlanData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${improvementPlanUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ImprovementPlan
 * 对应后端：ExportImprovementPlanAsync；fileName 仅传名称不含后缀
 */
export function exportImprovementPlanData(query: ImprovementPlanQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${improvementPlanUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
