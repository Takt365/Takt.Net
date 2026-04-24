// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/workflow/flow-instance
// 文件名称：flow-instance.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：flow-instance相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * FlowInstance类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceDto）
 */
export interface FlowInstance extends TaktEntityBase {
  /** 对应后端字段 instanceId */
  instanceId: string
  /** 对应后端字段 instanceCode */
  instanceCode: string
  /** 对应后端字段 processKey */
  processKey: string
  /** 对应后端字段 processName */
  processName: string
  /** 对应后端字段 schemeId */
  schemeId: string
  /** 对应后端字段 businessKey */
  businessKey?: string
  /** 对应后端字段 businessType */
  businessType?: string
  /** 对应后端字段 startUserId */
  startUserId: string
  /** 对应后端字段 startUserName */
  startUserName: string
  /** 对应后端字段 startDeptId */
  startDeptId?: string
  /** 对应后端字段 startDeptName */
  startDeptName?: string
  /** 对应后端字段 startTime */
  startTime: string
  /** 对应后端字段 endTime */
  endTime?: string
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
  instanceStatus: number
  /** 对应后端字段 isSuspended */
  isSuspended: number
  /** 对应后端字段 suspendTime */
  suspendTime?: string
  /** 对应后端字段 suspendReason */
  suspendReason?: string
  /** 对应后端字段 priority */
  priority: number
  /** 对应后端字段 processTitle */
  processTitle?: string
  /** 对应后端字段 formId */
  formId?: string
  /** 对应后端字段 formCode */
  formCode?: string
}

/**
 * FlowInstanceQuery类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceQueryDto）
 */
export interface FlowInstanceQuery extends TaktPagedQuery {
  /** 对应后端字段 processKey */
  processKey?: string
  /** 对应后端字段 instanceCode */
  instanceCode?: string
  /** 对应后端字段 instanceStatus */
  instanceStatus?: number
  /** 对应后端字段 myStartedOnly */
  myStartedOnly?: boolean
}

/**
 * FlowInstanceUpdate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceUpdateDto）
 */
export interface FlowInstanceUpdate {
  /** 对应后端字段 id */
  id: string
  /** 对应后端字段 processTitle */
  processTitle?: string
  /** 对应后端字段 frmData */
  frmData?: string
}

/**
 * FlowAddApproverItem类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowAddApproverItemDto）
 */
export interface FlowAddApproverItem {
  /** 对应后端字段 approverUserId */
  approverUserId: string
  /** 对应后端字段 approverUserName */
  approverUserName: string
  /** 对应后端字段 orderNo */
  orderNo?: number
}

/**
 * FlowAddApprovers类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowAddApproversDto）
 */
export interface FlowAddApprovers extends FlowInstanceOperate {
  /** 对应后端字段 approvers */
  approvers: unknown[]
  /** 对应后端字段 approveType */
  approveType: string
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 returnToSignNode */
  returnToSignNode: boolean
}

/**
 * FlowComplete类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowCompleteDto）
 */
export interface FlowComplete {
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 instanceCode */
  instanceCode?: string
  /** 对应后端字段 comment */
  comment?: string
  /** 对应后端字段 approved */
  approved: boolean
  /** 对应后端字段 frmData */
  frmData?: string
  /** 对应后端字段 nodeRejectStep */
  nodeRejectStep?: string
  /** 对应后端字段 selectedAssigneeIds */
  selectedAssigneeIds?: string
}

/**
 * FlowHistoryItem类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowHistoryItemDto）
 */
export interface FlowHistoryItem {
  /** 对应后端字段 fromNodeName */
  fromNodeName: string
  /** 对应后端字段 toNodeName */
  toNodeName: string
  /** 对应后端字段 transitionUserName */
  transitionUserName: string
  /** 对应后端字段 transitionTime */
  transitionTime: string
  /** 对应后端字段 transitionComment */
  transitionComment?: string
}

/**
 * FlowInstanceDelete类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceDeleteDto）
 */
export interface FlowInstanceDelete {
  /** 对应后端字段 ids */
  ids: string[]
}

/**
 * FlowInstanceDetail类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceDetailDto）
 */
