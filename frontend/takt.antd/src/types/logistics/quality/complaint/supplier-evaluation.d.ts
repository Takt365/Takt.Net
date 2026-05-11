// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/complaint/supplier-evaluation
// 文件名称：supplier-evaluation.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：supplier-evaluation相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SupplierEvaluation类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationDto）
 */
export interface SupplierEvaluation extends TaktEntityBase {
  /** 对应后端字段 supplierEvaluationId */
  supplierEvaluationId: string
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 supplierEvaluationCode */
  supplierEvaluationCode: string
  /** 对应后端字段 supplierId */
  supplierId: string
  /** 对应后端字段 supplierName */
  supplierName: string
  /** 对应后端字段 supplierCode */
  supplierCode?: string
  /** 对应后端字段 evaluationDate */
  evaluationDate: string
  /** 对应后端字段 evaluationPeriod */
  evaluationPeriod: number
  /** 对应后端字段 evaluationType */
  evaluationType: number
  /** 对应后端字段 evaluatorBy */
  evaluatorBy?: string
  /** 对应后端字段 evaluationDept */
  evaluationDept?: string
  /** 对应后端字段 overallRating */
  overallRating: number
  /** 对应后端字段 totalScore */
  totalScore?: number
  /** 对应后端字段 qualityScore */
  qualityScore?: number
  /** 对应后端字段 deliveryScore */
  deliveryScore?: number
  /** 对应后端字段 priceScore */
  priceScore?: number
  /** 对应后端字段 serviceScore */
  serviceScore?: number
  /** 对应后端字段 technicalScore */
  technicalScore?: number
  /** 对应后端字段 mainStrengths */
  mainStrengths?: string
  /** 对应后端字段 mainIssues */
  mainIssues?: string
  /** 对应后端字段 improvementRequirements */
  improvementRequirements?: string
  /** 对应后端字段 evaluationConclusion */
  evaluationConclusion: number
  /** 对应后端字段 rectificationDeadline */
  rectificationDeadline?: string
  /** 对应后端字段 evaluationStatus */
  evaluationStatus: number
  /** 对应后端字段 rectificationStatus */
  rectificationStatus: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * SupplierEvaluationQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationQueryDto）
 */
export interface SupplierEvaluationQuery extends TaktPagedQuery {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 supplierEvaluationCode */
  supplierEvaluationCode?: string
  /** 对应后端字段 supplierId */
  supplierId?: string
  /** 对应后端字段 supplierName */
  supplierName?: string
  /** 对应后端字段 supplierCode */
  supplierCode?: string
  /** 对应后端字段 evaluationDate */
  evaluationDate?: string
  /** 对应后端字段 evaluationDateStart */
  evaluationDateStart?: string
  /** 对应后端字段 evaluationDateEnd */
  evaluationDateEnd?: string
  /** 对应后端字段 evaluationPeriod */
  evaluationPeriod?: number
  /** 对应后端字段 evaluationType */
  evaluationType?: number
  /** 对应后端字段 evaluatorBy */
  evaluatorBy?: string
  /** 对应后端字段 evaluationDept */
  evaluationDept?: string
  /** 对应后端字段 overallRating */
  overallRating?: number
  /** 对应后端字段 totalScore */
  totalScore?: number
  /** 对应后端字段 qualityScore */
  qualityScore?: number
  /** 对应后端字段 deliveryScore */
  deliveryScore?: number
  /** 对应后端字段 priceScore */
  priceScore?: number
  /** 对应后端字段 serviceScore */
  serviceScore?: number
  /** 对应后端字段 technicalScore */
  technicalScore?: number
  /** 对应后端字段 mainStrengths */
  mainStrengths?: string
  /** 对应后端字段 mainIssues */
  mainIssues?: string
  /** 对应后端字段 improvementRequirements */
  improvementRequirements?: string
  /** 对应后端字段 evaluationConclusion */
  evaluationConclusion?: number
  /** 对应后端字段 rectificationDeadline */
  rectificationDeadline?: string
  /** 对应后端字段 rectificationDeadlineStart */
  rectificationDeadlineStart?: string
  /** 对应后端字段 rectificationDeadlineEnd */
  rectificationDeadlineEnd?: string
  /** 对应后端字段 evaluationStatus */
  evaluationStatus?: number
  /** 对应后端字段 rectificationStatus */
  rectificationStatus?: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
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
 * SupplierEvaluationCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationCreateDto）
 */
export interface SupplierEvaluationCreate {
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 supplierEvaluationCode */
  supplierEvaluationCode: string
  /** 对应后端字段 supplierId */
  supplierId: string
  /** 对应后端字段 supplierName */
  supplierName: string
  /** 对应后端字段 supplierCode */
  supplierCode?: string
  /** 对应后端字段 evaluationDate */
  evaluationDate: string
  /** 对应后端字段 evaluationPeriod */
  evaluationPeriod: number
  /** 对应后端字段 evaluationType */
  evaluationType: number
  /** 对应后端字段 evaluatorBy */
  evaluatorBy?: string
  /** 对应后端字段 evaluationDept */
  evaluationDept?: string
  /** 对应后端字段 overallRating */
  overallRating: number
  /** 对应后端字段 totalScore */
  totalScore?: number
  /** 对应后端字段 qualityScore */
  qualityScore?: number
  /** 对应后端字段 deliveryScore */
  deliveryScore?: number
  /** 对应后端字段 priceScore */
  priceScore?: number
  /** 对应后端字段 serviceScore */
  serviceScore?: number
  /** 对应后端字段 technicalScore */
  technicalScore?: number
  /** 对应后端字段 mainStrengths */
  mainStrengths?: string
  /** 对应后端字段 mainIssues */
  mainIssues?: string
  /** 对应后端字段 improvementRequirements */
  improvementRequirements?: string
  /** 对应后端字段 evaluationConclusion */
  evaluationConclusion: number
  /** 对应后端字段 rectificationDeadline */
  rectificationDeadline?: string
  /** 对应后端字段 evaluationStatus */
  evaluationStatus: number
  /** 对应后端字段 rectificationStatus */
  rectificationStatus: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * SupplierEvaluationUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationUpdateDto）
 */
export interface SupplierEvaluationUpdate extends SupplierEvaluationCreate {
  /** 对应后端字段 supplierEvaluationId */
  supplierEvaluationId: string
}

/**
 * SupplierEvaluationSort类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationSortDto）
 */
export interface SupplierEvaluationSort {
  /** 对应后端字段 supplierEvaluationId */
  supplierEvaluationId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * SupplierEvaluationEvaluationStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationEvaluationStatusDto）
 */
export interface SupplierEvaluationEvaluationStatus {
  /** 对应后端字段 supplierEvaluationId */
  supplierEvaluationId: string
  /** 对应后端字段 evaluationStatus */
  evaluationStatus: number
}

/**
 * SupplierEvaluationRectificationStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationRectificationStatusDto）
 */
export interface SupplierEvaluationRectificationStatus {
  /** 对应后端字段 supplierEvaluationId */
  supplierEvaluationId: string
  /** 对应后端字段 rectificationStatus */
  rectificationStatus: number
}
