// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-exception
// 文件名称：attendance-exception.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤异常相关类型定义，对应后端 TaktAttendanceExceptionDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 考勤异常类型（对应后端 TaktAttendanceExceptionDto；列表/详情）
 */
export interface AttendanceException extends TaktEntityBase {
  /** 异常记录 ID（对应后端 ExceptionId，序列化为 string） */
  exceptionId: string
  /** 员工 ID（对应后端 EmployeeId） */
  employeeId: string
  /** 异常归属日期（日期部分有效，对应后端 ExceptionDate） */
  exceptionDate: string
  /** 异常类型（1=上班缺卡 2=下班缺卡 3=迟到 4=早退 5=旷工 9=其他，对应后端 ExceptionType） */
  exceptionType: number
  /** 说明（对应后端 Summary） */
  summary: string
  /** 处理状态（0=待处理 1=已处理 2=已忽略，对应后端 HandleStatus） */
  handleStatus: number
}

/**
 * 考勤异常查询类型（对应后端 TaktAttendanceExceptionQueryDto；KeyWords 在后端用于匹配说明、备注）
 */
export interface AttendanceExceptionQuery extends TaktPagedQuery {
  /** 员工 ID（精确） */
  employeeId?: number
  /** 异常日期起（含，对应后端 ExceptionDateFrom） */
  exceptionDateFrom?: string
  /** 异常日期止（含，对应后端 ExceptionDateTo） */
  exceptionDateTo?: string
  /** 异常类型（精确） */
  exceptionType?: number
  /** 处理状态（精确） */
  handleStatus?: number
}

/**
 * 创建考勤异常类型（对应后端 TaktAttendanceExceptionCreateDto）
 */
export interface AttendanceExceptionCreate {
  /** 员工 ID */
  employeeId: string
  /** 异常归属日期 */
  exceptionDate: string
  /** 异常类型 */
  exceptionType: number
  /** 说明 */
  summary: string
  /** 处理状态 */
  handleStatus: number
  /** 备注 */
  remark?: string
}

/**
 * 更新考勤异常类型（对应后端 TaktAttendanceExceptionUpdateDto）
 */
export interface AttendanceExceptionUpdate extends AttendanceExceptionCreate {
  /** 异常记录 ID */
  exceptionId: string
}
