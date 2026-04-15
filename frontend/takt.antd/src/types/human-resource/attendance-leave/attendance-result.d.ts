// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-result
// 文件名称：attendance-result.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤日结结果相关类型定义，对应后端 TaktAttendanceResultDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 考勤日结结果类型（对应后端 TaktAttendanceResultDto；列表/详情）
 */
export interface AttendanceResult extends TaktEntityBase {
  /** 结果记录 ID（对应后端 ResultId，序列化为 string） */
  resultId: string
  /** 员工 ID（对应后端 EmployeeId） */
  employeeId: string
  /** 归属考勤日期（日期部分有效，对应后端 AttendanceDate） */
  attendanceDate: string
  /** 排班 ID（可选，对应后端 ShiftScheduleId） */
  shiftScheduleId?: string
  /** 出勤状态（0=正常 1=迟到 2=早退 3=缺卡 4=旷工 5=加班，对应后端 AttendanceStatus） */
  attendanceStatus: number
  /** 首次上班时间（对应后端 FirstInTime） */
  firstInTime?: string
  /** 末次下班时间（对应后端 LastOutTime） */
  lastOutTime?: string
  /** 计薪/计出勤分钟数（对应后端 WorkMinutes） */
  workMinutes: number
  /** 结果计算完成时间（对应后端 CalculatedAt） */
  calculatedAt?: string
}

/**
 * 考勤结果查询类型（对应后端 TaktAttendanceResultQueryDto）
 */
export interface AttendanceResultQuery extends TaktPagedQuery {
  /** 员工 ID（精确） */
  employeeId?: number
  /** 考勤日期起（含，对应后端 AttendanceDateFrom） */
  attendanceDateFrom?: string
  /** 考勤日期止（含，对应后端 AttendanceDateTo） */
  attendanceDateTo?: string
  /** 出勤状态（精确） */
  attendanceStatus?: number
}

/**
 * 创建考勤日结结果类型（对应后端 TaktAttendanceResultCreateDto）
 */
export interface AttendanceResultCreate {
  /** 员工 ID */
  employeeId: string
  /** 归属考勤日期 */
  attendanceDate: string
  /** 排班 ID */
  shiftScheduleId?: string
  /** 出勤状态 */
  attendanceStatus: number
  /** 首次上班时间 */
  firstInTime?: string
  /** 末次下班时间 */
  lastOutTime?: string
  /** 计薪/计出勤分钟数 */
  workMinutes: number
  /** 结果计算完成时间 */
  calculatedAt?: string
  /** 备注 */
  remark?: string
}

/**
 * 更新考勤日结结果类型（对应后端 TaktAttendanceResultUpdateDto）
 */
export interface AttendanceResultUpdate extends AttendanceResultCreate {
  /** 结果记录 ID */
  resultId: string
}
