// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/organization
// 文件名称：post.ts
// 功能描述：Post API，对应后端 Takt.WebApi.Controllers.HumanResource.Organization.TaktPosts
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Post,
  PostQuery,
  PostCreate,
  PostUpdate,
  PostStatus,
  PostSort
} from '@/types/human-resource/organization/post'

// ========================================
// Post相关 API（按后端控制器顺序）
// ========================================
const postUrl = '/api/TaktPosts';

/**
 * 获取Post列表（分页）
 * 对应后端：GetPostListAsync
 */
export function getPostList(params: PostQuery): Promise<TaktPagedResult<Post>> {
  return request({
    url: `${postUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Post
 * 对应后端：GetPostByIdAsync
 */
export function getPostById(id: string): Promise<Post> {
  return request({
    url: `${postUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Post选项列表（用于下拉框等）
 * 对应后端：GetPostOptionsAsync
 */
export function getPostOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${postUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Post
 * 对应后端：CreatePostAsync
 */
export function createPost(data: PostCreate): Promise<Post> {
  return request({
    url: postUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Post
 * 对应后端：UpdatePostAsync
 */
export function updatePost(id: string, data: PostUpdate): Promise<Post> {
  return request({
    url: `${postUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Post（单条）
 * 对应后端：DeletePostByIdAsync
 */
export function deletePostById(id: string): Promise<void> {
  return request({
    url: `${postUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Post
 * 对应后端：DeletePostBatchAsync
 */
export function deletePostBatch(ids: string[]): Promise<void> {
  return request({
    url: `${postUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Post状态
 * 对应后端：UpdatePostStatusAsync
 */
export function updatePostStatus(data: PostStatus): Promise<PostStatus> {
  return request({
    url: `${postUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新Post排序
 * 对应后端：UpdatePostSortAsync
 */
export function updatePostSort(data: PostSort): Promise<PostSort> {
  return request({
    url: `${postUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPostTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPostTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${postUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Post
 * 对应后端：ImportPostAsync
 */
export function importPostData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${postUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Post
 * 对应后端：ExportPostAsync；fileName 仅传名称不含后缀
 */
export function exportPostData(query: PostQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${postUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
