// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/talent/talent
// 文件名称：talent.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：talent相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Talent类型（对应后端 Takt.Application.Dtos.HumanResource.Talent.TaktTalentDto）
 */
export interface Talent extends TaktEntityBase {
  /** 对应后端字段 talentId */
  talentId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 talentLevel */
  talentLevel: string
  /** 对应后端字段 professionalSkills */
  professionalSkills: string
  /** 对应后端字段 coreCompetency */
  coreCompetency: string
  /** 对应后端字段 leadershipScore */
  leadershipScore: number
  /** 对应后端字段 innovationScore */
  innovationScore: number
  /** 对应后端字段 collaborationScore */
  collaborationScore: number
  /** 对应后端字段 developmentPotential */
  developmentPotential: string
  /** 对应后端字段 careerPlan */
  careerPlan: string
  /** 对应后端字段 talentTags */
  talentTags: string
  /** 对应后端字段 evaluationDate */
  evaluationDate: string
  /** 对应后端字段 evaluatorId */
  evaluatorId: string
  /** 对应后端字段 status */
  status: number
}

/**
 * TalentQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Talent.TaktTalentQueryDto）
 */
export interface TalentQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 talentLevel */
  talentLevel?: string
  /** 对应后端字段 professionalSkills */
  professionalSkills?: string
  /** 对应后端字段 coreCompetency */
  coreCompetency?: string
  /** 对应后端字段 leadershipScore */
  leadershipScore?: number
  /** 对应后端字段 innovationScore */
  innovationScore?: number
  /** 对应后端字段 collaborationScore */
  collaborationScore?: number
  /** 对应后端字段 developmentPotential */
  developmentPotential?: string
  /** 对应后端字段 careerPlan */
  careerPlan?: string
  /** 对应后端字段 talentTags */
  talentTags?: string
  /** 对应后端字段 evaluationDate */
  evaluationDate?: string
  /** 对应后端字段 evaluationDateStart */
  evaluationDateStart?: string
  /** 对应后端字段 evaluationDateEnd */
  evaluationDateEnd?: string
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
 * TalentCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Talent.TaktTalentCreateDto）
 */
export interface TalentCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 talentLevel */
  talentLevel: string
  /** 对应后端字段 professionalSkills */
  professionalSkills: string
  /** 对应后端字段 coreCompetency */
  coreCompetency: string
  /** 对应后端字段 leadershipScore */
  leadershipScore: number
  /** 对应后端字段 innovationScore */
  innovationScore: number
  /** 对应后端字段 collaborationScore */
  collaborationScore: number
  /** 对应后端字段 developmentPotential */
  developmentPotential: string
  /** 对应后端字段 careerPlan */
  careerPlan: string
  /** 对应后端字段 talentTags */
  talentTags: string
  /** 对应后端字段 evaluationDate */
  evaluationDate: string
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
 * TalentUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Talent.TaktTalentUpdateDto）
 */
export interface TalentUpdate extends TalentCreate {
  /** 对应后端字段 talentId */
  talentId: string
}

/**
 * TalentStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Talent.TaktTalentStatusDto）
 */
export interface TalentStatus {
  /** 对应后端字段 talentId */
  talentId: string
  /** 对应后端字段 status */
  status: number
}
