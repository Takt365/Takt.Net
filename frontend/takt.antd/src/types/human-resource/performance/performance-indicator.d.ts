// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/performance/performance-indicator
// 文件名称：performance-indicator.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：performance-indicator相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PerformanceIndicator类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceIndicatorDto）
 */
export interface PerformanceIndicator extends TaktEntityBase {
  /** 对应后端字段 performanceIndicatorId */
  performanceIndicatorId: string
  /** 对应后端字段 indicatorCode */
  indicatorCode: string
  /** 对应后端字段 indicatorName */
  indicatorName: string
  /** 对应后端字段 category */
  category: string
  /** 对应后端字段 indicatorType */
  indicatorType: string
  /** 对应后端字段 indicatorDescription */
  indicatorDescription: string
  /** 对应后端字段 scoringCriteria */
  scoringCriteria: string
  /** 对应后端字段 targetValue */
  targetValue: number
  /** 对应后端字段 minimumValue */
  minimumValue: number
  /** 对应后端字段 excellentValue */
  excellentValue: number
  /** 对应后端字段 standardWeight */
  standardWeight: number
  /** 对应后端字段 dataSource */
  dataSource: string
  /** 对应后端字段 evaluationCycle */
  evaluationCycle: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 status */
  status: number
}

/**
 * PerformanceIndicatorQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceIndicatorQueryDto）
 */
export interface PerformanceIndicatorQuery extends TaktPagedQuery {
  /** 对应后端字段 indicatorCode */
  indicatorCode?: string
  /** 对应后端字段 indicatorName */
  indicatorName?: string
  /** 对应后端字段 category */
  category?: string
  /** 对应后端字段 indicatorType */
  indicatorType?: string
  /** 对应后端字段 indicatorDescription */
  indicatorDescription?: string
  /** 对应后端字段 scoringCriteria */
  scoringCriteria?: string
  /** 对应后端字段 targetValue */
  targetValue?: number
  /** 对应后端字段 minimumValue */
  minimumValue?: number
  /** 对应后端字段 excellentValue */
  excellentValue?: number
  /** 对应后端字段 standardWeight */
  standardWeight?: number
  /** 对应后端字段 dataSource */
  dataSource?: string
  /** 对应后端字段 evaluationCycle */
  evaluationCycle?: string
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
 * PerformanceIndicatorCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceIndicatorCreateDto）
 */
export interface PerformanceIndicatorCreate {
  /** 对应后端字段 indicatorCode */
  indicatorCode: string
  /** 对应后端字段 indicatorName */
  indicatorName: string
  /** 对应后端字段 category */
  category: string
  /** 对应后端字段 indicatorType */
  indicatorType: string
  /** 对应后端字段 indicatorDescription */
  indicatorDescription: string
  /** 对应后端字段 scoringCriteria */
  scoringCriteria: string
  /** 对应后端字段 targetValue */
  targetValue: number
  /** 对应后端字段 minimumValue */
  minimumValue: number
  /** 对应后端字段 excellentValue */
  excellentValue: number
  /** 对应后端字段 standardWeight */
  standardWeight: number
  /** 对应后端字段 dataSource */
  dataSource: string
  /** 对应后端字段 evaluationCycle */
  evaluationCycle: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PerformanceIndicatorUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceIndicatorUpdateDto）
 */
export interface PerformanceIndicatorUpdate extends PerformanceIndicatorCreate {
  /** 对应后端字段 performanceIndicatorId */
  performanceIndicatorId: string
}

/**
 * PerformanceIndicatorStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceIndicatorStatusDto）
 */
export interface PerformanceIndicatorStatus {
  /** 对应后端字段 performanceIndicatorId */
  performanceIndicatorId: string
  /** 对应后端字段 status */
  status: number
}

/**
 * PerformanceIndicatorSort类型（对应后端 Takt.Application.Dtos.HumanResource.Performance.TaktPerformanceIndicatorSortDto）
 */
export interface PerformanceIndicatorSort {
  /** 对应后端字段 performanceIndicatorId */
  performanceIndicatorId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
