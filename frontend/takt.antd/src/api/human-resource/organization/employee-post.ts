// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/organization
// 文件名称：employee-post.ts
// 功能描述：EmployeePost API，对应后端 Takt.WebApi.Controllers.HumanResource.Organization.TaktEmployeePosts
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeePost,
  EmployeePostQuery,
  EmployeePostCreate,
  EmployeePostUpdate
} from '@/types/human-resource/organization/employee-post'

// ========================================
// EmployeePost相关 API（按后端控制器顺序）
// ========================================
const employeePostUrl = '/api/TaktEmployeePosts';

/**
 * 获取EmployeePost列表（分页）
 * 对应后端：GetEmployeePostListAsync
 */
export function getEmployeePostList(params: EmployeePostQuery): Promise<TaktPagedResult<EmployeePost>> {
  return request({
    url: `${employeePostUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeePost
 * 对应后端：GetEmployeePostByIdAsync
 */
export function getEmployeePostById(id: string): Promise<EmployeePost> {
  return request({
    url: `${employeePostUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeePost选项列表（用于下拉框等）
 * 对应后端：GetEmployeePostOptionsAsync
 */
export function getEmployeePostOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeePostUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeePost
 * 对应后端：CreateEmployeePostAsync
 */
export function createEmployeePost(data: EmployeePostCreate): Promise<EmployeePost> {
  return request({
    url: employeePostUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeePost
 * 对应后端：UpdateEmployeePostAsync
 */
export function updateEmployeePost(id: string, data: EmployeePostUpdate): Promise<EmployeePost> {
  return request({
    url: `${employeePostUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeePost（单条）
 * 对应后端：DeleteEmployeePostByIdAsync
 */
export function deleteEmployeePostById(id: string): Promise<void> {
  return request({
    url: `${employeePostUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeePost
 * 对应后端：DeleteEmployeePostBatchAsync
 */
export function deleteEmployeePostBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeePostUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeePostTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeePostTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeePostUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeePost
 * 对应后端：ImportEmployeePostAsync
 */
export function importEmployeePostData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeePostUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeePost
 * 对应后端：ExportEmployeePostAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeePostData(query: EmployeePostQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeePostUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
