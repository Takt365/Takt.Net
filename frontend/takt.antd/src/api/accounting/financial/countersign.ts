// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial
// 文件名称：countersign.ts
// 功能描述：Countersign API，对应后端 Takt.WebApi.Controllers.Accounting.Financial.TaktCountersigns
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Countersign,
  CountersignQuery,
  CountersignCreate,
  CountersignUpdate,
  CountersignStatus
} from '@/types/accounting/financial/countersign'

// ========================================
// Countersign相关 API（按后端控制器顺序）
// ========================================
const countersignUrl = '/api/TaktCountersigns';

/**
 * 获取Countersign列表（分页）
 * 对应后端：GetCountersignListAsync
 */
export function getCountersignList(params: CountersignQuery): Promise<TaktPagedResult<Countersign>> {
  return request({
    url: `${countersignUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Countersign
 * 对应后端：GetCountersignByIdAsync
 */
export function getCountersignById(id: string): Promise<Countersign> {
  return request({
    url: `${countersignUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Countersign选项列表（用于下拉框等）
 * 对应后端：GetCountersignOptionsAsync
 */
export function getCountersignOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${countersignUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Countersign
 * 对应后端：CreateCountersignAsync
 */
export function createCountersign(data: CountersignCreate): Promise<Countersign> {
  return request({
    url: countersignUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Countersign
 * 对应后端：UpdateCountersignAsync
 */
export function updateCountersign(id: string, data: CountersignUpdate): Promise<Countersign> {
  return request({
    url: `${countersignUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Countersign（单条）
 * 对应后端：DeleteCountersignByIdAsync
 */
export function deleteCountersignById(id: string): Promise<void> {
  return request({
    url: `${countersignUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Countersign
 * 对应后端：DeleteCountersignBatchAsync
 */
export function deleteCountersignBatch(ids: string[]): Promise<void> {
  return request({
    url: `${countersignUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Countersign状态
 * 对应后端：UpdateCountersignStatusAsync
 */
export function updateCountersignStatus(data: CountersignStatus): Promise<CountersignStatus> {
  return request({
    url: `${countersignUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCountersignTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCountersignTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${countersignUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Countersign
 * 对应后端：ImportCountersignAsync
 */
export function importCountersignData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${countersignUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Countersign
 * 对应后端：ExportCountersignAsync；fileName 仅传名称不含后缀
 */
export function exportCountersignData(query: CountersignQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${countersignUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
