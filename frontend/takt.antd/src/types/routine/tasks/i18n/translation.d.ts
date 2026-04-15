// 命名空间：@/types/routine/tasks/i18n/translation

import type { TaktEntityBase, TaktPagedQuery, TaktPagedResult } from '@/types/common'

export interface Translation extends TaktEntityBase {
  translationId: string
  resourceKey: string
  languageId: string
  cultureCode: string
  translationValue: string
  resourceType: string
  resourceGroup?: string
  orderNum: number
}

export interface TranslationQuery extends TaktPagedQuery {
  keyWords?: string
  languageId?: string
  resourceKey?: string
  cultureCode?: string
  resourceType?: string
  resourceGroup?: string
}

export interface TranslationCreate {
  resourceKey: string
  languageId: string
  cultureCode: string
  translationValue: string
  resourceType: string
  resourceGroup?: string
  orderNum: number
  remark?: string
}

export interface TranslationUpdate extends TranslationCreate {
  translationId: string
}

export interface TranslationTransposed {
  resourceKey: string
  resourceType: string
  resourceGroup?: string
  orderNum: number
  translations: Record<string, string>
}

export interface TranslationTransposedResult {
  paged: TaktPagedResult<TranslationTransposed>
  cultureCodeOrder: string[]
}
