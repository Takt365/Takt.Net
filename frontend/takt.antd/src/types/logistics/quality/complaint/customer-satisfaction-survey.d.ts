// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/complaint/customer-satisfaction-survey
// 文件名称：customer-satisfaction-survey.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：customer-satisfaction-survey相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CustomerSatisfactionSurvey类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyDto）
 */
export interface CustomerSatisfactionSurvey extends TaktEntityBase {
  /** 对应后端字段 customerSatisfactionSurveyId */
  customerSatisfactionSurveyId: string
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 customerSatisfactionSurveyCode */
  customerSatisfactionSurveyCode: string
  /** 对应后端字段 customerId */
  customerId: string
  /** 对应后端字段 customerName */
  customerName: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 surveyDate */
  surveyDate: string
  /** 对应后端字段 surveyMethod */
  surveyMethod: number
  /** 对应后端字段 surveyType */
  surveyType: number
  /** 对应后端字段 surveyPeriod */
  surveyPeriod: number
  /** 对应后端字段 surveyorBy */
  surveyorBy?: string
  /** 对应后端字段 customerContact */
  customerContact?: string
  /** 对应后端字段 customerPhone */
  customerPhone?: string
  /** 对应后端字段 overallSatisfaction */
  overallSatisfaction: number
  /** 对应后端字段 totalScore */
  totalScore?: number
  /** 对应后端字段 qualityScore */
  qualityScore?: number
  /** 对应后端字段 deliveryScore */
  deliveryScore?: number
  /** 对应后端字段 serviceScore */
  serviceScore?: number
  /** 对应后端字段 priceScore */
  priceScore?: number
  /** 对应后端字段 technicalScore */
  technicalScore?: number
  /** 对应后端字段 customerPraise */
  customerPraise?: string
  /** 对应后端字段 customerFeedback */
  customerFeedback?: string
  /** 对应后端字段 improvementPlan */
  improvementPlan?: string
  /** 对应后端字段 surveyStatus */
  surveyStatus: number
  /** 对应后端字段 followUpStatus */
  followUpStatus: number
  /** 对应后端字段 relatedComplaintId */
  relatedComplaintId?: string
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * CustomerSatisfactionSurveyQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyQueryDto）
 */
export interface CustomerSatisfactionSurveyQuery extends TaktPagedQuery {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 customerSatisfactionSurveyCode */
  customerSatisfactionSurveyCode?: string
  /** 对应后端字段 customerId */
  customerId?: string
  /** 对应后端字段 customerName */
  customerName?: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 surveyDate */
  surveyDate?: string
  /** 对应后端字段 surveyDateStart */
  surveyDateStart?: string
  /** 对应后端字段 surveyDateEnd */
  surveyDateEnd?: string
  /** 对应后端字段 surveyMethod */
  surveyMethod?: number
  /** 对应后端字段 surveyType */
  surveyType?: number
  /** 对应后端字段 surveyPeriod */
  surveyPeriod?: number
  /** 对应后端字段 surveyorBy */
  surveyorBy?: string
  /** 对应后端字段 customerContact */
  customerContact?: string
  /** 对应后端字段 customerPhone */
  customerPhone?: string
  /** 对应后端字段 overallSatisfaction */
  overallSatisfaction?: number
  /** 对应后端字段 totalScore */
  totalScore?: number
  /** 对应后端字段 qualityScore */
  qualityScore?: number
  /** 对应后端字段 deliveryScore */
  deliveryScore?: number
  /** 对应后端字段 serviceScore */
  serviceScore?: number
  /** 对应后端字段 priceScore */
  priceScore?: number
  /** 对应后端字段 technicalScore */
  technicalScore?: number
  /** 对应后端字段 customerPraise */
  customerPraise?: string
  /** 对应后端字段 customerFeedback */
  customerFeedback?: string
  /** 对应后端字段 improvementPlan */
  improvementPlan?: string
  /** 对应后端字段 surveyStatus */
  surveyStatus?: number
  /** 对应后端字段 followUpStatus */
  followUpStatus?: number
  /** 对应后端字段 relatedComplaintId */
  relatedComplaintId?: string
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
 * CustomerSatisfactionSurveyCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyCreateDto）
 */
export interface CustomerSatisfactionSurveyCreate {
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 customerSatisfactionSurveyCode */
  customerSatisfactionSurveyCode: string
  /** 对应后端字段 customerId */
  customerId: string
  /** 对应后端字段 customerName */
  customerName: string
  /** 对应后端字段 customerCode */
  customerCode?: string
  /** 对应后端字段 surveyDate */
  surveyDate: string
  /** 对应后端字段 surveyMethod */
  surveyMethod: number
  /** 对应后端字段 surveyType */
  surveyType: number
  /** 对应后端字段 surveyPeriod */
  surveyPeriod: number
  /** 对应后端字段 surveyorBy */
  surveyorBy?: string
  /** 对应后端字段 customerContact */
  customerContact?: string
  /** 对应后端字段 customerPhone */
  customerPhone?: string
  /** 对应后端字段 overallSatisfaction */
  overallSatisfaction: number
  /** 对应后端字段 totalScore */
  totalScore?: number
  /** 对应后端字段 qualityScore */
  qualityScore?: number
  /** 对应后端字段 deliveryScore */
  deliveryScore?: number
  /** 对应后端字段 serviceScore */
  serviceScore?: number
  /** 对应后端字段 priceScore */
  priceScore?: number
  /** 对应后端字段 technicalScore */
  technicalScore?: number
  /** 对应后端字段 customerPraise */
  customerPraise?: string
  /** 对应后端字段 customerFeedback */
  customerFeedback?: string
  /** 对应后端字段 improvementPlan */
  improvementPlan?: string
  /** 对应后端字段 surveyStatus */
  surveyStatus: number
  /** 对应后端字段 followUpStatus */
  followUpStatus: number
  /** 对应后端字段 relatedComplaintId */
  relatedComplaintId?: string
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
 * CustomerSatisfactionSurveyUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyUpdateDto）
 */
export interface CustomerSatisfactionSurveyUpdate extends CustomerSatisfactionSurveyCreate {
  /** 对应后端字段 customerSatisfactionSurveyId */
  customerSatisfactionSurveyId: string
}

/**
 * CustomerSatisfactionSurveySort类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveySortDto）
 */
export interface CustomerSatisfactionSurveySort {
  /** 对应后端字段 customerSatisfactionSurveyId */
  customerSatisfactionSurveyId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * CustomerSatisfactionSurveyFollowUpStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyFollowUpStatusDto）
 */
export interface CustomerSatisfactionSurveyFollowUpStatus {
  /** 对应后端字段 customerSatisfactionSurveyId */
  customerSatisfactionSurveyId: string
  /** 对应后端字段 followUpStatus */
  followUpStatus: number
}

/**
 * CustomerSatisfactionSurveySurveyStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveySurveyStatusDto）
 */
export interface CustomerSatisfactionSurveySurveyStatus {
  /** 对应后端字段 customerSatisfactionSurveyId */
  customerSatisfactionSurveyId: string
  /** 对应后端字段 surveyStatus */
  surveyStatus: number
}
