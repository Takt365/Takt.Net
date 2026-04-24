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
  /** 对应后端字段 priceId */
  priceId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 supplierCode */
  supplierCode: string
  /** 对应后端字段 priceType */
  priceType: number
  /** 对应后端字段 effectiveFrom */
  effectiveFrom: string
  /** 对应后端字段 effectiveTo */
  effectiveTo?: string
  /** 对应后端字段 priceStatus */
  priceStatus: number
  /** 对应后端字段 isEnabled */
  isEnabled: number
  /** 对应后端字段 items */
  items: unknown[]
}

/**
 * PurchasePriceQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceQueryDto）
 */
export interface PurchasePriceQuery extends TaktPagedQuery {
  /** 对应后端字段 supplierCode */
  supplierCode?: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 priceType */
  priceType?: number
  /** 对应后端字段 priceStatus */
  priceStatus?: number
  /** 对应后端字段 isEnabled */
  isEnabled?: number
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
  /** 对应后端字段 effectiveFrom */
  effectiveFrom: string
  /** 对应后端字段 effectiveTo */
  effectiveTo?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 items */
  items: unknown[]
}

/**
 * PurchasePriceUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceUpdateDto）
 */
export interface PurchasePriceUpdate extends PurchasePriceCreate {
  /** 对应后端字段 priceId */
  priceId: string
}

/**
 * PurchasePriceStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceStatusDto）
 */
export interface PurchasePriceStatus {
  /** 对应后端字段 priceId */
  priceId: string
  /** 对应后端字段 priceStatus */
  priceStatus: number
}

/**
 * PurchasePriceItem类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceItemDto）
 */
export interface PurchasePriceItem {
  /** 对应后端字段 itemId */
  itemId: string
  /** 对应后端字段 priceId */
  priceId: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 purchaseUnit */
  purchaseUnit: string
  /** 对应后端字段 purchasePrice */
  purchasePrice: number
  /** 对应后端字段 minPurchaseQuantity */
  minPurchaseQuantity: number
  /** 对应后端字段 maxPurchaseQuantity */
  maxPurchaseQuantity: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 scales */
  scales: unknown[]
}

/**
 * PurchasePriceItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceItemCreateDto）
 */
export interface PurchasePriceItemCreate {
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 purchaseUnit */
  purchaseUnit: string
  /** 对应后端字段 purchasePrice */
  purchasePrice: number
  /** 对应后端字段 minPurchaseQuantity */
  minPurchaseQuantity: number
  /** 对应后端字段 maxPurchaseQuantity */
  maxPurchaseQuantity: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 scales */
  scales: unknown[]
}

/**
 * PurchasePriceScale类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceScaleDto）
 */
export interface PurchasePriceScale {
  /** 对应后端字段 scaleId */
  scaleId: string
  /** 对应后端字段 itemId */
  itemId: string
  /** 对应后端字段 startQuantity */
  startQuantity: number
  /** 对应后端字段 endQuantity */
  endQuantity: number
  /** 对应后端字段 scalePrice */
  scalePrice: number
  /** 对应后端字段 orderNum */
  orderNum: number
}

/**
 * PurchasePriceScaleCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceScaleCreateDto）
 */
export interface PurchasePriceScaleCreate {
  /** 对应后端字段 startQuantity */
  startQuantity: number
  /** 对应后端字段 endQuantity */
  endQuantity: number
  /** 对应后端字段 scalePrice */
  scalePrice: number
  /** 对应后端字段 orderNum */
  orderNum: number
}
