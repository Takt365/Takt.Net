// ========================================
// ????????????�Takt Digital Factory (TDF)
// ?????@/api/workflow/instance
// ?????instance.ts
// ??????025-02-27
// ????Takt365(Cursor AI)
// ???????????? API??????TaktFlowInstancesController
//
// ?????Copyright (c) 2025 Takt  All rights reserved.
// ?????????? MIT License??????????????
// ========================================

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  FlowStart as FlowStartRequest,
  FlowStartResult,
  FlowInstance,
  FlowInstanceDetail,
  FlowInstanceQuery,
  FlowTodoItem,
  FlowTodoQuery,
  FlowComplete as FlowCompleteRequest,
  FlowInstanceOperate as FlowOperateBase,
  FlowInstanceUpdate,
  FlowUndoVerification,
  FlowSuspend,
  FlowResume,
  FlowTerminate,
  FlowTransfer,
  FlowAddApprovers,
  FlowReduceApproval,
  FlowOperationHistoryItem
} from '@/types/workflow/flow-instance'

// ========================================
// ?????? API??????????
// ========================================

const instanceUrl = '/api/TaktFlowInstances'

/**
 * ????????????
 * ?????????GetFlowInstanceListAsync?TaktFlowInstancesController.GetList??
 */
export function getFlowInstanceList(params: FlowInstanceQuery): Promise<TaktPagedResult<FlowInstance>> {
  return request({
    url: `${instanceUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * ?? ID ????????
 * ?????????GetFlowInstanceByIdAsync?TaktFlowInstancesController.GetDetail??
 *
 * @param {string} id - ???? ID
 * @returns {Promise<FlowInstanceDetail>} ????
 */
export function getFlowInstanceById(id: string): Promise<FlowInstanceDetail> {
  return request({
    url: `${instanceUrl}/${id}`,
    method: 'get'
  })
}

/**
 * ??????????
 * ?????????GetFlowInstanceOperationHistoriesAsync?TaktFlowInstancesController.GetInstanceHistories??
 */
export function getFlowInstanceOperationHistories(id: string): Promise<FlowOperationHistoryItem[]> {
  return request({
    url: `${instanceUrl}/${id}/histories`,
    method: 'get'
  })
}

/**
 * ??????????
 * ?????????GetFlowInstanceTodoListAsync?TaktFlowInstancesController.GetMyTodo??
 */
export function getFlowInstanceTodoList(params: FlowTodoQuery): Promise<TaktPagedResult<FlowTodoItem>> {
  return request({
    url: `${instanceUrl}/todo`,
    method: 'get',
    params
  })
}

/**
 * ????????????
 * ?????????GetFlowInstanceMyListAsync?TaktFlowInstancesController.GetMyInstances??
 */
export function getFlowInstanceMyList(params: FlowInstanceQuery): Promise<TaktPagedResult<FlowInstance>> {
  return request({
    url: `${instanceUrl}/my`,
    method: 'get',
    params
  })
}

/**
 * ??????????
 * ?????????GetFlowInstanceProcessedListAsync?TaktFlowInstancesController.GetMyProcessed??
 */
export function getFlowInstanceProcessedList(params: FlowInstanceQuery): Promise<TaktPagedResult<FlowInstance>> {
  return request({
    url: `${instanceUrl}/processed`,
    method: 'get',
    params
  })
}

/**
 * ?????????Excel??
 * ?????????ExportFlowInstanceAsync?TaktFlowInstancesController.Export??
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
 * ???????Excel??
 * ?????????ExportFlowInstanceTodoAsync?TaktFlowInstancesController.ExportTodo??
 */
export function exportFlowInstanceTodo(params: FlowTodoQuery & { sheetName?: string; fileName?: string }): Promise<Blob> {
  return request({
    url: `${instanceUrl}/todo/export`,
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * ?????????Excel??
 * ?????????ExportFlowInstanceMyAsync?TaktFlowInstancesController.ExportMy??
 */
export function exportFlowInstanceMy(params: FlowInstanceQuery & { sheetName?: string; fileName?: string }): Promise<Blob> {
  return request({
    url: `${instanceUrl}/my/export`,
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * ???????Excel??
 * ?????????ExportFlowInstanceProcessedAsync?TaktFlowInstancesController.ExportProcessed??
 */
export function exportFlowInstanceProcessed(params: FlowInstanceQuery & { sheetName?: string; fileName?: string }): Promise<Blob> {
  return request({
    url: `${instanceUrl}/processed/export`,
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * ?????????????? FlowInstanceId ??????
 * ?????????StartFlowInstanceAsync?TaktFlowInstancesController.Start??
 */
export function startFlowInstance(data: FlowStartRequest): Promise<FlowStartResult> {
  return request({
    url: `${instanceUrl}/start`,
    method: 'post',
    data
  })
}

/**
 * ??????
 * ?????????CreateFlowInstanceDraftAsync?TaktFlowInstancesController.CreateDraft??
 */
export function createFlowInstanceDraft(data: FlowStartRequest): Promise<FlowStartResult> {
  return request({
    url: `${instanceUrl}/create-draft`,
    method: 'post',
    data
  })
}

/**
 * ??????
 * ?????????StartFlowInstanceFromDraftAsync?TaktFlowInstancesController.StartFromDraft??
 */
export function startFlowInstanceFromDraft(id: string): Promise<FlowStartResult> {
  return request({
    url: `${instanceUrl}/start-from-draft/${id}`,
    method: 'post'
  })
}

/**
 * ????
 * ?????????CompleteFlowInstanceTaskAsync?TaktFlowInstancesController.Complete??
 */
export function completeFlowInstanceTask(data: FlowCompleteRequest): Promise<void> {
  return request({
    url: `${instanceUrl}/complete`,
    method: 'post',
    data
  })
}

/** ???????InstanceCode ??FlowInstanceId ?????*/
export interface RevokeRequest extends FlowOperateBase {
  description?: string
}

/**
 * ????
 * ?????????ResolveFlowInstanceCodeAsync + RevokeFlowInstanceAsync?TaktFlowInstancesController.Revoke??
 */
export function revokeFlowInstance(payload: RevokeRequest | string): Promise<void> {
  const data = typeof payload === 'string' ? { instanceCode: payload } : payload
  return request({
    url: `${instanceUrl}/revoke`,
    method: 'post',
    data
  })
}

/**
 * ????
 * ?????????SuspendFlowInstanceAsync?TaktFlowInstancesController.Suspend??
 */
export function suspendFlowInstance(data: FlowSuspend): Promise<void> {
  return request({
    url: `${instanceUrl}/suspend`,
    method: 'post',
    data
  })
}

/**
 * ????
 * ?????????ResumeFlowInstanceAsync?TaktFlowInstancesController.Resume??
 */
export function resumeFlowInstance(data: FlowResume): Promise<void> {
  return request({
    url: `${instanceUrl}/resume`,
    method: 'post',
    data
  })
}

/**
 * ????
 * ?????????TerminateFlowInstanceAsync?TaktFlowInstancesController.Terminate??
 */
export function terminateFlowInstance(data: FlowTerminate): Promise<void> {
  return request({
    url: `${instanceUrl}/terminate`,
    method: 'post',
    data
  })
}

/**
 * ??
 * ?????????TransferFlowInstanceAsync?TaktFlowInstancesController.Transfer??
 */
export function transferFlowInstance(data: FlowTransfer): Promise<void> {
  return request({
    url: `${instanceUrl}/transfer`,
    method: 'post',
    data
  })
}

/**
 * ??
 * ?????????AddFlowInstanceApproversAsync?TaktFlowInstancesController.AddApprovers??
 */
export function addFlowInstanceApprovers(data: FlowAddApprovers): Promise<void> {
  return request({
    url: `${instanceUrl}/add-sign`,
    method: 'post',
    data
  })
}

/**
 * ??
 * ?????????ReduceFlowInstanceApprovalAsync?TaktFlowInstancesController.ReduceApproval??
 */
export function reduceFlowInstanceApproval(data: FlowReduceApproval): Promise<void> {
  return request({
    url: `${instanceUrl}/reduce-sign`,
    method: 'post',
    data
  })
}

/**
 * ????????????????????????????
 * ?????????UpdateFlowInstanceAsync?TaktFlowInstancesController.Update??
 */
export function updateFlowInstance(data: FlowInstanceUpdate): Promise<void> {
  return request({
    url: `${instanceUrl}/update`,
    method: 'put',
    data
  })
}

/**
 * ????????
 * ?????????UndoFlowInstanceVerificationAsync?TaktFlowInstancesController.UndoVerification??
 */
export function undoFlowInstanceVerification(data: FlowUndoVerification): Promise<void> {
  return request({
    url: `${instanceUrl}/undo-verification`,
    method: 'post',
    data
  })
}

/**
 * ??????????
 * ?????????DeleteFlowInstanceByIdAsync?TaktFlowInstancesController.Delete??
 */
export function deleteFlowInstanceById(id: string): Promise<void> {
  return request({
    url: `${instanceUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * ????????
 * ?????????DeleteFlowInstanceBatchAsync?TaktFlowInstancesController.DeleteBatch??
 */
export function deleteFlowInstanceBatch(ids: string[]): Promise<void> {
  return request({
    url: `${instanceUrl}/delete`,
    method: 'post',
    data: ids
  })
}

/**
 * ??????????Key ????????????
 * ?????TaktFlowInstancesController.Verify??????StartFlowInstanceAsync?GetFlowInstanceTodoListAsync?CompleteFlowInstanceTaskAsync?GetFlowInstanceByIdAsync ??
 */
export function verify(processKey: string, processTitle?: string): Promise<{ ok: boolean; message?: string; instanceCode?: string; instanceId?: number; steps: string[] }> {
  return request({
    url: `${instanceUrl}/verify`,
    method: 'get',
    params: { processKey, processTitle }
  })
}

/** ?? CCFLOW ???????? */
export interface FlowVerifyScenarioResult {
  scenarioName: string
  ok: boolean
  message?: string
  steps: string[]
}

/** ?????? CCFLOW ?????? */
export interface FlowVerifyCcflowReport {
  processKey: string
  verifyTime: string
  scenarios: FlowVerifyScenarioResult[]
  allPassed: boolean
}

/**
 * ?????? CCFLOW ????????????????
 * ?????TaktFlowInstancesController.VerifyCcflow??????StartFlowInstanceAsync?GetFlowInstanceTodoListAsync?CompleteFlowInstanceTaskAsync?RevokeFlowInstanceAsync ??
 */
export function verifyCcflow(processKey: string, processTitle?: string): Promise<FlowVerifyCcflowReport> {
  return request({
    url: `${instanceUrl}/verify-ccflow`,
    method: 'get',
    params: { processKey, processTitle }
  })
}

