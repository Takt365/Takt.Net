// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling/cost-center
// 文件名称：cost-center.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：成本中心相关 API，对应后端 TaktCostCentersController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CostCenter,
  CostCenterQuery,
  CostCenterCreate,
  CostCenterUpdate,
  CostCenterStatus
} from '@/types/accounting/controlling/cost-center'

/**
 * 获取成本中心列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: CostCenterQuery): Promise<TaktPagedResult<CostCenter>> {
  return request({
    url: '/api/TaktCostCenters/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取成本中心
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<CostCenter> {
  return request({
    url: `/api/TaktCostCenters/${id}`,
    method: 'get'
  })
}

/**
 * 获取成本中心选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktCostCenters/options',
    method: 'get'
  })
}

/**
 * 创建成本中心
 * 对应后端：CreateAsync
 */
export function create(data: CostCenterCreate): Promise<CostCenter> {
  return request({
    url: '/api/TaktCostCenters',
    method: 'post',
    data
  })
}

/**
 * 更新成本中心
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: CostCenterUpdate): Promise<CostCenter> {
  return request({
    url: `/api/TaktCostCenters/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除成本中心
 * 对应后端：DeleteAsync
 */
export function deleteCostCenter(id: string): Promise<void> {
  return request({
    url: `/api/TaktCostCenters/${id}`,
    method: 'delete'
  })
}

/**
 * 更新成本中心状态
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: CostCenterStatus): Promise<CostCenter> {
  return request({
    url: '/api/TaktCostCenters/status',
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
    url: '/api/TaktCostCenters/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入成本中心
 * 对应后端：ImportAsync
 */
export function importCostCenters(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktCostCenters/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出成本中心
 * 对应后端：ExportAsync；fileName 仅传名称不含后缀
 */
export function exportCostCenters(
  query: CostCenterQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktCostCenters/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
