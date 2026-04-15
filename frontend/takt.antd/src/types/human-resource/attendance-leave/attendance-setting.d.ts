// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-setting
// 文件名称：attendance-setting.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设置（上下班方案）相关类型定义，对应后端 TaktAttendanceSettingDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 考勤设置类型（对应后端 TaktAttendanceSettingDto；列表/详情）
 */
export interface AttendanceSetting extends TaktEntityBase {
  /** 考勤设置 ID（对应后端 SettingId，序列化为 string） */
  settingId: string
  /** 方案编码（对应后端 SettingCode） */
  settingCode: string
  /** 方案名称（对应后端 SettingName） */
  settingName: string
  /** 标准上班时间 HH:mm（对应后端 WorkStartTime） */
  workStartTime: string
  /** 标准下班时间 HH:mm（对应后端 WorkEndTime） */
  workEndTime: string
  /** 迟到宽限分钟数（对应后端 LateGraceMinutes） */
  lateGraceMinutes: number
  /** 早退宽限分钟数（对应后端 EarlyLeaveGraceMinutes） */
  earlyLeaveGraceMinutes: number
  /** 是否默认方案（0=否 1=是，对应后端 IsDefault） */
  isDefault: number
  /** 排序号（对应后端 OrderNum） */
  orderNum: number
}

/**
 * 考勤设置查询类型（对应后端 TaktAttendanceSettingQueryDto）
 */
export interface AttendanceSettingQuery extends TaktPagedQuery {
  /** 方案编码（模糊） */
  settingCode?: string
  /** 方案名称（模糊） */
  settingName?: string
}

/**
 * 创建考勤设置类型（对应后端 TaktAttendanceSettingCreateDto）
 */
export interface AttendanceSettingCreate {
  /** 方案编码 */
  settingCode: string
  /** 方案名称 */
  settingName: string
  /** 标准上班时间 HH:mm */
  workStartTime: string
  /** 标准下班时间 HH:mm */
  workEndTime: string
  /** 迟到宽限分钟数 */
  lateGraceMinutes: number
  /** 早退宽限分钟数 */
  earlyLeaveGraceMinutes: number
  /** 是否默认方案 */
  isDefault: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remark?: string
}

/**
 * 更新考勤设置类型（对应后端 TaktAttendanceSettingUpdateDto）
 */
export interface AttendanceSettingUpdate extends AttendanceSettingCreate {
  /** 考勤设置 ID */
  settingId: string
}
