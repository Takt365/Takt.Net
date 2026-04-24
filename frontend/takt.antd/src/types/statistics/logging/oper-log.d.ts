// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/statistics/logging/oper-log
// 文件名称：oper-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：oper-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * OperLog类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktOperLogDto）
 */
export interface OperLog extends TaktEntityBase {
  /** 对应后端字段 operLogId */
  operLogId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 operModule */
  operModule?: string
  /** 对应后端字段 operType */
  operType?: string
  /** 对应后端字段 operMethod */
  operMethod?: string
  /** 对应后端字段 requestMethod */
  requestMethod?: string
  /** 对应后端字段 operUrl */
  operUrl?: string
  /** 对应后端字段 requestParam */
  requestParam?: string
  /** 对应后端字段 jsonResult */
  jsonResult?: string
  /** 对应后端字段 operStatus */
  operStatus: number
  /** 对应后端字段 errorMsg */
  errorMsg?: string
  /** 对应后端字段 operIp */
  operIp?: string
  /** 对应后端字段 operLocation */
  operLocation?: string
  /** 对应后端字段 operTime */
  operTime: string
  /** 对应后端字段 costTime */
  costTime: number
}

/**
 * OperLogQuery类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktOperLogQueryDto）
 */
export interface OperLogQuery extends TaktPagedQuery {
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 operModule */
  operModule?: string
  /** 对应后端字段 operType */
  operType?: string
  /** 对应后端字段 operStatus */
  operStatus?: number
  /** 对应后端字段 operTimeStart */
  operTimeStart?: string
  /** 对应后端字段 operTimeEnd */
  operTimeEnd?: string
}

/**
 * CreateOperLog类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktCreateOperLogDto）
 */
export interface CreateOperLog {
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 auditUserId */
  auditUserId?: string
  /** 对应后端字段 auditUserDisplayName */
  auditUserDisplayName?: string
  /** 对应后端字段 operModule */
  operModule?: string
  /** 对应后端字段 operType */
  operType?: string
  /** 对应后端字段 operMethod */
  operMethod?: string
  /** 对应后端字段 requestMethod */
  requestMethod?: string
  /** 对应后端字段 operUrl */
  operUrl?: string
  /** 对应后端字段 requestParam */
  requestParam?: string
  /** 对应后端字段 jsonResult */
  jsonResult?: string
  /** 对应后端字段 operStatus */
  operStatus: number
  /** 对应后端字段 errorMsg */
  errorMsg?: string
  /** 对应后端字段 operIp */
  operIp?: string
  /** 对应后端字段 operLocation */
  operLocation?: string
  /** 对应后端字段 operTime */
  operTime?: string
  /** 对应后端字段 costTime */
  costTime: number
}
