// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/workflow
// 文件名称：flow-execution.ts
// 功能描述：FlowExecution API，对应后端 Takt.WebApi.Controllers.Workflow.TaktFlowExecutions
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  FlowExecution,
  FlowExecutionQuery,
  FlowExecutionCreate,
  FlowExecutionUpdate
} from '@/types/workflow/flow-execution'

// ========================================
// FlowExecution相关 API（按后端控制器顺序）
// ========================================
const flowExecutionUrl = '/api/TaktFlowExecutions';

/**
 * 获取FlowExecution列表（分页）
 * 对应后端：GetFlowExecutionListAsync
 */
export function getFlowExecutionList(params: FlowExecutionQuery): Promise<TaktPagedResult<FlowExecution>> {
  return request({
    url: `${flowExecutionUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取FlowExecution
 * 对应后端：GetFlowExecutionByIdAsync
 */
export function getFlowExecutionById(id: string): Promise<FlowExecution> {
  return request({
    url: `${flowExecutionUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取FlowExecution选项列表（用于下拉框等）
 * 对应后端：GetFlowExecutionOptionsAsync
 */
export function getFlowExecutionOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${flowExecutionUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建FlowExecution
 * 对应后端：CreateFlowExecutionAsync
 */
export function createFlowExecution(data: FlowExecutionCreate): Promise<FlowExecution> {
  return request({
    url: flowExecutionUrl,
    method: 'post',
    data
  })
}

/**
 * 更新FlowExecution
 * 对应后端：UpdateFlowExecutionAsync
 */
export function updateFlowExecution(id: string, data: FlowExecutionUpdate): Promise<FlowExecution> {
  return request({
    url: `${flowExecutionUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除FlowExecution（单条）
 * 对应后端：DeleteFlowExecutionByIdAsync
 */
export function deleteFlowExecutionById(id: string): Promise<void> {
  return request({
    url: `${flowExecutionUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除FlowExecution
 * 对应后端：DeleteFlowExecutionBatchAsync
 */
export function deleteFlowExecutionBatch(ids: string[]): Promise<void> {
  return request({
    url: `${flowExecutionUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetFlowExecutionTemplateAsync；fileName 仅传名称不含后缀
 */
export function getFlowExecutionTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${flowExecutionUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入FlowExecution
 * 对应后端：ImportFlowExecutionAsync
 */
export function importFlowExecutionData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${flowExecutionUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出FlowExecution
 * 对应后端：ExportFlowExecutionAsync；fileName 仅传名称不含后缀
 */
export function exportFlowExecutionData(query: FlowExecutionQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${flowExecutionUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
