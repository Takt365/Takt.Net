// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/plant-material
// 文件名称：plant-material.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：plant-material相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PlantMaterial类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantMaterialDto）
 */
export interface PlantMaterial extends TaktEntityBase {
  /** 对应后端字段 plantMaterialId */
  plantMaterialId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 materialDescription */
  materialDescription?: string
  /** 对应后端字段 industrySector */
  industrySector?: string
  /** 对应后端字段 materialHierarchy */
  materialHierarchy?: string
  /** 对应后端字段 materialGroupCode */
  materialGroupCode?: string
  /** 对应后端字段 materialType */
  materialType: number
  /** 对应后端字段 materialModel */
  materialModel?: string
  /** 对应后端字段 materialBrand */
  materialBrand?: string
  /** 对应后端字段 baseUnit */
  baseUnit: string
  /** 对应后端字段 purchaseGroup */
  purchaseGroup?: string
  /** 对应后端字段 purchaseType */
  purchaseType: number
  /** 对应后端字段 specialProcurement */
  specialProcurement: number
  /** 对应后端字段 isBulk */
  isBulk: number
  /** 对应后端字段 minOrderQuantity */
  minOrderQuantity: number
  /** 对应后端字段 roundingValue */
  roundingValue: number
  /** 对应后端字段 plannedDeliveryTimeDays */
  plannedDeliveryTimeDays: number
  /** 对应后端字段 inHouseProductionDays */
  inHouseProductionDays: number
  /** 对应后端字段 manufacturer */
  manufacturer?: string
  /** 对应后端字段 manufacturerPartNumber */
  manufacturerPartNumber?: string
  /** 对应后端字段 currencyCode */
  currencyCode: string
  /** 对应后端字段 priceControl */
  priceControl: number
  /** 对应后端字段 priceUnit */
  priceUnit: number
  /** 对应后端字段 valuationCategory */
  valuationCategory?: string
  /** 对应后端字段 differenceCode */
  differenceCode?: string
  /** 对应后端字段 profitCenter */
  profitCenter?: string
  /** 对应后端字段 latestPurchasePrice */
  latestPurchasePrice: number
  /** 对应后端字段 salesPrice */
  salesPrice: number
  /** 对应后端字段 safetyStock */
  safetyStock: number
  /** 对应后端字段 maxStock */
  maxStock: number
  /** 对应后端字段 minStock */
  minStock: number
  /** 对应后端字段 currentStock */
  currentStock: number
  /** 对应后端字段 productionLocation */
  productionLocation?: string
  /** 对应后端字段 purchasingLocation */
  purchasingLocation?: string
  /** 对应后端字段 inspectionRequired */
  inspectionRequired: number
  /** 对应后端字段 isBatch */
  isBatch: number
  /** 对应后端字段 isExpiry */
  isExpiry: number
  /** 对应后端字段 expiryDays */
  expiryDays: number
  /** 对应后端字段 materialStatus */
  materialStatus: number
  /** 对应后端字段 materialAttributes */
  materialAttributes?: string
  /** 对应后端字段 isEndOfLife */
  isEndOfLife?: string
  /** 对应后端字段 endOfLifeDate */
  endOfLifeDate?: string
}

/**
 * PlantMaterialQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantMaterialQueryDto）
 */
export interface PlantMaterialQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 materialDescription */
  materialDescription?: string
  /** 对应后端字段 industrySector */
  industrySector?: string
  /** 对应后端字段 materialHierarchy */
  materialHierarchy?: string
  /** 对应后端字段 materialGroupCode */
  materialGroupCode?: string
  /** 对应后端字段 materialType */
  materialType?: number
  /** 对应后端字段 materialModel */
  materialModel?: string
  /** 对应后端字段 materialBrand */
  materialBrand?: string
  /** 对应后端字段 baseUnit */
  baseUnit?: string
  /** 对应后端字段 purchaseGroup */
  purchaseGroup?: string
  /** 对应后端字段 purchaseType */
  purchaseType?: number
  /** 对应后端字段 specialProcurement */
  specialProcurement?: number
  /** 对应后端字段 isBulk */
  isBulk?: number
  /** 对应后端字段 minOrderQuantity */
  minOrderQuantity?: number
  /** 对应后端字段 roundingValue */
  roundingValue?: number
  /** 对应后端字段 plannedDeliveryTimeDays */
  plannedDeliveryTimeDays?: number
  /** 对应后端字段 inHouseProductionDays */
  inHouseProductionDays?: number
  /** 对应后端字段 manufacturer */
  manufacturer?: string
  /** 对应后端字段 manufacturerPartNumber */
  manufacturerPartNumber?: string
  /** 对应后端字段 currencyCode */
  currencyCode?: string
  /** 对应后端字段 priceControl */
  priceControl?: number
  /** 对应后端字段 priceUnit */
  priceUnit?: number
  /** 对应后端字段 valuationCategory */
  valuationCategory?: string
  /** 对应后端字段 differenceCode */
  differenceCode?: string
  /** 对应后端字段 profitCenter */
  profitCenter?: string
  /** 对应后端字段 latestPurchasePrice */
  latestPurchasePrice?: number
  /** 对应后端字段 salesPrice */
  salesPrice?: number
  /** 对应后端字段 safetyStock */
  safetyStock?: number
  /** 对应后端字段 maxStock */
  maxStock?: number
  /** 对应后端字段 minStock */
  minStock?: number
  /** 对应后端字段 currentStock */
  currentStock?: number
  /** 对应后端字段 productionLocation */
  productionLocation?: string
  /** 对应后端字段 purchasingLocation */
  purchasingLocation?: string
  /** 对应后端字段 inspectionRequired */
  inspectionRequired?: number
  /** 对应后端字段 isBatch */
  isBatch?: number
  /** 对应后端字段 isExpiry */
  isExpiry?: number
  /** 对应后端字段 expiryDays */
  expiryDays?: number
  /** 对应后端字段 materialStatus */
  materialStatus?: number
  /** 对应后端字段 materialAttributes */
  materialAttributes?: string
  /** 对应后端字段 isEndOfLife */
  isEndOfLife?: string
  /** 对应后端字段 endOfLifeDate */
  endOfLifeDate?: string
  /** 对应后端字段 endOfLifeDateStart */
  endOfLifeDateStart?: string
  /** 对应后端字段 endOfLifeDateEnd */
  endOfLifeDateEnd?: string
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
 * PlantMaterialCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantMaterialCreateDto）
 */
