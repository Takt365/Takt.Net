// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/ipqc-order-item
// 文件名称：ipqc-order-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ipqc-order-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * IpqcOrderItem类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderItemDto）
 */
export interface IpqcOrderItem extends TaktEntityBase {
  /** 对应后端字段 ipqcOrderItemId */
  ipqcOrderItemId: string
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId: string
  /** 对应后端字段 ipqcOrderCode */
  ipqcOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 productionQuantity */
  productionQuantity: number
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
 * IpqcOrderItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderItemQueryDto）
 */
export interface IpqcOrderItemQuery extends TaktPagedQuery {
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId?: string
  /** 对应后端字段 ipqcOrderCode */
  ipqcOrderCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 productionQuantity */
  productionQuantity?: number
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
 * IpqcOrderItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderItemCreateDto）
 */
export interface IpqcOrderItemCreate {
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId: string
  /** 对应后端字段 ipqcOrderCode */
  ipqcOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 productionQuantity */
  productionQuantity: number
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
 * IpqcOrderItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderItemUpdateDto）
 */
export interface IpqcOrderItemUpdate extends IpqcOrderItemCreate {
  /** 对应后端字段 ipqcOrderItemId */
  ipqcOrderItemId: string
}

/**
 * IpqcOrderItemJudgeStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderItemJudgeStatusDto）
 */
export interface IpqcOrderItemJudgeStatus {
  /** 对应后端字段 ipqcOrderItemId */
  ipqcOrderItemId: string
  /** 对应后端字段 judgeStatus */
  judgeStatus: number
}
