// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/plant
// 文件名称：plant.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：plant相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Plant类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantDto）
 */
export interface Plant extends TaktEntityBase {
  /** 对应后端字段 plantId */
  plantId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 plantName */
  plantName: string
  /** 对应后端字段 plantShortName */
  plantShortName?: string
  /** 对应后端字段 registrationAddress */
  registrationAddress?: string
  /** 对应后端字段 registrationRegion */
  registrationRegion?: string
  /** 对应后端字段 registrationProvince */
  registrationProvince?: string
  /** 对应后端字段 registrationCity */
  registrationCity?: string
  /** 对应后端字段 businessRegion */
  businessRegion?: string
  /** 对应后端字段 businessProvince */
  businessProvince?: string
  /** 对应后端字段 businessCity */
  businessCity?: string
  /** 对应后端字段 businessAddress */
  businessAddress?: string
  /** 对应后端字段 plantAddress */
  plantAddress?: string
  /** 对应后端字段 plantPhone */
  plantPhone?: string
  /** 对应后端字段 plantEmail */
  plantEmail?: string
  /** 对应后端字段 plantManager */
  plantManager?: string
  /** 对应后端字段 enterpriseNature */
  enterpriseNature?: string
  /** 对应后端字段 industryAttribute */
  industryAttribute?: string
  /** 对应后端字段 enterpriseScale */
  enterpriseScale?: string
  /** 对应后端字段 businessScope */
  businessScope?: string
  /** 对应后端字段 relatedCompany */
  relatedCompany?: string
  /** 对应后端字段 plantStatus */
  plantStatus: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * PlantQuery类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantQueryDto）
 */
export interface PlantQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 plantName */
  plantName?: string
  /** 对应后端字段 plantShortName */
  plantShortName?: string
  /** 对应后端字段 registrationAddress */
  registrationAddress?: string
  /** 对应后端字段 registrationRegion */
  registrationRegion?: string
  /** 对应后端字段 registrationProvince */
  registrationProvince?: string
  /** 对应后端字段 registrationCity */
  registrationCity?: string
  /** 对应后端字段 businessRegion */
  businessRegion?: string
  /** 对应后端字段 businessProvince */
  businessProvince?: string
  /** 对应后端字段 businessCity */
  businessCity?: string
  /** 对应后端字段 businessAddress */
  businessAddress?: string
  /** 对应后端字段 plantAddress */
  plantAddress?: string
  /** 对应后端字段 plantPhone */
  plantPhone?: string
  /** 对应后端字段 plantEmail */
  plantEmail?: string
  /** 对应后端字段 plantManager */
  plantManager?: string
  /** 对应后端字段 enterpriseNature */
  enterpriseNature?: string
  /** 对应后端字段 industryAttribute */
  industryAttribute?: string
  /** 对应后端字段 enterpriseScale */
  enterpriseScale?: string
  /** 对应后端字段 businessScope */
  businessScope?: string
  /** 对应后端字段 relatedCompany */
  relatedCompany?: string
  /** 对应后端字段 plantStatus */
  plantStatus?: number
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
 * PlantCreate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantCreateDto）
 */
export interface PlantCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 plantName */
  plantName: string
  /** 对应后端字段 plantShortName */
  plantShortName?: string
  /** 对应后端字段 registrationAddress */
  registrationAddress?: string
  /** 对应后端字段 registrationRegion */
  registrationRegion?: string
  /** 对应后端字段 registrationProvince */
  registrationProvince?: string
  /** 对应后端字段 registrationCity */
  registrationCity?: string
  /** 对应后端字段 businessRegion */
  businessRegion?: string
  /** 对应后端字段 businessProvince */
  businessProvince?: string
  /** 对应后端字段 businessCity */
  businessCity?: string
  /** 对应后端字段 businessAddress */
  businessAddress?: string
  /** 对应后端字段 plantAddress */
  plantAddress?: string
  /** 对应后端字段 plantPhone */
  plantPhone?: string
  /** 对应后端字段 plantEmail */
  plantEmail?: string
  /** 对应后端字段 plantManager */
  plantManager?: string
  /** 对应后端字段 enterpriseNature */
  enterpriseNature?: string
  /** 对应后端字段 industryAttribute */
  industryAttribute?: string
  /** 对应后端字段 enterpriseScale */
  enterpriseScale?: string
  /** 对应后端字段 businessScope */
  businessScope?: string
  /** 对应后端字段 relatedCompany */
  relatedCompany?: string
  /** 对应后端字段 plantStatus */
  plantStatus: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PlantUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantUpdateDto）
 */
export interface PlantUpdate extends PlantCreate {
  /** 对应后端字段 plantId */
  plantId: string
}

/**
 * PlantStatus类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantStatusDto）
 */
export interface PlantStatus {
  /** 对应后端字段 plantId */
  plantId: string
  /** 对应后端字段 plantStatus */
  plantStatus: number
}

/**
 * PlantSort类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantSortDto）
 */
export interface PlantSort {
  /** 对应后端字段 plantId */
  plantId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
