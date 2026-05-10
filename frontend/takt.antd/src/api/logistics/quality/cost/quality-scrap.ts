// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-scrap.ts
// 功能描述：QualityScrap API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityScraps
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityScrap,
  QualityScrapQuery,
  QualityScrapCreate,
  QualityScrapUpdate
} from '@/types/logistics/quality/cost/quality-scrap'

// ========================================
// QualityScrap相关 API（按后端控制器顺序）
// ========================================
const qualityScrapUrl = '/api/TaktQualityScraps';

/**
 * 获取QualityScrap列表（分页）
 * 对应后端：GetQualityScrapListAsync
 */
export function getQualityScrapList(params: QualityScrapQuery): Promise<TaktPagedResult<QualityScrap>> {
  return request({
    url: `${qualityScrapUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityScrap
 * 对应后端：GetQualityScrapByIdAsync
 */
export function getQualityScrapById(id: string): Promise<QualityScrap> {
  return request({
    url: `${qualityScrapUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityScrap选项列表（用于下拉框等）
 * 对应后端：GetQualityScrapOptionsAsync
 */
export function getQualityScrapOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityScrapUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityScrap
 * 对应后端：CreateQualityScrapAsync
 */
export function createQualityScrap(data: QualityScrapCreate): Promise<QualityScrap> {
  return request({
    url: qualityScrapUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityScrap
 * 对应后端：UpdateQualityScrapAsync
 */
export function updateQualityScrap(id: string, data: QualityScrapUpdate): Promise<QualityScrap> {
  return request({
    url: `${qualityScrapUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityScrap（单条）
 * 对应后端：DeleteQualityScrapByIdAsync
 */
export function deleteQualityScrapById(id: string): Promise<void> {
  return request({
    url: `${qualityScrapUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityScrap
 * 对应后端：DeleteQualityScrapBatchAsync
 */
export function deleteQualityScrapBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityScrapUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityScrapTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityScrapTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityScrapUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityScrap
 * 对应后端：ImportQualityScrapAsync
 */
export function importQualityScrapData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityScrapUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityScrap
 * 对应后端：ExportQualityScrapAsync；fileName 仅传名称不含后缀
 */
export function exportQualityScrapData(query: QualityScrapQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityScrapUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
