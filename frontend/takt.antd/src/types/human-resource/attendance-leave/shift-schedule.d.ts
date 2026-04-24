// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/shift-schedule
// 文件名称：shift-schedule.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：shift-schedule相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ShiftSchedule类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktShiftScheduleDto）
 */
export interface ShiftSchedule extends TaktEntityBase {
  /** 对应后端字段 shiftScheduleId */
  shiftScheduleId: string
  /** 对应后端字段 scheduleType */
  scheduleType: number
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 scheduleDate */
  scheduleDate: string
  /** 对应后端字段 shiftId */
  shiftId: string
  /** 对应后端字段 shiftName */
  shiftName?: string
}

/**
 * ShiftScheduleQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktShiftScheduleQueryDto）
 */
export interface ShiftScheduleQuery extends TaktPagedQuery {
  /** 对应后端字段 scheduleType */
  scheduleType?: number
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 shiftId */
  shiftId?: string
  /** 对应后端字段 scheduleDateFrom */
  scheduleDateFrom?: string
  /** 对应后端字段 scheduleDateTo */
  scheduleDateTo?: string
}

/**
 * ShiftScheduleCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktShiftScheduleCreateDto）
 */
export interface ShiftScheduleCreate {
  /** 对应后端字段 scheduleType */
  scheduleType: number
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 scheduleDate */
  scheduleDate: string
  /** 对应后端字段 shiftId */
  shiftId: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ShiftScheduleUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktShiftScheduleUpdateDto）
 */
export interface ShiftScheduleUpdate extends ShiftScheduleCreate {
  /** 对应后端字段 shiftScheduleId */
  shiftScheduleId: string
}
