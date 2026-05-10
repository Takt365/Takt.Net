// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/engineering-change
// 文件名称：ec-notice.ts
// 功能描述：EcNotice API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange.TaktEcNotices
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EcNotice,
  EcNoticeQuery,
  EcNoticeCreate,
  EcNoticeUpdate
} from '@/types/logistics/manufacturing/engineering-change/ec-notice'

// ========================================
// EcNotice相关 API（按后端控制器顺序）
// ========================================
const ecNoticeUrl = '/api/TaktEcNotices';

/**
 * 获取EcNotice列表（分页）
 * 对应后端：GetEcNoticeListAsync
 */
export function getEcNoticeList(params: EcNoticeQuery): Promise<TaktPagedResult<EcNotice>> {
  return request({
    url: `${ecNoticeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EcNotice
 * 对应后端：GetEcNoticeByIdAsync
 */
export function getEcNoticeById(id: string): Promise<EcNotice> {
  return request({
    url: `${ecNoticeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EcNotice选项列表（用于下拉框等）
 * 对应后端：GetEcNoticeOptionsAsync
 */
export function getEcNoticeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ecNoticeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EcNotice
 * 对应后端：CreateEcNoticeAsync
 */
export function createEcNotice(data: EcNoticeCreate): Promise<EcNotice> {
  return request({
    url: ecNoticeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EcNotice
 * 对应后端：UpdateEcNoticeAsync
 */
export function updateEcNotice(id: string, data: EcNoticeUpdate): Promise<EcNotice> {
  return request({
    url: `${ecNoticeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EcNotice（单条）
 * 对应后端：DeleteEcNoticeByIdAsync
 */
export function deleteEcNoticeById(id: string): Promise<void> {
  return request({
    url: `${ecNoticeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EcNotice
 * 对应后端：DeleteEcNoticeBatchAsync
 */
export function deleteEcNoticeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ecNoticeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEcNoticeTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEcNoticeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ecNoticeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EcNotice
 * 对应后端：ImportEcNoticeAsync
 */
export function importEcNoticeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ecNoticeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EcNotice
 * 对应后端：ExportEcNoticeAsync；fileName 仅传名称不含后缀
 */
export function exportEcNoticeData(query: EcNoticeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ecNoticeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
