// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/routine/signalr/online
// 文件名称：online.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：在线用户类型定义，对应后端 Takt.Application.Dtos.Routine.SignalR.TaktOnlineDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 在线用户（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktOnlineDto）
 */
export interface Online {
  /** 在线用户ID（对应后端 OnlineId，序列化为string以避免Javascript精度问题） */
  onlineId: string
  /** 租户配置ID（对应后端 ConfigId） */
  configId: string
  /** SignalR连接ID（唯一索引，对应后端 ConnectionId） */
  connectionId: string
  /** 用户名（对应后端 UserName） */
  userName: string
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId?: string
  /** 在线状态（0=在线，1=离线，2=离开，对应后端 OnlineStatus） */
  onlineStatus: number
  /** 连接IP地址（对应后端 ConnectIp） */
  connectIp?: string
  /** 连接地点（对应后端 ConnectLocation） */
  connectLocation?: string
  /** User-Agent（浏览器信息，对应后端 UserAgent） */
  userAgent?: string
  /** 设备类型（如：PC、Mobile、Tablet，对应后端 DeviceType） */
  deviceType?: string
  /** 浏览器类型（如：Chrome、Firefox、Safari，对应后端 BrowserType） */
  browserType?: string
  /** 操作系统（如：Windows、macOS、Linux、iOS、Android，对应后端 OperatingSystem） */
  operatingSystem?: string
  /** 连接时间（对应后端 ConnectTime） */
  connectTime: string
  /** 最后活动时间（对应后端 LastActiveTime） */
  lastActiveTime?: string
  /** 断开时间（对应后端 DisconnectTime） */
  disconnectTime?: string
  /** 连接时长（秒，从连接到断开的时长，对应后端 ConnectionDuration） */
  connectionDuration?: number
  /** 创建时间（对应后端 CreateTime） */
  createTime: string
}

/**
 * 在线用户查询（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktOnlineQueryDto）
 */
export interface OnlineQuery extends TaktPagedQuery {
  /** 关键词（在用户名、连接ID、连接IP、连接地点中模糊查询，对应后端 KeyWords） */
  keyWords?: string
  /** 连接ID（对应后端 ConnectionId） */
  connectionId?: string
  /** 用户名（对应后端 UserName） */
  userName?: string
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId?: string
  /** 在线状态（0=在线，1=离线，2=离开，对应后端 OnlineStatus） */
  onlineStatus?: number
  /** 连接时间开始（对应后端 ConnectTimeStart） */
  connectTimeStart?: string
  /** 连接时间结束（对应后端 ConnectTimeEnd） */
  connectTimeEnd?: string
}

/**
 * 创建在线用户（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktOnlineCreateDto）
 */
export interface OnlineCreate {
  /** SignalR连接ID（唯一索引，对应后端 ConnectionId） */
  connectionId: string
  /** 用户名（对应后端 UserName） */
  userName: string
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId?: string
  /** 在线状态（0=在线，1=离线，2=离开，对应后端 OnlineStatus） */
  onlineStatus: number
  /** 连接IP地址（对应后端 ConnectIp） */
  connectIp?: string
  /** 连接地点（对应后端 ConnectLocation） */
  connectLocation?: string
  /** User-Agent（浏览器信息，对应后端 UserAgent） */
  userAgent?: string
  /** 设备类型（如：PC、Mobile、Tablet，对应后端 DeviceType） */
  deviceType?: string
  /** 浏览器类型（如：Chrome、Firefox、Safari，对应后端 BrowserType） */
  browserType?: string
  /** 操作系统（如：Windows、macOS、Linux、iOS、Android，对应后端 OperatingSystem） */
  operatingSystem?: string
  /** 连接时间（对应后端 ConnectTime） */
  connectTime?: string
}

/**
 * 在线用户状态（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktOnlineStatusDto）
 */
export interface OnlineStatus {
  /** 在线用户ID（对应后端 OnlineId，序列化为string以避免Javascript精度问题） */
  onlineId: string
  /** 在线状态（0=在线，1=离线，2=离开，对应后端 OnlineStatus） */
  onlineStatus: number
}

/**
 * 在线用户最后活动时间（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktOnlineLastDto）
 */
export interface OnlineLast {
  /** 连接ID（对应后端 ConnectionId） */
  connectionId: string
  /** 最后活动时间（对应后端 LastActiveTime） */
  lastActiveTime: string
}

/**
 * 在线用户连接时长（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktOnlineDurationDto）
 */
export interface OnlineDuration {
  /** 在线用户ID（对应后端 OnlineId，序列化为string以避免Javascript精度问题） */
  onlineId: string
  /** 断开时间（对应后端 DisconnectTime） */
  disconnectTime: string
  /** 连接时长（秒，从连接到断开的时长，对应后端 ConnectionDuration） */
  connectionDuration: number
}

/**
 * 在线用户导出（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktOnlineExportDto）
 */
export interface OnlineExport {
  /** 用户名（对应后端 UserName） */
  userName: string
  /** 在线状态（字符串格式，用于Excel显示，对应后端 OnlineStatus） */
  onlineStatus: string
  /** 连接IP地址（对应后端 ConnectIp） */
  connectIp: string
  /** 连接地点（对应后端 ConnectLocation） */
  connectLocation: string
  /** 设备类型（对应后端 DeviceType） */
  deviceType: string
  /** 浏览器类型（对应后端 BrowserType） */
  browserType: string
  /** 操作系统（对应后端 OperatingSystem） */
  operatingSystem: string
  /** 连接时间（对应后端 ConnectTime） */
  connectTime: string
  /** 最后活动时间（对应后端 LastActiveTime） */
  lastActiveTime?: string
  /** 断开时间（对应后端 DisconnectTime） */
  disconnectTime?: string
  /** 连接时长（秒，对应后端 ConnectionDuration） */
  connectionDuration?: number
}
