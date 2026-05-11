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
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 inspectionDate */
  inspectionDate?: string
  /** 对应后端字段 ipqcOrderCode */
  ipqcOrderCode: string
  /** 对应后端字段 processCode */
  processCode: string
  /** 对应后端字段 processName */
  processName: string
  /** 对应后端字段 totalProductionQuantity */
  totalProductionQuantity: number
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
 * IpqcOrderQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderQueryDto）
 */
export interface IpqcOrderQuery extends TaktPagedQuery {
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
  /** 对应后端字段 ipqcOrderCode */
  ipqcOrderCode?: string
  /** 对应后端字段 processCode */
  processCode?: string
  /** 对应后端字段 processName */
  processName?: string
  /** 对应后端字段 totalProductionQuantity */
  totalProductionQuantity?: number
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
 * IpqcOrderCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderCreateDto）
 */
export interface IpqcOrderCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 inspectionDate */
  inspectionDate?: string
  /** 对应后端字段 ipqcOrderCode */
  ipqcOrderCode: string
  /** 对应后端字段 processCode */
  processCode: string
  /** 对应后端字段 processName */
  processName: string
  /** 对应后端字段 totalProductionQuantity */
  totalProductionQuantity: number
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
 * IpqcOrderUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderUpdateDto）
 */
export interface IpqcOrderUpdate extends IpqcOrderCreate {
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId: string
}

/**
 * IpqcOrderJudgeStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderJudgeStatusDto）
 */
export interface IpqcOrderJudgeStatus {
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId: string
  /** 对应后端字段 judgeStatus */
  judgeStatus: number
}
