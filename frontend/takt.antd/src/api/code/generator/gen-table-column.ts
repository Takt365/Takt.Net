// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/code/generator
// 文件名称：gen-table-column.ts
// 功能描述：GenTableColumn API，对应后端 Takt.WebApi.Controllers.Code.Generator.TaktGenTableColumns
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  GenTableColumn,
  GenTableColumnQuery,
  GenTableColumnCreate,
  GenTableColumnUpdate,
  GenTableColumnSort
} from '@/types/code/generator/gen-table-column'

// ========================================
// GenTableColumn相关 API（按后端控制器顺序）
// ========================================
const genTableColumnUrl = '/api/TaktGenTableColumns';

/**
 * 获取GenTableColumn列表（分页）
 * 对应后端：GetGenTableColumnListAsync
 */
export function getGenTableColumnList(params: GenTableColumnQuery): Promise<TaktPagedResult<GenTableColumn>> {
  return request({
    url: `${genTableColumnUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取GenTableColumn
 * 对应后端：GetGenTableColumnByIdAsync
 */
export function getGenTableColumnById(id: string): Promise<GenTableColumn> {
  return request({
    url: `${genTableColumnUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取GenTableColumn选项列表（用于下拉框等）
 * 对应后端：GetGenTableColumnOptionsAsync
 */
export function getGenTableColumnOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${genTableColumnUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建GenTableColumn
 * 对应后端：CreateGenTableColumnAsync
 */
export function createGenTableColumn(data: GenTableColumnCreate): Promise<GenTableColumn> {
  return request({
    url: genTableColumnUrl,
    method: 'post',
    data
  })
}

/**
 * 更新GenTableColumn
 * 对应后端：UpdateGenTableColumnAsync
 */
export function updateGenTableColumn(id: string, data: GenTableColumnUpdate): Promise<GenTableColumn> {
  return request({
    url: `${genTableColumnUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除GenTableColumn（单条）
 * 对应后端：DeleteGenTableColumnByIdAsync
 */
export function deleteGenTableColumnById(id: string): Promise<void> {
  return request({
    url: `${genTableColumnUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除GenTableColumn
 * 对应后端：DeleteGenTableColumnBatchAsync
 */
export function deleteGenTableColumnBatch(ids: string[]): Promise<void> {
  return request({
    url: `${genTableColumnUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新GenTableColumn排序
 * 对应后端：UpdateGenTableColumnSortAsync
 */
export function updateGenTableColumnSort(data: GenTableColumnSort): Promise<GenTableColumnSort> {
  return request({
    url: `${genTableColumnUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetGenTableColumnTemplateAsync；fileName 仅传名称不含后缀
 */
export function getGenTableColumnTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${genTableColumnUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入GenTableColumn
 * 对应后端：ImportGenTableColumnAsync
 */
export function importGenTableColumnData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${genTableColumnUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出GenTableColumn
 * 对应后端：ExportGenTableColumnAsync；fileName 仅传名称不含后缀
 */
export function exportGenTableColumnData(query: GenTableColumnQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${genTableColumnUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
