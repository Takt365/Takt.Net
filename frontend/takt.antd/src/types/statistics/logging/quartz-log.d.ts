// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/logging/quartz-log
// 文件名称：quartz-log.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：任务日志相关类型定义，对应后端 Takt.Application.Dtos.Logging.TaktQuartzLogDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 任务日志类型（对应后端 Takt.Application.Dtos.Logging.TaktQuartzLogDto）
 */
export interface QuartzLog extends TaktEntityBase {
  /** 任务日志ID（对应后端 QuartzLogId，序列化为 string 以避免精度问题） */
  quartzLogId: string
  /** 用户名（系统任务可为 system） */
  userName?: string
  /** 任务名称 */
  jobName?: string
  /** 任务组 */
  jobGroup?: string
  /** 触发器名称 */
  triggerName?: string
  /** 触发器组 */
  triggerGroup?: string
  /** 执行状态（0=成功，1=失败） */
  executeStatus: number
  /** 执行结果（JSON） */
  executeResult?: string
  /** 错误消息 */
  errorMsg?: string
  /** 执行时间 */
  executeTime?: string
  /** 执行耗时（毫秒） */
  costTime?: number
  /** 任务参数（JSON） */
  jobData?: string
  /** 下一次执行时间 */
  nextFireTime?: string
  /** 上一次执行时间 */
  previousFireTime?: string
}

/**
 * 任务日志查询类型（对应后端 Takt.Application.Dtos.Logging.TaktQuartzLogQueryDto）
 * 请求时需转换为 PascalCase 与后端一致
 */
export interface QuartzLogQuery extends TaktPagedQuery {
  /** 关键词（在用户名、任务名称、触发器名称中模糊查询） */
  keyWords?: string
  /** 用户名 */
  userName?: string
  /** 任务名称 */
  jobName?: string
  /** 任务组 */
  jobGroup?: string
  /** 触发器名称 */
  triggerName?: string
  /** 触发器组 */
  triggerGroup?: string
  /** 执行状态（0=成功，1=失败） */
  executeStatus?: number
  /** 执行时间开始 */
  executeTimeStart?: string
  /** 执行时间结束 */
  executeTimeEnd?: string
}
