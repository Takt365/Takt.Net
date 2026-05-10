// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/compensation-benefits
// 文件名称：tax-rule.ts
// 功能描述：TaxRule API，对应后端 Takt.WebApi.Controllers.HumanResource.CompensationBenefits.TaktTaxRules
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  TaxRule,
  TaxRuleQuery,
  TaxRuleCreate,
  TaxRuleUpdate,
  TaxRuleStatus
} from '@/types/human-resource/compensation-benefits/tax-rule'

// ========================================
// TaxRule相关 API（按后端控制器顺序）
// ========================================
const taxRuleUrl = '/api/TaktTaxRules';

/**
 * 获取TaxRule列表（分页）
 * 对应后端：GetTaxRuleListAsync
 */
export function getTaxRuleList(params: TaxRuleQuery): Promise<TaktPagedResult<TaxRule>> {
  return request({
    url: `${taxRuleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取TaxRule
 * 对应后端：GetTaxRuleByIdAsync
 */
export function getTaxRuleById(id: string): Promise<TaxRule> {
  return request({
    url: `${taxRuleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取TaxRule选项列表（用于下拉框等）
 * 对应后端：GetTaxRuleOptionsAsync
 */
export function getTaxRuleOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${taxRuleUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建TaxRule
 * 对应后端：CreateTaxRuleAsync
 */
export function createTaxRule(data: TaxRuleCreate): Promise<TaxRule> {
  return request({
    url: taxRuleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新TaxRule
 * 对应后端：UpdateTaxRuleAsync
 */
export function updateTaxRule(id: string, data: TaxRuleUpdate): Promise<TaxRule> {
  return request({
    url: `${taxRuleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除TaxRule（单条）
 * 对应后端：DeleteTaxRuleByIdAsync
 */
export function deleteTaxRuleById(id: string): Promise<void> {
  return request({
    url: `${taxRuleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除TaxRule
 * 对应后端：DeleteTaxRuleBatchAsync
 */
export function deleteTaxRuleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${taxRuleUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新TaxRule状态
 * 对应后端：UpdateTaxRuleStatusAsync
 */
export function updateTaxRuleStatus(data: TaxRuleStatus): Promise<TaxRuleStatus> {
  return request({
    url: `${taxRuleUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTaxRuleTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTaxRuleTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${taxRuleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入TaxRule
 * 对应后端：ImportTaxRuleAsync
 */
export function importTaxRuleData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${taxRuleUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出TaxRule
 * 对应后端：ExportTaxRuleAsync；fileName 仅传名称不含后缀
 */
export function exportTaxRuleData(query: TaxRuleQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${taxRuleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
