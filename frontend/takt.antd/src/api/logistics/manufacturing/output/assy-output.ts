// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/output
// 文件名称：assy-output.ts
// 功能描述：AssyOutput API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Output.TaktAssyOutputs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AssyOutput,
  AssyOutputQuery,
  AssyOutputCreate,
  AssyOutputUpdate,
  AssyOutputStatus
} from '@/types/logistics/manufacturing/output/assy-output'

// ========================================
// AssyOutput相关 API（按后端控制器顺序）
// ========================================
const assyOutputUrl = '/api/TaktAssyOutputs';

/**
 * 获取AssyOutput列表（分页）
 * 对应后端：GetAssyOutputListAsync
 */
export function getAssyOutputList(params: AssyOutputQuery): Promise<TaktPagedResult<AssyOutput>> {
  return request({
    url: `${assyOutputUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AssyOutput
 * 对应后端：GetAssyOutputByIdAsync
 */
export function getAssyOutputById(id: string): Promise<AssyOutput> {
  return request({
    url: `${assyOutputUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AssyOutput选项列表（用于下拉框等）
 * 对应后端：GetAssyOutputOptionsAsync
 */
export function getAssyOutputOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${assyOutputUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AssyOutput
 * 对应后端：CreateAssyOutputAsync
 */
export function createAssyOutput(data: AssyOutputCreate): Promise<AssyOutput> {
  return request({
    url: assyOutputUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AssyOutput
 * 对应后端：UpdateAssyOutputAsync
 */
export function updateAssyOutput(id: string, data: AssyOutputUpdate): Promise<AssyOutput> {
  return request({
    url: `${assyOutputUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AssyOutput（单条）
 * 对应后端：DeleteAssyOutputByIdAsync
 */
export function deleteAssyOutputById(id: string): Promise<void> {
  return request({
    url: `${assyOutputUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AssyOutput
 * 对应后端：DeleteAssyOutputBatchAsync
 */
export function deleteAssyOutputBatch(ids: string[]): Promise<void> {
  return request({
    url: `${assyOutputUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新AssyOutput状态
 * 对应后端：UpdateAssyOutputStatusAsync
 */
export function updateAssyOutputStatus(data: AssyOutputStatus): Promise<AssyOutputStatus> {
  return request({
    url: `${assyOutputUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAssyOutputTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAssyOutputTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${assyOutputUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AssyOutput
 * 对应后端：ImportAssyOutputAsync
 */
export function importAssyOutputData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${assyOutputUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AssyOutput
 * 对应后端：ExportAssyOutputAsync；fileName 仅传名称不含后缀
 */
export function exportAssyOutputData(query: AssyOutputQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${assyOutputUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
