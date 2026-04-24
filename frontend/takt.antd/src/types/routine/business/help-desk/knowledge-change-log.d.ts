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
  /** 对应后端字段 changeLogId */
  changeLogId: string
  /** 对应后端字段 knowledgeId */
  knowledgeId: string
  /** 对应后端字段 knowledgeTitle */
  knowledgeTitle?: string
  /** 对应后端字段 changeType */
  changeType: number
  /** 对应后端字段 changeSummary */
  changeSummary?: string
  /** 对应后端字段 changeField */
  changeField?: string
  /** 对应后端字段 oldValue */
  oldValue?: string
  /** 对应后端字段 newValue */
  newValue?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 versionAtChange */
  versionAtChange?: number
}

/**
 * KnowledgeChangeLogQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktKnowledgeChangeLogQueryDto）
 */
export interface KnowledgeChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 knowledgeId */
  knowledgeId?: string
  /** 对应后端字段 changeType */
  changeType?: number
  /** 对应后端字段 createdById */
  createdById?: string
  /** 对应后端字段 createdAtFrom */
  createdAtFrom?: string
  /** 对应后端字段 createdAtTo */
  createdAtTo?: string
}
