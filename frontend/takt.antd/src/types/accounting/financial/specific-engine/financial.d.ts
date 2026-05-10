// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/financial/specific-engine/financial
// 文件名称：financial.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：financial相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AccountingTitleValidity类型（对应后端 Takt.Application.Dtos.Accounting.Financial.SpecificEngine.TaktAccountingTitleValidityDto）
 */
export interface AccountingTitleValidity {
  /** 对应后端字段 accountingTitleId */
  accountingTitleId: string
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
}

/**
 * AssetDepreciationMethod类型（对应后端 Takt.Application.Dtos.Accounting.Financial.SpecificEngine.TaktAssetDepreciationMethodDto）
 */
export interface AssetDepreciationMethod {
  /** 对应后端字段 assetId */
  assetId: string
  /** 对应后端字段 depreciationMethod */
  depreciationMethod: number
}

/**
 * AssetDisposalDate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.SpecificEngine.TaktAssetDisposalDateDto）
 */
export interface AssetDisposalDate {
  /** 对应后端字段 assetId */
  assetId: string
  /** 对应后端字段 disposalDate */
  disposalDate: string
}

/**
 * AssetLocation类型（对应后端 Takt.Application.Dtos.Accounting.Financial.SpecificEngine.TaktAssetLocationDto）
 */
export interface AssetLocation {
  /** 对应后端字段 assetId */
  assetId: string
  /** 对应后端字段 location */
  location: string
}

/**
 * AssetPurchaseDate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.SpecificEngine.TaktAssetPurchaseDateDto）
 */
export interface AssetPurchaseDate {
  /** 对应后端字段 assetId */
  assetId: string
  /** 对应后端字段 purchaseDate */
  purchaseDate: string
}

/**
 * AssetScrapDate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.SpecificEngine.TaktAssetScrapDateDto）
 */
export interface AssetScrapDate {
  /** 对应后端字段 assetId */
  assetId: string
  /** 对应后端字段 scrapDate */
  scrapDate: string
}

/**
 * AssetStartDate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.SpecificEngine.TaktAssetStartDateDto）
 */
export interface AssetStartDate {
  /** 对应后端字段 assetId */
  assetId: string
  /** 对应后端字段 startDate */
  startDate: string
}
