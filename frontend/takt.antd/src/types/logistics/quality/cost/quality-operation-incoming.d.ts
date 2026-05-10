// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-operation-incoming
// 文件名称：quality-operation-incoming.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-operation-incoming相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityOperationIncoming类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationIncomingDto）
 */
export interface QualityOperationIncoming extends TaktEntityBase {
  /** 对应后端字段 qualityOperationIncomingId */
  qualityOperationIncomingId: string
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 directManpowerCostPerMinute */
  directManpowerCostPerMinute: number
  /** 对应后端字段 incomingInspectionCost */
  incomingInspectionCost: number
  /** 对应后端字段 inspectionTimeMinutes */
  inspectionTimeMinutes: number
  /** 对应后端字段 travelCost */
  travelCost: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 incomingNote */
  incomingNote?: string
  /** 对应后端字段 operation */
  operation?: unknown
}

/**
 * QualityOperationIncomingQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationIncomingQueryDto）
 */
export interface QualityOperationIncomingQuery extends TaktPagedQuery {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 directManpowerCostPerMinute */
  directManpowerCostPerMinute?: number
  /** 对应后端字段 incomingInspectionCost */
  incomingInspectionCost?: number
  /** 对应后端字段 inspectionTimeMinutes */
  inspectionTimeMinutes?: number
  /** 对应后端字段 travelCost */
  travelCost?: number
  /** 对应后端字段 otherExpenses */
  otherExpenses?: number
  /** 对应后端字段 incomingNote */
  incomingNote?: string
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
 * QualityOperationIncomingCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationIncomingCreateDto）
 */
export interface QualityOperationIncomingCreate {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 directManpowerCostPerMinute */
  directManpowerCostPerMinute: number
  /** 对应后端字段 incomingInspectionCost */
  incomingInspectionCost: number
  /** 对应后端字段 inspectionTimeMinutes */
  inspectionTimeMinutes: number
  /** 对应后端字段 travelCost */
  travelCost: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 incomingNote */
  incomingNote?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * QualityOperationIncomingUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationIncomingUpdateDto）
 */
export interface QualityOperationIncomingUpdate extends QualityOperationIncomingCreate {
  /** 对应后端字段 qualityOperationIncomingId */
  qualityOperationIncomingId: string
}
