// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-setting
// 文件名称：attendance-setting.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：attendance-setting相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AttendanceSetting类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceSettingDto）
 */
export interface AttendanceSetting extends TaktEntityBase {
  /** 对应后端字段 settingId */
  settingId: string
  /** 对应后端字段 settingCode */
  settingCode: string
  /** 对应后端字段 settingName */
  settingName: string
  /** 对应后端字段 workStartTime */
  workStartTime: string
  /** 对应后端字段 workEndTime */
  workEndTime: string
  /** 对应后端字段 lateGraceMinutes */
  lateGraceMinutes: number
  /** 对应后端字段 earlyLeaveGraceMinutes */
  earlyLeaveGraceMinutes: number
  /** 对应后端字段 isDefault */
  isDefault: number
  /** 对应后端字段 orderNum */
  orderNum: number
}

/**
 * AttendanceSettingQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceSettingQueryDto）
 */
export interface AttendanceSettingQuery extends TaktPagedQuery {
  /** 对应后端字段 settingCode */
  settingCode?: string
  /** 对应后端字段 settingName */
  settingName?: string
}

/**
 * AttendanceSettingCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceSettingCreateDto）
 */
export interface AttendanceSettingCreate {
  /** 对应后端字段 settingCode */
  settingCode: string
  /** 对应后端字段 settingName */
  settingName: string
  /** 对应后端字段 workStartTime */
  workStartTime: string
  /** 对应后端字段 workEndTime */
  workEndTime: string
  /** 对应后端字段 lateGraceMinutes */
  lateGraceMinutes: number
  /** 对应后端字段 earlyLeaveGraceMinutes */
  earlyLeaveGraceMinutes: number
  /** 对应后端字段 isDefault */
  isDefault: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AttendanceSettingUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceSettingUpdateDto）
 */
export interface AttendanceSettingUpdate extends AttendanceSettingCreate {
  /** 对应后端字段 settingId */
  settingId: string
}
