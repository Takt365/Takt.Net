// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/performance/improvement-plan
// 文件名称：improvement-plan.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：improvement-plan相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ImprovementPlan类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktImprovementPlanDto）
 */
export interface ImprovementPlan extends TaktEntityBase {
  /** 对应后端字段 improvementPlanId */
  improvementPlanId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 performanceReviewId */
  performanceReviewId: string
  /** 对应后端字段 planTitle */
  planTitle: string
  /** 对应后端字段 improvementArea */
  improvementArea: string
  /** 对应后端字段 currentSituation */
  currentSituation: string
  /** 对应后端字段 improvementGoal */
  improvementGoal: string
  /** 对应后端字段 improvementActions */
  improvementActions: string
  /** 对应后端字段 requiredResources */
  requiredResources: string
  /** 对应后端字段 planDate */
  planDate: string
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 targetCompletionDate */
  targetCompletionDate: string
  /** 对应后端字段 actualCompletionDate */
  actualCompletionDate: string
  /** 对应后端字段 progressPercentage */
  progressPercentage: number
  /** 对应后端字段 midtermCheckDate */
  midtermCheckDate: string
  /** 对应后端字段 midtermCheckResult */
  midtermCheckResult: string
  /** 对应后端字段 resultDescription */
  resultDescription: string
  /** 对应后端字段 mentorId */
  mentorId: string
  /** 对应后端字段 approverId */
  approverId: string
  /** 对应后端字段 status */
  status: number
}

/**
 * ImprovementPlanQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktImprovementPlanQueryDto）
 */
export interface ImprovementPlanQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 performanceReviewId */
  performanceReviewId?: string
  /** 对应后端字段 planTitle */
  planTitle?: string
  /** 对应后端字段 improvementArea */
  improvementArea?: string
  /** 对应后端字段 currentSituation */
  currentSituation?: string
  /** 对应后端字段 improvementGoal */
  improvementGoal?: string
  /** 对应后端字段 improvementActions */
  improvementActions?: string
  /** 对应后端字段 requiredResources */
  requiredResources?: string
  /** 对应后端字段 planDate */
  planDate?: string
  /** 对应后端字段 planDateStart */
  planDateStart?: string
  /** 对应后端字段 planDateEnd */
  planDateEnd?: string
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 startDateStart */
  startDateStart?: string
  /** 对应后端字段 startDateEnd */
  startDateEnd?: string
  /** 对应后端字段 targetCompletionDate */
  targetCompletionDate?: string
  /** 对应后端字段 targetCompletionDateStart */
  targetCompletionDateStart?: string
  /** 对应后端字段 targetCompletionDateEnd */
  targetCompletionDateEnd?: string
  /** 对应后端字段 actualCompletionDate */
  actualCompletionDate?: string
  /** 对应后端字段 actualCompletionDateStart */
  actualCompletionDateStart?: string
  /** 对应后端字段 actualCompletionDateEnd */
  actualCompletionDateEnd?: string
  /** 对应后端字段 progressPercentage */
  progressPercentage?: number
  /** 对应后端字段 midtermCheckDate */
  midtermCheckDate?: string
  /** 对应后端字段 midtermCheckDateStart */
  midtermCheckDateStart?: string
  /** 对应后端字段 midtermCheckDateEnd */
  midtermCheckDateEnd?: string
  /** 对应后端字段 midtermCheckResult */
  midtermCheckResult?: string
  /** 对应后端字段 resultDescription */
  resultDescription?: string
  /** 对应后端字段 mentorId */
  mentorId?: string
  /** 对应后端字段 approverId */
  approverId?: string
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
 * ImprovementPlanCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktImprovementPlanCreateDto）
 */
export interface ImprovementPlanCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 performanceReviewId */
  performanceReviewId: string
  /** 对应后端字段 planTitle */
  planTitle: string
  /** 对应后端字段 improvementArea */
  improvementArea: string
  /** 对应后端字段 currentSituation */
  currentSituation: string
  /** 对应后端字段 improvementGoal */
  improvementGoal: string
  /** 对应后端字段 improvementActions */
  improvementActions: string
  /** 对应后端字段 requiredResources */
  requiredResources: string
  /** 对应后端字段 planDate */
  planDate: string
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 targetCompletionDate */
  targetCompletionDate: string
  /** 对应后端字段 actualCompletionDate */
  actualCompletionDate: string
  /** 对应后端字段 progressPercentage */
  progressPercentage: number
  /** 对应后端字段 midtermCheckDate */
  midtermCheckDate: string
  /** 对应后端字段 midtermCheckResult */
  midtermCheckResult: string
  /** 对应后端字段 resultDescription */
  resultDescription: string
  /** 对应后端字段 mentorId */
  mentorId: string
  /** 对应后端字段 approverId */
  approverId: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ImprovementPlanUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktImprovementPlanUpdateDto）
 */
export interface ImprovementPlanUpdate extends ImprovementPlanCreate {
  /** 对应后端字段 improvementPlanId */
  improvementPlanId: string
}

/**
 * ImprovementPlanStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktImprovementPlanStatusDto）
 */
export interface ImprovementPlanStatus {
  /** 对应后端字段 improvementPlanId */
  improvementPlanId: string
  /** 对应后端字段 status */
  status: number
}
