// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/specific-engine/purchase
// 文件名称：purchase.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchaseOrder类型（对应后端 Takt.Application.Dtos.Logistics.Materials.SpecificEngine.TaktPurchaseOrderDto）
 */
export interface PurchaseOrder {
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 orderCode */
  orderCode: string
  /** 对应后端字段 requestId */
  requestId?: string
  /** 对应后端字段 requestCode */
  requestCode?: string
  /** 对应后端字段 supplierId */
  supplierId: string
  /** 对应后端字段 supplierCode */
  supplierCode: string
  /** 对应后端字段 supplierName */
  supplierName: string
  /** 对应后端字段 supplierContact */
  supplierContact?: string
  /** 对应后端字段 supplierPhone */
  supplierPhone?: string
  /** 对应后端字段 supplierAddress */
  supplierAddress?: string
  /** 对应后端字段 orderDate */
  orderDate: string
  /** 对应后端字段 requiredArrivalDate */
  requiredArrivalDate?: string
  /** 对应后端字段 actualArrivalDate */
  actualArrivalDate?: string
  /** 对应后端字段 purchaseUserId */
  purchaseUserId: string
  /** 对应后端字段 purchaseUserName */
  purchaseUserName: string
  /** 对应后端字段 purchaseDeptId */
  purchaseDeptId?: string
  /** 对应后端字段 purchaseDeptName */
  purchaseDeptName?: string
  /** 对应后端字段 totalQuantity */
  totalQuantity: number
  /** 对应后端字段 totalAmount */
  totalAmount: number
  /** 对应后端字段 discountAmount */
  discountAmount: number
  /** 对应后端字段 taxAmount */
  taxAmount: number
  /** 对应后端字段 actualAmount */
  actualAmount: number
  /** 对应后端字段 receivedQuantity */
  receivedQuantity: number
  /** 对应后端字段 receivedAmount */
  receivedAmount: number
  /** 对应后端字段 paidAmount */
  paidAmount: number
  /** 对应后端字段 orderStatus */
  orderStatus: number
  /** 对应后端字段 paymentStatus */
  paymentStatus: number
  /** 对应后端字段 paymentMethod */
  paymentMethod: number
  /** 对应后端字段 deliveryMethod */
  deliveryMethod: number
  /** 对应后端字段 deliveryAddress */
  deliveryAddress?: string
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * PurchaseOrderCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.SpecificEngine.TaktPurchaseOrderCreateDto）
 */
export interface PurchaseOrderCreate {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 orderCode */
  orderCode: string
  /** 对应后端字段 requestId */
  requestId?: string
  /** 对应后端字段 requestCode */
  requestCode?: string
  /** 对应后端字段 supplierId */
  supplierId: string
  /** 对应后端字段 supplierCode */
  supplierCode: string
  /** 对应后端字段 supplierName */
  supplierName: string
  /** 对应后端字段 supplierContact */
  supplierContact?: string
  /** 对应后端字段 supplierPhone */
  supplierPhone?: string
  /** 对应后端字段 supplierAddress */
  supplierAddress?: string
  /** 对应后端字段 orderDate */
  orderDate: string
  /** 对应后端字段 requiredArrivalDate */
  requiredArrivalDate?: string
  /** 对应后端字段 actualArrivalDate */
  actualArrivalDate?: string
  /** 对应后端字段 purchaseUserId */
  purchaseUserId: string
  /** 对应后端字段 purchaseUserName */
  purchaseUserName: string
  /** 对应后端字段 purchaseDeptId */
  purchaseDeptId?: string
  /** 对应后端字段 purchaseDeptName */
  purchaseDeptName?: string
  /** 对应后端字段 totalQuantity */
  totalQuantity: number
  /** 对应后端字段 totalAmount */
  totalAmount: number
  /** 对应后端字段 discountAmount */
  discountAmount: number
  /** 对应后端字段 taxAmount */
  taxAmount: number
  /** 对应后端字段 actualAmount */
  actualAmount: number
  /** 对应后端字段 receivedQuantity */
  receivedQuantity: number
  /** 对应后端字段 receivedAmount */
  receivedAmount: number
  /** 对应后端字段 paidAmount */
  paidAmount: number
  /** 对应后端字段 orderStatus */
  orderStatus: number
  /** 对应后端字段 paymentStatus */
  paymentStatus: number
  /** 对应后端字段 paymentMethod */
  paymentMethod: number
  /** 对应后端字段 deliveryMethod */
  deliveryMethod: number
  /** 对应后端字段 deliveryAddress */
  deliveryAddress?: string
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
 * PurchaseOrderStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.SpecificEngine.TaktPurchaseOrderStatusDto）
 */
export interface PurchaseOrderStatus {
  /** 对应后端字段 orderId */
  orderId?: string
}

/**
 * PurchaseOrderUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.SpecificEngine.TaktPurchaseOrderUpdateDto）
 */
export interface PurchaseOrderUpdate extends PurchaseOrderCreate {
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId: string
}

/**
 * PurchasePrice类型（对应后端 Takt.Application.Dtos.Logistics.Materials.SpecificEngine.TaktPurchasePriceDto）
 */
export interface PurchasePrice {
  /** 对应后端字段 purchasePriceId */
  purchasePriceId: string
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
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * PurchasePriceCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.SpecificEngine.TaktPurchasePriceCreateDto）
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
  /** 对应后端字段 priceStatus */
  priceStatus: number
  /** 对应后端字段 isEnabled */
  isEnabled: number
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
 * PurchasePriceItem类型（对应后端 Takt.Application.Dtos.Logistics.Materials.SpecificEngine.TaktPurchasePriceItemDto）
 */
export interface PurchasePriceItem {
  /** 对应后端字段 purchasePriceItemId */
  purchasePriceItemId: string
  /** 对应后端字段 priceId */
  priceId: string
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
 * PurchasePriceStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.SpecificEngine.TaktPurchasePriceStatusDto）
 */
export interface PurchasePriceStatus {
  /** 对应后端字段 priceId */
  priceId?: string
}

/**
 * PurchasePriceUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.SpecificEngine.TaktPurchasePriceUpdateDto）
 */
export interface PurchasePriceUpdate extends PurchasePriceCreate {
  /** 对应后端字段 purchasePriceId */
  purchasePriceId: string
}
