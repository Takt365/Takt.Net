// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/iqc-order-item
// 文件名称：iqc-order-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：iqc-order-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * IqcOrderItem类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderItemDto）
 */
export interface IqcOrderItem extends TaktEntityBase {
  /** 对应后端字段 iqcOrderItemId */
  iqcOrderItemId: string
  /** 对应后端字段 iqcOrderId */
  iqcOrderId: string
  /** 对应后端字段 iqcOrderCode */
  iqcOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 purchaseQuantity */
  purchaseQuantity: number
  /** 对应后端字段 standardCode */
  standardCode: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod: number
  /** 对应后端字段 sampleQuantity */
  sampleQuantity: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity: number
  /** 对应后端字段 unqualifiedQuantity */
  unqualifiedQuantity: number
  /** 对应后端字段 inspectionReturnQuantity */
  inspectionReturnQuantity: number
  /** 对应后端字段 judgeStatus */
  judgeStatus: number
  /** 对应后端字段 sampleSerialNo */
  sampleSerialNo?: string
  /** 对应后端字段 inspectionDescription */
  inspectionDescription?: string
  /** 对应后端字段 inspectorBy */
  inspectorBy: string
  /** 对应后端字段 inspectionDate */
  inspectionDate: string
  /** 对应后端字段 order */
  order?: unknown
  /** 对应后端字段 defectHandlings */
  defectHandlings?: unknown[]
}

/**
 * IqcOrderItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderItemQueryDto）
 */
export interface IqcOrderItemQuery extends TaktPagedQuery {
  /** 对应后端字段 iqcOrderId */
  iqcOrderId?: string
  /** 对应后端字段 iqcOrderCode */
  iqcOrderCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 purchaseQuantity */
  purchaseQuantity?: number
  /** 对应后端字段 standardCode */
  standardCode?: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode?: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: number
  /** 对应后端字段 sampleQuantity */
  sampleQuantity?: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity?: number
  /** 对应后端字段 unqualifiedQuantity */
  unqualifiedQuantity?: number
  /** 对应后端字段 inspectionReturnQuantity */
  inspectionReturnQuantity?: number
  /** 对应后端字段 judgeStatus */
  judgeStatus?: number
  /** 对应后端字段 sampleSerialNo */
  sampleSerialNo?: string
  /** 对应后端字段 inspectionDescription */
  inspectionDescription?: string
  /** 对应后端字段 inspectorBy */
  inspectorBy?: string
  /** 对应后端字段 inspectionDate */
  inspectionDate?: string
  /** 对应后端字段 inspectionDateStart */
  inspectionDateStart?: string
  /** 对应后端字段 inspectionDateEnd */
  inspectionDateEnd?: string
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
 * IqcOrderItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderItemCreateDto）
 */
export interface IqcOrderItemCreate {
  /** 对应后端字段 iqcOrderId */
  iqcOrderId: string
  /** 对应后端字段 iqcOrderCode */
  iqcOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 purchaseQuantity */
  purchaseQuantity: number
  /** 对应后端字段 standardCode */
  standardCode: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod: number
  /** 对应后端字段 sampleQuantity */
  sampleQuantity: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity: number
  /** 对应后端字段 unqualifiedQuantity */
  unqualifiedQuantity: number
  /** 对应后端字段 inspectionReturnQuantity */
  inspectionReturnQuantity: number
  /** 对应后端字段 judgeStatus */
  judgeStatus: number
  /** 对应后端字段 sampleSerialNo */
  sampleSerialNo?: string
  /** 对应后端字段 inspectionDescription */
  inspectionDescription?: string
  /** 对应后端字段 inspectorBy */
  inspectorBy: string
  /** 对应后端字段 inspectionDate */
  inspectionDate: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 defectHandlings */
  defectHandlings?: unknown[]
}

/**
 * IqcOrderItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderItemUpdateDto）
 */
export interface IqcOrderItemUpdate extends IqcOrderItemCreate {
  /** 对应后端字段 iqcOrderItemId */
  iqcOrderItemId: string
}

/**
 * IqcOrderItemJudgeStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderItemJudgeStatusDto）
 */
export interface IqcOrderItemJudgeStatus {
  /** 对应后端字段 iqcOrderItemId */
  iqcOrderItemId: string
  /** 对应后端字段 judgeStatus */
  judgeStatus: number
}
