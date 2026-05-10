// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling
// 文件名称：cost-center.ts
// 功能描述：CostCenter API，对应后端 Takt.WebApi.Controllers.Accounting.Controlling.TaktCostCenters
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import type {
  CostCenter,
  CostCenterTree,
  CostCenterQuery,
  CostCenterCreate,
  CostCenterUpdate,
  CostCenterStatus,
  CostCenterSort
} from '@/types/accounting/controlling/cost-center'

// ========================================
// CostCenter相关 API（按后端控制器顺序）
// ========================================
const costCenterUrl = '/api/TaktCostCenters';

/**
 * 获取CostCenter列表（分页）
 * 对应后端：GetCostCenterListAsync
 */
export function getCostCenterList(params: CostCenterQuery): Promise<TaktPagedResult<CostCenter>> {
  return request({
    url: `${costCenterUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CostCenter
 * 对应后端：GetCostCenterByIdAsync
 */
export function getCostCenterById(id: string): Promise<CostCenter> {
  return request({
    url: `${costCenterUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CostCenter选项列表（用于下拉框等）
 * 对应后端：GetCostCenterOptionsAsync
 */
export function getCostCenterOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${costCenterUrl}/options`,
    method: 'get'
  })
}

/**
 * 获取CostCenter树形选项列表（用于树形下拉框等）
 * 对应后端：GetCostCenterTreeOptionsAsync
 */
export function getCostCenterTreeOptions(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: `${costCenterUrl}/tree-options`,
    method: 'get'
  })
}

/**
 * 获取CostCenter树形列表
 * 对应后端：GetCostCenterTreeAsync
 */
export function getCostCenterTree(parentId: number = 0, includeDisabled: boolean = false): Promise<CostCenterTree[]> {
  return request({
    url: `${costCenterUrl}/tree`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 获取CostCenter子节点列表
 * 对应后端：GetCostCenterChildrenAsync
 */
export function getCostCenterChildren(parentId: number, includeDisabled: boolean = false): Promise<CostCenter[]> {
  return request({
    url: `${costCenterUrl}/children`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 创建CostCenter
 * 对应后端：CreateCostCenterAsync
 */
export function createCostCenter(data: CostCenterCreate): Promise<CostCenter> {
  return request({
    url: costCenterUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CostCenter
 * 对应后端：UpdateCostCenterAsync
 */
export function updateCostCenter(id: string, data: CostCenterUpdate): Promise<CostCenter> {
  return request({
    url: `${costCenterUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CostCenter（单条）
 * 对应后端：DeleteCostCenterByIdAsync
 */
export function deleteCostCenterById(id: string): Promise<void> {
  return request({
    url: `${costCenterUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CostCenter
 * 对应后端：DeleteCostCenterBatchAsync
 */
export function deleteCostCenterBatch(ids: string[]): Promise<void> {
  return request({
    url: `${costCenterUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新CostCenter状态
 * 对应后端：UpdateCostCenterStatusAsync
 */
export function updateCostCenterStatus(data: CostCenterStatus): Promise<CostCenterStatus> {
  return request({
    url: `${costCenterUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新CostCenter排序
 * 对应后端：UpdateCostCenterSortAsync
 */
export function updateCostCenterSort(data: CostCenterSort): Promise<CostCenterSort> {
  return request({
    url: `${costCenterUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCostCenterTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCostCenterTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${costCenterUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CostCenter
 * 对应后端：ImportCostCenterAsync
 */
export function importCostCenterData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${costCenterUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CostCenter
 * 对应后端：ExportCostCenterAsync；fileName 仅传名称不含后缀
 */
export function exportCostCenterData(query: CostCenterQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${costCenterUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
