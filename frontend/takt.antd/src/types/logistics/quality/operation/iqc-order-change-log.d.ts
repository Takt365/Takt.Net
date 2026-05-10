// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/iqc-order-change-log
// 文件名称：iqc-order-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：iqc-order-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * IqcOrderChangeLog类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderChangeLogDto）
 */
export interface IqcOrderChangeLog extends TaktEntityBase {
  /** 对应后端字段 iqcOrderChangeLogId */
  iqcOrderChangeLogId: string
  /** 对应后端字段 iqcOrderId */
  iqcOrderId: string
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
 * IqcOrderChangeLogQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderChangeLogQueryDto）
 */
export interface IqcOrderChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 iqcOrderId */
  iqcOrderId?: string
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
 * IqcOrderChangeLogCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderChangeLogCreateDto）
 */
export interface IqcOrderChangeLogCreate {
  /** 对应后端字段 iqcOrderId */
  iqcOrderId: string
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
 * IqcOrderChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIqcOrderChangeLogUpdateDto）
 */
export interface IqcOrderChangeLogUpdate extends IqcOrderChangeLogCreate {
  /** 对应后端字段 iqcOrderChangeLogId */
  iqcOrderChangeLogId: string
}
