// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/plant
// 文件名称：plant.d.ts
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：工厂相关类型定义，对应后端 TaktPlantDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktPagedQuery } from '@/types/common'

export interface Plant extends Record<string, any> {
  plantId: string
  plantCode: string
  plantName: string
  plantName2?: string
  shortName?: string
  region?: string
  province?: string
  city?: string
  county?: string
  townStreet?: string
  village?: string
  address?: string
  localCurrency?: string
  languageCode?: string
  chartOfAccounts: string
  controllingArea?: string
  salesOrg?: string
  plantPhone?: string
  plantEmail?: string
  plantFax?: string
  plantWebsite?: string
  unifiedSocialCreditCode?: string
  taxRegistrationNumber?: string
  vatRegistrationNumber?: string
  legalRepresentative?: string
  industryType?: string
  enterpriseRegistrationType?: string
  enterpriseSize?: number
  registeredCapital: number
  establishmentDate?: string
  dissolutionDate?: string
  plantStatus: number
  orderNum: number
  configId?: string
  remark?: string
  createBy?: string
  createTime?: string
  updateBy?: string
  updateTime?: string
}

export interface PlantQuery extends TaktPagedQuery {
  keyWords?: string
  plantCode?: string
  plantName?: string
  plantStatus?: number
}

export interface PlantCreate {
  plantCode: string
  plantName: string
  plantName2?: string
  shortName?: string
  region?: string
  province?: string
  city?: string
  county?: string
  townStreet?: string
  village?: string
  address?: string
  localCurrency?: string
  languageCode?: string
  chartOfAccounts?: string
  controllingArea?: string
  salesOrg?: string
  plantPhone?: string
  plantEmail?: string
  plantFax?: string
  plantWebsite?: string
  unifiedSocialCreditCode?: string
  taxRegistrationNumber?: string
  vatRegistrationNumber?: string
  legalRepresentative?: string
  industryType?: string
  enterpriseRegistrationType?: string
  enterpriseSize?: number
  registeredCapital?: number
  establishmentDate?: string
  dissolutionDate?: string
  orderNum?: number
  remark?: string
}

export interface PlantUpdate extends PlantCreate {
  plantId: string
}

export interface PlantStatus {
  plantId: string
  plantStatus: number
}
