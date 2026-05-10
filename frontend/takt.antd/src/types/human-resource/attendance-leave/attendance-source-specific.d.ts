// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-source-specific
// 文件名称：attendance-source-specific.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：attendance-source-specific相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AttendanceSource类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktAttendanceSourceDto）
 */
export interface AttendanceSource {
  /** 对应后端字段 deviceCode */
  deviceCode?: string
}
