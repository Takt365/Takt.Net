// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial
// 文件名称：accounting-title-change-log.ts
// 功能描述：AccountingTitleChangeLog API，对应后端 Takt.WebApi.Controllers.Accounting.Financial.TaktAccountingTitleChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AccountingTitleChangeLog,
  AccountingTitleChangeLogQuery,
  AccountingTitleChangeLogCreate,
  AccountingTitleChangeLogUpdate
} from '@/types/accounting/financial/accounting-title-change-log'

// ========================================
// AccountingTitleChangeLog相关 API（按后端控制器顺序）
// ========================================
const accountingTitleChangeLogUrl = '/api/TaktAccountingTitleChangeLogs';

/**
 * 获取AccountingTitleChangeLog列表（分页）
 * 对应后端：GetAccountingTitleChangeLogListAsync
 */
export function getAccountingTitleChangeLogList(params: AccountingTitleChangeLogQuery): Promise<TaktPagedResult<AccountingTitleChangeLog>> {
  return request({
    url: `${accountingTitleChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AccountingTitleChangeLog
 * 对应后端：GetAccountingTitleChangeLogByIdAsync
 */
export function getAccountingTitleChangeLogById(id: string): Promise<AccountingTitleChangeLog> {
  return request({
    url: `${accountingTitleChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AccountingTitleChangeLog选项列表（用于下拉框等）
 * 对应后端：GetAccountingTitleChangeLogOptionsAsync
 */
export function getAccountingTitleChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${accountingTitleChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AccountingTitleChangeLog
 * 对应后端：CreateAccountingTitleChangeLogAsync
 */
export function createAccountingTitleChangeLog(data: AccountingTitleChangeLogCreate): Promise<AccountingTitleChangeLog> {
  return request({
    url: accountingTitleChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AccountingTitleChangeLog
 * 对应后端：UpdateAccountingTitleChangeLogAsync
 */
export function updateAccountingTitleChangeLog(id: string, data: AccountingTitleChangeLogUpdate): Promise<AccountingTitleChangeLog> {
  return request({
    url: `${accountingTitleChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AccountingTitleChangeLog（单条）
 * 对应后端：DeleteAccountingTitleChangeLogByIdAsync
 */
export function deleteAccountingTitleChangeLogById(id: string): Promise<void> {
  return request({
    url: `${accountingTitleChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AccountingTitleChangeLog
 * 对应后端：DeleteAccountingTitleChangeLogBatchAsync
 */
export function deleteAccountingTitleChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${accountingTitleChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAccountingTitleChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAccountingTitleChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${accountingTitleChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AccountingTitleChangeLog
 * 对应后端：ImportAccountingTitleChangeLogAsync
 */
export function importAccountingTitleChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${accountingTitleChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AccountingTitleChangeLog
 * 对应后端：ExportAccountingTitleChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportAccountingTitleChangeLogData(query: AccountingTitleChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${accountingTitleChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
