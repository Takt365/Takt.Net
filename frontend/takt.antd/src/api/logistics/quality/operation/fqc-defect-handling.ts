// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：fqc-defect-handling.ts
// 功能描述：FqcDefectHandling API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktFqcDefectHandlings
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  FqcDefectHandling,
  FqcDefectHandlingQuery,
  FqcDefectHandlingCreate,
  FqcDefectHandlingUpdate
} from '@/types/logistics/quality/operation/fqc-defect-handling'

// ========================================
// FqcDefectHandling相关 API（按后端控制器顺序）
// ========================================
const fqcDefectHandlingUrl = '/api/TaktFqcDefectHandlings';

/**
 * 获取FqcDefectHandling列表（分页）
 * 对应后端：GetFqcDefectHandlingListAsync
 */
export function getFqcDefectHandlingList(params: FqcDefectHandlingQuery): Promise<TaktPagedResult<FqcDefectHandling>> {
  return request({
    url: `${fqcDefectHandlingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取FqcDefectHandling
 * 对应后端：GetFqcDefectHandlingByIdAsync
 */
export function getFqcDefectHandlingById(id: string): Promise<FqcDefectHandling> {
  return request({
    url: `${fqcDefectHandlingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取FqcDefectHandling选项列表（用于下拉框等）
 * 对应后端：GetFqcDefectHandlingOptionsAsync
 */
export function getFqcDefectHandlingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${fqcDefectHandlingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建FqcDefectHandling
 * 对应后端：CreateFqcDefectHandlingAsync
 */
export function createFqcDefectHandling(data: FqcDefectHandlingCreate): Promise<FqcDefectHandling> {
  return request({
    url: fqcDefectHandlingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新FqcDefectHandling
 * 对应后端：UpdateFqcDefectHandlingAsync
 */
export function updateFqcDefectHandling(id: string, data: FqcDefectHandlingUpdate): Promise<FqcDefectHandling> {
  return request({
    url: `${fqcDefectHandlingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除FqcDefectHandling（单条）
 * 对应后端：DeleteFqcDefectHandlingByIdAsync
 */
export function deleteFqcDefectHandlingById(id: string): Promise<void> {
  return request({
    url: `${fqcDefectHandlingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除FqcDefectHandling
 * 对应后端：DeleteFqcDefectHandlingBatchAsync
 */
export function deleteFqcDefectHandlingBatch(ids: string[]): Promise<void> {
  return request({
    url: `${fqcDefectHandlingUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetFqcDefectHandlingTemplateAsync；fileName 仅传名称不含后缀
 */
export function getFqcDefectHandlingTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${fqcDefectHandlingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入FqcDefectHandling
 * 对应后端：ImportFqcDefectHandlingAsync
 */
export function importFqcDefectHandlingData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${fqcDefectHandlingUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出FqcDefectHandling
 * 对应后端：ExportFqcDefectHandlingAsync；fileName 仅传名称不含后缀
 */
export function exportFqcDefectHandlingData(query: FqcDefectHandlingQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${fqcDefectHandlingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
