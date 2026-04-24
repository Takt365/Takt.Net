// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/holiday
// 文件名称：holiday.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：holiday相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Holiday类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktHolidayDto）
 */
export interface Holiday extends TaktEntityBase {
  /** 对应后端字段 holidayId */
  holidayId: string
  /** 对应后端字段 region */
  region: string
  /** 对应后端字段 holidayName */
  holidayName: string
  /** 对应后端字段 holidayType */
  holidayType: number
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 isWorkingDay */
  isWorkingDay: number
  /** 对应后端字段 holidayGreeting */
  holidayGreeting?: string
  /** 对应后端字段 holidayQuote */
  holidayQuote?: string
  /** 对应后端字段 holidayTheme */
  holidayTheme: string
}

/**
 * HolidayQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktHolidayQueryDto）
 */
export interface HolidayQuery extends TaktPagedQuery {
  /** 对应后端字段 region */
  region?: string
  /** 对应后端字段 holidayName */
  holidayName?: string
}

/**
 * HolidayCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktHolidayCreateDto）
 */
export interface HolidayCreate {
  /** 对应后端字段 region */
  region: string
  /** 对应后端字段 holidayName */
  holidayName: string
  /** 对应后端字段 holidayType */
  holidayType: number
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 isWorkingDay */
  isWorkingDay: number
  /** 对应后端字段 holidayGreeting */
  holidayGreeting?: string
  /** 对应后端字段 holidayQuote */
  holidayQuote?: string
  /** 对应后端字段 holidayTheme */
  holidayTheme: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * HolidayUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktHolidayUpdateDto）
 */
export interface HolidayUpdate extends HolidayCreate {
  /** 对应后端字段 holidayId */
  holidayId: string
}
