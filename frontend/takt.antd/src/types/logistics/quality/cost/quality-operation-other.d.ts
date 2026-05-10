// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-operation-other
// 文件名称：quality-operation-other.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-operation-other相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityOperationOther类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationOtherDto）
 */
export interface QualityOperationOther extends TaktEntityBase {
  /** 对应后端字段 qualityOperationOtherId */
  qualityOperationOtherId: string
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 operationsCost */
  operationsCost: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 otherNote */
  otherNote?: string
  /** 对应后端字段 operation */
  operation?: unknown
}

/**
 * QualityOperationOtherQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationOtherQueryDto）
 */
export interface QualityOperationOtherQuery extends TaktPagedQuery {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 operationsCost */
  operationsCost?: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes?: number
  /** 对应后端字段 otherExpenses */
  otherExpenses?: number
  /** 对应后端字段 otherNote */
  otherNote?: string
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
 * QualityOperationOtherCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationOtherCreateDto）
 */
export interface QualityOperationOtherCreate {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 operationsCost */
  operationsCost: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 otherNote */
  otherNote?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * QualityOperationOtherUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationOtherUpdateDto）
 */
export interface QualityOperationOtherUpdate extends QualityOperationOtherCreate {
  /** 对应后端字段 qualityOperationOtherId */
  qualityOperationOtherId: string
}
