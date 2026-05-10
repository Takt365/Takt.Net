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
  /** 对应后端字段 leaveType */
  leaveType: string
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 reason */
  reason: string
  /** 对应后端字段 proofAttachmentsJson */
  proofAttachmentsJson?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
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
  /** 对应后端字段 leaveStatus */
  leaveStatus: number
}

/**
 * LeaveQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktLeaveQueryDto）
 */
export interface LeaveQuery extends TaktPagedQuery {
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
  /** 对应后端字段 leaveType */
  leaveType?: string
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 startDateStart */
  startDateStart?: string
  /** 对应后端字段 startDateEnd */
  startDateEnd?: string
  /** 对应后端字段 endDate */
  endDate?: string
  /** 对应后端字段 endDateStart */
  endDateStart?: string
  /** 对应后端字段 endDateEnd */
  endDateEnd?: string
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 proofAttachmentsJson */
  proofAttachmentsJson?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
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
  /** 对应后端字段 leaveStatus */
  leaveStatus?: number
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
 * LeaveCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktLeaveCreateDto）
 */
export interface LeaveCreate {
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
  /** 对应后端字段 leaveType */
  leaveType: string
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 reason */
  reason: string
  /** 对应后端字段 proofAttachmentsJson */
  proofAttachmentsJson?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
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
  /** 对应后端字段 leaveStatus */
  leaveStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
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
}
