// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/bom/packaging
// 文件名称：packaging.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：packaging相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Packaging类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktPackagingDto）
 */
export interface Packaging extends TaktEntityBase {
  /** 对应后端字段 packagingId */
  packagingId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 hsCode */
  hsCode?: string
  /** 对应后端字段 hsName */
  hsName?: string
  /** 对应后端字段 additionalCode */
  additionalCode?: string
  /** 对应后端字段 originCountryRegionCode */
  originCountryRegionCode?: string
  /** 对应后端字段 originCountryRegionName */
  originCountryRegionName?: string
  /** 对应后端字段 destinationCountryRegionCode */
  destinationCountryRegionCode?: string
  /** 对应后端字段 destinationCountryRegionName */
  destinationCountryRegionName?: string
  /** 对应后端字段 regulatoryConditionCode */
  regulatoryConditionCode?: string
  /** 对应后端字段 tariffRateType */
  tariffRateType?: string
  /** 对应后端字段 grossWeight */
  grossWeight?: number
  /** 对应后端字段 netWeight */
  netWeight?: number
  /** 对应后端字段 weightUnit */
  weightUnit: string
  /** 对应后端字段 businessVolume */
  businessVolume?: number
  /** 对应后端字段 volumeUnit */
  volumeUnit: string
  /** 对应后端字段 sizeDimension */
  sizeDimension?: string
  /** 对应后端字段 packagingType */
  packagingType: string
  /** 对应后端字段 packingUnit */
  packingUnit: string
  /** 对应后端字段 quantityPerPacking */
  quantityPerPacking?: number
  /** 对应后端字段 packagingSpec */
  packagingSpec?: string
  /** 对应后端字段 packagingDescription */
  packagingDescription?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * PackagingQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktPackagingQueryDto）
 */
export interface PackagingQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 hsCode */
  hsCode?: string
  /** 对应后端字段 hsName */
  hsName?: string
  /** 对应后端字段 additionalCode */
  additionalCode?: string
  /** 对应后端字段 originCountryRegionCode */
  originCountryRegionCode?: string
  /** 对应后端字段 originCountryRegionName */
  originCountryRegionName?: string
  /** 对应后端字段 destinationCountryRegionCode */
  destinationCountryRegionCode?: string
  /** 对应后端字段 destinationCountryRegionName */
  destinationCountryRegionName?: string
  /** 对应后端字段 regulatoryConditionCode */
  regulatoryConditionCode?: string
  /** 对应后端字段 tariffRateType */
  tariffRateType?: string
  /** 对应后端字段 grossWeight */
  grossWeight?: number
  /** 对应后端字段 netWeight */
  netWeight?: number
  /** 对应后端字段 weightUnit */
  weightUnit?: string
  /** 对应后端字段 businessVolume */
  businessVolume?: number
  /** 对应后端字段 volumeUnit */
  volumeUnit?: string
  /** 对应后端字段 sizeDimension */
  sizeDimension?: string
  /** 对应后端字段 packagingType */
  packagingType?: string
  /** 对应后端字段 packingUnit */
  packingUnit?: string
  /** 对应后端字段 quantityPerPacking */
  quantityPerPacking?: number
  /** 对应后端字段 packagingSpec */
  packagingSpec?: string
  /** 对应后端字段 packagingDescription */
  packagingDescription?: string
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
 * PackagingCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktPackagingCreateDto）
 */
export interface PackagingCreate {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 hsCode */
  hsCode?: string
  /** 对应后端字段 hsName */
  hsName?: string
  /** 对应后端字段 additionalCode */
  additionalCode?: string
  /** 对应后端字段 originCountryRegionCode */
  originCountryRegionCode?: string
  /** 对应后端字段 originCountryRegionName */
  originCountryRegionName?: string
  /** 对应后端字段 destinationCountryRegionCode */
  destinationCountryRegionCode?: string
  /** 对应后端字段 destinationCountryRegionName */
  destinationCountryRegionName?: string
  /** 对应后端字段 regulatoryConditionCode */
  regulatoryConditionCode?: string
  /** 对应后端字段 tariffRateType */
  tariffRateType?: string
  /** 对应后端字段 grossWeight */
  grossWeight?: number
  /** 对应后端字段 netWeight */
  netWeight?: number
  /** 对应后端字段 weightUnit */
  weightUnit: string
  /** 对应后端字段 businessVolume */
  businessVolume?: number
  /** 对应后端字段 volumeUnit */
  volumeUnit: string
  /** 对应后端字段 sizeDimension */
  sizeDimension?: string
  /** 对应后端字段 packagingType */
  packagingType: string
  /** 对应后端字段 packingUnit */
  packingUnit: string
  /** 对应后端字段 quantityPerPacking */
  quantityPerPacking?: number
  /** 对应后端字段 packagingSpec */
  packagingSpec?: string
  /** 对应后端字段 packagingDescription */
  packagingDescription?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PackagingUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktPackagingUpdateDto）
 */
export interface PackagingUpdate extends PackagingCreate {
  /** 对应后端字段 packagingId */
  packagingId: string
}

/**
 * PackagingSort类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktPackagingSortDto）
 */
export interface PackagingSort {
  /** 对应后端字段 packagingId */
  packagingId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
