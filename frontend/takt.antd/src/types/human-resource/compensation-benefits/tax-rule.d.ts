// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/compensation-benefits/tax-rule
// 文件名称：tax-rule.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：tax-rule相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * TaxRule类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktTaxRuleDto）
 */
export interface TaxRule extends TaktEntityBase {
  /** 对应后端字段 taxRuleId */
  taxRuleId: string
  /** 对应后端字段 ruleCode */
  ruleCode: string
  /** 对应后端字段 ruleName */
  ruleName: string
  /** 对应后端字段 taxYear */
  taxYear: number
  /** 对应后端字段 taxThreshold */
  taxThreshold: number
  /** 对应后端字段 taxableIncomeMin */
  taxableIncomeMin: number
  /** 对应后端字段 taxableIncomeMax */
  taxableIncomeMax: number
  /** 对应后端字段 taxRate */
  taxRate: number
  /** 对应后端字段 quickDeduction */
  quickDeduction: number
  /** 对应后端字段 specialDeductionStandard */
  specialDeductionStandard: number
  /** 对应后端字段 socialSecurityDeductionRate */
  socialSecurityDeductionRate: number
  /** 对应后端字段 housingFundDeductionRate */
  housingFundDeductionRate: number
  /** 对应后端字段 calculationFormula */
  calculationFormula: string
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 status */
  status: number
}

/**
 * TaxRuleQuery类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktTaxRuleQueryDto）
 */
export interface TaxRuleQuery extends TaktPagedQuery {
  /** 对应后端字段 ruleCode */
  ruleCode?: string
  /** 对应后端字段 ruleName */
  ruleName?: string
  /** 对应后端字段 taxYear */
  taxYear?: number
  /** 对应后端字段 taxThreshold */
  taxThreshold?: number
  /** 对应后端字段 taxableIncomeMin */
  taxableIncomeMin?: number
  /** 对应后端字段 taxableIncomeMax */
  taxableIncomeMax?: number
  /** 对应后端字段 taxRate */
  taxRate?: number
  /** 对应后端字段 quickDeduction */
  quickDeduction?: number
  /** 对应后端字段 specialDeductionStandard */
  specialDeductionStandard?: number
  /** 对应后端字段 socialSecurityDeductionRate */
  socialSecurityDeductionRate?: number
  /** 对应后端字段 housingFundDeductionRate */
  housingFundDeductionRate?: number
  /** 对应后端字段 calculationFormula */
  calculationFormula?: string
  /** 对应后端字段 description */
  description?: string
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
 * TaxRuleCreate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktTaxRuleCreateDto）
 */
export interface TaxRuleCreate {
  /** 对应后端字段 ruleCode */
  ruleCode: string
  /** 对应后端字段 ruleName */
  ruleName: string
  /** 对应后端字段 taxYear */
  taxYear: number
  /** 对应后端字段 taxThreshold */
  taxThreshold: number
  /** 对应后端字段 taxableIncomeMin */
  taxableIncomeMin: number
  /** 对应后端字段 taxableIncomeMax */
  taxableIncomeMax: number
  /** 对应后端字段 taxRate */
  taxRate: number
  /** 对应后端字段 quickDeduction */
  quickDeduction: number
  /** 对应后端字段 specialDeductionStandard */
  specialDeductionStandard: number
  /** 对应后端字段 socialSecurityDeductionRate */
  socialSecurityDeductionRate: number
  /** 对应后端字段 housingFundDeductionRate */
  housingFundDeductionRate: number
  /** 对应后端字段 calculationFormula */
  calculationFormula: string
  /** 对应后端字段 description */
  description: string
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
 * TaxRuleUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktTaxRuleUpdateDto）
 */
export interface TaxRuleUpdate extends TaxRuleCreate {
  /** 对应后端字段 taxRuleId */
  taxRuleId: string
}

/**
 * TaxRuleStatus类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktTaxRuleStatusDto）
 */
export interface TaxRuleStatus {
  /** 对应后端字段 taxRuleId */
  taxRuleId: string
  /** 对应后端字段 status */
  status: number
}
