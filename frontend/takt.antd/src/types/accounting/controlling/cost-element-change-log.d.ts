// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/controlling/cost-element-change-log
// 文件名称：cost-element-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：cost-element-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CostElementChangeLog类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementChangeLogDto）
 */
export interface CostElementChangeLog extends TaktEntityBase {
  /** 对应后端字段 costElementChangeLogId */
  costElementChangeLogId: string
  /** 对应后端字段 costElementId */
  costElementId: string
  /** 对应后端字段 costElementCode */
  costElementCode: string
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
 * CostElementChangeLogQuery类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementChangeLogQueryDto）
 */
export interface CostElementChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 costElementId */
  costElementId?: string
  /** 对应后端字段 costElementCode */
  costElementCode?: string
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
 * CostElementChangeLogCreate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementChangeLogCreateDto）
 */
export interface CostElementChangeLogCreate {
  /** 对应后端字段 costElementId */
  costElementId: string
  /** 对应后端字段 costElementCode */
  costElementCode: string
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
 * CostElementChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementChangeLogUpdateDto）
 */
export interface CostElementChangeLogUpdate extends CostElementChangeLogCreate {
  /** 对应后端字段 costElementChangeLogId */
  costElementChangeLogId: string
}
