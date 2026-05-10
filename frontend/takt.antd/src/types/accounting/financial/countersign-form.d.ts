// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/financial/countersign-form
// 文件名称：countersign-form.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：countersign-form相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CountersignForm类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCountersignFormDto）
 */
export interface CountersignForm extends TaktEntityBase {
  /** 对应后端字段 countersignFormId */
  countersignFormId: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 countersignCode */
  countersignCode: string
  /** 对应后端字段 countersignDepts */
  countersignDepts?: string
  /** 对应后端字段 financeDept */
  financeDept?: string
  /** 对应后端字段 budgetReviewComment */
  budgetReviewComment?: string
  /** 对应后端字段 executiveOffice */
  executiveOffice?: string
  /** 对应后端字段 approvalDate */
  approvalDate?: string
  /** 对应后端字段 applicationDate */
  applicationDate?: string
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 applicantName */
  applicantName?: string
  /** 对应后端字段 applicationDept */
  applicationDept?: string
  /** 对应后端字段 costBearerDept */
  costBearerDept?: string
  /** 对应后端字段 isBudget */
  isBudget: number
  /** 对应后端字段 budgetItem */
  budgetItem?: string
  /** 对应后端字段 budgetAmount */
  budgetAmount: number
  /** 对应后端字段 applicationAmount */
  applicationAmount: number
  /** 对应后端字段 countersignTitle */
  countersignTitle?: string
  /** 对应后端字段 applicationReason */
  applicationReason?: string
  /** 对应后端字段 budgetUsageDescription */
  budgetUsageDescription?: string
  /** 对应后端字段 targetAndExpectedBenefit */
  targetAndExpectedBenefit?: string
  /** 对应后端字段 attachments */
  attachments?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 countersignStatus */
  countersignStatus: number
}

/**
 * CountersignFormQuery类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCountersignFormQueryDto）
 */
export interface CountersignFormQuery extends TaktPagedQuery {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 countersignCode */
  countersignCode?: string
  /** 对应后端字段 countersignDepts */
  countersignDepts?: string
  /** 对应后端字段 financeDept */
  financeDept?: string
  /** 对应后端字段 budgetReviewComment */
  budgetReviewComment?: string
  /** 对应后端字段 executiveOffice */
  executiveOffice?: string
  /** 对应后端字段 approvalDate */
  approvalDate?: string
  /** 对应后端字段 approvalDateStart */
  approvalDateStart?: string
  /** 对应后端字段 approvalDateEnd */
  approvalDateEnd?: string
  /** 对应后端字段 applicationDate */
  applicationDate?: string
  /** 对应后端字段 applicationDateStart */
  applicationDateStart?: string
  /** 对应后端字段 applicationDateEnd */
  applicationDateEnd?: string
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 applicantName */
  applicantName?: string
  /** 对应后端字段 applicationDept */
  applicationDept?: string
  /** 对应后端字段 costBearerDept */
  costBearerDept?: string
  /** 对应后端字段 isBudget */
  isBudget?: number
  /** 对应后端字段 budgetItem */
  budgetItem?: string
  /** 对应后端字段 budgetAmount */
  budgetAmount?: number
  /** 对应后端字段 applicationAmount */
  applicationAmount?: number
  /** 对应后端字段 countersignTitle */
  countersignTitle?: string
  /** 对应后端字段 applicationReason */
  applicationReason?: string
  /** 对应后端字段 budgetUsageDescription */
  budgetUsageDescription?: string
  /** 对应后端字段 targetAndExpectedBenefit */
  targetAndExpectedBenefit?: string
  /** 对应后端字段 attachments */
  attachments?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 countersignStatus */
  countersignStatus?: number
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
 * CountersignFormCreate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCountersignFormCreateDto）
 */
export interface CountersignFormCreate {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 countersignCode */
  countersignCode: string
  /** 对应后端字段 countersignDepts */
  countersignDepts?: string
  /** 对应后端字段 financeDept */
  financeDept?: string
  /** 对应后端字段 budgetReviewComment */
  budgetReviewComment?: string
  /** 对应后端字段 executiveOffice */
  executiveOffice?: string
  /** 对应后端字段 approvalDate */
  approvalDate?: string
  /** 对应后端字段 applicationDate */
  applicationDate?: string
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 applicantName */
  applicantName?: string
  /** 对应后端字段 applicationDept */
  applicationDept?: string
  /** 对应后端字段 costBearerDept */
  costBearerDept?: string
  /** 对应后端字段 isBudget */
  isBudget: number
  /** 对应后端字段 budgetItem */
  budgetItem?: string
  /** 对应后端字段 budgetAmount */
  budgetAmount: number
  /** 对应后端字段 applicationAmount */
  applicationAmount: number
  /** 对应后端字段 countersignTitle */
  countersignTitle?: string
  /** 对应后端字段 applicationReason */
  applicationReason?: string
  /** 对应后端字段 budgetUsageDescription */
  budgetUsageDescription?: string
  /** 对应后端字段 targetAndExpectedBenefit */
  targetAndExpectedBenefit?: string
  /** 对应后端字段 attachments */
  attachments?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 countersignStatus */
  countersignStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * CountersignFormUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCountersignFormUpdateDto）
 */
export interface CountersignFormUpdate extends CountersignFormCreate {
  /** 对应后端字段 countersignFormId */
  countersignFormId: string
}

/**
 * CountersignFormCountersignStatus类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCountersignFormCountersignStatusDto）
 */
export interface CountersignFormCountersignStatus {
  /** 对应后端字段 countersignFormId */
  countersignFormId: string
  /** 对应后端字段 countersignStatus */
  countersignStatus: number
}
