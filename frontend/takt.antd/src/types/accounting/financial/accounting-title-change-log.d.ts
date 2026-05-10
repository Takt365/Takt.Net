// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/financial/accounting-title-change-log
// 文件名称：accounting-title-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：accounting-title-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AccountingTitleChangeLog类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleChangeLogDto）
 */
export interface AccountingTitleChangeLog extends TaktEntityBase {
  /** 对应后端字段 accountingTitleChangeLogId */
  accountingTitleChangeLogId: string
  /** 对应后端字段 accountingTitleId */
  accountingTitleId: string
  /** 对应后端字段 titleCode */
  titleCode: string
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
 * AccountingTitleChangeLogQuery类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleChangeLogQueryDto）
 */
export interface AccountingTitleChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 accountingTitleId */
  accountingTitleId?: string
  /** 对应后端字段 titleCode */
  titleCode?: string
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
 * AccountingTitleChangeLogCreate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleChangeLogCreateDto）
 */
export interface AccountingTitleChangeLogCreate {
  /** 对应后端字段 accountingTitleId */
  accountingTitleId: string
  /** 对应后端字段 titleCode */
  titleCode: string
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
 * AccountingTitleChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleChangeLogUpdateDto）
 */
export interface AccountingTitleChangeLogUpdate extends AccountingTitleChangeLogCreate {
  /** 对应后端字段 accountingTitleChangeLogId */
  accountingTitleChangeLogId: string
}
