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
 * IpqcOrderItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderItemQueryDto）
 */
export interface IpqcOrderItemQuery extends TaktPagedQuery {
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId?: string
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
 * IpqcOrderItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderItemCreateDto）
 */
export interface IpqcOrderItemCreate {
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId: string
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
 * IpqcOrderItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderItemUpdateDto）
 */
export interface IpqcOrderItemUpdate extends IpqcOrderItemCreate {
  /** 对应后端字段 ipqcOrderItemId */
  ipqcOrderItemId: string
}
