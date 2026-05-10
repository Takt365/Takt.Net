// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/workflow
// 文件名称：flow-operation.ts
// 功能描述：FlowOperation API，对应后端 Takt.WebApi.Controllers.Workflow.TaktFlowOperations
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  FlowOperation,
  FlowOperationQuery,
  FlowOperationCreate,
  FlowOperationUpdate
} from '@/types/workflow/flow-operation'

// ========================================
// FlowOperation相关 API（按后端控制器顺序）
// ========================================
const flowOperationUrl = '/api/TaktFlowOperations';

/**
 * 获取FlowOperation列表（分页）
 * 对应后端：GetFlowOperationListAsync
 */
export function getFlowOperationList(params: FlowOperationQuery): Promise<TaktPagedResult<FlowOperation>> {
  return request({
    url: `${flowOperationUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取FlowOperation
 * 对应后端：GetFlowOperationByIdAsync
 */
export function getFlowOperationById(id: string): Promise<FlowOperation> {
  return request({
    url: `${flowOperationUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取FlowOperation选项列表（用于下拉框等）
 * 对应后端：GetFlowOperationOptionsAsync
 */
export function getFlowOperationOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${flowOperationUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建FlowOperation
 * 对应后端：CreateFlowOperationAsync
 */
export function createFlowOperation(data: FlowOperationCreate): Promise<FlowOperation> {
  return request({
    url: flowOperationUrl,
    method: 'post',
    data
  })
}

/**
 * 更新FlowOperation
 * 对应后端：UpdateFlowOperationAsync
 */
export function updateFlowOperation(id: string, data: FlowOperationUpdate): Promise<FlowOperation> {
  return request({
    url: `${flowOperationUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除FlowOperation（单条）
 * 对应后端：DeleteFlowOperationByIdAsync
 */
export function deleteFlowOperationById(id: string): Promise<void> {
  return request({
    url: `${flowOperationUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除FlowOperation
 * 对应后端：DeleteFlowOperationBatchAsync
 */
export function deleteFlowOperationBatch(ids: string[]): Promise<void> {
  return request({
    url: `${flowOperationUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetFlowOperationTemplateAsync；fileName 仅传名称不含后缀
 */
export function getFlowOperationTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${flowOperationUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入FlowOperation
 * 对应后端：ImportFlowOperationAsync
 */
export function importFlowOperationData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${flowOperationUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出FlowOperation
 * 对应后端：ExportFlowOperationAsync；fileName 仅传名称不含后缀
 */
export function exportFlowOperationData(query: FlowOperationQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${flowOperationUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
