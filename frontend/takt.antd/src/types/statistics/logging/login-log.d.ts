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
  /** 对应后端字段 loginCountry */
  loginCountry?: string
  /** 对应后端字段 loginProvince */
  loginProvince?: string
  /** 对应后端字段 loginCity */
  loginCity?: string
  /** 对应后端字段 loginIsp */
  loginIsp?: string
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
  /** 对应后端字段 loginLocation */
  loginLocation?: string
  /** 对应后端字段 loginCountry */
  loginCountry?: string
  /** 对应后端字段 loginProvince */
  loginProvince?: string
  /** 对应后端字段 loginCity */
  loginCity?: string
  /** 对应后端字段 loginIsp */
  loginIsp?: string
  /** 对应后端字段 loginType */
  loginType?: string
  /** 对应后端字段 userAgent */
  userAgent?: string
  /** 对应后端字段 loginStatus */
  loginStatus?: number
  /** 对应后端字段 loginMsg */
  loginMsg?: string
  /** 对应后端字段 loginTime */
  loginTime?: string
  /** 对应后端字段 loginTimeStart */
  loginTimeStart?: string
  /** 对应后端字段 loginTimeEnd */
  loginTimeEnd?: string
  /** 对应后端字段 logoutTime */
  logoutTime?: string
  /** 对应后端字段 logoutTimeStart */
  logoutTimeStart?: string
  /** 对应后端字段 logoutTimeEnd */
  logoutTimeEnd?: string
  /** 对应后端字段 sessionDuration */
  sessionDuration?: number
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
 * LoginLogCreate类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktLoginLogCreateDto）
 */
export interface LoginLogCreate {
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 loginIp */
  loginIp?: string
  /** 对应后端字段 loginLocation */
  loginLocation?: string
  /** 对应后端字段 loginCountry */
  loginCountry?: string
  /** 对应后端字段 loginProvince */
  loginProvince?: string
  /** 对应后端字段 loginCity */
  loginCity?: string
  /** 对应后端字段 loginIsp */
  loginIsp?: string
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
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * LoginLogUpdate类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktLoginLogUpdateDto）
 */
export interface LoginLogUpdate extends LoginLogCreate {
  /** 对应后端字段 loginLogId */
  loginLogId: string
}

/**
 * LoginLogLoginStatus类型（对应后端 Takt.Application.Dtos.Statistics.Logging.TaktLoginLogLoginStatusDto）
 */
export interface LoginLogLoginStatus {
  /** 对应后端字段 loginLogId */
  loginLogId: string
  /** 对应后端字段 loginStatus */
  loginStatus: number
}
