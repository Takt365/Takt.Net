// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/signal-r/connect-engine
// 文件名称：connect.ts
// 功能描述：SignalR连接引擎API，对应后端 TaktConnectsController
// 路由前缀：api/TaktConnects
// ========================================

import request from '@/api/request'

/** 在线统计信息基类 */
export interface OnlineStatsDto {
  onlineCount: number
  peakCount: number
  averageCount: number
  timestamp: string
}

/** 实时在线统计 */
export interface RealTimeOnlineStatsDto extends OnlineStatsDto {
  onlineCount: number
  peakCount: number
  averageCount: number
  activeConnections: number
  timestamp: string
}

/** 今日在线统计 */
export interface TodayOnlineStatsDto extends OnlineStatsDto {
  maxOnlineTime: string
  minOnlineTime: string
  averageOnlineDuration: number
}

/** 月度在线统计 */
export interface MonthlyOnlineStatsDto {
  month: string
  totalOnlineTime: number
  averageOnlineUsers: number
  peakOnlineUsers: number
  dailyStats: Array<{
    date: string
    onlineCount: number
  }>
}

/** 消息统计信息 */
export interface MessageStatsDto {
  totalMessages: number
  unreadMessages: number
  readMessages: number
  averageMessageSize: number
}

/** 实时消息统计 */
export interface RealTimeMessageStatsDto extends MessageStatsDto {
  messagesPerMinute: number
  timestamp: string
}

/** 未读消息统计 */
export interface UnreadMessageDto {
  count: number
  lastMessageTime: string
  lastMessageContent?: string
}

/** 已读消息统计 */
export interface ReadMessageDto {
  count: number
  totalReadTime: number
  averageReadTime: number
}

/** 连接持续时间信息 */
export interface ConnectionDurationDto {
  connectionId: string
  startTime: string
  duration: string
  lastActivityTime: string
}

/** 断开连接时间信息 */
export interface DisconnectTimeDto {
  connectionId: string
  connectedAt: string
  disconnectedAt: string
  totalDuration: string
}

/** 在线用户统计 */
export interface OnlineUserStatsDto {
  userId: number
  userName: string
  onlineSince: string
  lastActivityTime: string
  ipAddress?: string
}

/** 离线用户统计 */
export interface OfflineUserStatsDto {
  userId: number
  userName: string
  lastOnlineTime: string
  totalOnlineDuration: string
  averageSessionDuration: number
}

/** 实时综合统计 */
export interface RealTimeCombinedStatsDto {
  onlineUsers: number
  activeConnections: number
  unreadMessages: number
  totalMessagesToday: number
  timestamp: string
}

/** 更新最后活跃时间请求 */
export interface TaktOnlineLastActiveTimeUpdateDto {
  userId: number
}

// ========================================
// SignalR连接引擎API
// ========================================
const connectUrl = '/api/TaktConnects'

/**
 * 获取实时在线用户统计信息
 * 对应后端：GetRealTimeOnlineStatsAsync
 */
export function getRealtimeOnlineStats(): Promise<RealTimeOnlineStatsDto> {
  return request({
    url: `${connectUrl}/realtime-online-stats`,
    method: 'get'
  })
}

/**
 * 获取今日在线用户统计信息
 * 对应后端：GetTodayOnlineStatsAsync
 */
export function getTodayOnlineStats(): Promise<TodayOnlineStatsDto> {
  return request({
    url: `${connectUrl}/today-online-stats`,
    method: 'get'
  })
}

/**
 * 获取月度在线用户统计信息
 * 对应后端：GetMonthlyOnlineStatsAsync
 * @param month 月份（如 "2026-05"）
 */
export function getMonthlyOnlineStats(month: string): Promise<MonthlyOnlineStatsDto> {
  return request({
    url: `${connectUrl}/monthly-online-stats/${month}`,
    method: 'get'
  })
}

/**
 * 获取实时消息统计信息
 * 对应后端：GetRealTimeMessageStatsAsync
 */
export function getRealtimeMessageStats(): Promise<RealTimeMessageStatsDto> {
  return request({
    url: `${connectUrl}/realtime-message-stats`,
    method: 'get'
  })
}

/**
 * 获取未读消息统计信息
 * 对应后端：GetUnreadMessageStatsAsync
 */
export function getUnreadMessageStats(): Promise<UnreadMessageDto> {
  return request({
    url: `${connectUrl}/unread-message-stats`,
    method: 'get'
  })
}

/**
 * 获取已读消息统计信息
 * 对应后端：GetReadMessageStatsAsync
 */
export function getReadMessageStats(): Promise<ReadMessageDto> {
  return request({
    url: `${connectUrl}/read-message-stats`,
    method: 'get'
  })
}

/**
 * 获取消息统计信息
 * 对应后端：GetMessageStatsAsync
 */
export function getMessageStats(): Promise<MessageStatsDto> {
  return request({
    url: `${connectUrl}/message-stats`,
    method: 'get'
  })
}

/**
 * 获取连接持续时间信息
 * 对应后端：GetConnectionDurationAsync
 * @param connectionId 连接ID
 */
export function getConnectionDuration(connectionId: string): Promise<ConnectionDurationDto> {
  return request({
    url: `${connectUrl}/connection-duration/${connectionId}`,
    method: 'get'
  })
}

/**
 * 获取连接持续时间统计信息
 * 对应后端：GetConnectionDurationStatsAsync
 */
export function getConnectionDurationStats(): Promise<ConnectionDurationDto> {
  return request({
    url: `${connectUrl}/connection-duration-stats`,
    method: 'get'
  })
}

/**
 * 获取断开连接时间信息
 * 对应后端：GetDisconnectTimeAsync
 * @param connectionId 连接ID
 */
export function getDisconnectTime(connectionId: string): Promise<DisconnectTimeDto> {
  return request({
    url: `${connectUrl}/disconnect-time/${connectionId}`,
    method: 'get'
  })
}

/**
 * 获取断开连接时间统计信息
 * 对应后端：GetDisconnectTimeStatsAsync
 */
export function getDisconnectTimeStats(): Promise<DisconnectTimeDto> {
  return request({
    url: `${connectUrl}/disconnect-time-stats`,
    method: 'get'
  })
}

/**
 * 获取在线用户统计信息
 * 对应后端：GetOnlineUserStatsAsync
 */
export function getOnlineUserStats(): Promise<OnlineUserStatsDto> {
  return request({
    url: `${connectUrl}/online-user-stats`,
    method: 'get'
  })
}

/**
 * 获取离线用户统计信息
 * 对应后端：GetOfflineUserStatsAsync
 */
export function getOfflineUserStats(): Promise<OfflineUserStatsDto> {
  return request({
    url: `${connectUrl}/offline-user-stats`,
    method: 'get'
  })
}

/**
 * 获取实时综合统计信息
 * 对应后端：GetRealTimeCombinedStatsAsync
 */
export function getRealtimeCombinedStats(): Promise<RealTimeCombinedStatsDto> {
  return request({
    url: `${connectUrl}/realtime-combined-stats`,
    method: 'get'
  })
}

/**
 * 更新用户最后活动时间
 * 对应后端：UpdateLastActiveTimeAsync
 * @param dto 更新参数
 */
export function updateLastActiveTime(dto: TaktOnlineLastActiveTimeUpdateDto): Promise<void> {
  return request({
    url: `${connectUrl}/update-last-active-time`,
    method: 'post',
    data: dto
  })
}