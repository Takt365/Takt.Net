// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/help-desk/ticket
// 文件名称：ticket.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ticket相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Ticket类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketDto）
 */
export interface Ticket extends TaktEntityBase {
  /** 对应后端字段 ticketId */
  ticketId: string
  /** 对应后端字段 ticketNo */
  ticketNo: string
  /** 对应后端字段 title */
  title: string
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 attachmentsJson */
  attachmentsJson?: string
  /** 对应后端字段 ticketStatus */
  ticketStatus: number
  /** 对应后端字段 priority */
  priority: number
  /** 对应后端字段 categoryCode */
  categoryCode?: string
  /** 对应后端字段 ticketSource */
  ticketSource: number
  /** 对应后端字段 submitterId */
  submitterId: string
  /** 对应后端字段 submitterName */
  submitterName?: string
  /** 对应后端字段 assigneeId */
  assigneeId?: string
  /** 对应后端字段 assigneeName */
  assigneeName?: string
  /** 对应后端字段 knowledgeId */
  knowledgeId?: string
  /** 对应后端字段 parentTicketId */
  parentTicketId?: string
  /** 对应后端字段 firstResponseAt */
  firstResponseAt?: string
  /** 对应后端字段 firstResponseDueBy */
  firstResponseDueBy?: string
  /** 对应后端字段 resolvedAt */
  resolvedAt?: string
  /** 对应后端字段 resolutionDueBy */
  resolutionDueBy?: string
  /** 对应后端字段 closedAt */
  closedAt?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 applicantDeptId */
  applicantDeptId?: string
  /** 对应后端字段 applicantDeptName */
  applicantDeptName?: string
  /** 对应后端字段 applicantId */
  applicantId?: string
  /** 对应后端字段 applicant */
  applicant?: string
  /** 对应后端字段 applicationDate */
  applicationDate?: string
  /** 对应后端字段 childTickets */
  childTickets?: Ticket[]
  /** 对应后端字段 evaluation */
  evaluation?: unknown
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * TicketQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketQueryDto）
 */
export interface TicketQuery extends TaktPagedQuery {
  /** 对应后端字段 ticketNo */
  ticketNo?: string
  /** 对应后端字段 title */
  title?: string
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 attachmentsJson */
  attachmentsJson?: string
  /** 对应后端字段 ticketStatus */
  ticketStatus?: number
  /** 对应后端字段 priority */
  priority?: number
  /** 对应后端字段 categoryCode */
  categoryCode?: string
  /** 对应后端字段 ticketSource */
  ticketSource?: number
  /** 对应后端字段 submitterId */
  submitterId?: string
  /** 对应后端字段 submitterName */
  submitterName?: string
  /** 对应后端字段 assigneeId */
  assigneeId?: string
  /** 对应后端字段 assigneeName */
  assigneeName?: string
  /** 对应后端字段 knowledgeId */
  knowledgeId?: string
  /** 对应后端字段 parentTicketId */
  parentTicketId?: string
  /** 对应后端字段 firstResponseAt */
  firstResponseAt?: string
  /** 对应后端字段 firstResponseAtStart */
  firstResponseAtStart?: string
  /** 对应后端字段 firstResponseAtEnd */
  firstResponseAtEnd?: string
  /** 对应后端字段 firstResponseDueBy */
  firstResponseDueBy?: string
  /** 对应后端字段 firstResponseDueByStart */
  firstResponseDueByStart?: string
  /** 对应后端字段 firstResponseDueByEnd */
  firstResponseDueByEnd?: string
  /** 对应后端字段 resolvedAt */
  resolvedAt?: string
  /** 对应后端字段 resolvedAtStart */
  resolvedAtStart?: string
  /** 对应后端字段 resolvedAtEnd */
  resolvedAtEnd?: string
  /** 对应后端字段 resolutionDueBy */
  resolutionDueBy?: string
  /** 对应后端字段 resolutionDueByStart */
  resolutionDueByStart?: string
  /** 对应后端字段 resolutionDueByEnd */
  resolutionDueByEnd?: string
  /** 对应后端字段 closedAt */
  closedAt?: string
  /** 对应后端字段 closedAtStart */
  closedAtStart?: string
  /** 对应后端字段 closedAtEnd */
  closedAtEnd?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 applicantDeptId */
  applicantDeptId?: string
  /** 对应后端字段 applicantDeptName */
  applicantDeptName?: string
  /** 对应后端字段 applicantId */
  applicantId?: string
  /** 对应后端字段 applicant */
  applicant?: string
  /** 对应后端字段 applicationDate */
  applicationDate?: string
  /** 对应后端字段 applicationDateStart */
  applicationDateStart?: string
  /** 对应后端字段 applicationDateEnd */
  applicationDateEnd?: string
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
 * TicketCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketCreateDto）
 */
export interface TicketCreate {
  /** 对应后端字段 ticketNo */
  ticketNo: string
  /** 对应后端字段 title */
  title: string
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 attachmentsJson */
  attachmentsJson?: string
  /** 对应后端字段 ticketStatus */
  ticketStatus: number
  /** 对应后端字段 priority */
  priority: number
  /** 对应后端字段 categoryCode */
  categoryCode?: string
  /** 对应后端字段 ticketSource */
  ticketSource: number
  /** 对应后端字段 submitterId */
  submitterId: string
  /** 对应后端字段 submitterName */
  submitterName?: string
  /** 对应后端字段 assigneeId */
  assigneeId?: string
  /** 对应后端字段 assigneeName */
  assigneeName?: string
  /** 对应后端字段 knowledgeId */
  knowledgeId?: string
  /** 对应后端字段 parentTicketId */
  parentTicketId?: string
  /** 对应后端字段 firstResponseAt */
  firstResponseAt?: string
  /** 对应后端字段 firstResponseDueBy */
  firstResponseDueBy?: string
  /** 对应后端字段 resolvedAt */
  resolvedAt?: string
  /** 对应后端字段 resolutionDueBy */
  resolutionDueBy?: string
  /** 对应后端字段 closedAt */
  closedAt?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 applicantDeptId */
  applicantDeptId?: string
  /** 对应后端字段 applicantDeptName */
  applicantDeptName?: string
  /** 对应后端字段 applicantId */
  applicantId?: string
  /** 对应后端字段 applicant */
  applicant?: string
  /** 对应后端字段 applicationDate */
  applicationDate?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 childTickets */
  childTickets?: TicketCreate[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * TicketUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketUpdateDto）
 */
export interface TicketUpdate extends TicketCreate {
  /** 对应后端字段 ticketId */
  ticketId: string
}

/**
 * TicketStatus类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketStatusDto）
 */
export interface TicketStatus {
  /** 对应后端字段 ticketId */
  ticketId: string
  /** 对应后端字段 ticketStatus */
  ticketStatus: number
}
