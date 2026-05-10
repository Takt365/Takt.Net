// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-skill
// 文件名称：employee-skill.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-skill相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeSkill类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeSkillDto）
 */
export interface EmployeeSkill extends TaktEntityBase {
  /** 对应后端字段 employeeSkillId */
  employeeSkillId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 skillName */
  skillName: string
  /** 对应后端字段 skillLevel */
  skillLevel: number
  /** 对应后端字段 certificateName */
  certificateName?: string
  /** 对应后端字段 certificateNo */
  certificateNo?: string
  /** 对应后端字段 obtainedDate */
  obtainedDate?: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
}

/**
 * EmployeeSkillQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeSkillQueryDto）
 */
export interface EmployeeSkillQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 skillName */
  skillName?: string
  /** 对应后端字段 skillLevel */
  skillLevel?: number
  /** 对应后端字段 certificateName */
  certificateName?: string
  /** 对应后端字段 certificateNo */
  certificateNo?: string
  /** 对应后端字段 obtainedDate */
  obtainedDate?: string
  /** 对应后端字段 obtainedDateStart */
  obtainedDateStart?: string
  /** 对应后端字段 obtainedDateEnd */
  obtainedDateEnd?: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
  /** 对应后端字段 expiryDateStart */
  expiryDateStart?: string
  /** 对应后端字段 expiryDateEnd */
  expiryDateEnd?: string
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
 * EmployeeSkillCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeSkillCreateDto）
 */
export interface EmployeeSkillCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 skillName */
  skillName: string
  /** 对应后端字段 skillLevel */
  skillLevel: number
  /** 对应后端字段 certificateName */
  certificateName?: string
  /** 对应后端字段 certificateNo */
  certificateNo?: string
  /** 对应后端字段 obtainedDate */
  obtainedDate?: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EmployeeSkillUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeSkillUpdateDto）
 */
export interface EmployeeSkillUpdate extends EmployeeSkillCreate {
  /** 对应后端字段 employeeSkillId */
  employeeSkillId: string
}
