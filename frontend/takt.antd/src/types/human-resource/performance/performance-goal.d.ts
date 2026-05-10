// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/performance/performance-goal
// 文件名称：performance-goal.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：performance-goal相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PerformanceGoal类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceGoalDto）
 */
export interface PerformanceGoal extends TaktEntityBase {
  /** 对应后端字段 performanceGoalId */
  performanceGoalId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 performanceIndicatorId */
  performanceIndicatorId: string
  /** 对应后端字段 goalPeriod */
  goalPeriod: string
  /** 对应后端字段 goalDescription */
  goalDescription: string
  /** 对应后端字段 targetValue */
  targetValue: number
  /** 对应后端字段 actualValue */
  actualValue: number
  /** 对应后端字段 completionPercentage */
  completionPercentage: number
  /** 对应后端字段 goalWeight */
  goalWeight: number
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 dueDate */
  dueDate: string
  /** 对应后端字段 completionDate */
  completionDate: string
  /** 对应后端字段 achievementNotes */
  achievementNotes: string
  /** 对应后端字段 failureReason */
  failureReason: string
  /** 对应后端字段 approverId */
  approverId: string
  /** 对应后端字段 status */
  status: number
}

/**
 * PerformanceGoalQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceGoalQueryDto）
 */
export interface PerformanceGoalQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 performanceIndicatorId */
  performanceIndicatorId?: string
  /** 对应后端字段 goalPeriod */
  goalPeriod?: string
  /** 对应后端字段 goalDescription */
  goalDescription?: string
  /** 对应后端字段 targetValue */
  targetValue?: number
  /** 对应后端字段 actualValue */
  actualValue?: number
  /** 对应后端字段 completionPercentage */
  completionPercentage?: number
  /** 对应后端字段 goalWeight */
  goalWeight?: number
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 startDateStart */
  startDateStart?: string
  /** 对应后端字段 startDateEnd */
  startDateEnd?: string
  /** 对应后端字段 dueDate */
  dueDate?: string
  /** 对应后端字段 dueDateStart */
  dueDateStart?: string
  /** 对应后端字段 dueDateEnd */
  dueDateEnd?: string
  /** 对应后端字段 completionDate */
  completionDate?: string
  /** 对应后端字段 completionDateStart */
  completionDateStart?: string
  /** 对应后端字段 completionDateEnd */
  completionDateEnd?: string
  /** 对应后端字段 achievementNotes */
  achievementNotes?: string
  /** 对应后端字段 failureReason */
  failureReason?: string
  /** 对应后端字段 approverId */
  approverId?: string
  /** 对应后端字段 status */
  status?: number
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
 * PerformanceGoalCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceGoalCreateDto）
 */
export interface PerformanceGoalCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 performanceIndicatorId */
  performanceIndicatorId: string
  /** 对应后端字段 goalPeriod */
  goalPeriod: string
  /** 对应后端字段 goalDescription */
  goalDescription: string
  /** 对应后端字段 targetValue */
  targetValue: number
  /** 对应后端字段 actualValue */
  actualValue: number
  /** 对应后端字段 completionPercentage */
  completionPercentage: number
  /** 对应后端字段 goalWeight */
  goalWeight: number
  /** 对应后端字段 startDate */
  startDate: string
  /** 对应后端字段 dueDate */
  dueDate: string
  /** 对应后端字段 completionDate */
  completionDate: string
  /** 对应后端字段 achievementNotes */
  achievementNotes: string
  /** 对应后端字段 failureReason */
  failureReason: string
  /** 对应后端字段 approverId */
  approverId: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PerformanceGoalUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceGoalUpdateDto）
 */
export interface PerformanceGoalUpdate extends PerformanceGoalCreate {
  /** 对应后端字段 performanceGoalId */
  performanceGoalId: string
}

/**
 * PerformanceGoalStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceGoalStatusDto）
 */
export interface PerformanceGoalStatus {
  /** 对应后端字段 performanceGoalId */
  performanceGoalId: string
  /** 对应后端字段 status */
  status: number
}
