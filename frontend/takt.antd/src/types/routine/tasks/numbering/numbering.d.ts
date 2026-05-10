// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/numbering/numbering
// 文件名称：numbering.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：numbering相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Numbering类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Numbering.TaktNumberingDto）
 */
export interface Numbering extends TaktEntityBase {
  /** 对应后端字段 numberingId */
  numberingId: string
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
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 ruleStatus */
  ruleStatus: number
}

/**
 * NumberingQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Numbering.TaktNumberingQueryDto）
 */
export interface NumberingQuery extends TaktPagedQuery {
  /** 对应后端字段 ruleCode */
  ruleCode?: string
  /** 对应后端字段 ruleName */
  ruleName?: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 deptCode */
  deptCode?: string
  /** 对应后端字段 prefix */
  prefix?: string
  /** 对应后端字段 dateFormat */
  dateFormat?: string
  /** 对应后端字段 numberLength */
  numberLength?: number
  /** 对应后端字段 suffix */
  suffix?: string
  /** 对应后端字段 currentNumber */
  currentNumber?: number
  /** 对应后端字段 step */
  step?: number
  /** 对应后端字段 ruleStatus */
  ruleStatus?: number
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 createdById */
  createdById?: string
  /** 对应后端字段 createdBy */
  createdBy?: string
  /** 对应后端字段 createdAt */
  createdAt?: string
  /** 对应后端字段 createdAtStart */
  createdAtStart?: string
  /** 对应后端字段 createdAtEnd */
  createdAtEnd?: string
}

/**
 * NumberingCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Numbering.TaktNumberingCreateDto）
 */
export interface NumberingCreate {
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
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 ruleStatus */
  ruleStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * NumberingUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Numbering.TaktNumberingUpdateDto）
 */
export interface NumberingUpdate extends NumberingCreate {
  /** 对应后端字段 numberingId */
  numberingId: string
}

/**
 * NumberingSort类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Numbering.TaktNumberingSortDto）
 */
export interface NumberingSort {
  /** 对应后端字段 numberingId */
  numberingId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * NumberingRuleStatus类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Numbering.TaktNumberingRuleStatusDto）
 */
export interface NumberingRuleStatus {
  /** 对应后端字段 numberingId */
  numberingId: string
  /** 对应后端字段 ruleStatus */
  ruleStatus: number
}
