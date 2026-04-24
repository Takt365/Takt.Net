// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-family
// 文件名称：employee-family.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-family相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeFamily类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeFamilyDto）
 */
export interface EmployeeFamily extends TaktEntityBase {
  /** 对应后端字段 employeeFamilyId */
  employeeFamilyId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 memberName */
  memberName: string
  /** 对应后端字段 relationType */
  relationType: number
  /** 对应后端字段 phoneNumber */
  phoneNumber?: string
  /** 对应后端字段 workUnit */
  workUnit?: string
  /** 对应后端字段 jobTitle */
  jobTitle?: string
  /** 对应后端字段 birthDate */
  birthDate?: string
  /** 对应后端字段 isEmergencyContact */
  isEmergencyContact: number
}

/**
 * EmployeeFamilyQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeFamilyQueryDto）
 */
export interface EmployeeFamilyQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 relationType */
  relationType?: number
  /** 对应后端字段 memberName */
  memberName?: string
}

/**
 * EmployeeFamilyCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeFamilyCreateDto）
 */
export interface EmployeeFamilyCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 memberName */
  memberName: string
  /** 对应后端字段 relationType */
  relationType: number
  /** 对应后端字段 phoneNumber */
  phoneNumber?: string
  /** 对应后端字段 workUnit */
  workUnit?: string
  /** 对应后端字段 jobTitle */
  jobTitle?: string
  /** 对应后端字段 birthDate */
  birthDate?: string
  /** 对应后端字段 isEmergencyContact */
  isEmergencyContact: number
}

/**
 * EmployeeFamilyUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeFamilyUpdateDto）
 */
export interface EmployeeFamilyUpdate extends EmployeeFamilyCreate {
  /** 对应后端字段 employeeFamilyId */
  employeeFamilyId: string
}
