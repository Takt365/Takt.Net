// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/ipqc-defect-handling
// 文件名称：ipqc-defect-handling.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ipqc-defect-handling相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * IpqcDefectHandling类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcDefectHandlingDto）
 */
export interface IpqcDefectHandling extends TaktEntityBase {
  /** 对应后端字段 ipqcDefectHandlingId */
  ipqcDefectHandlingId: string
  /** 对应后端字段 handlingCode */
  handlingCode: string
  /** 对应后端字段 ipqcOrderItemId */
  ipqcOrderItemId: string
  /** 对应后端字段 orderCode */
  orderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 defectType */
  defectType: number
  /** 对应后端字段 defectCode */
  defectCode: string
  /** 对应后端字段 defectDescription */
  defectDescription: string
  /** 对应后端字段 defectQuantity */
  defectQuantity: number
  /** 对应后端字段 handlingMethod */
  handlingMethod: number
  /** 对应后端字段 handlingDescription */
  handlingDescription?: string
  /** 对应后端字段 responsibleDept */
  responsibleDept?: string
  /** 对应后端字段 responsibleBy */
  responsibleBy?: string
  /** 对应后端字段 handlerBy */
  handlerBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 handlingStatus */
  handlingStatus: number
  /** 对应后端字段 correctiveAction */
  correctiveAction?: string
  /** 对应后端字段 defectImages */
  defectImages?: string
  /** 对应后端字段 orderItem */
  orderItem?: unknown
}

/**
 * IpqcDefectHandlingQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcDefectHandlingQueryDto）
 */
export interface IpqcDefectHandlingQuery extends TaktPagedQuery {
  /** 对应后端字段 handlingCode */
  handlingCode?: string
  /** 对应后端字段 ipqcOrderItemId */
  ipqcOrderItemId?: string
  /** 对应后端字段 orderCode */
  orderCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 defectType */
  defectType?: number
  /** 对应后端字段 defectCode */
  defectCode?: string
  /** 对应后端字段 defectDescription */
  defectDescription?: string
  /** 对应后端字段 defectQuantity */
  defectQuantity?: number
  /** 对应后端字段 handlingMethod */
  handlingMethod?: number
  /** 对应后端字段 handlingDescription */
  handlingDescription?: string
  /** 对应后端字段 responsibleDept */
  responsibleDept?: string
  /** 对应后端字段 responsibleBy */
  responsibleBy?: string
  /** 对应后端字段 handlerBy */
  handlerBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 handlingTimeStart */
  handlingTimeStart?: string
  /** 对应后端字段 handlingTimeEnd */
  handlingTimeEnd?: string
  /** 对应后端字段 handlingStatus */
  handlingStatus?: number
  /** 对应后端字段 correctiveAction */
  correctiveAction?: string
  /** 对应后端字段 defectImages */
  defectImages?: string
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
 * IpqcDefectHandlingCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcDefectHandlingCreateDto）
 */
export interface IpqcDefectHandlingCreate {
  /** 对应后端字段 handlingCode */
  handlingCode: string
  /** 对应后端字段 ipqcOrderItemId */
  ipqcOrderItemId: string
  /** 对应后端字段 orderCode */
  orderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 defectType */
  defectType: number
  /** 对应后端字段 defectCode */
  defectCode: string
  /** 对应后端字段 defectDescription */
  defectDescription: string
  /** 对应后端字段 defectQuantity */
  defectQuantity: number
  /** 对应后端字段 handlingMethod */
  handlingMethod: number
  /** 对应后端字段 handlingDescription */
  handlingDescription?: string
  /** 对应后端字段 responsibleDept */
  responsibleDept?: string
  /** 对应后端字段 responsibleBy */
  responsibleBy?: string
  /** 对应后端字段 handlerBy */
  handlerBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 handlingStatus */
  handlingStatus: number
  /** 对应后端字段 correctiveAction */
  correctiveAction?: string
  /** 对应后端字段 defectImages */
  defectImages?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * IpqcDefectHandlingUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcDefectHandlingUpdateDto）
 */
export interface IpqcDefectHandlingUpdate extends IpqcDefectHandlingCreate {
  /** 对应后端字段 ipqcDefectHandlingId */
  ipqcDefectHandlingId: string
}

/**
 * IpqcDefectHandlingHandlingStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcDefectHandlingHandlingStatusDto）
 */
export interface IpqcDefectHandlingHandlingStatus {
  /** 对应后端字段 ipqcDefectHandlingId */
  ipqcDefectHandlingId: string
  /** 对应后端字段 handlingStatus */
  handlingStatus: number
}
