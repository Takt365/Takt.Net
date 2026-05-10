// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/engineering-change
// 文件名称：ec-attachment.ts
// 功能描述：EcAttachment API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange.TaktEcAttachments
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EcAttachment,
  EcAttachmentQuery,
  EcAttachmentCreate,
  EcAttachmentUpdate,
  EcAttachmentSort
} from '@/types/logistics/manufacturing/engineering-change/ec-attachment'

// ========================================
// EcAttachment相关 API（按后端控制器顺序）
// ========================================
const ecAttachmentUrl = '/api/TaktEcAttachments';

/**
 * 获取EcAttachment列表（分页）
 * 对应后端：GetEcAttachmentListAsync
 */
export function getEcAttachmentList(params: EcAttachmentQuery): Promise<TaktPagedResult<EcAttachment>> {
  return request({
    url: `${ecAttachmentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EcAttachment
 * 对应后端：GetEcAttachmentByIdAsync
 */
export function getEcAttachmentById(id: string): Promise<EcAttachment> {
  return request({
    url: `${ecAttachmentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EcAttachment选项列表（用于下拉框等）
 * 对应后端：GetEcAttachmentOptionsAsync
 */
export function getEcAttachmentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ecAttachmentUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EcAttachment
 * 对应后端：CreateEcAttachmentAsync
 */
export function createEcAttachment(data: EcAttachmentCreate): Promise<EcAttachment> {
  return request({
    url: ecAttachmentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EcAttachment
 * 对应后端：UpdateEcAttachmentAsync
 */
export function updateEcAttachment(id: string, data: EcAttachmentUpdate): Promise<EcAttachment> {
  return request({
    url: `${ecAttachmentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EcAttachment（单条）
 * 对应后端：DeleteEcAttachmentByIdAsync
 */
export function deleteEcAttachmentById(id: string): Promise<void> {
  return request({
    url: `${ecAttachmentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EcAttachment
 * 对应后端：DeleteEcAttachmentBatchAsync
 */
export function deleteEcAttachmentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ecAttachmentUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新EcAttachment排序
 * 对应后端：UpdateEcAttachmentSortAsync
 */
export function updateEcAttachmentSort(data: EcAttachmentSort): Promise<EcAttachmentSort> {
  return request({
    url: `${ecAttachmentUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEcAttachmentTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEcAttachmentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ecAttachmentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EcAttachment
 * 对应后端：ImportEcAttachmentAsync
 */
export function importEcAttachmentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ecAttachmentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EcAttachment
 * 对应后端：ExportEcAttachmentAsync；fileName 仅传名称不含后缀
 */
export function exportEcAttachmentData(query: EcAttachmentQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ecAttachmentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
