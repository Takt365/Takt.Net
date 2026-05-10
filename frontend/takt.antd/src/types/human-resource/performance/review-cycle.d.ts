// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/performance/review-cycle
// 文件名称：review-cycle.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：review-cycle相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ReviewCycle类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktReviewCycleDto）
 */
export interface ReviewCycle extends TaktEntityBase {
  /** 对应后端字段 reviewCycleId */
  reviewCycleId: string
  /** 对应后端字段 cycleCode */
  cycleCode: string
  /** 对应后端字段 cycleName */
  cycleName: string
  /** 对应后端字段 cycleType */
  cycleType: string
  /** 对应后端字段 cycleYear */
  cycleYear: number
  /** 对应后端字段 cycleSequence */
  cycleSequence: number
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 goalSettingStartDate */
  goalSettingStartDate: string
  /** 对应后端字段 goalSettingDueDate */
  goalSettingDueDate: string
  /** 对应后端字段 selfEvaluationStartDate */
  selfEvaluationStartDate: string
  /** 对应后端字段 selfEvaluationDueDate */
  selfEvaluationDueDate: string
  /** 对应后端字段 supervisorReviewStartDate */
  supervisorReviewStartDate: string
  /** 对应后端字段 supervisorReviewDueDate */
  supervisorReviewDueDate: string
  /** 对应后端字段 interviewDueDate */
  interviewDueDate: string
  /** 对应后端字段 resultConfirmationDueDate */
  resultConfirmationDueDate: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 status */
  status: number
}

/**
 * ReviewCycleQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktReviewCycleQueryDto）
 */
export interface ReviewCycleQuery extends TaktPagedQuery {
  /** 对应后端字段 cycleCode */
  cycleCode?: string
  /** 对应后端字段 cycleName */
  cycleName?: string
  /** 对应后端字段 cycleType */
  cycleType?: string
  /** 对应后端字段 cycleYear */
  cycleYear?: number
  /** 对应后端字段 cycleSequence */
  cycleSequence?: number
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 startDateStart */
  startDateStart?: string
  /** 对应后端字段 startDateEnd */
  startDateEnd?: string
  /** 对应后端字段 endDate */
  endDate?: string
  /** 对应后端字段 endDateStart */
  endDateStart?: string
  /** 对应后端字段 endDateEnd */
  endDateEnd?: string
  /** 对应后端字段 goalSettingStartDate */
  goalSettingStartDate?: string
  /** 对应后端字段 goalSettingStartDateStart */
  goalSettingStartDateStart?: string
  /** 对应后端字段 goalSettingStartDateEnd */
  goalSettingStartDateEnd?: string
  /** 对应后端字段 goalSettingDueDate */
  goalSettingDueDate?: string
  /** 对应后端字段 goalSettingDueDateStart */
  goalSettingDueDateStart?: string
  /** 对应后端字段 goalSettingDueDateEnd */
  goalSettingDueDateEnd?: string
  /** 对应后端字段 selfEvaluationStartDate */
  selfEvaluationStartDate?: string
  /** 对应后端字段 selfEvaluationStartDateStart */
  selfEvaluationStartDateStart?: string
  /** 对应后端字段 selfEvaluationStartDateEnd */
  selfEvaluationStartDateEnd?: string
  /** 对应后端字段 selfEvaluationDueDate */
  selfEvaluationDueDate?: string
  /** 对应后端字段 selfEvaluationDueDateStart */
  selfEvaluationDueDateStart?: string
  /** 对应后端字段 selfEvaluationDueDateEnd */
  selfEvaluationDueDateEnd?: string
  /** 对应后端字段 supervisorReviewStartDate */
  supervisorReviewStartDate?: string
  /** 对应后端字段 supervisorReviewStartDateStart */
  supervisorReviewStartDateStart?: string
  /** 对应后端字段 supervisorReviewStartDateEnd */
  supervisorReviewStartDateEnd?: string
  /** 对应后端字段 supervisorReviewDueDate */
  supervisorReviewDueDate?: string
  /** 对应后端字段 supervisorReviewDueDateStart */
  supervisorReviewDueDateStart?: string
  /** 对应后端字段 supervisorReviewDueDateEnd */
  supervisorReviewDueDateEnd?: string
  /** 对应后端字段 interviewDueDate */
  interviewDueDate?: string
  /** 对应后端字段 interviewDueDateStart */
  interviewDueDateStart?: string
  /** 对应后端字段 interviewDueDateEnd */
  interviewDueDateEnd?: string
  /** 对应后端字段 resultConfirmationDueDate */
  resultConfirmationDueDate?: string
  /** 对应后端字段 resultConfirmationDueDateStart */
  resultConfirmationDueDateStart?: string
  /** 对应后端字段 resultConfirmationDueDateEnd */
  resultConfirmationDueDateEnd?: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment?: string
  /** 对应后端字段 description */
  description?: string
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
 * ReviewCycleCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktReviewCycleCreateDto）
 */
export interface ReviewCycleCreate {
  /** 对应后端字段 cycleCode */
  cycleCode: string
  /** 对应后端字段 cycleName */
  cycleName: string
  /** 对应后端字段 cycleType */
  cycleType: string
  /** 对应后端字段 cycleYear */
  cycleYear: number
  /** 对应后端字段 cycleSequence */
  cycleSequence: number
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 goalSettingStartDate */
  goalSettingStartDate: string
  /** 对应后端字段 goalSettingDueDate */
  goalSettingDueDate: string
  /** 对应后端字段 selfEvaluationStartDate */
  selfEvaluationStartDate: string
  /** 对应后端字段 selfEvaluationDueDate */
  selfEvaluationDueDate: string
  /** 对应后端字段 supervisorReviewStartDate */
  supervisorReviewStartDate: string
  /** 对应后端字段 supervisorReviewDueDate */
  supervisorReviewDueDate: string
  /** 对应后端字段 interviewDueDate */
  interviewDueDate: string
  /** 对应后端字段 resultConfirmationDueDate */
  resultConfirmationDueDate: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ReviewCycleUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktReviewCycleUpdateDto）
 */
export interface ReviewCycleUpdate extends ReviewCycleCreate {
  /** 对应后端字段 reviewCycleId */
  reviewCycleId: string
}

/**
 * ReviewCycleStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktReviewCycleStatusDto）
 */
export interface ReviewCycleStatus {
  /** 对应后端字段 reviewCycleId */
  reviewCycleId: string
  /** 对应后端字段 status */
  status: number
}
