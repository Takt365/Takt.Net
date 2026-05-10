// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/sales/sales-price-scale
// 文件名称：sales-price-scale.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：sales-price-scale相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SalesPriceScale类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceScaleDto）
 */
export interface SalesPriceScale extends TaktEntityBase {
  /** 对应后端字段 salesPriceScaleId */
  salesPriceScaleId: string
  /** 对应后端字段 itemId */
  itemId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 startQuantity */
  startQuantity: number
  /** 对应后端字段 endQuantity */
  endQuantity: number
  /** 对应后端字段 scalePrice */
  scalePrice: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 priceItem */
  priceItem?: unknown
}

/**
 * SalesPriceScaleQuery类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceScaleQueryDto）
 */
export interface SalesPriceScaleQuery extends TaktPagedQuery {
  /** 对应后端字段 itemId */
  itemId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 startQuantity */
  startQuantity?: number
  /** 对应后端字段 endQuantity */
  endQuantity?: number
  /** 对应后端字段 scalePrice */
  scalePrice?: number
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
 * SalesPriceScaleCreate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceScaleCreateDto）
 */
export interface SalesPriceScaleCreate {
  /** 对应后端字段 itemId */
  itemId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 startQuantity */
  startQuantity: number
  /** 对应后端字段 endQuantity */
  endQuantity: number
  /** 对应后端字段 scalePrice */
  scalePrice: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * SalesPriceScaleUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceScaleUpdateDto）
 */
export interface SalesPriceScaleUpdate extends SalesPriceScaleCreate {
  /** 对应后端字段 salesPriceScaleId */
  salesPriceScaleId: string
}

/**
 * SalesPriceScaleSort类型（对应后端 Takt.Application.Dtos.Logistics.Sales.TaktSalesPriceScaleSortDto）
 */
export interface SalesPriceScaleSort {
  /** 对应后端字段 salesPriceScaleId */
  salesPriceScaleId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
