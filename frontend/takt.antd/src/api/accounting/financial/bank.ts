// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial/bank
// 文件名称：bank.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：银行相关 API，对应后端 TaktBanksController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Bank,
  BankQuery,
  BankCreate,
  BankUpdate,
  BankStatus
} from '@/types/accounting/financial/bank'

/**
 * 获取银行列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: BankQuery): Promise<TaktPagedResult<Bank>> {
  return request({
    url: '/api/TaktBanks/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取银行
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<Bank> {
  return request({
    url: `/api/TaktBanks/${id}`,
    method: 'get'
  })
}

/**
 * 获取银行选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync；companyCode 可选，筛选该公司下的银行
 */
export function getOptions(companyCode?: string): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktBanks/options',
    method: 'get',
    params: companyCode != null ? { companyCode } : undefined
  })
}

/**
 * 创建银行
 * 对应后端：CreateAsync
 */
export function create(data: BankCreate): Promise<Bank> {
  return request({
    url: '/api/TaktBanks',
    method: 'post',
    data
  })
}

/**
 * 更新银行
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: BankUpdate): Promise<Bank> {
  return request({
    url: `/api/TaktBanks/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除银行
 * 对应后端：DeleteAsync
 */
export function deleteBank(id: string): Promise<void> {
  return request({
    url: `/api/TaktBanks/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除银行
 * 对应后端：DeleteBatchAsync；ids 为银行ID（传 string 或 number 均可，请求体为 number[]）
 */
export function deleteBatch(ids: string[] | number[]): Promise<void> {
  const body = ids.map((id) => Number(id))
  return request({
    url: '/api/TaktBanks/batch',
    method: 'delete',
    data: body
  })
}

/**
 * 更新银行状态
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: BankStatus): Promise<Bank> {
  return request({
    url: '/api/TaktBanks/status',
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
    url: '/api/TaktBanks/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入银行
 * 对应后端：ImportAsync
 */
export function importBanks(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktBanks/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出银行
 * 对应后端：ExportAsync；fileName 仅传名称不含后缀
 */
export function exportBanks(
  query: BankQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktBanks/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
