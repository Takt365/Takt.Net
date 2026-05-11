// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/complaint/supplier-evaluation-item
// 文件名称：supplier-evaluation-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：supplier-evaluation-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SupplierEvaluationItem类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationItemDto）
 */
export interface SupplierEvaluationItem extends TaktEntityBase {
  /** 对应后端字段 supplierEvaluationItemId */
  supplierEvaluationItemId: string
  /** 对应后端字段 evaluationId */
  evaluationId: string
  /** 对应后端字段 supplierEvaluationCode */
  supplierEvaluationCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 categoryType */
  categoryType: number
  /** 对应后端字段 itemName */
  itemName: string
  /** 对应后端字段 itemDescription */
  itemDescription?: string
  /** 对应后端字段 weight */
  weight: number
  /** 对应后端字段 scoringStandard */
  scoringStandard?: string
  /** 对应后端字段 score */
  score?: number
  /** 对应后端字段 ratingLevel */
  ratingLevel?: number
  /** 对应后端字段 evaluationComment */
  evaluationComment?: string
  /** 对应后端字段 existingIssues */
  existingIssues?: string
  /** 对应后端字段 improvementRequirement */
  improvementRequirement?: string
  /** 对应后端字段 rectificationRequired */
  rectificationRequired: number
  /** 对应后端字段 rectificationDeadline */
  rectificationDeadline?: string
  /** 对应后端字段 rectificationStatus */
  rectificationStatus: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 evaluation */
  evaluation?: unknown
}

/**
 * SupplierEvaluationItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationItemQueryDto）
 */
export interface SupplierEvaluationItemQuery extends TaktPagedQuery {
  /** 对应后端字段 evaluationId */
  evaluationId?: string
  /** 对应后端字段 supplierEvaluationCode */
  supplierEvaluationCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 categoryType */
  categoryType?: number
  /** 对应后端字段 itemName */
  itemName?: string
  /** 对应后端字段 itemDescription */
  itemDescription?: string
  /** 对应后端字段 weight */
  weight?: number
  /** 对应后端字段 scoringStandard */
  scoringStandard?: string
  /** 对应后端字段 score */
  score?: number
  /** 对应后端字段 ratingLevel */
  ratingLevel?: number
  /** 对应后端字段 evaluationComment */
  evaluationComment?: string
  /** 对应后端字段 existingIssues */
  existingIssues?: string
  /** 对应后端字段 improvementRequirement */
  improvementRequirement?: string
  /** 对应后端字段 rectificationRequired */
  rectificationRequired?: number
  /** 对应后端字段 rectificationDeadline */
  rectificationDeadline?: string
  /** 对应后端字段 rectificationDeadlineStart */
  rectificationDeadlineStart?: string
  /** 对应后端字段 rectificationDeadlineEnd */
  rectificationDeadlineEnd?: string
  /** 对应后端字段 rectificationStatus */
  rectificationStatus?: number
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
 * SupplierEvaluationItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationItemCreateDto）
 */
export interface SupplierEvaluationItemCreate {
  /** 对应后端字段 evaluationId */
  evaluationId: string
  /** 对应后端字段 supplierEvaluationCode */
  supplierEvaluationCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 categoryType */
  categoryType: number
  /** 对应后端字段 itemName */
  itemName: string
  /** 对应后端字段 itemDescription */
  itemDescription?: string
  /** 对应后端字段 weight */
  weight: number
  /** 对应后端字段 scoringStandard */
  scoringStandard?: string
  /** 对应后端字段 score */
  score?: number
  /** 对应后端字段 ratingLevel */
  ratingLevel?: number
  /** 对应后端字段 evaluationComment */
  evaluationComment?: string
  /** 对应后端字段 existingIssues */
  existingIssues?: string
  /** 对应后端字段 improvementRequirement */
  improvementRequirement?: string
  /** 对应后端字段 rectificationRequired */
  rectificationRequired: number
  /** 对应后端字段 rectificationDeadline */
  rectificationDeadline?: string
  /** 对应后端字段 rectificationStatus */
  rectificationStatus: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * SupplierEvaluationItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationItemUpdateDto）
 */
export interface SupplierEvaluationItemUpdate extends SupplierEvaluationItemCreate {
  /** 对应后端字段 supplierEvaluationItemId */
  supplierEvaluationItemId: string
}

/**
 * SupplierEvaluationItemSort类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationItemSortDto）
 */
export interface SupplierEvaluationItemSort {
  /** 对应后端字段 supplierEvaluationItemId */
  supplierEvaluationItemId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * SupplierEvaluationItemRectificationStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktSupplierEvaluationItemRectificationStatusDto）
 */
export interface SupplierEvaluationItemRectificationStatus {
  /** 对应后端字段 supplierEvaluationItemId */
  supplierEvaluationItemId: string
  /** 对应后端字段 rectificationStatus */
  rectificationStatus: number
}
