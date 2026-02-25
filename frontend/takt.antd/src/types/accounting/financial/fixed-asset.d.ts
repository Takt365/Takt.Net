// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/financial/fixed-asset
// 文件名称：fixed-asset.d.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：固定资产相关类型定义，对应后端 Takt.Application.Dtos.Accounting.Financial.TaktFixedAssetsDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktPagedQuery } from '@/types/common'

/**
 * 固定资产类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktFixedAssetDto）
 */
export interface FixedAsset {
  /** 固定资产ID（对应后端 FixedAssetsId，序列化为string以避免Javascript精度问题） */
  fixedAssetsId: string
  /** 资产编码 */
  assetCode: string
  /** 资产名称 */
  assetName: string
  /** 资产分类ID（序列化为string） */
  assetCategoryId: string
  /** 资产分类名称 */
  assetCategoryName?: string
  /** 资产类型 */
  assetType: number
  /** 资产原值 */
  assetOriginalValue: number
  /** 资产净值 */
  assetNetValue: number
  /** 累计折旧 */
  accumulatedDepreciation: number
  /** 成本中心ID（序列化为string） */
  costCenterId?: string
  /** 成本中心名称 */
  costCenterName?: string
  /** 部门ID（序列化为string） */
  deptId?: string
  /** 部门名称 */
  deptName?: string
  /** 使用人ID（序列化为string） */
  userId?: string
  /** 使用人姓名 */
  userName?: string
  /** 资产位置 */
  assetLocation?: string
  /** 购置日期 */
  purchaseDate?: string
  /** 启用日期 */
  startDate?: string
  /** 报废日期 */
  scrapDate?: string
  /** 处置日期 */
  disposalDate?: string
  /** 预计使用月数 */
  expectedLifeMonths: number
  /** 折旧方法 */
  depreciationMethod: number
  /** 月折旧额 */
  monthlyDepreciation: number
  /** 资产状态 */
  assetStatus: number
  /** 租户配置ID（ConfigId） */
  configId?: string
  /** 备注 */
  remark?: string
  /** 创建人（用户名） */
  createBy?: string
  /** 创建时间 */
  createTime?: string
  /** 更新人（用户名） */
  updateBy?: string
  /** 更新时间 */
  updateTime?: string
}

/**
 * 固定资产查询类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktFixedAssetQueryDto）
 */
export interface FixedAssetQuery extends TaktPagedQuery {
  /** 关键词 */
  keyWords?: string
  /** 资产编码 */
  assetCode?: string
  /** 资产名称 */
  assetName?: string
  /** 资产分类ID */
  assetCategoryId?: string
  /** 资产类型 */
  assetType?: number
  /** 资产状态 */
  assetStatus?: number
}

/**
 * 创建固定资产类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktFixedAssetCreateDto）
 */
export interface FixedAssetCreate {
  /** 资产编码 */
  assetCode: string
  /** 资产名称 */
  assetName: string
  /** 资产分类ID */
  assetCategoryId: string
  /** 资产分类名称 */
  assetCategoryName?: string
  /** 资产类型 */
  assetType?: number
  /** 资产原值 */
  assetOriginalValue?: number
  /** 资产净值 */
  assetNetValue?: number
  /** 累计折旧 */
  accumulatedDepreciation?: number
  /** 成本中心ID */
  costCenterId?: string
  /** 成本中心名称 */
  costCenterName?: string
  /** 部门ID */
  deptId?: string
  /** 部门名称 */
  deptName?: string
  /** 使用人ID */
  userId?: string
  /** 使用人姓名 */
  userName?: string
  /** 资产位置 */
  assetLocation?: string
  /** 购置日期 */
  purchaseDate?: string
  /** 启用日期 */
  startDate?: string
  /** 预计使用月数 */
  expectedLifeMonths?: number
  /** 折旧方法 */
  depreciationMethod?: number
  /** 月折旧额 */
  monthlyDepreciation?: number
  /** 资产状态 */
  assetStatus?: number
  /** 备注 */
  remark?: string
}

/**
 * 更新固定资产类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktFixedAssetUpdateDto）
 */
export interface FixedAssetUpdate extends FixedAssetCreate {
  /** 固定资产ID（序列化为string） */
  fixedAssetsId: string
}

/**
 * 固定资产状态类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktFixedAssetStatusDto）
 */
export interface FixedAssetStatus {
  /** 固定资产ID（序列化为string） */
  fixedAssetsId: string
  /** 资产状态 */
  assetStatus: number
}
