// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/operation/inspection-standard-item
// 文件名称：inspection-standard-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：inspection-standard-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * InspectionStandardItem类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktInspectionStandardItemDto）
 */
export interface InspectionStandardItem extends TaktEntityBase {
  /** 对应后端字段 inspectionStandardItemId */
  inspectionStandardItemId: string
  /** 对应后端字段 inspectionStandardId */
  inspectionStandardId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 itemCode */
  itemCode: string
  /** 对应后端字段 itemName */
  itemName: string
  /** 对应后端字段 itemType */
  itemType: number
  /** 对应后端字段 defectLevel */
  defectLevel: string
  /** 对应后端字段 inspectionMode */
  inspectionMode: number
  /** 对应后端字段 standardValue */
  standardValue: string
  /** 对应后端字段 upperLimit */
  upperLimit: string
  /** 对应后端字段 lowerLimit */
  lowerLimit: string
  /** 对应后端字段 inspectionTool */
  inspectionTool: string
  /** 对应后端字段 inspectionMethodDescription */
  inspectionMethodDescription: string
  /** 对应后端字段 acceptanceCriteria */
  acceptanceCriteria: string
  /** 对应后端字段 rejectionCriteria */
  rejectionCriteria: string
  /** 对应后端字段 isQualifiedBasis */
  isQualifiedBasis: number
  /** 对应后端字段 standard */
  standard?: unknown
}

/**
 * InspectionStandardItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktInspectionStandardItemQueryDto）
 */
export interface InspectionStandardItemQuery extends TaktPagedQuery {
  /** 对应后端字段 inspectionStandardId */
  inspectionStandardId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 itemCode */
  itemCode?: string
  /** 对应后端字段 itemName */
  itemName?: string
  /** 对应后端字段 itemType */
  itemType?: number
  /** 对应后端字段 defectLevel */
  defectLevel?: string
  /** 对应后端字段 inspectionMode */
  inspectionMode?: number
  /** 对应后端字段 standardValue */
  standardValue?: string
  /** 对应后端字段 upperLimit */
  upperLimit?: string
  /** 对应后端字段 lowerLimit */
  lowerLimit?: string
  /** 对应后端字段 inspectionTool */
  inspectionTool?: string
  /** 对应后端字段 inspectionMethodDescription */
  inspectionMethodDescription?: string
  /** 对应后端字段 acceptanceCriteria */
  acceptanceCriteria?: string
  /** 对应后端字段 rejectionCriteria */
  rejectionCriteria?: string
  /** 对应后端字段 isQualifiedBasis */
  isQualifiedBasis?: number
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
 * InspectionStandardItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktInspectionStandardItemCreateDto）
 */
export interface InspectionStandardItemCreate {
  /** 对应后端字段 inspectionStandardId */
  inspectionStandardId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 itemCode */
  itemCode: string
  /** 对应后端字段 itemName */
  itemName: string
  /** 对应后端字段 itemType */
  itemType: number
  /** 对应后端字段 defectLevel */
  defectLevel: string
  /** 对应后端字段 inspectionMode */
  inspectionMode: number
  /** 对应后端字段 standardValue */
  standardValue: string
  /** 对应后端字段 upperLimit */
  upperLimit: string
  /** 对应后端字段 lowerLimit */
  lowerLimit: string
  /** 对应后端字段 inspectionTool */
  inspectionTool: string
  /** 对应后端字段 inspectionMethodDescription */
  inspectionMethodDescription: string
  /** 对应后端字段 acceptanceCriteria */
  acceptanceCriteria: string
  /** 对应后端字段 rejectionCriteria */
  rejectionCriteria: string
  /** 对应后端字段 isQualifiedBasis */
  isQualifiedBasis: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * InspectionStandardItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Operation.TaktInspectionStandardItemUpdateDto）
 */
export interface InspectionStandardItemUpdate extends InspectionStandardItemCreate {
  /** 对应后端字段 inspectionStandardItemId */
  inspectionStandardItemId: string
}
