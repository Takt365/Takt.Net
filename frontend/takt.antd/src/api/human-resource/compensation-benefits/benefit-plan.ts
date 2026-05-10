// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/compensation-benefits
// 文件名称：benefit-plan.ts
// 功能描述：BenefitPlan API，对应后端 Takt.WebApi.Controllers.HumanResource.CompensationBenefits.TaktBenefitPlans
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  BenefitPlan,
  BenefitPlanQuery,
  BenefitPlanCreate,
  BenefitPlanUpdate,
  BenefitPlanStatus
} from '@/types/human-resource/compensation-benefits/benefit-plan'

// ========================================
// BenefitPlan相关 API（按后端控制器顺序）
// ========================================
const benefitPlanUrl = '/api/TaktBenefitPlans';

/**
 * 获取BenefitPlan列表（分页）
 * 对应后端：GetBenefitPlanListAsync
 */
export function getBenefitPlanList(params: BenefitPlanQuery): Promise<TaktPagedResult<BenefitPlan>> {
  return request({
    url: `${benefitPlanUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取BenefitPlan
 * 对应后端：GetBenefitPlanByIdAsync
 */
export function getBenefitPlanById(id: string): Promise<BenefitPlan> {
  return request({
    url: `${benefitPlanUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取BenefitPlan选项列表（用于下拉框等）
 * 对应后端：GetBenefitPlanOptionsAsync
 */
export function getBenefitPlanOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${benefitPlanUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建BenefitPlan
 * 对应后端：CreateBenefitPlanAsync
 */
export function createBenefitPlan(data: BenefitPlanCreate): Promise<BenefitPlan> {
  return request({
    url: benefitPlanUrl,
    method: 'post',
    data
  })
}

/**
 * 更新BenefitPlan
 * 对应后端：UpdateBenefitPlanAsync
 */
export function updateBenefitPlan(id: string, data: BenefitPlanUpdate): Promise<BenefitPlan> {
  return request({
    url: `${benefitPlanUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除BenefitPlan（单条）
 * 对应后端：DeleteBenefitPlanByIdAsync
 */
export function deleteBenefitPlanById(id: string): Promise<void> {
  return request({
    url: `${benefitPlanUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除BenefitPlan
 * 对应后端：DeleteBenefitPlanBatchAsync
 */
export function deleteBenefitPlanBatch(ids: string[]): Promise<void> {
  return request({
    url: `${benefitPlanUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新BenefitPlan状态
 * 对应后端：UpdateBenefitPlanStatusAsync
 */
export function updateBenefitPlanStatus(data: BenefitPlanStatus): Promise<BenefitPlanStatus> {
  return request({
    url: `${benefitPlanUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetBenefitPlanTemplateAsync；fileName 仅传名称不含后缀
 */
export function getBenefitPlanTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${benefitPlanUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入BenefitPlan
 * 对应后端：ImportBenefitPlanAsync
 */
export function importBenefitPlanData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${benefitPlanUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出BenefitPlan
 * 对应后端：ExportBenefitPlanAsync；fileName 仅传名称不含后缀
 */
export function exportBenefitPlanData(query: BenefitPlanQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${benefitPlanUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
