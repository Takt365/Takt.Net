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
  /** 对应后端字段 attendanceCorrectionId */
  attendanceCorrectionId: string
  /** 对应后端字段 employeeId */
  employeeId: string
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
  /** 对应后端字段 targetDate */
  targetDate: string
  /** 对应后端字段 correctionKind */
  correctionKind: number
  /** 对应后端字段 requestPunchTime */
  requestPunchTime: string
  /** 对应后端字段 reason */
  reason: string
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
  /** 对应后端字段 approvalStatus */
  approvalStatus: number
}

/**
 * AttendanceCorrectionQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceCorrectionQueryDto）
 */
export interface AttendanceCorrectionQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
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
  /** 对应后端字段 targetDate */
  targetDate?: string
  /** 对应后端字段 targetDateStart */
  targetDateStart?: string
  /** 对应后端字段 targetDateEnd */
  targetDateEnd?: string
  /** 对应后端字段 correctionKind */
  correctionKind?: number
  /** 对应后端字段 requestPunchTime */
  requestPunchTime?: string
  /** 对应后端字段 requestPunchTimeStart */
  requestPunchTimeStart?: string
  /** 对应后端字段 requestPunchTimeEnd */
  requestPunchTimeEnd?: string
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
  /** 对应后端字段 approvalStatus */
  approvalStatus?: number
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
 * AttendanceCorrectionCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceCorrectionCreateDto）
 */
export interface AttendanceCorrectionCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
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
  /** 对应后端字段 targetDate */
  targetDate: string
  /** 对应后端字段 correctionKind */
  correctionKind: number
  /** 对应后端字段 requestPunchTime */
  requestPunchTime: string
  /** 对应后端字段 reason */
  reason: string
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
  /** 对应后端字段 approvalStatus */
  approvalStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AttendanceCorrectionUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceCorrectionUpdateDto）
 */
export interface AttendanceCorrectionUpdate extends AttendanceCorrectionCreate {
  /** 对应后端字段 attendanceCorrectionId */
  attendanceCorrectionId: string
}

/**
 * AttendanceCorrectionApprovalStatus类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceCorrectionApprovalStatusDto）
 */
export interface AttendanceCorrectionApprovalStatus {
  /** 对应后端字段 attendanceCorrectionId */
  attendanceCorrectionId: string
  /** 对应后端字段 approvalStatus */
  approvalStatus: number
}
