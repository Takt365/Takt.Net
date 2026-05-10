// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/specific-engine/human-resource-attendance-leave
// 文件名称：human-resource-attendance-leave.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：human-resource-attendance-leave相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AttendanceDeviceOnlineStatus类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktAttendanceDeviceOnlineStatusDto）
 */
export interface AttendanceDeviceOnlineStatus {
  /** 对应后端字段 deviceId */
  deviceId: string
  /** 对应后端字段 isOnline */
  isOnline: boolean
  /** 对应后端字段 message */
  message: string
}

/**
 * AttendanceDeviceUserSyncItem类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktAttendanceDeviceUserSyncItemDto）
 */
export interface AttendanceDeviceUserSyncItem {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 enrollNumber */
  enrollNumber: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 cardNo */
  cardNo?: string
  /** 对应后端字段 certificateNo */
  certificateNo?: string
  /** 对应后端字段 enabled */
  enabled: boolean
  /** 对应后端字段 mobile */
  mobile?: string
}

/**
 * AttendancePullRequest类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktAttendancePullRequestDto）
 */
export interface AttendancePullRequest {
  /** 对应后端字段 deviceId */
  deviceId: string
  /** 对应后端字段 startTime */
  startTime: string
  /** 对应后端字段 endTime */
  endTime: string
}

/**
 * AttendancePullResult类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktAttendancePullResultDto）
 */
export interface AttendancePullResult {
  /** 对应后端字段 success */
  success: boolean
  /** 对应后端字段 acceptedCount */
  acceptedCount: number
  /** 对应后端字段 errors */
  errors: string[]
  /** 对应后端字段 updatedDeviceConfigJson */
  updatedDeviceConfigJson?: string
}

/**
 * AttendancePushHandleResult类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktAttendancePushHandleResultDto）
 */
export interface AttendancePushHandleResult {
  /** 对应后端字段 success */
  success: boolean
  /** 对应后端字段 acceptedCount */
  acceptedCount: number
  /** 对应后端字段 errors */
  errors: string[]
}

/**
 * AttendancePushRequest类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktAttendancePushRequestDto）
 */
export interface AttendancePushRequest {
  /** 对应后端字段 deviceCode */
  deviceCode: string
  /** 对应后端字段 deviceType */
  deviceType: string
  /** 对应后端字段 rawPayloadJson */
  rawPayloadJson: string
  /** 对应后端字段 signature */
  signature?: string
  /** 对应后端字段 timestamp */
  timestamp?: number
}

/**
 * AttendanceSourceIngestRow类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktAttendanceSourceIngestRowDto）
 */
export interface AttendanceSourceIngestRow {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 enrollNumber */
  enrollNumber: string
  /** 对应后端字段 rawPunchTime */
  rawPunchTime: string
  /** 对应后端字段 verifyMode */
  verifyMode: number
  /** 对应后端字段 externalRecordKey */
  externalRecordKey?: string
  /** 对应后端字段 rawPayloadJson */
  rawPayloadJson?: string
}

/**
 * AttendanceUserSyncRequest类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktAttendanceUserSyncRequestDto）
 */
export interface AttendanceUserSyncRequest {
  /** 对应后端字段 users */
  users: unknown[]
}

/**
 * AttendanceUserSyncResult类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktAttendanceUserSyncResultDto）
 */
export interface AttendanceUserSyncResult {
  /** 对应后端字段 success */
  success: boolean
  /** 对应后端字段 syncedCount */
  syncedCount: number
  /** 对应后端字段 errors */
  errors: string[]
}

/**
 * LeaveStatus类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktLeaveStatusDto）
 */
export interface LeaveStatus {
  /** 对应后端字段 leaveId */
  leaveId: string
  /** 对应后端字段 leaveStatus */
  leaveStatus: number
}

/**
 * LeaveSubmit类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktLeaveSubmitDto）
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
  reason: string
  /** 对应后端字段 proofAttachmentsJson */
  proofAttachmentsJson?: string
  /** 对应后端字段 processTitle */
  processTitle?: string
  /** 对应后端字段 frmData */
  frmData?: string
}

/**
 * LeaveSubmitResult类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktLeaveSubmitResultDto）
 */
export interface LeaveSubmitResult {
  /** 对应后端字段 leaveId */
  leaveId: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
  /** 对应后端字段 instanceCode */
  instanceCode: string
  /** 对应后端字段 schemeKey */
  schemeKey: string
  /** 对应后端字段 schemeName */
  schemeName: string
}
