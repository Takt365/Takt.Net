// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/news
// 文件名称：news-share.ts
// 功能描述：NewsShare API，对应后端 Takt.WebApi.Controllers.Routine.Business.News.TaktNewsShares
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  NewsShare,
  NewsShareQuery,
  NewsShareCreate,
  NewsShareUpdate
} from '@/types/routine/business/news/news-share'

// ========================================
// NewsShare相关 API（按后端控制器顺序）
// ========================================
const newsShareUrl = '/api/TaktNewsShares';

/**
 * 获取NewsShare列表（分页）
 * 对应后端：GetNewsShareListAsync
 */
export function getNewsShareList(params: NewsShareQuery): Promise<TaktPagedResult<NewsShare>> {
  return request({
    url: `${newsShareUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取NewsShare
 * 对应后端：GetNewsShareByIdAsync
 */
export function getNewsShareById(id: string): Promise<NewsShare> {
  return request({
    url: `${newsShareUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取NewsShare选项列表（用于下拉框等）
 * 对应后端：GetNewsShareOptionsAsync
 */
export function getNewsShareOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${newsShareUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建NewsShare
 * 对应后端：CreateNewsShareAsync
 */
export function createNewsShare(data: NewsShareCreate): Promise<NewsShare> {
  return request({
    url: newsShareUrl,
    method: 'post',
    data
  })
}

/**
 * 更新NewsShare
 * 对应后端：UpdateNewsShareAsync
 */
export function updateNewsShare(id: string, data: NewsShareUpdate): Promise<NewsShare> {
  return request({
    url: `${newsShareUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除NewsShare（单条）
 * 对应后端：DeleteNewsShareByIdAsync
 */
export function deleteNewsShareById(id: string): Promise<void> {
  return request({
    url: `${newsShareUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除NewsShare
 * 对应后端：DeleteNewsShareBatchAsync
 */
export function deleteNewsShareBatch(ids: string[]): Promise<void> {
  return request({
    url: `${newsShareUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetNewsShareTemplateAsync；fileName 仅传名称不含后缀
 */
export function getNewsShareTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${newsShareUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入NewsShare
 * 对应后端：ImportNewsShareAsync
 */
export function importNewsShareData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${newsShareUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出NewsShare
 * 对应后端：ExportNewsShareAsync；fileName 仅传名称不含后缀
 */
export function exportNewsShareData(query: NewsShareQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${newsShareUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
