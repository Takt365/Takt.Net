// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/scheduling/aps-schedule
// 文件名称：aps-schedule.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：aps-schedule相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ApsSchedule类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleDto）
 */
export interface ApsSchedule extends TaktEntityBase {
  /** 对应后端字段 apsScheduleId */
  apsScheduleId: string
  /** 对应后端字段 scheduleCode */
  scheduleCode: string
  /** 对应后端字段 scheduleName */
  scheduleName: string
  /** 对应后端字段 scheduleType */
  scheduleType: number
  /** 对应后端字段 planDate */
  planDate: string
  /** 对应后端字段 planStartTime */
  planStartTime: string
  /** 对应后端字段 planEndTime */
  planEndTime: string
  /** 对应后端字段 planCycle */
  planCycle: number
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 plantName */
  plantName?: string
  /** 对应后端字段 workshopCode */
  workshopCode?: string
  /** 对应后端字段 workshopName */
  workshopName?: string
  /** 对应后端字段 productionLineCode */
  productionLineCode?: string
  /** 对应后端字段 productionLineName */
  productionLineName?: string
  /** 对应后端字段 scheduleStrategy */
  scheduleStrategy: number
  /** 对应后端字段 scheduleAlgorithm */
  scheduleAlgorithm: number
  /** 对应后端字段 optimizationObjective */
  optimizationObjective: number
  /** 对应后端字段 scheduleStatus */
  scheduleStatus: number
  /** 对应后端字段 plannerId */
  plannerId?: string
  /** 对应后端字段 plannerName */
  plannerName?: string
  /** 对应后端字段 publishTime */
  publishTime?: string
  /** 对应后端字段 publishUserId */
  publishUserId?: string
  /** 对应后端字段 publishUserName */
  publishUserName?: string
  /** 对应后端字段 scheduleDescription */
  scheduleDescription?: string
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * ApsScheduleQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleQueryDto）
 */
export interface ApsScheduleQuery extends TaktPagedQuery {
  /** 对应后端字段 scheduleCode */
  scheduleCode?: string
  /** 对应后端字段 scheduleName */
  scheduleName?: string
  /** 对应后端字段 scheduleType */
  scheduleType?: number
  /** 对应后端字段 planDate */
  planDate?: string
  /** 对应后端字段 planDateStart */
  planDateStart?: string
  /** 对应后端字段 planDateEnd */
  planDateEnd?: string
  /** 对应后端字段 planStartTime */
  planStartTime?: string
  /** 对应后端字段 planStartTimeStart */
  planStartTimeStart?: string
  /** 对应后端字段 planStartTimeEnd */
  planStartTimeEnd?: string
  /** 对应后端字段 planEndTime */
  planEndTime?: string
  /** 对应后端字段 planEndTimeStart */
  planEndTimeStart?: string
  /** 对应后端字段 planEndTimeEnd */
  planEndTimeEnd?: string
  /** 对应后端字段 planCycle */
  planCycle?: number
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 plantName */
  plantName?: string
  /** 对应后端字段 workshopCode */
  workshopCode?: string
  /** 对应后端字段 workshopName */
  workshopName?: string
  /** 对应后端字段 productionLineCode */
  productionLineCode?: string
  /** 对应后端字段 productionLineName */
  productionLineName?: string
  /** 对应后端字段 scheduleStrategy */
  scheduleStrategy?: number
  /** 对应后端字段 scheduleAlgorithm */
  scheduleAlgorithm?: number
  /** 对应后端字段 optimizationObjective */
  optimizationObjective?: number
  /** 对应后端字段 scheduleStatus */
  scheduleStatus?: number
  /** 对应后端字段 plannerId */
  plannerId?: string
  /** 对应后端字段 plannerName */
  plannerName?: string
  /** 对应后端字段 publishTime */
  publishTime?: string
  /** 对应后端字段 publishTimeStart */
  publishTimeStart?: string
  /** 对应后端字段 publishTimeEnd */
  publishTimeEnd?: string
  /** 对应后端字段 publishUserId */
  publishUserId?: string
  /** 对应后端字段 publishUserName */
  publishUserName?: string
  /** 对应后端字段 scheduleDescription */
  scheduleDescription?: string
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
 * ApsScheduleCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleCreateDto）
 */
export interface ApsScheduleCreate {
  /** 对应后端字段 scheduleCode */
  scheduleCode: string
  /** 对应后端字段 scheduleName */
  scheduleName: string
  /** 对应后端字段 scheduleType */
  scheduleType: number
  /** 对应后端字段 planDate */
  planDate: string
  /** 对应后端字段 planStartTime */
  planStartTime: string
  /** 对应后端字段 planEndTime */
  planEndTime: string
  /** 对应后端字段 planCycle */
  planCycle: number
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 plantName */
  plantName?: string
  /** 对应后端字段 workshopCode */
  workshopCode?: string
  /** 对应后端字段 workshopName */
  workshopName?: string
  /** 对应后端字段 productionLineCode */
  productionLineCode?: string
  /** 对应后端字段 productionLineName */
  productionLineName?: string
  /** 对应后端字段 scheduleStrategy */
  scheduleStrategy: number
  /** 对应后端字段 scheduleAlgorithm */
  scheduleAlgorithm: number
  /** 对应后端字段 optimizationObjective */
  optimizationObjective: number
  /** 对应后端字段 scheduleStatus */
  scheduleStatus: number
  /** 对应后端字段 plannerId */
  plannerId?: string
  /** 对应后端字段 plannerName */
  plannerName?: string
  /** 对应后端字段 publishTime */
  publishTime?: string
  /** 对应后端字段 publishUserId */
  publishUserId?: string
  /** 对应后端字段 publishUserName */
  publishUserName?: string
  /** 对应后端字段 scheduleDescription */
  scheduleDescription?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * ApsScheduleUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleUpdateDto）
 */
export interface ApsScheduleUpdate extends ApsScheduleCreate {
  /** 对应后端字段 apsScheduleId */
  apsScheduleId: string
}

/**
 * ApsScheduleScheduleStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleScheduleStatusDto）
 */
export interface ApsScheduleScheduleStatus {
  /** 对应后端字段 apsScheduleId */
  apsScheduleId: string
  /** 对应后端字段 scheduleStatus */
  scheduleStatus: number
}
