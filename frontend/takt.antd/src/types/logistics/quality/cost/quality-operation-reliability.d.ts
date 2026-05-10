// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-operation-reliability
// 文件名称：quality-operation-reliability.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-operation-reliability相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityOperationReliability类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationReliabilityDto）
 */
export interface QualityOperationReliability extends TaktEntityBase {
  /** 对应后端字段 qualityOperationReliabilityId */
  qualityOperationReliabilityId: string
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 testCost */
  testCost: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 reliabilityNote */
  reliabilityNote?: string
  /** 对应后端字段 operation */
  operation?: unknown
}

/**
 * QualityOperationReliabilityQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationReliabilityQueryDto）
 */
export interface QualityOperationReliabilityQuery extends TaktPagedQuery {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 testCost */
  testCost?: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes?: number
  /** 对应后端字段 otherExpenses */
  otherExpenses?: number
  /** 对应后端字段 reliabilityNote */
  reliabilityNote?: string
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
 * QualityOperationReliabilityCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationReliabilityCreateDto）
 */
export interface QualityOperationReliabilityCreate {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 testCost */
  testCost: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 reliabilityNote */
  reliabilityNote?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * QualityOperationReliabilityUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationReliabilityUpdateDto）
 */
export interface QualityOperationReliabilityUpdate extends QualityOperationReliabilityCreate {
  /** 对应后端字段 qualityOperationReliabilityId */
  qualityOperationReliabilityId: string
}
