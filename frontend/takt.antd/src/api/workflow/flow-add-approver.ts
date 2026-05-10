// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/workflow
// 文件名称：flow-add-approver.ts
// 功能描述：FlowAddApprover API，对应后端 Takt.WebApi.Controllers.Workflow.TaktFlowAddApprovers
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  FlowAddApprover,
  FlowAddApproverQuery,
  FlowAddApproverCreate,
  FlowAddApproverUpdate,
  FlowAddApproverStatus
} from '@/types/workflow/flow-add-approver'

// ========================================
// FlowAddApprover相关 API（按后端控制器顺序）
// ========================================
const flowAddApproverUrl = '/api/TaktFlowAddApprovers';

/**
 * 获取FlowAddApprover列表（分页）
 * 对应后端：GetFlowAddApproverListAsync
 */
export function getFlowAddApproverList(params: FlowAddApproverQuery): Promise<TaktPagedResult<FlowAddApprover>> {
  return request({
    url: `${flowAddApproverUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取FlowAddApprover
 * 对应后端：GetFlowAddApproverByIdAsync
 */
export function getFlowAddApproverById(id: string): Promise<FlowAddApprover> {
  return request({
    url: `${flowAddApproverUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取FlowAddApprover选项列表（用于下拉框等）
 * 对应后端：GetFlowAddApproverOptionsAsync
 */
export function getFlowAddApproverOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${flowAddApproverUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建FlowAddApprover
 * 对应后端：CreateFlowAddApproverAsync
 */
export function createFlowAddApprover(data: FlowAddApproverCreate): Promise<FlowAddApprover> {
  return request({
    url: flowAddApproverUrl,
    method: 'post',
    data
  })
}

/**
 * 更新FlowAddApprover
 * 对应后端：UpdateFlowAddApproverAsync
 */
export function updateFlowAddApprover(id: string, data: FlowAddApproverUpdate): Promise<FlowAddApprover> {
  return request({
    url: `${flowAddApproverUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除FlowAddApprover（单条）
 * 对应后端：DeleteFlowAddApproverByIdAsync
 */
export function deleteFlowAddApproverById(id: string): Promise<void> {
  return request({
    url: `${flowAddApproverUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除FlowAddApprover
 * 对应后端：DeleteFlowAddApproverBatchAsync
 */
export function deleteFlowAddApproverBatch(ids: string[]): Promise<void> {
  return request({
    url: `${flowAddApproverUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新FlowAddApprover状态
 * 对应后端：UpdateFlowAddApproverStatusAsync
 */
export function updateFlowAddApproverStatus(data: FlowAddApproverStatus): Promise<FlowAddApproverStatus> {
  return request({
    url: `${flowAddApproverUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetFlowAddApproverTemplateAsync；fileName 仅传名称不含后缀
 */
export function getFlowAddApproverTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${flowAddApproverUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入FlowAddApprover
 * 对应后端：ImportFlowAddApproverAsync
 */
export function importFlowAddApproverData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${flowAddApproverUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出FlowAddApprover
 * 对应后端：ExportFlowAddApproverAsync；fileName 仅传名称不含后缀
 */
export function exportFlowAddApproverData(query: FlowAddApproverQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${flowAddApproverUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
