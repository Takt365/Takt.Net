// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/iqc-defect-handling
// 文件名称：iqc-defect-handling.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：iqc-defect-handling相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * IqcDefectHandling类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcDefectHandlingDto）
 */
export interface IqcDefectHandling extends TaktEntityBase {
  /** 对应后端字段 iqcDefectHandlingId */
  iqcDefectHandlingId: string
  /** 对应后端字段 handlingCode */
  handlingCode: string
  /** 对应后端字段 iqcOrderItemId */
  iqcOrderItemId: string
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
 * IqcDefectHandlingQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcDefectHandlingQueryDto）
 */
export interface IqcDefectHandlingQuery extends TaktPagedQuery {
  /** 对应后端字段 handlingCode */
  handlingCode?: string
  /** 对应后端字段 iqcOrderItemId */
  iqcOrderItemId?: string
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
 * IqcDefectHandlingCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcDefectHandlingCreateDto）
 */
export interface IqcDefectHandlingCreate {
  /** 对应后端字段 handlingCode */
  handlingCode: string
  /** 对应后端字段 iqcOrderItemId */
  iqcOrderItemId: string
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
 * IqcDefectHandlingUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcDefectHandlingUpdateDto）
 */
export interface IqcDefectHandlingUpdate extends IqcDefectHandlingCreate {
  /** 对应后端字段 iqcDefectHandlingId */
  iqcDefectHandlingId: string
}

/**
 * IqcDefectHandlingHandlingStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcDefectHandlingHandlingStatusDto）
 */
export interface IqcDefectHandlingHandlingStatus {
  /** 对应后端字段 iqcDefectHandlingId */
  iqcDefectHandlingId: string
  /** 对应后端字段 handlingStatus */
  handlingStatus: number
}
