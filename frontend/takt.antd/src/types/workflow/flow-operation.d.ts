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
  /** 对应后端字段 operationId */
  operationId: string
  /** 对应后端字段 instanceId */
  instanceId: string
  /** 对应后端字段 schemeId */
  schemeId: string
  /** 对应后端字段 taskId */
  taskId?: string
  /** 对应后端字段 instanceCode */
  instanceCode: string
  /** 对应后端字段 processKey */
  processKey: string
  /** 对应后端字段 processName */
  processName: string
  /** 对应后端字段 operationType */
  operationType: number
  /** 对应后端字段 operationTime */
  operationTime: string
  /** 对应后端字段 operatorId */
  operatorId: string
  /** 对应后端字段 operatorName */
  operatorName: string
  /** 对应后端字段 operatorDeptId */
  operatorDeptId?: string
  /** 对应后端字段 operatorDeptName */
  operatorDeptName?: string
  /** 对应后端字段 nodeId */
  nodeId?: string
  /** 对应后端字段 nodeName */
  nodeName?: string
  /** 对应后端字段 operationContent */
  operationContent?: string
  /** 对应后端字段 operationComment */
  operationComment?: string
  /** 对应后端字段 beforeStatus */
  beforeStatus?: string
  /** 对应后端字段 afterStatus */
  afterStatus?: string
  /** 对应后端字段 operationIp */
  operationIp?: string
  /** 对应后端字段 operationDevice */
  operationDevice?: string
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
  /** 对应后端字段 processKey */
  processKey?: string
  /** 对应后端字段 instanceCode */
  instanceCode?: string
  /** 对应后端字段 operationType */
  operationType?: number
}
