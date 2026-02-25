// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/workflow
// 文件名称：form.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：流程表单 API，对应后端 TaktFlowFormsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type { FlowForm, FlowFormQuery, FlowFormCreate, FlowFormUpdate, FlowFormStatus } from '@/types/workflow/form'

/** 获取流程表单列表（分页）。对应后端：GetListAsync */
export function getList(params: FlowFormQuery): Promise<TaktPagedResult<FlowForm>> {
  return request({
    url: '/api/TaktFlowForms/list',
    method: 'get',
    params
  })
}

/** 根据ID获取流程表单。对应后端：GetByIdAsync */
export function getById(id: string): Promise<FlowForm> {
  return request({
    url: `/api/TaktFlowForms/${id}`,
    method: 'get'
  })
}

/** 根据表单编码获取流程表单（仅已发布）。对应后端：GetByFormCodeAsync */
export function getByFormCode(formCode: string): Promise<FlowForm> {
  return request({
    url: `/api/TaktFlowForms/by-code/${encodeURIComponent(formCode)}`,
    method: 'get'
  })
}

/** 获取流程表单选项列表（仅已发布）。对应后端：GetOptionsAsync */
export function getOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktFlowForms/options',
    method: 'get'
  })
}

/** 创建流程表单。对应后端：CreateAsync */
export function create(data: FlowFormCreate): Promise<FlowForm> {
  return request({
    url: '/api/TaktFlowForms',
    method: 'post',
    data
  })
}

/** 更新流程表单。对应后端：UpdateAsync */
export function update(id: string, data: FlowFormUpdate): Promise<FlowForm> {
  return request({
    url: `/api/TaktFlowForms/${id}`,
    method: 'put',
    data: { ...data, formId: id }
  })
}

/** 更新流程表单状态（0=草稿，1=已发布，2=已停用）。对应后端：UpdateStatusAsync */
export function updateStatus(data: FlowFormStatus): Promise<FlowForm> {
  return request({
    url: '/api/TaktFlowForms/status',
    method: 'put',
    data
  })
}

/** 删除流程表单（软删除）。对应后端：DeleteAsync(long id) */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktFlowForms/${id}`,
    method: 'delete'
  })
}

/** 批量删除流程表单。对应后端：DeleteBatchAsync(IEnumerable<long> ids) */
export function removeBatch(ids: string[] | number[]): Promise<void> {
  const body = ids.map((id) => Number(id))
  return request({
    url: '/api/TaktFlowForms/batch',
    method: 'delete',
    data: body
  })
}

/** 获取导入模板。对应后端：GetTemplateAsync */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktFlowForms/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/** 导入流程表单。对应后端：ImportAsync */
export function importForms(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktFlowForms/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/** 导出流程表单。对应后端：ExportAsync */
export function exportForms(
  query: FlowFormQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktFlowForms/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
