// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling
// 文件名称：profit-center.ts
// 功能描述：ProfitCenter API，对应后端 Takt.WebApi.Controllers.Accounting.Controlling.TaktProfitCenters
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import type {
  ProfitCenter,
  ProfitCenterTree,
  ProfitCenterQuery,
  ProfitCenterCreate,
  ProfitCenterUpdate,
  ProfitCenterStatus,
  ProfitCenterSort
} from '@/types/accounting/controlling/profit-center'

// ========================================
// ProfitCenter相关 API（按后端控制器顺序）
// ========================================
const profitCenterUrl = '/api/TaktProfitCenters';

/**
 * 获取ProfitCenter列表（分页）
 * 对应后端：GetProfitCenterListAsync
 */
export function getProfitCenterList(params: ProfitCenterQuery): Promise<TaktPagedResult<ProfitCenter>> {
  return request({
    url: `${profitCenterUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ProfitCenter
 * 对应后端：GetProfitCenterByIdAsync
 */
export function getProfitCenterById(id: string): Promise<ProfitCenter> {
  return request({
    url: `${profitCenterUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ProfitCenter选项列表（用于下拉框等）
 * 对应后端：GetProfitCenterOptionsAsync
 */
export function getProfitCenterOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${profitCenterUrl}/options`,
    method: 'get'
  })
}

/**
 * 获取ProfitCenter树形选项列表（用于树形下拉框等）
 * 对应后端：GetProfitCenterTreeOptionsAsync
 */
export function getProfitCenterTreeOptions(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: `${profitCenterUrl}/tree-options`,
    method: 'get'
  })
}

/**
 * 获取ProfitCenter树形列表
 * 对应后端：GetProfitCenterTreeAsync
 */
export function getProfitCenterTree(parentId: number = 0, includeDisabled: boolean = false): Promise<ProfitCenterTree[]> {
  return request({
    url: `${profitCenterUrl}/tree`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 获取ProfitCenter子节点列表
 * 对应后端：GetProfitCenterChildrenAsync
 */
export function getProfitCenterChildren(parentId: number, includeDisabled: boolean = false): Promise<ProfitCenter[]> {
  return request({
    url: `${profitCenterUrl}/children`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 创建ProfitCenter
 * 对应后端：CreateProfitCenterAsync
 */
export function createProfitCenter(data: ProfitCenterCreate): Promise<ProfitCenter> {
  return request({
    url: profitCenterUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ProfitCenter
 * 对应后端：UpdateProfitCenterAsync
 */
export function updateProfitCenter(id: string, data: ProfitCenterUpdate): Promise<ProfitCenter> {
  return request({
    url: `${profitCenterUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ProfitCenter（单条）
 * 对应后端：DeleteProfitCenterByIdAsync
 */
export function deleteProfitCenterById(id: string): Promise<void> {
  return request({
    url: `${profitCenterUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ProfitCenter
 * 对应后端：DeleteProfitCenterBatchAsync
 */
export function deleteProfitCenterBatch(ids: string[]): Promise<void> {
  return request({
    url: `${profitCenterUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新ProfitCenter状态
 * 对应后端：UpdateProfitCenterStatusAsync
 */
export function updateProfitCenterStatus(data: ProfitCenterStatus): Promise<ProfitCenterStatus> {
  return request({
    url: `${profitCenterUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新ProfitCenter排序
 * 对应后端：UpdateProfitCenterSortAsync
 */
export function updateProfitCenterSort(data: ProfitCenterSort): Promise<ProfitCenterSort> {
  return request({
    url: `${profitCenterUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetProfitCenterTemplateAsync；fileName 仅传名称不含后缀
 */
export function getProfitCenterTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${profitCenterUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ProfitCenter
 * 对应后端：ImportProfitCenterAsync
 */
export function importProfitCenterData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${profitCenterUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ProfitCenter
 * 对应后端：ExportProfitCenterAsync；fileName 仅传名称不含后缀
 */
export function exportProfitCenterData(query: ProfitCenterQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${profitCenterUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
