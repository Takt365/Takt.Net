// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-issue-meeting.ts
// 功能描述：QualityIssueMeeting API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityIssueMeetings
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityIssueMeeting,
  QualityIssueMeetingQuery,
  QualityIssueMeetingCreate,
  QualityIssueMeetingUpdate
} from '@/types/logistics/quality/cost/quality-issue-meeting'

// ========================================
// QualityIssueMeeting相关 API（按后端控制器顺序）
// ========================================
const qualityIssueMeetingUrl = '/api/TaktQualityIssueMeetings';

/**
 * 获取QualityIssueMeeting列表（分页）
 * 对应后端：GetQualityIssueMeetingListAsync
 */
export function getQualityIssueMeetingList(params: QualityIssueMeetingQuery): Promise<TaktPagedResult<QualityIssueMeeting>> {
  return request({
    url: `${qualityIssueMeetingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityIssueMeeting
 * 对应后端：GetQualityIssueMeetingByIdAsync
 */
export function getQualityIssueMeetingById(id: string): Promise<QualityIssueMeeting> {
  return request({
    url: `${qualityIssueMeetingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityIssueMeeting选项列表（用于下拉框等）
 * 对应后端：GetQualityIssueMeetingOptionsAsync
 */
export function getQualityIssueMeetingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityIssueMeetingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityIssueMeeting
 * 对应后端：CreateQualityIssueMeetingAsync
 */
export function createQualityIssueMeeting(data: QualityIssueMeetingCreate): Promise<QualityIssueMeeting> {
  return request({
    url: qualityIssueMeetingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityIssueMeeting
 * 对应后端：UpdateQualityIssueMeetingAsync
 */
export function updateQualityIssueMeeting(id: string, data: QualityIssueMeetingUpdate): Promise<QualityIssueMeeting> {
  return request({
    url: `${qualityIssueMeetingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityIssueMeeting（单条）
 * 对应后端：DeleteQualityIssueMeetingByIdAsync
 */
export function deleteQualityIssueMeetingById(id: string): Promise<void> {
  return request({
    url: `${qualityIssueMeetingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityIssueMeeting
 * 对应后端：DeleteQualityIssueMeetingBatchAsync
 */
export function deleteQualityIssueMeetingBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityIssueMeetingUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityIssueMeetingTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityIssueMeetingTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityIssueMeetingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityIssueMeeting
 * 对应后端：ImportQualityIssueMeetingAsync
 */
export function importQualityIssueMeetingData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityIssueMeetingUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityIssueMeeting
 * 对应后端：ExportQualityIssueMeetingAsync；fileName 仅传名称不含后缀
 */
export function exportQualityIssueMeetingData(query: QualityIssueMeetingQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityIssueMeetingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
