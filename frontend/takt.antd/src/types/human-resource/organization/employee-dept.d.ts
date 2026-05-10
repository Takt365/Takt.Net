// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/organization/employee-dept
// 文件名称：employee-dept.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-dept相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeDept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktEmployeeDeptDto）
 */
export interface EmployeeDept extends TaktEntityBase {
  /** 对应后端字段 employeeDeptId */
  employeeDeptId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 deptId */
  deptId: string
}

/**
 * EmployeeDeptQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktEmployeeDeptQueryDto）
 */
export interface EmployeeDeptQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 deptId */
  deptId?: string
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
 * EmployeeDeptCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktEmployeeDeptCreateDto）
 */
export interface EmployeeDeptCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EmployeeDeptUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktEmployeeDeptUpdateDto）
 */
export interface EmployeeDeptUpdate extends EmployeeDeptCreate {
  /** 对应后端字段 employeeDeptId */
  employeeDeptId: string
}
