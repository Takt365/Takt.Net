// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-issue-meeting
// 文件名称：quality-issue-meeting.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-issue-meeting相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityIssueMeeting类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueMeetingDto）
 */
export interface QualityIssueMeeting extends TaktEntityBase {
  /** 对应后端字段 qualityIssueMeetingId */
  qualityIssueMeetingId: string
  /** 对应后端字段 qualityIssueId */
  qualityIssueId: string
  /** 对应后端字段 qualityIssueCode */
  qualityIssueCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 directManpowerCostPerMinute */
  directManpowerCostPerMinute: number
  /** 对应后端字段 indirectManpowerCostPerMinute */
  indirectManpowerCostPerMinute: number
  /** 对应后端字段 meetingInvestigationContent */
  meetingInvestigationContent?: string
  /** 对应后端字段 meetingInvestigationCost */
  meetingInvestigationCost: number
  /** 对应后端字段 meetingTimeMinutes */
  meetingTimeMinutes: number
  /** 对应后端字段 directParticipantCount */
  directParticipantCount: number
  /** 对应后端字段 indirectParticipantCount */
  indirectParticipantCount: number
  /** 对应后端字段 investigationWorkTimeMinutes */
  investigationWorkTimeMinutes: number
  /** 对应后端字段 travelCost */
  travelCost: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 otherWorkTimeMinutes */
  otherWorkTimeMinutes: number
  /** 对应后端字段 otherApparatusCost */
  otherApparatusCost: number
  /** 对应后端字段 meetingRecorder */
  meetingRecorder?: string
  /** 对应后端字段 issue */
  issue?: unknown
}

/**
 * QualityIssueMeetingQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueMeetingQueryDto）
 */
export interface QualityIssueMeetingQuery extends TaktPagedQuery {
  /** 对应后端字段 qualityIssueId */
  qualityIssueId?: string
  /** 对应后端字段 qualityIssueCode */
  qualityIssueCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 directManpowerCostPerMinute */
  directManpowerCostPerMinute?: number
  /** 对应后端字段 indirectManpowerCostPerMinute */
  indirectManpowerCostPerMinute?: number
  /** 对应后端字段 meetingInvestigationContent */
  meetingInvestigationContent?: string
  /** 对应后端字段 meetingInvestigationCost */
  meetingInvestigationCost?: number
  /** 对应后端字段 meetingTimeMinutes */
  meetingTimeMinutes?: number
  /** 对应后端字段 directParticipantCount */
  directParticipantCount?: number
  /** 对应后端字段 indirectParticipantCount */
  indirectParticipantCount?: number
  /** 对应后端字段 investigationWorkTimeMinutes */
  investigationWorkTimeMinutes?: number
  /** 对应后端字段 travelCost */
  travelCost?: number
  /** 对应后端字段 otherExpenses */
  otherExpenses?: number
  /** 对应后端字段 otherWorkTimeMinutes */
  otherWorkTimeMinutes?: number
  /** 对应后端字段 otherApparatusCost */
  otherApparatusCost?: number
  /** 对应后端字段 meetingRecorder */
  meetingRecorder?: string
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
 * QualityIssueMeetingCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueMeetingCreateDto）
 */
export interface QualityIssueMeetingCreate {
  /** 对应后端字段 qualityIssueId */
  qualityIssueId: string
  /** 对应后端字段 qualityIssueCode */
  qualityIssueCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 directManpowerCostPerMinute */
  directManpowerCostPerMinute: number
  /** 对应后端字段 indirectManpowerCostPerMinute */
  indirectManpowerCostPerMinute: number
  /** 对应后端字段 meetingInvestigationContent */
  meetingInvestigationContent?: string
  /** 对应后端字段 meetingInvestigationCost */
  meetingInvestigationCost: number
  /** 对应后端字段 meetingTimeMinutes */
  meetingTimeMinutes: number
  /** 对应后端字段 directParticipantCount */
  directParticipantCount: number
  /** 对应后端字段 indirectParticipantCount */
  indirectParticipantCount: number
  /** 对应后端字段 investigationWorkTimeMinutes */
  investigationWorkTimeMinutes: number
  /** 对应后端字段 travelCost */
  travelCost: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 otherWorkTimeMinutes */
  otherWorkTimeMinutes: number
  /** 对应后端字段 otherApparatusCost */
  otherApparatusCost: number
  /** 对应后端字段 meetingRecorder */
  meetingRecorder?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * QualityIssueMeetingUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityIssueMeetingUpdateDto）
 */
export interface QualityIssueMeetingUpdate extends QualityIssueMeetingCreate {
  /** 对应后端字段 qualityIssueMeetingId */
  qualityIssueMeetingId: string
}
