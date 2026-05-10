// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/purchase-price
// 文件名称：purchase-price.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase-price相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchasePrice类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceDto）
 */
export interface PurchasePrice extends TaktEntityBase {
  /** 对应后端字段 purchasePriceId */
  purchasePriceId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 supplierCode */
  supplierCode: string
  /** 对应后端字段 priceType */
  priceType: number
  /** 对应后端字段 effectiveStartDate */
  effectiveStartDate: string
  /** 对应后端字段 effectiveEndDate */
  effectiveEndDate?: string
  /** 对应后端字段 priceStatus */
  priceStatus: number
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * PurchasePriceQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceQueryDto）
 */
export interface PurchasePriceQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 supplierCode */
  supplierCode?: string
  /** 对应后端字段 priceType */
  priceType?: number
  /** 对应后端字段 effectiveStartDate */
  effectiveStartDate?: string
  /** 对应后端字段 effectiveStartDateStart */
  effectiveStartDateStart?: string
  /** 对应后端字段 effectiveStartDateEnd */
  effectiveStartDateEnd?: string
  /** 对应后端字段 effectiveEndDate */
  effectiveEndDate?: string
  /** 对应后端字段 effectiveEndDateStart */
  effectiveEndDateStart?: string
  /** 对应后端字段 effectiveEndDateEnd */
  effectiveEndDateEnd?: string
  /** 对应后端字段 priceStatus */
  priceStatus?: number
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
 * PurchasePriceCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceCreateDto）
 */
export interface PurchasePriceCreate {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 supplierCode */
  supplierCode: string
  /** 对应后端字段 priceType */
  priceType: number
  /** 对应后端字段 effectiveStartDate */
  effectiveStartDate: string
  /** 对应后端字段 effectiveEndDate */
  effectiveEndDate?: string
  /** 对应后端字段 priceStatus */
  priceStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * PurchasePriceUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceUpdateDto）
 */
export interface PurchasePriceUpdate extends PurchasePriceCreate {
  /** 对应后端字段 purchasePriceId */
  purchasePriceId: string
}

/**
 * PurchasePricePriceStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePricePriceStatusDto）
 */
export interface PurchasePricePriceStatus {
  /** 对应后端字段 purchasePriceId */
  purchasePriceId: string
  /** 对应后端字段 priceStatus */
  priceStatus: number
}
