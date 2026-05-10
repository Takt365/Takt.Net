// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：ipqc-defect-handling.ts
// 功能描述：IpqcDefectHandling API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktIpqcDefectHandlings
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  IpqcDefectHandling,
  IpqcDefectHandlingQuery,
  IpqcDefectHandlingCreate,
  IpqcDefectHandlingUpdate
} from '@/types/logistics/quality/operation/ipqc-defect-handling'

// ========================================
// IpqcDefectHandling相关 API（按后端控制器顺序）
// ========================================
const ipqcDefectHandlingUrl = '/api/TaktIpqcDefectHandlings';

/**
 * 获取IpqcDefectHandling列表（分页）
 * 对应后端：GetIpqcDefectHandlingListAsync
 */
export function getIpqcDefectHandlingList(params: IpqcDefectHandlingQuery): Promise<TaktPagedResult<IpqcDefectHandling>> {
  return request({
    url: `${ipqcDefectHandlingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取IpqcDefectHandling
 * 对应后端：GetIpqcDefectHandlingByIdAsync
 */
export function getIpqcDefectHandlingById(id: string): Promise<IpqcDefectHandling> {
  return request({
    url: `${ipqcDefectHandlingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取IpqcDefectHandling选项列表（用于下拉框等）
 * 对应后端：GetIpqcDefectHandlingOptionsAsync
 */
export function getIpqcDefectHandlingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ipqcDefectHandlingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建IpqcDefectHandling
 * 对应后端：CreateIpqcDefectHandlingAsync
 */
export function createIpqcDefectHandling(data: IpqcDefectHandlingCreate): Promise<IpqcDefectHandling> {
  return request({
    url: ipqcDefectHandlingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新IpqcDefectHandling
 * 对应后端：UpdateIpqcDefectHandlingAsync
 */
export function updateIpqcDefectHandling(id: string, data: IpqcDefectHandlingUpdate): Promise<IpqcDefectHandling> {
  return request({
    url: `${ipqcDefectHandlingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除IpqcDefectHandling（单条）
 * 对应后端：DeleteIpqcDefectHandlingByIdAsync
 */
export function deleteIpqcDefectHandlingById(id: string): Promise<void> {
  return request({
    url: `${ipqcDefectHandlingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除IpqcDefectHandling
 * 对应后端：DeleteIpqcDefectHandlingBatchAsync
 */
export function deleteIpqcDefectHandlingBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ipqcDefectHandlingUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetIpqcDefectHandlingTemplateAsync；fileName 仅传名称不含后缀
 */
export function getIpqcDefectHandlingTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ipqcDefectHandlingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入IpqcDefectHandling
 * 对应后端：ImportIpqcDefectHandlingAsync
 */
export function importIpqcDefectHandlingData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ipqcDefectHandlingUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出IpqcDefectHandling
 * 对应后端：ExportIpqcDefectHandlingAsync；fileName 仅传名称不含后缀
 */
export function exportIpqcDefectHandlingData(query: IpqcDefectHandlingQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ipqcDefectHandlingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
