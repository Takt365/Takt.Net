// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/bom
// 文件名称：standard-operation-time.ts
// 功能描述：StandardOperationTime API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Bom.TaktStandardOperationTimes
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  StandardOperationTime,
  StandardOperationTimeQuery,
  StandardOperationTimeCreate,
  StandardOperationTimeUpdate
} from '@/types/logistics/manufacturing/bom/standard-operation-time'

// ========================================
// StandardOperationTime相关 API（按后端控制器顺序）
// ========================================
const standardOperationTimeUrl = '/api/TaktStandardOperationTimes';

/**
 * 获取StandardOperationTime列表（分页）
 * 对应后端：GetStandardOperationTimeListAsync
 */
export function getStandardOperationTimeList(params: StandardOperationTimeQuery): Promise<TaktPagedResult<StandardOperationTime>> {
  return request({
    url: `${standardOperationTimeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取StandardOperationTime
 * 对应后端：GetStandardOperationTimeByIdAsync
 */
export function getStandardOperationTimeById(id: string): Promise<StandardOperationTime> {
  return request({
    url: `${standardOperationTimeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取StandardOperationTime选项列表（用于下拉框等）
 * 对应后端：GetStandardOperationTimeOptionsAsync
 */
export function getStandardOperationTimeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${standardOperationTimeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建StandardOperationTime
 * 对应后端：CreateStandardOperationTimeAsync
 */
export function createStandardOperationTime(data: StandardOperationTimeCreate): Promise<StandardOperationTime> {
  return request({
    url: standardOperationTimeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新StandardOperationTime
 * 对应后端：UpdateStandardOperationTimeAsync
 */
export function updateStandardOperationTime(id: string, data: StandardOperationTimeUpdate): Promise<StandardOperationTime> {
  return request({
    url: `${standardOperationTimeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除StandardOperationTime（单条）
 * 对应后端：DeleteStandardOperationTimeByIdAsync
 */
export function deleteStandardOperationTimeById(id: string): Promise<void> {
  return request({
    url: `${standardOperationTimeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除StandardOperationTime
 * 对应后端：DeleteStandardOperationTimeBatchAsync
 */
export function deleteStandardOperationTimeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${standardOperationTimeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetStandardOperationTimeTemplateAsync；fileName 仅传名称不含后缀
 */
export function getStandardOperationTimeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${standardOperationTimeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入StandardOperationTime
 * 对应后端：ImportStandardOperationTimeAsync
 */
export function importStandardOperationTimeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${standardOperationTimeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出StandardOperationTime
 * 对应后端：ExportStandardOperationTimeAsync；fileName 仅传名称不含后缀
 */
export function exportStandardOperationTimeData(query: StandardOperationTimeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${standardOperationTimeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
