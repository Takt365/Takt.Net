// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-device
// 文件名称：attendance-device.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设备相关类型定义，对应后端 TaktAttendanceDeviceDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 考勤设备类型（对应后端 TaktAttendanceDeviceDto；列表/详情）
 */
export interface AttendanceDevice extends TaktEntityBase {
  /** 设备 ID（对应后端 DeviceId，序列化为 string） */
  deviceId: string
  /** 设备编码（业务唯一，对应后端 DeviceCode） */
  deviceCode: string
  /** 设备名称（对应后端 DeviceName） */
  deviceName: string
  /** 设备类型（对应后端 DeviceType） */
  deviceType?: string
  /** 设备品牌（对应后端 Manufacturer：Hikvision / Deli / ZKTeco） */
  manufacturer?: string
  /** IP 地址（对应后端 IpAddress） */
  ipAddress?: string
  /** 通讯端口（对应后端 Port） */
  port?: number
  /** 设备型号/固件说明（对应后端 DeviceModel） */
  deviceModel?: string
  /** 设备状态（0=停用 1=正常 2=故障，对应后端 DeviceStatus） */
  deviceStatus: number
  /** 上次从设备拉取原始记录时间（对应后端 LastPullAt） */
  lastPullAt?: string
}

/**
 * 考勤设备查询类型（对应后端 TaktAttendanceDeviceQueryDto）
 */
export interface AttendanceDeviceQuery extends TaktPagedQuery {
  /** 设备编码（模糊） */
  deviceCode?: string
  /** 设备名称（模糊） */
  deviceName?: string
  /** 设备类型（精确） */
  deviceType?: string
  /** 设备品牌（精确） */
  manufacturer?: string
  /** 设备状态（精确） */
  deviceStatus?: number
}

/**
 * 创建考勤设备类型（对应后端 TaktAttendanceDeviceCreateDto）
 */
export interface AttendanceDeviceCreate {
  /** 设备编码 */
  deviceCode: string
  /** 设备名称 */
  deviceName: string
  /** 设备类型 */
  deviceType?: string
  /** 设备品牌（Hikvision / Deli / ZKTeco） */
  manufacturer?: string
  /** IP 地址 */
  ipAddress?: string
  /** 通讯端口 */
  port?: number
  /** 设备型号/固件说明 */
  deviceModel?: string
  /** 设备状态 */
  deviceStatus: number
  /** 备注 */
  remark?: string
}

/**
 * 更新考勤设备类型（对应后端 TaktAttendanceDeviceUpdateDto）
 */
export interface AttendanceDeviceUpdate extends AttendanceDeviceCreate {
  /** 设备 ID */
  deviceId: string
}
