// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/training-development/training-development
// 文件名称：training-development.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：training-development相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * TrainingDevelopment类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingDevelopmentDto）
 */
export interface TrainingDevelopment extends TaktEntityBase {
  /** 对应后端字段 trainingDevelopmentId */
  trainingDevelopmentId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 courseName */
  courseName: string
  /** 对应后端字段 trainingType */
  trainingType: string
  /** 对应后端字段 instructor */
  instructor: string
  /** 对应后端字段 trainingStartDate */
  trainingStartDate: string
  /** 对应后端字段 trainingEndDate */
  trainingEndDate: string
  /** 对应后端字段 trainingDate */
  trainingDate: string
  /** 对应后端字段 trainingHours */
  trainingHours: number
  /** 对应后端字段 trainingLocation */
  trainingLocation: string
  /** 对应后端字段 trainingScore */
  trainingScore: number
  /** 对应后端字段 isPassed */
  isPassed: number
  /** 对应后端字段 certificateNo */
  certificateNo: string
  /** 对应后端字段 trainingEvaluation */
  trainingEvaluation: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions: string
  /** 对应后端字段 developmentPlan */
  developmentPlan: string
  /** 对应后端字段 status */
  status: number
}

/**
 * TrainingDevelopmentQuery类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingDevelopmentQueryDto）
 */
export interface TrainingDevelopmentQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 courseName */
  courseName?: string
  /** 对应后端字段 trainingType */
  trainingType?: string
  /** 对应后端字段 instructor */
  instructor?: string
  /** 对应后端字段 trainingStartDate */
  trainingStartDate?: string
  /** 对应后端字段 trainingStartDateStart */
  trainingStartDateStart?: string
  /** 对应后端字段 trainingStartDateEnd */
  trainingStartDateEnd?: string
  /** 对应后端字段 trainingEndDate */
  trainingEndDate?: string
  /** 对应后端字段 trainingEndDateStart */
  trainingEndDateStart?: string
  /** 对应后端字段 trainingEndDateEnd */
  trainingEndDateEnd?: string
  /** 对应后端字段 trainingDate */
  trainingDate?: string
  /** 对应后端字段 trainingDateStart */
  trainingDateStart?: string
  /** 对应后端字段 trainingDateEnd */
  trainingDateEnd?: string
  /** 对应后端字段 trainingHours */
  trainingHours?: number
  /** 对应后端字段 trainingLocation */
  trainingLocation?: string
  /** 对应后端字段 trainingScore */
  trainingScore?: number
  /** 对应后端字段 isPassed */
  isPassed?: number
  /** 对应后端字段 certificateNo */
  certificateNo?: string
  /** 对应后端字段 trainingEvaluation */
  trainingEvaluation?: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions?: string
  /** 对应后端字段 developmentPlan */
  developmentPlan?: string
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
 * TrainingDevelopmentCreate类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingDevelopmentCreateDto）
 */
export interface TrainingDevelopmentCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 courseName */
  courseName: string
  /** 对应后端字段 trainingType */
  trainingType: string
  /** 对应后端字段 instructor */
  instructor: string
  /** 对应后端字段 trainingStartDate */
  trainingStartDate: string
  /** 对应后端字段 trainingEndDate */
  trainingEndDate: string
  /** 对应后端字段 trainingDate */
  trainingDate: string
  /** 对应后端字段 trainingHours */
  trainingHours: number
  /** 对应后端字段 trainingLocation */
  trainingLocation: string
  /** 对应后端字段 trainingScore */
  trainingScore: number
  /** 对应后端字段 isPassed */
  isPassed: number
  /** 对应后端字段 certificateNo */
  certificateNo: string
  /** 对应后端字段 trainingEvaluation */
  trainingEvaluation: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions: string
  /** 对应后端字段 developmentPlan */
  developmentPlan: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * TrainingDevelopmentUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingDevelopmentUpdateDto）
 */
export interface TrainingDevelopmentUpdate extends TrainingDevelopmentCreate {
  /** 对应后端字段 trainingDevelopmentId */
  trainingDevelopmentId: string
}

/**
 * TrainingDevelopmentStatus类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingDevelopmentStatusDto）
 */
export interface TrainingDevelopmentStatus {
  /** 对应后端字段 trainingDevelopmentId */
  trainingDevelopmentId: string
  /** 对应后端字段 status */
  status: number
}
