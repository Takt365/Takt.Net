// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/help-desk/ticket-category-assign
// 文件名称：ticket-category-assign.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ticket-category-assign相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * TicketCategoryAssign类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketCategoryAssignDto）
 */
export interface TicketCategoryAssign extends TaktEntityBase {
  /** 对应后端字段 ticketCategoryAssignId */
  ticketCategoryAssignId: string
  /** 对应后端字段 categoryCode */
  categoryCode: string
  /** 对应后端字段 assigneeId */
  assigneeId: string
  /** 对应后端字段 assigneeName */
  assigneeName?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * TicketCategoryAssignQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketCategoryAssignQueryDto）
 */
export interface TicketCategoryAssignQuery extends TaktPagedQuery {
  /** 对应后端字段 categoryCode */
  categoryCode?: string
  /** 对应后端字段 assigneeId */
  assigneeId?: string
  /** 对应后端字段 assigneeName */
  assigneeName?: string
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
 * TicketCategoryAssignCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketCategoryAssignCreateDto）
 */
export interface TicketCategoryAssignCreate {
  /** 对应后端字段 categoryCode */
  categoryCode: string
  /** 对应后端字段 assigneeId */
  assigneeId: string
  /** 对应后端字段 assigneeName */
  assigneeName?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * TicketCategoryAssignUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketCategoryAssignUpdateDto）
 */
export interface TicketCategoryAssignUpdate extends TicketCategoryAssignCreate {
  /** 对应后端字段 ticketCategoryAssignId */
  ticketCategoryAssignId: string
}

/**
 * TicketCategoryAssignSort类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktTicketCategoryAssignSortDto）
 */
export interface TicketCategoryAssignSort {
  /** 对应后端字段 ticketCategoryAssignId */
  ticketCategoryAssignId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
