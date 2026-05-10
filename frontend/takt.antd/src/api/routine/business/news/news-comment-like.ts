// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/news
// 文件名称：news-comment-like.ts
// 功能描述：NewsCommentLike API，对应后端 Takt.WebApi.Controllers.Routine.Business.News.TaktNewsCommentLikes
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  NewsCommentLike,
  NewsCommentLikeQuery,
  NewsCommentLikeCreate,
  NewsCommentLikeUpdate
} from '@/types/routine/business/news/news-comment-like'

// ========================================
// NewsCommentLike相关 API（按后端控制器顺序）
// ========================================
const newsCommentLikeUrl = '/api/TaktNewsCommentLikes';

/**
 * 获取NewsCommentLike列表（分页）
 * 对应后端：GetNewsCommentLikeListAsync
 */
export function getNewsCommentLikeList(params: NewsCommentLikeQuery): Promise<TaktPagedResult<NewsCommentLike>> {
  return request({
    url: `${newsCommentLikeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取NewsCommentLike
 * 对应后端：GetNewsCommentLikeByIdAsync
 */
export function getNewsCommentLikeById(id: string): Promise<NewsCommentLike> {
  return request({
    url: `${newsCommentLikeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取NewsCommentLike选项列表（用于下拉框等）
 * 对应后端：GetNewsCommentLikeOptionsAsync
 */
export function getNewsCommentLikeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${newsCommentLikeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建NewsCommentLike
 * 对应后端：CreateNewsCommentLikeAsync
 */
export function createNewsCommentLike(data: NewsCommentLikeCreate): Promise<NewsCommentLike> {
  return request({
    url: newsCommentLikeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新NewsCommentLike
 * 对应后端：UpdateNewsCommentLikeAsync
 */
export function updateNewsCommentLike(id: string, data: NewsCommentLikeUpdate): Promise<NewsCommentLike> {
  return request({
    url: `${newsCommentLikeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除NewsCommentLike（单条）
 * 对应后端：DeleteNewsCommentLikeByIdAsync
 */
export function deleteNewsCommentLikeById(id: string): Promise<void> {
  return request({
    url: `${newsCommentLikeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除NewsCommentLike
 * 对应后端：DeleteNewsCommentLikeBatchAsync
 */
export function deleteNewsCommentLikeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${newsCommentLikeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetNewsCommentLikeTemplateAsync；fileName 仅传名称不含后缀
 */
export function getNewsCommentLikeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${newsCommentLikeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入NewsCommentLike
 * 对应后端：ImportNewsCommentLikeAsync
 */
export function importNewsCommentLikeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${newsCommentLikeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出NewsCommentLike
 * 对应后端：ExportNewsCommentLikeAsync；fileName 仅传名称不含后缀
 */
export function exportNewsCommentLikeData(query: NewsCommentLikeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${newsCommentLikeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
