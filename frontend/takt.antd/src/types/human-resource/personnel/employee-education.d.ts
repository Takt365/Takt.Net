// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-education
// 文件名称：employee-education.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工教育经历相关类型定义，对应后端 TaktEmployeeEducationDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 员工教育经历类型（对应后端 TaktEmployeeEducationDto）
 */
export interface EmployeeEducation extends TaktEntityBase {
  /** 员工教育经历ID（对应后端 EmployeeEducationId） */
  employeeEducationId: string
  /** 员工ID（对应后端 EmployeeId） */
  employeeId: string
  /** 学历层次（0=其他，1=高中及以下，2=大专，3=本科，4=硕士，5=博士） */
  educationLevel: number
  /** 学校名称 */
  schoolName: string
  /** 专业 */
  majorName?: string
  /** 学位（0=无，1=学士，2=硕士，3=博士） */
  degreeLevel: number
  /** 入学日期 */
  startDate?: string
  /** 毕业日期 */
  endDate?: string
  /** 是否最高学历（0=否，1=是） */
  isHighest: number
  /** 证书编号 */
  certificateNo?: string
}

/**
 * 员工教育经历查询类型（对应后端 TaktEmployeeEducationQueryDto）
 */
export interface EmployeeEducationQuery extends TaktPagedQuery {
  /** 员工ID（精确） */
  employeeId?: string
  /** 学历层次（精确） */
  educationLevel?: number
  /** 是否最高学历（精确） */
  isHighest?: number
  /** 学校名称（模糊） */
  schoolName?: string
}

/**
 * 创建员工教育经历类型（对应后端 TaktEmployeeEducationCreateDto）
 */
export interface EmployeeEducationCreate {
  /** 员工ID */
  employeeId: string
  /** 学历层次 */
  educationLevel: number
  /** 学校名称 */
  schoolName: string
  /** 专业 */
  majorName?: string
  /** 学位 */
  degreeLevel: number
  /** 入学日期 */
  startDate?: string
  /** 毕业日期 */
  endDate?: string
  /** 是否最高学历 */
  isHighest: number
  /** 证书编号 */
  certificateNo?: string
}

/**
 * 更新员工教育经历类型（对应后端 TaktEmployeeEducationUpdateDto）
 */
export interface EmployeeEducationUpdate extends EmployeeEducationCreate {
  /** 员工教育经历ID */
  employeeEducationId: string
}
