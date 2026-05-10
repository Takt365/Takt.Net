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
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 itemCode */
  itemCode: string
  /** 对应后端字段 itemName */
  itemName: string
  /** 对应后端字段 itemType */
  itemType: number
  /** 对应后端字段 standardValue */
  standardValue?: string
  /** 对应后端字段 upperLimit */
  upperLimit?: string
  /** 对应后端字段 lowerLimit */
  lowerLimit?: string
  /** 对应后端字段 inspectionTool */
  inspectionTool?: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 actualValue */
  actualValue?: string
  /** 对应后端字段 inspectionResult */
  inspectionResult: number
  /** 对应后端字段 defectQuantity */
  defectQuantity: number
  /** 对应后端字段 defectDescription */
  defectDescription?: string
  /** 对应后端字段 inspectorBy */
  inspectorBy?: string
  /** 对应后端字段 inspectionTime */
  inspectionTime?: string
  /** 对应后端字段 inspectionImages */
  inspectionImages?: string
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
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 itemCode */
  itemCode?: string
  /** 对应后端字段 itemName */
  itemName?: string
  /** 对应后端字段 itemType */
  itemType?: number
  /** 对应后端字段 standardValue */
  standardValue?: string
  /** 对应后端字段 upperLimit */
  upperLimit?: string
  /** 对应后端字段 lowerLimit */
  lowerLimit?: string
  /** 对应后端字段 inspectionTool */
  inspectionTool?: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 actualValue */
  actualValue?: string
  /** 对应后端字段 inspectionResult */
  inspectionResult?: number
  /** 对应后端字段 defectQuantity */
  defectQuantity?: number
  /** 对应后端字段 defectDescription */
  defectDescription?: string
  /** 对应后端字段 inspectorBy */
  inspectorBy?: string
  /** 对应后端字段 inspectionTime */
  inspectionTime?: string
  /** 对应后端字段 inspectionTimeStart */
  inspectionTimeStart?: string
  /** 对应后端字段 inspectionTimeEnd */
  inspectionTimeEnd?: string
  /** 对应后端字段 inspectionImages */
  inspectionImages?: string
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
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 itemCode */
  itemCode: string
  /** 对应后端字段 itemName */
  itemName: string
  /** 对应后端字段 itemType */
  itemType: number
  /** 对应后端字段 standardValue */
  standardValue?: string
  /** 对应后端字段 upperLimit */
  upperLimit?: string
  /** 对应后端字段 lowerLimit */
  lowerLimit?: string
  /** 对应后端字段 inspectionTool */
  inspectionTool?: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 actualValue */
  actualValue?: string
  /** 对应后端字段 inspectionResult */
  inspectionResult: number
  /** 对应后端字段 defectQuantity */
  defectQuantity: number
  /** 对应后端字段 defectDescription */
  defectDescription?: string
  /** 对应后端字段 inspectorBy */
  inspectorBy?: string
  /** 对应后端字段 inspectionTime */
  inspectionTime?: string
  /** 对应后端字段 inspectionImages */
  inspectionImages?: string
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
