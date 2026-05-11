// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-issue-pcba-rework
// 文件名称：quality-issue-pcba-rework.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-issue-pcba-rework相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityIssuePcbaRework类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssuePcbaReworkDto）
 */
export interface QualityIssuePcbaRework extends TaktEntityBase {
  /** 对应后端字段 qualityIssuePcbaReworkId */
  qualityIssuePcbaReworkId: string
  /** 对应后端字段 qualityIssueId */
  qualityIssueId: string
  /** 对应后端字段 qualityIssueCode */
  qualityIssueCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 pcbaDefectParts */
  pcbaDefectParts?: string
  /** 对应后端字段 pcbaReworkCost */
  pcbaReworkCost: number
  /** 对应后端字段 pcbaReworkTimeMinutes */
  pcbaReworkTimeMinutes: number
  /** 对应后端字段 pcbaReinspectionTimeMinutes */
  pcbaReinspectionTimeMinutes: number
  /** 对应后端字段 pcbaTravelCost */
  pcbaTravelCost: number
  /** 对应后端字段 pcbaWarehouseCost */
  pcbaWarehouseCost: number
  /** 对应后端字段 pcbaOtherExpenses */
  pcbaOtherExpenses: number
  /** 对应后端字段 pcbaReworkNote */
  pcbaReworkNote?: string
  /** 对应后端字段 pcbaScrapCost */
  pcbaScrapCost: number
  /** 对应后端字段 pcbaCustomerName */
  pcbaCustomerName?: string
  /** 对应后端字段 pcbaDebitNoteNo */
  pcbaDebitNoteNo?: string
  /** 对应后端字段 pcbaOtherExpenses2 */
  pcbaOtherExpenses2: number
  /** 对应后端字段 pcbaNote */
  pcbaNote?: string
  /** 对应后端字段 pcbaRecorder */
  pcbaRecorder?: string
  /** 对应后端字段 issue */
  issue?: unknown
}

/**
 * QualityIssuePcbaReworkQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssuePcbaReworkQueryDto）
 */
export interface QualityIssuePcbaReworkQuery extends TaktPagedQuery {
  /** 对应后端字段 qualityIssueId */
  qualityIssueId?: string
  /** 对应后端字段 qualityIssueCode */
  qualityIssueCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 pcbaDefectParts */
  pcbaDefectParts?: string
  /** 对应后端字段 pcbaReworkCost */
  pcbaReworkCost?: number
  /** 对应后端字段 pcbaReworkTimeMinutes */
  pcbaReworkTimeMinutes?: number
  /** 对应后端字段 pcbaReinspectionTimeMinutes */
  pcbaReinspectionTimeMinutes?: number
  /** 对应后端字段 pcbaTravelCost */
  pcbaTravelCost?: number
  /** 对应后端字段 pcbaWarehouseCost */
  pcbaWarehouseCost?: number
  /** 对应后端字段 pcbaOtherExpenses */
  pcbaOtherExpenses?: number
  /** 对应后端字段 pcbaReworkNote */
  pcbaReworkNote?: string
  /** 对应后端字段 pcbaScrapCost */
  pcbaScrapCost?: number
  /** 对应后端字段 pcbaCustomerName */
  pcbaCustomerName?: string
  /** 对应后端字段 pcbaDebitNoteNo */
  pcbaDebitNoteNo?: string
  /** 对应后端字段 pcbaOtherExpenses2 */
  pcbaOtherExpenses2?: number
  /** 对应后端字段 pcbaNote */
  pcbaNote?: string
  /** 对应后端字段 pcbaRecorder */
  pcbaRecorder?: string
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
 * QualityIssuePcbaReworkCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssuePcbaReworkCreateDto）
 */
export interface QualityIssuePcbaReworkCreate {
  /** 对应后端字段 qualityIssueId */
  qualityIssueId: string
  /** 对应后端字段 qualityIssueCode */
  qualityIssueCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 pcbaDefectParts */
  pcbaDefectParts?: string
  /** 对应后端字段 pcbaReworkCost */
  pcbaReworkCost: number
  /** 对应后端字段 pcbaReworkTimeMinutes */
  pcbaReworkTimeMinutes: number
  /** 对应后端字段 pcbaReinspectionTimeMinutes */
  pcbaReinspectionTimeMinutes: number
  /** 对应后端字段 pcbaTravelCost */
  pcbaTravelCost: number
  /** 对应后端字段 pcbaWarehouseCost */
  pcbaWarehouseCost: number
  /** 对应后端字段 pcbaOtherExpenses */
  pcbaOtherExpenses: number
  /** 对应后端字段 pcbaReworkNote */
  pcbaReworkNote?: string
  /** 对应后端字段 pcbaScrapCost */
  pcbaScrapCost: number
  /** 对应后端字段 pcbaCustomerName */
  pcbaCustomerName?: string
  /** 对应后端字段 pcbaDebitNoteNo */
  pcbaDebitNoteNo?: string
  /** 对应后端字段 pcbaOtherExpenses2 */
  pcbaOtherExpenses2: number
  /** 对应后端字段 pcbaNote */
  pcbaNote?: string
  /** 对应后端字段 pcbaRecorder */
  pcbaRecorder?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * QualityIssuePcbaReworkUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssuePcbaReworkUpdateDto）
 */
export interface QualityIssuePcbaReworkUpdate extends QualityIssuePcbaReworkCreate {
  /** 对应后端字段 qualityIssuePcbaReworkId */
  qualityIssuePcbaReworkId: string
}
