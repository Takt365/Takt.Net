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
  nationality: number
  /** 对应后端字段 political */
  political: number
  /** 对应后端字段 marital */
  marital: number
  /** 对应后端字段 nativePlace */
  nativePlace?: string
  /** 对应后端字段 currentAddress */
  currentAddress?: string
  /** 对应后端字段 registeredAddress */
  registeredAddress?: string
  /** 对应后端字段 employeeStatus */
  employeeStatus: number
  /** 对应后端字段 employeeDelegates */
  employeeDelegates?: unknown[]
  /** 对应后端字段 employeeCareers */
  employeeCareers?: unknown[]
  /** 对应后端字段 employeeAttachments */
  employeeAttachments?: unknown[]
  /** 对应后端字段 employeeContracts */
  employeeContracts?: unknown[]
  /** 对应后端字段 employeeEducations */
  employeeEducations?: unknown[]
  /** 对应后端字段 employeeFamilies */
  employeeFamilies?: unknown[]
  /** 对应后端字段 employeeSkills */
  employeeSkills?: unknown[]
  /** 对应后端字段 employeeTransfers */
  employeeTransfers?: unknown[]
  /** 对应后端字段 employeeWorks */
  employeeWorks?: unknown[]
}

/**
 * EmployeeQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeQueryDto）
 */
export interface EmployeeQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeCode */
  employeeCode?: string
  /** 对应后端字段 realName */
  realName?: string
  /** 对应后端字段 formerName */
  formerName?: string
  /** 对应后端字段 fullName */
  fullName?: string
  /** 对应后端字段 nativeName */
  nativeName?: string
  /** 对应后端字段 displayName */
  displayName?: string
  /** 对应后端字段 gender */
  gender?: number
  /** 对应后端字段 birthDate */
  birthDate?: string
  /** 对应后端字段 birthDateStart */
  birthDateStart?: string
  /** 对应后端字段 birthDateEnd */
  birthDateEnd?: string
  /** 对应后端字段 age */
  age?: number
  /** 对应后端字段 idCard */
  idCard?: string
  /** 对应后端字段 phone */
  phone?: string
  /** 对应后端字段 email */
  email?: string
  /** 对应后端字段 avatar */
  avatar?: string
  /** 对应后端字段 nationality */
  nationality?: number
  /** 对应后端字段 political */
  political?: number
  /** 对应后端字段 marital */
  marital?: number
  /** 对应后端字段 nativePlace */
  nativePlace?: string
  /** 对应后端字段 currentAddress */
  currentAddress?: string
  /** 对应后端字段 registeredAddress */
  registeredAddress?: string
  /** 对应后端字段 employeeStatus */
  employeeStatus?: number
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
 * EmployeeCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCreateDto）
 */
export interface EmployeeCreate {
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
  nationality: number
  /** 对应后端字段 political */
  political: number
  /** 对应后端字段 marital */
  marital: number
  /** 对应后端字段 nativePlace */
  nativePlace?: string
  /** 对应后端字段 currentAddress */
  currentAddress?: string
  /** 对应后端字段 registeredAddress */
  registeredAddress?: string
  /** 对应后端字段 employeeStatus */
  employeeStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 employeeDelegates */
  employeeDelegates?: unknown[]
  /** 对应后端字段 employeeCareers */
  employeeCareers?: unknown[]
  /** 对应后端字段 employeeAttachments */
  employeeAttachments?: unknown[]
  /** 对应后端字段 employeeContracts */
  employeeContracts?: unknown[]
  /** 对应后端字段 employeeEducations */
  employeeEducations?: unknown[]
  /** 对应后端字段 employeeFamilies */
  employeeFamilies?: unknown[]
  /** 对应后端字段 employeeSkills */
  employeeSkills?: unknown[]
  /** 对应后端字段 employeeTransfers */
  employeeTransfers?: unknown[]
  /** 对应后端字段 employeeWorks */
  employeeWorks?: unknown[]
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
