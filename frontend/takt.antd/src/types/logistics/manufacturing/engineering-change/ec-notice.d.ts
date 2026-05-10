// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/engineering-change/ec-notice
// 文件名称：ec-notice.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ec-notice相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EcNotice类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcNoticeDto）
 */
export interface EcNotice extends TaktEntityBase {
  /** 对应后端字段 ecNoticeId */
  ecNoticeId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 noticeNo */
  noticeNo: string
  /** 对应后端字段 ecnId */
  ecnId: string
  /** 对应后端字段 ecnNo */
  ecnNo: string
  /** 对应后端字段 ecnTitle */
  ecnTitle?: string
  /** 对应后端字段 noticeDate */
  noticeDate: string
  /** 对应后端字段 noticeDeptCodes */
  noticeDeptCodes?: string
  /** 对应后端字段 noticeDeptNames */
  noticeDeptNames?: string
  /** 对应后端字段 notifierId */
  notifierId?: string
  /** 对应后端字段 notifierName */
  notifierName?: string
  /** 对应后端字段 noticeMethod */
  noticeMethod: number
  /** 对应后端字段 noticeStatus */
  noticeStatus: number
  /** 对应后端字段 confirmerId */
  confirmerId?: string
  /** 对应后端字段 confirmerName */
  confirmerName?: string
  /** 对应后端字段 confirmDate */
  confirmDate?: string
  /** 对应后端字段 confirmComment */
  confirmComment?: string
  /** 对应后端字段 requireFeedbackDate */
  requireFeedbackDate?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
  /** 对应后端字段 ecn */
  ecn?: unknown
}

/**
 * EcNoticeQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcNoticeQueryDto）
 */
export interface EcNoticeQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 noticeNo */
  noticeNo?: string
  /** 对应后端字段 ecnId */
  ecnId?: string
  /** 对应后端字段 ecnNo */
  ecnNo?: string
  /** 对应后端字段 ecnTitle */
  ecnTitle?: string
  /** 对应后端字段 noticeDate */
  noticeDate?: string
  /** 对应后端字段 noticeDateStart */
  noticeDateStart?: string
  /** 对应后端字段 noticeDateEnd */
  noticeDateEnd?: string
  /** 对应后端字段 noticeDeptCodes */
  noticeDeptCodes?: string
  /** 对应后端字段 noticeDeptNames */
  noticeDeptNames?: string
  /** 对应后端字段 notifierId */
  notifierId?: string
  /** 对应后端字段 notifierName */
  notifierName?: string
  /** 对应后端字段 noticeMethod */
  noticeMethod?: number
  /** 对应后端字段 noticeStatus */
  noticeStatus?: number
  /** 对应后端字段 confirmerId */
  confirmerId?: string
  /** 对应后端字段 confirmerName */
  confirmerName?: string
  /** 对应后端字段 confirmDate */
  confirmDate?: string
  /** 对应后端字段 confirmDateStart */
  confirmDateStart?: string
  /** 对应后端字段 confirmDateEnd */
  confirmDateEnd?: string
  /** 对应后端字段 confirmComment */
  confirmComment?: string
  /** 对应后端字段 requireFeedbackDate */
  requireFeedbackDate?: string
  /** 对应后端字段 requireFeedbackDateStart */
  requireFeedbackDateStart?: string
  /** 对应后端字段 requireFeedbackDateEnd */
  requireFeedbackDateEnd?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
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
 * EcNoticeCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcNoticeCreateDto）
 */
export interface EcNoticeCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 noticeNo */
  noticeNo: string
  /** 对应后端字段 ecnId */
  ecnId: string
  /** 对应后端字段 ecnNo */
  ecnNo: string
  /** 对应后端字段 ecnTitle */
  ecnTitle?: string
  /** 对应后端字段 noticeDate */
  noticeDate: string
  /** 对应后端字段 noticeDeptCodes */
  noticeDeptCodes?: string
  /** 对应后端字段 noticeDeptNames */
  noticeDeptNames?: string
  /** 对应后端字段 notifierId */
  notifierId?: string
  /** 对应后端字段 notifierName */
  notifierName?: string
  /** 对应后端字段 noticeMethod */
  noticeMethod: number
  /** 对应后端字段 noticeStatus */
  noticeStatus: number
  /** 对应后端字段 confirmerId */
  confirmerId?: string
  /** 对应后端字段 confirmerName */
  confirmerName?: string
  /** 对应后端字段 confirmDate */
  confirmDate?: string
  /** 对应后端字段 confirmComment */
  confirmComment?: string
  /** 对应后端字段 requireFeedbackDate */
  requireFeedbackDate?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EcNoticeUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcNoticeUpdateDto）
 */
export interface EcNoticeUpdate extends EcNoticeCreate {
  /** 对应后端字段 ecNoticeId */
  ecNoticeId: string
}

/**
 * EcNoticeNoticeStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcNoticeNoticeStatusDto）
 */
export interface EcNoticeNoticeStatus {
  /** 对应后端字段 ecNoticeId */
  ecNoticeId: string
  /** 对应后端字段 noticeStatus */
  noticeStatus: number
}
