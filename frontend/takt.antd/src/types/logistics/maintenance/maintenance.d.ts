// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/maintenance/maintenance
// 文件名称：maintenance.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：maintenance相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Maintenance类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktMaintenanceDto）
 */
export interface Maintenance extends TaktEntityBase {
  /** 对应后端字段 maintenanceId */
  maintenanceId: string
  /** 对应后端字段 equipmentId */
  equipmentId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 maintenanceType */
  maintenanceType: number
  /** 对应后端字段 maintenanceCompany */
  maintenanceCompany?: string
  /** 对应后端字段 maintenanceTechnician */
  maintenanceTechnician?: string
  /** 对应后端字段 maintenanceDate */
  maintenanceDate: string
  /** 对应后端字段 maintenanceStartTime */
  maintenanceStartTime?: string
  /** 对应后端字段 maintenanceEndTime */
  maintenanceEndTime?: string
  /** 对应后端字段 maintenanceContent */
  maintenanceContent?: string
  /** 对应后端字段 faultDescription */
  faultDescription?: string
  /** 对应后端字段 solution */
  solution?: string
  /** 对应后端字段 usedParts */
  usedParts?: string
  /** 对应后端字段 maintenanceCost */
  maintenanceCost: number
  /** 对应后端字段 maintenanceResult */
  maintenanceResult: number
  /** 对应后端字段 maintenanceStatus */
  maintenanceStatus: number
  /** 对应后端字段 nextMaintenanceDate */
  nextMaintenanceDate?: string
  /** 对应后端字段 maintenanceCycleDays */
  maintenanceCycleDays: number
  /** 对应后端字段 maintenanceDocuments */
  maintenanceDocuments?: string
  /** 对应后端字段 maintenanceImages */
  maintenanceImages?: string
  /** 对应后端字段 acceptedSummary */
  acceptedSummary?: string
  /** 对应后端字段 acceptedBy */
  acceptedBy?: string
  /** 对应后端字段 acceptedAt */
  acceptedAt?: string
  /** 对应后端字段 equipment */
  equipment?: unknown
}

/**
 * MaintenanceQuery类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktMaintenanceQueryDto）
 */
export interface MaintenanceQuery extends TaktPagedQuery {
  /** 对应后端字段 equipmentId */
  equipmentId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 maintenanceType */
  maintenanceType?: number
  /** 对应后端字段 maintenanceCompany */
  maintenanceCompany?: string
  /** 对应后端字段 maintenanceTechnician */
  maintenanceTechnician?: string
  /** 对应后端字段 maintenanceDate */
  maintenanceDate?: string
  /** 对应后端字段 maintenanceDateStart */
  maintenanceDateStart?: string
  /** 对应后端字段 maintenanceDateEnd */
  maintenanceDateEnd?: string
  /** 对应后端字段 maintenanceStartTime */
  maintenanceStartTime?: string
  /** 对应后端字段 maintenanceStartTimeStart */
  maintenanceStartTimeStart?: string
  /** 对应后端字段 maintenanceStartTimeEnd */
  maintenanceStartTimeEnd?: string
  /** 对应后端字段 maintenanceEndTime */
  maintenanceEndTime?: string
  /** 对应后端字段 maintenanceEndTimeStart */
  maintenanceEndTimeStart?: string
  /** 对应后端字段 maintenanceEndTimeEnd */
  maintenanceEndTimeEnd?: string
  /** 对应后端字段 maintenanceContent */
  maintenanceContent?: string
  /** 对应后端字段 faultDescription */
  faultDescription?: string
  /** 对应后端字段 solution */
  solution?: string
  /** 对应后端字段 usedParts */
  usedParts?: string
  /** 对应后端字段 maintenanceCost */
  maintenanceCost?: number
  /** 对应后端字段 maintenanceResult */
  maintenanceResult?: number
  /** 对应后端字段 maintenanceStatus */
  maintenanceStatus?: number
  /** 对应后端字段 nextMaintenanceDate */
  nextMaintenanceDate?: string
  /** 对应后端字段 nextMaintenanceDateStart */
  nextMaintenanceDateStart?: string
  /** 对应后端字段 nextMaintenanceDateEnd */
  nextMaintenanceDateEnd?: string
  /** 对应后端字段 maintenanceCycleDays */
  maintenanceCycleDays?: number
  /** 对应后端字段 maintenanceDocuments */
  maintenanceDocuments?: string
  /** 对应后端字段 maintenanceImages */
  maintenanceImages?: string
  /** 对应后端字段 acceptedSummary */
  acceptedSummary?: string
  /** 对应后端字段 acceptedBy */
  acceptedBy?: string
  /** 对应后端字段 acceptedAt */
  acceptedAt?: string
  /** 对应后端字段 acceptedAtStart */
  acceptedAtStart?: string
  /** 对应后端字段 acceptedAtEnd */
  acceptedAtEnd?: string
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
 * MaintenanceCreate类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktMaintenanceCreateDto）
 */
export interface MaintenanceCreate {
  /** 对应后端字段 equipmentId */
  equipmentId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 maintenanceType */
  maintenanceType: number
  /** 对应后端字段 maintenanceCompany */
  maintenanceCompany?: string
  /** 对应后端字段 maintenanceTechnician */
  maintenanceTechnician?: string
  /** 对应后端字段 maintenanceDate */
  maintenanceDate: string
  /** 对应后端字段 maintenanceStartTime */
  maintenanceStartTime?: string
  /** 对应后端字段 maintenanceEndTime */
  maintenanceEndTime?: string
  /** 对应后端字段 maintenanceContent */
  maintenanceContent?: string
  /** 对应后端字段 faultDescription */
  faultDescription?: string
  /** 对应后端字段 solution */
  solution?: string
  /** 对应后端字段 usedParts */
  usedParts?: string
  /** 对应后端字段 maintenanceCost */
  maintenanceCost: number
  /** 对应后端字段 maintenanceResult */
  maintenanceResult: number
  /** 对应后端字段 maintenanceStatus */
  maintenanceStatus: number
  /** 对应后端字段 nextMaintenanceDate */
  nextMaintenanceDate?: string
  /** 对应后端字段 maintenanceCycleDays */
  maintenanceCycleDays: number
  /** 对应后端字段 maintenanceDocuments */
  maintenanceDocuments?: string
  /** 对应后端字段 maintenanceImages */
  maintenanceImages?: string
  /** 对应后端字段 acceptedSummary */
  acceptedSummary?: string
  /** 对应后端字段 acceptedBy */
  acceptedBy?: string
  /** 对应后端字段 acceptedAt */
  acceptedAt?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * MaintenanceUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktMaintenanceUpdateDto）
 */
export interface MaintenanceUpdate extends MaintenanceCreate {
  /** 对应后端字段 maintenanceId */
  maintenanceId: string
}

/**
 * MaintenanceStatus类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktMaintenanceStatusDto）
 */
export interface MaintenanceStatus {
  /** 对应后端字段 maintenanceId */
  maintenanceId: string
  /** 对应后端字段 maintenanceStatus */
  maintenanceStatus: number
}
