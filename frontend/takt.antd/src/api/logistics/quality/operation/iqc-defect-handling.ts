// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：iqc-defect-handling.ts
// 功能描述：IqcDefectHandling API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktIqcDefectHandlings
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  IqcDefectHandling,
  IqcDefectHandlingQuery,
  IqcDefectHandlingCreate,
  IqcDefectHandlingUpdate
} from '@/types/logistics/quality/operation/iqc-defect-handling'

// ========================================
// IqcDefectHandling相关 API（按后端控制器顺序）
// ========================================
const iqcDefectHandlingUrl = '/api/TaktIqcDefectHandlings';

/**
 * 获取IqcDefectHandling列表（分页）
 * 对应后端：GetIqcDefectHandlingListAsync
 */
export function getIqcDefectHandlingList(params: IqcDefectHandlingQuery): Promise<TaktPagedResult<IqcDefectHandling>> {
  return request({
    url: `${iqcDefectHandlingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取IqcDefectHandling
 * 对应后端：GetIqcDefectHandlingByIdAsync
 */
export function getIqcDefectHandlingById(id: string): Promise<IqcDefectHandling> {
  return request({
    url: `${iqcDefectHandlingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取IqcDefectHandling选项列表（用于下拉框等）
 * 对应后端：GetIqcDefectHandlingOptionsAsync
 */
export function getIqcDefectHandlingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${iqcDefectHandlingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建IqcDefectHandling
 * 对应后端：CreateIqcDefectHandlingAsync
 */
export function createIqcDefectHandling(data: IqcDefectHandlingCreate): Promise<IqcDefectHandling> {
  return request({
    url: iqcDefectHandlingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新IqcDefectHandling
 * 对应后端：UpdateIqcDefectHandlingAsync
 */
export function updateIqcDefectHandling(id: string, data: IqcDefectHandlingUpdate): Promise<IqcDefectHandling> {
  return request({
    url: `${iqcDefectHandlingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除IqcDefectHandling（单条）
 * 对应后端：DeleteIqcDefectHandlingByIdAsync
 */
export function deleteIqcDefectHandlingById(id: string): Promise<void> {
  return request({
    url: `${iqcDefectHandlingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除IqcDefectHandling
 * 对应后端：DeleteIqcDefectHandlingBatchAsync
 */
export function deleteIqcDefectHandlingBatch(ids: string[]): Promise<void> {
  return request({
    url: `${iqcDefectHandlingUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetIqcDefectHandlingTemplateAsync；fileName 仅传名称不含后缀
 */
export function getIqcDefectHandlingTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${iqcDefectHandlingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入IqcDefectHandling
 * 对应后端：ImportIqcDefectHandlingAsync
 */
export function importIqcDefectHandlingData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${iqcDefectHandlingUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出IqcDefectHandling
 * 对应后端：ExportIqcDefectHandlingAsync；fileName 仅传名称不含后缀
 */
export function exportIqcDefectHandlingData(query: IqcDefectHandlingQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${iqcDefectHandlingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
