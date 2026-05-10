// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/training-development/training-activity
// 文件名称：training-activity.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：training-activity相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * TrainingActivity类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingActivityDto）
 */
export interface TrainingActivity extends TaktEntityBase {
  /** 对应后端字段 trainingActivityId */
  trainingActivityId: string
  /** 对应后端字段 activityCode */
  activityCode: string
  /** 对应后端字段 activityName */
  activityName: string
  /** 对应后端字段 trainingCourseId */
  trainingCourseId: string
  /** 对应后端字段 trainingPlanId */
  trainingPlanId: string
  /** 对应后端字段 trainingDate */
  trainingDate: string
  /** 对应后端字段 startTime */
  startTime: string
  /** 对应后端字段 endTime */
  endTime: string
  /** 对应后端字段 trainingLocation */
  trainingLocation: string
  /** 对应后端字段 instructor */
  instructor: string
  /** 对应后端字段 plannedAttendees */
  plannedAttendees: number
  /** 对应后端字段 actualAttendees */
  actualAttendees: number
  /** 对应后端字段 trainingHours */
  trainingHours: number
  /** 对应后端字段 trainingCost */
  trainingCost: number
  /** 对应后端字段 contentSummary */
  contentSummary: string
  /** 对应后端字段 trainingMaterials */
  trainingMaterials: string
  /** 对应后端字段 effectivenessEvaluation */
  effectivenessEvaluation: string
  /** 对应后端字段 participantFeedback */
  participantFeedback: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions: string
  /** 对应后端字段 organizerId */
  organizerId: string
  /** 对应后端字段 status */
  status: number
}

/**
 * TrainingActivityQuery类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingActivityQueryDto）
 */
export interface TrainingActivityQuery extends TaktPagedQuery {
  /** 对应后端字段 activityCode */
  activityCode?: string
  /** 对应后端字段 activityName */
  activityName?: string
  /** 对应后端字段 trainingCourseId */
  trainingCourseId?: string
  /** 对应后端字段 trainingPlanId */
  trainingPlanId?: string
  /** 对应后端字段 trainingDate */
  trainingDate?: string
  /** 对应后端字段 trainingDateStart */
  trainingDateStart?: string
  /** 对应后端字段 trainingDateEnd */
  trainingDateEnd?: string
  /** 对应后端字段 startTime */
  startTime?: string
  /** 对应后端字段 endTime */
  endTime?: string
  /** 对应后端字段 trainingLocation */
  trainingLocation?: string
  /** 对应后端字段 instructor */
  instructor?: string
  /** 对应后端字段 plannedAttendees */
  plannedAttendees?: number
  /** 对应后端字段 actualAttendees */
  actualAttendees?: number
  /** 对应后端字段 trainingHours */
  trainingHours?: number
  /** 对应后端字段 trainingCost */
  trainingCost?: number
  /** 对应后端字段 contentSummary */
  contentSummary?: string
  /** 对应后端字段 trainingMaterials */
  trainingMaterials?: string
  /** 对应后端字段 effectivenessEvaluation */
  effectivenessEvaluation?: string
  /** 对应后端字段 participantFeedback */
  participantFeedback?: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions?: string
  /** 对应后端字段 organizerId */
  organizerId?: string
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
 * TrainingActivityCreate类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingActivityCreateDto）
 */
export interface TrainingActivityCreate {
  /** 对应后端字段 activityCode */
  activityCode: string
  /** 对应后端字段 activityName */
  activityName: string
  /** 对应后端字段 trainingCourseId */
  trainingCourseId: string
  /** 对应后端字段 trainingPlanId */
  trainingPlanId: string
  /** 对应后端字段 trainingDate */
  trainingDate: string
  /** 对应后端字段 startTime */
  startTime: string
  /** 对应后端字段 endTime */
  endTime: string
  /** 对应后端字段 trainingLocation */
  trainingLocation: string
  /** 对应后端字段 instructor */
  instructor: string
  /** 对应后端字段 plannedAttendees */
  plannedAttendees: number
  /** 对应后端字段 actualAttendees */
  actualAttendees: number
  /** 对应后端字段 trainingHours */
  trainingHours: number
  /** 对应后端字段 trainingCost */
  trainingCost: number
  /** 对应后端字段 contentSummary */
  contentSummary: string
  /** 对应后端字段 trainingMaterials */
  trainingMaterials: string
  /** 对应后端字段 effectivenessEvaluation */
  effectivenessEvaluation: string
  /** 对应后端字段 participantFeedback */
  participantFeedback: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions: string
  /** 对应后端字段 organizerId */
  organizerId: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * TrainingActivityUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingActivityUpdateDto）
 */
export interface TrainingActivityUpdate extends TrainingActivityCreate {
  /** 对应后端字段 trainingActivityId */
  trainingActivityId: string
}

/**
 * TrainingActivityStatus类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingActivityStatusDto）
 */
export interface TrainingActivityStatus {
  /** 对应后端字段 trainingActivityId */
  trainingActivityId: string
  /** 对应后端字段 status */
  status: number
}
