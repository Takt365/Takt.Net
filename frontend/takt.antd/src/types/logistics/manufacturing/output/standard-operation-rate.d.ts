// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/output/standard-operation-rate
// 文件名称：standard-operation-rate.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：standard-operation-rate相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * StandardOperationRate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktStandardOperationRateDto）
 */
export interface StandardOperationRate extends TaktEntityBase {
  /** 对应后端字段 standardOperationRateId */
  standardOperationRateId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 financialYear */
  financialYear: string
  /** 对应后端字段 operationType */
  operationType: number
  /** 对应后端字段 operationRate */
  operationRate: number
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
  /** 对应后端字段 status */
  status: number
}

/**
 * StandardOperationRateQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktStandardOperationRateQueryDto）
 */
export interface StandardOperationRateQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 financialYear */
  financialYear?: string
  /** 对应后端字段 operationType */
  operationType?: number
  /** 对应后端字段 operationRate */
  operationRate?: number
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
  /** 对应后端字段 status */
  status?: number
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
 * StandardOperationRateCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktStandardOperationRateCreateDto）
 */
export interface StandardOperationRateCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 financialYear */
  financialYear: string
  /** 对应后端字段 operationType */
  operationType: number
  /** 对应后端字段 operationRate */
  operationRate: number
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * StandardOperationRateUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktStandardOperationRateUpdateDto）
 */
export interface StandardOperationRateUpdate extends StandardOperationRateCreate {
  /** 对应后端字段 standardOperationRateId */
  standardOperationRateId: string
}

/**
 * StandardOperationRateStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktStandardOperationRateStatusDto）
 */
export interface StandardOperationRateStatus {
  /** 对应后端字段 standardOperationRateId */
  standardOperationRateId: string
  /** 对应后端字段 status */
  status: number
}
