// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/defect/pcba-repair
// 文件名称：pcba-repair.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：pcba-repair相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PcbaRepair类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaRepairDto）
 */
export interface PcbaRepair extends TaktEntityBase {
  /** 对应后端字段 pcbaRepairId */
  pcbaRepairId: string
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
  /** 对应后端字段 prodOrderQty */
  prodOrderQty: number
  /** 对应后端字段 modelCode */
  modelCode: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 pcbaRepairDetails */
  pcbaRepairDetails?: unknown[]
}

/**
 * PcbaRepairQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaRepairQueryDto）
 */
export interface PcbaRepairQuery extends TaktPagedQuery {
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
  /** 对应后端字段 prodOrderQty */
  prodOrderQty?: number
  /** 对应后端字段 modelCode */
  modelCode?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 materialCode */
  materialCode?: string
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
 * PcbaRepairCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaRepairCreateDto）
 */
export interface PcbaRepairCreate {
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
  /** 对应后端字段 prodOrderQty */
  prodOrderQty: number
  /** 对应后端字段 modelCode */
  modelCode: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 pcbaRepairDetails */
  pcbaRepairDetails?: unknown[]
}

/**
 * PcbaRepairUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaRepairUpdateDto）
 */
export interface PcbaRepairUpdate extends PcbaRepairCreate {
  /** 对应后端字段 pcbaRepairId */
  pcbaRepairId: string
}

/**
 * PcbaRepairStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaRepairStatusDto）
 */
export interface PcbaRepairStatus {
  /** 对应后端字段 pcbaRepairId */
  pcbaRepairId: string
  /** 对应后端字段 status */
  status: number
}
