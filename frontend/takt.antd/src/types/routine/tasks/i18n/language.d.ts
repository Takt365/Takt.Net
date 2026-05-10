// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/i18n/language
// 文件名称：language.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：language相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Language类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.TaktLanguageDto）
 */
export interface Language extends TaktEntityBase {
  /** 对应后端字段 languageId */
  languageId: string
  /** 对应后端字段 languageName */
  languageName: string
  /** 对应后端字段 cultureCode */
  cultureCode: string
  /** 对应后端字段 nativeName */
  nativeName: string
  /** 对应后端字段 languageIcon */
  languageIcon?: string
  /** 对应后端字段 isDefault */
  isDefault: number
  /** 对应后端字段 isRtl */
  isRtl: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 languageStatus */
  languageStatus: number
  /** 对应后端字段 translations */
  translations?: unknown[]
}

/**
 * LanguageQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.TaktLanguageQueryDto）
 */
export interface LanguageQuery extends TaktPagedQuery {
  /** 对应后端字段 languageName */
  languageName?: string
  /** 对应后端字段 cultureCode */
  cultureCode?: string
  /** 对应后端字段 nativeName */
  nativeName?: string
  /** 对应后端字段 languageIcon */
  languageIcon?: string
  /** 对应后端字段 isDefault */
  isDefault?: number
  /** 对应后端字段 isRtl */
  isRtl?: number
  /** 对应后端字段 languageStatus */
  languageStatus?: number
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
 * LanguageCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.TaktLanguageCreateDto）
 */
export interface LanguageCreate {
  /** 对应后端字段 languageName */
  languageName: string
  /** 对应后端字段 cultureCode */
  cultureCode: string
  /** 对应后端字段 nativeName */
  nativeName: string
  /** 对应后端字段 languageIcon */
  languageIcon?: string
  /** 对应后端字段 isDefault */
  isDefault: number
  /** 对应后端字段 isRtl */
  isRtl: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 languageStatus */
  languageStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 translations */
  translations?: unknown[]
}

/**
 * LanguageUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.TaktLanguageUpdateDto）
 */
export interface LanguageUpdate extends LanguageCreate {
  /** 对应后端字段 languageId */
  languageId: string
}

/**
 * LanguageStatus类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.TaktLanguageStatusDto）
 */
export interface LanguageStatus {
  /** 对应后端字段 languageId */
  languageId: string
  /** 对应后端字段 languageStatus */
  languageStatus: number
}

/**
 * LanguageSort类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.TaktLanguageSortDto）
 */
export interface LanguageSort {
  /** 对应后端字段 languageId */
  languageId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
