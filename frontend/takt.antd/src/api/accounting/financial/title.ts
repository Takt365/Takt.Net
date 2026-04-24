// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial/title
// 文件名称：title.ts
// 功能描述：会计科目 API，与后端 TaktAccountingTitlesController 路由与方法一一对应
// 路由基址：GET/POST/PUT/DELETE `/api/TaktAccountingTitles/...`
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktTreeSelectOption } from '@/types/common'
import type {
  AccountingTitle,
  AccountingTitleCreate,
  AccountingTitleQuery,
  AccountingTitleStatus,
  AccountingTitleTree,
  AccountingTitleUpdate
} from '@/types/accounting/financial/title'

const titleUrl = '/api/TaktAccountingTitles'

// ---------- 查询 ----------

/**
 * 获取会计科目列表（分页）
 * 对应后端应用服务：GetAccountingTitleListAsync — GET `list`，[FromQuery] TaktAccountingTitleQueryDto
 */
export function getAccountingTitleList(params: AccountingTitleQuery): Promise<TaktPagedResult<AccountingTitle>> {
  return request({
    url: `${titleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据 ID 获取会计科目
 * 对应后端应用服务：GetAccountingTitleByIdAsync — GET `{id}`
 */
export function getAccountingTitleById(id: string): Promise<AccountingTitle> {
  return request({
    url: `${titleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取会计科目树形选项（树形下拉）
 * 对应后端：GetTreeOptionsAsync — GET `tree-options`
 */
export function getAccountingTitleTreeOptions(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: `${titleUrl}/tree-options`,
    method: 'get'
  })
}

/**
 * 获取会计科目树形列表
 * 对应后端应用服务：GetAccountingTitleTreeAsync（TaktAccountingTitlesController.GetTreeAsync）— GET `tree`，[FromQuery] parentId、includeDisabled
 */
export function getAccountingTitleTree(
  parentId: number = 0,
  includeDisabled: boolean = false
): Promise<AccountingTitleTree[]> {
  return request({
    url: `${titleUrl}/tree`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 获取指定父级下的子科目列表
 * 对应后端应用服务：GetAccountingTitleChildrenAsync — GET `children`，[FromQuery] parentId、includeDisabled
 */
export function getAccountingTitleChildren(
  parentId: number,
  includeDisabled: boolean = false
): Promise<AccountingTitle[]> {
  return request({
    url: `${titleUrl}/children`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

// ---------- 写操作 ----------

/**
 * 创建会计科目
 * 对应后端应用服务：CreateAccountingTitleAsync — POST ``（根路径），[FromBody] TaktAccountingTitleCreateDto
 */
export function createAccountingTitle(data: AccountingTitleCreate): Promise<AccountingTitle> {
  return request({
    url: titleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新会计科目
 * 对应后端应用服务：UpdateAccountingTitleAsync — PUT `{id}`，[FromBody] TaktAccountingTitleUpdateDto
 */
export function updateAccountingTitle(id: string, data: AccountingTitleUpdate): Promise<AccountingTitle> {
  return request({
    url: `${titleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除会计科目
 * 对应后端应用服务：DeleteAccountingTitleByIdAsync — DELETE `{id}`
 */
export function deleteAccountingTitleById(id: string): Promise<void> {
  return request({
    url: `${titleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 更新会计科目状态
 * 对应后端应用服务：UpdateAccountingTitleStatusAsync — PUT `status`，[FromBody] TaktAccountingTitleStatusDto
 */
export function updateAccountingTitleStatus(data: AccountingTitleStatus): Promise<AccountingTitle> {
  return request({
    url: `${titleUrl}/status`,
    method: 'put',
    data
  })
}

// ---------- 导入导出 ----------

/**
 * 获取导入模板
 * 对应后端应用服务：GetAccountingTitleTemplateAsync — GET `template`，[FromQuery] sheetName、fileName
 */
export function getAccountingTitleTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${titleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入会计科目
 * 对应后端应用服务：ImportAccountingTitleAsync — POST `import`，[FromForm] file、sheetName
 */
export function importAccountingTitleData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${titleUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出会计科目
 * 对应后端应用服务：ExportAccountingTitleAsync — POST `export`，[FromBody] TaktAccountingTitleQueryDto，[FromQuery] sheetName、fileName
 */
export function exportAccountingTitleData(
  query: AccountingTitleQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${titleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
