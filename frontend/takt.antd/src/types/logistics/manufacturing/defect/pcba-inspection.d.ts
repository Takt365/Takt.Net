// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/defect/pcba-inspection
// 文件名称：pcba-inspection.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：pcba-inspection相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PcbaInspection类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaInspectionDto）
 */
export interface PcbaInspection extends TaktEntityBase {
  /** 对应后端字段 pcbaInspectionId */
  pcbaInspectionId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 prodCategory */
  prodCategory: string
  /** 对应后端字段 prodDate */
  prodDate: string
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
  /** 对应后端字段 pcbaInspectionDetails */
  pcbaInspectionDetails?: unknown[]
}

/**
 * PcbaInspectionQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaInspectionQueryDto）
 */
export interface PcbaInspectionQuery extends TaktPagedQuery {
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
 * PcbaInspectionCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaInspectionCreateDto）
 */
export interface PcbaInspectionCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 prodCategory */
  prodCategory: string
  /** 对应后端字段 prodDate */
  prodDate: string
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
  /** 对应后端字段 pcbaInspectionDetails */
  pcbaInspectionDetails?: unknown[]
}

/**
 * PcbaInspectionUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaInspectionUpdateDto）
 */
export interface PcbaInspectionUpdate extends PcbaInspectionCreate {
  /** 对应后端字段 pcbaInspectionId */
  pcbaInspectionId: string
}

/**
 * PcbaInspectionStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaInspectionStatusDto）
 */
export interface PcbaInspectionStatus {
  /** 对应后端字段 pcbaInspectionId */
  pcbaInspectionId: string
  /** 对应后端字段 status */
  status: number
}
