// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-operation-first-article
// 文件名称：quality-operation-first-article.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-operation-first-article相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityOperationFirstArticle类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationFirstArticleDto）
 */
export interface QualityOperationFirstArticle extends TaktEntityBase {
  /** 对应后端字段 qualityOperationFirstArticleId */
  qualityOperationFirstArticleId: string
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 qualificationCost */
  qualificationCost: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 qualificationNote */
  qualificationNote?: string
  /** 对应后端字段 operation */
  operation?: unknown
}

/**
 * QualityOperationFirstArticleQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationFirstArticleQueryDto）
 */
export interface QualityOperationFirstArticleQuery extends TaktPagedQuery {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 qualificationCost */
  qualificationCost?: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes?: number
  /** 对应后端字段 otherExpenses */
  otherExpenses?: number
  /** 对应后端字段 qualificationNote */
  qualificationNote?: string
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
 * QualityOperationFirstArticleCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationFirstArticleCreateDto）
 */
export interface QualityOperationFirstArticleCreate {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 qualificationCost */
  qualificationCost: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 qualificationNote */
  qualificationNote?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * QualityOperationFirstArticleUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationFirstArticleUpdateDto）
 */
export interface QualityOperationFirstArticleUpdate extends QualityOperationFirstArticleCreate {
  /** 对应后端字段 qualityOperationFirstArticleId */
  qualityOperationFirstArticleId: string
}
