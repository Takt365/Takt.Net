// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-punch
// 文件名称：attendance-punch.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：打卡记录相关类型定义，对应后端 TaktAttendancePunchDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 打卡记录类型（对应后端 TaktAttendancePunchDto；列表/详情）
 */
export interface AttendancePunch extends TaktEntityBase {
  /** 打卡记录 ID（对应后端 PunchId，序列化为 string） */
  punchId: string
  /** 员工 ID（对应后端 EmployeeId） */
  employeeId: string
  /** 打卡时间（对应后端 PunchTime） */
  punchTime: string
  /** 打卡类型（1=上班 2=下班 3=外勤，对应后端 PunchType） */
  punchType: number
  /** 打卡来源（0=后台录入 1=移动端 2=导入，对应后端 PunchSource） */
  punchSource: number
  /** 打卡地点或说明（对应后端 PunchAddress） */
  punchAddress?: string
}

/**
 * 打卡记录查询类型（对应后端 TaktAttendancePunchQueryDto）
 */
export interface AttendancePunchQuery extends TaktPagedQuery {
  /** 员工 ID（精确） */
  employeeId?: number
  /** 打卡类型（精确） */
  punchType?: number
  /** 打卡时间起（含，对应后端 PunchTimeFrom） */
  punchTimeFrom?: string
  /** 打卡时间止（含，对应后端 PunchTimeTo） */
  punchTimeTo?: string
}

/**
 * 创建打卡记录类型（对应后端 TaktAttendancePunchCreateDto）
 */
export interface AttendancePunchCreate {
  /** 员工 ID */
  employeeId: string
  /** 打卡时间 */
  punchTime: string
  /** 打卡类型 */
  punchType: number
  /** 打卡来源 */
  punchSource: number
  /** 打卡地点或说明 */
  punchAddress?: string
  /** 备注 */
  remark?: string
}

/**
 * 更新打卡记录类型（对应后端 TaktAttendancePunchUpdateDto）
 */
export interface AttendancePunchUpdate extends AttendancePunchCreate {
  /** 打卡记录 ID */
  punchId: string
}
