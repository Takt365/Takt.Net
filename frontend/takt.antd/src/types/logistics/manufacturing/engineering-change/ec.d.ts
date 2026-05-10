// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/engineering-change/ec
// 文件名称：ec.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ec相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Ec类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcDto）
 */
export interface Ec extends TaktEntityBase {
  /** 对应后端字段 ecId */
  ecId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 ecnNo */
  ecnNo: string
  /** 对应后端字段 ecnIssueDate */
  ecnIssueDate: string
  /** 对应后端字段 changeStatus */
  changeStatus: number
  /** 对应后端字段 ecnTitle */
  ecnTitle: string
  /** 对应后端字段 ecnDetailText */
  ecnDetailText: string
  /** 对应后端字段 ecnLeader */
  ecnLeader: string
  /** 对应后端字段 ecnLossAmount */
  ecnLossAmount: number
  /** 对应后端字段 ecnDistinction */
  ecnDistinction: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 ecnEntryDate */
  ecnEntryDate: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
  /** 对应后端字段 ecStatus */
  ecStatus: number
  /** 对应后端字段 ecnDetails */
  ecnDetails?: unknown[]
  /** 对应后端字段 attachments */
  attachments?: unknown[]
}

/**
 * EcQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcQueryDto）
 */
export interface EcQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 ecnNo */
  ecnNo?: string
  /** 对应后端字段 ecnIssueDate */
  ecnIssueDate?: string
  /** 对应后端字段 ecnIssueDateStart */
  ecnIssueDateStart?: string
  /** 对应后端字段 ecnIssueDateEnd */
  ecnIssueDateEnd?: string
  /** 对应后端字段 changeStatus */
  changeStatus?: number
  /** 对应后端字段 ecnTitle */
  ecnTitle?: string
  /** 对应后端字段 ecnDetailText */
  ecnDetailText?: string
  /** 对应后端字段 ecnLeader */
  ecnLeader?: string
  /** 对应后端字段 ecnLossAmount */
  ecnLossAmount?: number
  /** 对应后端字段 ecnDistinction */
  ecnDistinction?: string
  /** 对应后端字段 effectiveDate */
  effectiveDate?: string
  /** 对应后端字段 effectiveDateStart */
  effectiveDateStart?: string
  /** 对应后端字段 effectiveDateEnd */
  effectiveDateEnd?: string
  /** 对应后端字段 ecnEntryDate */
  ecnEntryDate?: string
  /** 对应后端字段 ecnEntryDateStart */
  ecnEntryDateStart?: string
  /** 对应后端字段 ecnEntryDateEnd */
  ecnEntryDateEnd?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 ecStatus */
  ecStatus?: number
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
 * EcCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcCreateDto）
 */
export interface EcCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 ecnNo */
  ecnNo: string
  /** 对应后端字段 ecnIssueDate */
  ecnIssueDate: string
  /** 对应后端字段 changeStatus */
  changeStatus: number
  /** 对应后端字段 ecnTitle */
  ecnTitle: string
  /** 对应后端字段 ecnDetailText */
  ecnDetailText: string
  /** 对应后端字段 ecnLeader */
  ecnLeader: string
  /** 对应后端字段 ecnLossAmount */
  ecnLossAmount: number
  /** 对应后端字段 ecnDistinction */
  ecnDistinction: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 ecnEntryDate */
  ecnEntryDate: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
  /** 对应后端字段 ecStatus */
  ecStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 ecnDetails */
  ecnDetails?: unknown[]
  /** 对应后端字段 attachments */
  attachments?: unknown[]
}

/**
 * EcUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcUpdateDto）
 */
export interface EcUpdate extends EcCreate {
  /** 对应后端字段 ecId */
  ecId: string
}

/**
 * EcStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcStatusDto）
 */
export interface EcStatus {
  /** 对应后端字段 ecId */
  ecId: string
  /** 对应后端字段 ecStatus */
  ecStatus: number
}

/**
 * EcChangeStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcChangeStatusDto）
 */
export interface EcChangeStatus {
  /** 对应后端字段 ecId */
  ecId: string
  /** 对应后端字段 changeStatus */
  changeStatus: number
}
