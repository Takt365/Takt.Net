// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/news
// 文件名称：news.ts
// 功能描述：News API，对应后端 Takt.WebApi.Controllers.Routine.Business.News.TaktNewss
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  News,
  NewsQuery,
  NewsCreate,
  NewsUpdate,
  NewsStatus,
  NewsSort
} from '@/types/routine/business/news/news'

// ========================================
// News相关 API（按后端控制器顺序）
// ========================================
const newsUrl = '/api/TaktNewss';

/**
 * 获取News列表（分页）
 * 对应后端：GetNewsListAsync
 */
export function getNewsList(params: NewsQuery): Promise<TaktPagedResult<News>> {
  return request({
    url: `${newsUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取News
 * 对应后端：GetNewsByIdAsync
 */
export function getNewsById(id: string): Promise<News> {
  return request({
    url: `${newsUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取News选项列表（用于下拉框等）
 * 对应后端：GetNewsOptionsAsync
 */
export function getNewsOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${newsUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建News
 * 对应后端：CreateNewsAsync
 */
export function createNews(data: NewsCreate): Promise<News> {
  return request({
    url: newsUrl,
    method: 'post',
    data
  })
}

/**
 * 更新News
 * 对应后端：UpdateNewsAsync
 */
export function updateNews(id: string, data: NewsUpdate): Promise<News> {
  return request({
    url: `${newsUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除News（单条）
 * 对应后端：DeleteNewsByIdAsync
 */
export function deleteNewsById(id: string): Promise<void> {
  return request({
    url: `${newsUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除News
 * 对应后端：DeleteNewsBatchAsync
 */
export function deleteNewsBatch(ids: string[]): Promise<void> {
  return request({
    url: `${newsUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新News状态
 * 对应后端：UpdateNewsStatusAsync
 */
export function updateNewsStatus(data: NewsStatus): Promise<NewsStatus> {
  return request({
    url: `${newsUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新News排序
 * 对应后端：UpdateNewsSortAsync
 */
export function updateNewsSort(data: NewsSort): Promise<NewsSort> {
  return request({
    url: `${newsUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetNewsTemplateAsync；fileName 仅传名称不含后缀
 */
export function getNewsTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${newsUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入News
 * 对应后端：ImportNewsAsync
 */
export function importNewsData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${newsUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出News
 * 对应后端：ExportNewsAsync；fileName 仅传名称不含后缀
 */
export function exportNewsData(query: NewsQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${newsUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
