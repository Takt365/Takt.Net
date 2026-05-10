// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/statistics/kanban/kanban-visit
// 文件名称：kanban-visit.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：kanban-visit相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * KanbanVisit类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanVisitDto）
 */
export interface KanbanVisit extends TaktEntityBase {
  /** 对应后端字段 kanbanVisitId */
  kanbanVisitId: string
  /** 对应后端字段 companyName */
  companyName: string
  /** 对应后端字段 visitStartTime */
  visitStartTime: string
  /** 对应后端字段 visitEndTime */
  visitEndTime: string
  /** 对应后端字段 personIds */
  personIds?: string[]
}

/**
 * KanbanVisitQuery类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanVisitQueryDto）
 */
export interface KanbanVisitQuery extends TaktPagedQuery {
  /** 对应后端字段 kanbanVisitId */
  kanbanVisitId: string
  /** 对应后端字段 companyName */
  companyName?: string
  /** 对应后端字段 visitStartTime */
  visitStartTime?: string
  /** 对应后端字段 visitStartTimeStart */
  visitStartTimeStart?: string
  /** 对应后端字段 visitStartTimeEnd */
  visitStartTimeEnd?: string
  /** 对应后端字段 visitEndTime */
  visitEndTime?: string
  /** 对应后端字段 visitEndTimeStart */
  visitEndTimeStart?: string
  /** 对应后端字段 visitEndTimeEnd */
  visitEndTimeEnd?: string
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
 * KanbanVisitCreate类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanVisitCreateDto）
 */
export interface KanbanVisitCreate {
  /** 对应后端字段 companyName */
  companyName: string
  /** 对应后端字段 visitStartTime */
  visitStartTime: string
  /** 对应后端字段 visitEndTime */
  visitEndTime: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * KanbanVisitUpdate类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanVisitUpdateDto）
 */
export interface KanbanVisitUpdate extends KanbanVisitCreate {
  /** 对应后端字段 kanbanVisitId */
  kanbanVisitId: string
}
