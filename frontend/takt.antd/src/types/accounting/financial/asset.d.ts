// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/financial/asset
// 文件名称：asset.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：asset相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Asset类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAssetDto）
 */
export interface Asset extends TaktEntityBase {
  /** 对应后端字段 assetId */
  assetId: string
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 assetCode */
  assetCode: string
  /** 对应后端字段 assetName */
  assetName: string
  /** 对应后端字段 assetCategoryId */
  assetCategoryId: string
  /** 对应后端字段 assetCategoryName */
  assetCategoryName?: string
  /** 对应后端字段 assetType */
  assetType: number
  /** 对应后端字段 assetOriginalValue */
  assetOriginalValue: number
  /** 对应后端字段 assetNetValue */
  assetNetValue: number
  /** 对应后端字段 accumulatedDepreciation */
  accumulatedDepreciation: number
  /** 对应后端字段 costCenterId */
  costCenterId?: string
  /** 对应后端字段 costCenterName */
  costCenterName?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 assetLocation */
  assetLocation?: string
  /** 对应后端字段 purchaseDate */
  purchaseDate?: string
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 scrapDate */
  scrapDate?: string
  /** 对应后端字段 disposalDate */
  disposalDate?: string
  /** 对应后端字段 expectedLifeMonths */
  expectedLifeMonths: number
  /** 对应后端字段 depreciationMethod */
  depreciationMethod: number
  /** 对应后端字段 monthlyDepreciation */
  monthlyDepreciation: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 assetStatus */
  assetStatus: number
}

/**
 * AssetQuery类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAssetQueryDto）
 */
export interface AssetQuery extends TaktPagedQuery {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 assetCode */
  assetCode?: string
  /** 对应后端字段 assetName */
  assetName?: string
  /** 对应后端字段 assetCategoryId */
  assetCategoryId?: string
  /** 对应后端字段 assetCategoryName */
  assetCategoryName?: string
  /** 对应后端字段 assetType */
  assetType?: number
  /** 对应后端字段 assetOriginalValue */
  assetOriginalValue?: number
  /** 对应后端字段 assetNetValue */
  assetNetValue?: number
  /** 对应后端字段 accumulatedDepreciation */
  accumulatedDepreciation?: number
  /** 对应后端字段 costCenterId */
  costCenterId?: string
  /** 对应后端字段 costCenterName */
  costCenterName?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 assetLocation */
  assetLocation?: string
  /** 对应后端字段 purchaseDate */
  purchaseDate?: string
  /** 对应后端字段 purchaseDateStart */
  purchaseDateStart?: string
  /** 对应后端字段 purchaseDateEnd */
  purchaseDateEnd?: string
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 startDateStart */
  startDateStart?: string
  /** 对应后端字段 startDateEnd */
  startDateEnd?: string
  /** 对应后端字段 scrapDate */
  scrapDate?: string
  /** 对应后端字段 scrapDateStart */
  scrapDateStart?: string
  /** 对应后端字段 scrapDateEnd */
  scrapDateEnd?: string
  /** 对应后端字段 disposalDate */
  disposalDate?: string
  /** 对应后端字段 disposalDateStart */
  disposalDateStart?: string
  /** 对应后端字段 disposalDateEnd */
  disposalDateEnd?: string
  /** 对应后端字段 expectedLifeMonths */
  expectedLifeMonths?: number
  /** 对应后端字段 depreciationMethod */
  depreciationMethod?: number
  /** 对应后端字段 monthlyDepreciation */
  monthlyDepreciation?: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 assetStatus */
  assetStatus?: number
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
 * AssetCreate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAssetCreateDto）
 */
export interface AssetCreate {
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 assetCode */
  assetCode: string
  /** 对应后端字段 assetName */
  assetName: string
  /** 对应后端字段 assetCategoryId */
  assetCategoryId: string
  /** 对应后端字段 assetCategoryName */
  assetCategoryName?: string
  /** 对应后端字段 assetType */
  assetType: number
  /** 对应后端字段 assetOriginalValue */
  assetOriginalValue: number
  /** 对应后端字段 assetNetValue */
  assetNetValue: number
  /** 对应后端字段 accumulatedDepreciation */
  accumulatedDepreciation: number
  /** 对应后端字段 costCenterId */
  costCenterId?: string
  /** 对应后端字段 costCenterName */
  costCenterName?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 assetLocation */
  assetLocation?: string
  /** 对应后端字段 purchaseDate */
  purchaseDate?: string
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 scrapDate */
  scrapDate?: string
  /** 对应后端字段 disposalDate */
  disposalDate?: string
  /** 对应后端字段 expectedLifeMonths */
  expectedLifeMonths: number
  /** 对应后端字段 depreciationMethod */
  depreciationMethod: number
  /** 对应后端字段 monthlyDepreciation */
  monthlyDepreciation: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 assetStatus */
  assetStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AssetUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAssetUpdateDto）
 */
export interface AssetUpdate extends AssetCreate {
  /** 对应后端字段 assetId */
  assetId: string
}

/**
 * AssetStatus类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAssetStatusDto）
 */
export interface AssetStatus {
  /** 对应后端字段 assetId */
  assetId: string
  /** 对应后端字段 assetStatus */
  assetStatus: number
}
