// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/workflow/scheme
// 文件名称：scheme.ts
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：流程方案 API，对应后端 TaktFlowSchemesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  FlowScheme,
  FlowSchemeQuery,
  FlowSchemeCreateOrUpdate,
  FlowSchemeStatusUpdate
} from '@/types/workflow/scheme'

// ========================================
// 流程方案相关 API（按后端控制器顺序）
// ========================================

const schemeUrl = '/api/TaktFlowSchemes'

/**
 * 获取流程方案列表（分页）
 * 对应后端：GetList
 */
export function getFlowSchemeList(params: FlowSchemeQuery): Promise<TaktPagedResult<FlowScheme>> {
  return request({
    url: `${schemeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取流程方案
 * 对应后端：GetById
 */
export function getFlowSchemeById(id: string): Promise<FlowScheme> {
  return request({
    url: `${schemeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 根据流程Key获取流程方案
 * 对应后端：GetByProcessKey
 */
export function getByProcessKey(processKey: string): Promise<FlowScheme> {
  return request({
    url: `${schemeUrl}/by-key/${encodeURIComponent(processKey)}`,
    method: 'get'
  })
}

/**
 * 创建流程方案
 * 对应后端：Create
 */
export function createFlowScheme(data: Omit<FlowSchemeCreateOrUpdate, 'schemeId'>): Promise<FlowScheme> {
  return request({
    url: schemeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新流程方案
 * 对应后端：Update
 */
export function updateFlowScheme(id: string, data: FlowSchemeCreateOrUpdate): Promise<FlowScheme> {
  return request({
    url: `${schemeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除流程方案（单条）
 * 对应后端：Delete
 */
export function deleteFlowSchemeById(id: string): Promise<void> {
  return request({
    url: `${schemeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除流程方案
 * 对应后端：DeleteBatch
 */
export function deleteFlowSchemeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${schemeUrl}/delete`,
    method: 'post',
    data: ids
  })
}

/**
 * 更新流程方案状态
 * 对应后端：UpdateStatus
 */
export function updateFlowSchemeStatus(data: FlowSchemeStatusUpdate): Promise<FlowScheme> {
  return request({
    url: `${schemeUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplate
 */
export function getFlowSchemeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${schemeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入流程方案
 * 对应后端：Import
 */
export function importFlowSchemeData(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${schemeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出流程方案
 * 对应后端：Export
 */
export function exportFlowSchemeData(query: FlowSchemeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${schemeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 创建或更新流程方案（便捷方法：有 schemeId 走 Update，无则走 Create）
 * 对应后端：Create / Update
 */
export function createOrUpdateScheme(data: FlowSchemeCreateOrUpdate): Promise<FlowScheme> {
  if (data.schemeId) {
    return updateFlowScheme(data.schemeId, data)
  }
  return createFlowScheme(data)
}
