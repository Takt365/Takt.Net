// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-operation-outgoing
// 文件名称：quality-operation-outgoing.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-operation-outgoing相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityOperationOutgoing类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationOutgoingDto）
 */
export interface QualityOperationOutgoing extends TaktEntityBase {
  /** 对应后端字段 qualityOperationOutgoingId */
  qualityOperationOutgoingId: string
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 inspectionCost */
  inspectionCost: number
  /** 对应后端字段 inspectionTimeMinutes */
  inspectionTimeMinutes: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 outgoingNote */
  outgoingNote?: string
  /** 对应后端字段 operation */
  operation?: unknown
}

/**
 * QualityOperationOutgoingQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationOutgoingQueryDto）
 */
export interface QualityOperationOutgoingQuery extends TaktPagedQuery {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 inspectionCost */
  inspectionCost?: number
  /** 对应后端字段 inspectionTimeMinutes */
  inspectionTimeMinutes?: number
  /** 对应后端字段 otherExpenses */
  otherExpenses?: number
  /** 对应后端字段 outgoingNote */
  outgoingNote?: string
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
 * QualityOperationOutgoingCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationOutgoingCreateDto）
 */
export interface QualityOperationOutgoingCreate {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 inspectionCost */
  inspectionCost: number
  /** 对应后端字段 inspectionTimeMinutes */
  inspectionTimeMinutes: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 outgoingNote */
  outgoingNote?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * QualityOperationOutgoingUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationOutgoingUpdateDto）
 */
export interface QualityOperationOutgoingUpdate extends QualityOperationOutgoingCreate {
  /** 对应后端字段 qualityOperationOutgoingId */
  qualityOperationOutgoingId: string
}
