// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/compensation-benefits
// 文件名称：compensation-plan.ts
// 功能描述：CompensationPlan API，对应后端 Takt.WebApi.Controllers.HumanResource.CompensationBenefits.TaktCompensationPlans
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CompensationPlan,
  CompensationPlanQuery,
  CompensationPlanCreate,
  CompensationPlanUpdate,
  CompensationPlanStatus
} from '@/types/human-resource/compensation-benefits/compensation-plan'

// ========================================
// CompensationPlan相关 API（按后端控制器顺序）
// ========================================
const compensationPlanUrl = '/api/TaktCompensationPlans';

/**
 * 获取CompensationPlan列表（分页）
 * 对应后端：GetCompensationPlanListAsync
 */
export function getCompensationPlanList(params: CompensationPlanQuery): Promise<TaktPagedResult<CompensationPlan>> {
  return request({
    url: `${compensationPlanUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CompensationPlan
 * 对应后端：GetCompensationPlanByIdAsync
 */
export function getCompensationPlanById(id: string): Promise<CompensationPlan> {
  return request({
    url: `${compensationPlanUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CompensationPlan选项列表（用于下拉框等）
 * 对应后端：GetCompensationPlanOptionsAsync
 */
export function getCompensationPlanOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${compensationPlanUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建CompensationPlan
 * 对应后端：CreateCompensationPlanAsync
 */
export function createCompensationPlan(data: CompensationPlanCreate): Promise<CompensationPlan> {
  return request({
    url: compensationPlanUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CompensationPlan
 * 对应后端：UpdateCompensationPlanAsync
 */
export function updateCompensationPlan(id: string, data: CompensationPlanUpdate): Promise<CompensationPlan> {
  return request({
    url: `${compensationPlanUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CompensationPlan（单条）
 * 对应后端：DeleteCompensationPlanByIdAsync
 */
export function deleteCompensationPlanById(id: string): Promise<void> {
  return request({
    url: `${compensationPlanUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CompensationPlan
 * 对应后端：DeleteCompensationPlanBatchAsync
 */
export function deleteCompensationPlanBatch(ids: string[]): Promise<void> {
  return request({
    url: `${compensationPlanUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新CompensationPlan状态
 * 对应后端：UpdateCompensationPlanStatusAsync
 */
export function updateCompensationPlanStatus(data: CompensationPlanStatus): Promise<CompensationPlanStatus> {
  return request({
    url: `${compensationPlanUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCompensationPlanTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCompensationPlanTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${compensationPlanUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CompensationPlan
 * 对应后端：ImportCompensationPlanAsync
 */
export function importCompensationPlanData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${compensationPlanUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CompensationPlan
 * 对应后端：ExportCompensationPlanAsync；fileName 仅传名称不含后缀
 */
export function exportCompensationPlanData(query: CompensationPlanQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${compensationPlanUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
