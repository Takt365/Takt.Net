// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/i18n
// 文件名称：translation.ts
// 功能描述：Translation API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.I18n.TaktTranslations
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Translation,
  TranslationQuery,
  TranslationCreate,
  TranslationUpdate,
  TranslationSort
} from '@/types/routine/tasks/i18n/translation'

// ========================================
// Translation相关 API（按后端控制器顺序）
// ========================================
const translationUrl = '/api/TaktTranslations';

/**
 * 获取Translation列表（分页）
 * 对应后端：GetTranslationListAsync
 */
export function getTranslationList(params: TranslationQuery): Promise<TaktPagedResult<Translation>> {
  return request({
    url: `${translationUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Translation
 * 对应后端：GetTranslationByIdAsync
 */
export function getTranslationById(id: string): Promise<Translation> {
  return request({
    url: `${translationUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Translation选项列表（用于下拉框等）
 * 对应后端：GetTranslationOptionsAsync
 */
export function getTranslationOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${translationUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Translation
 * 对应后端：CreateTranslationAsync
 */
export function createTranslation(data: TranslationCreate): Promise<Translation> {
  return request({
    url: translationUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Translation
 * 对应后端：UpdateTranslationAsync
 */
export function updateTranslation(id: string, data: TranslationUpdate): Promise<Translation> {
  return request({
    url: `${translationUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Translation（单条）
 * 对应后端：DeleteTranslationByIdAsync
 */
export function deleteTranslationById(id: string): Promise<void> {
  return request({
    url: `${translationUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Translation
 * 对应后端：DeleteTranslationBatchAsync
 */
export function deleteTranslationBatch(ids: string[]): Promise<void> {
  return request({
    url: `${translationUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Translation排序
 * 对应后端：UpdateTranslationSortAsync
 */
export function updateTranslationSort(data: TranslationSort): Promise<TranslationSort> {
  return request({
    url: `${translationUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTranslationTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTranslationTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${translationUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Translation
 * 对应后端：ImportTranslationAsync
 */
export function importTranslationData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${translationUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Translation
 * 对应后端：ExportTranslationAsync；fileName 仅传名称不含后缀
 */
export function exportTranslationData(query: TranslationQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${translationUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
