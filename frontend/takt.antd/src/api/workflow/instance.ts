// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/workflow/instance
// 文件名称：instance.ts
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：流程实例与待办 API，对应后端 TaktFlowInstancesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  FlowStartRequest,
  FlowStartResult,
  FlowInstance,
  FlowInstanceDetail,
  FlowInstanceQuery,
  FlowTodoItem,
  FlowTodoQuery,
  FlowCompleteRequest,
  FlowOperateBase,
  FlowInstanceUpdate,
  FlowUndoVerification,
  FlowSuspend,
  FlowResume,
  FlowTerminate,
  FlowTransfer,
  FlowAddApprovers,
  FlowReduceApproval,
  FlowOperationHistoryItem
} from '@/types/workflow/instance'

// ========================================
// 流程实例相关 API（按后端控制器顺序）
// ========================================

const instanceUrl = '/api/TaktFlowInstances'

/**
 * 获取流程实例列表（分页）
 * 对应后端：GetList
 */
export function getFlowInstanceList(params: FlowInstanceQuery): Promise<TaktPagedResult<FlowInstance>> {
  return request({
    url: `${instanceUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据 ID 获取流程实例详情
 * 对应后端：GetDetail
 *
 * @param {string} id - 流程实例 ID
 * @returns {Promise<FlowInstanceDetail>} 实例详情
 */
export function getFlowInstanceById(id: string): Promise<FlowInstanceDetail> {
  return request({
    url: `${instanceUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取流程实例操作历史
 * 对应后端：GetInstanceHistories
 */
export function getInstanceHistories(id: string): Promise<FlowOperationHistoryItem[]> {
  return request({
    url: `${instanceUrl}/${id}/histories`,
    method: 'get'
  })
}

/**
 * 获取待办列表（分页）
 * 对应后端：GetMyTodo
 */
export function getMyTodo(params: FlowTodoQuery): Promise<TaktPagedResult<FlowTodoItem>> {
  return request({
    url: `${instanceUrl}/todo`,
    method: 'get',
    params
  })
}

/**
 * 获取我的流程列表（分页）
 * 对应后端：GetMyInstances
 */
export function getMyInstances(params: FlowInstanceQuery): Promise<TaktPagedResult<FlowInstance>> {
  return request({
    url: `${instanceUrl}/my`,
    method: 'get',
    params
  })
}

/**
 * 获取已办列表（分页）
 * 对应后端：GetMyProcessed
 */
export function getMyProcessed(params: FlowInstanceQuery): Promise<TaktPagedResult<FlowInstance>> {
  return request({
    url: `${instanceUrl}/processed`,
    method: 'get',
    params
  })
}

/**
 * 导出流程实例列表（Excel）
 * 对应后端：Export
 */
export function exportFlowInstanceData(params: FlowInstanceQuery & { sheetName?: string; fileName?: string }): Promise<Blob> {
  return request({
    url: `${instanceUrl}/export`,
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导出待办列表（Excel）
 * 对应后端：ExportTodo
 */
export function exportTodo(params: FlowTodoQuery & { sheetName?: string; fileName?: string }): Promise<Blob> {
  return request({
    url: `${instanceUrl}/todo/export`,
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导出我的流程列表（Excel）
 * 对应后端：ExportMy
 */
export function exportMy(params: FlowInstanceQuery & { sheetName?: string; fileName?: string }): Promise<Blob> {
  return request({
    url: `${instanceUrl}/my/export`,
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导出已办列表（Excel）
 * 对应后端：ExportProcessed
 */
export function exportProcessed(params: FlowInstanceQuery & { sheetName?: string; fileName?: string }): Promise<Blob> {
  return request({
    url: `${instanceUrl}/processed/export`,
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 启动流程（新建并启动，或传入 FlowInstanceId 从草稿启动）
 * 对应后端：Start
 */
export function start(data: FlowStartRequest): Promise<FlowStartResult> {
  return request({
    url: `${instanceUrl}/start`,
    method: 'post',
    data
  })
}

/**
 * 创建草稿实例
 * 对应后端：CreateDraft
 */
export function createDraft(data: FlowStartRequest): Promise<FlowStartResult> {
  return request({
    url: `${instanceUrl}/create-draft`,
    method: 'post',
    data
  })
}

/**
 * 从草稿启动
 * 对应后端：StartFromDraft
 */
export function startFromDraft(id: string): Promise<FlowStartResult> {
  return request({
    url: `${instanceUrl}/start-from-draft/${id}`,
    method: 'post'
  })
}

/**
 * 办结任务
 * 对应后端：Complete
 */
export function complete(data: FlowCompleteRequest): Promise<void> {
  return request({
    url: `${instanceUrl}/complete`,
    method: 'post',
    data
  })
}

/** 撤回请求参数（InstanceCode 与 FlowInstanceId 二选一） */
export interface RevokeRequest extends FlowOperateBase {
  description?: string
}

/**
 * 撤回流程
 * 对应后端：Revoke
 */
export function revoke(payload: RevokeRequest | string): Promise<void> {
  const data = typeof payload === 'string' ? { instanceCode: payload } : payload
  return request({
    url: `${instanceUrl}/revoke`,
    method: 'post',
    data
  })
}

/**
 * 挂起流程
 * 对应后端：Suspend
 */
export function suspend(data: FlowSuspend): Promise<void> {
  return request({
    url: `${instanceUrl}/suspend`,
    method: 'post',
    data
  })
}

/**
 * 恢复流程
 * 对应后端：Resume
 */
export function resume(data: FlowResume): Promise<void> {
  return request({
    url: `${instanceUrl}/resume`,
    method: 'post',
    data
  })
}

/**
 * 终止流程
 * 对应后端：Terminate
 */
export function terminate(data: FlowTerminate): Promise<void> {
  return request({
    url: `${instanceUrl}/terminate`,
    method: 'post',
    data
  })
}

/**
 * 转办
 * 对应后端：Transfer
 */
export function transfer(data: FlowTransfer): Promise<void> {
  return request({
    url: `${instanceUrl}/transfer`,
    method: 'post',
    data
  })
}

/**
 * 加签
 * 对应后端：AddApprovers
 */
export function addApprovers(data: FlowAddApprovers): Promise<void> {
  return request({
    url: `${instanceUrl}/add-sign`,
    method: 'post',
    data
  })
}

/**
 * 减签
 * 对应后端：ReduceApproval
 */
export function reduceApproval(data: FlowReduceApproval): Promise<void> {
  return request({
    url: `${instanceUrl}/reduce-sign`,
    method: 'post',
    data
  })
}

/**
 * 更新流程实例（仅运行中且发起人可更新流程标题与表单数据）
 * 对应后端：Update
 */
export function updateFlowInstance(data: FlowInstanceUpdate): Promise<void> {
  return request({
    url: `${instanceUrl}/update`,
    method: 'put',
    data
  })
}

/**
 * 撤销当前节点审批
 * 对应后端：UndoVerification
 */
export function undoVerification(data: FlowUndoVerification): Promise<void> {
  return request({
    url: `${instanceUrl}/undo-verification`,
    method: 'post',
    data
  })
}

/**
 * 删除流程实例（单条）
 * 对应后端：Delete
 */
export function deleteFlowInstanceById(id: string): Promise<void> {
  return request({
    url: `${instanceUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除流程实例
 * 对应后端：DeleteBatch
 */
export function deleteFlowInstanceBatch(ids: string[]): Promise<void> {
  return request({
    url: `${instanceUrl}/delete`,
    method: 'post',
    data: ids
  })
}

/**
 * 工作流验证（按流程 Key 启动并办结，校验引擎）
 * 对应后端：Verify
 */
export function verify(processKey: string, processTitle?: string): Promise<{ ok: boolean; message?: string; instanceCode?: string; instanceId?: number; steps: string[] }> {
  return request({
    url: `${instanceUrl}/verify`,
    method: 'get',
    params: { processKey, processTitle }
  })
}

/** 单条 CCFLOW 对照验证场景结果 */
export interface FlowVerifyScenarioResult {
  scenarioName: string
  ok: boolean
  message?: string
  steps: string[]
}

/** 当前工作流与 CCFLOW 对照验证报告 */
export interface FlowVerifyCcflowReport {
  processKey: string
  verifyTime: string
  scenarios: FlowVerifyScenarioResult[]
  allPassed: boolean
}

/**
 * 当前工作流与 CCFLOW 对照验证（多场景执行并返回报告）
 * 对应后端：VerifyCcflow
 */
export function verifyCcflow(processKey: string, processTitle?: string): Promise<FlowVerifyCcflowReport> {
  return request({
    url: `${instanceUrl}/verify-ccflow`,
    method: 'get',
    params: { processKey, processTitle }
  })
}
