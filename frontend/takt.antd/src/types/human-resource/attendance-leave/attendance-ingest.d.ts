// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-ingest
// 文件名称：attendance-ingest.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：attendance-ingest相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AttendanceDeviceStatus类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceDeviceStatusDto）
 */
export interface AttendanceDeviceStatus {
  /** 对应后端字段 deviceId */
  deviceId: string
  /** 对应后端字段 isOnline */
  isOnline: boolean
  /** 对应后端字段 message */
  message: string
}

/**
 * AttendanceDeviceUserSyncItem类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceDeviceUserSyncItemDto）
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
 * AttendancePullRequest类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendancePullRequestDto）
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
 * AttendancePullResult类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendancePullResultDto）
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
 * AttendancePushHandleResult类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendancePushHandleResultDto）
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
 * AttendancePushRequest类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendancePushRequestDto）
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
 * AttendanceSourceIngestRow类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceSourceIngestRowDto）
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
 * AttendanceUserSyncRequest类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceUserSyncRequestDto）
 */
export interface AttendanceUserSyncRequest {
  /** 对应后端字段 users */
  users: unknown[]
}

/**
 * AttendanceUserSyncResult类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceUserSyncResultDto）
 */
export interface AttendanceUserSyncResult {
  /** 对应后端字段 success */
  success: boolean
  /** 对应后端字段 syncedCount */
  syncedCount: number
  /** 对应后端字段 errors */
  errors: string[]
}
