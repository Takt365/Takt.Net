// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/help-desk/ticket-change-log
// 文件名称：ticket-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ticket-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * TicketChangeLog类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketChangeLogDto）
 */
export interface TicketChangeLog extends TaktEntityBase {
  /** 对应后端字段 ticketChangeLogId */
  ticketChangeLogId: string
  /** 对应后端字段 ticketId */
  ticketId: string
  /** 对应后端字段 ticketNo */
  ticketNo?: string
  /** 对应后端字段 changeType */
  changeType: number
  /** 对应后端字段 changeSummary */
  changeSummary?: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 ticket */
  ticket?: unknown
}

/**
 * TicketChangeLogQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketChangeLogQueryDto）
 */
export interface TicketChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 ticketId */
  ticketId?: string
  /** 对应后端字段 ticketNo */
  ticketNo?: string
  /** 对应后端字段 changeType */
  changeType?: number
  /** 对应后端字段 changeSummary */
  changeSummary?: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
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
 * TicketChangeLogCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketChangeLogCreateDto）
 */
export interface TicketChangeLogCreate {
  /** 对应后端字段 ticketId */
  ticketId: string
  /** 对应后端字段 ticketNo */
  ticketNo?: string
  /** 对应后端字段 changeType */
  changeType: number
  /** 对应后端字段 changeSummary */
  changeSummary?: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * TicketChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketChangeLogUpdateDto）
 */
export interface TicketChangeLogUpdate extends TicketChangeLogCreate {
  /** 对应后端字段 ticketChangeLogId */
  ticketChangeLogId: string
}
