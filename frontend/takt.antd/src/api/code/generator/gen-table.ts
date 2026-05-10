// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/code/generator
// 文件名称：gen-table.ts
// 功能描述：GenTable API，对应后端 Takt.WebApi.Controllers.Code.Generator.TaktGenTables
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  GenTable,
  GenTableQuery,
  GenTableCreate,
  GenTableUpdate
} from '@/types/code/generator/gen-table'

// ========================================
// GenTable相关 API（按后端控制器顺序）
// ========================================
const genTableUrl = '/api/TaktGenTables';

/**
 * 获取GenTable列表（分页）
 * 对应后端：GetGenTableListAsync
 */
export function getGenTableList(params: GenTableQuery): Promise<TaktPagedResult<GenTable>> {
  return request({
    url: `${genTableUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取GenTable
 * 对应后端：GetGenTableByIdAsync
 */
export function getGenTableById(id: string): Promise<GenTable> {
  return request({
    url: `${genTableUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取GenTable选项列表（用于下拉框等）
 * 对应后端：GetGenTableOptionsAsync
 */
export function getGenTableOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${genTableUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建GenTable
 * 对应后端：CreateGenTableAsync
 */
export function createGenTable(data: GenTableCreate): Promise<GenTable> {
  return request({
    url: genTableUrl,
    method: 'post',
    data
  })
}

/**
 * 更新GenTable
 * 对应后端：UpdateGenTableAsync
 */
export function updateGenTable(id: string, data: GenTableUpdate): Promise<GenTable> {
  return request({
    url: `${genTableUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除GenTable（单条）
 * 对应后端：DeleteGenTableByIdAsync
 */
export function deleteGenTableById(id: string): Promise<void> {
  return request({
    url: `${genTableUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除GenTable
 * 对应后端：DeleteGenTableBatchAsync
 */
export function deleteGenTableBatch(ids: string[]): Promise<void> {
  return request({
    url: `${genTableUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetGenTableTemplateAsync；fileName 仅传名称不含后缀
 */
export function getGenTableTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${genTableUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入GenTable
 * 对应后端：ImportGenTableAsync
 */
export function importGenTableData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${genTableUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出GenTable
 * 对应后端：ExportGenTableAsync；fileName 仅传名称不含后缀
 */
export function exportGenTableData(query: GenTableQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${genTableUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
