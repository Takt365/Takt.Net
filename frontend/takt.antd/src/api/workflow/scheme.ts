// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/workflow
// 文件名称：scheme.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：流程方案 API，对应后端 TaktFlowSchemesController（流程定义/流程建模）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type { FlowScheme, FlowSchemeQuery, FlowSchemeCreate, FlowSchemeUpdate, FlowSchemeStatus } from '@/types/workflow/scheme'

/** 获取流程方案列表（分页）。对应后端：GetListAsync */
export function getList(params: FlowSchemeQuery): Promise<TaktPagedResult<FlowScheme>> {
  return request({
    url: '/api/TaktFlowSchemes/list',
    method: 'get',
    params
  })
}

/** 根据ID获取流程方案（含 BpmnXml/ProcessJson）。对应后端：GetByIdAsync */
export function getById(id: string): Promise<FlowScheme> {
  return request({
    url: `/api/TaktFlowSchemes/${id}`,
    method: 'get'
  })
}

/** 获取流程方案选项列表（仅已发布）。对应后端：GetOptionsAsync */
export function getOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktFlowSchemes/options',
    method: 'get'
  })
}

/** 根据流程Key获取流程方案（仅已发布）。对应后端：GetByProcessKeyAsync */
export function getByProcessKey(processKey: string): Promise<FlowScheme> {
  return request({
    url: `/api/TaktFlowSchemes/by-key/${encodeURIComponent(processKey)}`,
    method: 'get'
  })
}

/** 创建流程方案。对应后端：CreateAsync */
export function create(data: FlowSchemeCreate): Promise<FlowScheme> {
  return request({
    url: '/api/TaktFlowSchemes',
    method: 'post',
    data
  })
}

/** 更新流程方案。对应后端：UpdateAsync */
export function update(id: string, data: FlowSchemeUpdate): Promise<FlowScheme> {
  return request({
    url: `/api/TaktFlowSchemes/${id}`,
    method: 'put',
    data: { ...data, schemeId: id }
  })
}

/** 更新流程方案状态（0=草稿，1=已发布，2=已停用）。对应后端：UpdateStatusAsync */
export function updateStatus(data: FlowSchemeStatus): Promise<FlowScheme> {
  return request({
    url: '/api/TaktFlowSchemes/status',
    method: 'put',
    data
  })
}

/** 删除流程方案（软删除）。对应后端：DeleteAsync(long id) */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktFlowSchemes/${id}`,
    method: 'delete'
  })
}

/** 批量删除流程方案。对应后端：DeleteBatchAsync(IEnumerable<long> ids) */
export function removeBatch(ids: string[] | number[]): Promise<void> {
  const body = ids.map((id) => Number(id))
  return request({
    url: '/api/TaktFlowSchemes/batch',
    method: 'delete',
    data: body
  })
}

/** 获取导入模板。对应后端：GetTemplateAsync */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktFlowSchemes/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/** 导入流程方案。对应后端：ImportAsync */
export function importSchemes(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktFlowSchemes/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/** 导出流程方案。对应后端：ExportAsync */
export function exportSchemes(
  query: FlowSchemeQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktFlowSchemes/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
