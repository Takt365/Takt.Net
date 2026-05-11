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
  /** 对应后端字段 applicantId */
  applicantId?: string
  /** 对应后端字段 applicantBy */
  applicantBy?: string
  /** 对应后端字段 applicationDate */
  applicationDate: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 overtimeDate */
  overtimeDate: string
  /** 对应后端字段 plannedStartTime */
  plannedStartTime: string
  /** 对应后端字段 plannedEndTime */
  plannedEndTime: string
  /** 对应后端字段 totalEmployees */
  totalEmployees: number
  /** 对应后端字段 totalPlannedHours */
  totalPlannedHours: number
  /** 对应后端字段 totalActualHours */
  totalActualHours: number
  /** 对应后端字段 overtimeType */
  overtimeType: number
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 approverId */
  approverId?: string
  /** 对应后端字段 approverBy */
  approverBy?: string
  /** 对应后端字段 approveTime */
  approveTime?: string
  /** 对应后端字段 approveComment */
  approveComment?: string
  /** 对应后端字段 handlingId */
  handlingId?: string
  /** 对应后端字段 handlingBy */
  handlingBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 handlingComment */
  handlingComment?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 overtimeStatus */
  overtimeStatus: number
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * OvertimeQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeQueryDto）
 */
export interface OvertimeQuery extends TaktPagedQuery {
  /** 对应后端字段 applicantId */
  applicantId?: string
  /** 对应后端字段 applicantBy */
  applicantBy?: string
  /** 对应后端字段 applicationDate */
  applicationDate?: string
  /** 对应后端字段 applicationDateStart */
  applicationDateStart?: string
  /** 对应后端字段 applicationDateEnd */
  applicationDateEnd?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 overtimeDate */
  overtimeDate?: string
  /** 对应后端字段 overtimeDateStart */
  overtimeDateStart?: string
  /** 对应后端字段 overtimeDateEnd */
  overtimeDateEnd?: string
  /** 对应后端字段 plannedStartTime */
  plannedStartTime?: string
  /** 对应后端字段 plannedStartTimeStart */
  plannedStartTimeStart?: string
  /** 对应后端字段 plannedStartTimeEnd */
  plannedStartTimeEnd?: string
  /** 对应后端字段 plannedEndTime */
  plannedEndTime?: string
  /** 对应后端字段 plannedEndTimeStart */
  plannedEndTimeStart?: string
  /** 对应后端字段 plannedEndTimeEnd */
  plannedEndTimeEnd?: string
  /** 对应后端字段 totalEmployees */
  totalEmployees?: number
  /** 对应后端字段 totalPlannedHours */
  totalPlannedHours?: number
  /** 对应后端字段 totalActualHours */
  totalActualHours?: number
  /** 对应后端字段 overtimeType */
  overtimeType?: number
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 approverId */
  approverId?: string
  /** 对应后端字段 approverBy */
  approverBy?: string
  /** 对应后端字段 approveTime */
  approveTime?: string
  /** 对应后端字段 approveTimeStart */
  approveTimeStart?: string
  /** 对应后端字段 approveTimeEnd */
  approveTimeEnd?: string
  /** 对应后端字段 approveComment */
  approveComment?: string
  /** 对应后端字段 handlingId */
  handlingId?: string
  /** 对应后端字段 handlingBy */
  handlingBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 handlingTimeStart */
  handlingTimeStart?: string
  /** 对应后端字段 handlingTimeEnd */
  handlingTimeEnd?: string
  /** 对应后端字段 handlingComment */
  handlingComment?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 overtimeStatus */
  overtimeStatus?: number
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
 * OvertimeCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeCreateDto）
 */
export interface OvertimeCreate {
  /** 对应后端字段 applicantId */
  applicantId?: string
  /** 对应后端字段 applicantBy */
  applicantBy?: string
  /** 对应后端字段 applicationDate */
  applicationDate: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 overtimeDate */
  overtimeDate: string
  /** 对应后端字段 plannedStartTime */
  plannedStartTime: string
  /** 对应后端字段 plannedEndTime */
  plannedEndTime: string
  /** 对应后端字段 totalEmployees */
  totalEmployees: number
  /** 对应后端字段 totalPlannedHours */
  totalPlannedHours: number
  /** 对应后端字段 totalActualHours */
  totalActualHours: number
  /** 对应后端字段 overtimeType */
  overtimeType: number
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 approverId */
  approverId?: string
  /** 对应后端字段 approverBy */
  approverBy?: string
  /** 对应后端字段 approveTime */
  approveTime?: string
  /** 对应后端字段 approveComment */
  approveComment?: string
  /** 对应后端字段 handlingId */
  handlingId?: string
  /** 对应后端字段 handlingBy */
  handlingBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 handlingComment */
  handlingComment?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 overtimeStatus */
  overtimeStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * OvertimeUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeUpdateDto）
 */
export interface OvertimeUpdate extends OvertimeCreate {
  /** 对应后端字段 overtimeId */
  overtimeId: string
}

/**
 * OvertimeStatus类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeStatusDto）
 */
export interface OvertimeStatus {
  /** 对应后端字段 overtimeId */
  overtimeId: string
  /** 对应后端字段 overtimeStatus */
  overtimeStatus: number
}