export interface FlowInstanceDetail extends FlowInstance {
  /** 对应后端字段 history */
  history: unknown[]
  /** 对应后端字段 canVerify */
  canVerify: boolean
  /** 对应后端字段 canUndoVerify */
  canUndoVerify: boolean
  /** 对应后端字段 pendingAddApprovers */
  pendingAddApprovers: unknown[]
}

/**
 * FlowInstanceListQuery类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceListQueryDto）
 */
export interface FlowInstanceListQuery extends TaktPagedQuery {
  /** 对应后端字段 processKey */
  processKey?: string
  /** 对应后端字段 instanceCode */
  instanceCode?: string
  /** 对应后端字段 instanceStatus */
  instanceStatus?: number
  /** 对应后端字段 type */
  type?: string
}

/**
 * FlowInstanceOperate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceOperateDto）
 */
export interface FlowInstanceOperate {
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 instanceCode */
  instanceCode?: string
}

/**
 * FlowOperationHistoryItem类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowOperationHistoryItemDto）
 */
export interface FlowOperationHistoryItem {
  /** 对应后端字段 content */
  content: string
  /** 对应后端字段 createUserId */
  createUserId: string
  /** 对应后端字段 createUserName */
  createUserName: string
  /** 对应后端字段 createdAt */
  createdAt: string
}

/**
 * FlowReduceApproval类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowReduceApprovalDto）
 */
export interface FlowReduceApproval extends FlowInstanceOperate {
  /** 对应后端字段 addApproverId */
  addApproverId: string
}

/**
 * FlowResume类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowResumeDto）
 */
export interface FlowResume extends FlowInstanceOperate {
}

/**
 * FlowStart类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowStartDto）
 */
export interface FlowStart {
  /** 对应后端字段 processKey */
  processKey: string
  /** 对应后端字段 businessKey */
  businessKey?: string
  /** 对应后端字段 businessType */
  businessType?: string
  /** 对应后端字段 processTitle */
  processTitle?: string
  /** 对应后端字段 frmData */
  frmData?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
}

/**
 * FlowStartResult类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowStartResultDto）
 */
export interface FlowStartResult {
  /** 对应后端字段 instanceCode */
  instanceCode: string
  /** 对应后端字段 instanceId */
  instanceId: string
  /** 对应后端字段 processKey */
  processKey: string
  /** 对应后端字段 processName */
  processName: string
}

/**
 * FlowSuspend类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSuspendDto）
 */
export interface FlowSuspend extends FlowInstanceOperate {
  /** 对应后端字段 reason */
  reason?: string
}

/**
 * FlowTerminate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowTerminateDto）
 */
export interface FlowTerminate extends FlowInstanceOperate {
  /** 对应后端字段 reason */
  reason?: string
}

/**
 * FlowTodoItem类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowTodoItemDto）
 */
export interface FlowTodoItem {
  /** 对应后端字段 instanceId */
  instanceId: string
  /** 对应后端字段 instanceCode */
  instanceCode: string
  /** 对应后端字段 processKey */
  processKey: string
  /** 对应后端字段 processName */
  processName: string
  /** 对应后端字段 nodeId */
  nodeId: string
  /** 对应后端字段 nodeName */
  nodeName: string
  /** 对应后端字段 processTitle */
  processTitle?: string
  /** 对应后端字段 startUserName */
  startUserName: string
  /** 对应后端字段 startTime */
  startTime: string
}

/**
 * FlowTodoQuery类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowTodoQueryDto）
 */
export interface FlowTodoQuery extends TaktPagedQuery {
  /** 对应后端字段 processKey */
  processKey?: string
}

/**
 * FlowTransfer类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowTransferDto）
 */
export interface FlowTransfer extends FlowInstanceOperate {
  /** 对应后端字段 toUserId */
  toUserId: string
  /** 对应后端字段 toUserName */
  toUserName: string
  /** 对应后端字段 comment */
  comment?: string
}

/**
 * FlowUndoVerification类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowUndoVerificationDto）
 */
export interface FlowUndoVerification {
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
}

/**
 * FlowVerifyCcflowReport类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowVerifyCcflowReportDto）
 */
export interface FlowVerifyCcflowReport {
  /** 对应后端字段 processKey */
  processKey: string
  /** 对应后端字段 verifyTime */
  verifyTime: string
  /** 对应后端字段 scenarios */
  scenarios: unknown[]
}

/**
 * FlowVerifyScenarioResult类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowVerifyScenarioResultDto）
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
