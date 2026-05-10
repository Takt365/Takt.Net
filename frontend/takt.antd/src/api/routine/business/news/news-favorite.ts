// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/news
// 文件名称：news-favorite.ts
// 功能描述：NewsFavorite API，对应后端 Takt.WebApi.Controllers.Routine.Business.News.TaktNewsFavorites
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  NewsFavorite,
  NewsFavoriteQuery,
  NewsFavoriteCreate,
  NewsFavoriteUpdate
} from '@/types/routine/business/news/news-favorite'

// ========================================
// NewsFavorite相关 API（按后端控制器顺序）
// ========================================
const newsFavoriteUrl = '/api/TaktNewsFavorites';

/**
 * 获取NewsFavorite列表（分页）
 * 对应后端：GetNewsFavoriteListAsync
 */
export function getNewsFavoriteList(params: NewsFavoriteQuery): Promise<TaktPagedResult<NewsFavorite>> {
  return request({
    url: `${newsFavoriteUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取NewsFavorite
 * 对应后端：GetNewsFavoriteByIdAsync
 */
export function getNewsFavoriteById(id: string): Promise<NewsFavorite> {
  return request({
    url: `${newsFavoriteUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取NewsFavorite选项列表（用于下拉框等）
 * 对应后端：GetNewsFavoriteOptionsAsync
 */
export function getNewsFavoriteOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${newsFavoriteUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建NewsFavorite
 * 对应后端：CreateNewsFavoriteAsync
 */
export function createNewsFavorite(data: NewsFavoriteCreate): Promise<NewsFavorite> {
  return request({
    url: newsFavoriteUrl,
    method: 'post',
    data
  })
}

/**
 * 更新NewsFavorite
 * 对应后端：UpdateNewsFavoriteAsync
 */
export function updateNewsFavorite(id: string, data: NewsFavoriteUpdate): Promise<NewsFavorite> {
  return request({
    url: `${newsFavoriteUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除NewsFavorite（单条）
 * 对应后端：DeleteNewsFavoriteByIdAsync
 */
export function deleteNewsFavoriteById(id: string): Promise<void> {
  return request({
    url: `${newsFavoriteUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除NewsFavorite
 * 对应后端：DeleteNewsFavoriteBatchAsync
 */
export function deleteNewsFavoriteBatch(ids: string[]): Promise<void> {
  return request({
    url: `${newsFavoriteUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetNewsFavoriteTemplateAsync；fileName 仅传名称不含后缀
 */
export function getNewsFavoriteTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${newsFavoriteUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入NewsFavorite
 * 对应后端：ImportNewsFavoriteAsync
 */
export function importNewsFavoriteData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${newsFavoriteUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出NewsFavorite
 * 对应后端：ExportNewsFavoriteAsync；fileName 仅传名称不含后缀
 */
export function exportNewsFavoriteData(query: NewsFavoriteQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${newsFavoriteUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
