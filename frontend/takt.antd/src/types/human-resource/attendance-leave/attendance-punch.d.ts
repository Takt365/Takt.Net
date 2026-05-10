// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-punch
// 文件名称：attendance-punch.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：attendance-punch相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AttendancePunch类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendancePunchDto）
 */
export interface AttendancePunch extends TaktEntityBase {
  /** 对应后端字段 attendancePunchId */
  attendancePunchId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 punchTime */
  punchTime: string
  /** 对应后端字段 punchType */
  punchType: number
  /** 对应后端字段 punchSource */
  punchSource: number
  /** 对应后端字段 punchAddress */
  punchAddress?: string
}

/**
 * AttendancePunchQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendancePunchQueryDto）
 */
export interface AttendancePunchQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 punchTime */
  punchTime?: string
  /** 对应后端字段 punchTimeStart */
  punchTimeStart?: string
  /** 对应后端字段 punchTimeEnd */
  punchTimeEnd?: string
  /** 对应后端字段 punchType */
  punchType?: number
  /** 对应后端字段 punchSource */
  punchSource?: number
  /** 对应后端字段 punchAddress */
  punchAddress?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 createdById */
  createdById?: string
  /** 对应后端字段 createdBy */
  createdBy?: string
  /** 对应后端字段 createdAt */
  createdAt?: string
  /** 对应后端字段 createdAtStart */
  createdAtStart?: string
  /** 对应后端字段 createdAtEnd */
  createdAtEnd?: string
}

/**
 * AttendancePunchCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendancePunchCreateDto）
 */
export interface AttendancePunchCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 punchTime */
  punchTime: string
  /** 对应后端字段 punchType */
  punchType: number
  /** 对应后端字段 punchSource */
  punchSource: number
  /** 对应后端字段 punchAddress */
  punchAddress?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AttendancePunchUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendancePunchUpdateDto）
 */
export interface AttendancePunchUpdate extends AttendancePunchCreate {
  /** 对应后端字段 attendancePunchId */
  attendancePunchId: string
}
