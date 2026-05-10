// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/inspection-execution
// 文件名称：inspection-execution.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：inspection-execution相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * InspectionExecution类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionExecutionDto）
 */
export interface InspectionExecution extends TaktEntityBase {
  /** 对应后端字段 inspectionExecutionId */
  inspectionExecutionId: string
  /** 对应后端字段 executionCode */
  executionCode: string
  /** 对应后端字段 planId */
  planId?: string
  /** 对应后端字段 sheetId */
  sheetId: string
  /** 对应后端字段 itemCode */
  itemCode: string
  /** 对应后端字段 itemName */
  itemName: string
  /** 对应后端字段 itemType */
  itemType: number
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 standardValue */
  standardValue?: string
  /** 对应后端字段 tolerance */
  tolerance?: string
  /** 对应后端字段 actualValue */
  actualValue?: string
  /** 对应后端字段 inspectionResult */
  inspectionResult: number
  /** 对应后端字段 defectQuantity */
  defectQuantity: number
  /** 对应后端字段 defectDescription */
  defectDescription?: string
  /** 对应后端字段 inspectorId */
  inspectorId: string
  /** 对应后端字段 inspectorName */
  inspectorName: string
  /** 对应后端字段 executionTime */
  executionTime: string
  /** 对应后端字段 inspectionEquipment */
  inspectionEquipment?: string
  /** 对应后端字段 inspectionEnvironment */
  inspectionEnvironment?: string
  /** 对应后端字段 inspectionImages */
  inspectionImages?: string
  /** 对应后端字段 executionStatus */
  executionStatus: number
  /** 对应后端字段 plan */
  plan?: unknown
  /** 对应后端字段 sheet */
  sheet?: unknown
  /** 对应后端字段 defectHandlings */
  defectHandlings?: unknown[]
}

/**
 * InspectionExecutionQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionExecutionQueryDto）
 */
export interface InspectionExecutionQuery extends TaktPagedQuery {
  /** 对应后端字段 executionCode */
  executionCode?: string
  /** 对应后端字段 planId */
  planId?: string
  /** 对应后端字段 sheetId */
  sheetId?: string
  /** 对应后端字段 itemCode */
  itemCode?: string
  /** 对应后端字段 itemName */
  itemName?: string
  /** 对应后端字段 itemType */
  itemType?: number
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 standardValue */
  standardValue?: string
  /** 对应后端字段 tolerance */
  tolerance?: string
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
  /** 对应后端字段 executionTime */
  executionTime?: string
  /** 对应后端字段 executionTimeStart */
  executionTimeStart?: string
  /** 对应后端字段 executionTimeEnd */
  executionTimeEnd?: string
  /** 对应后端字段 inspectionEquipment */
  inspectionEquipment?: string
  /** 对应后端字段 inspectionEnvironment */
  inspectionEnvironment?: string
  /** 对应后端字段 inspectionImages */
  inspectionImages?: string
  /** 对应后端字段 executionStatus */
  executionStatus?: number
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
 * InspectionExecutionCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionExecutionCreateDto）
 */
export interface InspectionExecutionCreate {
  /** 对应后端字段 executionCode */
  executionCode: string
  /** 对应后端字段 planId */
  planId?: string
  /** 对应后端字段 sheetId */
  sheetId: string
  /** 对应后端字段 itemCode */
  itemCode: string
  /** 对应后端字段 itemName */
  itemName: string
  /** 对应后端字段 itemType */
  itemType: number
  /** 对应后端字段 inspectionMethod */
  inspectionMethod?: string
  /** 对应后端字段 standardValue */
  standardValue?: string
  /** 对应后端字段 tolerance */
  tolerance?: string
  /** 对应后端字段 actualValue */
  actualValue?: string
  /** 对应后端字段 inspectionResult */
  inspectionResult: number
  /** 对应后端字段 defectQuantity */
  defectQuantity: number
  /** 对应后端字段 defectDescription */
  defectDescription?: string
  /** 对应后端字段 inspectorId */
  inspectorId: string
  /** 对应后端字段 inspectorName */
  inspectorName: string
  /** 对应后端字段 executionTime */
  executionTime: string
  /** 对应后端字段 inspectionEquipment */
  inspectionEquipment?: string
  /** 对应后端字段 inspectionEnvironment */
  inspectionEnvironment?: string
  /** 对应后端字段 inspectionImages */
  inspectionImages?: string
  /** 对应后端字段 executionStatus */
  executionStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 defectHandlings */
  defectHandlings?: unknown[]
}

/**
 * InspectionExecutionUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionExecutionUpdateDto）
 */
export interface InspectionExecutionUpdate extends InspectionExecutionCreate {
  /** 对应后端字段 inspectionExecutionId */
  inspectionExecutionId: string
}

/**
 * InspectionExecutionExecutionStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionExecutionExecutionStatusDto）
 */
export interface InspectionExecutionExecutionStatus {
  /** 对应后端字段 inspectionExecutionId */
  inspectionExecutionId: string
  /** 对应后端字段 executionStatus */
  executionStatus: number
}
