// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/workflow
// 文件名称：flow-scheme.ts
// 功能描述：FlowScheme API，对应后端 Takt.WebApi.Controllers.Workflow.TaktFlowSchemes
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  FlowScheme,
  FlowSchemeQuery,
  FlowSchemeCreate,
  FlowSchemeUpdate,
  FlowSchemeSort
} from '@/types/workflow/flow-scheme'

// ========================================
// FlowScheme相关 API（按后端控制器顺序）
// ========================================
const flowSchemeUrl = '/api/TaktFlowSchemes';

/**
 * 获取FlowScheme列表（分页）
 * 对应后端：GetFlowSchemeListAsync
 */
export function getFlowSchemeList(params: FlowSchemeQuery): Promise<TaktPagedResult<FlowScheme>> {
  return request({
    url: `${flowSchemeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取FlowScheme
 * 对应后端：GetFlowSchemeByIdAsync
 */
export function getFlowSchemeById(id: string): Promise<FlowScheme> {
  return request({
    url: `${flowSchemeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取FlowScheme选项列表（用于下拉框等）
 * 对应后端：GetFlowSchemeOptionsAsync
 */
export function getFlowSchemeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${flowSchemeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建FlowScheme
 * 对应后端：CreateFlowSchemeAsync
 */
export function createFlowScheme(data: FlowSchemeCreate): Promise<FlowScheme> {
  return request({
    url: flowSchemeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新FlowScheme
 * 对应后端：UpdateFlowSchemeAsync
 */
export function updateFlowScheme(id: string, data: FlowSchemeUpdate): Promise<FlowScheme> {
  return request({
    url: `${flowSchemeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除FlowScheme（单条）
 * 对应后端：DeleteFlowSchemeByIdAsync
 */
export function deleteFlowSchemeById(id: string): Promise<void> {
  return request({
    url: `${flowSchemeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除FlowScheme
 * 对应后端：DeleteFlowSchemeBatchAsync
 */
export function deleteFlowSchemeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${flowSchemeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新FlowScheme排序
 * 对应后端：UpdateFlowSchemeSortAsync
 */
export function updateFlowSchemeSort(data: FlowSchemeSort): Promise<FlowSchemeSort> {
  return request({
    url: `${flowSchemeUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetFlowSchemeTemplateAsync；fileName 仅传名称不含后缀
 */
export function getFlowSchemeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${flowSchemeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入FlowScheme
 * 对应后端：ImportFlowSchemeAsync
 */
export function importFlowSchemeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${flowSchemeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出FlowScheme
 * 对应后端：ExportFlowSchemeAsync；fileName 仅传名称不含后缀
 */
export function exportFlowSchemeData(query: FlowSchemeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${flowSchemeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
