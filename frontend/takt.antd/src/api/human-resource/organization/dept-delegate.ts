// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/organization
// 文件名称：dept-delegate.ts
// 功能描述：DeptDelegate API，对应后端 Takt.WebApi.Controllers.HumanResource.Organization.TaktDeptDelegates
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  DeptDelegate,
  DeptDelegateQuery,
  DeptDelegateCreate,
  DeptDelegateUpdate,
  DeptDelegateSort
} from '@/types/human-resource/organization/dept-delegate'

// ========================================
// DeptDelegate相关 API（按后端控制器顺序）
// ========================================
const deptDelegateUrl = '/api/TaktDeptDelegates';

/**
 * 获取DeptDelegate列表（分页）
 * 对应后端：GetDeptDelegateListAsync
 */
export function getDeptDelegateList(params: DeptDelegateQuery): Promise<TaktPagedResult<DeptDelegate>> {
  return request({
    url: `${deptDelegateUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取DeptDelegate
 * 对应后端：GetDeptDelegateByIdAsync
 */
export function getDeptDelegateById(id: string): Promise<DeptDelegate> {
  return request({
    url: `${deptDelegateUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取DeptDelegate选项列表（用于下拉框等）
 * 对应后端：GetDeptDelegateOptionsAsync
 */
export function getDeptDelegateOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${deptDelegateUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建DeptDelegate
 * 对应后端：CreateDeptDelegateAsync
 */
export function createDeptDelegate(data: DeptDelegateCreate): Promise<DeptDelegate> {
  return request({
    url: deptDelegateUrl,
    method: 'post',
    data
  })
}

/**
 * 更新DeptDelegate
 * 对应后端：UpdateDeptDelegateAsync
 */
export function updateDeptDelegate(id: string, data: DeptDelegateUpdate): Promise<DeptDelegate> {
  return request({
    url: `${deptDelegateUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除DeptDelegate（单条）
 * 对应后端：DeleteDeptDelegateByIdAsync
 */
export function deleteDeptDelegateById(id: string): Promise<void> {
  return request({
    url: `${deptDelegateUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除DeptDelegate
 * 对应后端：DeleteDeptDelegateBatchAsync
 */
export function deleteDeptDelegateBatch(ids: string[]): Promise<void> {
  return request({
    url: `${deptDelegateUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新DeptDelegate排序
 * 对应后端：UpdateDeptDelegateSortAsync
 */
export function updateDeptDelegateSort(data: DeptDelegateSort): Promise<DeptDelegateSort> {
  return request({
    url: `${deptDelegateUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetDeptDelegateTemplateAsync；fileName 仅传名称不含后缀
 */
export function getDeptDelegateTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${deptDelegateUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入DeptDelegate
 * 对应后端：ImportDeptDelegateAsync
 */
export function importDeptDelegateData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${deptDelegateUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出DeptDelegate
 * 对应后端：ExportDeptDelegateAsync；fileName 仅传名称不含后缀
 */
export function exportDeptDelegateData(query: DeptDelegateQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${deptDelegateUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
