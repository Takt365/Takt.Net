// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling/profit-center
// 文件名称：profit-center.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：利润中心相关 API，对应后端 TaktProfitCentersController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ProfitCenter,
  ProfitCenterQuery,
  ProfitCenterCreate,
  ProfitCenterUpdate,
  ProfitCenterStatus
} from '@/types/accounting/controlling/profit-center'

/**
 * 获取利润中心列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: ProfitCenterQuery): Promise<TaktPagedResult<ProfitCenter>> {
  return request({
    url: '/api/TaktProfitCenters/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取利润中心
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<ProfitCenter> {
  return request({
    url: `/api/TaktProfitCenters/${id}`,
    method: 'get'
  })
}

/**
 * 获取利润中心选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktProfitCenters/options',
    method: 'get'
  })
}

/**
 * 创建利润中心
 * 对应后端：CreateAsync
 */
export function create(data: ProfitCenterCreate): Promise<ProfitCenter> {
  return request({
    url: '/api/TaktProfitCenters',
    method: 'post',
    data
  })
}

/**
 * 更新利润中心
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: ProfitCenterUpdate): Promise<ProfitCenter> {
  return request({
    url: `/api/TaktProfitCenters/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除利润中心
 * 对应后端：DeleteAsync
 */
export function deleteProfitCenter(id: string): Promise<void> {
  return request({
    url: `/api/TaktProfitCenters/${id}`,
    method: 'delete'
  })
}

/**
 * 更新利润中心状态
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: ProfitCenterStatus): Promise<ProfitCenter> {
  return request({
    url: '/api/TaktProfitCenters/status',
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
    url: '/api/TaktProfitCenters/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入利润中心
 * 对应后端：ImportAsync
 */
export function importProfitCenters(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktProfitCenters/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出利润中心
 * 对应后端：ExportAsync；fileName 仅传名称不含后缀
 */
export function exportProfitCenters(
  query: ProfitCenterQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktProfitCenters/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
