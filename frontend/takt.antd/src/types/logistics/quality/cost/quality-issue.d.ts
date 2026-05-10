// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-issue
// 文件名称：quality-issue.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-issue相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityIssue类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueDto）
 */
export interface QualityIssue extends TaktEntityBase {
  /** 对应后端字段 qualityIssueId */
  qualityIssueId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 issueNo */
  issueNo: string
  /** 对应后端字段 issueDate */
  issueDate: string
  /** 对应后端字段 model */
  model: string
  /** 对应后端字段 lot */
  lot: string
  /** 对应后端字段 qualityProblemsResponse */
  qualityProblemsResponse?: string
  /** 对应后端字段 reworkDueToDefects */
  reworkDueToDefects?: string
  /** 对应后端字段 needRework */
  needRework?: string
  /** 对应后端字段 totalTimeMinutes */
  totalTimeMinutes: number
  /** 对应后端字段 totalCost */
  totalCost: number
  /** 对应后端字段 costCurrency */
  costCurrency: string
  /** 对应后端字段 meetingItems */
  meetingItems?: unknown[]
  /** 对应后端字段 assyReworkItems */
  assyReworkItems?: unknown[]
  /** 对应后端字段 pcbaReworkItems */
  pcbaReworkItems?: unknown[]
}

/**
 * QualityIssueQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueQueryDto）
 */
export interface QualityIssueQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 issueNo */
  issueNo?: string
  /** 对应后端字段 issueDate */
  issueDate?: string
  /** 对应后端字段 issueDateStart */
  issueDateStart?: string
  /** 对应后端字段 issueDateEnd */
  issueDateEnd?: string
  /** 对应后端字段 model */
  model?: string
  /** 对应后端字段 lot */
  lot?: string
  /** 对应后端字段 qualityProblemsResponse */
  qualityProblemsResponse?: string
  /** 对应后端字段 reworkDueToDefects */
  reworkDueToDefects?: string
  /** 对应后端字段 needRework */
  needRework?: string
  /** 对应后端字段 totalTimeMinutes */
  totalTimeMinutes?: number
  /** 对应后端字段 totalCost */
  totalCost?: number
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
 * QualityIssueCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueCreateDto）
 */
export interface QualityIssueCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 issueNo */
  issueNo: string
  /** 对应后端字段 issueDate */
  issueDate: string
  /** 对应后端字段 model */
  model: string
  /** 对应后端字段 lot */
  lot: string
  /** 对应后端字段 qualityProblemsResponse */
  qualityProblemsResponse?: string
  /** 对应后端字段 reworkDueToDefects */
  reworkDueToDefects?: string
  /** 对应后端字段 needRework */
  needRework?: string
  /** 对应后端字段 totalTimeMinutes */
  totalTimeMinutes: number
  /** 对应后端字段 totalCost */
  totalCost: number
  /** 对应后端字段 costCurrency */
  costCurrency: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 meetingItems */
  meetingItems?: unknown[]
  /** 对应后端字段 assyReworkItems */
  assyReworkItems?: unknown[]
  /** 对应后端字段 pcbaReworkItems */
  pcbaReworkItems?: unknown[]
}

/**
 * QualityIssueUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueUpdateDto）
 */
export interface QualityIssueUpdate extends QualityIssueCreate {
  /** 对应后端字段 qualityIssueId */
  qualityIssueId: string
}
