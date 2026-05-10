// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-operation-customer-response
// 文件名称：quality-operation-customer-response.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-operation-customer-response相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityOperationCustomerResponse类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationCustomerResponseDto）
 */
export interface QualityOperationCustomerResponse extends TaktEntityBase {
  /** 对应后端字段 qualityOperationCustomerResponseId */
  qualityOperationCustomerResponseId: string
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 responseCost */
  responseCost: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 customerResponseNote */
  customerResponseNote?: string
  /** 对应后端字段 operation */
  operation?: unknown
}

/**
 * QualityOperationCustomerResponseQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationCustomerResponseQueryDto）
 */
export interface QualityOperationCustomerResponseQuery extends TaktPagedQuery {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 responseCost */
  responseCost?: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes?: number
  /** 对应后端字段 otherExpenses */
  otherExpenses?: number
  /** 对应后端字段 customerResponseNote */
  customerResponseNote?: string
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
 * QualityOperationCustomerResponseCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationCustomerResponseCreateDto）
 */
export interface QualityOperationCustomerResponseCreate {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 responseCost */
  responseCost: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 customerResponseNote */
  customerResponseNote?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * QualityOperationCustomerResponseUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationCustomerResponseUpdateDto）
 */
export interface QualityOperationCustomerResponseUpdate extends QualityOperationCustomerResponseCreate {
  /** 对应后端字段 qualityOperationCustomerResponseId */
  qualityOperationCustomerResponseId: string
}
