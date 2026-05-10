// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial
// 文件名称：accounting-title.ts
// 功能描述：AccountingTitle API，对应后端 Takt.WebApi.Controllers.Accounting.Financial.TaktAccountingTitles
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import type {
  AccountingTitle,
  AccountingTitleTree,
  AccountingTitleQuery,
  AccountingTitleCreate,
  AccountingTitleUpdate,
  AccountingTitleSort
} from '@/types/accounting/financial/accounting-title'

// ========================================
// AccountingTitle相关 API（按后端控制器顺序）
// ========================================
const accountingTitleUrl = '/api/TaktAccountingTitles';

/**
 * 获取AccountingTitle列表（分页）
 * 对应后端：GetAccountingTitleListAsync
 */
export function getAccountingTitleList(params: AccountingTitleQuery): Promise<TaktPagedResult<AccountingTitle>> {
  return request({
    url: `${accountingTitleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AccountingTitle
 * 对应后端：GetAccountingTitleByIdAsync
 */
export function getAccountingTitleById(id: string): Promise<AccountingTitle> {
  return request({
    url: `${accountingTitleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AccountingTitle选项列表（用于下拉框等）
 * 对应后端：GetAccountingTitleOptionsAsync
 */
export function getAccountingTitleOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${accountingTitleUrl}/options`,
    method: 'get'
  })
}

/**
 * 获取AccountingTitle树形选项列表（用于树形下拉框等）
 * 对应后端：GetAccountingTitleTreeOptionsAsync
 */
export function getAccountingTitleTreeOptions(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: `${accountingTitleUrl}/tree-options`,
    method: 'get'
  })
}

/**
 * 获取AccountingTitle树形列表
 * 对应后端：GetAccountingTitleTreeAsync
 */
export function getAccountingTitleTree(parentId: number = 0, includeDisabled: boolean = false): Promise<AccountingTitleTree[]> {
  return request({
    url: `${accountingTitleUrl}/tree`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 获取AccountingTitle子节点列表
 * 对应后端：GetAccountingTitleChildrenAsync
 */
export function getAccountingTitleChildren(parentId: number, includeDisabled: boolean = false): Promise<AccountingTitle[]> {
  return request({
    url: `${accountingTitleUrl}/children`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 创建AccountingTitle
 * 对应后端：CreateAccountingTitleAsync
 */
export function createAccountingTitle(data: AccountingTitleCreate): Promise<AccountingTitle> {
  return request({
    url: accountingTitleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AccountingTitle
 * 对应后端：UpdateAccountingTitleAsync
 */
export function updateAccountingTitle(id: string, data: AccountingTitleUpdate): Promise<AccountingTitle> {
  return request({
    url: `${accountingTitleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AccountingTitle（单条）
 * 对应后端：DeleteAccountingTitleByIdAsync
 */
export function deleteAccountingTitleById(id: string): Promise<void> {
  return request({
    url: `${accountingTitleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AccountingTitle
 * 对应后端：DeleteAccountingTitleBatchAsync
 */
export function deleteAccountingTitleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${accountingTitleUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新AccountingTitle排序
 * 对应后端：UpdateAccountingTitleSortAsync
 */
export function updateAccountingTitleSort(data: AccountingTitleSort): Promise<AccountingTitleSort> {
  return request({
    url: `${accountingTitleUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAccountingTitleTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAccountingTitleTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${accountingTitleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AccountingTitle
 * 对应后端：ImportAccountingTitleAsync
 */
export function importAccountingTitleData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${accountingTitleUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AccountingTitle
 * 对应后端：ExportAccountingTitleAsync；fileName 仅传名称不含后缀
 */
export function exportAccountingTitleData(query: AccountingTitleQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${accountingTitleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
