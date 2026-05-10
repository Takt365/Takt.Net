// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-operation.ts
// 功能描述：QualityOperation API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityOperations
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityOperation,
  QualityOperationQuery,
  QualityOperationCreate,
  QualityOperationUpdate
} from '@/types/logistics/quality/cost/quality-operation'

// ========================================
// QualityOperation相关 API（按后端控制器顺序）
// ========================================
const qualityOperationUrl = '/api/TaktQualityOperations';

/**
 * 获取QualityOperation列表（分页）
 * 对应后端：GetQualityOperationListAsync
 */
export function getQualityOperationList(params: QualityOperationQuery): Promise<TaktPagedResult<QualityOperation>> {
  return request({
    url: `${qualityOperationUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityOperation
 * 对应后端：GetQualityOperationByIdAsync
 */
export function getQualityOperationById(id: string): Promise<QualityOperation> {
  return request({
    url: `${qualityOperationUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityOperation选项列表（用于下拉框等）
 * 对应后端：GetQualityOperationOptionsAsync
 */
export function getQualityOperationOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityOperationUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityOperation
 * 对应后端：CreateQualityOperationAsync
 */
export function createQualityOperation(data: QualityOperationCreate): Promise<QualityOperation> {
  return request({
    url: qualityOperationUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityOperation
 * 对应后端：UpdateQualityOperationAsync
 */
export function updateQualityOperation(id: string, data: QualityOperationUpdate): Promise<QualityOperation> {
  return request({
    url: `${qualityOperationUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityOperation（单条）
 * 对应后端：DeleteQualityOperationByIdAsync
 */
export function deleteQualityOperationById(id: string): Promise<void> {
  return request({
    url: `${qualityOperationUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityOperation
 * 对应后端：DeleteQualityOperationBatchAsync
 */
export function deleteQualityOperationBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityOperationUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityOperationTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityOperationTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityOperationUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityOperation
 * 对应后端：ImportQualityOperationAsync
 */
export function importQualityOperationData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityOperationUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityOperation
 * 对应后端：ExportQualityOperationAsync；fileName 仅传名称不含后缀
 */
export function exportQualityOperationData(query: QualityOperationQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityOperationUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
