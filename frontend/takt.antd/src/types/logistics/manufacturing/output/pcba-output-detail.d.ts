// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/output/pcba-output-detail
// 文件名称：pcba-output-detail.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：pcba-output-detail相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PcbaOutputDetail类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPcbaOutputDetailDto）
 */
export interface PcbaOutputDetail extends TaktEntityBase {
  /** 对应后端字段 pcbaOutputDetailId */
  pcbaOutputDetailId: string
  /** 对应后端字段 pcbaOutputId */
  pcbaOutputId: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 timePeriod */
  timePeriod: string
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 pcbBoardType */
  pcbBoardType: string
  /** 对应后端字段 panelSide */
  panelSide: string
  /** 对应后端字段 batchQty */
  batchQty: number
  /** 对应后端字段 dailyCompletedQty */
  dailyCompletedQty: number
  /** 对应后端字段 totalCompletedQty */
  totalCompletedQty: number
  /** 对应后端字段 completedStatus */
  completedStatus: number
  /** 对应后端字段 serialNo */
  serialNo: string
  /** 对应后端字段 defectCount */
  defectCount: number
  /** 对应后端字段 inputMinutes */
  inputMinutes: number
  /** 对应后端字段 repairMinutes */
  repairMinutes: number
  /** 对应后端字段 switchCount */
  switchCount: number
  /** 对应后端字段 switchTime */
  switchTime: number
  /** 对应后端字段 stopTime */
  stopTime: number
  /** 对应后端字段 totalMinutes */
  totalMinutes: number
  /** 对应后端字段 unachievedReason */
  unachievedReason?: string
  /** 对应后端字段 unachievedDescription */
  unachievedDescription?: string
  /** 对应后端字段 pcbaOutput */
  pcbaOutput?: unknown
}

/**
 * PcbaOutputDetailQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPcbaOutputDetailQueryDto）
 */
export interface PcbaOutputDetailQuery extends TaktPagedQuery {
  /** 对应后端字段 pcbaOutputId */
  pcbaOutputId?: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 timePeriod */
  timePeriod?: string
  /** 对应后端字段 shiftNo */
  shiftNo?: number
  /** 对应后端字段 pcbBoardType */
  pcbBoardType?: string
  /** 对应后端字段 panelSide */
  panelSide?: string
  /** 对应后端字段 batchQty */
  batchQty?: number
  /** 对应后端字段 dailyCompletedQty */
  dailyCompletedQty?: number
  /** 对应后端字段 totalCompletedQty */
  totalCompletedQty?: number
  /** 对应后端字段 completedStatus */
  completedStatus?: number
  /** 对应后端字段 serialNo */
  serialNo?: string
  /** 对应后端字段 defectCount */
  defectCount?: number
  /** 对应后端字段 inputMinutes */
  inputMinutes?: number
  /** 对应后端字段 repairMinutes */
  repairMinutes?: number
  /** 对应后端字段 switchCount */
  switchCount?: number
  /** 对应后端字段 switchTime */
  switchTime?: number
  /** 对应后端字段 stopTime */
  stopTime?: number
  /** 对应后端字段 totalMinutes */
  totalMinutes?: number
  /** 对应后端字段 unachievedReason */
  unachievedReason?: string
  /** 对应后端字段 unachievedDescription */
  unachievedDescription?: string
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
 * PcbaOutputDetailCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPcbaOutputDetailCreateDto）
 */
export interface PcbaOutputDetailCreate {
  /** 对应后端字段 pcbaOutputId */
  pcbaOutputId: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 timePeriod */
  timePeriod: string
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 pcbBoardType */
  pcbBoardType: string
  /** 对应后端字段 panelSide */
  panelSide: string
  /** 对应后端字段 batchQty */
  batchQty: number
  /** 对应后端字段 dailyCompletedQty */
  dailyCompletedQty: number
  /** 对应后端字段 totalCompletedQty */
  totalCompletedQty: number
  /** 对应后端字段 completedStatus */
  completedStatus: number
  /** 对应后端字段 serialNo */
  serialNo: string
  /** 对应后端字段 defectCount */
  defectCount: number
  /** 对应后端字段 inputMinutes */
  inputMinutes: number
  /** 对应后端字段 repairMinutes */
  repairMinutes: number
  /** 对应后端字段 switchCount */
  switchCount: number
  /** 对应后端字段 switchTime */
  switchTime: number
  /** 对应后端字段 stopTime */
  stopTime: number
  /** 对应后端字段 totalMinutes */
  totalMinutes: number
  /** 对应后端字段 unachievedReason */
  unachievedReason?: string
  /** 对应后端字段 unachievedDescription */
  unachievedDescription?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PcbaOutputDetailUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPcbaOutputDetailUpdateDto）
 */
export interface PcbaOutputDetailUpdate extends PcbaOutputDetailCreate {
  /** 对应后端字段 pcbaOutputDetailId */
  pcbaOutputDetailId: string
}

/**
 * PcbaOutputDetailCompletedStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPcbaOutputDetailCompletedStatusDto）
 */
export interface PcbaOutputDetailCompletedStatus {
  /** 对应后端字段 pcbaOutputDetailId */
  pcbaOutputDetailId: string
  /** 对应后端字段 completedStatus */
  completedStatus: number
}
