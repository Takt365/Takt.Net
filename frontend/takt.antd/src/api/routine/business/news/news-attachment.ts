// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/news
// 文件名称：news-attachment.ts
// 功能描述：NewsAttachment API，对应后端 Takt.WebApi.Controllers.Routine.Business.News.TaktNewsAttachments
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  NewsAttachment,
  NewsAttachmentQuery,
  NewsAttachmentCreate,
  NewsAttachmentUpdate,
  NewsAttachmentSort
} from '@/types/routine/business/news/news-attachment'

// ========================================
// NewsAttachment相关 API（按后端控制器顺序）
// ========================================
const newsAttachmentUrl = '/api/TaktNewsAttachments';

/**
 * 获取NewsAttachment列表（分页）
 * 对应后端：GetNewsAttachmentListAsync
 */
export function getNewsAttachmentList(params: NewsAttachmentQuery): Promise<TaktPagedResult<NewsAttachment>> {
  return request({
    url: `${newsAttachmentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取NewsAttachment
 * 对应后端：GetNewsAttachmentByIdAsync
 */
export function getNewsAttachmentById(id: string): Promise<NewsAttachment> {
  return request({
    url: `${newsAttachmentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取NewsAttachment选项列表（用于下拉框等）
 * 对应后端：GetNewsAttachmentOptionsAsync
 */
export function getNewsAttachmentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${newsAttachmentUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建NewsAttachment
 * 对应后端：CreateNewsAttachmentAsync
 */
export function createNewsAttachment(data: NewsAttachmentCreate): Promise<NewsAttachment> {
  return request({
    url: newsAttachmentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新NewsAttachment
 * 对应后端：UpdateNewsAttachmentAsync
 */
export function updateNewsAttachment(id: string, data: NewsAttachmentUpdate): Promise<NewsAttachment> {
  return request({
    url: `${newsAttachmentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除NewsAttachment（单条）
 * 对应后端：DeleteNewsAttachmentByIdAsync
 */
export function deleteNewsAttachmentById(id: string): Promise<void> {
  return request({
    url: `${newsAttachmentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除NewsAttachment
 * 对应后端：DeleteNewsAttachmentBatchAsync
 */
export function deleteNewsAttachmentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${newsAttachmentUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新NewsAttachment排序
 * 对应后端：UpdateNewsAttachmentSortAsync
 */
export function updateNewsAttachmentSort(data: NewsAttachmentSort): Promise<NewsAttachmentSort> {
  return request({
    url: `${newsAttachmentUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetNewsAttachmentTemplateAsync；fileName 仅传名称不含后缀
 */
export function getNewsAttachmentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${newsAttachmentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入NewsAttachment
 * 对应后端：ImportNewsAttachmentAsync
 */
export function importNewsAttachmentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${newsAttachmentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出NewsAttachment
 * 对应后端：ExportNewsAttachmentAsync；fileName 仅传名称不含后缀
 */
export function exportNewsAttachmentData(query: NewsAttachmentQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${newsAttachmentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
