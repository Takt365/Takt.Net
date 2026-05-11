// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/purchase-price-item
// 文件名称：purchase-price-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase-price-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchasePriceItem类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceItemDto）
 */
export interface PurchasePriceItem extends TaktEntityBase {
  /** 对应后端字段 purchasePriceItemId */
  purchasePriceItemId: string
  /** 对应后端字段 purchasePriceId */
  purchasePriceId: string
  /** 对应后端字段 purchasePriceCode */
  purchasePriceCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 purchaseUnit */
  purchaseUnit: string
  /** 对应后端字段 purchasePrice */
  purchasePrice: number
  /** 对应后端字段 minPurchaseQuantity */
  minPurchaseQuantity: number
  /** 对应后端字段 maxPurchaseQuantity */
  maxPurchaseQuantity: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 scales */
  scales?: unknown[]
}

/**
 * PurchasePriceItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceItemQueryDto）
 */
export interface PurchasePriceItemQuery extends TaktPagedQuery {
  /** 对应后端字段 purchasePriceId */
  purchasePriceId?: string
  /** 对应后端字段 purchasePriceCode */
  purchasePriceCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 purchaseUnit */
  purchaseUnit?: string
  /** 对应后端字段 purchasePrice */
  purchasePrice?: number
  /** 对应后端字段 minPurchaseQuantity */
  minPurchaseQuantity?: number
  /** 对应后端字段 maxPurchaseQuantity */
  maxPurchaseQuantity?: number
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
 * PurchasePriceItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceItemCreateDto）
 */
export interface PurchasePriceItemCreate {
  /** 对应后端字段 purchasePriceId */
  purchasePriceId: string
  /** 对应后端字段 purchasePriceCode */
  purchasePriceCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 purchaseUnit */
  purchaseUnit: string
  /** 对应后端字段 purchasePrice */
  purchasePrice: number
  /** 对应后端字段 minPurchaseQuantity */
  minPurchaseQuantity: number
  /** 对应后端字段 maxPurchaseQuantity */
  maxPurchaseQuantity: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 scales */
  scales?: unknown[]
}

/**
 * PurchasePriceItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceItemUpdateDto）
 */
export interface PurchasePriceItemUpdate extends PurchasePriceItemCreate {
  /** 对应后端字段 purchasePriceItemId */
  purchasePriceItemId: string
}

/**
 * PurchasePriceItemSort类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceItemSortDto）
 */
export interface PurchasePriceItemSort {
  /** 对应后端字段 purchasePriceItemId */
  purchasePriceItemId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
