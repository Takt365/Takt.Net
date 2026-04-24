// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/statistics/logging/quartz-log
// 文件名称：quartz-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quartz-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QuartzLog类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktQuartzLogDto）
 */
export interface QuartzLog extends TaktEntityBase {
  /** 对应后端字段 quartzLogId */
  quartzLogId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 jobName */
  jobName: string
  /** 对应后端字段 jobGroup */
  jobGroup: string
  /** 对应后端字段 triggerName */
  triggerName: string
  /** 对应后端字段 triggerGroup */
  triggerGroup: string
  /** 对应后端字段 executeStatus */
  executeStatus: number
  /** 对应后端字段 executeResult */
  executeResult?: string
  /** 对应后端字段 errorMsg */
  errorMsg?: string
  /** 对应后端字段 executeTime */
  executeTime: string
  /** 对应后端字段 costTime */
  costTime: number
  /** 对应后端字段 jobData */
  jobData?: string
  /** 对应后端字段 nextFireTime */
  nextFireTime?: string
  /** 对应后端字段 previousFireTime */
  previousFireTime?: string
}

/**
 * QuartzLogQuery类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktQuartzLogQueryDto）
 */
export interface QuartzLogQuery extends TaktPagedQuery {
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 jobName */
  jobName?: string
  /** 对应后端字段 jobGroup */
  jobGroup?: string
  /** 对应后端字段 triggerName */
  triggerName?: string
  /** 对应后端字段 triggerGroup */
  triggerGroup?: string
  /** 对应后端字段 executeStatus */
  executeStatus?: number
  /** 对应后端字段 executeTimeStart */
  executeTimeStart?: string
  /** 对应后端字段 executeTimeEnd */
  executeTimeEnd?: string
}

/**
 * CreateQuartzLog类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktCreateQuartzLogDto）
 */
export interface CreateQuartzLog {
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 jobName */
  jobName: string
  /** 对应后端字段 jobGroup */
  jobGroup: string
  /** 对应后端字段 triggerName */
  triggerName: string
  /** 对应后端字段 triggerGroup */
  triggerGroup: string
  /** 对应后端字段 executeStatus */
  executeStatus: number
  /** 对应后端字段 executeResult */
  executeResult?: string
  /** 对应后端字段 errorMsg */
  errorMsg?: string
  /** 对应后端字段 executeTime */
  executeTime?: string
  /** 对应后端字段 costTime */
  costTime: number
  /** 对应后端字段 jobData */
  jobData?: string
  /** 对应后端字段 nextFireTime */
  nextFireTime?: string
  /** 对应后端字段 previousFireTime */
  previousFireTime?: string
}
