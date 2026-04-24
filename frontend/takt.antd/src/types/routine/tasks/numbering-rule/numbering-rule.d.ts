// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/numbering-rule/numbering-rule
// 文件名称：numbering-rule.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：numbering-rule相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * NumberingRule类型（对应后端 Takt.Application.Dtos.Routine.Tasks.NumberingRule.TaktNumberingRuleDto）
 */
export interface NumberingRule extends TaktEntityBase {
  /** 对应后端字段 numberingRuleId */
  numberingRuleId: string
  /** 对应后端字段 ruleCode */
  ruleCode: string
  /** 对应后端字段 ruleName */
  ruleName: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 deptCode */
  deptCode?: string
  /** 对应后端字段 prefix */
  prefix?: string
  /** 对应后端字段 dateFormat */
  dateFormat?: string
  /** 对应后端字段 numberLength */
  numberLength: number
  /** 对应后端字段 suffix */
  suffix?: string
  /** 对应后端字段 currentNumber */
  currentNumber: number
  /** 对应后端字段 step */
  step: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 ruleStatus */
  ruleStatus: number
}

/**
 * NumberingRuleQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.NumberingRule.TaktNumberingRuleQueryDto）
 */
export interface NumberingRuleQuery extends TaktPagedQuery {
  /** 对应后端字段 ruleCode */
  ruleCode?: string
  /** 对应后端字段 ruleName */
  ruleName?: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 deptCode */
  deptCode?: string
  /** 对应后端字段 ruleStatus */
  ruleStatus?: number
}

/**
 * NumberingRuleCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.NumberingRule.TaktNumberingRuleCreateDto）
 */
export interface NumberingRuleCreate {
  /** 对应后端字段 ruleCode */
  ruleCode: string
  /** 对应后端字段 ruleName */
  ruleName: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 deptCode */
  deptCode?: string
  /** 对应后端字段 prefix */
  prefix?: string
  /** 对应后端字段 dateFormat */
  dateFormat?: string
  /** 对应后端字段 numberLength */
  numberLength: number
  /** 对应后端字段 suffix */
  suffix?: string
  /** 对应后端字段 step */
  step: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * NumberingRuleUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.NumberingRule.TaktNumberingRuleUpdateDto）
 */
export interface NumberingRuleUpdate extends NumberingRuleCreate {
  /** 对应后端字段 numberingRuleId */
  numberingRuleId: string
}

/**
 * NumberingRuleStatus类型（对应后端 Takt.Application.Dtos.Routine.Tasks.NumberingRule.TaktNumberingRuleStatusDto）
 */
export interface NumberingRuleStatus {
  /** 对应后端字段 numberingRuleId */
  numberingRuleId: string
  /** 对应后端字段 ruleStatus */
  ruleStatus: number
}
