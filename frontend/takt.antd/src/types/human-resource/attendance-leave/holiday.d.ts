// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/humanresource/attendance-leave/holiday
// 文件名称：holiday.d.ts
// 功能描述：假日相关类型定义，对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktHolidayDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 假日类型（对应后端 TaktHolidayDto）
 */
export interface Holiday extends TaktEntityBase {
  /** 假日ID（对应后端 HolidayId，序列化为string以避免Javascript精度问题） */
  holidayId: string
  /** 地区（Region，如 CN、US、TW、HK） */
  region: string
  /** 假日名称 */
  holidayName: string
  /** 假日类型（0=法定 1=调休 2=公司） */
  holidayType: number
  /** 假日开始日期 */
  startDate: string
  /** 假日结束日期 */
  endDate: string
  /** 是否工作日（0=非工作日 1=工作日 2=半天等） */
  isWorkingDay: number
  /** 假日问候语（简短，用于问候语行） */
  holidayGreeting?: string
  /** 假日引用/诗句（用于引用区展示） */
  holidayQuote?: string
  /** 假日主题（对应前端 themeColorMap 的 key） */
  holidayTheme: string
}

/**
 * 假日查询类型（对应后端 TaktHolidayQueryDto）
 */
export interface HolidayQuery extends TaktPagedQuery {
  /** 关键词（在假日名称中模糊查询） */
  keyWords?: string
  /** 地区 */
  region?: string
  /** 假日名称 */
  holidayName?: string
}

/**
 * 创建假日类型（对应后端 TaktHolidayCreateDto）
 */
export interface HolidayCreate {
  /** 地区（Region，如 CN、US、TW、HK） */
  region: string
  /** 假日名称 */
  holidayName: string
  /** 假日类型（0=法定 1=调休 2=公司） */
  holidayType: number
  /** 假日开始日期 */
  startDate: string
  /** 假日结束日期 */
  endDate: string
  /** 是否工作日（0=非工作日 1=工作日 2=半天等） */
  isWorkingDay: number
  /** 假日问候语（简短，用于问候语行） */
  holidayGreeting?: string
  /** 假日引用/诗句（用于引用区展示） */
  holidayQuote?: string
  /** 假日主题（对应前端 themeColorMap 的 key） */
  holidayTheme: string
  /** 备注 */
  remark?: string
}

/**
 * 更新假日类型（对应后端 TaktHolidayUpdateDto）
 */
export interface HolidayUpdate extends HolidayCreate {
  /** 假日ID（对应后端 HolidayId，序列化为string以避免Javascript精度问题） */
  holidayId: string
}

/**
 * 假日导入模板类型（对应后端 TaktHolidayTemplateDto，用于生成 Excel 表头/模板）
 */
export interface HolidayTemplate {
  /** 地区（Region，如 CN、US、TW、HK） */
  region: string
  /** 假日名称 */
  holidayName: string
  /** 假日类型（0=法定 1=调休 2=公司） */
  holidayType: number
  /** 假日开始日期 */
  startDate: string
  /** 假日结束日期 */
  endDate: string
  /** 是否工作日（0=非工作日 1=工作日 2=半天等） */
  isWorkingDay: number
  /** 假日问候语（简短，用于问候语行） */
  holidayGreeting?: string
  /** 假日引用/诗句（用于引用区展示） */
  holidayQuote?: string
  /** 假日主题（对应前端 themeColorMap 的 key） */
  holidayTheme: string
  /** 备注 */
  remark?: string
}

/**
 * 假日导入类型（对应后端 TaktHolidayImportDto，Excel 导入行映射）
 */
export interface HolidayImport {
  /** 地区（Region，如 CN、US、TW、HK） */
  region: string
  /** 假日名称 */
  holidayName: string
  /** 假日类型（0=法定 1=调休 2=公司） */
  holidayType: number
  /** 假日开始日期 */
  startDate: string
  /** 假日结束日期 */
  endDate: string
  /** 是否工作日（0=非工作日 1=工作日 2=半天等） */
  isWorkingDay: number
  /** 假日问候语（简短，用于问候语行） */
  holidayGreeting?: string
  /** 假日引用/诗句（用于引用区展示） */
  holidayQuote?: string
  /** 假日主题（对应前端 themeColorMap 的 key） */
  holidayTheme: string
  /** 备注 */
  remark?: string
}

/**
 * 假日导出类型（对应后端 TaktHolidayExportDto，Excel 导出行映射）
 */
export interface HolidayExport {
  /** 地区 */
  region: string
  /** 假日名称 */
  holidayName: string
  /** 假日类型（0=法定 1=调休 2=公司） */
  holidayType: number
  /** 假日开始日期 */
  startDate: string
  /** 假日结束日期 */
  endDate: string
  /** 是否工作日（0=非工作日 1=工作日 2=半天等） */
  isWorkingDay: number
  /** 假日问候语（简短，用于问候语行） */
  holidayGreeting?: string
  /** 假日引用/诗句（用于引用区展示） */
  holidayQuote?: string
  /** 假日主题 */
  holidayTheme: string
  /** 备注 */
  remark?: string
  /** 创建时间（ISO 字符串） */
  createTime: string
}