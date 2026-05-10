// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/i18n/translation
// 文件名称：translation.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：translation相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Translation类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.TaktTranslationDto）
 */
export interface Translation extends TaktEntityBase {
  /** 对应后端字段 translationId */
  translationId: string
  /** 对应后端字段 languageId */
  languageId: string
  /** 对应后端字段 cultureCode */
  cultureCode: string
  /** 对应后端字段 resourceKey */
  resourceKey: string
  /** 对应后端字段 translationValue */
  translationValue: string
  /** 对应后端字段 resourceType */
  resourceType: string
  /** 对应后端字段 resourceGroup */
  resourceGroup?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 language */
  language?: unknown
}

/**
 * TranslationQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.TaktTranslationQueryDto）
 */
export interface TranslationQuery extends TaktPagedQuery {
  /** 对应后端字段 languageId */
  languageId?: string
  /** 对应后端字段 cultureCode */
  cultureCode?: string
  /** 对应后端字段 resourceKey */
  resourceKey?: string
  /** 对应后端字段 translationValue */
  translationValue?: string
  /** 对应后端字段 resourceType */
  resourceType?: string
  /** 对应后端字段 resourceGroup */
  resourceGroup?: string
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
 * TranslationCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.TaktTranslationCreateDto）
 */
export interface TranslationCreate {
  /** 对应后端字段 languageId */
  languageId: string
  /** 对应后端字段 cultureCode */
  cultureCode: string
  /** 对应后端字段 resourceKey */
  resourceKey: string
  /** 对应后端字段 translationValue */
  translationValue: string
  /** 对应后端字段 resourceType */
  resourceType: string
  /** 对应后端字段 resourceGroup */
  resourceGroup?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * TranslationUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.TaktTranslationUpdateDto）
 */
export interface TranslationUpdate extends TranslationCreate {
  /** 对应后端字段 translationId */
  translationId: string
}

/**
 * TranslationSort类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.TaktTranslationSortDto）
 */
export interface TranslationSort {
  /** 对应后端字段 translationId */
  translationId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
