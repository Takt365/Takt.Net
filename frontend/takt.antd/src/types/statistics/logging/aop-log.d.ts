// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/statistics/logging/aop-log
// 文件名称：aop-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：aop-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AopLog类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktAopLogDto）
 */
export interface AopLog extends TaktEntityBase {
  /** 对应后端字段 aopLogId */
  aopLogId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 operType */
  operType: string
  /** 对应后端字段 tableName */
  tableName: string
  /** 对应后端字段 primaryKeyId */
  primaryKeyId?: string
  /** 对应后端字段 beforeData */
  beforeData?: string
  /** 对应后端字段 afterData */
  afterData?: string
  /** 对应后端字段 diffData */
  diffData?: string
  /** 对应后端字段 sqlStatement */
  sqlStatement?: string
  /** 对应后端字段 operTime */
  operTime: string
  /** 对应后端字段 costTime */
  costTime: number
}

/**
 * AopLogQuery类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktAopLogQueryDto）
 */
export interface AopLogQuery extends TaktPagedQuery {
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 operType */
  operType?: string
  /** 对应后端字段 tableName */
  tableName?: string
  /** 对应后端字段 primaryKeyId */
  primaryKeyId?: string
  /** 对应后端字段 operTimeStart */
  operTimeStart?: string
  /** 对应后端字段 operTimeEnd */
  operTimeEnd?: string
}

/**
 * CreateAopLog类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktCreateAopLogDto）
 */
export interface CreateAopLog {
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 auditUserId */
  auditUserId?: string
  /** 对应后端字段 auditUserDisplayName */
  auditUserDisplayName?: string
  /** 对应后端字段 operType */
  operType: string
  /** 对应后端字段 tableName */
  tableName: string
  /** 对应后端字段 primaryKeyId */
  primaryKeyId?: string
  /** 对应后端字段 beforeData */
  beforeData?: string
  /** 对应后端字段 afterData */
  afterData?: string
  /** 对应后端字段 diffData */
  diffData?: string
  /** 对应后端字段 sqlStatement */
  sqlStatement?: string
  /** 对应后端字段 operTime */
  operTime?: string
  /** 对应后端字段 costTime */
  costTime: number
}
