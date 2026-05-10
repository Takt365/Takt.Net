// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/sales/sales-price-item
// 文件名称：sales-price-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：sales-price-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SalesPriceItem类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceItemDto）
 */
export interface SalesPriceItem extends TaktEntityBase {
  /** 对应后端字段 salesPriceItemId */
  salesPriceItemId: string
  /** 对应后端字段 salesPriceId */
  salesPriceId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 salesUnit */
  salesUnit: string
  /** 对应后端字段 salesPrice */
  salesPrice: number
  /** 对应后端字段 minOrderQuantity */
  minOrderQuantity: number
  /** 对应后端字段 maxOrderQuantity */
  maxOrderQuantity: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 scales */
  scales?: unknown[]
  /** 对应后端字段 price */
  price?: unknown
}

/**
 * SalesPriceItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceItemQueryDto）
 */
export interface SalesPriceItemQuery extends TaktPagedQuery {
  /** 对应后端字段 salesPriceId */
  salesPriceId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 salesUnit */
  salesUnit?: string
  /** 对应后端字段 salesPrice */
  salesPrice?: number
  /** 对应后端字段 minOrderQuantity */
  minOrderQuantity?: number
  /** 对应后端字段 maxOrderQuantity */
  maxOrderQuantity?: number
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
 * SalesPriceItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceItemCreateDto）
 */
export interface SalesPriceItemCreate {
  /** 对应后端字段 salesPriceId */
  salesPriceId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 salesUnit */
  salesUnit: string
  /** 对应后端字段 salesPrice */
  salesPrice: number
  /** 对应后端字段 minOrderQuantity */
  minOrderQuantity: number
  /** 对应后端字段 maxOrderQuantity */
  maxOrderQuantity: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 scales */
  scales?: unknown[]
}

/**
 * SalesPriceItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceItemUpdateDto）
 */
export interface SalesPriceItemUpdate extends SalesPriceItemCreate {
  /** 对应后端字段 salesPriceItemId */
  salesPriceItemId: string
}

/**
 * SalesPriceItemSort类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceItemSortDto）
 */
export interface SalesPriceItemSort {
  /** 对应后端字段 salesPriceItemId */
  salesPriceItemId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
