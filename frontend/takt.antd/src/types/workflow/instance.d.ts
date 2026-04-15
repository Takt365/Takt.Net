// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/workflow/instance
// 文件名称：instance.d.ts
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流实例与待办类型定义，对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceDto 等
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/**
 * 工作流实例与待办类型定义
 * 对应后端 TaktFlowInstanceDto/TaktFlowInstanceDetailDto/TaktFlowStartDto/TaktFlowStartResultDto/
 * TaktFlowTodoItemDto/TaktFlowTodoQueryDto/TaktFlowCompleteDto 等
 */

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 流程实例（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceDto）
 */
export interface FlowInstance extends TaktEntityBase {
  /** 实例ID（对应后端 Id/InstanceId，后端 long，前端用 string 以避免精度问题） */
  instanceId: string
  /** 实例编码 */
  instanceCode: string
  /** 流程Key */
  processKey: string
  /** 流程名称 */
  processName: string
  /** 流程方案ID（对应后端 SchemeId） */
  schemeId: string
  /** 业务主键 */
  businessKey?: string
  /** 业务类型 */
  businessType?: string
  /** 启动人ID（对应后端 StartUserId，后端 long，前端用 string） */
  startUserId: string
  /** 启动人姓名 */
  startUserName: string
  /** 启动部门ID（对应后端 StartDeptId，后端 long，前端用 string） */
  startDeptId?: string
  /** 启动部门名称 */
  startDeptName?: string
  /** 启动时间 */
  startTime: string
  /** 结束时间 */
  endTime?: string
  /** 当前节点ID */
  currentNodeId?: string
  /** 当前节点名称 */
  currentNodeName?: string
  /** 当前节点展示名称（活动名称） */
  activityName?: string
  /** 上一节点ID */
  previousNodeId?: string
  /** 当前节点执行人ID列表（逗号分隔） */
  makerList?: string
  /** 表单数据（JSON） */
  frmData?: string
  /** 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回，5=草稿） */
  instanceStatus: number
  /** 是否挂起（0=否，1=是） */
  isSuspended?: number
  /** 挂起时间 */
  suspendTime?: string
  /** 挂起原因 */
  suspendReason?: string
  /** 优先级（0=低，1=中，2=高，3=紧急） */
  priority?: number
  /** 流程标题 */
  processTitle?: string
  /** 流程表单ID（启动时从方案快照） */
  formId?: string
  /** 流程表单编码（启动时从方案快照） */
  formCode?: string
}

/**
 * 流程实例查询（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceQueryDto）
 */
export interface FlowInstanceQuery extends TaktPagedQuery {
  /** 流程Key */
  processKey?: string
  /** 实例编码 */
  instanceCode?: string
  /** 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回） */
  instanceStatus?: number
  /** 仅查我发起的 */
  myStartedOnly?: boolean
}

/**
 * 启动流程请求（对应后端 Takt.Application.Dtos.Workflow.TaktFlowStartDto）
 */
export interface FlowStartRequest {
  /** 流程Key */
  processKey: string
  /** 业务主键 */
  businessKey?: string
  /** 业务类型 */
  businessType?: string
  /** 流程标题 */
  processTitle?: string
  /** 表单数据（JSON） */
  frmData?: string
  /** 草稿实例ID（若传入且为草稿，则从草稿启动；对应后端 FlowInstanceId） */
  flowInstanceId?: string
}

/**
 * 启动流程结果（对应后端 Takt.Application.Dtos.Workflow.TaktFlowStartResultDto）
 */
export interface FlowStartResult {
  /** 实例编码 */
  instanceCode: string
  /** 实例ID（后端 long，前端 string） */
  instanceId: string
  /** 流程Key */
  processKey: string
  /** 流程名称 */
  processName: string
}

/**
 * 待办列表项（按实例维度，对应后端 Takt.Application.Dtos.Workflow.TaktFlowTodoItemDto）
 */
export interface FlowTodoItem {
  /** 实例ID（对应后端 InstanceId，后端 long，前端 string） */
  instanceId: string
  /** 实例编码 */
  instanceCode: string
  /** 流程Key */
  processKey: string
  /** 流程名称 */
  processName: string
  /** 节点ID（对应后端 NodeId） */
  nodeId: string
  /** 节点名称（对应后端 NodeName） */
  nodeName: string
  /** 流程标题 */
  processTitle?: string
  /** 启动人姓名 */
  startUserName: string
  /** 启动时间 */
  startTime: string
}

