// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-career
// 文件名称：employee-career.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-career相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeCareer类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCareerDto）
 */
export interface EmployeeCareer extends TaktEntityBase {
  /** 对应后端字段 careerId */
  careerId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 deptName */
  deptName: string
  /** 对应后端字段 postId */
  postId?: string
  /** 对应后端字段 postName */
  postName?: string
  /** 对应后端字段 jobLevel */
  jobLevel?: string
  /** 对应后端字段 jobTitle */
  jobTitle?: string
  /** 对应后端字段 joinDate */
  joinDate?: string
  /** 对应后端字段 regularizationDate */
  regularizationDate?: string
  /** 对应后端字段 leaveDate */
  leaveDate?: string
  /** 对应后端字段 workYears */
  workYears?: number
  /** 对应后端字段 workLocation */
  workLocation?: string
  /** 对应后端字段 workNature */
  workNature: number
  /** 对应后端字段 employmentType */
  employmentType: number
  /** 对应后端字段 isPrimary */
  isPrimary: number
  /** 对应后端字段 directManagerId */
  directManagerId?: string
  /** 对应后端字段 directManagerName */
  directManagerName?: string
}

/**
 * EmployeeCareerQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCareerQueryDto）
 */
export interface EmployeeCareerQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 postId */
  postId?: string
  /** 对应后端字段 isPrimary */
  isPrimary?: number
}

/**
 * EmployeeCareerCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCareerCreateDto）
 */
export interface EmployeeCareerCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 deptName */
  deptName: string
  /** 对应后端字段 postId */
  postId?: string
  /** 对应后端字段 postName */
  postName?: string
  /** 对应后端字段 jobLevel */
  jobLevel?: string
  /** 对应后端字段 jobTitle */
  jobTitle?: string
  /** 对应后端字段 joinDate */
  joinDate?: string
  /** 对应后端字段 regularizationDate */
  regularizationDate?: string
  /** 对应后端字段 leaveDate */
  leaveDate?: string
  /** 对应后端字段 workYears */
  workYears?: number
  /** 对应后端字段 workLocation */
  workLocation?: string
  /** 对应后端字段 workNature */
  workNature: number
  /** 对应后端字段 employmentType */
  employmentType: number
  /** 对应后端字段 isPrimary */
  isPrimary: number
  /** 对应后端字段 directManagerId */
  directManagerId?: string
  /** 对应后端字段 directManagerName */
  directManagerName?: string
}

/**
 * EmployeeCareerUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCareerUpdateDto）
 */
export interface EmployeeCareerUpdate extends EmployeeCareerCreate {
  /** 对应后端字段 careerId */
  careerId: string
}
