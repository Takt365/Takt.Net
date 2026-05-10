// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/performance/performance
// 文件名称：performance.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：performance相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Performance类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceDto）
 */
export interface Performance extends TaktEntityBase {
  /** 对应后端字段 performanceId */
  performanceId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 evaluationPeriod */
  evaluationPeriod: string
  /** 对应后端字段 evaluationDate */
  evaluationDate: string
  /** 对应后端字段 evaluationCriteria */
  evaluationCriteria: string
  /** 对应后端字段 score */
  score: number
  /** 对应后端字段 grade */
  grade: string
  /** 对应后端字段 selfScore */
  selfScore: number
  /** 对应后端字段 supervisorScore */
  supervisorScore: number
  /** 对应后端字段 comments */
  comments: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions: string
  /** 对应后端字段 evaluatorId */
  evaluatorId: string
  /** 对应后端字段 status */
  status: number
}

/**
 * PerformanceQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceQueryDto）
 */
export interface PerformanceQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 evaluationPeriod */
  evaluationPeriod?: string
  /** 对应后端字段 evaluationDate */
  evaluationDate?: string
  /** 对应后端字段 evaluationDateStart */
  evaluationDateStart?: string
  /** 对应后端字段 evaluationDateEnd */
  evaluationDateEnd?: string
  /** 对应后端字段 evaluationCriteria */
  evaluationCriteria?: string
  /** 对应后端字段 score */
  score?: number
  /** 对应后端字段 grade */
  grade?: string
  /** 对应后端字段 selfScore */
  selfScore?: number
  /** 对应后端字段 supervisorScore */
  supervisorScore?: number
  /** 对应后端字段 comments */
  comments?: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions?: string
  /** 对应后端字段 evaluatorId */
  evaluatorId?: string
  /** 对应后端字段 status */
  status?: number
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
 * PerformanceCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceCreateDto）
 */
export interface PerformanceCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 evaluationPeriod */
  evaluationPeriod: string
  /** 对应后端字段 evaluationDate */
  evaluationDate: string
  /** 对应后端字段 evaluationCriteria */
  evaluationCriteria: string
  /** 对应后端字段 score */
  score: number
  /** 对应后端字段 grade */
  grade: string
  /** 对应后端字段 selfScore */
  selfScore: number
  /** 对应后端字段 supervisorScore */
  supervisorScore: number
  /** 对应后端字段 comments */
  comments: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions: string
  /** 对应后端字段 evaluatorId */
  evaluatorId: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PerformanceUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceUpdateDto）
 */
export interface PerformanceUpdate extends PerformanceCreate {
  /** 对应后端字段 performanceId */
  performanceId: string
}

/**
 * PerformanceStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceStatusDto）
 */
export interface PerformanceStatus {
  /** 对应后端字段 performanceId */
  performanceId: string
  /** 对应后端字段 status */
  status: number
}
