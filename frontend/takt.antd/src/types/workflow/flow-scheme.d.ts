// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/workflow/flow-scheme
// 文件名称：flow-scheme.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：flow-scheme相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * FlowScheme类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeDto）
 */
export interface FlowScheme extends TaktEntityBase {
  /** 对应后端字段 schemeId */
  schemeId: string
  /** 对应后端字段 processKey */
  processKey: string
  /** 对应后端字段 processName */
  processName: string
  /** 对应后端字段 processCategory */
  processCategory: number
  /** 对应后端字段 processVersion */
  processVersion: number
  /** 对应后端字段 processDescription */
  processDescription?: string
  /** 对应后端字段 formId */
  formId?: string
  /** 对应后端字段 formCode */
  formCode?: string
  /** 对应后端字段 processContent */
  processContent?: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 processStatus */
  processStatus: number
}

/**
 * FlowSchemeQuery类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeQueryDto）
 */
export interface FlowSchemeQuery extends TaktPagedQuery {
  /** 对应后端字段 processKey */
  processKey?: string
  /** 对应后端字段 processName */
  processName?: string
  /** 对应后端字段 processStatus */
  processStatus?: number
  /** 对应后端字段 processCategory */
  processCategory?: number
  /** 对应后端字段 formCode */
  formCode?: string
}

/**
 * FlowSchemeCreate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeCreateDto）
 */
export interface FlowSchemeCreate {
  /** 对应后端字段 processKey */
  processKey: string
  /** 对应后端字段 processName */
  processName: string
  /** 对应后端字段 processCategory */
  processCategory: number
  /** 对应后端字段 processDescription */
  processDescription?: string
  /** 对应后端字段 formId */
  formId?: string
  /** 对应后端字段 formCode */
  formCode?: string
  /** 对应后端字段 processContent */
  processContent?: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 processStatus */
  processStatus: number
}

/**
 * FlowSchemeUpdate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeUpdateDto）
 */
export interface FlowSchemeUpdate extends FlowSchemeCreate {
  /** 对应后端字段 schemeId */
  schemeId: string
}

/**
 * FlowSchemeStatus类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeStatusDto）
 */
export interface FlowSchemeStatus {
  /** 对应后端字段 schemeId */
  schemeId: string
  /** 对应后端字段 processStatus */
  processStatus: number
  /** 对应后端字段 remark */
  remark?: string
}
