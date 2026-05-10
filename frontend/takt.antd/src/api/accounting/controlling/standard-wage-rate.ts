// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling
// 文件名称：standard-wage-rate.ts
// 功能描述：StandardWageRate API，对应后端 Takt.WebApi.Controllers.Accounting.Controlling.TaktStandardWageRates
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  StandardWageRate,
  StandardWageRateQuery,
  StandardWageRateCreate,
  StandardWageRateUpdate
} from '@/types/accounting/controlling/standard-wage-rate'

// ========================================
// StandardWageRate相关 API（按后端控制器顺序）
// ========================================
const standardWageRateUrl = '/api/TaktStandardWageRates';

/**
 * 获取StandardWageRate列表（分页）
 * 对应后端：GetStandardWageRateListAsync
 */
export function getStandardWageRateList(params: StandardWageRateQuery): Promise<TaktPagedResult<StandardWageRate>> {
  return request({
    url: `${standardWageRateUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取StandardWageRate
 * 对应后端：GetStandardWageRateByIdAsync
 */
export function getStandardWageRateById(id: string): Promise<StandardWageRate> {
  return request({
    url: `${standardWageRateUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取StandardWageRate选项列表（用于下拉框等）
 * 对应后端：GetStandardWageRateOptionsAsync
 */
export function getStandardWageRateOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${standardWageRateUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建StandardWageRate
 * 对应后端：CreateStandardWageRateAsync
 */
export function createStandardWageRate(data: StandardWageRateCreate): Promise<StandardWageRate> {
  return request({
    url: standardWageRateUrl,
    method: 'post',
    data
  })
}

/**
 * 更新StandardWageRate
 * 对应后端：UpdateStandardWageRateAsync
 */
export function updateStandardWageRate(id: string, data: StandardWageRateUpdate): Promise<StandardWageRate> {
  return request({
    url: `${standardWageRateUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除StandardWageRate（单条）
 * 对应后端：DeleteStandardWageRateByIdAsync
 */
export function deleteStandardWageRateById(id: string): Promise<void> {
  return request({
    url: `${standardWageRateUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除StandardWageRate
 * 对应后端：DeleteStandardWageRateBatchAsync
 */
export function deleteStandardWageRateBatch(ids: string[]): Promise<void> {
  return request({
    url: `${standardWageRateUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetStandardWageRateTemplateAsync；fileName 仅传名称不含后缀
 */
export function getStandardWageRateTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${standardWageRateUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入StandardWageRate
 * 对应后端：ImportStandardWageRateAsync
 */
export function importStandardWageRateData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${standardWageRateUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出StandardWageRate
 * 对应后端：ExportStandardWageRateAsync；fileName 仅传名称不含后缀
 */
export function exportStandardWageRateData(query: StandardWageRateQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${standardWageRateUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
