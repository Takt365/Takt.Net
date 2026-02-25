// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/controlling/wage-rate
// 文件名称：wage-rate.d.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：工资率相关类型定义，对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktWageRateDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktPagedQuery } from '@/types/common'

/**
 * 工资率类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktWageRateDto）
 */
export interface WageRate {
  /** 工资率ID（对应后端 WageRateId，序列化为string以避免Javascript精度问题） */
  wageRateId: string
  /** 工厂代码 */
  plantCode: string
  /** 年月（格式yyyyMM） */
  yearMonth: string
  /** 工资率类别（0=标准，1=预算，2=实际） */
  wageRateType: number
  /** 工作天数 */
  workingDays: number
  /** 销售额 */
  salesAmount: number
  /** 直接人数 */
  directLaborCount: number
  /** 直接工资 */
  directLaborWage: number
  /** 直接加班小时 */
  directOvertimeHours: number
  /** 直接加班总额 */
  directOvertimeTotal: number
  /** 直接工资率（元/小时） */
  directWageRate: number
  /** 间接人数 */
  indirectLaborCount?: number
  /** 间接工资 */
  indirectLaborWage?: number
  /** 间接工资率（元/小时） */
  indirectWageRate?: number
  /** 创建人（用户名） */
  createBy?: string
  /** 创建时间 */
  createTime?: string
  /** 更新人（用户名） */
  updateBy?: string
  /** 更新时间 */
  updateTime?: string
}

/**
 * 工资率查询类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktWageRateQueryDto）
 */
export interface WageRateQuery extends TaktPagedQuery {
  /** 工厂代码 */
  plantCode?: string
  /** 年月（格式yyyyMM） */
  yearMonth?: string
  /** 工资率类别（0=标准，1=预算，2=实际） */
  wageRateType?: number
}

/**
 * 创建工资率类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktWageRateCreateDto）
 */
export interface WageRateCreate {
  /** 工厂代码 */
  plantCode: string
  /** 年月（格式yyyyMM） */
  yearMonth: string
  /** 工资率类别（0=标准，1=预算，2=实际） */
  wageRateType: number
  /** 工作天数 */
  workingDays?: number
  /** 销售额 */
  salesAmount?: number
  /** 直接人数 */
  directLaborCount?: number
  /** 直接工资 */
  directLaborWage?: number
  /** 直接加班小时 */
  directOvertimeHours?: number
  /** 直接加班总额 */
  directOvertimeTotal?: number
  /** 直接工资率（元/小时） */
  directWageRate?: number
  /** 间接人数 */
  indirectLaborCount?: number
  /** 间接工资 */
  indirectLaborWage?: number
  /** 间接工资率（元/小时） */
  indirectWageRate?: number
}

/**
 * 更新工资率类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktWageRateUpdateDto）
 */
export interface WageRateUpdate extends WageRateCreate {
  /** 工资率ID（序列化为string） */
  wageRateId: string
}
