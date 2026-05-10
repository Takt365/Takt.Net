// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/organization
// 文件名称：user-dept.ts
// 功能描述：UserDept API，对应后端 Takt.WebApi.Controllers.HumanResource.Organization.TaktUserDepts
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  UserDept,
  UserDeptQuery,
  UserDeptCreate,
  UserDeptUpdate
} from '@/types/human-resource/organization/user-dept'

// ========================================
// UserDept相关 API（按后端控制器顺序）
// ========================================
const userDeptUrl = '/api/TaktUserDepts';

/**
 * 获取UserDept列表（分页）
 * 对应后端：GetUserDeptListAsync
 */
export function getUserDeptList(params: UserDeptQuery): Promise<TaktPagedResult<UserDept>> {
  return request({
    url: `${userDeptUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取UserDept
 * 对应后端：GetUserDeptByIdAsync
 */
export function getUserDeptById(id: string): Promise<UserDept> {
  return request({
    url: `${userDeptUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取UserDept选项列表（用于下拉框等）
 * 对应后端：GetUserDeptOptionsAsync
 */
export function getUserDeptOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${userDeptUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建UserDept
 * 对应后端：CreateUserDeptAsync
 */
export function createUserDept(data: UserDeptCreate): Promise<UserDept> {
  return request({
    url: userDeptUrl,
    method: 'post',
    data
  })
}

/**
 * 更新UserDept
 * 对应后端：UpdateUserDeptAsync
 */
export function updateUserDept(id: string, data: UserDeptUpdate): Promise<UserDept> {
  return request({
    url: `${userDeptUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除UserDept（单条）
 * 对应后端：DeleteUserDeptByIdAsync
 */
export function deleteUserDeptById(id: string): Promise<void> {
  return request({
    url: `${userDeptUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除UserDept
 * 对应后端：DeleteUserDeptBatchAsync
 */
export function deleteUserDeptBatch(ids: string[]): Promise<void> {
  return request({
    url: `${userDeptUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetUserDeptTemplateAsync；fileName 仅传名称不含后缀
 */
export function getUserDeptTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${userDeptUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入UserDept
 * 对应后端：ImportUserDeptAsync
 */
export function importUserDeptData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${userDeptUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出UserDept
 * 对应后端：ExportUserDeptAsync；fileName 仅传名称不含后缀
 */
export function exportUserDeptData(query: UserDeptQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${userDeptUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
