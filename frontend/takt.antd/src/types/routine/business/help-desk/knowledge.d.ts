// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/help-desk/knowledge
// 文件名称：knowledge.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：knowledge相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Knowledge类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktKnowledgeDto）
 */
export interface Knowledge extends TaktEntityBase {
  /** 对应后端字段 knowledgeId */
  knowledgeId: string
  /** 对应后端字段 title */
  title: string
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 summary */
  summary?: string
  /** 对应后端字段 categoryCode */
  categoryCode?: string
  /** 对应后端字段 tags */
  tags?: string
  /** 对应后端字段 knowledgeStatus */
  knowledgeStatus: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 viewCount */
  viewCount: number
  /** 对应后端字段 helpfulCount */
  helpfulCount: number
  /** 对应后端字段 unhelpfulCount */
  unhelpfulCount: number
}

/**
 * KnowledgeQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktKnowledgeQueryDto）
 */
export interface KnowledgeQuery extends TaktPagedQuery {
  /** 对应后端字段 title */
  title?: string
  /** 对应后端字段 categoryCode */
  categoryCode?: string
  /** 对应后端字段 knowledgeStatus */
  knowledgeStatus?: number
  /** 对应后端字段 tags */
  tags?: string
}

/**
 * KnowledgeCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktKnowledgeCreateDto）
 */
export interface KnowledgeCreate {
  /** 对应后端字段 title */
  title: string
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 summary */
  summary?: string
  /** 对应后端字段 categoryCode */
  categoryCode?: string
  /** 对应后端字段 tags */
  tags?: string
  /** 对应后端字段 knowledgeStatus */
  knowledgeStatus: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * KnowledgeUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktKnowledgeUpdateDto）
 */
export interface KnowledgeUpdate extends KnowledgeCreate {
  /** 对应后端字段 knowledgeId */
  knowledgeId: string
}
