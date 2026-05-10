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
  materialCategoryCode?: string
  /** 对应后端字段 materialCategoryName */
  materialCategoryName?: string
  /** 对应后端字段 samplingSchemeId */
  samplingSchemeId?: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode?: string
  /** 对应后端字段 inspectionItemsJson */
  inspectionItemsJson?: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 inspectionTools */
  inspectionTools?: string
  /** 对应后端字段 judgmentRules */
  judgmentRules?: string
  /** 对应后端字段 isEnabled */
  isEnabled: number
  /** 对应后端字段 standardStatus */
  standardStatus: number
  /** 对应后端字段 standardDescription */
  standardDescription?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
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
  /** 对应后端字段 samplingSchemeId */
  samplingSchemeId?: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode?: string
  /** 对应后端字段 inspectionItemsJson */
  inspectionItemsJson?: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 inspectionTools */
  inspectionTools?: string
  /** 对应后端字段 judgmentRules */
  judgmentRules?: string
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
  materialCategoryCode?: string
  /** 对应后端字段 materialCategoryName */
  materialCategoryName?: string
  /** 对应后端字段 samplingSchemeId */
  samplingSchemeId?: string
  /** 对应后端字段 samplingSchemeCode */
  samplingSchemeCode?: string
  /** 对应后端字段 inspectionItemsJson */
  inspectionItemsJson?: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 inspectionTools */
  inspectionTools?: string
  /** 对应后端字段 judgmentRules */
  judgmentRules?: string
  /** 对应后端字段 isEnabled */
  isEnabled: number
  /** 对应后端字段 standardStatus */
  standardStatus: number
  /** 对应后端字段 standardDescription */
  standardDescription?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * InspectionStandardUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktInspectionStandardUpdateDto）
 */
export interface InspectionStandardUpdate extends InspectionStandardCreate {
  /** 对应后端字段 inspectionStandardId */
  inspectionStandardId: string
}

/**
 * InspectionStandardSort类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktInspectionStandardSortDto）
 */
export interface InspectionStandardSort {
  /** 对应后端字段 inspectionStandardId */
  inspectionStandardId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
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
