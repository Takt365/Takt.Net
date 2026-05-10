// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-operation-outgoing.ts
// 功能描述：QualityOperationOutgoing API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityOperationOutgoings
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityOperationOutgoing,
  QualityOperationOutgoingQuery,
  QualityOperationOutgoingCreate,
  QualityOperationOutgoingUpdate
} from '@/types/logistics/quality/cost/quality-operation-outgoing'

// ========================================
// QualityOperationOutgoing相关 API（按后端控制器顺序）
// ========================================
const qualityOperationOutgoingUrl = '/api/TaktQualityOperationOutgoings';

/**
 * 获取QualityOperationOutgoing列表（分页）
 * 对应后端：GetQualityOperationOutgoingListAsync
 */
export function getQualityOperationOutgoingList(params: QualityOperationOutgoingQuery): Promise<TaktPagedResult<QualityOperationOutgoing>> {
  return request({
    url: `${qualityOperationOutgoingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityOperationOutgoing
 * 对应后端：GetQualityOperationOutgoingByIdAsync
 */
export function getQualityOperationOutgoingById(id: string): Promise<QualityOperationOutgoing> {
  return request({
    url: `${qualityOperationOutgoingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityOperationOutgoing选项列表（用于下拉框等）
 * 对应后端：GetQualityOperationOutgoingOptionsAsync
 */
export function getQualityOperationOutgoingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityOperationOutgoingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityOperationOutgoing
 * 对应后端：CreateQualityOperationOutgoingAsync
 */
export function createQualityOperationOutgoing(data: QualityOperationOutgoingCreate): Promise<QualityOperationOutgoing> {
  return request({
    url: qualityOperationOutgoingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityOperationOutgoing
 * 对应后端：UpdateQualityOperationOutgoingAsync
 */
export function updateQualityOperationOutgoing(id: string, data: QualityOperationOutgoingUpdate): Promise<QualityOperationOutgoing> {
  return request({
    url: `${qualityOperationOutgoingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityOperationOutgoing（单条）
 * 对应后端：DeleteQualityOperationOutgoingByIdAsync
 */
export function deleteQualityOperationOutgoingById(id: string): Promise<void> {
  return request({
    url: `${qualityOperationOutgoingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityOperationOutgoing
 * 对应后端：DeleteQualityOperationOutgoingBatchAsync
 */
export function deleteQualityOperationOutgoingBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityOperationOutgoingUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityOperationOutgoingTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityOperationOutgoingTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityOperationOutgoingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityOperationOutgoing
 * 对应后端：ImportQualityOperationOutgoingAsync
 */
export function importQualityOperationOutgoingData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityOperationOutgoingUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityOperationOutgoing
 * 对应后端：ExportQualityOperationOutgoingAsync；fileName 仅传名称不含后缀
 */
export function exportQualityOperationOutgoingData(query: QualityOperationOutgoingQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityOperationOutgoingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
