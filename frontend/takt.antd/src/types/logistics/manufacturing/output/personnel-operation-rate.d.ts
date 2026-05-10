// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/output/personnel-operation-rate
// 文件名称：personnel-operation-rate.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：personnel-operation-rate相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PersonnelOperationRate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPersonnelOperationRateDto）
 */
export interface PersonnelOperationRate extends TaktEntityBase {
  /** 对应后端字段 personnelOperationRateId */
  personnelOperationRateId: string
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
  /** 对应后端字段 productionLine */
  productionLine: string
  /** 对应后端字段 productionLineName */
  productionLineName?: string
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 plannedDirectPersonnelCount */
  plannedDirectPersonnelCount: number
  /** 对应后端字段 actualDirectPersonnelCount */
  actualDirectPersonnelCount: number
  /** 对应后端字段 plannedIndirectPersonnelCount */
  plannedIndirectPersonnelCount: number
  /** 对应后端字段 actualIndirectPersonnelCount */
  actualIndirectPersonnelCount: number
  /** 对应后端字段 plannedWorkTime */
  plannedWorkTime: number
  /** 对应后端字段 actualWorkTime */
  actualWorkTime: number
  /** 对应后端字段 breakTime */
  breakTime: number
  /** 对应后端字段 idleTime */
  idleTime: number
  /** 对应后端字段 personnelOperationRate */
  personnelOperationRate: number
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
  /** 对应后端字段 workEfficiency */
  workEfficiency: number
  /** 对应后端字段 idleReasonType */
  idleReasonType?: number
  /** 对应后端字段 idleReason */
  idleReason?: string
  /** 对应后端字段 overtimeHours */
  overtimeHours: number
  /** 对应后端字段 teamLeader */
  teamLeader?: string
  /** 对应后端字段 supervisor */
  supervisor?: string
  /** 对应后端字段 status */
  status: number
}

/**
 * PersonnelOperationRateQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPersonnelOperationRateQueryDto）
 */
export interface PersonnelOperationRateQuery extends TaktPagedQuery {
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
  /** 对应后端字段 productionLine */
  productionLine?: string
  /** 对应后端字段 productionLineName */
  productionLineName?: string
  /** 对应后端字段 shiftNo */
  shiftNo?: number
  /** 对应后端字段 plannedDirectPersonnelCount */
  plannedDirectPersonnelCount?: number
  /** 对应后端字段 actualDirectPersonnelCount */
  actualDirectPersonnelCount?: number
  /** 对应后端字段 plannedIndirectPersonnelCount */
  plannedIndirectPersonnelCount?: number
  /** 对应后端字段 actualIndirectPersonnelCount */
  actualIndirectPersonnelCount?: number
  /** 对应后端字段 plannedWorkTime */
  plannedWorkTime?: number
  /** 对应后端字段 actualWorkTime */
  actualWorkTime?: number
  /** 对应后端字段 breakTime */
  breakTime?: number
  /** 对应后端字段 idleTime */
  idleTime?: number
  /** 对应后端字段 personnelOperationRate */
  personnelOperationRate?: number
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
  /** 对应后端字段 workEfficiency */
  workEfficiency?: number
  /** 对应后端字段 idleReasonType */
  idleReasonType?: number
  /** 对应后端字段 idleReason */
  idleReason?: string
  /** 对应后端字段 overtimeHours */
  overtimeHours?: number
  /** 对应后端字段 teamLeader */
  teamLeader?: string
  /** 对应后端字段 supervisor */
  supervisor?: string
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
 * PersonnelOperationRateCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPersonnelOperationRateCreateDto）
 */
export interface PersonnelOperationRateCreate {
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
  /** 对应后端字段 productionLine */
  productionLine: string
  /** 对应后端字段 productionLineName */
  productionLineName?: string
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 plannedDirectPersonnelCount */
  plannedDirectPersonnelCount: number
  /** 对应后端字段 actualDirectPersonnelCount */
  actualDirectPersonnelCount: number
  /** 对应后端字段 plannedIndirectPersonnelCount */
  plannedIndirectPersonnelCount: number
  /** 对应后端字段 actualIndirectPersonnelCount */
  actualIndirectPersonnelCount: number
  /** 对应后端字段 plannedWorkTime */
  plannedWorkTime: number
  /** 对应后端字段 actualWorkTime */
  actualWorkTime: number
  /** 对应后端字段 breakTime */
  breakTime: number
  /** 对应后端字段 idleTime */
  idleTime: number
  /** 对应后端字段 personnelOperationRate */
  personnelOperationRate: number
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
  /** 对应后端字段 workEfficiency */
  workEfficiency: number
  /** 对应后端字段 idleReasonType */
  idleReasonType?: number
  /** 对应后端字段 idleReason */
  idleReason?: string
  /** 对应后端字段 overtimeHours */
  overtimeHours: number
  /** 对应后端字段 teamLeader */
  teamLeader?: string
  /** 对应后端字段 supervisor */
  supervisor?: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PersonnelOperationRateUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPersonnelOperationRateUpdateDto）
 */
export interface PersonnelOperationRateUpdate extends PersonnelOperationRateCreate {
  /** 对应后端字段 personnelOperationRateId */
  personnelOperationRateId: string
}

/**
 * PersonnelOperationRateStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPersonnelOperationRateStatusDto）
 */
export interface PersonnelOperationRateStatus {
  /** 对应后端字段 personnelOperationRateId */
  personnelOperationRateId: string
  /** 对应后端字段 status */
  status: number
}
