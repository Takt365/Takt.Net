// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/purchase-order-change-log
// 文件名称：purchase-order-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase-order-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchaseOrderChangeLog类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderChangeLogDto）
 */
export interface PurchaseOrderChangeLog extends TaktEntityBase {
  /** 对应后端字段 purchaseOrderChangeLogId */
  purchaseOrderChangeLogId: string
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId: string
  /** 对应后端字段 orderCode */
  orderCode: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
}

/**
 * PurchaseOrderChangeLogQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderChangeLogQueryDto）
 */
export interface PurchaseOrderChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId?: string
  /** 对应后端字段 orderCode */
  orderCode?: string
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
 * PurchaseOrderChangeLogCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderChangeLogCreateDto）
 */
export interface PurchaseOrderChangeLogCreate {
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId: string
  /** 对应后端字段 orderCode */
  orderCode: string
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
 * PurchaseOrderChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderChangeLogUpdateDto）
 */
export interface PurchaseOrderChangeLogUpdate extends PurchaseOrderChangeLogCreate {
  /** 对应后端字段 purchaseOrderChangeLogId */
  purchaseOrderChangeLogId: string
}
