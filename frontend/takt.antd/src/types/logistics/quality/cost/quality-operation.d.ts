// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-operation
// 文件名称：quality-operation.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-operation相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityOperation类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationDto）
 */
export interface QualityOperation extends TaktEntityBase {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 qualityOperationCode */
  qualityOperationCode: string
  /** 对应后端字段 operationMonth */
  operationMonth: string
  /** 对应后端字段 customerName */
  customerName?: string
  /** 对应后端字段 debitNoteNo */
  debitNoteNo?: string
  /** 对应后端字段 recorder */
  recorder?: string
  /** 对应后端字段 totalQualityCost */
  totalQualityCost: number
  /** 对应后端字段 costCurrency */
  costCurrency: string
  /** 对应后端字段 incomingItems */
  incomingItems?: unknown[]
  /** 对应后端字段 firstArticleItems */
  firstArticleItems?: unknown[]
  /** 对应后端字段 calibrationItems */
  calibrationItems?: unknown[]
  /** 对应后端字段 otherItems */
  otherItems?: unknown[]
  /** 对应后端字段 outgoingItems */
  outgoingItems?: unknown[]
  /** 对应后端字段 reliabilityItems */
  reliabilityItems?: unknown[]
  /** 对应后端字段 customerResponseItems */
  customerResponseItems?: unknown[]
}

/**
 * QualityOperationQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationQueryDto）
 */
export interface QualityOperationQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 qualityOperationCode */
  qualityOperationCode?: string
  /** 对应后端字段 operationMonth */
  operationMonth?: string
  /** 对应后端字段 customerName */
  customerName?: string
  /** 对应后端字段 debitNoteNo */
  debitNoteNo?: string
  /** 对应后端字段 recorder */
  recorder?: string
  /** 对应后端字段 totalQualityCost */
  totalQualityCost?: number
  /** 对应后端字段 costCurrency */
  costCurrency?: string
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
 * QualityOperationCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationCreateDto）
 */
export interface QualityOperationCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 qualityOperationCode */
  qualityOperationCode: string
  /** 对应后端字段 operationMonth */
  operationMonth: string
  /** 对应后端字段 customerName */
  customerName?: string
  /** 对应后端字段 debitNoteNo */
  debitNoteNo?: string
  /** 对应后端字段 recorder */
  recorder?: string
  /** 对应后端字段 totalQualityCost */
  totalQualityCost: number
  /** 对应后端字段 costCurrency */
  costCurrency: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 incomingItems */
  incomingItems?: unknown[]
  /** 对应后端字段 firstArticleItems */
  firstArticleItems?: unknown[]
  /** 对应后端字段 calibrationItems */
  calibrationItems?: unknown[]
  /** 对应后端字段 otherItems */
  otherItems?: unknown[]
  /** 对应后端字段 outgoingItems */
  outgoingItems?: unknown[]
  /** 对应后端字段 reliabilityItems */
  reliabilityItems?: unknown[]
  /** 对应后端字段 customerResponseItems */
  customerResponseItems?: unknown[]
}

/**
 * QualityOperationUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationUpdateDto）
 */
export interface QualityOperationUpdate extends QualityOperationCreate {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
}
