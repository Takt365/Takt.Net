// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee
// 文件名称：employee.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Employee类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeDto）
 */
export interface Employee extends TaktEntityBase {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 employeeCode */
  employeeCode: string
  /** 对应后端字段 realName */
  realName: string
  /** 对应后端字段 formerName */
  formerName?: string
  /** 对应后端字段 fullName */
  fullName?: string
  /** 对应后端字段 nativeName */
  nativeName?: string
  /** 对应后端字段 displayName */
  displayName?: string
  /** 对应后端字段 gender */
  gender: number
  /** 对应后端字段 birthDate */
  birthDate: string
  /** 对应后端字段 age */
  age?: number
  /** 对应后端字段 idCard */
  idCard: string
  /** 对应后端字段 phone */
  phone?: string
  /** 对应后端字段 email */
  email?: string
  /** 对应后端字段 avatar */
  avatar?: string
  /** 对应后端字段 nationality */
  nationality: string
  /** 对应后端字段 politicalStatus */
  politicalStatus: number
  /** 对应后端字段 maritalStatus */
  maritalStatus: number
  /** 对应后端字段 nativePlace */
  nativePlace?: string
  /** 对应后端字段 currentAddress */
  currentAddress?: string
  /** 对应后端字段 registeredAddress */
  registeredAddress?: string
  /** 对应后端字段 employeeStatus */
  employeeStatus: number
}

/**
 * EmployeeQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeQueryDto）
 */
export interface EmployeeQuery extends TaktPagedQuery {
  /** 对应后端字段 realName */
  realName?: string
  /** 对应后端字段 employeeCode */
  employeeCode?: string
  /** 对应后端字段 phone */
  phone?: string
  /** 对应后端字段 employeeStatus */
  employeeStatus?: number
}

/**
 * EmployeeCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCreateDto）
 */
export interface EmployeeCreate {
  /** 对应后端字段 employeeCode */
  employeeCode: string
  /** 对应后端字段 isSystemEmployeeCode */
  isSystemEmployeeCode: boolean
  /** 对应后端字段 realName */
  realName: string
  /** 对应后端字段 formerName */
  formerName?: string
  /** 对应后端字段 fullName */
  fullName?: string
  /** 对应后端字段 nativeName */
  nativeName?: string
  /** 对应后端字段 displayName */
  displayName?: string
  /** 对应后端字段 gender */
  gender: number
  /** 对应后端字段 birthDate */
  birthDate: string
  /** 对应后端字段 age */
  age?: number
  /** 对应后端字段 idCard */
  idCard: string
  /** 对应后端字段 phone */
  phone?: string
  /** 对应后端字段 email */
  email?: string
  /** 对应后端字段 avatar */
  avatar?: string
  /** 对应后端字段 nationality */
  nationality: string
  /** 对应后端字段 politicalStatus */
  politicalStatus: number
  /** 对应后端字段 maritalStatus */
  maritalStatus: number
  /** 对应后端字段 nativePlace */
  nativePlace?: string
  /** 对应后端字段 currentAddress */
  currentAddress?: string
  /** 对应后端字段 registeredAddress */
  registeredAddress?: string
  /** 对应后端字段 employeeStatus */
  employeeStatus: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EmployeeUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeUpdateDto）
 */
export interface EmployeeUpdate extends EmployeeCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
}

/**
 * EmployeeStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeStatusDto）
 */
export interface EmployeeStatus {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 employeeStatus */
  employeeStatus: number
}
