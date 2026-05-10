// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/organization
// 文件名称：dept.ts
// 功能描述：Dept API，对应后端 Takt.WebApi.Controllers.HumanResource.Organization.TaktDepts
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import type {
  Dept,
  DeptTree,
  DeptQuery,
  DeptCreate,
  DeptUpdate,
  DeptStatus,
  DeptSort
} from '@/types/human-resource/organization/dept'

// ========================================
// Dept相关 API（按后端控制器顺序）
// ========================================
const deptUrl = '/api/TaktDepts';

/**
 * 获取Dept列表（分页）
 * 对应后端：GetDeptListAsync
 */
export function getDeptList(params: DeptQuery): Promise<TaktPagedResult<Dept>> {
  return request({
    url: `${deptUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Dept
 * 对应后端：GetDeptByIdAsync
 */
export function getDeptById(id: string): Promise<Dept> {
  return request({
    url: `${deptUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Dept选项列表（用于下拉框等）
 * 对应后端：GetDeptOptionsAsync
 */
export function getDeptOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${deptUrl}/options`,
    method: 'get'
  })
}

/**
 * 获取Dept树形选项列表（用于树形下拉框等）
 * 对应后端：GetDeptTreeOptionsAsync
 */
export function getDeptTreeOptions(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: `${deptUrl}/tree-options`,
    method: 'get'
  })
}

/**
 * 获取Dept树形列表
 * 对应后端：GetDeptTreeAsync
 */
export function getDeptTree(parentId: number = 0, includeDisabled: boolean = false): Promise<DeptTree[]> {
  return request({
    url: `${deptUrl}/tree`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 获取Dept子节点列表
 * 对应后端：GetDeptChildrenAsync
 */
export function getDeptChildren(parentId: number, includeDisabled: boolean = false): Promise<Dept[]> {
  return request({
    url: `${deptUrl}/children`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 创建Dept
 * 对应后端：CreateDeptAsync
 */
export function createDept(data: DeptCreate): Promise<Dept> {
  return request({
    url: deptUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Dept
 * 对应后端：UpdateDeptAsync
 */
export function updateDept(id: string, data: DeptUpdate): Promise<Dept> {
  return request({
    url: `${deptUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Dept（单条）
 * 对应后端：DeleteDeptByIdAsync
 */
export function deleteDeptById(id: string): Promise<void> {
  return request({
    url: `${deptUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Dept
 * 对应后端：DeleteDeptBatchAsync
 */
export function deleteDeptBatch(ids: string[]): Promise<void> {
  return request({
    url: `${deptUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Dept状态
 * 对应后端：UpdateDeptStatusAsync
 */
export function updateDeptStatus(data: DeptStatus): Promise<DeptStatus> {
  return request({
    url: `${deptUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新Dept排序
 * 对应后端：UpdateDeptSortAsync
 */
export function updateDeptSort(data: DeptSort): Promise<DeptSort> {
  return request({
    url: `${deptUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetDeptTemplateAsync；fileName 仅传名称不含后缀
 */
export function getDeptTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${deptUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Dept
 * 对应后端：ImportDeptAsync
 */
export function importDeptData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${deptUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Dept
 * 对应后端：ExportDeptAsync；fileName 仅传名称不含后缀
 */
export function exportDeptData(query: DeptQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${deptUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
