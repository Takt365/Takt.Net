// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/purchase-specific
// 文件名称：purchase-specific.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase-specific相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchaseOrder类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderDto）
 */
export interface PurchaseOrder {
  /** 对应后端字段 orderId */
  orderId?: string
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * PurchaseOrderCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderCreateDto）
 */
export interface PurchaseOrderCreate {
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * PurchaseOrderStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderStatusDto）
 */
export interface PurchaseOrderStatus {
  /** 对应后端字段 orderId */
  orderId?: string
}

/**
 * PurchaseOrderUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderUpdateDto）
 */
export interface PurchaseOrderUpdate extends PurchaseOrderCreate {
  /** 对应后端字段 orderId */
  orderId?: string
}

/**
 * PurchasePrice类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceDto）
 */
export interface PurchasePrice {
  /** 对应后端字段 priceId */
  priceId?: string
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * PurchasePriceCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceCreateDto）
 */
export interface PurchasePriceCreate {
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * PurchasePriceItem类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceItemDto）
 */
export interface PurchasePriceItem {
  /** 对应后端字段 itemId */
  itemId?: string
  /** 对应后端字段 scales */
  scales?: unknown[]
}

/**
 * PurchasePriceStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceStatusDto）
 */
export interface PurchasePriceStatus {
  /** 对应后端字段 priceId */
  priceId?: string
}

/**
 * PurchasePriceUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceUpdateDto）
 */
export interface PurchasePriceUpdate extends PurchasePriceCreate {
  /** 对应后端字段 priceId */
  priceId?: string
}
