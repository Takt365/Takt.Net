// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/compensation-benefits/salary-adjustment
// 文件名称：salary-adjustment.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：salary-adjustment相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SalaryAdjustment类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryAdjustmentDto）
 */
export interface SalaryAdjustment extends TaktEntityBase {
  /** 对应后端字段 salaryAdjustmentId */
  salaryAdjustmentId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 adjustmentType */
  adjustmentType: string
  /** 对应后端字段 adjustmentDate */
  adjustmentDate: string
  /** 对应后端字段 previousSalary */
  previousSalary: number
  /** 对应后端字段 newSalary */
  newSalary: number
  /** 对应后端字段 adjustmentAmount */
  adjustmentAmount: number
  /** 对应后端字段 adjustmentPercentage */
  adjustmentPercentage: number
  /** 对应后端字段 adjustmentReason */
  adjustmentReason: string
  /** 对应后端字段 previousSalaryLevel */
  previousSalaryLevel: string
  /** 对应后端字段 newSalaryLevel */
  newSalaryLevel: string
  /** 对应后端字段 approverId */
  approverId: string
  /** 对应后端字段 approvalDate */
  approvalDate: string
  /** 对应后端字段 approvalComments */
  approvalComments: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 status */
  status: number
}

/**
 * SalaryAdjustmentQuery类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryAdjustmentQueryDto）
 */
export interface SalaryAdjustmentQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 adjustmentType */
  adjustmentType?: string
  /** 对应后端字段 adjustmentDate */
  adjustmentDate?: string
  /** 对应后端字段 adjustmentDateStart */
  adjustmentDateStart?: string
  /** 对应后端字段 adjustmentDateEnd */
  adjustmentDateEnd?: string
  /** 对应后端字段 previousSalary */
  previousSalary?: number
  /** 对应后端字段 newSalary */
  newSalary?: number
  /** 对应后端字段 adjustmentAmount */
  adjustmentAmount?: number
  /** 对应后端字段 adjustmentPercentage */
  adjustmentPercentage?: number
  /** 对应后端字段 adjustmentReason */
  adjustmentReason?: string
  /** 对应后端字段 previousSalaryLevel */
  previousSalaryLevel?: string
  /** 对应后端字段 newSalaryLevel */
  newSalaryLevel?: string
  /** 对应后端字段 approverId */
  approverId?: string
  /** 对应后端字段 approvalDate */
  approvalDate?: string
  /** 对应后端字段 approvalDateStart */
  approvalDateStart?: string
  /** 对应后端字段 approvalDateEnd */
  approvalDateEnd?: string
  /** 对应后端字段 approvalComments */
  approvalComments?: string
  /** 对应后端字段 effectiveDate */
  effectiveDate?: string
  /** 对应后端字段 effectiveDateStart */
  effectiveDateStart?: string
  /** 对应后端字段 effectiveDateEnd */
  effectiveDateEnd?: string
  /** 对应后端字段 status */
  status?: number
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
 * SalaryAdjustmentCreate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryAdjustmentCreateDto）
 */
export interface SalaryAdjustmentCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 adjustmentType */
  adjustmentType: string
  /** 对应后端字段 adjustmentDate */
  adjustmentDate: string
  /** 对应后端字段 previousSalary */
  previousSalary: number
  /** 对应后端字段 newSalary */
  newSalary: number
  /** 对应后端字段 adjustmentAmount */
  adjustmentAmount: number
  /** 对应后端字段 adjustmentPercentage */
  adjustmentPercentage: number
  /** 对应后端字段 adjustmentReason */
  adjustmentReason: string
  /** 对应后端字段 previousSalaryLevel */
  previousSalaryLevel: string
  /** 对应后端字段 newSalaryLevel */
  newSalaryLevel: string
  /** 对应后端字段 approverId */
  approverId: string
  /** 对应后端字段 approvalDate */
  approvalDate: string
  /** 对应后端字段 approvalComments */
  approvalComments: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * SalaryAdjustmentUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryAdjustmentUpdateDto）
 */
export interface SalaryAdjustmentUpdate extends SalaryAdjustmentCreate {
  /** 对应后端字段 salaryAdjustmentId */
  salaryAdjustmentId: string
}

/**
 * SalaryAdjustmentStatus类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryAdjustmentStatusDto）
 */
export interface SalaryAdjustmentStatus {
  /** 对应后端字段 salaryAdjustmentId */
  salaryAdjustmentId: string
  /** 对应后端字段 status */
  status: number
}
