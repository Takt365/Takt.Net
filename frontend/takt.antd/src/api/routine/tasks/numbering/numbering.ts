// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/numbering
// 文件名称：numbering.ts
// 功能描述：Numbering API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.Numbering.TaktNumberings
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Numbering,
  NumberingQuery,
  NumberingCreate,
  NumberingUpdate,
  NumberingSort
} from '@/types/routine/tasks/numbering/numbering'

// ========================================
// Numbering相关 API（按后端控制器顺序）
// ========================================
const numberingUrl = '/api/TaktNumberings';

/**
 * 获取Numbering列表（分页）
 * 对应后端：GetNumberingListAsync
 */
export function getNumberingList(params: NumberingQuery): Promise<TaktPagedResult<Numbering>> {
  return request({
    url: `${numberingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Numbering
 * 对应后端：GetNumberingByIdAsync
 */
export function getNumberingById(id: string): Promise<Numbering> {
  return request({
    url: `${numberingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Numbering选项列表（用于下拉框等）
 * 对应后端：GetNumberingOptionsAsync
 */
export function getNumberingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${numberingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Numbering
 * 对应后端：CreateNumberingAsync
 */
export function createNumbering(data: NumberingCreate): Promise<Numbering> {
  return request({
    url: numberingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Numbering
 * 对应后端：UpdateNumberingAsync
 */
export function updateNumbering(id: string, data: NumberingUpdate): Promise<Numbering> {
  return request({
    url: `${numberingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Numbering（单条）
 * 对应后端：DeleteNumberingByIdAsync
 */
export function deleteNumberingById(id: string): Promise<void> {
  return request({
    url: `${numberingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Numbering
 * 对应后端：DeleteNumberingBatchAsync
 */
export function deleteNumberingBatch(ids: string[]): Promise<void> {
  return request({
    url: `${numberingUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Numbering排序
 * 对应后端：UpdateNumberingSortAsync
 */
export function updateNumberingSort(data: NumberingSort): Promise<NumberingSort> {
  return request({
    url: `${numberingUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetNumberingTemplateAsync；fileName 仅传名称不含后缀
 */
export function getNumberingTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${numberingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Numbering
 * 对应后端：ImportNumberingAsync
 */
export function importNumberingData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${numberingUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Numbering
 * 对应后端：ExportNumberingAsync；fileName 仅传名称不含后缀
 */
export function exportNumberingData(query: NumberingQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${numberingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
