// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/performance/performance-review
// 文件名称：performance-review.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：performance-review相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PerformanceReview类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceReviewDto）
 */
export interface PerformanceReview extends TaktEntityBase {
  /** 对应后端字段 performanceReviewId */
  performanceReviewId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 reviewPeriod */
  reviewPeriod: string
  /** 对应后端字段 reviewDate */
  reviewDate: string
  /** 对应后端字段 performancePlanId */
  performancePlanId: string
  /** 对应后端字段 selfScore */
  selfScore: number
  /** 对应后端字段 selfEvaluationNotes */
  selfEvaluationNotes: string
  /** 对应后端字段 supervisorScore */
  supervisorScore: number
  /** 对应后端字段 supervisorComments */
  supervisorComments: string
  /** 对应后端字段 finalScore */
  finalScore: number
  /** 对应后端字段 performanceGrade */
  performanceGrade: string
  /** 对应后端字段 reviewerId */
  reviewerId: string
  /** 对应后端字段 interviewDate */
  interviewDate: string
  /** 对应后端字段 interviewNotes */
  interviewNotes: string
  /** 对应后端字段 employeeFeedback */
  employeeFeedback: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions: string
  /** 对应后端字段 nextReviewDate */
  nextReviewDate: string
  /** 对应后端字段 status */
  status: number
}

/**
 * PerformanceReviewQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceReviewQueryDto）
 */
export interface PerformanceReviewQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 reviewPeriod */
  reviewPeriod?: string
  /** 对应后端字段 reviewDate */
  reviewDate?: string
  /** 对应后端字段 reviewDateStart */
  reviewDateStart?: string
  /** 对应后端字段 reviewDateEnd */
  reviewDateEnd?: string
  /** 对应后端字段 performancePlanId */
  performancePlanId?: string
  /** 对应后端字段 selfScore */
  selfScore?: number
  /** 对应后端字段 selfEvaluationNotes */
  selfEvaluationNotes?: string
  /** 对应后端字段 supervisorScore */
  supervisorScore?: number
  /** 对应后端字段 supervisorComments */
  supervisorComments?: string
  /** 对应后端字段 finalScore */
  finalScore?: number
  /** 对应后端字段 performanceGrade */
  performanceGrade?: string
  /** 对应后端字段 reviewerId */
  reviewerId?: string
  /** 对应后端字段 interviewDate */
  interviewDate?: string
  /** 对应后端字段 interviewDateStart */
  interviewDateStart?: string
  /** 对应后端字段 interviewDateEnd */
  interviewDateEnd?: string
  /** 对应后端字段 interviewNotes */
  interviewNotes?: string
  /** 对应后端字段 employeeFeedback */
  employeeFeedback?: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions?: string
  /** 对应后端字段 nextReviewDate */
  nextReviewDate?: string
  /** 对应后端字段 nextReviewDateStart */
  nextReviewDateStart?: string
  /** 对应后端字段 nextReviewDateEnd */
  nextReviewDateEnd?: string
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
 * PerformanceReviewCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceReviewCreateDto）
 */
export interface PerformanceReviewCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 reviewPeriod */
  reviewPeriod: string
  /** 对应后端字段 reviewDate */
  reviewDate: string
  /** 对应后端字段 performancePlanId */
  performancePlanId: string
  /** 对应后端字段 selfScore */
  selfScore: number
  /** 对应后端字段 selfEvaluationNotes */
  selfEvaluationNotes: string
  /** 对应后端字段 supervisorScore */
  supervisorScore: number
  /** 对应后端字段 supervisorComments */
  supervisorComments: string
  /** 对应后端字段 finalScore */
  finalScore: number
  /** 对应后端字段 performanceGrade */
  performanceGrade: string
  /** 对应后端字段 reviewerId */
  reviewerId: string
  /** 对应后端字段 interviewDate */
  interviewDate: string
  /** 对应后端字段 interviewNotes */
  interviewNotes: string
  /** 对应后端字段 employeeFeedback */
  employeeFeedback: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions: string
  /** 对应后端字段 nextReviewDate */
  nextReviewDate: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PerformanceReviewUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceReviewUpdateDto）
 */
export interface PerformanceReviewUpdate extends PerformanceReviewCreate {
  /** 对应后端字段 performanceReviewId */
  performanceReviewId: string
}

/**
 * PerformanceReviewStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceReviewStatusDto）
 */
export interface PerformanceReviewStatus {
  /** 对应后端字段 performanceReviewId */
  performanceReviewId: string
  /** 对应后端字段 status */
  status: number
}
