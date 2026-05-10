// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/compensation-benefits/compensation-benefit
// 文件名称：compensation-benefit.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：compensation-benefit相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CompensationBenefit类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktCompensationBenefitDto）
 */
export interface CompensationBenefit extends TaktEntityBase {
  /** 对应后端字段 compensationBenefitId */
  compensationBenefitId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 baseSalary */
  baseSalary: number
  /** 对应后端字段 positionAllowance */
  positionAllowance: number
  /** 对应后端字段 performanceBonus */
  performanceBonus: number
  /** 对应后端字段 overtimePay */
  overtimePay: number
  /** 对应后端字段 transportAllowance */
  transportAllowance: number
  /** 对应后端字段 mealAllowance */
  mealAllowance: number
  /** 对应后端字段 housingAllowance */
  housingAllowance: number
  /** 对应后端字段 socialSecurityBase */
  socialSecurityBase: number
  /** 对应后端字段 housingFundBase */
  housingFundBase: number
  /** 对应后端字段 otherBenefits */
  otherBenefits: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 status */
  status: number
}

/**
 * CompensationBenefitQuery类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktCompensationBenefitQueryDto）
 */
export interface CompensationBenefitQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 baseSalary */
  baseSalary?: number
  /** 对应后端字段 positionAllowance */
  positionAllowance?: number
  /** 对应后端字段 performanceBonus */
  performanceBonus?: number
  /** 对应后端字段 overtimePay */
  overtimePay?: number
  /** 对应后端字段 transportAllowance */
  transportAllowance?: number
  /** 对应后端字段 mealAllowance */
  mealAllowance?: number
  /** 对应后端字段 housingAllowance */
  housingAllowance?: number
  /** 对应后端字段 socialSecurityBase */
  socialSecurityBase?: number
  /** 对应后端字段 housingFundBase */
  housingFundBase?: number
  /** 对应后端字段 otherBenefits */
  otherBenefits?: string
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
 * CompensationBenefitCreate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktCompensationBenefitCreateDto）
 */
export interface CompensationBenefitCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 baseSalary */
  baseSalary: number
  /** 对应后端字段 positionAllowance */
  positionAllowance: number
  /** 对应后端字段 performanceBonus */
  performanceBonus: number
  /** 对应后端字段 overtimePay */
  overtimePay: number
  /** 对应后端字段 transportAllowance */
  transportAllowance: number
  /** 对应后端字段 mealAllowance */
  mealAllowance: number
  /** 对应后端字段 housingAllowance */
  housingAllowance: number
  /** 对应后端字段 socialSecurityBase */
  socialSecurityBase: number
  /** 对应后端字段 housingFundBase */
  housingFundBase: number
  /** 对应后端字段 otherBenefits */
  otherBenefits: string
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
 * CompensationBenefitUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktCompensationBenefitUpdateDto）
 */
export interface CompensationBenefitUpdate extends CompensationBenefitCreate {
  /** 对应后端字段 compensationBenefitId */
  compensationBenefitId: string
}

/**
 * CompensationBenefitStatus类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktCompensationBenefitStatusDto）
 */
export interface CompensationBenefitStatus {
  /** 对应后端字段 compensationBenefitId */
  compensationBenefitId: string
  /** 对应后端字段 status */
  status: number
}
