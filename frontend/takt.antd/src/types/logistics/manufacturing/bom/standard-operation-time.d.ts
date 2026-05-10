// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/bom/standard-operation-time
// 文件名称：standard-operation-time.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：standard-operation-time相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * StandardOperationTime类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktStandardOperationTimeDto）
 */
export interface StandardOperationTime extends TaktEntityBase {
  /** 对应后端字段 standardOperationTimeId */
  standardOperationTimeId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 workCenter */
  workCenter: string
  /** 对应后端字段 operationDesc */
  operationDesc?: string
  /** 对应后端字段 standardMinutes */
  standardMinutes: number
  /** 对应后端字段 timeUnit */
  timeUnit: string
  /** 对应后端字段 standardShorts */
  standardShorts: number
  /** 对应后端字段 pointsUnit */
  pointsUnit: string
  /** 对应后端字段 pointsToMinutesRate */
  pointsToMinutesRate: number
  /** 对应后端字段 convertedMinutes */
  convertedMinutes: number
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
  /** 对应后端字段 approvalStatus */
  approvalStatus: number
  /** 对应后端字段 approver */
  approver?: string
  /** 对应后端字段 approvalDate */
  approvalDate?: string
}

/**
 * StandardOperationTimeQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktStandardOperationTimeQueryDto）
 */
export interface StandardOperationTimeQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 workCenter */
  workCenter?: string
  /** 对应后端字段 operationDesc */
  operationDesc?: string
  /** 对应后端字段 standardMinutes */
  standardMinutes?: number
  /** 对应后端字段 timeUnit */
  timeUnit?: string
  /** 对应后端字段 standardShorts */
  standardShorts?: number
  /** 对应后端字段 pointsUnit */
  pointsUnit?: string
  /** 对应后端字段 pointsToMinutesRate */
  pointsToMinutesRate?: number
  /** 对应后端字段 convertedMinutes */
  convertedMinutes?: number
  /** 对应后端字段 effectiveDate */
  effectiveDate?: string
  /** 对应后端字段 effectiveDateStart */
  effectiveDateStart?: string
  /** 对应后端字段 effectiveDateEnd */
  effectiveDateEnd?: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
  /** 对应后端字段 expiryDateStart */
  expiryDateStart?: string
  /** 对应后端字段 expiryDateEnd */
  expiryDateEnd?: string
  /** 对应后端字段 approvalStatus */
  approvalStatus?: number
  /** 对应后端字段 approver */
  approver?: string
  /** 对应后端字段 approvalDate */
  approvalDate?: string
  /** 对应后端字段 approvalDateStart */
  approvalDateStart?: string
  /** 对应后端字段 approvalDateEnd */
  approvalDateEnd?: string
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
 * StandardOperationTimeCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktStandardOperationTimeCreateDto）
 */
export interface StandardOperationTimeCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 workCenter */
  workCenter: string
  /** 对应后端字段 operationDesc */
  operationDesc?: string
  /** 对应后端字段 standardMinutes */
  standardMinutes: number
  /** 对应后端字段 timeUnit */
  timeUnit: string
  /** 对应后端字段 standardShorts */
  standardShorts: number
  /** 对应后端字段 pointsUnit */
  pointsUnit: string
  /** 对应后端字段 pointsToMinutesRate */
  pointsToMinutesRate: number
  /** 对应后端字段 convertedMinutes */
  convertedMinutes: number
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
  /** 对应后端字段 approvalStatus */
  approvalStatus: number
  /** 对应后端字段 approver */
  approver?: string
  /** 对应后端字段 approvalDate */
  approvalDate?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * StandardOperationTimeUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktStandardOperationTimeUpdateDto）
 */
export interface StandardOperationTimeUpdate extends StandardOperationTimeCreate {
  /** 对应后端字段 standardOperationTimeId */
  standardOperationTimeId: string
}

/**
 * StandardOperationTimeApprovalStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktStandardOperationTimeApprovalStatusDto）
 */
export interface StandardOperationTimeApprovalStatus {
  /** 对应后端字段 standardOperationTimeId */
  standardOperationTimeId: string
  /** 对应后端字段 approvalStatus */
  approvalStatus: number
}
