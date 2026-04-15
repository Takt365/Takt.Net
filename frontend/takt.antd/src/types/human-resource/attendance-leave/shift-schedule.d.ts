// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/shift-schedule
// 文件名称：shift-schedule.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：排班计划相关类型定义，对应后端 TaktShiftScheduleDtos（前端为人员排班子集字段）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 排班计划类型（对应后端 TaktShiftScheduleDto；含 ScheduleType/DeptId 等见后端完整 DTO）
 */
export interface ShiftSchedule extends TaktEntityBase {
  /** 排班记录 ID（对应后端 ShiftScheduleId，序列化为 string） */
  shiftScheduleId: string
  /** 员工 ID（ScheduleType=1 时有效，对应后端 EmployeeId） */
  employeeId: string
  /** 排班日期（日期部分有效，对应后端 ScheduleDate） */
  scheduleDate: string
  /** 班次 ID（对应 TaktWorkShift，对应后端 ShiftId） */
  shiftId: string
  /** 班次名称（列表展示用，服务层填充，对应后端 ShiftName） */
  shiftName?: string
}

/**
 * 排班计划查询类型（对应后端 TaktShiftScheduleQueryDto；完整查询条件见后端）
 */
export interface ShiftScheduleQuery extends TaktPagedQuery {
  /** 员工 ID（精确） */
  employeeId?: number
  /** 班次 ID（精确） */
  shiftId?: number
  /** 排班日期起（含，对应后端 ScheduleDateFrom） */
  scheduleDateFrom?: string
  /** 排班日期止（含，对应后端 ScheduleDateTo） */
  scheduleDateTo?: string
}

/**
 * 创建排班计划类型（对应后端 TaktShiftScheduleCreateDto 之人员排班场景常用字段）
 */
export interface ShiftScheduleCreate {
  /** 员工 ID */
  employeeId: string
  /** 排班日期 */
  scheduleDate: string
  /** 班次 ID */
  shiftId: string
  /** 备注 */
  remark?: string
}

/**
 * 更新排班计划类型（对应后端 TaktShiftScheduleUpdateDto）
 */
export interface ShiftScheduleUpdate extends ShiftScheduleCreate {
  /** 排班记录 ID */
  shiftScheduleId: string
}
