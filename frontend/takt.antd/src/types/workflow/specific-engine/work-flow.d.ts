// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/workflow/specific-engine/work-flow
// 文件名称：work-flow.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：work-flow相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * FlowAddApprover类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowAddApproverDto）
 */
export interface FlowAddApprover {
  /** 对应后端字段 flowAddApproverId */
  flowAddApproverId: string
  /** 对应后端字段 instanceId */
  instanceId: string
  /** 对应后端字段 activityId */
  activityId: string
  /** 对应后端字段 approverUserId */
  approverUserId: string
  /** 对应后端字段 approverUserName */
  approverUserName: string
  /** 对应后端字段 approveType */
  approveType?: string
  /** 对应后端字段 orderNo */
  orderNo?: number
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 verifyComment */
  verifyComment?: string
  /** 对应后端字段 verifyTime */
  verifyTime?: string
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 createUserId */
  createUserId?: string
  /** 对应后端字段 createUserName */
  createUserName?: string
  /** 对应后端字段 returnToSignNode */
  returnToSignNode?: boolean
}

/**
 * FlowAddApprovers类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowAddApproversDto）
 */
export interface FlowAddApprovers {
  /** 对应后端字段 instanceCode */
  instanceCode?: string
  /** 对应后端字段 approvers */
  approvers?: number[]
  /** 对应后端字段 approveType */
  approveType?: number
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 returnToSignNode */
  returnToSignNode?: boolean
}

/**
 * FlowComplete类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowCompleteDto）
 */
export interface FlowComplete {
  /** 对应后端字段 instanceCode */
  instanceCode?: string
  /** 对应后端字段 approved */
  approved?: boolean
  /** 对应后端字段 nodeRejectStep */
  nodeRejectStep?: number
  /** 对应后端字段 selectedAssigneeIds */
  selectedAssigneeIds?: string[]
}

/**
 * FlowFormBindableEntity类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowFormBindableEntityDto）
 */
export interface FlowFormBindableEntity {
  /** 对应后端字段 entityKey */
  entityKey: string
  /** 对应后端字段 displayName */
  displayName: string
}

/**
 * FlowFormStatus类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowFormStatusDto）
 */
