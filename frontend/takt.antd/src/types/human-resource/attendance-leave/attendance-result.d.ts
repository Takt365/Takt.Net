// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-result
// 文件名称：attendance-result.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：attendance-result相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AttendanceResult类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceResultDto）
 */
export interface AttendanceResult extends TaktEntityBase {
  /** 对应后端字段 resultId */
  resultId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 attendanceDate */
  attendanceDate: string
  /** 对应后端字段 shiftScheduleId */
  shiftScheduleId?: string
  /** 对应后端字段 attendanceStatus */
  attendanceStatus: number
  /** 对应后端字段 firstInTime */
  firstInTime?: string
  /** 对应后端字段 lastOutTime */
  lastOutTime?: string
  /** 对应后端字段 workMinutes */
  workMinutes: number
  /** 对应后端字段 calculatedAt */
  calculatedAt?: string
}

/**
 * AttendanceResultQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceResultQueryDto）
 */
export interface AttendanceResultQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 attendanceDateFrom */
  attendanceDateFrom?: string
  /** 对应后端字段 attendanceDateTo */
  attendanceDateTo?: string
  /** 对应后端字段 attendanceStatus */
  attendanceStatus?: number
}

/**
 * AttendanceResultCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceResultCreateDto）
 */
export interface AttendanceResultCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 attendanceDate */
  attendanceDate: string
  /** 对应后端字段 shiftScheduleId */
  shiftScheduleId?: string
  /** 对应后端字段 attendanceStatus */
  attendanceStatus: number
  /** 对应后端字段 firstInTime */
  firstInTime?: string
  /** 对应后端字段 lastOutTime */
  lastOutTime?: string
  /** 对应后端字段 workMinutes */
  workMinutes: number
  /** 对应后端字段 calculatedAt */
  calculatedAt?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AttendanceResultUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceResultUpdateDto）
 */
export interface AttendanceResultUpdate extends AttendanceResultCreate {
  /** 对应后端字段 resultId */
  resultId: string
}
