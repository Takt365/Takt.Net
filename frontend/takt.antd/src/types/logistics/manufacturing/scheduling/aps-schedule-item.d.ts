// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/scheduling/aps-schedule-item
// 文件名称：aps-schedule-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：aps-schedule-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ApsScheduleItem类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleItemDto）
 */
export interface ApsScheduleItem extends TaktEntityBase {
  /** 对应后端字段 apsScheduleItemId */
  apsScheduleItemId: string
  /** 对应后端字段 apsScheduleId */
  apsScheduleId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 workOrderCode */
  workOrderCode: string
  /** 对应后端字段 workOrderId */
  workOrderId: string
  /** 对应后端字段 productCode */
  productCode: string
  /** 对应后端字段 productName */
  productName: string
  /** 对应后端字段 workCenterCode */
  workCenterCode?: string
  /** 对应后端字段 workCenterName */
  workCenterName?: string
  /** 对应后端字段 processCode */
  processCode: string
  /** 对应后端字段 processName */
  processName: string
  /** 对应后端字段 processSequence */
  processSequence: number
  /** 对应后端字段 processStandardST */
  processStandardST: number
  /** 对应后端字段 processStandardSTUnit */
  processStandardSTUnit: number
  /** 对应后端字段 extraMinutes */
  extraMinutes: number
  /** 对应后端字段 planQuantity */
  planQuantity: number
  /** 对应后端字段 planStartTime */
  planStartTime: string
  /** 对应后端字段 planEndTime */
  planEndTime: string
  /** 对应后端字段 actualStartTime */
  actualStartTime?: string
  /** 对应后端字段 actualEndTime */
  actualEndTime?: string
  /** 对应后端字段 equipmentCode */
  equipmentCode?: string
  /** 对应后端字段 equipmentName */
  equipmentName?: string
  /** 对应后端字段 teamCode */
  teamCode?: string
  /** 对应后端字段 teamName */
  teamName?: string
  /** 对应后端字段 processStatus */
  processStatus: number
  /** 对应后端字段 priority */
  priority: number
  /** 对应后端字段 schedule */
  schedule?: unknown
}

/**
 * ApsScheduleItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleItemQueryDto）
 */
export interface ApsScheduleItemQuery extends TaktPagedQuery {
  /** 对应后端字段 apsScheduleId */
  apsScheduleId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 workOrderCode */
  workOrderCode?: string
  /** 对应后端字段 workOrderId */
  workOrderId?: string
  /** 对应后端字段 productCode */
  productCode?: string
  /** 对应后端字段 productName */
  productName?: string
  /** 对应后端字段 workCenterCode */
  workCenterCode?: string
  /** 对应后端字段 workCenterName */
  workCenterName?: string
  /** 对应后端字段 processCode */
  processCode?: string
  /** 对应后端字段 processName */
  processName?: string
  /** 对应后端字段 processSequence */
  processSequence?: number
  /** 对应后端字段 processStandardST */
  processStandardST?: number
  /** 对应后端字段 processStandardSTUnit */
  processStandardSTUnit?: number
  /** 对应后端字段 extraMinutes */
  extraMinutes?: number
  /** 对应后端字段 planQuantity */
  planQuantity?: number
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
  /** 对应后端字段 equipmentCode */
  equipmentCode?: string
  /** 对应后端字段 equipmentName */
  equipmentName?: string
  /** 对应后端字段 teamCode */
  teamCode?: string
  /** 对应后端字段 teamName */
  teamName?: string
  /** 对应后端字段 processStatus */
  processStatus?: number
  /** 对应后端字段 priority */
  priority?: number
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
 * ApsScheduleItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleItemCreateDto）
 */
export interface ApsScheduleItemCreate {
  /** 对应后端字段 apsScheduleId */
  apsScheduleId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 workOrderCode */
  workOrderCode: string
  /** 对应后端字段 workOrderId */
  workOrderId: string
  /** 对应后端字段 productCode */
  productCode: string
  /** 对应后端字段 productName */
  productName: string
  /** 对应后端字段 workCenterCode */
  workCenterCode?: string
  /** 对应后端字段 workCenterName */
  workCenterName?: string
  /** 对应后端字段 processCode */
  processCode: string
  /** 对应后端字段 processName */
  processName: string
  /** 对应后端字段 processSequence */
  processSequence: number
  /** 对应后端字段 processStandardST */
  processStandardST: number
  /** 对应后端字段 processStandardSTUnit */
  processStandardSTUnit: number
  /** 对应后端字段 extraMinutes */
  extraMinutes: number
  /** 对应后端字段 planQuantity */
  planQuantity: number
  /** 对应后端字段 planStartTime */
  planStartTime: string
  /** 对应后端字段 planEndTime */
  planEndTime: string
  /** 对应后端字段 actualStartTime */
  actualStartTime?: string
  /** 对应后端字段 actualEndTime */
  actualEndTime?: string
  /** 对应后端字段 equipmentCode */
  equipmentCode?: string
  /** 对应后端字段 equipmentName */
  equipmentName?: string
  /** 对应后端字段 teamCode */
  teamCode?: string
  /** 对应后端字段 teamName */
  teamName?: string
  /** 对应后端字段 processStatus */
  processStatus: number
  /** 对应后端字段 priority */
  priority: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ApsScheduleItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleItemUpdateDto）
 */
export interface ApsScheduleItemUpdate extends ApsScheduleItemCreate {
  /** 对应后端字段 apsScheduleItemId */
  apsScheduleItemId: string
}

/**
 * ApsScheduleItemProcessStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Scheduling.TaktApsScheduleItemProcessStatusDto）
 */
export interface ApsScheduleItemProcessStatus {
  /** 对应后端字段 apsScheduleItemId */
  apsScheduleItemId: string
  /** 对应后端字段 processStatus */
  processStatus: number
}