export interface PlantMaterialCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 materialSpecification */
  materialSpecification?: string
  /** 对应后端字段 materialDescription */
  materialDescription?: string
  /** 对应后端字段 industrySector */
  industrySector?: string
  /** 对应后端字段 materialHierarchy */
  materialHierarchy?: string
  /** 对应后端字段 materialGroupCode */
  materialGroupCode?: string
  /** 对应后端字段 materialType */
  materialType: number
  /** 对应后端字段 materialModel */
  materialModel?: string
  /** 对应后端字段 materialBrand */
  materialBrand?: string
  /** 对应后端字段 baseUnit */
  baseUnit: string
  /** 对应后端字段 purchaseGroup */
  purchaseGroup?: string
  /** 对应后端字段 purchaseType */
  purchaseType: number
  /** 对应后端字段 specialProcurement */
  specialProcurement: number
  /** 对应后端字段 isBulk */
  isBulk: number
  /** 对应后端字段 minOrderQuantity */
  minOrderQuantity: number
  /** 对应后端字段 roundingValue */
  roundingValue: number
  /** 对应后端字段 plannedDeliveryTimeDays */
  plannedDeliveryTimeDays: number
  /** 对应后端字段 inHouseProductionDays */
  inHouseProductionDays: number
  /** 对应后端字段 manufacturer */
  manufacturer?: string
  /** 对应后端字段 manufacturerPartNumber */
  manufacturerPartNumber?: string
  /** 对应后端字段 currencyCode */
  currencyCode: string
  /** 对应后端字段 priceControl */
  priceControl: number
  /** 对应后端字段 priceUnit */
  priceUnit: number
  /** 对应后端字段 valuationCategory */
  valuationCategory?: string
  /** 对应后端字段 differenceCode */
  differenceCode?: string
  /** 对应后端字段 profitCenter */
  profitCenter?: string
  /** 对应后端字段 latestPurchasePrice */
  latestPurchasePrice: number
  /** 对应后端字段 salesPrice */
  salesPrice: number
  /** 对应后端字段 safetyStock */
  safetyStock: number
  /** 对应后端字段 maxStock */
  maxStock: number
  /** 对应后端字段 minStock */
  minStock: number
  /** 对应后端字段 currentStock */
  currentStock: number
  /** 对应后端字段 productionLocation */
  productionLocation?: string
  /** 对应后端字段 purchasingLocation */
  purchasingLocation?: string
  /** 对应后端字段 inspectionRequired */
  inspectionRequired: number
  /** 对应后端字段 isBatch */
  isBatch: number
  /** 对应后端字段 isExpiry */
  isExpiry: number
  /** 对应后端字段 expiryDays */
  expiryDays: number
  /** 对应后端字段 materialStatus */
  materialStatus: number
  /** 对应后端字段 materialAttributes */
  materialAttributes?: string
  /** 对应后端字段 isEndOfLife */
  isEndOfLife?: string
  /** 对应后端字段 endOfLifeDate */
  endOfLifeDate?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PlantMaterialUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantMaterialUpdateDto）
 */
export interface PlantMaterialUpdate extends PlantMaterialCreate {
  /** 对应后端字段 plantMaterialId */
  plantMaterialId: string
}

/**
 * PlantMaterialMaterialStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantMaterialMaterialStatusDto）
 */
export interface PlantMaterialMaterialStatus {
  /** 对应后端字段 plantMaterialId */
  plantMaterialId: string
  /** 对应后端字段 materialStatus */
  materialStatus: number
}
