// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/controlling/standard-wage-rate
// 文件名称：standard-wage-rate.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：standard-wage-rate相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * StandardWageRate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktStandardWageRateDto）
 */
export interface StandardWageRate extends TaktEntityBase {
  /** 对应后端字段 standardWageRateId */
  standardWageRateId: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 yearMonth */
  yearMonth: string
  /** 对应后端字段 workingDays */
  workingDays: number
  /** 对应后端字段 salesAmount */
  salesAmount: number
  /** 对应后端字段 directLaborCount */
  directLaborCount: number
  /** 对应后端字段 directLaborWage */
  directLaborWage: number
  /** 对应后端字段 directOvertimeHours */
  directOvertimeHours: number
  /** 对应后端字段 directOvertimeTotal */
  directOvertimeTotal: number
  /** 对应后端字段 directWageRate */
  directWageRate: number
  /** 对应后端字段 indirectLaborCount */
  indirectLaborCount: number
  /** 对应后端字段 indirectLaborWage */
  indirectLaborWage: number
  /** 对应后端字段 indirectOvertimeHours */
  indirectOvertimeHours: number
  /** 对应后端字段 indirectOvertimeTotal */
  indirectOvertimeTotal: number
  /** 对应后端字段 indirectWageRate */
  indirectWageRate: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
}

/**
 * StandardWageRateQuery类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktStandardWageRateQueryDto）
 */
export interface StandardWageRateQuery extends TaktPagedQuery {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 yearMonth */
  yearMonth?: string
  /** 对应后端字段 workingDays */
  workingDays?: number
  /** 对应后端字段 salesAmount */
  salesAmount?: number
  /** 对应后端字段 directLaborCount */
  directLaborCount?: number
  /** 对应后端字段 directLaborWage */
  directLaborWage?: number
  /** 对应后端字段 directOvertimeHours */
  directOvertimeHours?: number
  /** 对应后端字段 directOvertimeTotal */
  directOvertimeTotal?: number
  /** 对应后端字段 directWageRate */
  directWageRate?: number
  /** 对应后端字段 indirectLaborCount */
  indirectLaborCount?: number
  /** 对应后端字段 indirectLaborWage */
  indirectLaborWage?: number
  /** 对应后端字段 indirectOvertimeHours */
  indirectOvertimeHours?: number
  /** 对应后端字段 indirectOvertimeTotal */
  indirectOvertimeTotal?: number
  /** 对应后端字段 indirectWageRate */
  indirectWageRate?: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
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
 * StandardWageRateCreate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktStandardWageRateCreateDto）
 */
export interface StandardWageRateCreate {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 yearMonth */
  yearMonth: string
  /** 对应后端字段 workingDays */
  workingDays: number
  /** 对应后端字段 salesAmount */
  salesAmount: number
  /** 对应后端字段 directLaborCount */
  directLaborCount: number
  /** 对应后端字段 directLaborWage */
  directLaborWage: number
  /** 对应后端字段 directOvertimeHours */
  directOvertimeHours: number
  /** 对应后端字段 directOvertimeTotal */
  directOvertimeTotal: number
  /** 对应后端字段 directWageRate */
  directWageRate: number
  /** 对应后端字段 indirectLaborCount */
  indirectLaborCount: number
  /** 对应后端字段 indirectLaborWage */
  indirectLaborWage: number
  /** 对应后端字段 indirectOvertimeHours */
  indirectOvertimeHours: number
  /** 对应后端字段 indirectOvertimeTotal */
  indirectOvertimeTotal: number
  /** 对应后端字段 indirectWageRate */
  indirectWageRate: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * StandardWageRateUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktStandardWageRateUpdateDto）
 */
export interface StandardWageRateUpdate extends StandardWageRateCreate {
  /** 对应后端字段 standardWageRateId */
  standardWageRateId: string
}
