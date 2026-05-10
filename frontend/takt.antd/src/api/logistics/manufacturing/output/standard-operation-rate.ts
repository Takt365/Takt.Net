// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/output
// 文件名称：standard-operation-rate.ts
// 功能描述：StandardOperationRate API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Output.TaktStandardOperationRates
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  StandardOperationRate,
  StandardOperationRateQuery,
  StandardOperationRateCreate,
  StandardOperationRateUpdate,
  StandardOperationRateStatus
} from '@/types/logistics/manufacturing/output/standard-operation-rate'

// ========================================
// StandardOperationRate相关 API（按后端控制器顺序）
// ========================================
const standardOperationRateUrl = '/api/TaktStandardOperationRates';

/**
 * 获取StandardOperationRate列表（分页）
 * 对应后端：GetStandardOperationRateListAsync
 */
export function getStandardOperationRateList(params: StandardOperationRateQuery): Promise<TaktPagedResult<StandardOperationRate>> {
  return request({
    url: `${standardOperationRateUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取StandardOperationRate
 * 对应后端：GetStandardOperationRateByIdAsync
 */
export function getStandardOperationRateById(id: string): Promise<StandardOperationRate> {
  return request({
    url: `${standardOperationRateUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取StandardOperationRate选项列表（用于下拉框等）
 * 对应后端：GetStandardOperationRateOptionsAsync
 */
export function getStandardOperationRateOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${standardOperationRateUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建StandardOperationRate
 * 对应后端：CreateStandardOperationRateAsync
 */
export function createStandardOperationRate(data: StandardOperationRateCreate): Promise<StandardOperationRate> {
  return request({
    url: standardOperationRateUrl,
    method: 'post',
    data
  })
}

/**
 * 更新StandardOperationRate
 * 对应后端：UpdateStandardOperationRateAsync
 */
export function updateStandardOperationRate(id: string, data: StandardOperationRateUpdate): Promise<StandardOperationRate> {
  return request({
    url: `${standardOperationRateUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除StandardOperationRate（单条）
 * 对应后端：DeleteStandardOperationRateByIdAsync
 */
export function deleteStandardOperationRateById(id: string): Promise<void> {
  return request({
    url: `${standardOperationRateUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除StandardOperationRate
 * 对应后端：DeleteStandardOperationRateBatchAsync
 */
export function deleteStandardOperationRateBatch(ids: string[]): Promise<void> {
  return request({
    url: `${standardOperationRateUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新StandardOperationRate状态
 * 对应后端：UpdateStandardOperationRateStatusAsync
 */
export function updateStandardOperationRateStatus(data: StandardOperationRateStatus): Promise<StandardOperationRateStatus> {
  return request({
    url: `${standardOperationRateUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetStandardOperationRateTemplateAsync；fileName 仅传名称不含后缀
 */
export function getStandardOperationRateTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${standardOperationRateUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入StandardOperationRate
 * 对应后端：ImportStandardOperationRateAsync
 */
export function importStandardOperationRateData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${standardOperationRateUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出StandardOperationRate
 * 对应后端：ExportStandardOperationRateAsync；fileName 仅传名称不含后缀
 */
export function exportStandardOperationRateData(query: StandardOperationRateQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${standardOperationRateUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
