// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/logging/login-log
// 文件名称：login-log.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：登录日志相关类型定义，对应后端 Takt.Application.Dtos.Logging.TaktLoginLogDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 登录日志类型（对应后端 Takt.Application.Dtos.Logging.TaktLoginLogDto）
 */
export interface LoginLog extends TaktEntityBase {
  /** 登录日志ID（对应后端 LoginLogId，序列化为 string 以避免精度问题） */
  loginLogId: string
  /** 用户名 */
  userName?: string
  /** 登录IP */
  loginIp?: string
  /** 登录地点 */
  loginLocation?: string
  /** 登录方式（如：Password、RefreshToken、Sms、Email） */
  loginType?: string
  /** User-Agent */
  userAgent?: string
  /** 登录状态（0=成功，1=失败） */
  loginStatus: number
  /** 登录消息 */
  loginMsg?: string
  /** 登录时间 */
  loginTime?: string
  /** 退出时间 */
  logoutTime?: string
  /** 会话时长（秒） */
  sessionDuration?: number
}

/**
 * 登录日志查询类型（对应后端 Takt.Application.Dtos.Logging.TaktLoginLogQueryDto）
 * 请求时需转换为 PascalCase 与后端一致
 */
export interface LoginLogQuery extends TaktPagedQuery {
  /** 关键词（在用户名、登录IP中模糊查询） */
  keyWords?: string
  /** 用户名 */
  userName?: string
  /** 登录IP */
  loginIp?: string
  /** 登录方式 */
  loginType?: string
  /** 登录状态（0=成功，1=失败） */
  loginStatus?: number
  /** 登录时间开始 */
  loginTimeStart?: string
  /** 登录时间结束 */
  loginTimeEnd?: string
}
