// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/workflow
// 文件名称：flow-instance.ts
// 功能描述：FlowInstance API，对应后端 Takt.WebApi.Controllers.Workflow.TaktFlowInstances
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  FlowInstance,
  FlowInstanceQuery,
  FlowInstanceCreate,
  FlowInstanceUpdate
} from '@/types/workflow/flow-instance'

// ========================================
// FlowInstance相关 API（按后端控制器顺序）
// ========================================
const flowInstanceUrl = '/api/TaktFlowInstances';

/**
 * 获取FlowInstance列表（分页）
 * 对应后端：GetFlowInstanceListAsync
 */
export function getFlowInstanceList(params: FlowInstanceQuery): Promise<TaktPagedResult<FlowInstance>> {
  return request({
    url: `${flowInstanceUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取FlowInstance
 * 对应后端：GetFlowInstanceByIdAsync
 */
export function getFlowInstanceById(id: string): Promise<FlowInstance> {
  return request({
    url: `${flowInstanceUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取FlowInstance选项列表（用于下拉框等）
 * 对应后端：GetFlowInstanceOptionsAsync
 */
export function getFlowInstanceOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${flowInstanceUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建FlowInstance
 * 对应后端：CreateFlowInstanceAsync
 */
export function createFlowInstance(data: FlowInstanceCreate): Promise<FlowInstance> {
  return request({
    url: flowInstanceUrl,
    method: 'post',
    data
  })
}

/**
 * 更新FlowInstance
 * 对应后端：UpdateFlowInstanceAsync
 */
export function updateFlowInstance(id: string, data: FlowInstanceUpdate): Promise<FlowInstance> {
  return request({
    url: `${flowInstanceUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除FlowInstance（单条）
 * 对应后端：DeleteFlowInstanceByIdAsync
 */
export function deleteFlowInstanceById(id: string): Promise<void> {
  return request({
    url: `${flowInstanceUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除FlowInstance
 * 对应后端：DeleteFlowInstanceBatchAsync
 */
export function deleteFlowInstanceBatch(ids: string[]): Promise<void> {
  return request({
    url: `${flowInstanceUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetFlowInstanceTemplateAsync；fileName 仅传名称不含后缀
 */
export function getFlowInstanceTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${flowInstanceUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入FlowInstance
 * 对应后端：ImportFlowInstanceAsync
 */
export function importFlowInstanceData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${flowInstanceUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出FlowInstance
 * 对应后端：ExportFlowInstanceAsync；fileName 仅传名称不含后缀
 */
export function exportFlowInstanceData(query: FlowInstanceQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${flowInstanceUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
