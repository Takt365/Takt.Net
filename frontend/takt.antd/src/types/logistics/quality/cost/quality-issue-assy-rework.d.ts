// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-issue-assy-rework
// 文件名称：quality-issue-assy-rework.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-issue-assy-rework相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityIssueAssyRework类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueAssyReworkDto）
 */
export interface QualityIssueAssyRework extends TaktEntityBase {
  /** 对应后端字段 qualityIssueAssyReworkId */
  qualityIssueAssyReworkId: string
  /** 对应后端字段 qualityIssueId */
  qualityIssueId: string
  /** 对应后端字段 qualityIssueCode */
  qualityIssueCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 assyDefectParts */
  assyDefectParts?: string
  /** 对应后端字段 assyReworkCost */
  assyReworkCost: number
  /** 对应后端字段 assyReworkTimeMinutes */
  assyReworkTimeMinutes: number
  /** 对应后端字段 assyReinspectionTimeMinutes */
  assyReinspectionTimeMinutes: number
  /** 对应后端字段 assyTravelCost */
  assyTravelCost: number
  /** 对应后端字段 assyWarehouseCost */
  assyWarehouseCost: number
  /** 对应后端字段 assyOtherExpenses */
  assyOtherExpenses: number
  /** 对应后端字段 assyReworkNote */
  assyReworkNote?: string
  /** 对应后端字段 assyScrapCost */
  assyScrapCost: number
  /** 对应后端字段 assyCustomerName */
  assyCustomerName?: string
  /** 对应后端字段 assyDebitNoteNo */
  assyDebitNoteNo?: string
  /** 对应后端字段 assyOtherExpenses2 */
  assyOtherExpenses2: number
  /** 对应后端字段 assyNote */
  assyNote?: string
  /** 对应后端字段 assyRecorder */
  assyRecorder?: string
  /** 对应后端字段 issue */
  issue?: unknown
}

/**
 * QualityIssueAssyReworkQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueAssyReworkQueryDto）
 */
export interface QualityIssueAssyReworkQuery extends TaktPagedQuery {
  /** 对应后端字段 qualityIssueId */
  qualityIssueId?: string
  /** 对应后端字段 qualityIssueCode */
  qualityIssueCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 assyDefectParts */
  assyDefectParts?: string
  /** 对应后端字段 assyReworkCost */
  assyReworkCost?: number
  /** 对应后端字段 assyReworkTimeMinutes */
  assyReworkTimeMinutes?: number
  /** 对应后端字段 assyReinspectionTimeMinutes */
  assyReinspectionTimeMinutes?: number
  /** 对应后端字段 assyTravelCost */
  assyTravelCost?: number
  /** 对应后端字段 assyWarehouseCost */
  assyWarehouseCost?: number
  /** 对应后端字段 assyOtherExpenses */
  assyOtherExpenses?: number
  /** 对应后端字段 assyReworkNote */
  assyReworkNote?: string
  /** 对应后端字段 assyScrapCost */
  assyScrapCost?: number
  /** 对应后端字段 assyCustomerName */
  assyCustomerName?: string
  /** 对应后端字段 assyDebitNoteNo */
  assyDebitNoteNo?: string
  /** 对应后端字段 assyOtherExpenses2 */
  assyOtherExpenses2?: number
  /** 对应后端字段 assyNote */
  assyNote?: string
  /** 对应后端字段 assyRecorder */
  assyRecorder?: string
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
 * QualityIssueAssyReworkCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueAssyReworkCreateDto）
 */
export interface QualityIssueAssyReworkCreate {
  /** 对应后端字段 qualityIssueId */
  qualityIssueId: string
  /** 对应后端字段 qualityIssueCode */
  qualityIssueCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 assyDefectParts */
  assyDefectParts?: string
  /** 对应后端字段 assyReworkCost */
  assyReworkCost: number
  /** 对应后端字段 assyReworkTimeMinutes */
  assyReworkTimeMinutes: number
  /** 对应后端字段 assyReinspectionTimeMinutes */
  assyReinspectionTimeMinutes: number
  /** 对应后端字段 assyTravelCost */
  assyTravelCost: number
  /** 对应后端字段 assyWarehouseCost */
  assyWarehouseCost: number
  /** 对应后端字段 assyOtherExpenses */
  assyOtherExpenses: number
  /** 对应后端字段 assyReworkNote */
  assyReworkNote?: string
  /** 对应后端字段 assyScrapCost */
  assyScrapCost: number
  /** 对应后端字段 assyCustomerName */
  assyCustomerName?: string
  /** 对应后端字段 assyDebitNoteNo */
  assyDebitNoteNo?: string
  /** 对应后端字段 assyOtherExpenses2 */
  assyOtherExpenses2: number
  /** 对应后端字段 assyNote */
  assyNote?: string
  /** 对应后端字段 assyRecorder */
  assyRecorder?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * QualityIssueAssyReworkUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueAssyReworkUpdateDto）
 */
export interface QualityIssueAssyReworkUpdate extends QualityIssueAssyReworkCreate {
  /** 对应后端字段 qualityIssueAssyReworkId */
  qualityIssueAssyReworkId: string
}
