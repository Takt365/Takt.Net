// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/news
// 文件名称：news-read.ts
// 功能描述：NewsRead API，对应后端 Takt.WebApi.Controllers.Routine.Business.News.TaktNewsReads
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  NewsRead,
  NewsReadQuery,
  NewsReadCreate,
  NewsReadUpdate
} from '@/types/routine/business/news/news-read'

// ========================================
// NewsRead相关 API（按后端控制器顺序）
// ========================================
const newsReadUrl = '/api/TaktNewsReads';

/**
 * 获取NewsRead列表（分页）
 * 对应后端：GetNewsReadListAsync
 */
export function getNewsReadList(params: NewsReadQuery): Promise<TaktPagedResult<NewsRead>> {
  return request({
    url: `${newsReadUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取NewsRead
 * 对应后端：GetNewsReadByIdAsync
 */
export function getNewsReadById(id: string): Promise<NewsRead> {
  return request({
    url: `${newsReadUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取NewsRead选项列表（用于下拉框等）
 * 对应后端：GetNewsReadOptionsAsync
 */
export function getNewsReadOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${newsReadUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建NewsRead
 * 对应后端：CreateNewsReadAsync
 */
export function createNewsRead(data: NewsReadCreate): Promise<NewsRead> {
  return request({
    url: newsReadUrl,
    method: 'post',
    data
  })
}

/**
 * 更新NewsRead
 * 对应后端：UpdateNewsReadAsync
 */
export function updateNewsRead(id: string, data: NewsReadUpdate): Promise<NewsRead> {
  return request({
    url: `${newsReadUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除NewsRead（单条）
 * 对应后端：DeleteNewsReadByIdAsync
 */
export function deleteNewsReadById(id: string): Promise<void> {
  return request({
    url: `${newsReadUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除NewsRead
 * 对应后端：DeleteNewsReadBatchAsync
 */
export function deleteNewsReadBatch(ids: string[]): Promise<void> {
  return request({
    url: `${newsReadUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetNewsReadTemplateAsync；fileName 仅传名称不含后缀
 */
export function getNewsReadTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${newsReadUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入NewsRead
 * 对应后端：ImportNewsReadAsync
 */
export function importNewsReadData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${newsReadUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出NewsRead
 * 对应后端：ExportNewsReadAsync；fileName 仅传名称不含后缀
 */
export function exportNewsReadData(query: NewsReadQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${newsReadUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
