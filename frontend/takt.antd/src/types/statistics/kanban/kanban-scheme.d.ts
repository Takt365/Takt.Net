// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/statistics/kanban/kanban-scheme
// 文件名称：kanban-scheme.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：kanban-scheme相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * KanbanScheme类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanSchemeDto）
 */
export interface KanbanScheme extends TaktEntityBase {
  /** 对应后端字段 kanbanSchemeId */
  kanbanSchemeId: string
  /** 对应后端字段 schemeCode */
  schemeCode: string
  /** 对应后端字段 schemeName */
  schemeName: string
  /** 对应后端字段 kanbanType */
  kanbanType: string
  /** 对应后端字段 schemeDescription */
  schemeDescription: string
  /** 对应后端字段 dataSourceConfig */
  dataSourceConfig: string
  /** 对应后端字段 layoutConfig */
  layoutConfig: string
  /** 对应后端字段 componentConfig */
  componentConfig: string
  /** 对应后端字段 refreshStrategy */
  refreshStrategy: string
  /** 对应后端字段 refreshInterval */
  refreshInterval: number
  /** 对应后端字段 themeStyle */
  themeStyle: string
  /** 对应后端字段 isFullscreen */
  isFullscreen: number
  /** 对应后端字段 enableAlert */
  enableAlert: number
  /** 对应后端字段 alertConfig */
  alertConfig: string
  /** 对应后端字段 filterConfig */
  filterConfig: string
  /** 对应后端字段 sortConfig */
  sortConfig: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 workshopCode */
  workshopCode: string
  /** 对应后端字段 lineCode */
  lineCode: string
  /** 对应后端字段 displayOrder */
  displayOrder: number
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 isPublic */
  isPublic: number
  /** 对应后端字段 creatorIds */
  creatorIds: string
  /** 对应后端字段 accessConfig */
  accessConfig: string
}

/**
 * KanbanSchemeQuery类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanSchemeQueryDto）
 */
export interface KanbanSchemeQuery extends TaktPagedQuery {
  /** 对应后端字段 schemeCode */
  schemeCode?: string
  /** 对应后端字段 schemeName */
  schemeName?: string
  /** 对应后端字段 kanbanType */
  kanbanType?: string
  /** 对应后端字段 schemeDescription */
  schemeDescription?: string
  /** 对应后端字段 dataSourceConfig */
  dataSourceConfig?: string
  /** 对应后端字段 layoutConfig */
  layoutConfig?: string
  /** 对应后端字段 componentConfig */
  componentConfig?: string
  /** 对应后端字段 refreshStrategy */
  refreshStrategy?: string
  /** 对应后端字段 refreshInterval */
  refreshInterval?: number
  /** 对应后端字段 themeStyle */
  themeStyle?: string
  /** 对应后端字段 isFullscreen */
  isFullscreen?: number
  /** 对应后端字段 enableAlert */
  enableAlert?: number
  /** 对应后端字段 alertConfig */
  alertConfig?: string
  /** 对应后端字段 filterConfig */
  filterConfig?: string
  /** 对应后端字段 sortConfig */
  sortConfig?: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 workshopCode */
  workshopCode?: string
  /** 对应后端字段 lineCode */
  lineCode?: string
  /** 对应后端字段 displayOrder */
  displayOrder?: number
  /** 对应后端字段 status */
  status?: number
  /** 对应后端字段 isPublic */
  isPublic?: number
  /** 对应后端字段 creatorIds */
  creatorIds?: string
  /** 对应后端字段 accessConfig */
  accessConfig?: string
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
 * KanbanSchemeCreate类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanSchemeCreateDto）
 */
export interface KanbanSchemeCreate {
  /** 对应后端字段 schemeCode */
  schemeCode: string
  /** 对应后端字段 schemeName */
  schemeName: string
  /** 对应后端字段 kanbanType */
  kanbanType: string
  /** 对应后端字段 schemeDescription */
  schemeDescription: string
  /** 对应后端字段 dataSourceConfig */
  dataSourceConfig: string
  /** 对应后端字段 layoutConfig */
  layoutConfig: string
  /** 对应后端字段 componentConfig */
  componentConfig: string
  /** 对应后端字段 refreshStrategy */
  refreshStrategy: string
  /** 对应后端字段 refreshInterval */
  refreshInterval: number
  /** 对应后端字段 themeStyle */
  themeStyle: string
  /** 对应后端字段 isFullscreen */
  isFullscreen: number
  /** 对应后端字段 enableAlert */
  enableAlert: number
  /** 对应后端字段 alertConfig */
  alertConfig: string
  /** 对应后端字段 filterConfig */
  filterConfig: string
  /** 对应后端字段 sortConfig */
  sortConfig: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 workshopCode */
  workshopCode: string
  /** 对应后端字段 lineCode */
  lineCode: string
  /** 对应后端字段 displayOrder */
  displayOrder: number
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 isPublic */
  isPublic: number
  /** 对应后端字段 creatorIds */
  creatorIds: string
  /** 对应后端字段 accessConfig */
  accessConfig: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * KanbanSchemeUpdate类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanSchemeUpdateDto）
 */
export interface KanbanSchemeUpdate extends KanbanSchemeCreate {
  /** 对应后端字段 kanbanSchemeId */
  kanbanSchemeId: string
}

/**
 * KanbanSchemeStatus类型（对应后端 Takt.Application.Dtos.Statistics.Kanban.TaktKanbanSchemeStatusDto）
 */
export interface KanbanSchemeStatus {
  /** 对应后端字段 kanbanSchemeId */
  kanbanSchemeId: string
  /** 对应后端字段 status */
  status: number
}
