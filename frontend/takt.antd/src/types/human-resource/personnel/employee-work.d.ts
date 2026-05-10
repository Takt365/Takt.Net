// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-work
// 文件名称：employee-work.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-work相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeWork类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeWorkDto）
 */
export interface EmployeeWork extends TaktEntityBase {
  /** 对应后端字段 employeeWorkId */
  employeeWorkId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 companyName */
  companyName: string
  /** 对应后端字段 positionName */
  positionName?: string
  /** 对应后端字段 jobContent */
  jobContent?: string
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 endDate */
  endDate?: string
  /** 对应后端字段 witnessName */
  witnessName?: string
  /** 对应后端字段 witnessPhone */
  witnessPhone?: string
}

/**
 * EmployeeWorkQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeWorkQueryDto）
 */
export interface EmployeeWorkQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 companyName */
  companyName?: string
  /** 对应后端字段 positionName */
  positionName?: string
  /** 对应后端字段 jobContent */
  jobContent?: string
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 startDateStart */
  startDateStart?: string
  /** 对应后端字段 startDateEnd */
  startDateEnd?: string
  /** 对应后端字段 endDate */
  endDate?: string
  /** 对应后端字段 endDateStart */
  endDateStart?: string
  /** 对应后端字段 endDateEnd */
  endDateEnd?: string
  /** 对应后端字段 witnessName */
  witnessName?: string
  /** 对应后端字段 witnessPhone */
  witnessPhone?: string
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
 * EmployeeWorkCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeWorkCreateDto）
 */
export interface EmployeeWorkCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 companyName */
  companyName: string
  /** 对应后端字段 positionName */
  positionName?: string
  /** 对应后端字段 jobContent */
  jobContent?: string
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 endDate */
  endDate?: string
  /** 对应后端字段 witnessName */
  witnessName?: string
  /** 对应后端字段 witnessPhone */
  witnessPhone?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EmployeeWorkUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeWorkUpdateDto）
 */
export interface EmployeeWorkUpdate extends EmployeeWorkCreate {
  /** 对应后端字段 employeeWorkId */
  employeeWorkId: string
}
