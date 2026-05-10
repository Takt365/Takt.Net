// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/inspection-sheet
// 文件名称：inspection-sheet.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：inspection-sheet相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * InspectionSheet类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetDto）
 */
export interface InspectionSheet extends TaktEntityBase {
  /** 对应后端字段 inspectionSheetId */
  inspectionSheetId: string
  /** 对应后端字段 sheetCode */
  sheetCode: string
  /** 对应后端字段 inspectionType */
  inspectionType: number
  /** 对应后端字段 sourceType */
  sourceType: number
  /** 对应后端字段 sourceId */
  sourceId: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 planId */
  planId?: string
  /** 对应后端字段 standardId */
  standardId: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 supplierCode */
  supplierCode?: string
  /** 对应后端字段 supplierName */
  supplierName?: string
  /** 对应后端字段 incomingQuantity */
  incomingQuantity: number
  /** 对应后端字段 processCode */
  processCode?: string
  /** 对应后端字段 processName */
  processName?: string
  /** 对应后端字段 samplingSchemeId */
  samplingSchemeId?: string
  /** 对应后端字段 sampleQuantity */
  sampleQuantity: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity: number
  /** 对应后端字段 unqualifiedQuantity */
  unqualifiedQuantity: number
  /** 对应后端字段 inspectionConclusion */
  inspectionConclusion: number
  /** 对应后端字段 inspectorId */
  inspectorId?: string
  /** 对应后端字段 inspectorName */
  inspectorName?: string
  /** 对应后端字段 inspectionTime */
  inspectionTime?: string
  /** 对应后端字段 judgeUserId */
  judgeUserId?: string
  /** 对应后端字段 judgeUserName */
  judgeUserName?: string
  /** 对应后端字段 judgeTime */
  judgeTime?: string
  /** 对应后端字段 inspectionRemark */
  inspectionRemark?: string
  /** 对应后端字段 sheetStatus */
  sheetStatus: number
  /** 对应后端字段 plan */
  plan?: unknown
  /** 对应后端字段 standard */
  standard?: unknown
  /** 对应后端字段 executions */
  executions?: unknown[]
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 defectHandlings */
  defectHandlings?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * InspectionSheetQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetQueryDto）
 */
export interface InspectionSheetQuery extends TaktPagedQuery {
  /** 对应后端字段 sheetCode */
  sheetCode?: string
  /** 对应后端字段 inspectionType */
  inspectionType?: number
  /** 对应后端字段 sourceType */
  sourceType?: number
  /** 对应后端字段 sourceId */
  sourceId?: string
  /** 对应后端字段 sourceCode */
  sourceCode?: string
  /** 对应后端字段 planId */
  planId?: string
  /** 对应后端字段 standardId */
  standardId?: string
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 supplierCode */
  supplierCode?: string
  /** 对应后端字段 supplierName */
  supplierName?: string
  /** 对应后端字段 incomingQuantity */
  incomingQuantity?: number
  /** 对应后端字段 processCode */
  processCode?: string
  /** 对应后端字段 processName */
  processName?: string
  /** 对应后端字段 samplingSchemeId */
  samplingSchemeId?: string
  /** 对应后端字段 sampleQuantity */
  sampleQuantity?: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity?: number
  /** 对应后端字段 unqualifiedQuantity */
  unqualifiedQuantity?: number
  /** 对应后端字段 inspectionConclusion */
  inspectionConclusion?: number
  /** 对应后端字段 inspectorId */
  inspectorId?: string
  /** 对应后端字段 inspectorName */
  inspectorName?: string
  /** 对应后端字段 inspectionTime */
  inspectionTime?: string
  /** 对应后端字段 inspectionTimeStart */
  inspectionTimeStart?: string
  /** 对应后端字段 inspectionTimeEnd */
  inspectionTimeEnd?: string
  /** 对应后端字段 judgeUserId */
  judgeUserId?: string
  /** 对应后端字段 judgeUserName */
  judgeUserName?: string
  /** 对应后端字段 judgeTime */
  judgeTime?: string
  /** 对应后端字段 judgeTimeStart */
  judgeTimeStart?: string
  /** 对应后端字段 judgeTimeEnd */
  judgeTimeEnd?: string
  /** 对应后端字段 inspectionRemark */
  inspectionRemark?: string
  /** 对应后端字段 sheetStatus */
  sheetStatus?: number
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
 * InspectionSheetCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetCreateDto）
 */
export interface InspectionSheetCreate {
  /** 对应后端字段 sheetCode */
  sheetCode: string
  /** 对应后端字段 inspectionType */
  inspectionType: number
  /** 对应后端字段 sourceType */
  sourceType: number
  /** 对应后端字段 sourceId */
  sourceId: string
  /** 对应后端字段 sourceCode */
  sourceCode: string
  /** 对应后端字段 planId */
  planId?: string
  /** 对应后端字段 standardId */
  standardId: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 supplierCode */
  supplierCode?: string
  /** 对应后端字段 supplierName */
  supplierName?: string
  /** 对应后端字段 incomingQuantity */
  incomingQuantity: number
  /** 对应后端字段 processCode */
  processCode?: string
  /** 对应后端字段 processName */
  processName?: string
  /** 对应后端字段 samplingSchemeId */
  samplingSchemeId?: string
  /** 对应后端字段 sampleQuantity */
  sampleQuantity: number
  /** 对应后端字段 qualifiedQuantity */
  qualifiedQuantity: number
  /** 对应后端字段 unqualifiedQuantity */
  unqualifiedQuantity: number
  /** 对应后端字段 inspectionConclusion */
  inspectionConclusion: number
  /** 对应后端字段 inspectorId */
  inspectorId?: string
  /** 对应后端字段 inspectorName */
  inspectorName?: string
  /** 对应后端字段 inspectionTime */
  inspectionTime?: string
  /** 对应后端字段 judgeUserId */
  judgeUserId?: string
  /** 对应后端字段 judgeUserName */
  judgeUserName?: string
  /** 对应后端字段 judgeTime */
  judgeTime?: string
  /** 对应后端字段 inspectionRemark */
  inspectionRemark?: string
  /** 对应后端字段 sheetStatus */
  sheetStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 executions */
  executions?: unknown[]
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 defectHandlings */
  defectHandlings?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * InspectionSheetUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetUpdateDto）
 */
export interface InspectionSheetUpdate extends InspectionSheetCreate {
  /** 对应后端字段 inspectionSheetId */
  inspectionSheetId: string
}

/**
 * InspectionSheetSheetStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetSheetStatusDto）
 */
export interface InspectionSheetSheetStatus {
  /** 对应后端字段 inspectionSheetId */
  inspectionSheetId: string
  /** 对应后端字段 sheetStatus */
  sheetStatus: number
}