export interface FlowFormStatus {
  /** 对应后端字段 formId */
  formId?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * FlowHistoryItem类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowHistoryItemDto）
 */
export interface FlowHistoryItem {
  /** 对应后端字段 id */
  id: string
  /** 对应后端字段 fromNodeName */
  fromNodeName?: string
  /** 对应后端字段 toNodeName */
  toNodeName?: string
  /** 对应后端字段 transitionUserName */
  transitionUserName?: string
  /** 对应后端字段 transitionTime */
  transitionTime?: string
  /** 对应后端字段 transitionComment */
  transitionComment?: string
  /** 对应后端字段 nodeName */
  nodeName: string
  /** 对应后端字段 operationType */
  operationType: string
  /** 对应后端字段 operatorName */
  operatorName?: string
  /** 对应后端字段 comment */
  comment?: string
  /** 对应后端字段 operationTime */
  operationTime: string
}

/**
 * FlowInstanceDetail类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowInstanceDetailDto）
 */
export interface FlowInstanceDetail {
  /** 对应后端字段 currentNodeId */
  currentNodeId?: string
  /** 对应后端字段 history */
  history?: unknown[]
  /** 对应后端字段 canVerify */
  canVerify?: boolean
  /** 对应后端字段 canUndoVerify */
  canUndoVerify?: boolean
  /** 对应后端字段 pendingAddApprovers */
  pendingAddApprovers?: number[]
}

/**
 * FlowInstanceQuery类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowInstanceQueryDto）
 */
export interface FlowInstanceQuery extends TaktPagedQuery {
  /** 对应后端字段 instanceCode */
  instanceCode?: string
  /** 对应后端字段 schemeKey */
  schemeKey?: string
  /** 对应后端字段 schemeId */
  schemeId?: string
  /** 对应后端字段 schemeName */
  schemeName?: string
  /** 对应后端字段 businessKey */
  businessKey?: string
  /** 对应后端字段 businessType */
  businessType?: string
  /** 对应后端字段 startUserId */
  startUserId?: string
  /** 对应后端字段 startUserName */
  startUserName?: string
  /** 对应后端字段 startDeptId */
  startDeptId?: string
  /** 对应后端字段 startDeptName */
  startDeptName?: string
  /** 对应后端字段 startTime */
  startTime?: string
  /** 对应后端字段 startTimeStart */
  startTimeStart?: string
  /** 对应后端字段 startTimeEnd */
  startTimeEnd?: string
  /** 对应后端字段 endTime */
  endTime?: string
  /** 对应后端字段 endTimeStart */
  endTimeStart?: string
  /** 对应后端字段 endTimeEnd */
  endTimeEnd?: string
  /** 对应后端字段 currentNodeId */
  currentNodeId?: string
  /** 对应后端字段 currentNodeName */
  currentNodeName?: string
  /** 对应后端字段 activityName */
  activityName?: string
  /** 对应后端字段 previousNodeId */
  previousNodeId?: string
  /** 对应后端字段 makerList */
  makerList?: string
  /** 对应后端字段 frmData */
  frmData?: string
  /** 对应后端字段 instanceStatus */
  instanceStatus?: number
  /** 对应后端字段 isSuspended */
  isSuspended?: number
  /** 对应后端字段 suspendTime */
  suspendTime?: string
  /** 对应后端字段 suspendTimeStart */
  suspendTimeStart?: string
  /** 对应后端字段 suspendTimeEnd */
  suspendTimeEnd?: string
  /** 对应后端字段 suspendReason */
  suspendReason?: string
  /** 对应后端字段 priority */
  priority?: number
  /** 对应后端字段 processTitle */
  processTitle?: string
  /** 对应后端字段 formId */
  formId?: string
  /** 对应后端字段 formCode */
  formCode?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 createdById */
  createdById?: string
  /** 对应后端字段 createdBy */
  createdBy?: string
  /** 对应后端字段 createdAt */
  createdAt?: string
  /** 对应后端字段 createdAtStart */
  createdAtStart?: string
  /** 对应后端字段 createdAtEnd */
  createdAtEnd?: string
}

/**
 * FlowInstanceUpdate类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowInstanceUpdateDto）
 */
export interface FlowInstanceUpdate extends FlowInstanceCreate {
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
}

/**
 * FlowOperationHistoryItem类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowOperationHistoryItemDto）
 */
export interface FlowOperationHistoryItem {
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 createUserId */
  createUserId?: string
  /** 对应后端字段 createUserName */
  createUserName?: string
  /** 对应后端字段 createdAt */
  createdAt?: string
}

/**
 * FlowReduceApproval类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowReduceApprovalDto）
 */
export interface FlowReduceApproval {
  /** 对应后端字段 instanceCode */
  instanceCode?: string
  /** 对应后端字段 addApproverId */
  addApproverId?: string
}

/**
 * FlowResume类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowResumeDto）
 */
export interface FlowResume {
  /** 对应后端字段 instanceCode */
  instanceCode?: string
}

/**
 * FlowStart类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowStartDto）
 */
export interface FlowStart {
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 schemeKey */
  schemeKey: string
  /** 对应后端字段 businessKey */
  businessKey?: string
  /** 对应后端字段 businessType */
  businessType?: string
  /** 对应后端字段 frmData */
  frmData?: string
  /** 对应后端字段 processTitle */
  processTitle?: string
}

/**
 * FlowStartResult类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowStartResultDto）
 */
export interface FlowStartResult {
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
  /** 对应后端字段 instanceId */
  instanceId: string
  /** 对应后端字段 instanceCode */
  instanceCode: string
  /** 对应后端字段 schemeKey */
  schemeKey: string
  /** 对应后端字段 schemeName */
  schemeName: string
}

/**
 * FlowSuspend类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowSuspendDto）
 */
export interface FlowSuspend {
  /** 对应后端字段 instanceCode */
  instanceCode?: string
}

/**
 * FlowTerminate类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowTerminateDto）
 */
export interface FlowTerminate {
  /** 对应后端字段 instanceCode */
  instanceCode?: string
}

/**
 * FlowTodoItem类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowTodoItemDto）
 */
export interface FlowTodoItem {
  /** 对应后端字段 instanceId */
  instanceId?: string
  /** 对应后端字段 schemeName */
  schemeName?: string
  /** 对应后端字段 nodeId */
  nodeId?: string
  /** 对应后端字段 startUserName */
  startUserName?: string
  /** 对应后端字段 startTime */
  startTime?: string
}

/**
 * FlowTodoQuery类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowTodoQueryDto）
 */
export interface FlowTodoQuery extends TaktPagedQuery {
  /** 对应后端字段 schemeKey */
  schemeKey?: string
  /** 对应后端字段 instanceCode */
  instanceCode?: string
}

/**
 * FlowTransfer类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowTransferDto）
 */
export interface FlowTransfer {
  /** 对应后端字段 instanceCode */
  instanceCode?: string
  /** 对应后端字段 toUserId */
  toUserId?: string
  /** 对应后端字段 toUserName */
  toUserName?: string
  /** 对应后端字段 comment */
  comment?: string
}

/**
 * FlowUndoVerification类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowUndoVerificationDto）
 */
export interface FlowUndoVerification {
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
  /** 对应后端字段 operationId */
  operationId: string
}

/**
 * FlowVerifyCcflowReport类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowVerifyCcflowReportDto）
 */
export interface FlowVerifyCcflowReport {
  /** 对应后端字段 schemeKey */
  schemeKey: string
  /** 对应后端字段 verifyTime */
  verifyTime: string
  /** 对应后端字段 scenarios */
  scenarios: unknown[]
}

/**
 * FlowVerifyScenarioResult类型（对应后端 Takt.Application.Dtos.Workflow.SpecificEngine.TaktFlowVerifyScenarioResultDto）
 */
export interface FlowVerifyScenarioResult {
  /** 对应后端字段 scenarioName */
  scenarioName: string
  /** 对应后端字段 ok */
  ok: boolean
  /** 对应后端字段 message */
  message?: string
  /** 对应后端字段 steps */
  steps: string[]
}
