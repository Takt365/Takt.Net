// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-operation-reliability.ts
// 功能描述：QualityOperationReliability API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityOperationReliabilitys
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityOperationReliability,
  QualityOperationReliabilityQuery,
  QualityOperationReliabilityCreate,
  QualityOperationReliabilityUpdate
} from '@/types/logistics/quality/cost/quality-operation-reliability'

// ========================================
// QualityOperationReliability相关 API（按后端控制器顺序）
// ========================================
const qualityOperationReliabilityUrl = '/api/TaktQualityOperationReliabilitys';

/**
 * 获取QualityOperationReliability列表（分页）
 * 对应后端：GetQualityOperationReliabilityListAsync
 */
export function getQualityOperationReliabilityList(params: QualityOperationReliabilityQuery): Promise<TaktPagedResult<QualityOperationReliability>> {
  return request({
    url: `${qualityOperationReliabilityUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityOperationReliability
 * 对应后端：GetQualityOperationReliabilityByIdAsync
 */
export function getQualityOperationReliabilityById(id: string): Promise<QualityOperationReliability> {
  return request({
    url: `${qualityOperationReliabilityUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityOperationReliability选项列表（用于下拉框等）
 * 对应后端：GetQualityOperationReliabilityOptionsAsync
 */
export function getQualityOperationReliabilityOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityOperationReliabilityUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityOperationReliability
 * 对应后端：CreateQualityOperationReliabilityAsync
 */
export function createQualityOperationReliability(data: QualityOperationReliabilityCreate): Promise<QualityOperationReliability> {
  return request({
    url: qualityOperationReliabilityUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityOperationReliability
 * 对应后端：UpdateQualityOperationReliabilityAsync
 */
export function updateQualityOperationReliability(id: string, data: QualityOperationReliabilityUpdate): Promise<QualityOperationReliability> {
  return request({
    url: `${qualityOperationReliabilityUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityOperationReliability（单条）
 * 对应后端：DeleteQualityOperationReliabilityByIdAsync
 */
export function deleteQualityOperationReliabilityById(id: string): Promise<void> {
  return request({
    url: `${qualityOperationReliabilityUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityOperationReliability
 * 对应后端：DeleteQualityOperationReliabilityBatchAsync
 */
export function deleteQualityOperationReliabilityBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityOperationReliabilityUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityOperationReliabilityTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityOperationReliabilityTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityOperationReliabilityUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityOperationReliability
 * 对应后端：ImportQualityOperationReliabilityAsync
 */
export function importQualityOperationReliabilityData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityOperationReliabilityUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityOperationReliability
 * 对应后端：ExportQualityOperationReliabilityAsync；fileName 仅传名称不含后缀
 */
export function exportQualityOperationReliabilityData(query: QualityOperationReliabilityQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityOperationReliabilityUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
