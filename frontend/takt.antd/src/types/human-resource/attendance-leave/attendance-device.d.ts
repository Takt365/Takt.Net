// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-device
// 文件名称：attendance-device.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：attendance-device相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AttendanceDevice类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceDeviceDto）
 */
export interface AttendanceDevice extends TaktEntityBase {
  /** 对应后端字段 deviceId */
  deviceId: string
  /** 对应后端字段 deviceCode */
  deviceCode: string
  /** 对应后端字段 deviceName */
  deviceName: string
  /** 对应后端字段 deviceType */
  deviceType: string
  /** 对应后端字段 manufacturer */
  manufacturer?: string
  /** 对应后端字段 ipAddress */
  ipAddress?: string
  /** 对应后端字段 port */
  port?: number
  /** 对应后端字段 deviceModel */
  deviceModel?: string
  /** 对应后端字段 configJson */
  configJson?: string
  /** 对应后端字段 deviceStatus */
  deviceStatus: number
  /** 对应后端字段 isPushEnabled */
  isPushEnabled: number
  /** 对应后端字段 lastPullAt */
  lastPullAt?: string
  /** 对应后端字段 lastPushAt */
  lastPushAt?: string
}

/**
 * AttendanceDeviceQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceDeviceQueryDto）
 */
export interface AttendanceDeviceQuery extends TaktPagedQuery {
  /** 对应后端字段 deviceCode */
  deviceCode?: string
  /** 对应后端字段 deviceName */
  deviceName?: string
  /** 对应后端字段 deviceType */
  deviceType?: string
  /** 对应后端字段 manufacturer */
  manufacturer?: string
  /** 对应后端字段 deviceStatus */
  deviceStatus?: number
}

/**
 * AttendanceDeviceCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceDeviceCreateDto）
 */
export interface AttendanceDeviceCreate {
  /** 对应后端字段 deviceCode */
  deviceCode: string
  /** 对应后端字段 deviceName */
  deviceName: string
  /** 对应后端字段 deviceType */
  deviceType: string
  /** 对应后端字段 manufacturer */
  manufacturer?: string
  /** 对应后端字段 ipAddress */
  ipAddress?: string
  /** 对应后端字段 port */
  port?: number
  /** 对应后端字段 deviceModel */
  deviceModel?: string
  /** 对应后端字段 apiSecret */
  apiSecret?: string
  /** 对应后端字段 configJson */
  configJson?: string
  /** 对应后端字段 deviceStatus */
  deviceStatus: number
  /** 对应后端字段 isPushEnabled */
  isPushEnabled: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AttendanceDeviceUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceDeviceUpdateDto）
 */
export interface AttendanceDeviceUpdate extends AttendanceDeviceCreate {
  /** 对应后端字段 deviceId */
  deviceId: string
}
