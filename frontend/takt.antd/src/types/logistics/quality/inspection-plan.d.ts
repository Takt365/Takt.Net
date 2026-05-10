// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/inspection-plan
// 文件名称：inspection-plan.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：inspection-plan相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * InspectionPlan类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionPlanDto）
 */
export interface InspectionPlan extends TaktEntityBase {
  /** 对应后端字段 inspectionPlanId */
  inspectionPlanId: string
  /** 对应后端字段 planCode */
  planCode: string
  /** 对应后端字段 inspectionType */
  inspectionType: number
  /** 对应后端字段 sourceType */
  sourceType: number
  /** 对应后端字段 sourceId */
  sourceId: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 standardId */
  standardId: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 plannedQuantity */
  plannedQuantity: number
  /** 对应后端字段 sampleQuantity */
  sampleQuantity: number
  /** 对应后端字段 plannedDate */
  plannedDate: string
  /** 对应后端字段 inspectorId */
  inspectorId?: string
  /** 对应后端字段 inspectorName */
  inspectorName?: string
  /** 对应后端字段 planStatus */
  planStatus: number
  /** 对应后端字段 planDescription */
  planDescription?: string
  /** 对应后端字段 standard */
  standard?: unknown
  /** 对应后端字段 executions */
  executions?: unknown[]
}

/**
 * InspectionPlanQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionPlanQueryDto）
 */
export interface InspectionPlanQuery extends TaktPagedQuery {
  /** 对应后端字段 planCode */
  planCode?: string
  /** 对应后端字段 inspectionType */
  inspectionType?: number
  /** 对应后端字段 sourceType */
  sourceType?: number
  /** 对应后端字段 sourceId */
  sourceId?: string
  /** 对应后端字段 sourceCode */
  sourceCode?: string
  /** 对应后端字段 standardId */
  standardId?: string
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 plannedQuantity */
  plannedQuantity?: number
  /** 对应后端字段 sampleQuantity */
  sampleQuantity?: number
  /** 对应后端字段 plannedDate */
  plannedDate?: string
  /** 对应后端字段 plannedDateStart */
  plannedDateStart?: string
  /** 对应后端字段 plannedDateEnd */
  plannedDateEnd?: string
  /** 对应后端字段 inspectorId */
  inspectorId?: string
  /** 对应后端字段 inspectorName */
  inspectorName?: string
  /** 对应后端字段 planStatus */
  planStatus?: number
  /** 对应后端字段 planDescription */
  planDescription?: string
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
 * InspectionPlanCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionPlanCreateDto）
 */
export interface InspectionPlanCreate {
  /** 对应后端字段 planCode */
  planCode: string
  /** 对应后端字段 inspectionType */
  inspectionType: number
  /** 对应后端字段 sourceType */
  sourceType: number
  /** 对应后端字段 sourceId */
  sourceId: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 standardId */
  standardId: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 plannedQuantity */
  plannedQuantity: number
  /** 对应后端字段 sampleQuantity */
  sampleQuantity: number
  /** 对应后端字段 plannedDate */
  plannedDate: string
  /** 对应后端字段 inspectorId */
  inspectorId?: string
  /** 对应后端字段 inspectorName */
  inspectorName?: string
  /** 对应后端字段 planStatus */
  planStatus: number
  /** 对应后端字段 planDescription */
  planDescription?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 executions */
  executions?: unknown[]
}

/**
 * InspectionPlanUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionPlanUpdateDto）
 */
export interface InspectionPlanUpdate extends InspectionPlanCreate {
  /** 对应后端字段 inspectionPlanId */
  inspectionPlanId: string
}

/**
 * InspectionPlanPlanStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionPlanPlanStatusDto）
 */
export interface InspectionPlanPlanStatus {
  /** 对应后端字段 inspectionPlanId */
  inspectionPlanId: string
  /** 对应后端字段 planStatus */
  planStatus: number
}
