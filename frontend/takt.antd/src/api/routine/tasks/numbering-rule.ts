// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/numbering-rule
// 文件名称：numbering-rule.ts
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：编码规则相关 API，对应后端 TaktNumberingRulesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  NumberingRule,
  NumberingRuleQuery,
  NumberingRuleCreate,
  NumberingRuleUpdate,
  NumberingRuleStatus
} from '@/types/routine/tasks/numbering-rule'

// ========================================
// 编码规则相关 API（按后端控制器顺序）
// ========================================

const ruleUrl = '/api/TaktNumberingRules'

/**
 * 获取编码规则列表（分页）
 * 对应后端：GetListAsync
 */
export function getNumberingRuleList(params: NumberingRuleQuery): Promise<TaktPagedResult<NumberingRule>> {
  return request({
    url: `${ruleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取编码规则
 * 对应后端：GetByIdAsync
 */
export function getNumberingRuleById(id: string): Promise<NumberingRule> {
  return request({
    url: `${ruleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 创建编码规则
 * 对应后端：CreateAsync
 */
export function createNumberingRule(data: NumberingRuleCreate): Promise<NumberingRule> {
  return request({
    url: ruleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新编码规则
 * 对应后端：UpdateAsync
 */
export function updateNumberingRule(id: string, data: NumberingRuleUpdate): Promise<NumberingRule> {
  return request({
    url: `${ruleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除编码规则
 * 对应后端：DeleteAsync
 */
export function deleteNumberingRule(id: string): Promise<void> {
  return request({
    url: `${ruleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除编码规则
 * 对应后端：DeleteBatchAsync
 */
export function deleteNumberingRuleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ruleUrl}/batch`,
    method: 'delete',
    data: ids.map(id => Number(id))
  })
}

/**
 * 更新编码规则状态
 * 对应后端：UpdateStatusAsync
 */
export function updateNumberingRuleStatus(data: NumberingRuleStatus): Promise<NumberingRule> {
  return request({
    url: `${ruleUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 导出编码规则
 * 对应后端：ExportAsync；fileName 仅传名称不含后缀
 */
export function exportNumberingRules(
  query: NumberingRuleQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${ruleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
