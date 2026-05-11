// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/complaint/customer-complaint
// 文件名称：customer-complaint.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：customer-complaint相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CustomerComplaint类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintDto）
 */
export interface CustomerComplaint extends TaktEntityBase {
  /** 对应后端字段 customerComplaintId */
  customerComplaintId: string
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 customerComplaintCode */
  customerComplaintCode: string
  /** 对应后端字段 customerId */
  customerId: string
  /** 对应后端字段 customerName */
  customerName: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 complaintDate */
  complaintDate: string
  /** 对应后端字段 complaintMethod */
  complaintMethod: number
  /** 对应后端字段 complaintType */
  complaintType: number
  /** 对应后端字段 complaintLevel */
  complaintLevel: number
  /** 对应后端字段 responsibleDeptId */
  responsibleDeptId?: string
  /** 对应后端字段 responsibleDeptName */
  responsibleDeptName?: string
  /** 对应后端字段 responsiblePersonId */
  responsiblePersonId?: string
  /** 对应后端字段 responsiblePersonName */
  responsiblePersonName?: string
  /** 对应后端字段 requiredReplyDate */
  requiredReplyDate?: string
  /** 对应后端字段 actualReplyDate */
  actualReplyDate?: string
  /** 对应后端字段 complaintStatus */
  complaintStatus: number
  /** 对应后端字段 complaintDescription */
  complaintDescription: string
  /** 对应后端字段 handlingResult */
  handlingResult?: string
  /** 对应后端字段 customerSatisfaction */
  customerSatisfaction?: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * CustomerComplaintQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintQueryDto）
 */
export interface CustomerComplaintQuery extends TaktPagedQuery {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 customerComplaintCode */
  customerComplaintCode?: string
  /** 对应后端字段 customerId */
  customerId?: string
  /** 对应后端字段 customerName */
  customerName?: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 complaintDate */
  complaintDate?: string
  /** 对应后端字段 complaintDateStart */
  complaintDateStart?: string
  /** 对应后端字段 complaintDateEnd */
  complaintDateEnd?: string
  /** 对应后端字段 complaintMethod */
  complaintMethod?: number
  /** 对应后端字段 complaintType */
  complaintType?: number
  /** 对应后端字段 complaintLevel */
  complaintLevel?: number
  /** 对应后端字段 responsibleDeptId */
  responsibleDeptId?: string
  /** 对应后端字段 responsibleDeptName */
  responsibleDeptName?: string
  /** 对应后端字段 responsiblePersonId */
  responsiblePersonId?: string
  /** 对应后端字段 responsiblePersonName */
  responsiblePersonName?: string
  /** 对应后端字段 requiredReplyDate */
  requiredReplyDate?: string
  /** 对应后端字段 requiredReplyDateStart */
  requiredReplyDateStart?: string
  /** 对应后端字段 requiredReplyDateEnd */
  requiredReplyDateEnd?: string
  /** 对应后端字段 actualReplyDate */
  actualReplyDate?: string
  /** 对应后端字段 actualReplyDateStart */
  actualReplyDateStart?: string
  /** 对应后端字段 actualReplyDateEnd */
  actualReplyDateEnd?: string
  /** 对应后端字段 complaintStatus */
  complaintStatus?: number
  /** 对应后端字段 complaintDescription */
  complaintDescription?: string
  /** 对应后端字段 handlingResult */
  handlingResult?: string
  /** 对应后端字段 customerSatisfaction */
  customerSatisfaction?: number
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
 * CustomerComplaintCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintCreateDto）
 */
export interface CustomerComplaintCreate {
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 customerComplaintCode */
  customerComplaintCode: string
  /** 对应后端字段 customerId */
  customerId: string
  /** 对应后端字段 customerName */
  customerName: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 complaintDate */
  complaintDate: string
  /** 对应后端字段 complaintMethod */
  complaintMethod: number
  /** 对应后端字段 complaintType */
  complaintType: number
  /** 对应后端字段 complaintLevel */
  complaintLevel: number
  /** 对应后端字段 responsibleDeptId */
  responsibleDeptId?: string
  /** 对应后端字段 responsibleDeptName */
  responsibleDeptName?: string
  /** 对应后端字段 responsiblePersonId */
  responsiblePersonId?: string
  /** 对应后端字段 responsiblePersonName */
  responsiblePersonName?: string
  /** 对应后端字段 requiredReplyDate */
  requiredReplyDate?: string
  /** 对应后端字段 actualReplyDate */
  actualReplyDate?: string
  /** 对应后端字段 complaintStatus */
  complaintStatus: number
  /** 对应后端字段 complaintDescription */
  complaintDescription: string
  /** 对应后端字段 handlingResult */
  handlingResult?: string
  /** 对应后端字段 customerSatisfaction */
  customerSatisfaction?: number
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
 * CustomerComplaintUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintUpdateDto）
 */
export interface CustomerComplaintUpdate extends CustomerComplaintCreate {
  /** 对应后端字段 customerComplaintId */
  customerComplaintId: string
}

/**
 * CustomerComplaintSort类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintSortDto）
 */
export interface CustomerComplaintSort {
  /** 对应后端字段 customerComplaintId */
  customerComplaintId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * CustomerComplaintComplaintStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerComplaintComplaintStatusDto）
 */
export interface CustomerComplaintComplaintStatus {
  /** 对应后端字段 customerComplaintId */
  customerComplaintId: string
  /** 对应后端字段 complaintStatus */
  complaintStatus: number
}
