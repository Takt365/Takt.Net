// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/routine/numberingRule
// 文件名称：numberingRule.d.ts
// 功能描述：单据编码规则类型定义，对应后端 Takt.Application.Dtos.Routine.NumberingRules
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 单据编码规则（对应后端 TaktNumberingRuleDto）
 */
export interface NumberingRule extends TaktEntityBase {
  /** 规则ID（对应后端 RuleId，序列化为 string） */
  ruleId: string
  /** 规则编码（唯一） */
  ruleCode: string
  /** 规则名称 */
  ruleName: string
  /** 单据类型 */
  documentType: string
  /** 公司代码（为空表示不限定公司） */
  companyCode?: string
  /** 部门编码（为空表示不限定部门） */
  deptCode?: string
  /** 编号前缀 */
  prefix?: string
  /** 日期格式（如 yyyyMMdd） */
  dateFormat?: string
  /** 流水号位数 */
  serialLength: number
  /** 编号后缀 */
  suffix?: string
  /** 当前流水号值 */
  currentValue: number
  /** 重置周期（0=不重置，1=按日，2=按月，3=按年） */
  resetCycle: number
  /** 排序号 */
  orderNum: number
  /** 规则状态（0=启用，1=禁用） */
  ruleStatus: number
}

/**
 * 编码规则查询（对应后端 TaktNumberingRuleQueryDto）
 */
export interface NumberingRuleQuery extends TaktPagedQuery {
  keyWords?: string
  ruleCode?: string
  ruleName?: string
  documentType?: string
  companyCode?: string
  deptCode?: string
  ruleStatus?: number
}

/**
 * 创建编码规则（对应后端 TaktNumberingRuleCreateDto）
 */
export interface NumberingRuleCreate {
  ruleCode: string
  ruleName: string
  documentType: string
  companyCode?: string
  deptCode?: string
  prefix?: string
  dateFormat?: string
  serialLength?: number
  suffix?: string
  resetCycle?: number
  orderNum?: number
  ruleStatus?: number
  remark?: string
}

/**
 * 更新编码规则（对应后端 TaktNumberingRuleUpdateDto）
 */
export interface NumberingRuleUpdate extends NumberingRuleCreate {
  ruleId: string
}

/**
 * 编码规则状态（对应后端 TaktNumberingRuleStatusDto）
 */
export interface NumberingRuleStatus {
  ruleId: string
  ruleStatus: number
}
