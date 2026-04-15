// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-family
// 文件名称：employee-family.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工家庭成员相关类型定义，对应后端 TaktEmployeeFamilyDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 员工家庭成员类型（对应后端 TaktEmployeeFamilyDto）
 */
export interface EmployeeFamily extends TaktEntityBase {
  /** 员工家庭成员ID（对应后端 EmployeeFamilyId） */
  employeeFamilyId: string
  /** 员工ID（对应后端 EmployeeId） */
  employeeId: string
  /** 成员姓名 */
  memberName: string
  /** 关系类型 */
  relationType: number
  /** 联系电话 */
  phoneNumber?: string
  /** 工作单位 */
  workUnit?: string
  /** 职务 */
  jobTitle?: string
  /** 出生日期 */
  birthDate?: string
  /** 是否紧急联系人（0=否，1=是） */
  isEmergencyContact: number
}

/**
 * 员工家庭成员查询类型（对应后端 TaktEmployeeFamilyQueryDto）
 */
export interface EmployeeFamilyQuery extends TaktPagedQuery {
  /** 员工ID（精确） */
  employeeId?: string
  /** 关系类型（精确） */
  relationType?: number
  /** 成员姓名（模糊） */
  memberName?: string
}

/**
 * 创建员工家庭成员类型（对应后端 TaktEmployeeFamilyCreateDto）
 */
export interface EmployeeFamilyCreate {
  /** 员工ID */
  employeeId: string
  /** 成员姓名 */
  memberName: string
  /** 关系类型 */
  relationType: number
  /** 联系电话 */
  phoneNumber?: string
  /** 工作单位 */
  workUnit?: string
  /** 职务 */
  jobTitle?: string
  /** 出生日期 */
  birthDate?: string
  /** 是否紧急联系人 */
  isEmergencyContact: number
}

/**
 * 更新员工家庭成员类型（对应后端 TaktEmployeeFamilyUpdateDto）
 */
export interface EmployeeFamilyUpdate extends EmployeeFamilyCreate {
  /** 员工家庭成员ID */
  employeeFamilyId: string
}
