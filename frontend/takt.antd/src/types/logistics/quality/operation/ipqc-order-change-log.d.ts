// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/ipqc-order-change-log
// 文件名称：ipqc-order-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ipqc-order-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * IpqcOrderChangeLog类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderChangeLogDto）
 */
export interface IpqcOrderChangeLog extends TaktEntityBase {
  /** 对应后端字段 ipqcOrderChangeLogId */
  ipqcOrderChangeLogId: string
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId: string
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
 * IpqcOrderChangeLogQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderChangeLogQueryDto）
 */
export interface IpqcOrderChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId?: string
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
 * IpqcOrderChangeLogCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderChangeLogCreateDto）
 */
export interface IpqcOrderChangeLogCreate {
  /** 对应后端字段 ipqcOrderId */
  ipqcOrderId: string
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
 * IpqcOrderChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktIpqcOrderChangeLogUpdateDto）
 */
export interface IpqcOrderChangeLogUpdate extends IpqcOrderChangeLogCreate {
  /** 对应后端字段 ipqcOrderChangeLogId */
  ipqcOrderChangeLogId: string
}
