// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-operation-incoming.ts
// 功能描述：QualityOperationIncoming API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityOperationIncomings
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityOperationIncoming,
  QualityOperationIncomingQuery,
  QualityOperationIncomingCreate,
  QualityOperationIncomingUpdate
} from '@/types/logistics/quality/cost/quality-operation-incoming'

// ========================================
// QualityOperationIncoming相关 API（按后端控制器顺序）
// ========================================
const qualityOperationIncomingUrl = '/api/TaktQualityOperationIncomings';

/**
 * 获取QualityOperationIncoming列表（分页）
 * 对应后端：GetQualityOperationIncomingListAsync
 */
export function getQualityOperationIncomingList(params: QualityOperationIncomingQuery): Promise<TaktPagedResult<QualityOperationIncoming>> {
  return request({
    url: `${qualityOperationIncomingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityOperationIncoming
 * 对应后端：GetQualityOperationIncomingByIdAsync
 */
export function getQualityOperationIncomingById(id: string): Promise<QualityOperationIncoming> {
  return request({
    url: `${qualityOperationIncomingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityOperationIncoming选项列表（用于下拉框等）
 * 对应后端：GetQualityOperationIncomingOptionsAsync
 */
export function getQualityOperationIncomingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityOperationIncomingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityOperationIncoming
 * 对应后端：CreateQualityOperationIncomingAsync
 */
export function createQualityOperationIncoming(data: QualityOperationIncomingCreate): Promise<QualityOperationIncoming> {
  return request({
    url: qualityOperationIncomingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityOperationIncoming
 * 对应后端：UpdateQualityOperationIncomingAsync
 */
export function updateQualityOperationIncoming(id: string, data: QualityOperationIncomingUpdate): Promise<QualityOperationIncoming> {
  return request({
    url: `${qualityOperationIncomingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityOperationIncoming（单条）
 * 对应后端：DeleteQualityOperationIncomingByIdAsync
 */
export function deleteQualityOperationIncomingById(id: string): Promise<void> {
  return request({
    url: `${qualityOperationIncomingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityOperationIncoming
 * 对应后端：DeleteQualityOperationIncomingBatchAsync
 */
export function deleteQualityOperationIncomingBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityOperationIncomingUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityOperationIncomingTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityOperationIncomingTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityOperationIncomingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityOperationIncoming
 * 对应后端：ImportQualityOperationIncomingAsync
 */
export function importQualityOperationIncomingData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityOperationIncomingUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityOperationIncoming
 * 对应后端：ExportQualityOperationIncomingAsync；fileName 仅传名称不含后缀
 */
export function exportQualityOperationIncomingData(query: QualityOperationIncomingQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityOperationIncomingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
