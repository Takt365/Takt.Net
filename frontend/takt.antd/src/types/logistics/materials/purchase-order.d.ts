// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/purchase-order
// 文件名称：purchase-order.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase-order相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchaseOrder类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderDto）
 */
export interface PurchaseOrder extends TaktEntityBase {
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 purchaseOrderCode */
  purchaseOrderCode: string
  /** 对应后端字段 supplierCode */
  supplierCode: string
  /** 对应后端字段 supplierName */
  supplierName: string
  /** 对应后端字段 orderDate */
  orderDate: string
  /** 对应后端字段 requiredArrivalDate */
  requiredArrivalDate?: string
  /** 对应后端字段 actualArrivalDate */
  actualArrivalDate?: string
  /** 对应后端字段 purchaseGroup */
  purchaseGroup?: string
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
  /** 对应后端字段 deliveryStatus */
  deliveryStatus: number
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
 * PurchaseOrderQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderQueryDto）
 */
export interface PurchaseOrderQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 purchaseOrderCode */
  purchaseOrderCode?: string
  /** 对应后端字段 supplierCode */
  supplierCode?: string
  /** 对应后端字段 supplierName */
  supplierName?: string
  /** 对应后端字段 orderDate */
  orderDate?: string
  /** 对应后端字段 orderDateStart */
  orderDateStart?: string
  /** 对应后端字段 orderDateEnd */
  orderDateEnd?: string
  /** 对应后端字段 requiredArrivalDate */
  requiredArrivalDate?: string
  /** 对应后端字段 requiredArrivalDateStart */
  requiredArrivalDateStart?: string
  /** 对应后端字段 requiredArrivalDateEnd */
  requiredArrivalDateEnd?: string
  /** 对应后端字段 actualArrivalDate */
  actualArrivalDate?: string
  /** 对应后端字段 actualArrivalDateStart */
  actualArrivalDateStart?: string
  /** 对应后端字段 actualArrivalDateEnd */
  actualArrivalDateEnd?: string
  /** 对应后端字段 purchaseGroup */
  purchaseGroup?: string
  /** 对应后端字段 totalQuantity */
  totalQuantity?: number
  /** 对应后端字段 totalAmount */
  totalAmount?: number
  /** 对应后端字段 discountAmount */
  discountAmount?: number
  /** 对应后端字段 taxAmount */
  taxAmount?: number
  /** 对应后端字段 actualAmount */
  actualAmount?: number
  /** 对应后端字段 receivedQuantity */
  receivedQuantity?: number
  /** 对应后端字段 receivedAmount */
  receivedAmount?: number
  /** 对应后端字段 paidAmount */
  paidAmount?: number
  /** 对应后端字段 orderStatus */
  orderStatus?: number
  /** 对应后端字段 deliveryStatus */
  deliveryStatus?: number
  /** 对应后端字段 paymentMethod */
  paymentMethod?: number
  /** 对应后端字段 deliveryMethod */
  deliveryMethod?: number
  /** 对应后端字段 deliveryAddress */
  deliveryAddress?: string
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
 * PurchaseOrderCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderCreateDto）
 */
export interface PurchaseOrderCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 purchaseOrderCode */
  purchaseOrderCode: string
  /** 对应后端字段 supplierCode */
  supplierCode: string
  /** 对应后端字段 supplierName */
  supplierName: string
  /** 对应后端字段 orderDate */
  orderDate: string
  /** 对应后端字段 requiredArrivalDate */
  requiredArrivalDate?: string
  /** 对应后端字段 actualArrivalDate */
  actualArrivalDate?: string
  /** 对应后端字段 purchaseGroup */
  purchaseGroup?: string
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
  /** 对应后端字段 deliveryStatus */
  deliveryStatus: number
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
 * PurchaseOrderUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderUpdateDto）
 */
export interface PurchaseOrderUpdate extends PurchaseOrderCreate {
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId: string
}

/**
 * PurchaseOrderDeliveryStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderDeliveryStatusDto）
 */
export interface PurchaseOrderDeliveryStatus {
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId: string
  /** 对应后端字段 deliveryStatus */
  deliveryStatus: number
}

/**
 * PurchaseOrderOrderStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderOrderStatusDto）
 */
export interface PurchaseOrderOrderStatus {
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId: string
  /** 对应后端字段 orderStatus */
  orderStatus: number
}
