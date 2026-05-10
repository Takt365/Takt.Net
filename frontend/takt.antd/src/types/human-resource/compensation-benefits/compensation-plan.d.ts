// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/compensation-benefits/compensation-plan
// 文件名称：compensation-plan.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：compensation-plan相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CompensationPlan类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktCompensationPlanDto）
 */
export interface CompensationPlan extends TaktEntityBase {
  /** 对应后端字段 compensationPlanId */
  compensationPlanId: string
  /** 对应后端字段 planCode */
  planCode: string
  /** 对应后端字段 planName */
  planName: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicablePosition */
  applicablePosition: string
  /** 对应后端字段 applicableLevel */
  applicableLevel: string
  /** 对应后端字段 salaryStructureId */
  salaryStructureId: string
  /** 对应后端字段 baseSalaryRatio */
  baseSalaryRatio: number
  /** 对应后端字段 performanceSalaryRatio */
  performanceSalaryRatio: number
  /** 对应后端字段 allowanceRatio */
  allowanceRatio: number
  /** 对应后端字段 annualAdjustmentRatio */
  annualAdjustmentRatio: number
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 status */
  status: number
}

/**
 * CompensationPlanQuery类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktCompensationPlanQueryDto）
 */
export interface CompensationPlanQuery extends TaktPagedQuery {
  /** 对应后端字段 planCode */
  planCode?: string
  /** 对应后端字段 planName */
  planName?: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment?: string
  /** 对应后端字段 applicablePosition */
  applicablePosition?: string
  /** 对应后端字段 applicableLevel */
  applicableLevel?: string
  /** 对应后端字段 salaryStructureId */
  salaryStructureId?: string
  /** 对应后端字段 baseSalaryRatio */
  baseSalaryRatio?: number
  /** 对应后端字段 performanceSalaryRatio */
  performanceSalaryRatio?: number
  /** 对应后端字段 allowanceRatio */
  allowanceRatio?: number
  /** 对应后端字段 annualAdjustmentRatio */
  annualAdjustmentRatio?: number
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
 * CompensationPlanCreate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktCompensationPlanCreateDto）
 */
export interface CompensationPlanCreate {
  /** 对应后端字段 planCode */
  planCode: string
  /** 对应后端字段 planName */
  planName: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicablePosition */
  applicablePosition: string
  /** 对应后端字段 applicableLevel */
  applicableLevel: string
  /** 对应后端字段 salaryStructureId */
  salaryStructureId: string
  /** 对应后端字段 baseSalaryRatio */
  baseSalaryRatio: number
  /** 对应后端字段 performanceSalaryRatio */
  performanceSalaryRatio: number
  /** 对应后端字段 allowanceRatio */
  allowanceRatio: number
  /** 对应后端字段 annualAdjustmentRatio */
  annualAdjustmentRatio: number
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
 * CompensationPlanUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktCompensationPlanUpdateDto）
 */
export interface CompensationPlanUpdate extends CompensationPlanCreate {
  /** 对应后端字段 compensationPlanId */
  compensationPlanId: string
}

/**
 * CompensationPlanStatus类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktCompensationPlanStatusDto）
 */
export interface CompensationPlanStatus {
  /** 对应后端字段 compensationPlanId */
  compensationPlanId: string
  /** 对应后端字段 status */
  status: number
}
