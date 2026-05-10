// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/sales/sales-order-change-log
// 文件名称：sales-order-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：sales-order-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SalesOrderChangeLog类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderChangeLogDto）
 */
export interface SalesOrderChangeLog extends TaktEntityBase {
  /** 对应后端字段 salesOrderChangeLogId */
  salesOrderChangeLogId: string
  /** 对应后端字段 salesOrderId */
  salesOrderId: string
  /** 对应后端字段 orderCode */
  orderCode: string
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
 * SalesOrderChangeLogQuery类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderChangeLogQueryDto）
 */
export interface SalesOrderChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 salesOrderId */
  salesOrderId?: string
  /** 对应后端字段 orderCode */
  orderCode?: string
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
 * SalesOrderChangeLogCreate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderChangeLogCreateDto）
 */
export interface SalesOrderChangeLogCreate {
  /** 对应后端字段 salesOrderId */
  salesOrderId: string
  /** 对应后端字段 orderCode */
  orderCode: string
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
 * SalesOrderChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesOrderChangeLogUpdateDto）
 */
export interface SalesOrderChangeLogUpdate extends SalesOrderChangeLogCreate {
  /** 对应后端字段 salesOrderChangeLogId */
  salesOrderChangeLogId: string
}
