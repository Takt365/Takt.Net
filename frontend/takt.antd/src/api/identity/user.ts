// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity
// 文件名称：user.ts
// 功能描述：User API，对应后端 Takt.WebApi.Controllers.Identity.TaktUsers
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  User,
  UserQuery,
  UserCreate,
  UserUpdate,
  UserStatus
} from '@/types/identity/user'

// ========================================
// User相关 API（按后端控制器顺序）
// ========================================
const userUrl = '/api/TaktUsers';

/**
 * 获取User列表（分页）
 * 对应后端：GetUserListAsync
 */
export function getUserList(params: UserQuery): Promise<TaktPagedResult<User>> {
  return request({
    url: `${userUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取User
 * 对应后端：GetUserByIdAsync
 */
export function getUserById(id: string): Promise<User> {
  return request({
    url: `${userUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取User选项列表（用于下拉框等）
 * 对应后端：GetUserOptionsAsync
 */
export function getUserOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${userUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建User
 * 对应后端：CreateUserAsync
 */
export function createUser(data: UserCreate): Promise<User> {
  return request({
    url: userUrl,
    method: 'post',
    data
  })
}

/**
 * 更新User
 * 对应后端：UpdateUserAsync
 */
export function updateUser(id: string, data: UserUpdate): Promise<User> {
  return request({
    url: `${userUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除User（单条）
 * 对应后端：DeleteUserByIdAsync
 */
export function deleteUserById(id: string): Promise<void> {
  return request({
    url: `${userUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除User
 * 对应后端：DeleteUserBatchAsync
 */
export function deleteUserBatch(ids: string[]): Promise<void> {
  return request({
    url: `${userUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新User状态
 * 对应后端：UpdateUserStatusAsync
 */
export function updateUserStatus(data: UserStatus): Promise<UserStatus> {
  return request({
    url: `${userUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetUserTemplateAsync；fileName 仅传名称不含后缀
 */
export function getUserTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${userUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入User
 * 对应后端：ImportUserAsync
 */
export function importUserData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${userUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出User
 * 对应后端：ExportUserAsync；fileName 仅传名称不含后缀
 */
export function exportUserData(query: UserQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${userUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
