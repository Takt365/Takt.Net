// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/complaint/customer-satisfaction-survey-item
// 文件名称：customer-satisfaction-survey-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：customer-satisfaction-survey-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CustomerSatisfactionSurveyItem类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyItemDto）
 */
export interface CustomerSatisfactionSurveyItem extends TaktEntityBase {
  /** 对应后端字段 customerSatisfactionSurveyItemId */
  customerSatisfactionSurveyItemId: string
  /** 对应后端字段 surveyId */
  surveyId: string
  /** 对应后端字段 customerSatisfactionSurveyCode */
  customerSatisfactionSurveyCode: string
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
  /** 对应后端字段 score */
  score?: number
  /** 对应后端字段 satisfactionLevel */
  satisfactionLevel?: number
  /** 对应后端字段 customerFeedback */
  customerFeedback?: string
  /** 对应后端字段 improvementSuggestion */
  improvementSuggestion?: string
  /** 对应后端字段 followUpAction */
  followUpAction?: string
  /** 对应后端字段 followUpStatus */
  followUpStatus: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 survey */
  survey?: unknown
}

/**
 * CustomerSatisfactionSurveyItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyItemQueryDto）
 */
export interface CustomerSatisfactionSurveyItemQuery extends TaktPagedQuery {
  /** 对应后端字段 surveyId */
  surveyId?: string
  /** 对应后端字段 customerSatisfactionSurveyCode */
  customerSatisfactionSurveyCode?: string
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
  /** 对应后端字段 score */
  score?: number
  /** 对应后端字段 satisfactionLevel */
  satisfactionLevel?: number
  /** 对应后端字段 customerFeedback */
  customerFeedback?: string
  /** 对应后端字段 improvementSuggestion */
  improvementSuggestion?: string
  /** 对应后端字段 followUpAction */
  followUpAction?: string
  /** 对应后端字段 followUpStatus */
  followUpStatus?: number
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
 * CustomerSatisfactionSurveyItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyItemCreateDto）
 */
export interface CustomerSatisfactionSurveyItemCreate {
  /** 对应后端字段 surveyId */
  surveyId: string
  /** 对应后端字段 customerSatisfactionSurveyCode */
  customerSatisfactionSurveyCode: string
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
  /** 对应后端字段 score */
  score?: number
  /** 对应后端字段 satisfactionLevel */
  satisfactionLevel?: number
  /** 对应后端字段 customerFeedback */
  customerFeedback?: string
  /** 对应后端字段 improvementSuggestion */
  improvementSuggestion?: string
  /** 对应后端字段 followUpAction */
  followUpAction?: string
  /** 对应后端字段 followUpStatus */
  followUpStatus: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * CustomerSatisfactionSurveyItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyItemUpdateDto）
 */
export interface CustomerSatisfactionSurveyItemUpdate extends CustomerSatisfactionSurveyItemCreate {
  /** 对应后端字段 customerSatisfactionSurveyItemId */
  customerSatisfactionSurveyItemId: string
}

/**
 * CustomerSatisfactionSurveyItemSort类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyItemSortDto）
 */
export interface CustomerSatisfactionSurveyItemSort {
  /** 对应后端字段 customerSatisfactionSurveyItemId */
  customerSatisfactionSurveyItemId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * CustomerSatisfactionSurveyItemFollowUpStatus类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyItemFollowUpStatusDto）
 */
export interface CustomerSatisfactionSurveyItemFollowUpStatus {
  /** 对应后端字段 customerSatisfactionSurveyItemId */
  customerSatisfactionSurveyItemId: string
  /** 对应后端字段 followUpStatus */
  followUpStatus: number
}
