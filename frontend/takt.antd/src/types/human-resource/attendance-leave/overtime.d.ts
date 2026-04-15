// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/overtime
// 文件名称：overtime.d.ts
// 功能描述：加班类型，对应后端 TaktOvertimeDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

export interface Overtime extends TaktEntityBase {
  overtimeId: string
  employeeId: string
  overtimeDate: string
  plannedHours: number
  actualHours?: number
  reason: string
  overtimeStatus: number
}

export interface OvertimeQuery extends TaktPagedQuery {
  employeeId?: number
  overtimeStatus?: number
  overtimeDateFrom?: string
  overtimeDateTo?: string
}

export interface OvertimeCreate {
  employeeId: string
  overtimeDate: string
  plannedHours: number
  actualHours?: number
  reason: string
  overtimeStatus: number
  remark?: string
}

export interface OvertimeUpdate extends OvertimeCreate {
  overtimeId: string
}
