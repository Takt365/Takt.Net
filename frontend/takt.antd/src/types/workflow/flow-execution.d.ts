// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/workflow/flow-execution
// 文件名称：flow-execution.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：flow-execution相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * FlowExecution类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowExecutionDto）
 */
export interface FlowExecution extends TaktEntityBase {
  /** 对应后端字段 flowExecutionId */
  flowExecutionId: string
  /** 对应后端字段 instanceId */
  instanceId: string
  /** 对应后端字段 schemeId */
  schemeId: string
  /** 对应后端字段 taskId */
  taskId?: string
  /** 对应后端字段 instanceCode */
  instanceCode: string
  /** 对应后端字段 schemeKey */
  schemeKey: string
  /** 对应后端字段 schemeName */
  schemeName: string
  /** 对应后端字段 fromNodeId */
  fromNodeId?: string
  /** 对应后端字段 fromNodeName */
  fromNodeName?: string
  /** 对应后端字段 toNodeId */
  toNodeId: string
  /** 对应后端字段 toNodeName */
  toNodeName: string
  /** 对应后端字段 transitionType */
  transitionType: number
  /** 对应后端字段 transitionTime */
  transitionTime: string
  /** 对应后端字段 transitionUserId */
  transitionUserId: string
  /** 对应后端字段 transitionUserName */
  transitionUserName: string
  /** 对应后端字段 transitionDeptId */
  transitionDeptId?: string
  /** 对应后端字段 transitionDeptName */
  transitionDeptName?: string
  /** 对应后端字段 transitionComment */
  transitionComment?: string
  /** 对应后端字段 transitionAttachments */
  transitionAttachments?: string
  /** 对应后端字段 elapsedMilliseconds */
  elapsedMilliseconds: number
}

/**
 * FlowExecutionQuery类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowExecutionQueryDto）
 */
export interface FlowExecutionQuery extends TaktPagedQuery {
  /** 对应后端字段 instanceId */
  instanceId?: string
  /** 对应后端字段 schemeId */
  schemeId?: string
  /** 对应后端字段 taskId */
  taskId?: string
  /** 对应后端字段 instanceCode */
  instanceCode?: string
  /** 对应后端字段 schemeKey */
  schemeKey?: string
  /** 对应后端字段 schemeName */
  schemeName?: string
  /** 对应后端字段 fromNodeId */
  fromNodeId?: string
  /** 对应后端字段 fromNodeName */
  fromNodeName?: string
  /** 对应后端字段 toNodeId */
  toNodeId?: string
  /** 对应后端字段 toNodeName */
  toNodeName?: string
  /** 对应后端字段 transitionType */
  transitionType?: number
  /** 对应后端字段 transitionTime */
  transitionTime?: string
  /** 对应后端字段 transitionTimeStart */
  transitionTimeStart?: string
  /** 对应后端字段 transitionTimeEnd */
  transitionTimeEnd?: string
  /** 对应后端字段 transitionUserId */
  transitionUserId?: string
  /** 对应后端字段 transitionUserName */
  transitionUserName?: string
  /** 对应后端字段 transitionDeptId */
  transitionDeptId?: string
  /** 对应后端字段 transitionDeptName */
  transitionDeptName?: string
  /** 对应后端字段 transitionComment */
  transitionComment?: string
  /** 对应后端字段 transitionAttachments */
  transitionAttachments?: string
  /** 对应后端字段 elapsedMilliseconds */
  elapsedMilliseconds?: number
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
 * FlowExecutionCreate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowExecutionCreateDto）
 */
export interface FlowExecutionCreate {
  /** 对应后端字段 instanceId */
  instanceId: string
  /** 对应后端字段 schemeId */
  schemeId: string
  /** 对应后端字段 taskId */
  taskId?: string
  /** 对应后端字段 instanceCode */
  instanceCode: string
  /** 对应后端字段 schemeKey */
  schemeKey: string
  /** 对应后端字段 schemeName */
  schemeName: string
  /** 对应后端字段 fromNodeId */
  fromNodeId?: string
  /** 对应后端字段 fromNodeName */
  fromNodeName?: string
  /** 对应后端字段 toNodeId */
  toNodeId: string
  /** 对应后端字段 toNodeName */
  toNodeName: string
  /** 对应后端字段 transitionType */
  transitionType: number
  /** 对应后端字段 transitionTime */
  transitionTime: string
  /** 对应后端字段 transitionUserId */
  transitionUserId: string
  /** 对应后端字段 transitionUserName */
  transitionUserName: string
  /** 对应后端字段 transitionDeptId */
  transitionDeptId?: string
  /** 对应后端字段 transitionDeptName */
  transitionDeptName?: string
  /** 对应后端字段 transitionComment */
  transitionComment?: string
  /** 对应后端字段 transitionAttachments */
  transitionAttachments?: string
  /** 对应后端字段 elapsedMilliseconds */
  elapsedMilliseconds: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * FlowExecutionUpdate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowExecutionUpdateDto）
 */
export interface FlowExecutionUpdate extends FlowExecutionCreate {
  /** 对应后端字段 flowExecutionId */
  flowExecutionId: string
}
