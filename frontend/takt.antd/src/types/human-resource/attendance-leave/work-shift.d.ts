// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/work-shift
// 文件名称：work-shift.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：班次相关类型定义，对应后端 TaktWorkShiftDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 班次类型（对应后端 TaktWorkShiftDto；列表/详情）
 */
export interface WorkShift extends TaktEntityBase {
  /** 班次 ID（对应后端 ShiftId，序列化为 string） */
  shiftId: string
  /** 班次编码（对应后端 ShiftCode） */
  shiftCode: string
  /** 班次名称（对应后端 ShiftName） */
  shiftName: string
  /** 当班开始时间 HH:mm（对应后端 StartTime） */
  startTime: string
  /** 当班结束时间 HH:mm（对应后端 EndTime） */
  endTime: string
  /** 是否跨自然日（0=否 1=是，对应后端 CrossMidnight） */
  crossMidnight: number
  /** 排序号（对应后端 OrderNum） */
  orderNum: number
}

/**
 * 班次查询类型（对应后端 TaktWorkShiftQueryDto）
 */
export interface WorkShiftQuery extends TaktPagedQuery {
  /** 班次编码（模糊） */
  shiftCode?: string
  /** 班次名称（模糊） */
  shiftName?: string
}

/**
 * 创建班次类型（对应后端 TaktWorkShiftCreateDto）
 */
export interface WorkShiftCreate {
  /** 班次编码 */
  shiftCode: string
  /** 班次名称 */
  shiftName: string
  /** 当班开始时间 HH:mm */
  startTime: string
  /** 当班结束时间 HH:mm */
  endTime: string
  /** 是否跨自然日 */
  crossMidnight: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remark?: string
}

/**
 * 更新班次类型（对应后端 TaktWorkShiftUpdateDto）
 */
export interface WorkShiftUpdate extends WorkShiftCreate {
  /** 班次 ID */
  shiftId: string
}
