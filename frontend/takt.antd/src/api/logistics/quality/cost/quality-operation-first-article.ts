// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-operation-first-article.ts
// 功能描述：QualityOperationFirstArticle API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityOperationFirstArticles
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityOperationFirstArticle,
  QualityOperationFirstArticleQuery,
  QualityOperationFirstArticleCreate,
  QualityOperationFirstArticleUpdate
} from '@/types/logistics/quality/cost/quality-operation-first-article'

// ========================================
// QualityOperationFirstArticle相关 API（按后端控制器顺序）
// ========================================
const qualityOperationFirstArticleUrl = '/api/TaktQualityOperationFirstArticles';

/**
 * 获取QualityOperationFirstArticle列表（分页）
 * 对应后端：GetQualityOperationFirstArticleListAsync
 */
export function getQualityOperationFirstArticleList(params: QualityOperationFirstArticleQuery): Promise<TaktPagedResult<QualityOperationFirstArticle>> {
  return request({
    url: `${qualityOperationFirstArticleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityOperationFirstArticle
 * 对应后端：GetQualityOperationFirstArticleByIdAsync
 */
export function getQualityOperationFirstArticleById(id: string): Promise<QualityOperationFirstArticle> {
  return request({
    url: `${qualityOperationFirstArticleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityOperationFirstArticle选项列表（用于下拉框等）
 * 对应后端：GetQualityOperationFirstArticleOptionsAsync
 */
export function getQualityOperationFirstArticleOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityOperationFirstArticleUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityOperationFirstArticle
 * 对应后端：CreateQualityOperationFirstArticleAsync
 */
export function createQualityOperationFirstArticle(data: QualityOperationFirstArticleCreate): Promise<QualityOperationFirstArticle> {
  return request({
    url: qualityOperationFirstArticleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityOperationFirstArticle
 * 对应后端：UpdateQualityOperationFirstArticleAsync
 */
export function updateQualityOperationFirstArticle(id: string, data: QualityOperationFirstArticleUpdate): Promise<QualityOperationFirstArticle> {
  return request({
    url: `${qualityOperationFirstArticleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityOperationFirstArticle（单条）
 * 对应后端：DeleteQualityOperationFirstArticleByIdAsync
 */
export function deleteQualityOperationFirstArticleById(id: string): Promise<void> {
  return request({
    url: `${qualityOperationFirstArticleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityOperationFirstArticle
 * 对应后端：DeleteQualityOperationFirstArticleBatchAsync
 */
export function deleteQualityOperationFirstArticleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityOperationFirstArticleUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityOperationFirstArticleTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityOperationFirstArticleTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityOperationFirstArticleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityOperationFirstArticle
 * 对应后端：ImportQualityOperationFirstArticleAsync
 */
export function importQualityOperationFirstArticleData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityOperationFirstArticleUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityOperationFirstArticle
 * 对应后端：ExportQualityOperationFirstArticleAsync；fileName 仅传名称不含后缀
 */
export function exportQualityOperationFirstArticleData(query: QualityOperationFirstArticleQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityOperationFirstArticleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
