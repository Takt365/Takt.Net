// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-delegate
// 文件名称：employee-delegate.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-delegate相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeDelegate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeDelegateDto）
 */
export interface EmployeeDelegate extends TaktEntityBase {
  /** 对应后端字段 employeeDelegateId */
  employeeDelegateId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 delegateMode */
  delegateMode: number
  /** 对应后端字段 delegateEmployeeId */
  delegateEmployeeId?: string
  /** 对应后端字段 delegateDeptId */
  delegateDeptId?: string
  /** 对应后端字段 delegatePostId */
  delegatePostId?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * EmployeeDelegateQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeDelegateQueryDto）
 */
export interface EmployeeDelegateQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 delegateMode */
  delegateMode?: number
  /** 对应后端字段 delegateEmployeeId */
  delegateEmployeeId?: string
  /** 对应后端字段 delegateDeptId */
  delegateDeptId?: string
  /** 对应后端字段 delegatePostId */
  delegatePostId?: string
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
 * EmployeeDelegateCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeDelegateCreateDto）
 */
export interface EmployeeDelegateCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 delegateMode */
  delegateMode: number
  /** 对应后端字段 delegateEmployeeId */
  delegateEmployeeId?: string
  /** 对应后端字段 delegateDeptId */
  delegateDeptId?: string
  /** 对应后端字段 delegatePostId */
  delegatePostId?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EmployeeDelegateUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeDelegateUpdateDto）
 */
export interface EmployeeDelegateUpdate extends EmployeeDelegateCreate {
  /** 对应后端字段 employeeDelegateId */
  employeeDelegateId: string
}

/**
 * EmployeeDelegateSort类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeDelegateSortDto）
 */
export interface EmployeeDelegateSort {
  /** 对应后端字段 employeeDelegateId */
  employeeDelegateId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
