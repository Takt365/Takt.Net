// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/performance/performance-plan
// 文件名称：performance-plan.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：performance-plan相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PerformancePlan类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformancePlanDto）
 */
export interface PerformancePlan extends TaktEntityBase {
  /** 对应后端字段 performancePlanId */
  performancePlanId: string
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
  /** 对应后端字段 cycleType */
  cycleType: string
  /** 对应后端字段 scoringStandard */
  scoringStandard: string
  /** 对应后端字段 selfEvaluationWeight */
  selfEvaluationWeight: number
  /** 对应后端字段 supervisorWeight */
  supervisorWeight: number
  /** 对应后端字段 peerWeight */
  peerWeight: number
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 status */
  status: number
}

/**
 * PerformancePlanQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformancePlanQueryDto）
 */
export interface PerformancePlanQuery extends TaktPagedQuery {
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
  /** 对应后端字段 cycleType */
  cycleType?: string
  /** 对应后端字段 scoringStandard */
  scoringStandard?: string
  /** 对应后端字段 selfEvaluationWeight */
  selfEvaluationWeight?: number
  /** 对应后端字段 supervisorWeight */
  supervisorWeight?: number
  /** 对应后端字段 peerWeight */
  peerWeight?: number
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
 * PerformancePlanCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformancePlanCreateDto）
 */
export interface PerformancePlanCreate {
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
  /** 对应后端字段 cycleType */
  cycleType: string
  /** 对应后端字段 scoringStandard */
  scoringStandard: string
  /** 对应后端字段 selfEvaluationWeight */
  selfEvaluationWeight: number
  /** 对应后端字段 supervisorWeight */
  supervisorWeight: number
  /** 对应后端字段 peerWeight */
  peerWeight: number
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
 * PerformancePlanUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformancePlanUpdateDto）
 */
export interface PerformancePlanUpdate extends PerformancePlanCreate {
  /** 对应后端字段 performancePlanId */
  performancePlanId: string
}

/**
 * PerformancePlanStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformancePlanStatusDto）
 */
export interface PerformancePlanStatus {
  /** 对应后端字段 performancePlanId */
  performancePlanId: string
  /** 对应后端字段 status */
  status: number
}
