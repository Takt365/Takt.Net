// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/engineering-change
// 文件名称：ec.ts
// 功能描述：Ec API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange.TaktEcs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Ec,
  EcQuery,
  EcCreate,
  EcUpdate,
  EcStatus
} from '@/types/logistics/manufacturing/engineering-change/ec'

// ========================================
// Ec相关 API（按后端控制器顺序）
// ========================================
const ecUrl = '/api/TaktEcs';

/**
 * 获取Ec列表（分页）
 * 对应后端：GetEcListAsync
 */
export function getEcList(params: EcQuery): Promise<TaktPagedResult<Ec>> {
  return request({
    url: `${ecUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Ec
 * 对应后端：GetEcByIdAsync
 */
export function getEcById(id: string): Promise<Ec> {
  return request({
    url: `${ecUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Ec选项列表（用于下拉框等）
 * 对应后端：GetEcOptionsAsync
 */
export function getEcOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ecUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Ec
 * 对应后端：CreateEcAsync
 */
export function createEc(data: EcCreate): Promise<Ec> {
  return request({
    url: ecUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Ec
 * 对应后端：UpdateEcAsync
 */
export function updateEc(id: string, data: EcUpdate): Promise<Ec> {
  return request({
    url: `${ecUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Ec（单条）
 * 对应后端：DeleteEcByIdAsync
 */
export function deleteEcById(id: string): Promise<void> {
  return request({
    url: `${ecUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Ec
 * 对应后端：DeleteEcBatchAsync
 */
export function deleteEcBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ecUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Ec状态
 * 对应后端：UpdateEcStatusAsync
 */
export function updateEcStatus(data: EcStatus): Promise<EcStatus> {
  return request({
    url: `${ecUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEcTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEcTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ecUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Ec
 * 对应后端：ImportEcAsync
 */
export function importEcData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ecUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Ec
 * 对应后端：ExportEcAsync；fileName 仅传名称不含后缀
 */
export function exportEcData(query: EcQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ecUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
