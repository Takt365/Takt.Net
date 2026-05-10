// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/inspection-sheet-item
// 文件名称：inspection-sheet-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：inspection-sheet-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * InspectionSheetItem类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetItemDto）
 */
export interface InspectionSheetItem extends TaktEntityBase {
  /** 对应后端字段 inspectionSheetItemId */
  inspectionSheetItemId: string
  /** 对应后端字段 sheetId */
  sheetId: string
  /** 对应后端字段 itemCode */
  itemCode: string
  /** 对应后端字段 itemName */
  itemName: string
  /** 对应后端字段 itemType */
  itemType: number
  /** 对应后端字段 standardValue */
  standardValue?: string
  /** 对应后端字段 upperLimit */
  upperLimit?: string
  /** 对应后端字段 lowerLimit */
  lowerLimit?: string
  /** 对应后端字段 inspectionTool */
  inspectionTool?: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 actualValue */
  actualValue?: string
  /** 对应后端字段 inspectionResult */
  inspectionResult: number
  /** 对应后端字段 defectQuantity */
  defectQuantity: number
  /** 对应后端字段 defectDescription */
  defectDescription?: string
  /** 对应后端字段 inspectorId */
  inspectorId?: string
  /** 对应后端字段 inspectorName */
  inspectorName?: string
  /** 对应后端字段 inspectionTime */
  inspectionTime?: string
  /** 对应后端字段 inspectionImages */
  inspectionImages?: string
  /** 对应后端字段 sheet */
  sheet?: unknown
  /** 对应后端字段 defectHandlings */
  defectHandlings?: unknown[]
}

/**
 * InspectionSheetItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetItemQueryDto）
 */
export interface InspectionSheetItemQuery extends TaktPagedQuery {
  /** 对应后端字段 sheetId */
  sheetId?: string
  /** 对应后端字段 itemCode */
  itemCode?: string
  /** 对应后端字段 itemName */
  itemName?: string
  /** 对应后端字段 itemType */
  itemType?: number
  /** 对应后端字段 standardValue */
  standardValue?: string
  /** 对应后端字段 upperLimit */
  upperLimit?: string
  /** 对应后端字段 lowerLimit */
  lowerLimit?: string
  /** 对应后端字段 inspectionTool */
  inspectionTool?: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 actualValue */
  actualValue?: string
  /** 对应后端字段 inspectionResult */
  inspectionResult?: number
  /** 对应后端字段 defectQuantity */
  defectQuantity?: number
  /** 对应后端字段 defectDescription */
  defectDescription?: string
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
  /** 对应后端字段 inspectionImages */
  inspectionImages?: string
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
 * InspectionSheetItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetItemCreateDto）
 */
export interface InspectionSheetItemCreate {
  /** 对应后端字段 sheetId */
  sheetId: string
  /** 对应后端字段 itemCode */
  itemCode: string
  /** 对应后端字段 itemName */
  itemName: string
  /** 对应后端字段 itemType */
  itemType: number
  /** 对应后端字段 standardValue */
  standardValue?: string
  /** 对应后端字段 upperLimit */
  upperLimit?: string
  /** 对应后端字段 lowerLimit */
  lowerLimit?: string
  /** 对应后端字段 inspectionTool */
  inspectionTool?: string
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 actualValue */
  actualValue?: string
  /** 对应后端字段 inspectionResult */
  inspectionResult: number
  /** 对应后端字段 defectQuantity */
  defectQuantity: number
  /** 对应后端字段 defectDescription */
  defectDescription?: string
  /** 对应后端字段 inspectorId */
  inspectorId?: string
  /** 对应后端字段 inspectorName */
  inspectorName?: string
  /** 对应后端字段 inspectionTime */
  inspectionTime?: string
  /** 对应后端字段 inspectionImages */
  inspectionImages?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 defectHandlings */
  defectHandlings?: unknown[]
}

/**
 * InspectionSheetItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetItemUpdateDto）
 */
export interface InspectionSheetItemUpdate extends InspectionSheetItemCreate {
  /** 对应后端字段 inspectionSheetItemId */
  inspectionSheetItemId: string
}
