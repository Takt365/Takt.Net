// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/organization
// 文件名称：user-post.ts
// 功能描述：UserPost API，对应后端 Takt.WebApi.Controllers.HumanResource.Organization.TaktUserPosts
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  UserPost,
  UserPostQuery,
  UserPostCreate,
  UserPostUpdate
} from '@/types/human-resource/organization/user-post'

// ========================================
// UserPost相关 API（按后端控制器顺序）
// ========================================
const userPostUrl = '/api/TaktUserPosts';

/**
 * 获取UserPost列表（分页）
 * 对应后端：GetUserPostListAsync
 */
export function getUserPostList(params: UserPostQuery): Promise<TaktPagedResult<UserPost>> {
  return request({
    url: `${userPostUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取UserPost
 * 对应后端：GetUserPostByIdAsync
 */
export function getUserPostById(id: string): Promise<UserPost> {
  return request({
    url: `${userPostUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取UserPost选项列表（用于下拉框等）
 * 对应后端：GetUserPostOptionsAsync
 */
export function getUserPostOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${userPostUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建UserPost
 * 对应后端：CreateUserPostAsync
 */
export function createUserPost(data: UserPostCreate): Promise<UserPost> {
  return request({
    url: userPostUrl,
    method: 'post',
    data
  })
}

/**
 * 更新UserPost
 * 对应后端：UpdateUserPostAsync
 */
export function updateUserPost(id: string, data: UserPostUpdate): Promise<UserPost> {
  return request({
    url: `${userPostUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除UserPost（单条）
 * 对应后端：DeleteUserPostByIdAsync
 */
export function deleteUserPostById(id: string): Promise<void> {
  return request({
    url: `${userPostUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除UserPost
 * 对应后端：DeleteUserPostBatchAsync
 */
export function deleteUserPostBatch(ids: string[]): Promise<void> {
  return request({
    url: `${userPostUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetUserPostTemplateAsync；fileName 仅传名称不含后缀
 */
export function getUserPostTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${userPostUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入UserPost
 * 对应后端：ImportUserPostAsync
 */
export function importUserPostData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${userPostUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出UserPost
 * 对应后端：ExportUserPostAsync；fileName 仅传名称不含后缀
 */
export function exportUserPostData(query: UserPostQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${userPostUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