/**
 * 待办查询（对应后端 Takt.Application.Dtos.Workflow.TaktFlowTodoQueryDto）
 */
export interface FlowTodoQuery extends TaktPagedQuery {
  /** 流程Key */
  processKey?: string
}

/**
 * 办结请求（对应后端 Takt.Application.Dtos.Workflow.TaktFlowCompleteDto，按实例标识）
 */
export interface FlowCompleteRequest {
  /** 流程实例ID（与 instanceCode 二选一，对应后端 FlowInstanceId） */
  flowInstanceId?: string
  /** 实例编码（与 flowInstanceId 二选一，对应后端 InstanceCode） */
  instanceCode?: string
  /** 审批意见（对应后端 Comment） */
  comment?: string
  /** 是否通过（对应后端 Approved） */
  approved: boolean
  /** 表单数据（JSON，对应后端 FrmData） */
  frmData?: string
  /** 驳回时指定退回节点ID（对应后端 NodeRejectStep） */
  nodeRejectStep?: string
}

/**
 * 流程轨迹项（对应后端 Takt.Application.Dtos.Workflow.TaktFlowHistoryItemDto）
 */
export interface FlowHistoryItem {
  /** 来源节点名称 */
  fromNodeName: string
  /** 目标节点名称 */
  toNodeName: string
  /** 流转操作人姓名 */
  transitionUserName: string
  /** 流转时间 */
  transitionTime: string
  /** 流转备注 */
  transitionComment?: string
}

/**
 * 实例详情（含轨迹与审批能力，对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceDetailDto）
 */
export interface FlowInstanceDetail extends FlowInstance {
  /** 流转历史列表 */
  history: FlowHistoryItem[]
  /** 当前用户是否可审批 */
  canVerify?: boolean
  /** 当前用户是否可撤销上一节点审批 */
  canUndoVerify?: boolean
  /**
   * 未处理加签记录（对应后端 PendingAddApprovers，Status=0）；用于展示与减签
   */
  pendingAddApprovers?: FlowPendingAddApprover[]
}

/** 加签记录项（与后端 TaktFlowAddApproverDto 对齐，字段 camelCase） */
export interface FlowPendingAddApprover {
  addApproverId: string
  instanceId: string
  activityId: string
  approverUserId: string
  approverUserName: string
  approveType?: string
  orderNo?: number
  status: number
  reason?: string
  createUserName?: string
}

/** 流程实例操作基类（实例标识二选一，对应后端 TaktFlowInstanceOperateDto） */
export interface FlowOperateBase {
  flowInstanceId?: string
  instanceCode?: string
}

/** 流程实例更新（对应后端 TaktFlowInstanceUpdateDto） */
export interface FlowInstanceUpdate {
  id: string
  processTitle?: string
  frmData?: string
}

/** 撤销审批（对应后端 TaktFlowUndoVerificationDto） */
export interface FlowUndoVerification {
  flowInstanceId: string
}

/** 挂起（对应后端 TaktFlowSuspendDto） */
export interface FlowSuspend extends FlowOperateBase {
  reason?: string
}

/** 恢复（对应后端 TaktFlowResumeDto） */
export interface FlowResume extends FlowOperateBase {}

/** 终止（对应后端 TaktFlowTerminateDto） */
export interface FlowTerminate extends FlowOperateBase {
  reason?: string
}

/** 转办（对应后端 TaktFlowTransferDto） */
export interface FlowTransfer extends FlowOperateBase {
  toUserId: string
  toUserName: string
  comment?: string
}

/** 加签单项（对应后端 TaktFlowAddApproverItemDto） */
export interface FlowAddApproverItem {
  approverUserId: string
  approverUserName: string
  orderNo?: number
}

/** 加签（对应后端 TaktFlowAddApproversDto） */
export interface FlowAddApprovers extends FlowOperateBase {
  approvers: FlowAddApproverItem[]
  approveType?: string
  reason?: string
  returnToSignNode?: boolean
}

/** 减签（对应后端 TaktFlowReduceApprovalDto） */
export interface FlowReduceApproval extends FlowOperateBase {
  addApproverId: string
}

/** 操作历史项（对应后端 TaktFlowOperationHistoryItemDto） */
export interface FlowOperationHistoryItem {
  content: string
  createUserId: string
  createUserName: string
  createdAt: string
}

