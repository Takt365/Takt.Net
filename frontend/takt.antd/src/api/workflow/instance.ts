// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/workflow
// 文件名称：instance.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：流程实例 API，对应后端 TaktFlowInstancesController（流程执行）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  FlowInstance,
  FlowInstanceQuery,
  FlowInstanceCreate,
  FlowInstanceUpdate,
  FlowInstanceStatus,
  FlowInstanceApprove,
  FlowInstanceRecall,
  FlowInstanceHistory,
  FlowInstanceHistoryQuery
} from '@/types/workflow/instance'

/** 获取流程实例列表（分页）。对应后端：GetListAsync */
export function getList(params: FlowInstanceQuery): Promise<TaktPagedResult<FlowInstance>> {
  return request({
    url: '/api/TaktFlowInstances/list',
    method: 'get',
    params
  })
}

/** 根据ID获取流程实例。对应后端：GetByIdAsync */
export function getById(id: string): Promise<FlowInstance> {
  return request({
    url: `/api/TaktFlowInstances/${id}`,
    method: 'get'
  })
}

/** 获取流程实例选项列表（仅运行中，用于下拉框等）。对应后端：GetOptionsAsync */
export function getOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktFlowInstances/options',
    method: 'get'
  })
}

/** 创建并启动流程实例。对应后端：CreateAsync */
export function create(data: FlowInstanceCreate): Promise<FlowInstance> {
  return request({
    url: '/api/TaktFlowInstances',
    method: 'post',
    data
  })
}

/** 更新流程实例（仅运行中可更新标题、优先级）。对应后端：UpdateAsync */
export function update(id: string, data: FlowInstanceUpdate): Promise<FlowInstance> {
  return request({
    url: `/api/TaktFlowInstances/${id}`,
    method: 'put',
    data: { ...data, instanceId: id }
  })
}

/** 更新流程实例状态（0=运行中，1=已完成，2=已终止，3=挂起等）。对应后端：UpdateStatusAsync */
export function updateStatus(data: FlowInstanceStatus): Promise<FlowInstance> {
  return request({
    url: '/api/TaktFlowInstances/status',
    method: 'put',
    data
  })
}

/** 审批流程。对应后端：ApproveAsync */
export function approve(data: FlowInstanceApprove): Promise<FlowInstance> {
  return request({
    url: '/api/TaktFlowInstances/Approve',
    method: 'post',
    data
  })
}

/** 撤回流程。对应后端：RecallAsync */
export function recall(data: FlowInstanceRecall): Promise<FlowInstance> {
  return request({
    url: '/api/TaktFlowInstances/Recall',
    method: 'post',
    data
  })
}

/** 分页查询流程实例流转历史。对应后端：GetHistoryListAsync */
export function getHistoryList(
  params: FlowInstanceHistoryQuery
): Promise<TaktPagedResult<FlowInstanceHistory>> {
  return request({
    url: '/api/TaktFlowInstances/History',
    method: 'get',
    params
  })
}

/** 删除流程实例（软删除）。对应后端：DeleteAsync(long id) */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktFlowInstances/${id}`,
    method: 'delete'
  })
}

/** 批量删除流程实例。对应后端：DeleteBatchAsync(IEnumerable<long> ids) */
export function removeBatch(ids: string[] | number[]): Promise<void> {
  const body = ids.map((id) => Number(id))
  return request({
    url: '/api/TaktFlowInstances/batch',
    method: 'delete',
    data: body
  })
}

/** 获取导入模板。对应后端：GetTemplateAsync */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktFlowInstances/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/** 导入流程实例。对应后端：ImportAsync */
export function importInstances(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktFlowInstances/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/** 导出流程实例。对应后端：ExportAsync */
export function exportInstances(
  query: FlowInstanceQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktFlowInstances/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
