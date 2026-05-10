// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/inspection-sheet-change-log
// 文件名称：inspection-sheet-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：inspection-sheet-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * InspectionSheetChangeLog类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetChangeLogDto）
 */
export interface InspectionSheetChangeLog extends TaktEntityBase {
  /** 对应后端字段 inspectionSheetChangeLogId */
  inspectionSheetChangeLogId: string
  /** 对应后端字段 sheetId */
  sheetId: string
  /** 对应后端字段 changeField */
  changeField: string
  /** 对应后端字段 changeFieldDescription */
  changeFieldDescription?: string
  /** 对应后端字段 oldValue */
  oldValue?: string
  /** 对应后端字段 newValue */
  newValue?: string
  /** 对应后端字段 changeType */
  changeType: number
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 changeUserId */
  changeUserId?: string
  /** 对应后端字段 changeUserName */
  changeUserName?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 sheet */
  sheet?: unknown
}

/**
 * InspectionSheetChangeLogQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetChangeLogQueryDto）
 */
export interface InspectionSheetChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 sheetId */
  sheetId?: string
  /** 对应后端字段 changeField */
  changeField?: string
  /** 对应后端字段 changeFieldDescription */
  changeFieldDescription?: string
  /** 对应后端字段 oldValue */
  oldValue?: string
  /** 对应后端字段 newValue */
  newValue?: string
  /** 对应后端字段 changeType */
  changeType?: number
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 changeUserId */
  changeUserId?: string
  /** 对应后端字段 changeUserName */
  changeUserName?: string
  /** 对应后端字段 changeTime */
  changeTime?: string
  /** 对应后端字段 changeTimeStart */
  changeTimeStart?: string
  /** 对应后端字段 changeTimeEnd */
  changeTimeEnd?: string
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
 * InspectionSheetChangeLogCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetChangeLogCreateDto）
 */
export interface InspectionSheetChangeLogCreate {
  /** 对应后端字段 sheetId */
  sheetId: string
  /** 对应后端字段 changeField */
  changeField: string
  /** 对应后端字段 changeFieldDescription */
  changeFieldDescription?: string
  /** 对应后端字段 oldValue */
  oldValue?: string
  /** 对应后端字段 newValue */
  newValue?: string
  /** 对应后端字段 changeType */
  changeType: number
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 changeUserId */
  changeUserId?: string
  /** 对应后端字段 changeUserName */
  changeUserName?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * InspectionSheetChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.TaktInspectionSheetChangeLogUpdateDto）
 */
export interface InspectionSheetChangeLogUpdate extends InspectionSheetChangeLogCreate {
  /** 对应后端字段 inspectionSheetChangeLogId */
  inspectionSheetChangeLogId: string
}
