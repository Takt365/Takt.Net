// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/controlling/cost-center-change-log
// 文件名称：cost-center-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：cost-center-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CostCenterChangeLog类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterChangeLogDto）
 */
export interface CostCenterChangeLog extends TaktEntityBase {
  /** 对应后端字段 costCenterChangeLogId */
  costCenterChangeLogId: string
  /** 对应后端字段 costCenterId */
  costCenterId: string
  /** 对应后端字段 costCenterCode */
  costCenterCode: string
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
 * CostCenterChangeLogQuery类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterChangeLogQueryDto）
 */
export interface CostCenterChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 costCenterId */
  costCenterId?: string
  /** 对应后端字段 costCenterCode */
  costCenterCode?: string
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
 * CostCenterChangeLogCreate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterChangeLogCreateDto）
 */
export interface CostCenterChangeLogCreate {
  /** 对应后端字段 costCenterId */
  costCenterId: string
  /** 对应后端字段 costCenterCode */
  costCenterCode: string
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
 * CostCenterChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterChangeLogUpdateDto）
 */
export interface CostCenterChangeLogUpdate extends CostCenterChangeLogCreate {
  /** 对应后端字段 costCenterChangeLogId */
  costCenterChangeLogId: string
}
