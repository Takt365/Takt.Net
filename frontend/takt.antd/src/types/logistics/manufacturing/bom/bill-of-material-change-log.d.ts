// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/bom/bill-of-material-change-log
// 文件名称：bill-of-material-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：bill-of-material-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * BillOfMaterialChangeLog类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialChangeLogDto）
 */
export interface BillOfMaterialChangeLog extends TaktEntityBase {
  /** 对应后端字段 billOfMaterialChangeLogId */
  billOfMaterialChangeLogId: string
  /** 对应后端字段 billOfMaterialId */
  billOfMaterialId: string
  /** 对应后端字段 bomCode */
  bomCode: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 bom */
  bom?: unknown
}

/**
 * BillOfMaterialChangeLogQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialChangeLogQueryDto）
 */
export interface BillOfMaterialChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 billOfMaterialId */
  billOfMaterialId?: string
  /** 对应后端字段 bomCode */
  bomCode?: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeTime */
  changeTime?: string
  /** 对应后端字段 changeTimeStart */
  changeTimeStart?: string
  /** 对应后端字段 changeTimeEnd */
  changeTimeEnd?: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
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
 * BillOfMaterialChangeLogCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialChangeLogCreateDto）
 */
export interface BillOfMaterialChangeLogCreate {
  /** 对应后端字段 billOfMaterialId */
  billOfMaterialId: string
  /** 对应后端字段 bomCode */
  bomCode: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * BillOfMaterialChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialChangeLogUpdateDto）
 */
export interface BillOfMaterialChangeLogUpdate extends BillOfMaterialChangeLogCreate {
  /** 对应后端字段 billOfMaterialChangeLogId */
  billOfMaterialChangeLogId: string
}
