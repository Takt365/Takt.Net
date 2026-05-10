// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/ipqc-order
// 文件名称：ipqc-order.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ipqc-order相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * IpqcOrder类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderDto）
 */
export interface IpqcOrder extends TaktEntityBase {
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId: string
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
  /** 对应后端字段 processCode */
  processCode: string
  /** 对应后端字段 processName */
  processName: string
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
 * IpqcOrderQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderQueryDto）
 */
export interface IpqcOrderQuery extends TaktPagedQuery {
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
  /** 对应后端字段 processCode */
  processCode?: string
  /** 对应后端字段 processName */
  processName?: string
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
 * IpqcOrderCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderCreateDto）
 */
export interface IpqcOrderCreate {
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
  /** 对应后端字段 processCode */
  processCode: string
  /** 对应后端字段 processName */
  processName: string
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
 * IpqcOrderUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderUpdateDto）
 */
export interface IpqcOrderUpdate extends IpqcOrderCreate {
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId: string
}

/**
 * IpqcOrderOrderStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderOrderStatusDto）
 */
export interface IpqcOrderOrderStatus {
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId: string
  /** 对应后端字段 orderStatus */
  orderStatus: number
}
