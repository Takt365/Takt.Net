// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/training-development/skill-assessment
// 文件名称：skill-assessment.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：skill-assessment相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SkillAssessment类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktSkillAssessmentDto）
 */
export interface SkillAssessment extends TaktEntityBase {
  /** 对应后端字段 skillAssessmentId */
  skillAssessmentId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 skillCategory */
  skillCategory: string
  /** 对应后端字段 skillName */
  skillName: string
  /** 对应后端字段 skillDescription */
  skillDescription: string
  /** 对应后端字段 assessmentDate */
  assessmentDate: string
  /** 对应后端字段 assessmentMethod */
  assessmentMethod: string
  /** 对应后端字段 assessmentScore */
  assessmentScore: number
  /** 对应后端字段 skillLevel */
  skillLevel: string
  /** 对应后端字段 previousLevel */
  previousLevel: string
  /** 对应后端字段 newLevel */
  newLevel: string
  /** 对应后端字段 isPassed */
  isPassed: number
  /** 对应后端字段 certificateNo */
  certificateNo: string
  /** 对应后端字段 certificateExpiryDate */
  certificateExpiryDate: string
  /** 对应后端字段 assessorId */
  assessorId: string
  /** 对应后端字段 assessmentComments */
  assessmentComments: string
  /** 对应后端字段 strengthsAnalysis */
  strengthsAnalysis: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions: string
  /** 对应后端字段 nextAssessmentDate */
  nextAssessmentDate: string
  /** 对应后端字段 status */
  status: number
}

/**
 * SkillAssessmentQuery类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktSkillAssessmentQueryDto）
 */
export interface SkillAssessmentQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 skillCategory */
  skillCategory?: string
  /** 对应后端字段 skillName */
  skillName?: string
  /** 对应后端字段 skillDescription */
  skillDescription?: string
  /** 对应后端字段 assessmentDate */
  assessmentDate?: string
  /** 对应后端字段 assessmentDateStart */
  assessmentDateStart?: string
  /** 对应后端字段 assessmentDateEnd */
  assessmentDateEnd?: string
  /** 对应后端字段 assessmentMethod */
  assessmentMethod?: string
  /** 对应后端字段 assessmentScore */
  assessmentScore?: number
  /** 对应后端字段 skillLevel */
  skillLevel?: string
  /** 对应后端字段 previousLevel */
  previousLevel?: string
  /** 对应后端字段 newLevel */
  newLevel?: string
  /** 对应后端字段 isPassed */
  isPassed?: number
  /** 对应后端字段 certificateNo */
  certificateNo?: string
  /** 对应后端字段 certificateExpiryDate */
  certificateExpiryDate?: string
  /** 对应后端字段 certificateExpiryDateStart */
  certificateExpiryDateStart?: string
  /** 对应后端字段 certificateExpiryDateEnd */
  certificateExpiryDateEnd?: string
  /** 对应后端字段 assessorId */
  assessorId?: string
  /** 对应后端字段 assessmentComments */
  assessmentComments?: string
  /** 对应后端字段 strengthsAnalysis */
  strengthsAnalysis?: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions?: string
  /** 对应后端字段 nextAssessmentDate */
  nextAssessmentDate?: string
  /** 对应后端字段 nextAssessmentDateStart */
  nextAssessmentDateStart?: string
  /** 对应后端字段 nextAssessmentDateEnd */
  nextAssessmentDateEnd?: string
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
 * SkillAssessmentCreate类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktSkillAssessmentCreateDto）
 */
export interface SkillAssessmentCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 skillCategory */
  skillCategory: string
  /** 对应后端字段 skillName */
  skillName: string
  /** 对应后端字段 skillDescription */
  skillDescription: string
  /** 对应后端字段 assessmentDate */
  assessmentDate: string
  /** 对应后端字段 assessmentMethod */
  assessmentMethod: string
  /** 对应后端字段 assessmentScore */
  assessmentScore: number
  /** 对应后端字段 skillLevel */
  skillLevel: string
  /** 对应后端字段 previousLevel */
  previousLevel: string
  /** 对应后端字段 newLevel */
  newLevel: string
  /** 对应后端字段 isPassed */
  isPassed: number
  /** 对应后端字段 certificateNo */
  certificateNo: string
  /** 对应后端字段 certificateExpiryDate */
  certificateExpiryDate: string
  /** 对应后端字段 assessorId */
  assessorId: string
  /** 对应后端字段 assessmentComments */
  assessmentComments: string
  /** 对应后端字段 strengthsAnalysis */
  strengthsAnalysis: string
  /** 对应后端字段 improvementSuggestions */
  improvementSuggestions: string
  /** 对应后端字段 nextAssessmentDate */
  nextAssessmentDate: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * SkillAssessmentUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktSkillAssessmentUpdateDto）
 */
export interface SkillAssessmentUpdate extends SkillAssessmentCreate {
  /** 对应后端字段 skillAssessmentId */
  skillAssessmentId: string
}

/**
 * SkillAssessmentStatus类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktSkillAssessmentStatusDto）
 */
export interface SkillAssessmentStatus {
  /** 对应后端字段 skillAssessmentId */
  skillAssessmentId: string
  /** 对应后端字段 status */
  status: number
}
