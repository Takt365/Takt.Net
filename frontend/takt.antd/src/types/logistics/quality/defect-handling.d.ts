// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/defect-handling
// 文件名称：defect-handling.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：defect-handling相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * DefectHandling类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktDefectHandlingDto）
 */
export interface DefectHandling extends TaktEntityBase {
  /** 对应后端字段 defectHandlingId */
  defectHandlingId: string
  /** 对应后端字段 handlingCode */
  handlingCode: string
  /** 对应后端字段 executionId */
  executionId: string
  /** 对应后端字段 sheetItemId */
  sheetItemId?: string
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
  /** 对应后端字段 responsibleUserId */
  responsibleUserId?: string
  /** 对应后端字段 responsibleUserName */
  responsibleUserName?: string
  /** 对应后端字段 handlerId */
  handlerId?: string
  /** 对应后端字段 handlerName */
  handlerName?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 handlingStatus */
  handlingStatus: number
  /** 对应后端字段 correctiveAction */
  correctiveAction?: string
  /** 对应后端字段 defectImages */
  defectImages?: string
  /** 对应后端字段 execution */
  execution?: unknown
  /** 对应后端字段 sheetItem */
  sheetItem?: unknown
}

/**
 * DefectHandlingQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktDefectHandlingQueryDto）
 */
export interface DefectHandlingQuery extends TaktPagedQuery {
  /** 对应后端字段 handlingCode */
  handlingCode?: string
  /** 对应后端字段 executionId */
  executionId?: string
  /** 对应后端字段 sheetItemId */
  sheetItemId?: string
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
  /** 对应后端字段 responsibleUserId */
  responsibleUserId?: string
  /** 对应后端字段 responsibleUserName */
  responsibleUserName?: string
  /** 对应后端字段 handlerId */
  handlerId?: string
  /** 对应后端字段 handlerName */
  handlerName?: string
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
 * DefectHandlingCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktDefectHandlingCreateDto）
 */
export interface DefectHandlingCreate {
  /** 对应后端字段 handlingCode */
  handlingCode: string
  /** 对应后端字段 executionId */
  executionId: string
  /** 对应后端字段 sheetItemId */
  sheetItemId?: string
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
  /** 对应后端字段 responsibleUserId */
  responsibleUserId?: string
  /** 对应后端字段 responsibleUserName */
  responsibleUserName?: string
  /** 对应后端字段 handlerId */
  handlerId?: string
  /** 对应后端字段 handlerName */
  handlerName?: string
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
 * DefectHandlingUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktDefectHandlingUpdateDto）
 */
export interface DefectHandlingUpdate extends DefectHandlingCreate {
  /** 对应后端字段 defectHandlingId */
  defectHandlingId: string
}

/**
 * DefectHandlingHandlingStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktDefectHandlingHandlingStatusDto）
 */
export interface DefectHandlingHandlingStatus {
  /** 对应后端字段 defectHandlingId */
  defectHandlingId: string
  /** 对应后端字段 handlingStatus */
  handlingStatus: number
}
