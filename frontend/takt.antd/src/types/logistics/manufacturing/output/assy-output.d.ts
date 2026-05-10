// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/output/assy-output
// 文件名称：assy-output.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：assy-output相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AssyOutput类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktAssyOutputDto）
 */
export interface AssyOutput extends TaktEntityBase {
  /** 对应后端字段 assyOutputId */
  assyOutputId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 prodCategory */
  prodCategory: string
  /** 对应后端字段 prodDate */
  prodDate: string
  /** 对应后端字段 prodLine */
  prodLine: string
  /** 对应后端字段 directLabor */
  directLabor: number
  /** 对应后端字段 indirectLabor */
  indirectLabor: number
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 prodOrderType */
  prodOrderType?: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 modelCode */
  modelCode: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 prodOrderQty */
  prodOrderQty: number
  /** 对应后端字段 stdMinutes */
  stdMinutes: number
  /** 对应后端字段 stdCapacity */
  stdCapacity: number
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 assyOutputDetails */
  assyOutputDetails?: unknown[]
}

/**
 * AssyOutputQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktAssyOutputQueryDto）
 */
export interface AssyOutputQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 prodCategory */
  prodCategory?: string
  /** 对应后端字段 prodDate */
  prodDate?: string
  /** 对应后端字段 prodDateStart */
  prodDateStart?: string
  /** 对应后端字段 prodDateEnd */
  prodDateEnd?: string
  /** 对应后端字段 prodLine */
  prodLine?: string
  /** 对应后端字段 directLabor */
  directLabor?: number
  /** 对应后端字段 indirectLabor */
  indirectLabor?: number
  /** 对应后端字段 shiftNo */
  shiftNo?: number
  /** 对应后端字段 prodOrderType */
  prodOrderType?: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode?: string
  /** 对应后端字段 modelCode */
  modelCode?: string
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 prodOrderQty */
  prodOrderQty?: number
  /** 对应后端字段 stdMinutes */
  stdMinutes?: number
  /** 对应后端字段 stdCapacity */
  stdCapacity?: number
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
 * AssyOutputCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktAssyOutputCreateDto）
 */
export interface AssyOutputCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 prodCategory */
  prodCategory: string
  /** 对应后端字段 prodDate */
  prodDate: string
  /** 对应后端字段 prodLine */
  prodLine: string
  /** 对应后端字段 directLabor */
  directLabor: number
  /** 对应后端字段 indirectLabor */
  indirectLabor: number
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 prodOrderType */
  prodOrderType?: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 modelCode */
  modelCode: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 prodOrderQty */
  prodOrderQty: number
  /** 对应后端字段 stdMinutes */
  stdMinutes: number
  /** 对应后端字段 stdCapacity */
  stdCapacity: number
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 assyOutputDetails */
  assyOutputDetails?: unknown[]
}

/**
 * AssyOutputUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktAssyOutputUpdateDto）
 */
export interface AssyOutputUpdate extends AssyOutputCreate {
  /** 对应后端字段 assyOutputId */
  assyOutputId: string
}

/**
 * AssyOutputStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktAssyOutputStatusDto）
 */
export interface AssyOutputStatus {
  /** 对应后端字段 assyOutputId */
  assyOutputId: string
  /** 对应后端字段 status */
  status: number
}
