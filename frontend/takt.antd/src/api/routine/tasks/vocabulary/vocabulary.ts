// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/vocabulary
// 文件名称：vocabulary.ts
// 功能描述：Vocabulary API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.Vocabulary.TaktVocabularys
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Vocabulary,
  VocabularyQuery,
  VocabularyCreate,
  VocabularyUpdate,
  VocabularyStatus
} from '@/types/routine/tasks/vocabulary/vocabulary'

// ========================================
// Vocabulary相关 API（按后端控制器顺序）
// ========================================
const vocabularyUrl = '/api/TaktVocabularys';

/**
 * 获取Vocabulary列表（分页）
 * 对应后端：GetVocabularyListAsync
 */
export function getVocabularyList(params: VocabularyQuery): Promise<TaktPagedResult<Vocabulary>> {
  return request({
    url: `${vocabularyUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Vocabulary
 * 对应后端：GetVocabularyByIdAsync
 */
export function getVocabularyById(id: string): Promise<Vocabulary> {
  return request({
    url: `${vocabularyUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Vocabulary选项列表（用于下拉框等）
 * 对应后端：GetVocabularyOptionsAsync
 */
export function getVocabularyOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${vocabularyUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Vocabulary
 * 对应后端：CreateVocabularyAsync
 */
export function createVocabulary(data: VocabularyCreate): Promise<Vocabulary> {
  return request({
    url: vocabularyUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Vocabulary
 * 对应后端：UpdateVocabularyAsync
 */
export function updateVocabulary(id: string, data: VocabularyUpdate): Promise<Vocabulary> {
  return request({
    url: `${vocabularyUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Vocabulary（单条）
 * 对应后端：DeleteVocabularyByIdAsync
 */
export function deleteVocabularyById(id: string): Promise<void> {
  return request({
    url: `${vocabularyUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Vocabulary
 * 对应后端：DeleteVocabularyBatchAsync
 */
export function deleteVocabularyBatch(ids: string[]): Promise<void> {
  return request({
    url: `${vocabularyUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Vocabulary状态
 * 对应后端：UpdateVocabularyStatusAsync
 */
export function updateVocabularyStatus(data: VocabularyStatus): Promise<VocabularyStatus> {
  return request({
    url: `${vocabularyUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetVocabularyTemplateAsync；fileName 仅传名称不含后缀
 */
export function getVocabularyTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${vocabularyUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Vocabulary
 * 对应后端：ImportVocabularyAsync
 */
export function importVocabularyData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${vocabularyUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Vocabulary
 * 对应后端：ExportVocabularyAsync；fileName 仅传名称不含后缀
 */
export function exportVocabularyData(query: VocabularyQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${vocabularyUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
