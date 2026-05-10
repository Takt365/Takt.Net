// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/training-development/training-plan
// 文件名称：training-plan.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：training-plan相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * TrainingPlan类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingPlanDto）
 */
export interface TrainingPlan extends TaktEntityBase {
  /** 对应后端字段 trainingPlanId */
  trainingPlanId: string
  /** 对应后端字段 planCode */
  planCode: string
  /** 对应后端字段 planName */
  planName: string
  /** 对应后端字段 planYear */
  planYear: number
  /** 对应后端字段 planType */
  planType: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicablePosition */
  applicablePosition: string
  /** 对应后端字段 applicableLevel */
  applicableLevel: string
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 trainingObjectives */
  trainingObjectives: string
  /** 对应后端字段 plannedHeadcount */
  plannedHeadcount: number
  /** 对应后端字段 actualHeadcount */
  actualHeadcount: number
  /** 对应后端字段 plannedTotalHours */
  plannedTotalHours: number
  /** 对应后端字段 actualTotalHours */
  actualTotalHours: number
  /** 对应后端字段 trainingBudget */
  trainingBudget: number
  /** 对应后端字段 actualCost */
  actualCost: number
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 approverId */
  approverId: string
  /** 对应后端字段 status */
  status: number
}

/**
 * TrainingPlanQuery类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingPlanQueryDto）
 */
export interface TrainingPlanQuery extends TaktPagedQuery {
  /** 对应后端字段 planCode */
  planCode?: string
  /** 对应后端字段 planName */
  planName?: string
  /** 对应后端字段 planYear */
  planYear?: number
  /** 对应后端字段 planType */
  planType?: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment?: string
  /** 对应后端字段 applicablePosition */
  applicablePosition?: string
  /** 对应后端字段 applicableLevel */
  applicableLevel?: string
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
  /** 对应后端字段 trainingObjectives */
  trainingObjectives?: string
  /** 对应后端字段 plannedHeadcount */
  plannedHeadcount?: number
  /** 对应后端字段 actualHeadcount */
  actualHeadcount?: number
  /** 对应后端字段 plannedTotalHours */
  plannedTotalHours?: number
  /** 对应后端字段 actualTotalHours */
  actualTotalHours?: number
  /** 对应后端字段 trainingBudget */
  trainingBudget?: number
  /** 对应后端字段 actualCost */
  actualCost?: number
  /** 对应后端字段 description */
  description?: string
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
 * TrainingPlanCreate类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingPlanCreateDto）
 */
export interface TrainingPlanCreate {
  /** 对应后端字段 planCode */
  planCode: string
  /** 对应后端字段 planName */
  planName: string
  /** 对应后端字段 planYear */
  planYear: number
  /** 对应后端字段 planType */
  planType: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicablePosition */
  applicablePosition: string
  /** 对应后端字段 applicableLevel */
  applicableLevel: string
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 trainingObjectives */
  trainingObjectives: string
  /** 对应后端字段 plannedHeadcount */
  plannedHeadcount: number
  /** 对应后端字段 actualHeadcount */
  actualHeadcount: number
  /** 对应后端字段 plannedTotalHours */
  plannedTotalHours: number
  /** 对应后端字段 actualTotalHours */
  actualTotalHours: number
  /** 对应后端字段 trainingBudget */
  trainingBudget: number
  /** 对应后端字段 actualCost */
  actualCost: number
  /** 对应后端字段 description */
  description: string
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
 * TrainingPlanUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingPlanUpdateDto）
 */
export interface TrainingPlanUpdate extends TrainingPlanCreate {
  /** 对应后端字段 trainingPlanId */
  trainingPlanId: string
}

/**
 * TrainingPlanStatus类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingPlanStatusDto）
 */
export interface TrainingPlanStatus {
  /** 对应后端字段 trainingPlanId */
  trainingPlanId: string
  /** 对应后端字段 status */
  status: number
}
