// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/sales/sales-price-change-log
// 文件名称：sales-price-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：sales-price-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SalesPriceChangeLog类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceChangeLogDto）
 */
export interface SalesPriceChangeLog extends TaktEntityBase {
  /** 对应后端字段 salesPriceChangeLogId */
  salesPriceChangeLogId: string
  /** 对应后端字段 salesPriceId */
  salesPriceId: string
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
 * SalesPriceChangeLogQuery类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceChangeLogQueryDto）
 */
export interface SalesPriceChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 salesPriceId */
  salesPriceId?: string
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
 * SalesPriceChangeLogCreate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceChangeLogCreateDto）
 */
export interface SalesPriceChangeLogCreate {
  /** 对应后端字段 salesPriceId */
  salesPriceId: string
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
 * SalesPriceChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceChangeLogUpdateDto）
 */
export interface SalesPriceChangeLogUpdate extends SalesPriceChangeLogCreate {
  /** 对应后端字段 salesPriceChangeLogId */
  salesPriceChangeLogId: string
}
