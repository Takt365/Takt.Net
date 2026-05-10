// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/specific-engine/attendance-source
// 文件名称：attendance-source.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：attendance-source相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AttendanceSource类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.SpecificEngine.TaktAttendanceSourceDto）
 */
export interface AttendanceSource extends TaktEntityBase {
  /** 对应后端字段 attendanceSourceId */
  attendanceSourceId: string
  /** 对应后端字段 deviceId */
  deviceId: string
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
  /** 对应后端字段 downloadBatchNo */
  downloadBatchNo?: string
  /** 对应后端字段 rawPayloadJson */
  rawPayloadJson?: string
}
