// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/i18n
// 文件名称：language.ts
// 功能描述：Language API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.I18n.TaktLanguages
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Language,
  LanguageQuery,
  LanguageCreate,
  LanguageUpdate,
  LanguageStatus,
  LanguageSort
} from '@/types/routine/tasks/i18n/language'

// ========================================
// Language相关 API（按后端控制器顺序）
// ========================================
const languageUrl = '/api/TaktLanguages';

/**
 * 获取Language列表（分页）
 * 对应后端：GetLanguageListAsync
 */
export function getLanguageList(params: LanguageQuery): Promise<TaktPagedResult<Language>> {
  return request({
    url: `${languageUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Language
 * 对应后端：GetLanguageByIdAsync
 */
export function getLanguageById(id: string): Promise<Language> {
  return request({
    url: `${languageUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Language选项列表（用于下拉框等）
 * 对应后端：GetLanguageOptionsAsync
 */
export function getLanguageOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${languageUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Language
 * 对应后端：CreateLanguageAsync
 */
export function createLanguage(data: LanguageCreate): Promise<Language> {
  return request({
    url: languageUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Language
 * 对应后端：UpdateLanguageAsync
 */
export function updateLanguage(id: string, data: LanguageUpdate): Promise<Language> {
  return request({
    url: `${languageUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Language（单条）
 * 对应后端：DeleteLanguageByIdAsync
 */
export function deleteLanguageById(id: string): Promise<void> {
  return request({
    url: `${languageUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Language
 * 对应后端：DeleteLanguageBatchAsync
 */
export function deleteLanguageBatch(ids: string[]): Promise<void> {
  return request({
    url: `${languageUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Language状态
 * 对应后端：UpdateLanguageStatusAsync
 */
export function updateLanguageStatus(data: LanguageStatus): Promise<LanguageStatus> {
  return request({
    url: `${languageUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新Language排序
 * 对应后端：UpdateLanguageSortAsync
 */
export function updateLanguageSort(data: LanguageSort): Promise<LanguageSort> {
  return request({
    url: `${languageUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetLanguageTemplateAsync；fileName 仅传名称不含后缀
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
 * 导入Language
 * 对应后端：ImportLanguageAsync
 */
export function importLanguageData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${languageUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Language
 * 对应后端：ExportLanguageAsync；fileName 仅传名称不含后缀
 */
export function exportLanguageData(query: LanguageQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${languageUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
