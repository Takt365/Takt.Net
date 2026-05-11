// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/iqc-order
// 文件名称：iqc-order.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：iqc-order相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * IqcOrder类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderDto）
 */
export interface IqcOrder extends TaktEntityBase {
  /** 对应后端字段 iqcOrderId */
  iqcOrderId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 inspectionDate */
  inspectionDate?: string
  /** 对应后端字段 iqcOrderCode */
  iqcOrderCode: string
  /** 对应后端字段 supplierCode */
  supplierCode: string
  /** 对应后端字段 totalPurchaseQuantity */
  totalPurchaseQuantity: number
  /** 对应后端字段 totalSampleQuantity */
  totalSampleQuantity: number
  /** 对应后端字段 totalQualifiedQuantity */
  totalQualifiedQuantity: number
  /** 对应后端字段 totalUnqualifiedQuantity */
  totalUnqualifiedQuantity: number
  /** 对应后端字段 totalInspectionReturnQuantity */
  totalInspectionReturnQuantity: number
  /** 对应后端字段 judgeStatus */
  judgeStatus: number
  /** 对应后端字段 judgeBy */
  judgeBy?: string
  /** 对应后端字段 judgeDate */
  judgeDate?: string
  /** 对应后端字段 judgeDescription */
  judgeDescription?: string
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * IqcOrderQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderQueryDto）
 */
export interface IqcOrderQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 sourceCode */
  sourceCode?: string
  /** 对应后端字段 inspectionDate */
  inspectionDate?: string
  /** 对应后端字段 inspectionDateStart */
  inspectionDateStart?: string
  /** 对应后端字段 inspectionDateEnd */
  inspectionDateEnd?: string
  /** 对应后端字段 iqcOrderCode */
  iqcOrderCode?: string
  /** 对应后端字段 supplierCode */
  supplierCode?: string
  /** 对应后端字段 totalPurchaseQuantity */
  totalPurchaseQuantity?: number
  /** 对应后端字段 totalSampleQuantity */
  totalSampleQuantity?: number
  /** 对应后端字段 totalQualifiedQuantity */
  totalQualifiedQuantity?: number
  /** 对应后端字段 totalUnqualifiedQuantity */
  totalUnqualifiedQuantity?: number
  /** 对应后端字段 totalInspectionReturnQuantity */
  totalInspectionReturnQuantity?: number
  /** 对应后端字段 judgeStatus */
  judgeStatus?: number
  /** 对应后端字段 judgeBy */
  judgeBy?: string
  /** 对应后端字段 judgeDate */
  judgeDate?: string
  /** 对应后端字段 judgeDateStart */
  judgeDateStart?: string
  /** 对应后端字段 judgeDateEnd */
  judgeDateEnd?: string
  /** 对应后端字段 judgeDescription */
  judgeDescription?: string
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
 * IqcOrderCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderCreateDto）
 */
export interface IqcOrderCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 inspectionDate */
  inspectionDate?: string
  /** 对应后端字段 iqcOrderCode */
  iqcOrderCode: string
  /** 对应后端字段 supplierCode */
  supplierCode: string
  /** 对应后端字段 totalPurchaseQuantity */
  totalPurchaseQuantity: number
  /** 对应后端字段 totalSampleQuantity */
  totalSampleQuantity: number
  /** 对应后端字段 totalQualifiedQuantity */
  totalQualifiedQuantity: number
  /** 对应后端字段 totalUnqualifiedQuantity */
  totalUnqualifiedQuantity: number
  /** 对应后端字段 totalInspectionReturnQuantity */
  totalInspectionReturnQuantity: number
  /** 对应后端字段 judgeStatus */
  judgeStatus: number
  /** 对应后端字段 judgeBy */
  judgeBy?: string
  /** 对应后端字段 judgeDate */
  judgeDate?: string
  /** 对应后端字段 judgeDescription */
  judgeDescription?: string
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
 * IqcOrderUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderUpdateDto）
 */
export interface IqcOrderUpdate extends IqcOrderCreate {
  /** 对应后端字段 iqcOrderId */
  iqcOrderId: string
}

/**
 * IqcOrderJudgeStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderJudgeStatusDto）
 */
export interface IqcOrderJudgeStatus {
  /** 对应后端字段 iqcOrderId */
  iqcOrderId: string
  /** 对应后端字段 judgeStatus */
  judgeStatus: number
}
