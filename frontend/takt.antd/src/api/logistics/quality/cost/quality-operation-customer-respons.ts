// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-operation-customer-respons.ts
// 功能描述：QualityOperationCustomerRespons API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityOperationCustomerResponses
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityOperationCustomerRespons,
  QualityOperationCustomerResponsQuery,
  QualityOperationCustomerResponsCreate,
  QualityOperationCustomerResponsUpdate
} from '@/types/logistics/quality/cost/quality-operation-customer-respons'

// ========================================
// QualityOperationCustomerRespons相关 API（按后端控制器顺序）
// ========================================
const qualityOperationCustomerResponsUrl = '/api/TaktQualityOperationCustomerResponses';

/**
 * 获取QualityOperationCustomerRespons列表（分页）
 * 对应后端：GetQualityOperationCustomerResponseListAsync
 */
export function getQualityOperationCustomerResponsList(params: QualityOperationCustomerResponsQuery): Promise<TaktPagedResult<QualityOperationCustomerResponse>> {
  return request({
    url: `${qualityOperationCustomerResponsUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityOperationCustomerRespons
 * 对应后端：GetQualityOperationCustomerResponseByIdAsync
 */
export function getQualityOperationCustomerResponsById(id: string): Promise<QualityOperationCustomerRespons> {
  return request({
    url: `${qualityOperationCustomerResponsUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityOperationCustomerRespons选项列表（用于下拉框等）
 * 对应后端：GetQualityOperationCustomerResponseOptionsAsync
 */
export function getQualityOperationCustomerResponsOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityOperationCustomerResponsUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityOperationCustomerRespons
 * 对应后端：CreateQualityOperationCustomerResponseAsync
 */
export function createQualityOperationCustomerRespons(data: QualityOperationCustomerResponsCreate): Promise<QualityOperationCustomerRespons> {
  return request({
    url: qualityOperationCustomerResponsUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityOperationCustomerRespons
 * 对应后端：UpdateQualityOperationCustomerResponseAsync
 */
export function updateQualityOperationCustomerRespons(id: string, data: QualityOperationCustomerResponsUpdate): Promise<QualityOperationCustomerRespons> {
  return request({
    url: `${qualityOperationCustomerResponsUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityOperationCustomerRespons（单条）
 * 对应后端：DeleteQualityOperationCustomerResponseByIdAsync
 */
export function deleteQualityOperationCustomerResponsById(id: string): Promise<void> {
  return request({
    url: `${qualityOperationCustomerResponsUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityOperationCustomerRespons
 * 对应后端：DeleteQualityOperationCustomerResponseBatchAsync
 */
export function deleteQualityOperationCustomerResponsBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityOperationCustomerResponsUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityOperationCustomerResponseTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityOperationCustomerResponsTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityOperationCustomerResponsUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityOperationCustomerRespons
 * 对应后端：ImportQualityOperationCustomerResponseAsync
 */
export function importQualityOperationCustomerResponsData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityOperationCustomerResponsUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityOperationCustomerRespons
 * 对应后端：ExportQualityOperationCustomerResponseAsync；fileName 仅传名称不含后缀
 */
export function exportQualityOperationCustomerResponsData(query: QualityOperationCustomerResponsQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityOperationCustomerResponsUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
