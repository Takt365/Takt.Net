// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-education
// 文件名称：employee-education.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-education相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeEducation类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeEducationDto）
 */
export interface EmployeeEducation extends TaktEntityBase {
  /** 对应后端字段 employeeEducationId */
  employeeEducationId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 educationLevel */
  educationLevel: number
  /** 对应后端字段 schoolName */
  schoolName: string
  /** 对应后端字段 majorName */
  majorName?: string
  /** 对应后端字段 degreeLevel */
  degreeLevel: number
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 endDate */
  endDate?: string
  /** 对应后端字段 isHighest */
  isHighest: number
  /** 对应后端字段 certificateNo */
  certificateNo?: string
}

/**
 * EmployeeEducationQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeEducationQueryDto）
 */
export interface EmployeeEducationQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 educationLevel */
  educationLevel?: number
  /** 对应后端字段 isHighest */
  isHighest?: number
  /** 对应后端字段 schoolName */
  schoolName?: string
}

/**
 * EmployeeEducationCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeEducationCreateDto）
 */
export interface EmployeeEducationCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 educationLevel */
  educationLevel: number
  /** 对应后端字段 schoolName */
  schoolName: string
  /** 对应后端字段 majorName */
  majorName?: string
  /** 对应后端字段 degreeLevel */
  degreeLevel: number
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 endDate */
  endDate?: string
  /** 对应后端字段 isHighest */
  isHighest: number
  /** 对应后端字段 certificateNo */
  certificateNo?: string
}

/**
 * EmployeeEducationUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeEducationUpdateDto）
 */
export interface EmployeeEducationUpdate extends EmployeeEducationCreate {
  /** 对应后端字段 employeeEducationId */
  employeeEducationId: string
}
