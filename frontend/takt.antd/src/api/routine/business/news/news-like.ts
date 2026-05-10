// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/news
// 文件名称：news-like.ts
// 功能描述：NewsLike API，对应后端 Takt.WebApi.Controllers.Routine.Business.News.TaktNewsLikes
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  NewsLike,
  NewsLikeQuery,
  NewsLikeCreate,
  NewsLikeUpdate
} from '@/types/routine/business/news/news-like'

// ========================================
// NewsLike相关 API（按后端控制器顺序）
// ========================================
const newsLikeUrl = '/api/TaktNewsLikes';

/**
 * 获取NewsLike列表（分页）
 * 对应后端：GetNewsLikeListAsync
 */
export function getNewsLikeList(params: NewsLikeQuery): Promise<TaktPagedResult<NewsLike>> {
  return request({
    url: `${newsLikeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取NewsLike
 * 对应后端：GetNewsLikeByIdAsync
 */
export function getNewsLikeById(id: string): Promise<NewsLike> {
  return request({
    url: `${newsLikeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取NewsLike选项列表（用于下拉框等）
 * 对应后端：GetNewsLikeOptionsAsync
 */
export function getNewsLikeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${newsLikeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建NewsLike
 * 对应后端：CreateNewsLikeAsync
 */
export function createNewsLike(data: NewsLikeCreate): Promise<NewsLike> {
  return request({
    url: newsLikeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新NewsLike
 * 对应后端：UpdateNewsLikeAsync
 */
export function updateNewsLike(id: string, data: NewsLikeUpdate): Promise<NewsLike> {
  return request({
    url: `${newsLikeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除NewsLike（单条）
 * 对应后端：DeleteNewsLikeByIdAsync
 */
export function deleteNewsLikeById(id: string): Promise<void> {
  return request({
    url: `${newsLikeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除NewsLike
 * 对应后端：DeleteNewsLikeBatchAsync
 */
export function deleteNewsLikeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${newsLikeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetNewsLikeTemplateAsync；fileName 仅传名称不含后缀
 */
export function getNewsLikeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${newsLikeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入NewsLike
 * 对应后端：ImportNewsLikeAsync
 */
export function importNewsLikeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${newsLikeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出NewsLike
 * 对应后端：ExportNewsLikeAsync；fileName 仅传名称不含后缀
 */
export function exportNewsLikeData(query: NewsLikeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${newsLikeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
