// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：sampling-scheme.ts
// 功能描述：SamplingScheme API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktSamplingSchemes
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SamplingScheme,
  SamplingSchemeQuery,
  SamplingSchemeCreate,
  SamplingSchemeUpdate,
  SamplingSchemeStatus
} from '@/types/logistics/quality/operation/sampling-scheme'

// ========================================
// SamplingScheme相关 API（按后端控制器顺序）
// ========================================
const samplingSchemeUrl = '/api/TaktSamplingSchemes';

/**
 * 获取SamplingScheme列表（分页）
 * 对应后端：GetSamplingSchemeListAsync
 */
export function getSamplingSchemeList(params: SamplingSchemeQuery): Promise<TaktPagedResult<SamplingScheme>> {
  return request({
    url: `${samplingSchemeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SamplingScheme
 * 对应后端：GetSamplingSchemeByIdAsync
 */
export function getSamplingSchemeById(id: string): Promise<SamplingScheme> {
  return request({
    url: `${samplingSchemeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SamplingScheme选项列表（用于下拉框等）
 * 对应后端：GetSamplingSchemeOptionsAsync
 */
export function getSamplingSchemeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${samplingSchemeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SamplingScheme
 * 对应后端：CreateSamplingSchemeAsync
 */
export function createSamplingScheme(data: SamplingSchemeCreate): Promise<SamplingScheme> {
  return request({
    url: samplingSchemeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SamplingScheme
 * 对应后端：UpdateSamplingSchemeAsync
 */
export function updateSamplingScheme(id: string, data: SamplingSchemeUpdate): Promise<SamplingScheme> {
  return request({
    url: `${samplingSchemeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SamplingScheme（单条）
 * 对应后端：DeleteSamplingSchemeByIdAsync
 */
export function deleteSamplingSchemeById(id: string): Promise<void> {
  return request({
    url: `${samplingSchemeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SamplingScheme
 * 对应后端：DeleteSamplingSchemeBatchAsync
 */
export function deleteSamplingSchemeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${samplingSchemeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新SamplingScheme状态
 * 对应后端：UpdateSamplingSchemeStatusAsync
 */
export function updateSamplingSchemeStatus(data: SamplingSchemeStatus): Promise<SamplingSchemeStatus> {
  return request({
    url: `${samplingSchemeUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSamplingSchemeTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSamplingSchemeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${samplingSchemeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SamplingScheme
 * 对应后端：ImportSamplingSchemeAsync
 */
export function importSamplingSchemeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${samplingSchemeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SamplingScheme
 * 对应后端：ExportSamplingSchemeAsync；fileName 仅传名称不含后缀
 */
export function exportSamplingSchemeData(query: SamplingSchemeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${samplingSchemeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
