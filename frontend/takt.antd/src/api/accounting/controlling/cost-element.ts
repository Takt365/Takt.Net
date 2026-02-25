// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling/cost-element
// 文件名称：cost-element.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：成本要素相关 API，对应后端 TaktCostElementsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult } from '@/types/common'
import type {
  CostElement,
  CostElementTree,
  CostElementTreeOption,
  CostElementQuery,
  CostElementCreate,
  CostElementUpdate,
  CostElementStatus
} from '@/types/accounting/controlling/cost-element'

/**
 * 获取成本要素列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: CostElementQuery): Promise<TaktPagedResult<CostElement>> {
  return request({
    url: '/api/TaktCostElements/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取成本要素
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<CostElement> {
  return request({
    url: `/api/TaktCostElements/${id}`,
    method: 'get'
  })
}

/**
 * 获取成本要素树形选项（用于树形下拉等）
 * 对应后端：GetTreeOptionsAsync
 */
export function getTreeOptions(): Promise<CostElementTreeOption[]> {
  return request({
    url: '/api/TaktCostElements/tree-options',
    method: 'get'
  })
}

/**
 * 获取成本要素树形列表
 * 对应后端：GetTreeAsync
 */
export function getTree(
  parentId: string | number = 0,
  includeDisabled = false
): Promise<CostElementTree[]> {
  return request({
    url: '/api/TaktCostElements/tree',
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 获取成本要素子节点列表
 * 对应后端：GetChildrenAsync
 */
export function getChildren(
  parentId: string,
  includeDisabled = false
): Promise<CostElement[]> {
  return request({
    url: '/api/TaktCostElements/children',
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 创建成本要素
 * 对应后端：CreateAsync
 */
export function create(data: CostElementCreate): Promise<CostElement> {
  return request({
    url: '/api/TaktCostElements',
    method: 'post',
    data
  })
}

/**
 * 更新成本要素
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: CostElementUpdate): Promise<CostElement> {
  return request({
    url: `/api/TaktCostElements/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除成本要素
 * 对应后端：DeleteAsync
 */
export function deleteCostElement(id: string): Promise<void> {
  return request({
    url: `/api/TaktCostElements/${id}`,
    method: 'delete'
  })
}

/**
 * 更新成本要素状态
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: CostElementStatus): Promise<CostElement> {
  return request({
    url: '/api/TaktCostElements/status',
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktCostElements/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入成本要素
 * 对应后端：ImportAsync
 */
export function importCostElements(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktCostElements/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出成本要素
 * 对应后端：ExportAsync；fileName 仅传名称不含后缀
 */
export function exportCostElements(
  query: CostElementQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktCostElements/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
