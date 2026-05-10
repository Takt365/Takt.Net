// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/output/equipment-operation-rate
// 文件名称：equipment-operation-rate.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：equipment-operation-rate相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EquipmentOperationRate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktEquipmentOperationRateDto）
 */
export interface EquipmentOperationRate extends TaktEntityBase {
  /** 对应后端字段 equipmentOperationRateId */
  equipmentOperationRateId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 timeCategory */
  timeCategory: number
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 weekNumber */
  weekNumber?: number
  /** 对应后端字段 monthNumber */
  monthNumber?: number
  /** 对应后端字段 equipmentCode */
  equipmentCode: string
  /** 对应后端字段 equipmentName */
  equipmentName: string
  /** 对应后端字段 equipmentType */
  equipmentType: number
  /** 对应后端字段 productionLine */
  productionLine?: string
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 plannedRuntime */
  plannedRuntime: number
  /** 对应后端字段 actualRuntime */
  actualRuntime: number
  /** 对应后端字段 downtime */
  downtime: number
  /** 对应后端字段 equipmentOperationRate */
  equipmentOperationRate: number
  /** 对应后端字段 plannedOutput */
  plannedOutput: number
  /** 对应后端字段 actualOutput */
  actualOutput: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity: number
  /** 对应后端字段 defectiveQuantity */
  defectiveQuantity: number
  /** 对应后端字段 yieldRate */
  yieldRate: number
  /** 对应后端字段 downtimeReasonType */
  downtimeReasonType?: number
  /** 对应后端字段 downtimeReason */
  downtimeReason?: string
  /** 对应后端字段 equipmentStatus */
  equipmentStatus: number
  /** 对应后端字段 equipmentOperator */
  equipmentOperator?: string
  /** 对应后端字段 equipmentMaintainer */
  equipmentMaintainer?: string
  /** 对应后端字段 teamLeader */
  teamLeader?: string
  /** 对应后端字段 status */
  status: number
}

/**
 * EquipmentOperationRateQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktEquipmentOperationRateQueryDto）
 */
export interface EquipmentOperationRateQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 timeCategory */
  timeCategory?: number
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 startDateStart */
  startDateStart?: string
  /** 对应后端字段 startDateEnd */
  startDateEnd?: string
  /** 对应后端字段 endDate */
  endDate?: string
  /** 对应后端字段 endDateStart */
  endDateStart?: string
  /** 对应后端字段 endDateEnd */
  endDateEnd?: string
  /** 对应后端字段 weekNumber */
  weekNumber?: number
  /** 对应后端字段 monthNumber */
  monthNumber?: number
  /** 对应后端字段 equipmentCode */
  equipmentCode?: string
  /** 对应后端字段 equipmentName */
  equipmentName?: string
  /** 对应后端字段 equipmentType */
  equipmentType?: number
  /** 对应后端字段 productionLine */
  productionLine?: string
  /** 对应后端字段 shiftNo */
  shiftNo?: number
  /** 对应后端字段 plannedRuntime */
  plannedRuntime?: number
  /** 对应后端字段 actualRuntime */
  actualRuntime?: number
  /** 对应后端字段 downtime */
  downtime?: number
  /** 对应后端字段 equipmentOperationRate */
  equipmentOperationRate?: number
  /** 对应后端字段 plannedOutput */
  plannedOutput?: number
  /** 对应后端字段 actualOutput */
  actualOutput?: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity?: number
  /** 对应后端字段 defectiveQuantity */
  defectiveQuantity?: number
  /** 对应后端字段 yieldRate */
  yieldRate?: number
  /** 对应后端字段 downtimeReasonType */
  downtimeReasonType?: number
  /** 对应后端字段 downtimeReason */
  downtimeReason?: string
  /** 对应后端字段 equipmentStatus */
  equipmentStatus?: number
  /** 对应后端字段 equipmentOperator */
  equipmentOperator?: string
  /** 对应后端字段 equipmentMaintainer */
  equipmentMaintainer?: string
  /** 对应后端字段 teamLeader */
  teamLeader?: string
  /** 对应后端字段 status */
  status?: number
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
 * EquipmentOperationRateCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktEquipmentOperationRateCreateDto）
 */
export interface EquipmentOperationRateCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 timeCategory */
  timeCategory: number
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 endDate */
  endDate: string
  /** 对应后端字段 weekNumber */
  weekNumber?: number
  /** 对应后端字段 monthNumber */
  monthNumber?: number
  /** 对应后端字段 equipmentCode */
  equipmentCode: string
  /** 对应后端字段 equipmentName */
  equipmentName: string
  /** 对应后端字段 equipmentType */
  equipmentType: number
  /** 对应后端字段 productionLine */
  productionLine?: string
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 plannedRuntime */
  plannedRuntime: number
  /** 对应后端字段 actualRuntime */
  actualRuntime: number
  /** 对应后端字段 downtime */
  downtime: number
  /** 对应后端字段 equipmentOperationRate */
  equipmentOperationRate: number
  /** 对应后端字段 plannedOutput */
  plannedOutput: number
  /** 对应后端字段 actualOutput */
  actualOutput: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity: number
  /** 对应后端字段 defectiveQuantity */
  defectiveQuantity: number
  /** 对应后端字段 yieldRate */
  yieldRate: number
  /** 对应后端字段 downtimeReasonType */
  downtimeReasonType?: number
  /** 对应后端字段 downtimeReason */
  downtimeReason?: string
  /** 对应后端字段 equipmentStatus */
  equipmentStatus: number
  /** 对应后端字段 equipmentOperator */
  equipmentOperator?: string
  /** 对应后端字段 equipmentMaintainer */
  equipmentMaintainer?: string
  /** 对应后端字段 teamLeader */
  teamLeader?: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EquipmentOperationRateUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktEquipmentOperationRateUpdateDto）
 */
export interface EquipmentOperationRateUpdate extends EquipmentOperationRateCreate {
  /** 对应后端字段 equipmentOperationRateId */
  equipmentOperationRateId: string
}

/**
 * EquipmentOperationRateStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktEquipmentOperationRateStatusDto）
 */
export interface EquipmentOperationRateStatus {
  /** 对应后端字段 equipmentOperationRateId */
  equipmentOperationRateId: string
  /** 对应后端字段 status */
  status: number
}

/**
 * EquipmentOperationRateEquipmentStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktEquipmentOperationRateEquipmentStatusDto）
 */
export interface EquipmentOperationRateEquipmentStatus {
  /** 对应后端字段 equipmentOperationRateId */
  equipmentOperationRateId: string
  /** 对应后端字段 equipmentStatus */
  equipmentStatus: number
}
