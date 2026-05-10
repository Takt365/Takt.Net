// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/purchase-price-change-log
// 文件名称：purchase-price-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase-price-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchasePriceChangeLog类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceChangeLogDto）
 */
export interface PurchasePriceChangeLog extends TaktEntityBase {
  /** 对应后端字段 purchasePriceChangeLogId */
  purchasePriceChangeLogId: string
  /** 对应后端字段 purchasePriceId */
  purchasePriceId: string
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
 * PurchasePriceChangeLogQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceChangeLogQueryDto）
 */
export interface PurchasePriceChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 purchasePriceId */
  purchasePriceId?: string
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
 * PurchasePriceChangeLogCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceChangeLogCreateDto）
 */
export interface PurchasePriceChangeLogCreate {
  /** 对应后端字段 purchasePriceId */
  purchasePriceId: string
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
 * PurchasePriceChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceChangeLogUpdateDto）
 */
export interface PurchasePriceChangeLogUpdate extends PurchasePriceChangeLogCreate {
  /** 对应后端字段 purchasePriceChangeLogId */
  purchasePriceChangeLogId: string
}
