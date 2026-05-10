// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/help-desk/ticket-evaluation
// 文件名称：ticket-evaluation.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ticket-evaluation相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * TicketEvaluation类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketEvaluationDto）
 */
export interface TicketEvaluation extends TaktEntityBase {
  /** 对应后端字段 ticketEvaluationId */
  ticketEvaluationId: string
  /** 对应后端字段 ticketId */
  ticketId: string
  /** 对应后端字段 score */
  score: number
  /** 对应后端字段 comment */
  comment?: string
  /** 对应后端字段 evaluatorId */
  evaluatorId: string
  /** 对应后端字段 evaluatorName */
  evaluatorName?: string
  /** 对应后端字段 evaluatedAt */
  evaluatedAt: string
  /** 对应后端字段 ticket */
  ticket?: unknown
}

/**
 * TicketEvaluationQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketEvaluationQueryDto）
 */
export interface TicketEvaluationQuery extends TaktPagedQuery {
  /** 对应后端字段 ticketId */
  ticketId?: string
  /** 对应后端字段 score */
  score?: number
  /** 对应后端字段 comment */
  comment?: string
  /** 对应后端字段 evaluatorId */
  evaluatorId?: string
  /** 对应后端字段 evaluatorName */
  evaluatorName?: string
  /** 对应后端字段 evaluatedAt */
  evaluatedAt?: string
  /** 对应后端字段 evaluatedAtStart */
  evaluatedAtStart?: string
  /** 对应后端字段 evaluatedAtEnd */
  evaluatedAtEnd?: string
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
 * TicketEvaluationCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketEvaluationCreateDto）
 */
export interface TicketEvaluationCreate {
  /** 对应后端字段 ticketId */
  ticketId: string
  /** 对应后端字段 score */
  score: number
  /** 对应后端字段 comment */
  comment?: string
  /** 对应后端字段 evaluatorId */
  evaluatorId: string
  /** 对应后端字段 evaluatorName */
  evaluatorName?: string
  /** 对应后端字段 evaluatedAt */
  evaluatedAt: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * TicketEvaluationUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketEvaluationUpdateDto）
 */
export interface TicketEvaluationUpdate extends TicketEvaluationCreate {
  /** 对应后端字段 ticketEvaluationId */
  ticketEvaluationId: string
}
