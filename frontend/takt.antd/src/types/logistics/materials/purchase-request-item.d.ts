// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/purchase-request-item
// 文件名称：purchase-request-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase-request-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchaseRequestItem类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestItemDto）
 */
export interface PurchaseRequestItem extends TaktEntityBase {
  /** 对应后端字段 purchaseRequestItemId */
  purchaseRequestItemId: string
  /** 对应后端字段 purchaseRequestId */
  purchaseRequestId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 requestUnit */
  requestUnit: string
  /** 对应后端字段 requestQuantity */
  requestQuantity: number
  /** 对应后端字段 convertedQuantity */
  convertedQuantity: number
  /** 对应后端字段 estimatedUnitPrice */
  estimatedUnitPrice: number
  /** 对应后端字段 estimatedAmount */
  estimatedAmount: number
  /** 对应后端字段 referenceSupplierCode */
  referenceSupplierCode?: string
  /** 对应后端字段 referenceSupplierName */
  referenceSupplierName?: string
}

/**
 * PurchaseRequestItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestItemQueryDto）
 */
export interface PurchaseRequestItemQuery extends TaktPagedQuery {
  /** 对应后端字段 purchaseRequestId */
  purchaseRequestId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 requestUnit */
  requestUnit?: string
  /** 对应后端字段 requestQuantity */
  requestQuantity?: number
  /** 对应后端字段 convertedQuantity */
  convertedQuantity?: number
  /** 对应后端字段 estimatedUnitPrice */
  estimatedUnitPrice?: number
  /** 对应后端字段 estimatedAmount */
  estimatedAmount?: number
  /** 对应后端字段 referenceSupplierCode */
  referenceSupplierCode?: string
  /** 对应后端字段 referenceSupplierName */
  referenceSupplierName?: string
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
 * PurchaseRequestItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestItemCreateDto）
 */
export interface PurchaseRequestItemCreate {
  /** 对应后端字段 purchaseRequestId */
  purchaseRequestId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 requestUnit */
  requestUnit: string
  /** 对应后端字段 requestQuantity */
  requestQuantity: number
  /** 对应后端字段 convertedQuantity */
  convertedQuantity: number
  /** 对应后端字段 estimatedUnitPrice */
  estimatedUnitPrice: number
  /** 对应后端字段 estimatedAmount */
  estimatedAmount: number
  /** 对应后端字段 referenceSupplierCode */
  referenceSupplierCode?: string
  /** 对应后端字段 referenceSupplierName */
  referenceSupplierName?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PurchaseRequestItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestItemUpdateDto）
 */
export interface PurchaseRequestItemUpdate extends PurchaseRequestItemCreate {
  /** 对应后端字段 purchaseRequestItemId */
  purchaseRequestItemId: string
}
