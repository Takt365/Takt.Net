// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/complaint/customer-complaint-handling
// 文件名称：customer-complaint-handling.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：customer-complaint-handling相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CustomerComplaintHandling类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintHandlingDto）
 */
export interface CustomerComplaintHandling extends TaktEntityBase {
  /** 对应后端字段 customerComplaintHandlingId */
  customerComplaintHandlingId: string
  /** 对应后端字段 complaintHandlingCode */
  complaintHandlingCode: string
  /** 对应后端字段 complaintId */
  complaintId: string
  /** 对应后端字段 complaintNo */
  complaintNo: string
  /** 对应后端字段 complaintItemId */
  complaintItemId?: string
  /** 对应后端字段 handlingStage */
  handlingStage: number
  /** 对应后端字段 handlingMethod */
  handlingMethod: number
  /** 对应后端字段 handlingDescription */
  handlingDescription: string
  /** 对应后端字段 causeAnalysis */
  causeAnalysis?: string
  /** 对应后端字段 correctiveAction */
  correctiveAction?: string
  /** 对应后端字段 preventiveAction */
  preventiveAction?: string
  /** 对应后端字段 responsibleDept */
  responsibleDept?: string
  /** 对应后端字段 responsibleBy */
  responsibleBy?: string
  /** 对应后端字段 handlerBy */
  handlerBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 plannedCompletionDate */
  plannedCompletionDate?: string
  /** 对应后端字段 actualCompletionDate */
  actualCompletionDate?: string
  /** 对应后端字段 handlingStatus */
  handlingStatus: number
  /** 对应后端字段 handlingCost */
  handlingCost?: number
  /** 对应后端字段 customerFeedback */
  customerFeedback?: string
  /** 对应后端字段 customerSatisfaction */
  customerSatisfaction?: number
  /** 对应后端字段 attachmentPaths */
  attachmentPaths?: string
  /** 对应后端字段 complaint */
  complaint?: unknown
}

/**
 * CustomerComplaintHandlingQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintHandlingQueryDto）
 */
export interface CustomerComplaintHandlingQuery extends TaktPagedQuery {
  /** 对应后端字段 complaintHandlingCode */
  complaintHandlingCode?: string
  /** 对应后端字段 complaintId */
  complaintId?: string
  /** 对应后端字段 complaintNo */
  complaintNo?: string
  /** 对应后端字段 complaintItemId */
  complaintItemId?: string
  /** 对应后端字段 handlingStage */
  handlingStage?: number
  /** 对应后端字段 handlingMethod */
  handlingMethod?: number
  /** 对应后端字段 handlingDescription */
  handlingDescription?: string
  /** 对应后端字段 causeAnalysis */
  causeAnalysis?: string
  /** 对应后端字段 correctiveAction */
  correctiveAction?: string
  /** 对应后端字段 preventiveAction */
  preventiveAction?: string
  /** 对应后端字段 responsibleDept */
  responsibleDept?: string
  /** 对应后端字段 responsibleBy */
  responsibleBy?: string
  /** 对应后端字段 handlerBy */
  handlerBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 handlingTimeStart */
  handlingTimeStart?: string
  /** 对应后端字段 handlingTimeEnd */
  handlingTimeEnd?: string
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
  /** 对应后端字段 handlingStatus */
  handlingStatus?: number
  /** 对应后端字段 handlingCost */
  handlingCost?: number
  /** 对应后端字段 customerFeedback */
  customerFeedback?: string
  /** 对应后端字段 customerSatisfaction */
  customerSatisfaction?: number
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
 * CustomerComplaintHandlingCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintHandlingCreateDto）
 */
export interface CustomerComplaintHandlingCreate {
  /** 对应后端字段 complaintHandlingCode */
  complaintHandlingCode: string
  /** 对应后端字段 complaintId */
  complaintId: string
  /** 对应后端字段 complaintNo */
  complaintNo: string
  /** 对应后端字段 complaintItemId */
  complaintItemId?: string
  /** 对应后端字段 handlingStage */
  handlingStage: number
  /** 对应后端字段 handlingMethod */
  handlingMethod: number
  /** 对应后端字段 handlingDescription */
  handlingDescription: string
  /** 对应后端字段 causeAnalysis */
  causeAnalysis?: string
  /** 对应后端字段 correctiveAction */
  correctiveAction?: string
  /** 对应后端字段 preventiveAction */
  preventiveAction?: string
  /** 对应后端字段 responsibleDept */
  responsibleDept?: string
  /** 对应后端字段 responsibleBy */
  responsibleBy?: string
  /** 对应后端字段 handlerBy */
  handlerBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 plannedCompletionDate */
  plannedCompletionDate?: string
  /** 对应后端字段 actualCompletionDate */
  actualCompletionDate?: string
  /** 对应后端字段 handlingStatus */
  handlingStatus: number
  /** 对应后端字段 handlingCost */
  handlingCost?: number
  /** 对应后端字段 customerFeedback */
  customerFeedback?: string
  /** 对应后端字段 customerSatisfaction */
  customerSatisfaction?: number
  /** 对应后端字段 attachmentPaths */
  attachmentPaths?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * CustomerComplaintHandlingUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintHandlingUpdateDto）
 */
export interface CustomerComplaintHandlingUpdate extends CustomerComplaintHandlingCreate {
  /** 对应后端字段 customerComplaintHandlingId */
  customerComplaintHandlingId: string
}

/**
 * CustomerComplaintHandlingHandlingStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintHandlingHandlingStatusDto）
 */
export interface CustomerComplaintHandlingHandlingStatus {
  /** 对应后端字段 customerComplaintHandlingId */
  customerComplaintHandlingId: string
  /** 对应后端字段 handlingStatus */
  handlingStatus: number
}
