// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/statistics/kanban/kanban-visit-person
// 文件名称：kanban-visit-person.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：kanban-visit-person相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * KanbanVisitPerson类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanVisitPersonDto）
 */
export interface KanbanVisitPerson extends TaktEntityBase {
  /** 对应后端字段 kanbanVisitPersonId */
  kanbanVisitPersonId: string
  /** 对应后端字段 visitId */
  visitId: string
  /** 对应后端字段 department */
  department?: string
  /** 对应后端字段 jobTitle */
  jobTitle?: string
  /** 对应后端字段 personName */
  personName: string
}

/**
 * KanbanVisitPersonQuery类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanVisitPersonQueryDto）
 */
export interface KanbanVisitPersonQuery extends TaktPagedQuery {
  /** 对应后端字段 kanbanVisitPersonId */
  kanbanVisitPersonId: string
  /** 对应后端字段 visitId */
  visitId?: string
  /** 对应后端字段 department */
  department?: string
  /** 对应后端字段 jobTitle */
  jobTitle?: string
  /** 对应后端字段 personName */
  personName?: string
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
 * KanbanVisitPersonCreate类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanVisitPersonCreateDto）
 */
export interface KanbanVisitPersonCreate {
  /** 对应后端字段 visitId */
  visitId: string
  /** 对应后端字段 department */
  department?: string
  /** 对应后端字段 jobTitle */
  jobTitle?: string
  /** 对应后端字段 personName */
  personName: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * KanbanVisitPersonUpdate类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanVisitPersonUpdateDto）
 */
export interface KanbanVisitPersonUpdate extends KanbanVisitPersonCreate {
  /** 对应后端字段 kanbanVisitPersonId */
  kanbanVisitPersonId: string
}
