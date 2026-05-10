// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/compensation-benefits
// 文件名称：compensation-benefit.ts
// 功能描述：CompensationBenefit API，对应后端 Takt.WebApi.Controllers.HumanResource.CompensationBenefits.TaktCompensationBenefits
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CompensationBenefit,
  CompensationBenefitQuery,
  CompensationBenefitCreate,
  CompensationBenefitUpdate,
  CompensationBenefitStatus
} from '@/types/human-resource/compensation-benefits/compensation-benefit'

// ========================================
// CompensationBenefit相关 API（按后端控制器顺序）
// ========================================
const compensationBenefitUrl = '/api/TaktCompensationBenefits';

/**
 * 获取CompensationBenefit列表（分页）
 * 对应后端：GetCompensationBenefitListAsync
 */
export function getCompensationBenefitList(params: CompensationBenefitQuery): Promise<TaktPagedResult<CompensationBenefit>> {
  return request({
    url: `${compensationBenefitUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CompensationBenefit
 * 对应后端：GetCompensationBenefitByIdAsync
 */
export function getCompensationBenefitById(id: string): Promise<CompensationBenefit> {
  return request({
    url: `${compensationBenefitUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CompensationBenefit选项列表（用于下拉框等）
 * 对应后端：GetCompensationBenefitOptionsAsync
 */
export function getCompensationBenefitOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${compensationBenefitUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建CompensationBenefit
 * 对应后端：CreateCompensationBenefitAsync
 */
export function createCompensationBenefit(data: CompensationBenefitCreate): Promise<CompensationBenefit> {
  return request({
    url: compensationBenefitUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CompensationBenefit
 * 对应后端：UpdateCompensationBenefitAsync
 */
export function updateCompensationBenefit(id: string, data: CompensationBenefitUpdate): Promise<CompensationBenefit> {
  return request({
    url: `${compensationBenefitUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CompensationBenefit（单条）
 * 对应后端：DeleteCompensationBenefitByIdAsync
 */
export function deleteCompensationBenefitById(id: string): Promise<void> {
  return request({
    url: `${compensationBenefitUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CompensationBenefit
 * 对应后端：DeleteCompensationBenefitBatchAsync
 */
export function deleteCompensationBenefitBatch(ids: string[]): Promise<void> {
  return request({
    url: `${compensationBenefitUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新CompensationBenefit状态
 * 对应后端：UpdateCompensationBenefitStatusAsync
 */
export function updateCompensationBenefitStatus(data: CompensationBenefitStatus): Promise<CompensationBenefitStatus> {
  return request({
    url: `${compensationBenefitUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCompensationBenefitTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCompensationBenefitTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${compensationBenefitUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CompensationBenefit
 * 对应后端：ImportCompensationBenefitAsync
 */
export function importCompensationBenefitData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${compensationBenefitUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CompensationBenefit
 * 对应后端：ExportCompensationBenefitAsync；fileName 仅传名称不含后缀
 */
export function exportCompensationBenefitData(query: CompensationBenefitQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${compensationBenefitUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
