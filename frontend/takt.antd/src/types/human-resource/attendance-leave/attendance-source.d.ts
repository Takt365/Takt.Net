// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-source
// 文件名称：attendance-source.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤源记录（设备原始打卡行）相关类型定义，对应后端 TaktAttendanceSourceDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 考勤源记录类型（对应后端 TaktAttendanceSourceDto；设备下载原始行）
 */
export interface AttendanceSource extends TaktEntityBase {
  /** 源记录 ID（对应后端 SourceId，序列化为 string） */
  sourceId: string
  /** 设备 ID（对应后端 DeviceId） */
  deviceId: string
  /** 设备编码（列表展示用，服务层填充，对应后端 DeviceCode） */
  deviceCode?: string
  /** 员工 ID（对应后端 EmployeeId） */
  employeeId: string
  /** 设备登记号/机内用户号（对应后端 EnrollNumber） */
  enrollNumber: string
  /** 设备原始打卡时间（对应后端 RawPunchTime） */
  rawPunchTime: string
  /** 验证方式（0=未知 1=指纹 2=人脸 3=密码 4=卡，对应后端 VerifyMode） */
  verifyMode: number
  /** 设备侧记录唯一键（去重用，对应后端 ExternalRecordKey） */
  externalRecordKey?: string
  /** 下载批次号（对应后端 DownloadBatchNo） */
  downloadBatchNo?: string
  /** 原始报文 JSON（对应后端 RawPayloadJson） */
  rawPayloadJson?: string
}

/**
 * 考勤源记录查询类型（对应后端 TaktAttendanceSourceQueryDto）
 */
export interface AttendanceSourceQuery extends TaktPagedQuery {
  /** 设备 ID（精确） */
  deviceId?: number
  /** 员工 ID（精确） */
  employeeId?: number
  /** 原始打卡时间起（含，对应后端 RawPunchTimeFrom） */
  rawPunchTimeFrom?: string
  /** 原始打卡时间止（含，对应后端 RawPunchTimeTo） */
  rawPunchTimeTo?: string
}

/**
 * 创建考勤源记录类型（对应后端 TaktAttendanceSourceCreateDto）
 */
export interface AttendanceSourceCreate {
  /** 设备 ID */
  deviceId: string
  /** 员工 ID */
  employeeId: string
  /** 设备登记号 */
  enrollNumber: string
  /** 设备原始打卡时间 */
  rawPunchTime: string
  /** 验证方式 */
  verifyMode: number
  /** 设备侧记录唯一键 */
  externalRecordKey?: string
  /** 下载批次号 */
  downloadBatchNo?: string
  /** 原始报文 JSON */
  rawPayloadJson?: string
  /** 备注 */
  remark?: string
}

/**
 * 更新考勤源记录类型（对应后端 TaktAttendanceSourceUpdateDto）
 */
export interface AttendanceSourceUpdate extends AttendanceSourceCreate {
  /** 源记录 ID */
  sourceId: string
}
