// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/sampling-scheme
// 文件名称：sampling-scheme.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：sampling-scheme相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SamplingScheme类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktSamplingSchemeDto）
 */
export interface SamplingScheme extends TaktEntityBase {
  /** 对应后端字段 samplingSchemeId */
  samplingSchemeId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 schemeCode */
  schemeCode: string
  /** 对应后端字段 schemeName */
  schemeName: string
  /** 对应后端字段 schemeType */
  schemeType: number
  /** 对应后端字段 samplingStandard */
  samplingStandard: number
  /** 对应后端字段 inspectionLevel */
  inspectionLevel: number
  /** 对应后端字段 aqlValue */
  aqlValue: number
  /** 对应后端字段 lotSizeMin */
  lotSizeMin: number
  /** 对应后端字段 lotSizeMax */
  lotSizeMax: number
  /** 对应后端字段 sampleSize */
  sampleSize: number
  /** 对应后端字段 acceptanceNumber */
  acceptanceNumber: number
  /** 对应后端字段 rejectionNumber */
  rejectionNumber: number
  /** 对应后端字段 inspectionStrictness */
  inspectionStrictness: number
  /** 对应后端字段 isTransferRuleEnabled */
  isTransferRuleEnabled: number
  /** 对应后端字段 transferRuleConfig */
  transferRuleConfig?: string
  /** 对应后端字段 isEnabled */
  isEnabled: number
  /** 对应后端字段 schemeStatus */
  schemeStatus: number
  /** 对应后端字段 schemeDescription */
  schemeDescription?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 inspectionStandards */
  inspectionStandards?: unknown[]
}

/**
 * SamplingSchemeQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktSamplingSchemeQueryDto）
 */
export interface SamplingSchemeQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 schemeCode */
  schemeCode?: string
  /** 对应后端字段 schemeName */
  schemeName?: string
  /** 对应后端字段 schemeType */
  schemeType?: number
  /** 对应后端字段 samplingStandard */
  samplingStandard?: number
  /** 对应后端字段 inspectionLevel */
  inspectionLevel?: number
  /** 对应后端字段 aqlValue */
  aqlValue?: number
  /** 对应后端字段 lotSizeMin */
  lotSizeMin?: number
  /** 对应后端字段 lotSizeMax */
  lotSizeMax?: number
  /** 对应后端字段 sampleSize */
  sampleSize?: number
  /** 对应后端字段 acceptanceNumber */
  acceptanceNumber?: number
  /** 对应后端字段 rejectionNumber */
  rejectionNumber?: number
  /** 对应后端字段 inspectionStrictness */
  inspectionStrictness?: number
  /** 对应后端字段 isTransferRuleEnabled */
  isTransferRuleEnabled?: number
  /** 对应后端字段 transferRuleConfig */
  transferRuleConfig?: string
  /** 对应后端字段 isEnabled */
  isEnabled?: number
  /** 对应后端字段 schemeStatus */
  schemeStatus?: number
  /** 对应后端字段 schemeDescription */
  schemeDescription?: string
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
 * SamplingSchemeCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktSamplingSchemeCreateDto）
 */
export interface SamplingSchemeCreate {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 schemeCode */
  schemeCode: string
  /** 对应后端字段 schemeName */
  schemeName: string
  /** 对应后端字段 schemeType */
  schemeType: number
  /** 对应后端字段 samplingStandard */
  samplingStandard: number
  /** 对应后端字段 inspectionLevel */
  inspectionLevel: number
  /** 对应后端字段 aqlValue */
  aqlValue: number
  /** 对应后端字段 lotSizeMin */
  lotSizeMin: number
  /** 对应后端字段 lotSizeMax */
  lotSizeMax: number
  /** 对应后端字段 sampleSize */
  sampleSize: number
  /** 对应后端字段 acceptanceNumber */
  acceptanceNumber: number
  /** 对应后端字段 rejectionNumber */
  rejectionNumber: number
  /** 对应后端字段 inspectionStrictness */
  inspectionStrictness: number
  /** 对应后端字段 isTransferRuleEnabled */
  isTransferRuleEnabled: number
  /** 对应后端字段 transferRuleConfig */
  transferRuleConfig?: string
  /** 对应后端字段 isEnabled */
  isEnabled: number
  /** 对应后端字段 schemeStatus */
  schemeStatus: number
  /** 对应后端字段 schemeDescription */
  schemeDescription?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 inspectionStandards */
  inspectionStandards?: unknown[]
}

/**
 * SamplingSchemeUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktSamplingSchemeUpdateDto）
 */
export interface SamplingSchemeUpdate extends SamplingSchemeCreate {
  /** 对应后端字段 samplingSchemeId */
  samplingSchemeId: string
}

/**
 * SamplingSchemeSort类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktSamplingSchemeSortDto）
 */
export interface SamplingSchemeSort {
  /** 对应后端字段 samplingSchemeId */
  samplingSchemeId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * SamplingSchemeSchemeStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktSamplingSchemeSchemeStatusDto）
 */
export interface SamplingSchemeSchemeStatus {
  /** 对应后端字段 samplingSchemeId */
  samplingSchemeId: string
  /** 对应后端字段 schemeStatus */
  schemeStatus: number
}
