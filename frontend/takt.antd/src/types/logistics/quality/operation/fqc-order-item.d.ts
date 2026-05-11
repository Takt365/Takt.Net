// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/fqc-order-item
// 文件名称：fqc-order-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：fqc-order-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * FqcOrderItem类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderItemDto）
 */
export interface FqcOrderItem extends TaktEntityBase {
  /** 对应后端字段 fqcOrderItemId */
  fqcOrderItemId: string
  /** 对应后端字段 fqcOrderId */
  fqcOrderId: string
  /** 对应后端字段 fqcOrderCode */
  fqcOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 warehouseQuantity */
  warehouseQuantity: number
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
 * FqcOrderItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderItemQueryDto）
 */
export interface FqcOrderItemQuery extends TaktPagedQuery {
  /** 对应后端字段 fqcOrderId */
  fqcOrderId?: string
  /** 对应后端字段 fqcOrderCode */
  fqcOrderCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 warehouseQuantity */
  warehouseQuantity?: number
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
 * FqcOrderItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderItemCreateDto）
 */
export interface FqcOrderItemCreate {
  /** 对应后端字段 fqcOrderId */
  fqcOrderId: string
  /** 对应后端字段 fqcOrderCode */
  fqcOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 warehouseQuantity */
  warehouseQuantity: number
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
 * FqcOrderItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderItemUpdateDto）
 */
export interface FqcOrderItemUpdate extends FqcOrderItemCreate {
  /** 对应后端字段 fqcOrderItemId */
  fqcOrderItemId: string
}

/**
 * FqcOrderItemJudgeStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderItemJudgeStatusDto）
 */
export interface FqcOrderItemJudgeStatus {
  /** 对应后端字段 fqcOrderItemId */
  fqcOrderItemId: string
  /** 对应后端字段 judgeStatus */
  judgeStatus: number
}
