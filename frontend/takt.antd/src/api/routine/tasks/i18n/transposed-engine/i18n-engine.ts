// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/i18n/transposed-engine
// 文件名称：i18n-engine.ts
// 功能描述：I18nEngine API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.I18n.TransposedEngine.TaktI18nEngineController
// ========================================

import request from '@/api/request'
import type { TranslationQuery } from '@/types/routine/tasks/i18n/translation'
import type {
  TranslationTransposed,
  TranslationTransposedResult
} from '@/types/routine/tasks/i18n/specific-engine/i18n'
import type { TaktSelectOption } from '@/types/common'

// ========================================
// I18nEngine转置引擎相关 API
// ========================================
const i18nEngineUrl = '/api/TaktTransposeds'

/**
 * 获取转置后的翻译列表（按资源键分组，各语言为列）
 * 对应后端：GetTransposedTranslationsAsync
 */
export function getTransposedTranslations(data: TranslationQuery): Promise<TranslationTransposedResult> {
  return request({
    url: `${i18nEngineUrl}/transposed-list`,
    method: 'post',
    data
  })
}

/**
 * 批量更新翻译（转置模式）
 * 对应后端：BatchUpdateTranslationsAsync
 */
export function batchUpdateTranslations(translations: TranslationTransposed[]): Promise<number> {
  return request({
    url: `${i18nEngineUrl}/transposed-batch-update`,
    method: 'post',
    data: translations
  })
}

/**
 * 获取翻译选项列表（用于下拉框等）
 * 对应后端：GetI18nOptionsAsync
 */
export function getI18nOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${i18nEngineUrl}/i18n-options`,
    method: 'get'
  })
}
