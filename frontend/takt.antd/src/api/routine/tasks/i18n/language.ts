// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/routine/i18n/language
// 文件名称：language.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：语言管理 API，对应后端 TaktLanguagesController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '../../../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Language,
  LanguageQuery,
  LanguageCreate,
  LanguageUpdate,
  LanguageStatus
} from '@/types/routine/tasks/i18n/language'

const languageUrl = '/api/TaktLanguages'

/**
 * 获取语言列表（分页）
 * @param params 查询参数
 * @returns 分页结果
 */
export function getLanguageList(params: LanguageQuery): Promise<TaktPagedResult<Language>> {
  return request({
    url: `${languageUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取语言
 * @param id 语言ID
 * @returns 语言信息
 */
export function getLanguageById(id: string): Promise<Language> {
  return request({
    url: `${languageUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取语言选项列表（用于下拉框等）
 * @returns 语言选项列表
 */
export function getLanguageOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${languageUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建语言
 * @param data 创建语言数据
 * @returns 语言信息
 */
export function createLanguage(data: LanguageCreate): Promise<Language> {
  return request({
    url: languageUrl,
    method: 'post',
    data
  })
}

/**
 * 更新语言
 * @param id 语言ID
 * @param data 更新语言数据
 * @returns 语言信息
 */
export function updateLanguage(id: string, data: LanguageUpdate): Promise<Language> {
  return request({
    url: `${languageUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除语言
 * @param id 语言ID
 * @returns 无内容
 */
export function deleteLanguageById(id: string): Promise<void> {
  return request({
    url: `${languageUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除语言
 * @param ids 语言ID列表
 * @returns 无内容
 */
export function deleteLanguageBatch(ids: string[]): Promise<void> {
  return request({
    url: `${languageUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新语言状态
 * @param data 语言状态数据
 * @returns 语言信息
 */
export function updateLanguageStatus(data: LanguageStatus): Promise<Language> {
  return request({
    url: `${languageUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * @param sheetName 工作表名称
 * @param fileName 文件名
 * @returns Excel模板文件（Blob）
 */
export function getLanguageTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${languageUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入语言
 * @param file Excel文件
 * @param sheetName 工作表名称
 * @returns 导入结果
 */
export function importLanguageData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: `${languageUrl}/import`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出语言
 * @param query 语言查询条件
 * @param sheetName 工作表名称
 * @param fileName 文件名
 * @returns Excel文件（Blob）
 */
export function exportLanguageData(
  query: LanguageQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${languageUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
