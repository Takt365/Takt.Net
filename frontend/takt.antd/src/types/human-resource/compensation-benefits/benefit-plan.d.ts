// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/compensation-benefits/benefit-plan
// 文件名称：benefit-plan.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：benefit-plan相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * BenefitPlan类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktBenefitPlanDto）
 */
export interface BenefitPlan extends TaktEntityBase {
  /** 对应后端字段 benefitPlanId */
  benefitPlanId: string
  /** 对应后端字段 planCode */
  planCode: string
  /** 对应后端字段 planName */
  planName: string
  /** 对应后端字段 benefitType */
  benefitType: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicablePosition */
  applicablePosition: string
  /** 对应后端字段 applicableLevel */
  applicableLevel: string
  /** 对应后端字段 benefitStandardAmount */
  benefitStandardAmount: number
  /** 对应后端字段 distributionMethod */
  distributionMethod: string
  /** 对应后端字段 benefitConditions */
  benefitConditions: string
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 status */
  status: number
}

/**
 * BenefitPlanQuery类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktBenefitPlanQueryDto）
 */
export interface BenefitPlanQuery extends TaktPagedQuery {
  /** 对应后端字段 planCode */
  planCode?: string
  /** 对应后端字段 planName */
  planName?: string
  /** 对应后端字段 benefitType */
  benefitType?: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment?: string
  /** 对应后端字段 applicablePosition */
  applicablePosition?: string
  /** 对应后端字段 applicableLevel */
  applicableLevel?: string
  /** 对应后端字段 benefitStandardAmount */
  benefitStandardAmount?: number
  /** 对应后端字段 distributionMethod */
  distributionMethod?: string
  /** 对应后端字段 benefitConditions */
  benefitConditions?: string
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
 * BenefitPlanCreate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktBenefitPlanCreateDto）
 */
export interface BenefitPlanCreate {
  /** 对应后端字段 planCode */
  planCode: string
  /** 对应后端字段 planName */
  planName: string
  /** 对应后端字段 benefitType */
  benefitType: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicablePosition */
  applicablePosition: string
  /** 对应后端字段 applicableLevel */
  applicableLevel: string
  /** 对应后端字段 benefitStandardAmount */
  benefitStandardAmount: number
  /** 对应后端字段 distributionMethod */
  distributionMethod: string
  /** 对应后端字段 benefitConditions */
  benefitConditions: string
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
 * BenefitPlanUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktBenefitPlanUpdateDto）
 */
export interface BenefitPlanUpdate extends BenefitPlanCreate {
  /** 对应后端字段 benefitPlanId */
  benefitPlanId: string
}

/**
 * BenefitPlanStatus类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktBenefitPlanStatusDto）
 */
export interface BenefitPlanStatus {
  /** 对应后端字段 benefitPlanId */
  benefitPlanId: string
  /** 对应后端字段 status */
  status: number
}
