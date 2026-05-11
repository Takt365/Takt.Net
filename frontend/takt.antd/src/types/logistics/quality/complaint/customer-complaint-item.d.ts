// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/complaint/customer-complaint-item
// 文件名称：customer-complaint-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：customer-complaint-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CustomerComplaintItem类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintItemDto）
 */
export interface CustomerComplaintItem extends TaktEntityBase {
  /** 对应后端字段 customerComplaintItemId */
  customerComplaintItemId: string
  /** 对应后端字段 complaintId */
  complaintId: string
  /** 对应后端字段 customerComplaintCode */
  customerComplaintCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 productCode */
  productCode?: string
  /** 对应后端字段 productName */
  productName?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 itemType */
  itemType: number
  /** 对应后端字段 defectDescription */
  defectDescription: string
  /** 对应后端字段 defectLevel */
  defectLevel: string
  /** 对应后端字段 defectQuantity */
  defectQuantity: number
  /** 对应后端字段 defectRate */
  defectRate?: number
  /** 对应后端字段 causeAnalysis */
  causeAnalysis?: string
  /** 对应后端字段 improvementAction */
  improvementAction?: string
  /** 对应后端字段 improvementResponsible */
  improvementResponsible?: string
  /** 对应后端字段 plannedCompletionDate */
  plannedCompletionDate?: string
  /** 对应后端字段 actualCompletionDate */
  actualCompletionDate?: string
  /** 对应后端字段 improvementStatus */
  improvementStatus: number
  /** 对应后端字段 attachmentPaths */
  attachmentPaths?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 complaint */
  complaint?: unknown
}

/**
 * CustomerComplaintItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintItemQueryDto）
 */
export interface CustomerComplaintItemQuery extends TaktPagedQuery {
  /** 对应后端字段 complaintId */
  complaintId?: string
  /** 对应后端字段 customerComplaintCode */
  customerComplaintCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 productCode */
  productCode?: string
  /** 对应后端字段 productName */
  productName?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 itemType */
  itemType?: number
  /** 对应后端字段 defectDescription */
  defectDescription?: string
  /** 对应后端字段 defectLevel */
  defectLevel?: string
  /** 对应后端字段 defectQuantity */
  defectQuantity?: number
  /** 对应后端字段 defectRate */
  defectRate?: number
  /** 对应后端字段 causeAnalysis */
  causeAnalysis?: string
  /** 对应后端字段 improvementAction */
  improvementAction?: string
  /** 对应后端字段 improvementResponsible */
  improvementResponsible?: string
  /** 对应后端字段 plannedCompletionDate */
  plannedCompletionDate?: string
  /** 对应后端字段 plannedCompletionDateStart */
  plannedCompletionDateStart?: string
  /** 对应后端字段 plannedCompletionDateEnd */
  plannedCompletionDateEnd?: string
  /** 对应后端字段 actualCompletionDate */
  actualCompletionDate?: string
  /** 对应后端字段 actualCompletionDateStart */
  actualCompletionDateStart?: string
  /** 对应后端字段 actualCompletionDateEnd */
  actualCompletionDateEnd?: string
  /** 对应后端字段 improvementStatus */
  improvementStatus?: number
  /** 对应后端字段 attachmentPaths */
  attachmentPaths?: string
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
 * CustomerComplaintItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintItemCreateDto）
 */
export interface CustomerComplaintItemCreate {
  /** 对应后端字段 complaintId */
  complaintId: string
  /** 对应后端字段 customerComplaintCode */
  customerComplaintCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 productCode */
  productCode?: string
  /** 对应后端字段 productName */
  productName?: string
  /** 对应后端字段 batchNo */
  batchNo?: string
  /** 对应后端字段 itemType */
  itemType: number
  /** 对应后端字段 defectDescription */
  defectDescription: string
  /** 对应后端字段 defectLevel */
  defectLevel: string
  /** 对应后端字段 defectQuantity */
  defectQuantity: number
  /** 对应后端字段 defectRate */
  defectRate?: number
  /** 对应后端字段 causeAnalysis */
  causeAnalysis?: string
  /** 对应后端字段 improvementAction */
  improvementAction?: string
  /** 对应后端字段 improvementResponsible */
  improvementResponsible?: string
  /** 对应后端字段 plannedCompletionDate */
  plannedCompletionDate?: string
  /** 对应后端字段 actualCompletionDate */
  actualCompletionDate?: string
  /** 对应后端字段 improvementStatus */
  improvementStatus: number
  /** 对应后端字段 attachmentPaths */
  attachmentPaths?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * CustomerComplaintItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintItemUpdateDto）
 */
export interface CustomerComplaintItemUpdate extends CustomerComplaintItemCreate {
  /** 对应后端字段 customerComplaintItemId */
  customerComplaintItemId: string
}

/**
 * CustomerComplaintItemSort类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintItemSortDto）
 */
export interface CustomerComplaintItemSort {
  /** 对应后端字段 customerComplaintItemId */
  customerComplaintItemId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * CustomerComplaintItemImprovementStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintItemImprovementStatusDto）
 */
export interface CustomerComplaintItemImprovementStatus {
  /** 对应后端字段 customerComplaintItemId */
  customerComplaintItemId: string
  /** 对应后端字段 improvementStatus */
  improvementStatus: number
}
