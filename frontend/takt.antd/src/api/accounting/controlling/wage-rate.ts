// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling/wage-rate
// 文件名称：wage-rate.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：工资率相关 API，对应后端 TaktWageRatesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult } from '@/types/common'
import type {
  WageRate,
  WageRateQuery,
  WageRateCreate,
  WageRateUpdate
} from '@/types/accounting/controlling/wage-rate'

/**
 * 获取工资率列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: WageRateQuery): Promise<TaktPagedResult<WageRate>> {
  return request({
    url: '/api/TaktWageRates/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取工资率
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<WageRate> {
  return request({
    url: `/api/TaktWageRates/${id}`,
    method: 'get'
  })
}

/**
 * 创建工资率
 * 对应后端：CreateAsync
 */
export function create(data: WageRateCreate): Promise<WageRate> {
  return request({
    url: '/api/TaktWageRates',
    method: 'post',
    data
  })
}

/**
 * 更新工资率
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: WageRateUpdate): Promise<WageRate> {
  return request({
    url: `/api/TaktWageRates/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除工资率
 * 对应后端：DeleteAsync
 */
export function deleteWageRate(id: string): Promise<void> {
  return request({
    url: `/api/TaktWageRates/${id}`,
    method: 'delete'
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktWageRates/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入工资率
 * 对应后端：ImportAsync
 */
export function importWageRates(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktWageRates/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出工资率
 * 对应后端：ExportAsync；fileName 仅传名称不含后缀
 */
export function exportWageRates(
  query: WageRateQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktWageRates/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
