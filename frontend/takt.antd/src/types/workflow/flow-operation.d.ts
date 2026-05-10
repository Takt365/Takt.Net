// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/workflow/flow-operation
// 文件名称：flow-operation.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：flow-operation相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * FlowOperation类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowOperationDto）
 */
export interface FlowOperation extends TaktEntityBase {
  /** 对应后端字段 flowOperationId */
  flowOperationId: string
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
  /** 对应后端字段 operationType */
  operationType: number
  /** 对应后端字段 nodeId */
  nodeId?: string
  /** 对应后端字段 nodeName */
  nodeName?: string
  /** 对应后端字段 operationContent */
  operationContent?: string
  /** 对应后端字段 operationComment */
  operationComment?: string
  /** 对应后端字段 operationResult */
  operationResult: number
  /** 对应后端字段 errorMessage */
  errorMessage?: string
}

/**
 * FlowOperationQuery类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowOperationQueryDto）
 */
export interface FlowOperationQuery extends TaktPagedQuery {
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
  /** 对应后端字段 operationType */
  operationType?: number
  /** 对应后端字段 nodeId */
  nodeId?: string
  /** 对应后端字段 nodeName */
  nodeName?: string
  /** 对应后端字段 operationContent */
  operationContent?: string
  /** 对应后端字段 operationComment */
  operationComment?: string
  /** 对应后端字段 operationResult */
  operationResult?: number
  /** 对应后端字段 errorMessage */
  errorMessage?: string
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
 * FlowOperationCreate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowOperationCreateDto）
 */
export interface FlowOperationCreate {
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
  /** 对应后端字段 operationType */
  operationType: number
  /** 对应后端字段 nodeId */
  nodeId?: string
  /** 对应后端字段 nodeName */
  nodeName?: string
  /** 对应后端字段 operationContent */
  operationContent?: string
  /** 对应后端字段 operationComment */
  operationComment?: string
  /** 对应后端字段 operationResult */
  operationResult: number
  /** 对应后端字段 errorMessage */
  errorMessage?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * FlowOperationUpdate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowOperationUpdateDto）
 */
export interface FlowOperationUpdate extends FlowOperationCreate {
  /** 对应后端字段 flowOperationId */
  flowOperationId: string
}
