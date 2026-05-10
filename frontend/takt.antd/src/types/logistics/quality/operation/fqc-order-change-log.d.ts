// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/fqc-order-change-log
// 文件名称：fqc-order-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：fqc-order-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * FqcOrderChangeLog类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderChangeLogDto）
 */
export interface FqcOrderChangeLog extends TaktEntityBase {
  /** 对应后端字段 fqcOrderChangeLogId */
  fqcOrderChangeLogId: string
  /** 对应后端字段 fqcOrderId */
  fqcOrderId: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeType */
  changeType: number
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 order */
  order?: unknown
}

/**
 * FqcOrderChangeLogQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderChangeLogQueryDto）
 */
export interface FqcOrderChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 fqcOrderId */
  fqcOrderId?: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeType */
  changeType?: number
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeTime */
  changeTime?: string
  /** 对应后端字段 changeTimeStart */
  changeTimeStart?: string
  /** 对应后端字段 changeTimeEnd */
  changeTimeEnd?: string
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
 * FqcOrderChangeLogCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderChangeLogCreateDto）
 */
export interface FqcOrderChangeLogCreate {
  /** 对应后端字段 fqcOrderId */
  fqcOrderId: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeType */
  changeType: number
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * FqcOrderChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktFqcOrderChangeLogUpdateDto）
 */
export interface FqcOrderChangeLogUpdate extends FqcOrderChangeLogCreate {
  /** 对应后端字段 fqcOrderChangeLogId */
  fqcOrderChangeLogId: string
}
