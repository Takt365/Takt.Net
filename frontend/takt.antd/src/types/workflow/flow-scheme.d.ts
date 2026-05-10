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
  /** 对应后端字段 flowSchemeId */
  flowSchemeId: string
  /** 对应后端字段 schemeKey */
  schemeKey: string
  /** 对应后端字段 schemeName */
  schemeName: string
  /** 对应后端字段 schemeCategory */
  schemeCategory: number
  /** 对应后端字段 schemeVersion */
  schemeVersion: number
  /** 对应后端字段 schemeDescription */
  schemeDescription?: string
  /** 对应后端字段 formId */
  formId?: string
  /** 对应后端字段 formCode */
  formCode?: string
  /** 对应后端字段 schemeContent */
  schemeContent?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 schemeStatus */
  schemeStatus: number
}

/**
 * FlowSchemeQuery类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeQueryDto）
 */
export interface FlowSchemeQuery extends TaktPagedQuery {
  /** 对应后端字段 schemeKey */
  schemeKey?: string
  /** 对应后端字段 schemeName */
  schemeName?: string
  /** 对应后端字段 schemeCategory */
  schemeCategory?: number
  /** 对应后端字段 schemeVersion */
  schemeVersion?: number
  /** 对应后端字段 schemeDescription */
  schemeDescription?: string
  /** 对应后端字段 formId */
  formId?: string
  /** 对应后端字段 formCode */
  formCode?: string
  /** 对应后端字段 schemeContent */
  schemeContent?: string
  /** 对应后端字段 schemeStatus */
  schemeStatus?: number
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
 * FlowSchemeCreate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeCreateDto）
 */
export interface FlowSchemeCreate {
  /** 对应后端字段 schemeKey */
  schemeKey: string
  /** 对应后端字段 schemeName */
  schemeName: string
  /** 对应后端字段 schemeCategory */
  schemeCategory: number
  /** 对应后端字段 schemeVersion */
  schemeVersion: number
  /** 对应后端字段 schemeDescription */
  schemeDescription?: string
  /** 对应后端字段 formId */
  formId?: string
  /** 对应后端字段 formCode */
  formCode?: string
  /** 对应后端字段 schemeContent */
  schemeContent?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 schemeStatus */
  schemeStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * FlowSchemeUpdate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeUpdateDto）
 */
export interface FlowSchemeUpdate extends FlowSchemeCreate {
  /** 对应后端字段 flowSchemeId */
  flowSchemeId: string
}

/**
 * FlowSchemeSort类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeSortDto）
 */
export interface FlowSchemeSort {
  /** 对应后端字段 flowSchemeId */
  flowSchemeId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * FlowSchemeSchemeStatus类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeSchemeStatusDto）
 */
export interface FlowSchemeSchemeStatus {
  /** 对应后端字段 flowSchemeId */
  flowSchemeId: string
  /** 对应后端字段 schemeStatus */
  schemeStatus: number
}
