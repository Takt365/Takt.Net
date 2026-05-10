// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/vocabulary/vocabulary
// 文件名称：vocabulary.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：vocabulary相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Vocabulary类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Vocabulary.TaktVocabularyDto）
 */
export interface Vocabulary extends TaktEntityBase {
  /** 对应后端字段 vocabularyId */
  vocabularyId: string
  /** 对应后端字段 wordText */
  wordText: string
  /** 对应后端字段 wordCategory */
  wordCategory: number
  /** 对应后端字段 filterLevel */
  filterLevel: number
  /** 对应后端字段 replaceText */
  replaceText?: string
  /** 对应后端字段 status */
  status: number
}

/**
 * VocabularyQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Vocabulary.TaktVocabularyQueryDto）
 */
export interface VocabularyQuery extends TaktPagedQuery {
  /** 对应后端字段 wordText */
  wordText?: string
  /** 对应后端字段 wordCategory */
  wordCategory?: number
  /** 对应后端字段 filterLevel */
  filterLevel?: number
  /** 对应后端字段 replaceText */
  replaceText?: string
  /** 对应后端字段 status */
  status?: number
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
 * VocabularyCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Vocabulary.TaktVocabularyCreateDto）
 */
export interface VocabularyCreate {
  /** 对应后端字段 wordText */
  wordText: string
  /** 对应后端字段 wordCategory */
  wordCategory: number
  /** 对应后端字段 filterLevel */
  filterLevel: number
  /** 对应后端字段 replaceText */
  replaceText?: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * VocabularyUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Vocabulary.TaktVocabularyUpdateDto）
 */
export interface VocabularyUpdate extends VocabularyCreate {
  /** 对应后端字段 vocabularyId */
  vocabularyId: string
}

/**
 * VocabularyStatus类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Vocabulary.TaktVocabularyStatusDto）
 */
export interface VocabularyStatus {
  /** 对应后端字段 vocabularyId */
  vocabularyId: string
  /** 对应后端字段 status */
  status: number
}
