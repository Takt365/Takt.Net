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
  /** 对应后端字段 orderCode */
  orderCode: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 planCode */
  planCode?: string
  /** 对应后端字段 standardCode */
  standardCode: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 customerCode */
  customerCode: string
  /** 对应后端字段 customerName */
  customerName: string
  /** 对应后端字段 outgoingQuantity */
  outgoingQuantity: number
  /** 对应后端字段 deliveryOrderCode */
  deliveryOrderCode?: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode?: string
  /** 对应后端字段 sampleQuantity */
  sampleQuantity: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity: number
  /** 对应后端字段 unqualifiedQuantity */
  unqualifiedQuantity: number
  /** 对应后端字段 inspectionConclusion */
  inspectionConclusion: number
  /** 对应后端字段 judgeBy */
  judgeBy?: string
  /** 对应后端字段 judgeTime */
  judgeTime?: string
  /** 对应后端字段 inspectionRemark */
  inspectionRemark?: string
  /** 对应后端字段 orderStatus */
  orderStatus: number
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * FqcOrderQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderQueryDto）
 */
export interface FqcOrderQuery extends TaktPagedQuery {
  /** 对应后端字段 orderCode */
  orderCode?: string
  /** 对应后端字段 sourceCode */
  sourceCode?: string
  /** 对应后端字段 planCode */
  planCode?: string
  /** 对应后端字段 standardCode */
  standardCode?: string
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 customerName */
  customerName?: string
  /** 对应后端字段 outgoingQuantity */
  outgoingQuantity?: number
  /** 对应后端字段 deliveryOrderCode */
  deliveryOrderCode?: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode?: string
  /** 对应后端字段 sampleQuantity */
  sampleQuantity?: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity?: number
  /** 对应后端字段 unqualifiedQuantity */
  unqualifiedQuantity?: number
  /** 对应后端字段 inspectionConclusion */
  inspectionConclusion?: number
  /** 对应后端字段 judgeBy */
  judgeBy?: string
  /** 对应后端字段 judgeTime */
  judgeTime?: string
  /** 对应后端字段 judgeTimeStart */
  judgeTimeStart?: string
  /** 对应后端字段 judgeTimeEnd */
  judgeTimeEnd?: string
  /** 对应后端字段 inspectionRemark */
  inspectionRemark?: string
  /** 对应后端字段 orderStatus */
  orderStatus?: number
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
  /** 对应后端字段 orderCode */
  orderCode: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 planCode */
  planCode?: string
  /** 对应后端字段 standardCode */
  standardCode: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 customerCode */
  customerCode: string
  /** 对应后端字段 customerName */
  customerName: string
  /** 对应后端字段 outgoingQuantity */
  outgoingQuantity: number
  /** 对应后端字段 deliveryOrderCode */
  deliveryOrderCode?: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode?: string
  /** 对应后端字段 sampleQuantity */
  sampleQuantity: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity: number
  /** 对应后端字段 unqualifiedQuantity */
  unqualifiedQuantity: number
  /** 对应后端字段 inspectionConclusion */
  inspectionConclusion: number
  /** 对应后端字段 judgeBy */
  judgeBy?: string
  /** 对应后端字段 judgeTime */
  judgeTime?: string
  /** 对应后端字段 inspectionRemark */
  inspectionRemark?: string
  /** 对应后端字段 orderStatus */
  orderStatus: number
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
 * FqcOrderOrderStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderOrderStatusDto）
 */
export interface FqcOrderOrderStatus {
  /** 对应后端字段 fqcOrderId */
  fqcOrderId: string
  /** 对应后端字段 orderStatus */
  orderStatus: number
}
