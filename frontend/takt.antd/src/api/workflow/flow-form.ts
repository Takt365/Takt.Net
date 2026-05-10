// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/workflow
// 文件名称：flow-form.ts
// 功能描述：FlowForm API，对应后端 Takt.WebApi.Controllers.Workflow.TaktFlowForms
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  FlowForm,
  FlowFormQuery,
  FlowFormCreate,
  FlowFormUpdate,
  FlowFormSort
} from '@/types/workflow/flow-form'

// ========================================
// FlowForm相关 API（按后端控制器顺序）
// ========================================
const flowFormUrl = '/api/TaktFlowForms';

/**
 * 获取FlowForm列表（分页）
 * 对应后端：GetFlowFormListAsync
 */
export function getFlowFormList(params: FlowFormQuery): Promise<TaktPagedResult<FlowForm>> {
  return request({
    url: `${flowFormUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取FlowForm
 * 对应后端：GetFlowFormByIdAsync
 */
export function getFlowFormById(id: string): Promise<FlowForm> {
  return request({
    url: `${flowFormUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取FlowForm选项列表（用于下拉框等）
 * 对应后端：GetFlowFormOptionsAsync
 */
export function getFlowFormOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${flowFormUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建FlowForm
 * 对应后端：CreateFlowFormAsync
 */
export function createFlowForm(data: FlowFormCreate): Promise<FlowForm> {
  return request({
    url: flowFormUrl,
    method: 'post',
    data
  })
}

/**
 * 更新FlowForm
 * 对应后端：UpdateFlowFormAsync
 */
export function updateFlowForm(id: string, data: FlowFormUpdate): Promise<FlowForm> {
  return request({
    url: `${flowFormUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除FlowForm（单条）
 * 对应后端：DeleteFlowFormByIdAsync
 */
export function deleteFlowFormById(id: string): Promise<void> {
  return request({
    url: `${flowFormUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除FlowForm
 * 对应后端：DeleteFlowFormBatchAsync
 */
export function deleteFlowFormBatch(ids: string[]): Promise<void> {
  return request({
    url: `${flowFormUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新FlowForm排序
 * 对应后端：UpdateFlowFormSortAsync
 */
export function updateFlowFormSort(data: FlowFormSort): Promise<FlowFormSort> {
  return request({
    url: `${flowFormUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetFlowFormTemplateAsync；fileName 仅传名称不含后缀
 */
export function getFlowFormTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${flowFormUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入FlowForm
 * 对应后端：ImportFlowFormAsync
 */
export function importFlowFormData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${flowFormUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出FlowForm
 * 对应后端：ExportFlowFormAsync；fileName 仅传名称不含后缀
 */
export function exportFlowFormData(query: FlowFormQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${flowFormUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
