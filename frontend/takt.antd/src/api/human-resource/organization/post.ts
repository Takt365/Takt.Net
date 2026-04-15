// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/organization/post
// 文件名称：post.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：岗位相关 API，对应后端 TaktPostsController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Post,
  PostQuery,
  PostCreate,
  PostUpdate,
  PostStatus
} from '@/types/human-resource/organization/post'
import type { UserPost } from '@/types/human-resource/organization/user-post'

// ========================================
// 岗位相关 API（按后端控制器顺序）
// ========================================

const postUrl = '/api/TaktPosts'

/**
 * 获取岗位列表（分页）
 * 对应后端：GetListAsync
 */
export function getPostList(params: PostQuery): Promise<TaktPagedResult<Post>> {
  return request({
    url: `${postUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取岗位
 * 对应后端：GetByIdAsync
 */
export function getPostById(id: string): Promise<Post> {
  return request({
    url: `${postUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取岗位选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getPostOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${postUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建岗位
 * 对应后端：CreateAsync
 */
export function createPost(data: PostCreate): Promise<Post> {
  return request({
    url: postUrl,
    method: 'post',
    data
  })
}

/**
 * 更新岗位
 * 对应后端：UpdateAsync
 */
export function updatePost(id: string, data: PostUpdate): Promise<Post> {
  return request({
    url: `${postUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除岗位
 * 对应后端：DeleteAsync
 */
export function deletePostById(id: string): Promise<void> {
  return request({
    url: `${postUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 更新岗位状态
 * 对应后端：UpdateStatusAsync
 */
export function updatePostStatus(data: PostStatus): Promise<Post> {
  return request({
    url: `${postUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取岗位用户列表
 * 对应后端：GetUserPostIdsAsync
 */
export function getUserPostIds(postId: string): Promise<UserPost[]> {
  return request({
    url: `${postUrl}/${postId}/users`,
    method: 'get'
  })
}

/**
 * 分配岗位用户
 * 对应后端：AssignUserPostsAsync
 */
export function assignUserPosts(postId: string, userIds: string[]): Promise<{ success: boolean }> {
  return request({
    url: `${postUrl}/${postId}/users`,
    method: 'put',
    data: userIds
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync
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
 * 导入岗位
 * 对应后端：ImportAsync
 */
export function importPostData(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: `${postUrl}/import`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出岗位
 * 对应后端：ExportAsync
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
