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
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
  /** 对应后端字段 instanceCode */
  instanceCode: string
  /** 对应后端字段 schemeKey */
  schemeKey: string
  /** 对应后端字段 schemeId */
  schemeId: string
  /** 对应后端字段 schemeName */
  schemeName: string
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
 * FlowInstanceCreate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceCreateDto）
 */
export interface FlowInstanceCreate {
  /** 对应后端字段 instanceCode */
  instanceCode: string
  /** 对应后端字段 schemeKey */
  schemeKey: string
  /** 对应后端字段 schemeId */
  schemeId: string
  /** 对应后端字段 schemeName */
  schemeName: string
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
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * FlowInstanceUpdate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceUpdateDto）
 */
export interface FlowInstanceUpdate extends FlowInstanceCreate {
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
}

/**
 * FlowInstanceInstanceStatus类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceInstanceStatusDto）
 */
export interface FlowInstanceInstanceStatus {
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
  /** 对应后端字段 instanceStatus */
  instanceStatus: number
}
