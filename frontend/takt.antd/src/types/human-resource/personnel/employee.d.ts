// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee
// 文件名称：employee.d.ts
// 功能描述：员工相关类型定义，对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 员工类型（对应后端 TaktEmployeeDto）
 * 紧急联系人不在本 DTO；请使用「员工家庭成员」中 isEmergencyContact=1 的成员。
 */
export interface Employee extends TaktEntityBase {
  /** 员工ID（对应后端 EmployeeId，序列化为 string 以避免精度问题） */
  employeeId: string
  /** 员工编码 */
  employeeCode: string
  /** 实名（身份证/户口本姓名） */
  realName: string
  /** 曾用名 */
  formerName?: string
  /** 全名 */
  fullName?: string
  /** 本地化姓名（对应后端 NativeName） */
  nativeName?: string
  /** 显示名 */
  displayName?: string
  /** 性别（0=未知，1=男，2=女） */
  gender: number
  /** 出生日期 */
  birthDate?: string
  /** 年龄 */
  age?: number
  /** 身份证号 */
  idCard?: string
  /** 手机号 */
  phone?: string
  /** 邮箱 */
  email?: string
  /** 头像 */
  avatar?: string
  /** 民族 */
  nationality?: string
  /** 政治面貌（0=群众，1=团员，2=党员，3=其他） */
  politicalStatus: number
  /** 婚姻状况（0=未婚，1=已婚，2=离异，3=丧偶） */
  maritalStatus: number
  /** 籍贯 */
  nativePlace?: string
  /** 现居住地址 */
  currentAddress?: string
  /** 户籍地址 */
  registeredAddress?: string
  /** 紧急联系人 */
  emergencyContact?: string
  /** 紧急联系人电话 */
  emergencyContactPhone?: string
  /** 紧急联系人关系 */
  emergencyContactRelation?: string
  /** 员工状态（0=在职，1=离职，2=停薪留职，3=退休） */
  employeeStatus: number
}

/**
 * 员工查询类型（对应后端 TaktEmployeeQueryDto）
 */
export interface EmployeeQuery extends TaktPagedQuery {
  /** 员工编码（模糊） */
  employeeCode?: string
  /** 实名（模糊） */
  realName?: string
  /** 手机号（模糊） */
  phone?: string
  /** 员工状态（0=在职，1=离职，2=停薪留职，3=退休；不传为全部） */
  employeeStatus?: number
}

/**
 * 创建员工类型（对应后端 TaktEmployeeCreateDto）
 */
export interface EmployeeCreate {
  /** 创建时由服务端生成，可省略；更新时必填 */
  employeeCode?: string
  /** 是否使用用户编号(系统)规则（9 开头）。仅管理员(1)或超级管理员(2)可设为 true */
  isSystemEmployeeCode?: boolean
  /** 实名（后端必填） */
  realName: string
  formerName?: string
  fullName?: string
  nativeName?: string
  displayName?: string
  gender?: number
  birthDate?: string
  age?: number
  idCard?: string
  phone?: string
  email?: string
  avatar?: string
  nationality?: string
  politicalStatus?: number
  maritalStatus?: number
  nativePlace?: string
  currentAddress?: string
  registeredAddress?: string
  employeeStatus?: number
  remark?: string
}

/**
 * 更新员工类型（对应后端 TaktEmployeeUpdateDto）
 */
export interface EmployeeUpdate extends EmployeeCreate {
  /** 员工ID */
  employeeId: string
}

/**
 * 员工状态类型（对应后端 TaktEmployeeStatusDto，用于状态变更）
 */
export interface EmployeeStatusDto {
  employeeId: string
  employeeStatus: number
}
