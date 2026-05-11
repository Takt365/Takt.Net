// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/purchase-price-scale
// 文件名称：purchase-price-scale.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：purchase-price-scale相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PurchasePriceScale类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceScaleDto）
 */
export interface PurchasePriceScale extends TaktEntityBase {
  /** 对应后端字段 purchasePriceScaleId */
  purchasePriceScaleId: string
  /** 对应后端字段 purchasePriceItemId */
  purchasePriceItemId: string
  /** 对应后端字段 purchasePriceCode */
  purchasePriceCode: string
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
}

/**
 * PurchasePriceScaleQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceScaleQueryDto）
 */
export interface PurchasePriceScaleQuery extends TaktPagedQuery {
  /** 对应后端字段 purchasePriceItemId */
  purchasePriceItemId?: string
  /** 对应后端字段 purchasePriceCode */
  purchasePriceCode?: string
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
 * PurchasePriceScaleCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceScaleCreateDto）
 */
export interface PurchasePriceScaleCreate {
  /** 对应后端字段 purchasePriceItemId */
  purchasePriceItemId: string
  /** 对应后端字段 purchasePriceCode */
  purchasePriceCode: string
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
 * PurchasePriceScaleUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceScaleUpdateDto）
 */
export interface PurchasePriceScaleUpdate extends PurchasePriceScaleCreate {
  /** 对应后端字段 purchasePriceScaleId */
  purchasePriceScaleId: string
}

/**
 * PurchasePriceScaleSort类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPurchasePriceScaleSortDto）
 */
export interface PurchasePriceScaleSort {
  /** 对应后端字段 purchasePriceScaleId */
  purchasePriceScaleId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
