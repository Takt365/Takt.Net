// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/output/pcba-output
// 文件名称：pcba-output.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：pcba-output相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PcbaOutput类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPcbaOutputDto）
 */
export interface PcbaOutput extends TaktEntityBase {
  /** 对应后端字段 pcbaOutputId */
  pcbaOutputId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 prodCategory */
  prodCategory: string
  /** 对应后端字段 prodDate */
  prodDate: string
  /** 对应后端字段 prodLine */
  prodLine: string
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 modelCode */
  modelCode: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 prodOrderQty */
  prodOrderQty: number
  /** 对应后端字段 stdMinutes */
  stdMinutes: number
  /** 对应后端字段 stdShorts */
  stdShorts: number
  /** 对应后端字段 stdCapacity */
  stdCapacity: number
  /** 对应后端字段 pcbaOutputDetails */
  pcbaOutputDetails?: unknown[]
}

/**
 * PcbaOutputQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPcbaOutputQueryDto）
 */
export interface PcbaOutputQuery extends TaktPagedQuery {
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
  /** 对应后端字段 shiftNo */
  shiftNo?: number
  /** 对应后端字段 prodOrderCode */
  prodOrderCode?: string
  /** 对应后端字段 modelCode */
  modelCode?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 prodOrderQty */
  prodOrderQty?: number
  /** 对应后端字段 stdMinutes */
  stdMinutes?: number
  /** 对应后端字段 stdShorts */
  stdShorts?: number
  /** 对应后端字段 stdCapacity */
  stdCapacity?: number
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
 * PcbaOutputCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPcbaOutputCreateDto）
 */
export interface PcbaOutputCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 prodCategory */
  prodCategory: string
  /** 对应后端字段 prodDate */
  prodDate: string
  /** 对应后端字段 prodLine */
  prodLine: string
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 modelCode */
  modelCode: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 prodOrderQty */
  prodOrderQty: number
  /** 对应后端字段 stdMinutes */
  stdMinutes: number
  /** 对应后端字段 stdShorts */
  stdShorts: number
  /** 对应后端字段 stdCapacity */
  stdCapacity: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 pcbaOutputDetails */
  pcbaOutputDetails?: unknown[]
}

/**
 * PcbaOutputUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktPcbaOutputUpdateDto）
 */
export interface PcbaOutputUpdate extends PcbaOutputCreate {
  /** 对应后端字段 pcbaOutputId */
  pcbaOutputId: string
}
