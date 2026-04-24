// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/leave
// 文件名称：leave.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：leave相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Leave类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktLeaveDto）
 */
export interface Leave extends TaktEntityBase {
  /** 对应后端字段 leaveId */
  leaveId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 leaveType */
  leaveType: string
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 proofAttachmentsJson */
  proofAttachmentsJson?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 leaveStatus */
  leaveStatus: number
}

/**
 * LeaveQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktLeaveQueryDto）
 */
export interface LeaveQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 leaveType */
  leaveType?: string
  /** 对应后端字段 leaveStatus */
  leaveStatus?: number
  /** 对应后端字段 startDateFrom */
  startDateFrom?: string
  /** 对应后端字段 startDateTo */
  startDateTo?: string
}

/**
 * LeaveCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktLeaveCreateDto）
 */
export interface LeaveCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 leaveType */
  leaveType: string
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 proofAttachmentsJson */
  proofAttachmentsJson?: string
}

/**
 * LeaveUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktLeaveUpdateDto）
 */
export interface LeaveUpdate extends LeaveCreate {
  /** 对应后端字段 leaveId */
  leaveId: string
}

/**
 * LeaveStatus类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktLeaveStatusDto）
 */
export interface LeaveStatus {
  /** 对应后端字段 leaveId */
  leaveId: string
  /** 对应后端字段 leaveStatus */
  leaveStatus: number
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
}

/**
 * LeaveSubmit类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktLeaveSubmitDto）
 */
export interface LeaveSubmit {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 leaveType */
  leaveType: string
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 proofAttachmentsJson */
  proofAttachmentsJson?: string
  /** 对应后端字段 processTitle */
  processTitle?: string
  /** 对应后端字段 frmData */
  frmData?: string
}

/**
 * LeaveSubmitResult类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktLeaveSubmitResultDto）
 */
export interface LeaveSubmitResult {
  /** 对应后端字段 leaveId */
  leaveId: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
  /** 对应后端字段 instanceCode */
  instanceCode: string
  /** 对应后端字段 processKey */
  processKey: string
  /** 对应后端字段 processName */
  processName: string
}
