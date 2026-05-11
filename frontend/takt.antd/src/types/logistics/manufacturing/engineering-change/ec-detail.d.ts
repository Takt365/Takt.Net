// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/engineering-change/ec-detail
// 文件名称：ec-detail.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ec-detail相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EcDetail类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcDetailDto）
 */
export interface EcDetail extends TaktEntityBase {
  /** 对应后端字段 ecDetailId */
  ecDetailId: string
  /** 对应后端字段 ecnId */
  ecnId: string
  /** 对应后端字段 ecnNo */
  ecnNo: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 ecnModel */
  ecnModel: string
  /** 对应后端字段 ecnBomItem */
  ecnBomItem?: string
  /** 对应后端字段 ecnBomSubItem */
  ecnBomSubItem?: string
  /** 对应后端字段 ecnBomNo */
  ecnBomNo?: string
  /** 对应后端字段 ecnChange */
  ecnChange?: string
  /** 对应后端字段 ecnLocal */
  ecnLocal?: string
  /** 对应后端字段 ecnNote */
  ecnNote?: string
  /** 对应后端字段 ecnProcess */
  ecnProcess?: string
  /** 对应后端字段 ecnBomDate */
  ecnBomDate: string
  /** 对应后端字段 ecnEntryDate */
  ecnEntryDate: string
  /** 对应后端字段 ecnOldItem */
  ecnOldItem?: string
  /** 对应后端字段 ecnOldText */
  ecnOldText?: string
  /** 对应后端字段 ecnOldQty */
  ecnOldQty?: number
  /** 对应后端字段 ecnOldSet */
  ecnOldSet?: string
  /** 对应后端字段 ecnNewItem */
  ecnNewItem?: string
  /** 对应后端字段 ecnNewText */
  ecnNewText?: string
  /** 对应后端字段 ecnNewQty */
  ecnNewQty?: number
  /** 对应后端字段 ecnNewSet */
  ecnNewSet?: string
  /** 对应后端字段 isProcurement */
  isProcurement: number
  /** 对应后端字段 isCheck */
  isCheck: number
  /** 对应后端字段 ecnWarehouse */
  ecnWarehouse?: string
  /** 对应后端字段 isEndOfLine */
  isEndOfLine: number
  /** 对应后端字段 ecn */
  ecn?: unknown
  /** 对应后端字段 deptRecords */
  deptRecords?: unknown[]
}

/**
 * EcDetailQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcDetailQueryDto）
 */
export interface EcDetailQuery extends TaktPagedQuery {
  /** 对应后端字段 ecnId */
  ecnId?: string
  /** 对应后端字段 ecnNo */
  ecnNo?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 ecnModel */
  ecnModel?: string
  /** 对应后端字段 ecnBomItem */
  ecnBomItem?: string
  /** 对应后端字段 ecnBomSubItem */
  ecnBomSubItem?: string
  /** 对应后端字段 ecnBomNo */
  ecnBomNo?: string
  /** 对应后端字段 ecnChange */
  ecnChange?: string
  /** 对应后端字段 ecnLocal */
  ecnLocal?: string
  /** 对应后端字段 ecnNote */
  ecnNote?: string
  /** 对应后端字段 ecnProcess */
  ecnProcess?: string
  /** 对应后端字段 ecnBomDate */
  ecnBomDate?: string
  /** 对应后端字段 ecnBomDateStart */
  ecnBomDateStart?: string
  /** 对应后端字段 ecnBomDateEnd */
  ecnBomDateEnd?: string
  /** 对应后端字段 ecnEntryDate */
  ecnEntryDate?: string
  /** 对应后端字段 ecnEntryDateStart */
  ecnEntryDateStart?: string
  /** 对应后端字段 ecnEntryDateEnd */
  ecnEntryDateEnd?: string
  /** 对应后端字段 ecnOldItem */
  ecnOldItem?: string
  /** 对应后端字段 ecnOldText */
  ecnOldText?: string
  /** 对应后端字段 ecnOldQty */
  ecnOldQty?: number
  /** 对应后端字段 ecnOldSet */
  ecnOldSet?: string
  /** 对应后端字段 ecnNewItem */
  ecnNewItem?: string
  /** 对应后端字段 ecnNewText */
  ecnNewText?: string
  /** 对应后端字段 ecnNewQty */
  ecnNewQty?: number
  /** 对应后端字段 ecnNewSet */
  ecnNewSet?: string
  /** 对应后端字段 isProcurement */
  isProcurement?: number
  /** 对应后端字段 isCheck */
  isCheck?: number
  /** 对应后端字段 ecnWarehouse */
  ecnWarehouse?: string
  /** 对应后端字段 isEndOfLine */
  isEndOfLine?: number
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
 * EcDetailCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcDetailCreateDto）
 */
export interface EcDetailCreate {
  /** 对应后端字段 ecnId */
  ecnId: string
  /** 对应后端字段 ecnNo */
  ecnNo: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 ecnModel */
  ecnModel: string
  /** 对应后端字段 ecnBomItem */
  ecnBomItem?: string
  /** 对应后端字段 ecnBomSubItem */
  ecnBomSubItem?: string
  /** 对应后端字段 ecnBomNo */
  ecnBomNo?: string
  /** 对应后端字段 ecnChange */
  ecnChange?: string
  /** 对应后端字段 ecnLocal */
  ecnLocal?: string
  /** 对应后端字段 ecnNote */
  ecnNote?: string
  /** 对应后端字段 ecnProcess */
  ecnProcess?: string
  /** 对应后端字段 ecnBomDate */
  ecnBomDate: string
  /** 对应后端字段 ecnEntryDate */
  ecnEntryDate: string
  /** 对应后端字段 ecnOldItem */
  ecnOldItem?: string
  /** 对应后端字段 ecnOldText */
  ecnOldText?: string
  /** 对应后端字段 ecnOldQty */
  ecnOldQty?: number
  /** 对应后端字段 ecnOldSet */
  ecnOldSet?: string
  /** 对应后端字段 ecnNewItem */
  ecnNewItem?: string
  /** 对应后端字段 ecnNewText */
  ecnNewText?: string
  /** 对应后端字段 ecnNewQty */
  ecnNewQty?: number
  /** 对应后端字段 ecnNewSet */
  ecnNewSet?: string
  /** 对应后端字段 isProcurement */
  isProcurement: number
  /** 对应后端字段 isCheck */
  isCheck: number
  /** 对应后端字段 ecnWarehouse */
  ecnWarehouse?: string
  /** 对应后端字段 isEndOfLine */
  isEndOfLine: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 deptRecords */
  deptRecords?: unknown[]
}

/**
 * EcDetailUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcDetailUpdateDto）
 */
export interface EcDetailUpdate extends EcDetailCreate {
  /** 对应后端字段 ecDetailId */
  ecDetailId: string
}
