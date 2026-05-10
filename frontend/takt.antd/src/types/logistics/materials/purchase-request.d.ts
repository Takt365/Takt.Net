// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/purchase-request
// 文件名称：purchase-request.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase-request相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchaseRequest类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestDto）
 */
export interface PurchaseRequest extends TaktEntityBase {
  /** 对应后端字段 purchaseRequestId */
  purchaseRequestId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 requestCode */
  requestCode: string
  /** 对应后端字段 requestDate */
  requestDate: string
  /** 对应后端字段 requiredArrivalDate */
  requiredArrivalDate?: string
  /** 对应后端字段 requestId */
  requestId?: string
  /** 对应后端字段 requestBy */
  requestBy: string
  /** 对应后端字段 totalQuantity */
  totalQuantity: number
  /** 对应后端字段 totalAmount */
  totalAmount: number
  /** 对应后端字段 convertedQuantity */
  convertedQuantity: number
  /** 对应后端字段 convertedAmount */
  convertedAmount: number
  /** 对应后端字段 requestStatus */
  requestStatus: number
  /** 对应后端字段 convertedStatus */
  convertedStatus: number
  /** 对应后端字段 approverBy */
  approverBy?: string
  /** 对应后端字段 approverId */
  approverId?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 approveTime */
  approveTime?: string
  /** 对应后端字段 approveComment */
  approveComment?: string
  /** 对应后端字段 requestReason */
  requestReason?: string
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * PurchaseRequestQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestQueryDto）
 */
export interface PurchaseRequestQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 requestCode */
  requestCode?: string
  /** 对应后端字段 requestDate */
  requestDate?: string
  /** 对应后端字段 requestDateStart */
  requestDateStart?: string
  /** 对应后端字段 requestDateEnd */
  requestDateEnd?: string
  /** 对应后端字段 requiredArrivalDate */
  requiredArrivalDate?: string
  /** 对应后端字段 requiredArrivalDateStart */
  requiredArrivalDateStart?: string
  /** 对应后端字段 requiredArrivalDateEnd */
  requiredArrivalDateEnd?: string
  /** 对应后端字段 requestId */
  requestId?: string
  /** 对应后端字段 requestBy */
  requestBy?: string
  /** 对应后端字段 totalQuantity */
  totalQuantity?: number
  /** 对应后端字段 totalAmount */
  totalAmount?: number
  /** 对应后端字段 convertedQuantity */
  convertedQuantity?: number
  /** 对应后端字段 convertedAmount */
  convertedAmount?: number
  /** 对应后端字段 requestStatus */
  requestStatus?: number
  /** 对应后端字段 convertedStatus */
  convertedStatus?: number
  /** 对应后端字段 approverBy */
  approverBy?: string
  /** 对应后端字段 approverId */
  approverId?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 approveTime */
  approveTime?: string
  /** 对应后端字段 approveTimeStart */
  approveTimeStart?: string
  /** 对应后端字段 approveTimeEnd */
  approveTimeEnd?: string
  /** 对应后端字段 approveComment */
  approveComment?: string
  /** 对应后端字段 requestReason */
  requestReason?: string
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
 * PurchaseRequestCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestCreateDto）
 */
export interface PurchaseRequestCreate {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 requestCode */
  requestCode: string
  /** 对应后端字段 requestDate */
  requestDate: string
  /** 对应后端字段 requiredArrivalDate */
  requiredArrivalDate?: string
  /** 对应后端字段 requestId */
  requestId?: string
  /** 对应后端字段 requestBy */
  requestBy: string
  /** 对应后端字段 totalQuantity */
  totalQuantity: number
  /** 对应后端字段 totalAmount */
  totalAmount: number
  /** 对应后端字段 convertedQuantity */
  convertedQuantity: number
  /** 对应后端字段 convertedAmount */
  convertedAmount: number
  /** 对应后端字段 requestStatus */
  requestStatus: number
  /** 对应后端字段 convertedStatus */
  convertedStatus: number
  /** 对应后端字段 approverBy */
  approverBy?: string
  /** 对应后端字段 approverId */
  approverId?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 approveTime */
  approveTime?: string
  /** 对应后端字段 approveComment */
  approveComment?: string
  /** 对应后端字段 requestReason */
  requestReason?: string
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
 * PurchaseRequestUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestUpdateDto）
 */
export interface PurchaseRequestUpdate extends PurchaseRequestCreate {
  /** 对应后端字段 purchaseRequestId */
  purchaseRequestId: string
}

/**
 * PurchaseRequestConvertedStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestConvertedStatusDto）
 */
export interface PurchaseRequestConvertedStatus {
  /** 对应后端字段 purchaseRequestId */
  purchaseRequestId: string
  /** 对应后端字段 convertedStatus */
  convertedStatus: number
}

/**
 * PurchaseRequestRequestStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchaseRequestRequestStatusDto）
 */
export interface PurchaseRequestRequestStatus {
  /** 对应后端字段 purchaseRequestId */
  purchaseRequestId: string
  /** 对应后端字段 requestStatus */
  requestStatus: number
}
