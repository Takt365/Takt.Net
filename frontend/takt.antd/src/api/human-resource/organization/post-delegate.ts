// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/organization
// 文件名称：post-delegate.ts
// 功能描述：PostDelegate API，对应后端 Takt.WebApi.Controllers.HumanResource.Organization.TaktPostDelegates
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PostDelegate,
  PostDelegateQuery,
  PostDelegateCreate,
  PostDelegateUpdate,
  PostDelegateSort
} from '@/types/human-resource/organization/post-delegate'

// ========================================
// PostDelegate相关 API（按后端控制器顺序）
// ========================================
const postDelegateUrl = '/api/TaktPostDelegates';

/**
 * 获取PostDelegate列表（分页）
 * 对应后端：GetPostDelegateListAsync
 */
export function getPostDelegateList(params: PostDelegateQuery): Promise<TaktPagedResult<PostDelegate>> {
  return request({
    url: `${postDelegateUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PostDelegate
 * 对应后端：GetPostDelegateByIdAsync
 */
export function getPostDelegateById(id: string): Promise<PostDelegate> {
  return request({
    url: `${postDelegateUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PostDelegate选项列表（用于下拉框等）
 * 对应后端：GetPostDelegateOptionsAsync
 */
export function getPostDelegateOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${postDelegateUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PostDelegate
 * 对应后端：CreatePostDelegateAsync
 */
export function createPostDelegate(data: PostDelegateCreate): Promise<PostDelegate> {
  return request({
    url: postDelegateUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PostDelegate
 * 对应后端：UpdatePostDelegateAsync
 */
export function updatePostDelegate(id: string, data: PostDelegateUpdate): Promise<PostDelegate> {
  return request({
    url: `${postDelegateUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PostDelegate（单条）
 * 对应后端：DeletePostDelegateByIdAsync
 */
export function deletePostDelegateById(id: string): Promise<void> {
  return request({
    url: `${postDelegateUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PostDelegate
 * 对应后端：DeletePostDelegateBatchAsync
 */
export function deletePostDelegateBatch(ids: string[]): Promise<void> {
  return request({
    url: `${postDelegateUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新PostDelegate排序
 * 对应后端：UpdatePostDelegateSortAsync
 */
export function updatePostDelegateSort(data: PostDelegateSort): Promise<PostDelegateSort> {
  return request({
    url: `${postDelegateUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPostDelegateTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPostDelegateTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${postDelegateUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PostDelegate
 * 对应后端：ImportPostDelegateAsync
 */
export function importPostDelegateData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${postDelegateUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PostDelegate
 * 对应后端：ExportPostDelegateAsync；fileName 仅传名称不含后缀
 */
export function exportPostDelegateData(query: PostDelegateQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${postDelegateUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
