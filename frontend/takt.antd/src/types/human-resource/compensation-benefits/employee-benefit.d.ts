// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/compensation-benefits/employee-benefit
// 文件名称：employee-benefit.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-benefit相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeBenefit类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktEmployeeBenefitDto）
 */
export interface EmployeeBenefit extends TaktEntityBase {
  /** 对应后端字段 employeeBenefitId */
  employeeBenefitId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 benefitPlanId */
  benefitPlanId: string
  /** 对应后端字段 benefitType */
  benefitType: string
  /** 对应后端字段 benefitName */
  benefitName: string
  /** 对应后端字段 benefitAmount */
  benefitAmount: number
  /** 对应后端字段 distributionMethod */
  distributionMethod: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 expiryDate */
  expiryDate: string
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 status */
  status: number
}

/**
 * EmployeeBenefitQuery类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktEmployeeBenefitQueryDto）
 */
export interface EmployeeBenefitQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 benefitPlanId */
  benefitPlanId?: string
  /** 对应后端字段 benefitType */
  benefitType?: string
  /** 对应后端字段 benefitName */
  benefitName?: string
  /** 对应后端字段 benefitAmount */
  benefitAmount?: number
  /** 对应后端字段 distributionMethod */
  distributionMethod?: string
  /** 对应后端字段 effectiveDate */
  effectiveDate?: string
  /** 对应后端字段 effectiveDateStart */
  effectiveDateStart?: string
  /** 对应后端字段 effectiveDateEnd */
  effectiveDateEnd?: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
  /** 对应后端字段 expiryDateStart */
  expiryDateStart?: string
  /** 对应后端字段 expiryDateEnd */
  expiryDateEnd?: string
  /** 对应后端字段 description */
  description?: string
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
 * EmployeeBenefitCreate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktEmployeeBenefitCreateDto）
 */
export interface EmployeeBenefitCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 benefitPlanId */
  benefitPlanId: string
  /** 对应后端字段 benefitType */
  benefitType: string
  /** 对应后端字段 benefitName */
  benefitName: string
  /** 对应后端字段 benefitAmount */
  benefitAmount: number
  /** 对应后端字段 distributionMethod */
  distributionMethod: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 expiryDate */
  expiryDate: string
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EmployeeBenefitUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktEmployeeBenefitUpdateDto）
 */
export interface EmployeeBenefitUpdate extends EmployeeBenefitCreate {
  /** 对应后端字段 employeeBenefitId */
  employeeBenefitId: string
}

/**
 * EmployeeBenefitStatus类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktEmployeeBenefitStatusDto）
 */
export interface EmployeeBenefitStatus {
  /** 对应后端字段 employeeBenefitId */
  employeeBenefitId: string
  /** 对应后端字段 status */
  status: number
}
