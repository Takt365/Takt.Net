// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/sales/sales-price
// 文件名称：sales-price.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：sales-price相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SalesPrice类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceDto）
 */
export interface SalesPrice extends TaktEntityBase {
  /** 对应后端字段 salesPriceId */
  salesPriceId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 salesPriceCode */
  salesPriceCode: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 priceType */
  priceType: number
  /** 对应后端字段 effectiveStartDate */
  effectiveStartDate: string
  /** 对应后端字段 effectiveEndDate */
  effectiveEndDate?: string
  /** 对应后端字段 priceStatus */
  priceStatus: number
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * SalesPriceQuery类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceQueryDto）
 */
export interface SalesPriceQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 salesPriceCode */
  salesPriceCode?: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 priceType */
  priceType?: number
  /** 对应后端字段 effectiveStartDate */
  effectiveStartDate?: string
  /** 对应后端字段 effectiveStartDateStart */
  effectiveStartDateStart?: string
  /** 对应后端字段 effectiveStartDateEnd */
  effectiveStartDateEnd?: string
  /** 对应后端字段 effectiveEndDate */
  effectiveEndDate?: string
  /** 对应后端字段 effectiveEndDateStart */
  effectiveEndDateStart?: string
  /** 对应后端字段 effectiveEndDateEnd */
  effectiveEndDateEnd?: string
  /** 对应后端字段 priceStatus */
  priceStatus?: number
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
 * SalesPriceCreate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceCreateDto）
 */
export interface SalesPriceCreate {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 salesPriceCode */
  salesPriceCode: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 priceType */
  priceType: number
  /** 对应后端字段 effectiveStartDate */
  effectiveStartDate: string
  /** 对应后端字段 effectiveEndDate */
  effectiveEndDate?: string
  /** 对应后端字段 priceStatus */
  priceStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * SalesPriceUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceUpdateDto）
 */
export interface SalesPriceUpdate extends SalesPriceCreate {
  /** 对应后端字段 salesPriceId */
  salesPriceId: string
}

/**
 * SalesPricePriceStatus类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPricePriceStatusDto）
 */
export interface SalesPricePriceStatus {
  /** 对应后端字段 salesPriceId */
  salesPriceId: string
  /** 对应后端字段 priceStatus */
  priceStatus: number
}
