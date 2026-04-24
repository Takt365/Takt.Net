// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-correction
// 文件名称：attendance-correction.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：attendance-correction相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AttendanceCorrection类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceCorrectionDto）
 */
export interface AttendanceCorrection extends TaktEntityBase {
  /** 对应后端字段 correctionId */
  correctionId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 targetDate */
  targetDate: string
  /** 对应后端字段 correctionKind */
  correctionKind: number
  /** 对应后端字段 requestPunchTime */
  requestPunchTime: string
  /** 对应后端字段 reason */
  reason: string
  /** 对应后端字段 approvalStatus */
  approvalStatus: number
}

/**
 * AttendanceCorrectionQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceCorrectionQueryDto）
 */
export interface AttendanceCorrectionQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 targetDateFrom */
  targetDateFrom?: string
  /** 对应后端字段 targetDateTo */
  targetDateTo?: string
  /** 对应后端字段 approvalStatus */
  approvalStatus?: number
}

/**
 * AttendanceCorrectionCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceCorrectionCreateDto）
 */
export interface AttendanceCorrectionCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 targetDate */
  targetDate: string
  /** 对应后端字段 correctionKind */
  correctionKind: number
  /** 对应后端字段 requestPunchTime */
  requestPunchTime: string
  /** 对应后端字段 reason */
  reason: string
  /** 对应后端字段 approvalStatus */
  approvalStatus: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AttendanceCorrectionUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceCorrectionUpdateDto）
 */
export interface AttendanceCorrectionUpdate extends AttendanceCorrectionCreate {
  /** 对应后端字段 correctionId */
  correctionId: string
}
