// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/engineering-change
// 文件名称：ec-detail.ts
// 功能描述：EcDetail API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange.TaktEcDetails
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EcDetail,
  EcDetailQuery,
  EcDetailCreate,
  EcDetailUpdate
} from '@/types/logistics/manufacturing/engineering-change/ec-detail'

// ========================================
// EcDetail相关 API（按后端控制器顺序）
// ========================================
const ecDetailUrl = '/api/TaktEcDetails';

/**
 * 获取EcDetail列表（分页）
 * 对应后端：GetEcDetailListAsync
 */
export function getEcDetailList(params: EcDetailQuery): Promise<TaktPagedResult<EcDetail>> {
  return request({
    url: `${ecDetailUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EcDetail
 * 对应后端：GetEcDetailByIdAsync
 */
export function getEcDetailById(id: string): Promise<EcDetail> {
  return request({
    url: `${ecDetailUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EcDetail选项列表（用于下拉框等）
 * 对应后端：GetEcDetailOptionsAsync
 */
export function getEcDetailOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ecDetailUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EcDetail
 * 对应后端：CreateEcDetailAsync
 */
export function createEcDetail(data: EcDetailCreate): Promise<EcDetail> {
  return request({
    url: ecDetailUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EcDetail
 * 对应后端：UpdateEcDetailAsync
 */
export function updateEcDetail(id: string, data: EcDetailUpdate): Promise<EcDetail> {
  return request({
    url: `${ecDetailUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EcDetail（单条）
 * 对应后端：DeleteEcDetailByIdAsync
 */
export function deleteEcDetailById(id: string): Promise<void> {
  return request({
    url: `${ecDetailUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EcDetail
 * 对应后端：DeleteEcDetailBatchAsync
 */
export function deleteEcDetailBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ecDetailUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEcDetailTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEcDetailTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ecDetailUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EcDetail
 * 对应后端：ImportEcDetailAsync
 */
export function importEcDetailData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ecDetailUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EcDetail
 * 对应后端：ExportEcDetailAsync；fileName 仅传名称不含后缀
 */
export function exportEcDetailData(query: EcDetailQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ecDetailUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
