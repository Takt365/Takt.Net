// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/fqc-order
// 文件名称：fqc-order.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：fqc-order相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * FqcOrder类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderDto）
 */
export interface FqcOrder extends TaktEntityBase {
  /** 对应后端字段 fqcOrderId */
  fqcOrderId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 inspectionDate */
  inspectionDate?: string
  /** 对应后端字段 fqcOrderCode */
  fqcOrderCode: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 totalWarehouseQuantity */
  totalWarehouseQuantity: number
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
 * FqcOrderQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderQueryDto）
 */
export interface FqcOrderQuery extends TaktPagedQuery {
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
  /** 对应后端字段 fqcOrderCode */
  fqcOrderCode?: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 totalWarehouseQuantity */
  totalWarehouseQuantity?: number
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
 * FqcOrderCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderCreateDto）
 */
export interface FqcOrderCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 inspectionDate */
  inspectionDate?: string
  /** 对应后端字段 fqcOrderCode */
  fqcOrderCode: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 totalWarehouseQuantity */
  totalWarehouseQuantity: number
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
 * FqcOrderUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderUpdateDto）
 */
export interface FqcOrderUpdate extends FqcOrderCreate {
  /** 对应后端字段 fqcOrderId */
  fqcOrderId: string
}

/**
 * FqcOrderJudgeStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderJudgeStatusDto）
 */
export interface FqcOrderJudgeStatus {
  /** 对应后端字段 fqcOrderId */
  fqcOrderId: string
  /** 对应后端字段 judgeStatus */
  judgeStatus: number
}
