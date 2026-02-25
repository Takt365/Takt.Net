// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial/account-title
// 文件名称：account-title.ts
// 功能描述：会计科目相关 API，对应后端 TaktAccountTitlesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult } from '@/types/common'
import type {
  AccountTitle,
  AccountTitleTree,
  AccountTitleTreeOption,
  AccountTitleQuery,
  AccountTitleCreate,
  AccountTitleUpdate,
  AccountTitleStatus
} from '@/types/accounting/financial/account-title'

const BASE = '/api/TaktAccountTitles'

/**
 * 获取会计科目列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: AccountTitleQuery): Promise<TaktPagedResult<AccountTitle>> {
  return request({
    url: `${BASE}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取会计科目
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<AccountTitle> {
  return request({
    url: `${BASE}/${id}`,
    method: 'get'
  })
}

/**
 * 获取会计科目树形选项（用于树形下拉等）
 * 对应后端：GetTreeOptionsAsync
 */
export function getTreeOptions(): Promise<AccountTitleTreeOption[]> {
  return request({
    url: `${BASE}/tree-options`,
    method: 'get'
  })
}

/**
 * 获取会计科目树形列表
 * 对应后端：GetTreeAsync
 */
export function getTree(parentId: string | number = 0, includeDisabled = false): Promise<AccountTitleTree[]> {
  return request({
    url: `${BASE}/tree`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 获取会计科目子节点列表
 * 对应后端：GetChildrenAsync
 */
export function getChildren(parentId: string, includeDisabled = false): Promise<AccountTitle[]> {
  return request({
    url: `${BASE}/children`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 创建会计科目
 * 对应后端：CreateAsync
 */
export function create(data: AccountTitleCreate): Promise<AccountTitle> {
  return request({
    url: BASE,
    method: 'post',
    data
  })
}

/**
 * 更新会计科目
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: AccountTitleUpdate): Promise<AccountTitle> {
  return request({
    url: `${BASE}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除会计科目
 * 对应后端：DeleteAsync
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `${BASE}/${id}`,
    method: 'delete'
  })
}

/**
 * 更新会计科目状态
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: AccountTitleStatus): Promise<AccountTitle> {
  return request({
    url: `${BASE}/status`,
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
    url: `${BASE}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入会计科目
 * 对应后端：ImportAsync
 */
export function importTitles(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: `${BASE}/import`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出会计科目
 * 对应后端：ExportAsync；fileName 仅传名称不含后缀
 */
export function exportTitles(
  query: AccountTitleQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${BASE}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
