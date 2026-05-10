// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/controlling/profit-center-change-log
// 文件名称：profit-center-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：profit-center-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ProfitCenterChangeLog类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktProfitCenterChangeLogDto）
 */
export interface ProfitCenterChangeLog extends TaktEntityBase {
  /** 对应后端字段 profitCenterChangeLogId */
  profitCenterChangeLogId: string
  /** 对应后端字段 profitCenterId */
  profitCenterId: string
  /** 对应后端字段 profitCenterCode */
  profitCenterCode: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
}

/**
 * ProfitCenterChangeLogQuery类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktProfitCenterChangeLogQueryDto）
 */
export interface ProfitCenterChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 profitCenterId */
  profitCenterId?: string
  /** 对应后端字段 profitCenterCode */
  profitCenterCode?: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeTime */
  changeTime?: string
  /** 对应后端字段 changeTimeStart */
  changeTimeStart?: string
  /** 对应后端字段 changeTimeEnd */
  changeTimeEnd?: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
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
 * ProfitCenterChangeLogCreate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktProfitCenterChangeLogCreateDto）
 */
export interface ProfitCenterChangeLogCreate {
  /** 对应后端字段 profitCenterId */
  profitCenterId: string
  /** 对应后端字段 profitCenterCode */
  profitCenterCode: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ProfitCenterChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktProfitCenterChangeLogUpdateDto）
 */
export interface ProfitCenterChangeLogUpdate extends ProfitCenterChangeLogCreate {
  /** 对应后端字段 profitCenterChangeLogId */
  profitCenterChangeLogId: string
}
