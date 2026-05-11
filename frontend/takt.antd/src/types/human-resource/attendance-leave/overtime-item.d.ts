// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/overtime-item
// 文件名称：overtime-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：overtime-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * OvertimeItem类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeItemDto）
 */
export interface OvertimeItem extends TaktEntityBase {
  /** 对应后端字段 overtimeItemId */
  overtimeItemId: string
  /** 对应后端字段 overtimeId */
  overtimeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 employeeName */
  employeeName: string
  /** 对应后端字段 plannedHours */
  plannedHours: number
  /** 对应后端字段 actualStartTime */
  actualStartTime?: string
  /** 对应后端字段 actualEndTime */
  actualEndTime?: string
  /** 对应后端字段 actualHours */
  actualHours?: number
  /** 对应后端字段 overtime */
  overtime?: unknown
}

/**
 * OvertimeItemQuery类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeItemQueryDto）
 */
export interface OvertimeItemQuery extends TaktPagedQuery {
  /** 对应后端字段 overtimeId */
  overtimeId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 employeeName */
  employeeName?: string
  /** 对应后端字段 plannedHours */
  plannedHours?: number
  /** 对应后端字段 actualStartTime */
  actualStartTime?: string
  /** 对应后端字段 actualStartTimeStart */
  actualStartTimeStart?: string
  /** 对应后端字段 actualStartTimeEnd */
  actualStartTimeEnd?: string
  /** 对应后端字段 actualEndTime */
  actualEndTime?: string
  /** 对应后端字段 actualEndTimeStart */
  actualEndTimeStart?: string
  /** 对应后端字段 actualEndTimeEnd */
  actualEndTimeEnd?: string
  /** 对应后端字段 actualHours */
  actualHours?: number
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 createdById */
  createdById?: string
  /** 对应后端字段 createdBy */
  createdBy?: string
  /** 对应后端字段 createdAt */
  createdAt?: string
  /** 对应后端字段 createdAtStart */
  createdAtStart?: string
  /** 对应后端字段 createdAtEnd */
  createdAtEnd?: string
}

/**
 * OvertimeItemCreate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeItemCreateDto）
 */
export interface OvertimeItemCreate {
  /** 对应后端字段 overtimeId */
  overtimeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 employeeName */
  employeeName: string
  /** 对应后端字段 plannedHours */
  plannedHours: number
  /** 对应后端字段 actualStartTime */
  actualStartTime?: string
  /** 对应后端字段 actualEndTime */
  actualEndTime?: string
  /** 对应后端字段 actualHours */
  actualHours?: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * OvertimeItemUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktOvertimeItemUpdateDto）
 */
export interface OvertimeItemUpdate extends OvertimeItemCreate {
  /** 对应后端字段 overtimeItemId */
  overtimeItemId: string
}
