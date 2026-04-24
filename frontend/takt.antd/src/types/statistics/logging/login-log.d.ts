// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/statistics/logging/login-log
// 文件名称：login-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：login-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * LoginLog类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktLoginLogDto）
 */
export interface LoginLog extends TaktEntityBase {
  /** 对应后端字段 loginLogId */
  loginLogId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 loginIp */
  loginIp?: string
  /** 对应后端字段 loginLocation */
  loginLocation?: string
  /** 对应后端字段 loginType */
  loginType?: string
  /** 对应后端字段 userAgent */
  userAgent?: string
  /** 对应后端字段 loginStatus */
  loginStatus: number
  /** 对应后端字段 loginMsg */
  loginMsg?: string
  /** 对应后端字段 loginTime */
  loginTime: string
  /** 对应后端字段 logoutTime */
  logoutTime?: string
  /** 对应后端字段 sessionDuration */
  sessionDuration?: number
}

/**
 * LoginLogQuery类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktLoginLogQueryDto）
 */
export interface LoginLogQuery extends TaktPagedQuery {
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 loginIp */
  loginIp?: string
  /** 对应后端字段 loginType */
  loginType?: string
  /** 对应后端字段 loginStatus */
  loginStatus?: number
  /** 对应后端字段 loginTimeStart */
  loginTimeStart?: string
  /** 对应后端字段 loginTimeEnd */
  loginTimeEnd?: string
}

/**
 * CreateLoginLog类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktCreateLoginLogDto）
 */
export interface CreateLoginLog {
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 loginIp */
  loginIp?: string
  /** 对应后端字段 loginLocation */
  loginLocation?: string
  /** 对应后端字段 loginType */
  loginType?: string
  /** 对应后端字段 userAgent */
  userAgent?: string
  /** 对应后端字段 loginStatus */
  loginStatus: number
  /** 对应后端字段 loginMsg */
  loginMsg?: string
  /** 对应后端字段 loginTime */
  loginTime?: string
  /** 对应后端字段 logoutTime */
  logoutTime?: string
  /** 对应后端字段 sessionDuration */
  sessionDuration?: number
}
