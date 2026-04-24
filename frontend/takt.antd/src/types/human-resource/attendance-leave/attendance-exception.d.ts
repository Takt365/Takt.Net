// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-exception
// 文件名称：attendance-exception.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：attendance-exception相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AttendanceException类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceExceptionDto）
 */
export interface AttendanceException extends TaktEntityBase {
  /** 对应后端字段 exceptionId */
  exceptionId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 exceptionDate */
  exceptionDate: string
  /** 对应后端字段 exceptionType */
  exceptionType: number
  /** 对应后端字段 summary */
  summary: string
  /** 对应后端字段 handleStatus */
  handleStatus: number
}

/**
 * AttendanceExceptionQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceExceptionQueryDto）
 */
export interface AttendanceExceptionQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 exceptionDateFrom */
  exceptionDateFrom?: string
  /** 对应后端字段 exceptionDateTo */
  exceptionDateTo?: string
  /** 对应后端字段 exceptionType */
  exceptionType?: number
  /** 对应后端字段 handleStatus */
  handleStatus?: number
}

/**
 * AttendanceExceptionCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceExceptionCreateDto）
 */
export interface AttendanceExceptionCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 exceptionDate */
  exceptionDate: string
  /** 对应后端字段 exceptionType */
  exceptionType: number
  /** 对应后端字段 summary */
  summary: string
  /** 对应后端字段 handleStatus */
  handleStatus: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AttendanceExceptionUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceExceptionUpdateDto）
 */
export interface AttendanceExceptionUpdate extends AttendanceExceptionCreate {
  /** 对应后端字段 exceptionId */
  exceptionId: string
}
