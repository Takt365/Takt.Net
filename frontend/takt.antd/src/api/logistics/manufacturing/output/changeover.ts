// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/output
// 文件名称：changeover.ts
// 功能描述：Changeover API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Output.TaktChangeovers
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Changeover,
  ChangeoverQuery,
  ChangeoverCreate,
  ChangeoverUpdate
} from '@/types/logistics/manufacturing/output/changeover'

// ========================================
// Changeover相关 API（按后端控制器顺序）
// ========================================
const changeoverUrl = '/api/TaktChangeovers';

/**
 * 获取Changeover列表（分页）
 * 对应后端：GetChangeoverListAsync
 */
export function getChangeoverList(params: ChangeoverQuery): Promise<TaktPagedResult<Changeover>> {
  return request({
    url: `${changeoverUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Changeover
 * 对应后端：GetChangeoverByIdAsync
 */
export function getChangeoverById(id: string): Promise<Changeover> {
  return request({
    url: `${changeoverUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Changeover选项列表（用于下拉框等）
 * 对应后端：GetChangeoverOptionsAsync
 */
export function getChangeoverOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${changeoverUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Changeover
 * 对应后端：CreateChangeoverAsync
 */
export function createChangeover(data: ChangeoverCreate): Promise<Changeover> {
  return request({
    url: changeoverUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Changeover
 * 对应后端：UpdateChangeoverAsync
 */
export function updateChangeover(id: string, data: ChangeoverUpdate): Promise<Changeover> {
  return request({
    url: `${changeoverUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Changeover（单条）
 * 对应后端：DeleteChangeoverByIdAsync
 */
export function deleteChangeoverById(id: string): Promise<void> {
  return request({
    url: `${changeoverUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Changeover
 * 对应后端：DeleteChangeoverBatchAsync
 */
export function deleteChangeoverBatch(ids: string[]): Promise<void> {
  return request({
    url: `${changeoverUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetChangeoverTemplateAsync；fileName 仅传名称不含后缀
 */
export function getChangeoverTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${changeoverUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Changeover
 * 对应后端：ImportChangeoverAsync
 */
export function importChangeoverData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${changeoverUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Changeover
 * 对应后端：ExportChangeoverAsync；fileName 仅传名称不含后缀
 */
export function exportChangeoverData(query: ChangeoverQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${changeoverUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
