// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/purchase-order-item
// 文件名称：purchase-order-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase-order-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchaseOrderItem类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderItemDto）
 */
export interface PurchaseOrderItem extends TaktEntityBase {
  /** 对应后端字段 purchaseOrderItemId */
  purchaseOrderItemId: string
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId: string
  /** 对应后端字段 purchaseOrderCode */
  purchaseOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 requestCode */
  requestCode?: string
  /** 对应后端字段 requestLineNumber */
  requestLineNumber?: number
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
  /** 对应后端字段 deliveryStatus */
  deliveryStatus: number
}

/**
 * PurchaseOrderItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderItemQueryDto）
 */
export interface PurchaseOrderItemQuery extends TaktPagedQuery {
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId?: string
  /** 对应后端字段 purchaseOrderCode */
  purchaseOrderCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 requestCode */
  requestCode?: string
  /** 对应后端字段 requestLineNumber */
  requestLineNumber?: number
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 purchaseUnit */
  purchaseUnit?: string
  /** 对应后端字段 orderQuantity */
  orderQuantity?: number
  /** 对应后端字段 receivedQuantity */
  receivedQuantity?: number
  /** 对应后端字段 unitPrice */
  unitPrice?: number
  /** 对应后端字段 discountRate */
  discountRate?: number
  /** 对应后端字段 discountAmount */
  discountAmount?: number
  /** 对应后端字段 taxRate */
  taxRate?: number
  /** 对应后端字段 taxAmount */
  taxAmount?: number
  /** 对应后端字段 subtotalAmount */
  subtotalAmount?: number
  /** 对应后端字段 deliveryStatus */
  deliveryStatus?: number
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
 * PurchaseOrderItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderItemCreateDto）
 */
export interface PurchaseOrderItemCreate {
  /** 对应后端字段 purchaseOrderId */
  purchaseOrderId: string
  /** 对应后端字段 purchaseOrderCode */
  purchaseOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 requestCode */
  requestCode?: string
  /** 对应后端字段 requestLineNumber */
  requestLineNumber?: number
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
  /** 对应后端字段 deliveryStatus */
  deliveryStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PurchaseOrderItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderItemUpdateDto）
 */
export interface PurchaseOrderItemUpdate extends PurchaseOrderItemCreate {
  /** 对应后端字段 purchaseOrderItemId */
  purchaseOrderItemId: string
}

/**
 * PurchaseOrderItemDeliveryStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseOrderItemDeliveryStatusDto）
 */
export interface PurchaseOrderItemDeliveryStatus {
  /** 对应后端字段 purchaseOrderItemId */
  purchaseOrderItemId: string
  /** 对应后端字段 deliveryStatus */
  deliveryStatus: number
}
