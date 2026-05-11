// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/inspection-standard
// 文件名称：inspection-standard.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：inspection-standard相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * InspectionStandard类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktInspectionStandardDto）
 */
export interface InspectionStandard extends TaktEntityBase {
  /** 对应后端字段 inspectionStandardId */
  inspectionStandardId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 standardCode */
  standardCode: string
  /** 对应后端字段 standardName */
  standardName: string
  /** 对应后端字段 inspectionType */
  inspectionType: number
  /** 对应后端字段 materialCategoryCode */
  materialCategoryCode: string
  /** 对应后端字段 materialCategoryName */
  materialCategoryName: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode?: string
  /** 对应后端字段 samplingSchemeName */
  samplingSchemeName?: string
  /** 对应后端字段 isEnabled */
  isEnabled: number
  /** 对应后端字段 standardStatus */
  standardStatus: number
  /** 对应后端字段 standardDescription */
  standardDescription?: string
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * InspectionStandardQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktInspectionStandardQueryDto）
 */
export interface InspectionStandardQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 standardCode */
  standardCode?: string
  /** 对应后端字段 standardName */
  standardName?: string
  /** 对应后端字段 inspectionType */
  inspectionType?: number
  /** 对应后端字段 materialCategoryCode */
  materialCategoryCode?: string
  /** 对应后端字段 materialCategoryName */
  materialCategoryName?: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode?: string
  /** 对应后端字段 samplingSchemeName */
  samplingSchemeName?: string
  /** 对应后端字段 isEnabled */
  isEnabled?: number
  /** 对应后端字段 standardStatus */
  standardStatus?: number
  /** 对应后端字段 standardDescription */
  standardDescription?: string
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
 * InspectionStandardCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktInspectionStandardCreateDto）
 */
export interface InspectionStandardCreate {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 standardCode */
  standardCode: string
  /** 对应后端字段 standardName */
  standardName: string
  /** 对应后端字段 inspectionType */
  inspectionType: number
  /** 对应后端字段 materialCategoryCode */
  materialCategoryCode: string
  /** 对应后端字段 materialCategoryName */
  materialCategoryName: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode?: string
  /** 对应后端字段 samplingSchemeName */
  samplingSchemeName?: string
  /** 对应后端字段 isEnabled */
  isEnabled: number
  /** 对应后端字段 standardStatus */
  standardStatus: number
  /** 对应后端字段 standardDescription */
  standardDescription?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * InspectionStandardUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktInspectionStandardUpdateDto）
 */
export interface InspectionStandardUpdate extends InspectionStandardCreate {
  /** 对应后端字段 inspectionStandardId */
  inspectionStandardId: string
}

/**
 * InspectionStandardStandardStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktInspectionStandardStandardStatusDto）
 */
export interface InspectionStandardStandardStatus {
  /** 对应后端字段 inspectionStandardId */
  inspectionStandardId: string
  /** 对应后端字段 standardStatus */
  standardStatus: number
}
