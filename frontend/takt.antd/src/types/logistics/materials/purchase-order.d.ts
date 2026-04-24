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
  /** 对应后端字段 orderId */
  orderId: string
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
  items: unknown[]
}

/**
 * PurchaseOrderQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderQueryDto）
 */
export interface PurchaseOrderQuery extends TaktPagedQuery {
  /** 对应后端字段 orderCode */
  orderCode?: string
  /** 对应后端字段 supplierCode */
  supplierCode?: string
  /** 对应后端字段 supplierName */
  supplierName?: string
  /** 对应后端字段 requestId */
  requestId?: string
  /** 对应后端字段 purchaseUserId */
  purchaseUserId?: string
  /** 对应后端字段 orderStatus */
  orderStatus?: number
  /** 对应后端字段 paymentStatus */
  paymentStatus?: number
  /** 对应后端字段 orderDateStart */
  orderDateStart?: string
  /** 对应后端字段 orderDateEnd */
  orderDateEnd?: string
}

/**
 * PurchaseOrderCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderCreateDto）
 */
export interface PurchaseOrderCreate {
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
  /** 对应后端字段 purchaseUserId */
  purchaseUserId: string
  /** 对应后端字段 purchaseUserName */
  purchaseUserName: string
  /** 对应后端字段 purchaseDeptId */
  purchaseDeptId?: string
  /** 对应后端字段 purchaseDeptName */
  purchaseDeptName?: string
  /** 对应后端字段 paymentMethod */
  paymentMethod: number
  /** 对应后端字段 deliveryMethod */
  deliveryMethod: number
  /** 对应后端字段 deliveryAddress */
  deliveryAddress?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 items */
  items: unknown[]
}

/**
 * PurchaseOrderUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderUpdateDto）
 */
export interface PurchaseOrderUpdate extends PurchaseOrderCreate {
  /** 对应后端字段 orderId */
  orderId: string
}

/**
 * PurchaseOrderStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderStatusDto）
 */
export interface PurchaseOrderStatus {
  /** 对应后端字段 orderId */
  orderId: string
  /** 对应后端字段 orderStatus */
  orderStatus: number
}

/**
 * PurchaseOrderItem类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderItemDto）
 */
export interface PurchaseOrderItem {
  /** 对应后端字段 itemId */
  itemId: string
  /** 对应后端字段 orderId */
  orderId: string
  /** 对应后端字段 orderCode */
  orderCode: string
  /** 对应后端字段 materialId */
  materialId: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 purchaseUnit */
  purchaseUnit: string
  /** 对应后端字段 orderQuantity */
  orderQuantity: number
  /** 对应后端字段 receivedQuantity */
  receivedQuantity: number
  /** 对应后端字段 unitPrice */
  unitPrice: number
  /** 对应后端字段 discountRate */
  discountRate: number
  /** 对应后端字段 discountAmount */
  discountAmount: number
  /** 对应后端字段 taxRate */
  taxRate: number
  /** 对应后端字段 taxAmount */
  taxAmount: number
  /** 对应后端字段 subtotalAmount */
  subtotalAmount: number
  /** 对应后端字段 lineNumber */
  lineNumber: number
}

/**
 * PurchaseOrderItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderItemCreateDto）
 */
export interface PurchaseOrderItemCreate {
  /** 对应后端字段 materialId */
  materialId: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 purchaseUnit */
  purchaseUnit: string
  /** 对应后端字段 orderQuantity */
  orderQuantity: number
  /** 对应后端字段 unitPrice */
  unitPrice: number
  /** 对应后端字段 discountRate */
  discountRate: number
  /** 对应后端字段 taxRate */
  taxRate: number
  /** 对应后端字段 lineNumber */
  lineNumber: number
}
