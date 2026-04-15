// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-career
// 文件名称：employee-career.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工职业信息相关类型定义，对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCareerDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 员工职业信息类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCareerDto）
 */
export interface EmployeeCareer extends TaktEntityBase {
  /** 职业记录ID（对应后端 CareerId，序列化为string以避免Javascript精度问题） */
  careerId: string
  /** 员工ID（对应后端 EmployeeId，序列化为string以避免Javascript精度问题） */
  employeeId: string
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId: string
  /** 部门名称 */
  deptName: string
  /** 岗位ID（对应后端 PostId，序列化为string以避免Javascript精度问题） */
  postId?: string
  /** 岗位名称 */
  postName?: string
  /** 职级 */
  jobLevel?: string
  /** 职位 */
  jobTitle?: string
  /** 入职日期 */
  joinDate?: string
  /** 转正日期 */
  regularizationDate?: string
  /** 离职日期 */
  leaveDate?: string
  /** 工作年限 */
  workYears?: number
  /** 工作地点 */
  workLocation?: string
  /** 工作性质（0=全职，1=兼职，2=实习，3=外包，4=其他） */
  workNature: number
  /** 用工形式（0=正式，1=合同，2=派遣，3=其他） */
  employmentType: number
  /** 是否主职（0=否，1=是） */
  isPrimary: number
  /** 直接上级员工ID（对应后端 DirectManagerId，序列化为string以避免Javascript精度问题） */
  directManagerId?: string
  /** 直接上级姓名 */
  directManagerName?: string
}

/**
 * 员工职业信息查询类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCareerQueryDto）
 */
export interface EmployeeCareerQuery extends TaktPagedQuery {
  /** 员工ID（精确） */
  employeeId?: string
  /** 部门ID（精确） */
  deptId?: string
  /** 岗位ID（精确） */
  postId?: string
  /** 是否主职（0=否，1=是；null 表示全部） */
  isPrimary?: number
}

/**
 * 创建员工职业信息类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCareerCreateDto）
 */
export interface EmployeeCareerCreate {
  /** 员工ID */
  employeeId: string
  /** 部门ID */
  deptId: string
  /** 部门名称 */
  deptName: string
  /** 岗位ID */
  postId?: string
  /** 岗位名称 */
  postName?: string
  /** 职级 */
  jobLevel?: string
  /** 职位 */
  jobTitle?: string
  /** 入职日期 */
  joinDate?: string
  /** 转正日期 */
  regularizationDate?: string
  /** 离职日期 */
  leaveDate?: string
  /** 工作年限 */
  workYears?: number
  /** 工作地点 */
  workLocation?: string
  /** 工作性质（0=全职，1=兼职，2=实习，3=外包，4=其他） */
  workNature: number
  /** 用工形式（0=正式，1=合同，2=派遣，3=其他） */
  employmentType: number
  /** 是否主职（0=否，1=是） */
  isPrimary: number
  /** 直接上级员工ID */
  directManagerId?: string
  /** 直接上级姓名 */
  directManagerName?: string
}

/**
 * 更新员工职业信息类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCareerUpdateDto）
 */
export interface EmployeeCareerUpdate extends EmployeeCareerCreate {
  /** 职业记录ID（对应后端 CareerId，序列化为string以避免Javascript精度问题） */
  careerId: string
}
