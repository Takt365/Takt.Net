// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/sales/sales-order
// 文件名称：sales-order.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：sales-order相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SalesOrder类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderDto）
 */
export interface SalesOrder extends TaktEntityBase {
  /** 对应后端字段 salesOrderId */
  salesOrderId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 orderCode */
  orderCode: string
  /** 对应后端字段 customerCode */
  customerCode: string
  /** 对应后端字段 customerName */
  customerName: string
  /** 对应后端字段 orderDate */
  orderDate: string
  /** 对应后端字段 requiredDeliveryDate */
  requiredDeliveryDate?: string
  /** 对应后端字段 actualDeliveryDate */
  actualDeliveryDate?: string
  /** 对应后端字段 salesBy */
  salesBy?: string
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
  /** 对应后端字段 shippedQuantity */
  shippedQuantity: number
  /** 对应后端字段 shippedAmount */
  shippedAmount: number
  /** 对应后端字段 receivedAmount */
  receivedAmount: number
  /** 对应后端字段 orderStatus */
  orderStatus: number
  /** 对应后端字段 deliveryStatus */
  deliveryStatus: number
  /** 对应后端字段 deliveryMethod */
  deliveryMethod: number
  /** 对应后端字段 paymentMethod */
  paymentMethod: number
  /** 对应后端字段 deliveryAddress */
  deliveryAddress?: string
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * SalesOrderQuery类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderQueryDto）
 */
export interface SalesOrderQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 orderCode */
  orderCode?: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 customerName */
  customerName?: string
  /** 对应后端字段 orderDate */
  orderDate?: string
  /** 对应后端字段 orderDateStart */
  orderDateStart?: string
  /** 对应后端字段 orderDateEnd */
  orderDateEnd?: string
  /** 对应后端字段 requiredDeliveryDate */
  requiredDeliveryDate?: string
  /** 对应后端字段 requiredDeliveryDateStart */
  requiredDeliveryDateStart?: string
  /** 对应后端字段 requiredDeliveryDateEnd */
  requiredDeliveryDateEnd?: string
  /** 对应后端字段 actualDeliveryDate */
  actualDeliveryDate?: string
  /** 对应后端字段 actualDeliveryDateStart */
  actualDeliveryDateStart?: string
  /** 对应后端字段 actualDeliveryDateEnd */
  actualDeliveryDateEnd?: string
  /** 对应后端字段 salesBy */
  salesBy?: string
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
  /** 对应后端字段 shippedQuantity */
  shippedQuantity?: number
  /** 对应后端字段 shippedAmount */
  shippedAmount?: number
  /** 对应后端字段 receivedAmount */
  receivedAmount?: number
  /** 对应后端字段 orderStatus */
  orderStatus?: number
  /** 对应后端字段 deliveryStatus */
  deliveryStatus?: number
  /** 对应后端字段 deliveryMethod */
  deliveryMethod?: number
  /** 对应后端字段 paymentMethod */
  paymentMethod?: number
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
 * SalesOrderCreate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderCreateDto）
 */
export interface SalesOrderCreate {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 orderCode */
  orderCode: string
  /** 对应后端字段 customerCode */
  customerCode: string
  /** 对应后端字段 customerName */
  customerName: string
  /** 对应后端字段 orderDate */
  orderDate: string
  /** 对应后端字段 requiredDeliveryDate */
  requiredDeliveryDate?: string
  /** 对应后端字段 actualDeliveryDate */
  actualDeliveryDate?: string
  /** 对应后端字段 salesBy */
  salesBy?: string
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
  /** 对应后端字段 shippedQuantity */
  shippedQuantity: number
  /** 对应后端字段 shippedAmount */
  shippedAmount: number
  /** 对应后端字段 receivedAmount */
  receivedAmount: number
  /** 对应后端字段 orderStatus */
  orderStatus: number
  /** 对应后端字段 deliveryStatus */
  deliveryStatus: number
  /** 对应后端字段 deliveryMethod */
  deliveryMethod: number
  /** 对应后端字段 paymentMethod */
  paymentMethod: number
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
 * SalesOrderUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderUpdateDto）
 */
export interface SalesOrderUpdate extends SalesOrderCreate {
  /** 对应后端字段 salesOrderId */
  salesOrderId: string
}

/**
 * SalesOrderDeliveryStatus类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderDeliveryStatusDto）
 */
export interface SalesOrderDeliveryStatus {
  /** 对应后端字段 salesOrderId */
  salesOrderId: string
  /** 对应后端字段 deliveryStatus */
  deliveryStatus: number
}

/**
 * SalesOrderOrderStatus类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderOrderStatusDto）
 */
export interface SalesOrderOrderStatus {
  /** 对应后端字段 salesOrderId */
  salesOrderId: string
  /** 对应后端字段 orderStatus */
  orderStatus: number
}
