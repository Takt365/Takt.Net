// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/i18n/specific-engine/i18n
// 文件名称：i18n.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：i18n相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Language类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.SpecificEngine.TaktLanguageDto）
 */
export interface Language {
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
 * LanguageCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.SpecificEngine.TaktLanguageCreateDto）
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
 * LanguageUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.SpecificEngine.TaktLanguageUpdateDto）
 */
export interface LanguageUpdate extends LanguageCreate {
  /** 对应后端字段 languageId */
  languageId: string
}

/**
 * TranslationTransposed类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.SpecificEngine.TaktTranslationTransposedDto）
 */
export interface TranslationTransposed {
  /** 对应后端字段 resourceKey */
  resourceKey: string
  /** 对应后端字段 resourceType */
  resourceType: string
  /** 对应后端字段 resourceGroup */
  resourceGroup?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * TranslationTransposedResult类型（对应后端 Takt.Application.Dtos.Routine.Tasks.I18n.SpecificEngine.TaktTranslationTransposedResultDto）
 */
export interface TranslationTransposedResult {
  /** 对应后端字段 paged */
  paged: unknown
  /** 对应后端字段 cultureCodeOrder */
  cultureCodeOrder: unknown
}
