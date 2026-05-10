// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/compensation-benefits/salary-component
// 文件名称：salary-component.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：salary-component相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SalaryComponent类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryComponentDto）
 */
export interface SalaryComponent extends TaktEntityBase {
  /** 对应后端字段 salaryComponentId */
  salaryComponentId: string
  /** 对应后端字段 componentCode */
  componentCode: string
  /** 对应后端字段 componentName */
  componentName: string
  /** 对应后端字段 componentType */
  componentType: string
  /** 对应后端字段 calculationMethod */
  calculationMethod: string
  /** 对应后端字段 calculationFormula */
  calculationFormula: string
  /** 对应后端字段 fixedAmount */
  fixedAmount: number
  /** 对应后端字段 percentage */
  percentage: number
  /** 对应后端字段 isTaxable */
  isTaxable: number
  /** 对应后端字段 isSocialSecurityBase */
  isSocialSecurityBase: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 status */
  status: number
}

/**
 * SalaryComponentQuery类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryComponentQueryDto）
 */
export interface SalaryComponentQuery extends TaktPagedQuery {
  /** 对应后端字段 componentCode */
  componentCode?: string
  /** 对应后端字段 componentName */
  componentName?: string
  /** 对应后端字段 componentType */
  componentType?: string
  /** 对应后端字段 calculationMethod */
  calculationMethod?: string
  /** 对应后端字段 calculationFormula */
  calculationFormula?: string
  /** 对应后端字段 fixedAmount */
  fixedAmount?: number
  /** 对应后端字段 percentage */
  percentage?: number
  /** 对应后端字段 isTaxable */
  isTaxable?: number
  /** 对应后端字段 isSocialSecurityBase */
  isSocialSecurityBase?: number
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
 * SalaryComponentCreate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryComponentCreateDto）
 */
export interface SalaryComponentCreate {
  /** 对应后端字段 componentCode */
  componentCode: string
  /** 对应后端字段 componentName */
  componentName: string
  /** 对应后端字段 componentType */
  componentType: string
  /** 对应后端字段 calculationMethod */
  calculationMethod: string
  /** 对应后端字段 calculationFormula */
  calculationFormula: string
  /** 对应后端字段 fixedAmount */
  fixedAmount: number
  /** 对应后端字段 percentage */
  percentage: number
  /** 对应后端字段 isTaxable */
  isTaxable: number
  /** 对应后端字段 isSocialSecurityBase */
  isSocialSecurityBase: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
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
 * SalaryComponentUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryComponentUpdateDto）
 */
export interface SalaryComponentUpdate extends SalaryComponentCreate {
  /** 对应后端字段 salaryComponentId */
  salaryComponentId: string
}

/**
 * SalaryComponentStatus类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryComponentStatusDto）
 */
export interface SalaryComponentStatus {
  /** 对应后端字段 salaryComponentId */
  salaryComponentId: string
  /** 对应后端字段 status */
  status: number
}

/**
 * SalaryComponentSort类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryComponentSortDto）
 */
export interface SalaryComponentSort {
  /** 对应后端字段 salaryComponentId */
  salaryComponentId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
