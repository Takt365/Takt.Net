// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/help-desk/knowledge-change-log
// 文件名称：knowledge-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：knowledge-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * KnowledgeChangeLog类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktKnowledgeChangeLogDto）
 */
export interface KnowledgeChangeLog extends TaktEntityBase {
  /** 对应后端字段 knowledgeChangeLogId */
  knowledgeChangeLogId: string
  /** 对应后端字段 knowledgeId */
  knowledgeId: string
  /** 对应后端字段 knowledgeTitle */
  knowledgeTitle?: string
  /** 对应后端字段 changeType */
  changeType: number
  /** 对应后端字段 changeSummary */
  changeSummary?: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 versionAtChange */
  versionAtChange?: number
  /** 对应后端字段 knowledge */
  knowledge?: unknown
}

/**
 * KnowledgeChangeLogQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktKnowledgeChangeLogQueryDto）
 */
export interface KnowledgeChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 knowledgeId */
  knowledgeId?: string
  /** 对应后端字段 knowledgeTitle */
  knowledgeTitle?: string
  /** 对应后端字段 changeType */
  changeType?: number
  /** 对应后端字段 changeSummary */
  changeSummary?: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 versionAtChange */
  versionAtChange?: number
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
 * KnowledgeChangeLogCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktKnowledgeChangeLogCreateDto）
 */
export interface KnowledgeChangeLogCreate {
  /** 对应后端字段 knowledgeId */
  knowledgeId: string
  /** 对应后端字段 knowledgeTitle */
  knowledgeTitle?: string
  /** 对应后端字段 changeType */
  changeType: number
  /** 对应后端字段 changeSummary */
  changeSummary?: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 versionAtChange */
  versionAtChange?: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * KnowledgeChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktKnowledgeChangeLogUpdateDto）
 */
export interface KnowledgeChangeLogUpdate extends KnowledgeChangeLogCreate {
  /** 对应后端字段 knowledgeChangeLogId */
  knowledgeChangeLogId: string
}
