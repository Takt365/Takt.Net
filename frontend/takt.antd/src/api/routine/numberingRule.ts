// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/numberingRule
// 文件名称：numberingRule.ts
// 功能描述：单据编码规则 API，对应后端 TaktNumberingRulesController
// ========================================

import request from '../request'
import type { TaktPagedResult } from '@/types/common'
import type {
  NumberingRule,
  NumberingRuleQuery,
  NumberingRuleCreate,
  NumberingRuleUpdate,
  NumberingRuleStatus
} from '@/types/routine/numberingRule'
import type { TaktSelectOption } from '@/types/common'

const BASE = '/api/TaktNumberingRules'

/** 获取编码规则列表（分页） */
export function getNumberingRuleList(params: NumberingRuleQuery): Promise<TaktPagedResult<NumberingRule>> {
  return request({
    url: `${BASE}/list`,
    method: 'get',
    params
  })
}

/** 根据ID获取编码规则 */
export function getNumberingRuleById(id: string): Promise<NumberingRule> {
  return request({
    url: `${BASE}/${id}`,
    method: 'get'
  })
}

/** 获取编码规则选项列表（用于下拉框等） */
export function getNumberingRuleOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${BASE}/options`,
    method: 'get'
  })
}

/** 创建编码规则 */
export function createNumberingRule(data: NumberingRuleCreate): Promise<NumberingRule> {
  return request({
    url: BASE,
    method: 'post',
    data
  })
}

/** 更新编码规则 */
export function updateNumberingRule(id: string, data: NumberingRuleUpdate): Promise<NumberingRule> {
  return request({
    url: `${BASE}/${id}`,
    method: 'put',
    data
  })
}

/** 删除编码规则 */
export function deleteNumberingRule(id: string): Promise<void> {
  return request({
    url: `${BASE}/${id}`,
    method: 'delete'
  })
}

/** 批量删除编码规则 */
export function deleteNumberingRuleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${BASE}/batch`,
    method: 'delete',
    data: ids.map(id => Number(id))
  })
}

/** 更新编码规则状态 */
export function updateNumberingRuleStatus(data: NumberingRuleStatus): Promise<NumberingRule> {
  return request({
    url: `${BASE}/status`,
    method: 'put',
    data
  })
}
