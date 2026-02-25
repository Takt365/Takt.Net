// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial/company
// 文件名称：company.ts
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：公司相关 API，对应后端 TaktCompaniesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Company,
  CompanyQuery,
  CompanyCreate,
  CompanyUpdate,
  CompanyStatus
} from '@/types/accounting/financial/company'

/**
 * 获取公司列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: CompanyQuery): Promise<TaktPagedResult<Company>> {
  return request({
    url: '/api/TaktCompanies/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取公司
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<Company> {
  return request({
    url: `/api/TaktCompanies/${id}`,
    method: 'get'
  })
}

/**
 * 获取公司选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktCompanies/options',
    method: 'get'
  })
}

/**
 * 创建公司
 * 对应后端：CreateAsync
 */
export function create(data: CompanyCreate): Promise<Company> {
  return request({
    url: '/api/TaktCompanies',
    method: 'post',
    data
  })
}

/**
 * 更新公司
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: CompanyUpdate): Promise<Company> {
  return request({
    url: `/api/TaktCompanies/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除公司
 * 对应后端：DeleteAsync
 */
export function deleteCompany(id: string): Promise<void> {
  return request({
    url: `/api/TaktCompanies/${id}`,
    method: 'delete'
  })
}

/**
 * 更新公司状态
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: CompanyStatus): Promise<Company> {
  return request({
    url: '/api/TaktCompanies/status',
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
    url: '/api/TaktCompanies/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入公司
 * 对应后端：ImportAsync
 */
export function importCompanies(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktCompanies/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出公司
 * 对应后端：ExportAsync；fileName 仅传名称不含后缀
 */
export function exportCompanies(
  query: CompanyQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktCompanies/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
