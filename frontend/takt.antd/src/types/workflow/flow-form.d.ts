// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/workflow/flow-form
// 文件名称：flow-form.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：flow-form相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * FlowForm类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormDto）
 */
export interface FlowForm extends TaktEntityBase {
  /** 对应后端字段 flowFormId */
  flowFormId: string
  /** 对应后端字段 formCode */
  formCode: string
  /** 对应后端字段 formName */
  formName: string
  /** 对应后端字段 formCategory */
  formCategory: number
  /** 对应后端字段 formType */
  formType: number
  /** 对应后端字段 formConfig */
  formConfig?: string
  /** 对应后端字段 formTemplate */
  formTemplate?: string
  /** 对应后端字段 formVersion */
  formVersion: string
  /** 对应后端字段 isDatasource */
  isDatasource: number
  /** 对应后端字段 relatedDataBaseName */
  relatedDataBaseName?: string
  /** 对应后端字段 relatedTableName */
  relatedTableName?: string
  /** 对应后端字段 relatedFormField */
  relatedFormField?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 formStatus */
  formStatus: number
}

/**
 * FlowFormQuery类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormQueryDto）
 */
export interface FlowFormQuery extends TaktPagedQuery {
  /** 对应后端字段 formCode */
  formCode?: string
  /** 对应后端字段 formName */
  formName?: string
  /** 对应后端字段 formCategory */
  formCategory?: number
  /** 对应后端字段 formType */
  formType?: number
  /** 对应后端字段 formConfig */
  formConfig?: string
  /** 对应后端字段 formTemplate */
  formTemplate?: string
  /** 对应后端字段 formVersion */
  formVersion?: string
  /** 对应后端字段 isDatasource */
  isDatasource?: number
  /** 对应后端字段 relatedDataBaseName */
  relatedDataBaseName?: string
  /** 对应后端字段 relatedTableName */
  relatedTableName?: string
  /** 对应后端字段 relatedFormField */
  relatedFormField?: string
  /** 对应后端字段 formStatus */
  formStatus?: number
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
 * FlowFormCreate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormCreateDto）
 */
export interface FlowFormCreate {
  /** 对应后端字段 formCode */
  formCode: string
  /** 对应后端字段 formName */
  formName: string
  /** 对应后端字段 formCategory */
  formCategory: number
  /** 对应后端字段 formType */
  formType: number
  /** 对应后端字段 formConfig */
  formConfig?: string
  /** 对应后端字段 formTemplate */
  formTemplate?: string
  /** 对应后端字段 formVersion */
  formVersion: string
  /** 对应后端字段 isDatasource */
  isDatasource: number
  /** 对应后端字段 relatedDataBaseName */
  relatedDataBaseName?: string
  /** 对应后端字段 relatedTableName */
  relatedTableName?: string
  /** 对应后端字段 relatedFormField */
  relatedFormField?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 formStatus */
  formStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * FlowFormUpdate类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormUpdateDto）
 */
export interface FlowFormUpdate extends FlowFormCreate {
  /** 对应后端字段 flowFormId */
  flowFormId: string
}

/**
 * FlowFormSort类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormSortDto）
 */
export interface FlowFormSort {
  /** 对应后端字段 flowFormId */
  flowFormId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * FlowFormFormStatus类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormFormStatusDto）
 */
export interface FlowFormFormStatus {
  /** 对应后端字段 flowFormId */
  flowFormId: string
  /** 对应后端字段 formStatus */
  formStatus: number
}
