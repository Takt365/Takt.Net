// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/work-shift
// 文件名称：work-shift.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：work-shift相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * WorkShift类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktWorkShiftDto）
 */
export interface WorkShift extends TaktEntityBase {
  /** 对应后端字段 shiftId */
  shiftId: string
  /** 对应后端字段 shiftCode */
  shiftCode: string
  /** 对应后端字段 shiftName */
  shiftName: string
  /** 对应后端字段 startTime */
  startTime: string
  /** 对应后端字段 endTime */
  endTime: string
  /** 对应后端字段 crossMidnight */
  crossMidnight: number
  /** 对应后端字段 orderNum */
  orderNum: number
}

/**
 * WorkShiftQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktWorkShiftQueryDto）
 */
export interface WorkShiftQuery extends TaktPagedQuery {
  /** 对应后端字段 shiftCode */
  shiftCode?: string
  /** 对应后端字段 shiftName */
  shiftName?: string
}

/**
 * WorkShiftCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktWorkShiftCreateDto）
 */
export interface WorkShiftCreate {
  /** 对应后端字段 shiftCode */
  shiftCode: string
  /** 对应后端字段 shiftName */
  shiftName: string
  /** 对应后端字段 startTime */
  startTime: string
  /** 对应后端字段 endTime */
  endTime: string
  /** 对应后端字段 crossMidnight */
  crossMidnight: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * WorkShiftUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktWorkShiftUpdateDto）
 */
export interface WorkShiftUpdate extends WorkShiftCreate {
  /** 对应后端字段 shiftId */
  shiftId: string
}
