// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/defect/assy-defect
// 文件名称：assy-defect.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：assy-defect相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AssyDefect类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktAssyDefectDto）
 */
export interface AssyDefect extends TaktEntityBase {
  /** 对应后端字段 assyDefectId */
  assyDefectId: string
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
  /** 对应后端字段 prodActualQty */
  prodActualQty: number
  /** 对应后端字段 goodQuantity */
  goodQuantity: number
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 assyDefectDetails */
  assyDefectDetails?: unknown[]
}

/**
 * AssyDefectQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktAssyDefectQueryDto）
 */
export interface AssyDefectQuery extends TaktPagedQuery {
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
  /** 对应后端字段 prodActualQty */
  prodActualQty?: number
  /** 对应后端字段 goodQuantity */
  goodQuantity?: number
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
 * AssyDefectCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktAssyDefectCreateDto）
 */
export interface AssyDefectCreate {
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
  /** 对应后端字段 prodActualQty */
  prodActualQty: number
  /** 对应后端字段 goodQuantity */
  goodQuantity: number
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 assyDefectDetails */
  assyDefectDetails?: unknown[]
}

/**
 * AssyDefectUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktAssyDefectUpdateDto）
 */
export interface AssyDefectUpdate extends AssyDefectCreate {
  /** 对应后端字段 assyDefectId */
  assyDefectId: string
}

/**
 * AssyDefectStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktAssyDefectStatusDto）
 */
export interface AssyDefectStatus {
  /** 对应后端字段 assyDefectId */
  assyDefectId: string
  /** 对应后端字段 status */
  status: number
}
