// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/news
// 文件名称：news-comment.ts
// 功能描述：NewsComment API，对应后端 Takt.WebApi.Controllers.Routine.Business.News.TaktNewsComments
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import type {
  NewsComment,
  NewsCommentTree,
  NewsCommentQuery,
  NewsCommentCreate,
  NewsCommentUpdate
} from '@/types/routine/business/news/news-comment'

// ========================================
// NewsComment相关 API（按后端控制器顺序）
// ========================================
const newsCommentUrl = '/api/TaktNewsComments';

/**
 * 获取NewsComment列表（分页）
 * 对应后端：GetNewsCommentListAsync
 */
export function getNewsCommentList(params: NewsCommentQuery): Promise<TaktPagedResult<NewsComment>> {
  return request({
    url: `${newsCommentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取NewsComment
 * 对应后端：GetNewsCommentByIdAsync
 */
export function getNewsCommentById(id: string): Promise<NewsComment> {
  return request({
    url: `${newsCommentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取NewsComment选项列表（用于下拉框等）
 * 对应后端：GetNewsCommentOptionsAsync
 */
export function getNewsCommentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${newsCommentUrl}/options`,
    method: 'get'
  })
}

/**
 * 获取NewsComment树形选项列表（用于树形下拉框等）
 * 对应后端：GetNewsCommentTreeOptionsAsync
 */
export function getNewsCommentTreeOptions(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: `${newsCommentUrl}/tree-options`,
    method: 'get'
  })
}

/**
 * 获取NewsComment树形列表
 * 对应后端：GetNewsCommentTreeAsync
 */
export function getNewsCommentTree(parentId: number = 0, includeDisabled: boolean = false): Promise<NewsCommentTree[]> {
  return request({
    url: `${newsCommentUrl}/tree`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 获取NewsComment子节点列表
 * 对应后端：GetNewsCommentChildrenAsync
 */
export function getNewsCommentChildren(parentId: number, includeDisabled: boolean = false): Promise<NewsComment[]> {
  return request({
    url: `${newsCommentUrl}/children`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 创建NewsComment
 * 对应后端：CreateNewsCommentAsync
 */
export function createNewsComment(data: NewsCommentCreate): Promise<NewsComment> {
  return request({
    url: newsCommentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新NewsComment
 * 对应后端：UpdateNewsCommentAsync
 */
export function updateNewsComment(id: string, data: NewsCommentUpdate): Promise<NewsComment> {
  return request({
    url: `${newsCommentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除NewsComment（单条）
 * 对应后端：DeleteNewsCommentByIdAsync
 */
export function deleteNewsCommentById(id: string): Promise<void> {
  return request({
    url: `${newsCommentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除NewsComment
 * 对应后端：DeleteNewsCommentBatchAsync
 */
export function deleteNewsCommentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${newsCommentUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetNewsCommentTemplateAsync；fileName 仅传名称不含后缀
 */
export function getNewsCommentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${newsCommentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入NewsComment
 * 对应后端：ImportNewsCommentAsync
 */
export function importNewsCommentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${newsCommentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出NewsComment
 * 对应后端：ExportNewsCommentAsync；fileName 仅传名称不含后缀
 */
export function exportNewsCommentData(query: NewsCommentQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${newsCommentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
