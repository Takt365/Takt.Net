// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling
// 文件名称：cost-element.ts
// 功能描述：CostElement API，对应后端 Takt.WebApi.Controllers.Accounting.Controlling.TaktCostElements
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import type {
  CostElement,
  CostElementTree,
  CostElementQuery,
  CostElementCreate,
  CostElementUpdate,
  CostElementStatus,
  CostElementSort
} from '@/types/accounting/controlling/cost-element'

// ========================================
// CostElement相关 API（按后端控制器顺序）
// ========================================
const costElementUrl = '/api/TaktCostElements';

/**
 * 获取CostElement列表（分页）
 * 对应后端：GetCostElementListAsync
 */
export function getCostElementList(params: CostElementQuery): Promise<TaktPagedResult<CostElement>> {
  return request({
    url: `${costElementUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CostElement
 * 对应后端：GetCostElementByIdAsync
 */
export function getCostElementById(id: string): Promise<CostElement> {
  return request({
    url: `${costElementUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CostElement选项列表（用于下拉框等）
 * 对应后端：GetCostElementOptionsAsync
 */
export function getCostElementOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${costElementUrl}/options`,
    method: 'get'
  })
}

/**
 * 获取CostElement树形选项列表（用于树形下拉框等）
 * 对应后端：GetCostElementTreeOptionsAsync
 */
export function getCostElementTreeOptions(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: `${costElementUrl}/tree-options`,
    method: 'get'
  })
}

/**
 * 获取CostElement树形列表
 * 对应后端：GetCostElementTreeAsync
 */
export function getCostElementTree(parentId: number = 0, includeDisabled: boolean = false): Promise<CostElementTree[]> {
  return request({
    url: `${costElementUrl}/tree`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 获取CostElement子节点列表
 * 对应后端：GetCostElementChildrenAsync
 */
export function getCostElementChildren(parentId: number, includeDisabled: boolean = false): Promise<CostElement[]> {
  return request({
    url: `${costElementUrl}/children`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 创建CostElement
 * 对应后端：CreateCostElementAsync
 */
export function createCostElement(data: CostElementCreate): Promise<CostElement> {
  return request({
    url: costElementUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CostElement
 * 对应后端：UpdateCostElementAsync
 */
export function updateCostElement(id: string, data: CostElementUpdate): Promise<CostElement> {
  return request({
    url: `${costElementUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CostElement（单条）
 * 对应后端：DeleteCostElementByIdAsync
 */
export function deleteCostElementById(id: string): Promise<void> {
  return request({
    url: `${costElementUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CostElement
 * 对应后端：DeleteCostElementBatchAsync
 */
export function deleteCostElementBatch(ids: string[]): Promise<void> {
  return request({
    url: `${costElementUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新CostElement状态
 * 对应后端：UpdateCostElementStatusAsync
 */
export function updateCostElementStatus(data: CostElementStatus): Promise<CostElementStatus> {
  return request({
    url: `${costElementUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新CostElement排序
 * 对应后端：UpdateCostElementSortAsync
 */
export function updateCostElementSort(data: CostElementSort): Promise<CostElementSort> {
  return request({
    url: `${costElementUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCostElementTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCostElementTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${costElementUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CostElement
 * 对应后端：ImportCostElementAsync
 */
export function importCostElementData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${costElementUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CostElement
 * 对应后端：ExportCostElementAsync；fileName 仅传名称不含后缀
 */
export function exportCostElementData(query: CostElementQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${costElementUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
