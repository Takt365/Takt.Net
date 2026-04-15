// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/routine/i18n/language
// 文件名称：language.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：语言类型定义，对应后端 Takt.Application.Dtos.Routine.i18n.TaktLanguageDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery, TaktPagedResult, TaktSelectOption } from '@/types/common'
import type { Translation } from './translation'

/**
 * 语言（对应后端 TaktLanguageDto）
 */
export interface Language extends TaktEntityBase {
  /** 语言ID */
  languageId: string
  /** 语言名称（中文名称，如：简体中文） */
  languageName: string
  /** 语言编码（ISO 639-1/639-2，如：zh-CN、en-US） */
  cultureCode: string
  /** 本地化名称（该语言下的名称，如：简体中文、English） */
  nativeName: string
  /** 语言图标（国旗图标或语言图标URL） */
  languageIcon?: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 语言状态（0=启用，1=禁用） */
  languageStatus: number
  /** 是否默认语言（1=是，0=否） */
  isDefault: number
  /** 是否启用RTL（从右到左，1=是，0=否） */
  isRtl: number
  /** 翻译列表（主子表关系，对应后端 TranslationList） */
  translationList?: Translation[]
}

/**
 * 语言查询（对应后端 TaktLanguageQueryDto）
 */
export interface LanguageQuery extends TaktPagedQuery {
  /** 关键词（在语言名称、语言编码、本地化名称中模糊查询） */
  keyWords?: string
  /** 语言编码 */
  cultureCode?: string
  /** 语言状态（0=启用，1=禁用） */
  languageStatus?: number
  /** 是否默认语言（1=是，0=否） */
  isDefault?: number
}

/**
 * 语言创建（对应后端 TaktLanguageCreateDto）
 */
export interface LanguageCreate {
  /** 语言名称（中文名称，如：简体中文） */
  languageName: string
  /** 语言编码（ISO 639-1/639-2，如：zh-CN、en-US） */
  cultureCode: string
  /** 本地化名称（该语言下的名称，如：简体中文、English） */
  nativeName: string
  /** 语言图标（国旗图标或语言图标URL） */
  languageIcon?: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 语言状态（0=启用，1=禁用） */
  languageStatus: number
  /** 是否默认语言（1=是，0=否） */
  isDefault: number
  /** 是否启用RTL（从右到左，1=是，0=否） */
  isRtl: number
  /** 备注 */
  remark?: string
  /** 翻译列表（主子表关系，对应后端 TranslationList） */
  translationList?: import('./translation').TranslationCreate[]
}

/**
 * 语言更新（对应后端 TaktLanguageUpdateDto）
 */
export interface LanguageUpdate extends LanguageCreate {
  /** 语言ID */
  languageId: string
}

/**
 * 语言状态（对应后端 TaktLanguageStatusDto）
 */
export interface LanguageStatus {
  /** 语言ID */
  languageId: string
  /** 语言状态（0=禁用，1=启用） */
  languageStatus: number
}
