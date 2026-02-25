/**
 * 节拍数字工厂 · Takt Digital Factory (TDF)
 * @file module.ts
 * @description 流程设计器 bpmn-js 扩展模块：多语言 translate、网格 grid、默认文字标签。仅标准 BPMN，不引入 Flowable/Activiti moddle（后端为 Takt）。
 * DefaultLabelBehavior 使用动态 import 隔离加载，避免与 diagram-js-grid 等同 chunk 时触发 require is not defined。
 * Copyright (c) 2025 Takt  All rights reserved.
 */

import { getBpmnJsTranslate } from '@/locales/workflow/bpmn-js'
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-ignore - diagram-js-grid 无类型声明
import gridModule from 'diagram-js-grid'
import { ELEMENT_TYPE_I18N_KEYS } from './config'

/**
 * 创建 bpmn-js 使用的 translate 模块（调色板、右键菜单等文案）
 * @param locale 语言代码，如 zh-CN、en-US
 * @returns bpmn-js additionalModules 项：{ translate: ['value', fn] }
 */
export function createTranslateModule(locale: string): { translate: ['value', (template: string, replacements?: Record<string, string>) => string] } {
  const fn = getBpmnJsTranslate(locale)
  const translateFn = (template: string, replacements?: Record<string, string>) => {
    let s = fn(template)
    if (replacements) s = s.replace(/{([^}]+)}/g, (_, k) => replacements[k] ?? `{${k}}`)
    return s
  }
  return { translate: ['value', translateFn] }
}

/**
 * 创建默认文字标签提供模块：根据 element.type 返回 i18n 默认名称，供 DefaultLabelBehavior 使用
 * @param t vue-i18n 的 t 或等价函数
 */
/**
 * 将 bpmn:UserTask 转为可读默认名（如「用户任务」），用于无 i18n key 时的兜底
 */
function fallbackLabelFromType(type: string): string {
  if (!type || typeof type !== 'string') return ''
  const withoutNs = type.replace(/^bpmn:/i, '').trim()
  if (!withoutNs) return type
  return withoutNs.replace(/([A-Z])/g, (_, c) => ` ${c}`).replace(/^\s+/, '')
}

export function createDefaultLabelModule(t: (key: string) => string): { getDefaultLabel: ['value', (type: string) => string] } {
  return {
    getDefaultLabel: [
      'value',
      (type: string) => {
        const key = ELEMENT_TYPE_I18N_KEYS[type]
        const text = key ? t(key) : fallbackLabelFromType(type)
        return text && String(text).trim() ? text : fallbackLabelFromType(type) || type
      }
    ]
  }
}

/**
 * 获取设计器 Modeler 所需的 additionalModules 列表（translate + grid + 默认标签）。
 * DefaultLabelBehavior 动态导入，避免与 diagram-js-grid 打在同一 chunk 时在 ESM 下触发 require is not defined。
 * @param locale 当前语言代码
 * @param t 用于默认标签的 i18n 函数，如 useI18n().t
 */
export async function getAdditionalModules(locale: string, t: (key: string) => string): Promise<unknown[]> {
  const { default: DefaultLabelBehavior } = await import('./default-label-behavior')
  return [createTranslateModule(locale), createDefaultLabelModule(t), gridModule, DefaultLabelBehavior]
}
