// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/sales/sales-order-item
// 文件名称：sales-order-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：sales-order-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SalesOrderItem类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderItemDto）
 */
export interface SalesOrderItem extends TaktEntityBase {
  /** 对应后端字段 salesOrderItemId */
  salesOrderItemId: string
  /** 对应后端字段 salesOrderId */
  salesOrderId: string
  /** 对应后端字段 salesOrderCode */
  salesOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 salesUnit */
  salesUnit: string
  /** 对应后端字段 orderQuantity */
  orderQuantity: number
  /** 对应后端字段 shippedQuantity */
  shippedQuantity: number
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
 * SalesOrderItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderItemQueryDto）
 */
export interface SalesOrderItemQuery extends TaktPagedQuery {
  /** 对应后端字段 salesOrderId */
  salesOrderId?: string
  /** 对应后端字段 salesOrderCode */
  salesOrderCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 salesUnit */
  salesUnit?: string
  /** 对应后端字段 orderQuantity */
  orderQuantity?: number
  /** 对应后端字段 shippedQuantity */
  shippedQuantity?: number
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
 * SalesOrderItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderItemCreateDto）
 */
export interface SalesOrderItemCreate {
  /** 对应后端字段 salesOrderId */
  salesOrderId: string
  /** 对应后端字段 salesOrderCode */
  salesOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 salesUnit */
  salesUnit: string
  /** 对应后端字段 orderQuantity */
  orderQuantity: number
  /** 对应后端字段 shippedQuantity */
  shippedQuantity: number
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
 * SalesOrderItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderItemUpdateDto）
 */
export interface SalesOrderItemUpdate extends SalesOrderItemCreate {
  /** 对应后端字段 salesOrderItemId */
  salesOrderItemId: string
}

/**
 * SalesOrderItemDeliveryStatus类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderItemDeliveryStatusDto）
 */
export interface SalesOrderItemDeliveryStatus {
  /** 对应后端字段 salesOrderItemId */
  salesOrderItemId: string
  /** 对应后端字段 deliveryStatus */
  deliveryStatus: number
}
