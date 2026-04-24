// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/overtime
// 文件名称：overtime.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：overtime相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Overtime类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeDto）
 */
export interface Overtime extends TaktEntityBase {
  /** 对应后端字段 overtimeId */
  overtimeId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 overtimeDate */
  overtimeDate: string
  /** 对应后端字段 plannedHours */
  plannedHours: number
  /** 对应后端字段 actualHours */
  actualHours?: number
  /** 对应后端字段 reason */
  reason: string
  /** 对应后端字段 overtimeStatus */
  overtimeStatus: number
}

/**
 * OvertimeQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeQueryDto）
 */
export interface OvertimeQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 overtimeStatus */
  overtimeStatus?: number
  /** 对应后端字段 overtimeDateFrom */
  overtimeDateFrom?: string
  /** 对应后端字段 overtimeDateTo */
  overtimeDateTo?: string
}

/**
 * OvertimeCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeCreateDto）
 */
export interface OvertimeCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 overtimeDate */
  overtimeDate: string
  /** 对应后端字段 plannedHours */
  plannedHours: number
  /** 对应后端字段 actualHours */
  actualHours?: number
  /** 对应后端字段 reason */
  reason: string
  /** 对应后端字段 overtimeStatus */
  overtimeStatus: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * OvertimeUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeUpdateDto）
 */
export interface OvertimeUpdate extends OvertimeCreate {
  /** 对应后端字段 overtimeId */
  overtimeId: string
}
