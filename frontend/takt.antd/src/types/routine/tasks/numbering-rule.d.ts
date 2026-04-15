// 命名空间：@/types/routine/tasks/numbering-rule

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

export interface NumberingRule extends TaktEntityBase {
  numberingRuleId: string
  ruleCode: string
  ruleName: string
  companyCode?: string
  deptCode?: string
  prefix?: string
  dateFormat?: string
  numberLength: number
  suffix?: string
  currentNumber: number
  step: number
  orderNum: number
  ruleStatus: number
}

export interface NumberingRuleQuery extends TaktPagedQuery {
  keyWords?: string
  ruleCode?: string
  ruleName?: string
  companyCode?: string
  deptCode?: string
  ruleStatus?: number
}

export interface NumberingRuleCreate {
  ruleCode: string
  ruleName: string
  companyCode?: string
  deptCode?: string
  prefix?: string
  dateFormat?: string
  numberLength?: number
  suffix?: string
  step?: number
  orderNum?: number
  remark?: string
}

export interface NumberingRuleUpdate extends NumberingRuleCreate {
  numberingRuleId: string
}

export interface NumberingRuleStatus {
  numberingRuleId: string
  ruleStatus: number
}
