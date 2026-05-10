// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/output
// 文件名称：pcba-output.ts
// 功能描述：PcbaOutput API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Output.TaktPcbaOutputs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PcbaOutput,
  PcbaOutputQuery,
  PcbaOutputCreate,
  PcbaOutputUpdate
} from '@/types/logistics/manufacturing/output/pcba-output'

// ========================================
// PcbaOutput相关 API（按后端控制器顺序）
// ========================================
const pcbaOutputUrl = '/api/TaktPcbaOutputs';

/**
 * 获取PcbaOutput列表（分页）
 * 对应后端：GetPcbaOutputListAsync
 */
export function getPcbaOutputList(params: PcbaOutputQuery): Promise<TaktPagedResult<PcbaOutput>> {
  return request({
    url: `${pcbaOutputUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PcbaOutput
 * 对应后端：GetPcbaOutputByIdAsync
 */
export function getPcbaOutputById(id: string): Promise<PcbaOutput> {
  return request({
    url: `${pcbaOutputUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PcbaOutput选项列表（用于下拉框等）
 * 对应后端：GetPcbaOutputOptionsAsync
 */
export function getPcbaOutputOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${pcbaOutputUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PcbaOutput
 * 对应后端：CreatePcbaOutputAsync
 */
export function createPcbaOutput(data: PcbaOutputCreate): Promise<PcbaOutput> {
  return request({
    url: pcbaOutputUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PcbaOutput
 * 对应后端：UpdatePcbaOutputAsync
 */
export function updatePcbaOutput(id: string, data: PcbaOutputUpdate): Promise<PcbaOutput> {
  return request({
    url: `${pcbaOutputUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PcbaOutput（单条）
 * 对应后端：DeletePcbaOutputByIdAsync
 */
export function deletePcbaOutputById(id: string): Promise<void> {
  return request({
    url: `${pcbaOutputUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PcbaOutput
 * 对应后端：DeletePcbaOutputBatchAsync
 */
export function deletePcbaOutputBatch(ids: string[]): Promise<void> {
  return request({
    url: `${pcbaOutputUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPcbaOutputTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPcbaOutputTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${pcbaOutputUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PcbaOutput
 * 对应后端：ImportPcbaOutputAsync
 */
export function importPcbaOutputData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${pcbaOutputUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PcbaOutput
 * 对应后端：ExportPcbaOutputAsync；fileName 仅传名称不含后缀
 */
export function exportPcbaOutputData(query: PcbaOutputQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${pcbaOutputUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
