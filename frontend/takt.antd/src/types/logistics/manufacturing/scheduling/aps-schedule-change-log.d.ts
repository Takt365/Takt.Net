// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/scheduling/aps-schedule-change-log
// 文件名称：aps-schedule-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：aps-schedule-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ApsScheduleChangeLog类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleChangeLogDto）
 */
export interface ApsScheduleChangeLog extends TaktEntityBase {
  /** 对应后端字段 apsScheduleChangeLogId */
  apsScheduleChangeLogId: string
  /** 对应后端字段 apsScheduleId */
  apsScheduleId: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeType */
  changeType: number
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 schedule */
  schedule?: unknown
}

/**
 * ApsScheduleChangeLogQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleChangeLogQueryDto）
 */
export interface ApsScheduleChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 apsScheduleId */
  apsScheduleId?: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeType */
  changeType?: number
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeTime */
  changeTime?: string
  /** 对应后端字段 changeTimeStart */
  changeTimeStart?: string
  /** 对应后端字段 changeTimeEnd */
  changeTimeEnd?: string
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
 * ApsScheduleChangeLogCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleChangeLogCreateDto）
 */
export interface ApsScheduleChangeLogCreate {
  /** 对应后端字段 apsScheduleId */
  apsScheduleId: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeType */
  changeType: number
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ApsScheduleChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleChangeLogUpdateDto）
 */
export interface ApsScheduleChangeLogUpdate extends ApsScheduleChangeLogCreate {
  /** 对应后端字段 apsScheduleChangeLogId */
  apsScheduleChangeLogId: string
}
