// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/signal-r/online
// 文件名称：online.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：online相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Online类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.TaktOnlineDto）
 */
export interface Online extends TaktEntityBase {
  /** 对应后端字段 onlineId */
  onlineId: string
  /** 对应后端字段 connectionId */
  connectionId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 onlineStatus */
  onlineStatus: number
  /** 对应后端字段 connectIp */
  connectIp?: string
  /** 对应后端字段 connectLocation */
  connectLocation?: string
  /** 对应后端字段 userAgent */
  userAgent?: string
  /** 对应后端字段 deviceType */
  deviceType?: string
  /** 对应后端字段 browserType */
  browserType?: string
  /** 对应后端字段 operatingSystem */
  operatingSystem?: string
  /** 对应后端字段 connectTime */
  connectTime: string
  /** 对应后端字段 lastActiveTime */
  lastActiveTime?: string
  /** 对应后端字段 disconnectTime */
  disconnectTime?: string
  /** 对应后端字段 connectionDuration */
  connectionDuration?: number
}

/**
 * OnlineQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.TaktOnlineQueryDto）
 */
export interface OnlineQuery extends TaktPagedQuery {
  /** 对应后端字段 connectionId */
  connectionId?: string
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 onlineStatus */
  onlineStatus?: number
  /** 对应后端字段 connectTimeStart */
  connectTimeStart?: string
  /** 对应后端字段 connectTimeEnd */
  connectTimeEnd?: string
}

/**
 * OnlineCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.TaktOnlineCreateDto）
 */
export interface OnlineCreate {
  /** 对应后端字段 connectionId */
  connectionId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 onlineStatus */
  onlineStatus: number
  /** 对应后端字段 connectIp */
  connectIp?: string
  /** 对应后端字段 connectLocation */
  connectLocation?: string
  /** 对应后端字段 userAgent */
  userAgent?: string
  /** 对应后端字段 deviceType */
  deviceType?: string
  /** 对应后端字段 browserType */
  browserType?: string
  /** 对应后端字段 operatingSystem */
  operatingSystem?: string
  /** 对应后端字段 connectTime */
  connectTime?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * OnlineStatus类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.TaktOnlineStatusDto）
 */
export interface OnlineStatus {
  /** 对应后端字段 onlineId */
  onlineId: string
  /** 对应后端字段 onlineStatus */
  onlineStatus: number
}

/**
 * OnlineDuration类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.TaktOnlineDurationDto）
 */
export interface OnlineDuration {
  /** 对应后端字段 onlineId */
  onlineId: string
  /** 对应后端字段 disconnectTime */
  disconnectTime: string
  /** 对应后端字段 connectionDuration */
  connectionDuration: number
}

/**
 * OnlineLast类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.TaktOnlineLastDto）
 */
export interface OnlineLast {
  /** 对应后端字段 connectionId */
  connectionId: string
  /** 对应后端字段 lastActiveTime */
  lastActiveTime: string
}
