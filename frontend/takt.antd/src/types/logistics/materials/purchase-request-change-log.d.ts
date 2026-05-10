// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/purchase-request-change-log
// 文件名称：purchase-request-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase-request-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchaseRequestChangeLog类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestChangeLogDto）
 */
export interface PurchaseRequestChangeLog extends TaktEntityBase {
  /** 对应后端字段 purchaseRequestChangeLogId */
  purchaseRequestChangeLogId: string
  /** 对应后端字段 purchaseRequestId */
  purchaseRequestId: string
  /** 对应后端字段 requestCode */
  requestCode: string
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
 * PurchaseRequestChangeLogQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestChangeLogQueryDto）
 */
export interface PurchaseRequestChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 purchaseRequestId */
  purchaseRequestId?: string
  /** 对应后端字段 requestCode */
  requestCode?: string
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
 * PurchaseRequestChangeLogCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestChangeLogCreateDto）
 */
export interface PurchaseRequestChangeLogCreate {
  /** 对应后端字段 purchaseRequestId */
  purchaseRequestId: string
  /** 对应后端字段 requestCode */
  requestCode: string
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
 * PurchaseRequestChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestChangeLogUpdateDto）
 */
export interface PurchaseRequestChangeLogUpdate extends PurchaseRequestChangeLogCreate {
  /** 对应后端字段 purchaseRequestChangeLogId */
  purchaseRequestChangeLogId: string
}
