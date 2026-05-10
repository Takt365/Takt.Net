// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/workflow/flow-add-approver
// 文件名称：flow-add-approver.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：flow-add-approver相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * FlowAddApprover类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowAddApproverDto）
 */
export interface FlowAddApprover extends TaktEntityBase {
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
 * FlowAddApproverQuery类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowAddApproverQueryDto）
 */
export interface FlowAddApproverQuery extends TaktPagedQuery {
  /** 对应后端字段 instanceId */
  instanceId?: string
  /** 对应后端字段 activityId */
  activityId?: string
  /** 对应后端字段 approverUserId */
  approverUserId?: string
  /** 对应后端字段 approverUserName */
  approverUserName?: string
  /** 对应后端字段 approveType */
  approveType?: string
  /** 对应后端字段 orderNo */
  orderNo?: number
  /** 对应后端字段 status */
  status?: number
  /** 对应后端字段 verifyComment */
  verifyComment?: string
  /** 对应后端字段 verifyTime */
  verifyTime?: string
  /** 对应后端字段 verifyTimeStart */
  verifyTimeStart?: string
  /** 对应后端字段 verifyTimeEnd */
  verifyTimeEnd?: string
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 createUserId */
  createUserId?: string
  /** 对应后端字段 createUserName */
  createUserName?: string
  /** 对应后端字段 returnToSignNode */
  returnToSignNode?: boolean
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
 * FlowAddApproverCreate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowAddApproverCreateDto）
 */
export interface FlowAddApproverCreate {
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
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * FlowAddApproverUpdate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowAddApproverUpdateDto）
 */
export interface FlowAddApproverUpdate extends FlowAddApproverCreate {
  /** 对应后端字段 flowAddApproverId */
  flowAddApproverId: string
}

/**
 * FlowAddApproverStatus类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowAddApproverStatusDto）
 */
export interface FlowAddApproverStatus {
  /** 对应后端字段 flowAddApproverId */
  flowAddApproverId: string
  /** 对应后端字段 status */
  status: number
}
