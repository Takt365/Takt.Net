// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/signal-r/specific-engine/signal-r
// 文件名称：signal-r.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：SignalR事件相关类型定义（基于后端TaktNotificationHub实际发送的数据结构）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * MessageRead类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.SpecificEngine.TaktMessageReadDto）
 */
export interface MessageReadDto {
  /** 对应后端字段 messageId */
  messageId: string
}

/**
 * OnlineLastActiveTimeUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.SpecificEngine.TaktOnlineLastActiveTimeUpdateDto）
 */
export interface OnlineLastActiveTimeUpdateDto {
  /** 对应后端字段 connectionId */
  connectionId: string
}

/**
 * OnlineUser类型（对应后端Hub OnlineMessage事件中的用户信息）
 * 基于 TaktNotificationHub.cs 第80-88行的匿名对象结构
 */
export interface OnlineUserDto {
  /** 用户名 */
  userName: string
  /** 用户ID（字符串类型，避免JS精度问题） */
  userId: string
  /** 真实姓名 */
  realName?: string
  /** 连接时间 */
  connectTime: string
  /** 消息内容 */
  message: string
  /** 消息类型 */
  messageType: string
}

/**
 * UserConnectedEvent类型（对应后端Hub UserConnected事件）
 * 注意：后端Hub暂未实现此事件，为前端预留
 */
export interface UserConnectedEventDto {
  /** SignalR连接ID */
  connectionId: string
  /** 用户名 */
  userName: string
  /** 用户ID（字符串类型） */
  userId: string
  /** 连接时间 */
  connectTime: string
  /** Hub列表 */
  hubs: string[]
}

/**
 * UserDisconnectedEvent类型（对应后端Hub UserDisconnected事件）
 * 注意：后端Hub暂未实现此事件，为前端预留
 */
export interface UserDisconnectedEventDto {
  /** SignalR连接ID */
  connectionId: string
  /** 用户名 */
  userName: string
  /** 断开原因 */
  reason?: string
  /** 断开时间 */
  disconnectTime: string
}

/**
 * SignalRMessage类型（对应后端Hub ReceiveMessage事件）
 * 基于 TaktNotificationHub.cs 第180-193行的匿名对象结构，与TaktMessageDto一致
 */
export interface SignalRMessageDto {
  /** 消息ID（字符串类型） */
  messageId?: string
  /** 发送者用户名 */
  fromUserName: string
  /** 发送者用户ID（字符串类型） */
  fromUserId?: string
  /** 接收者用户名 */
  toUserName: string
  /** 接收者用户ID（字符串类型） */
  toUserId?: string
  /** 消息标题 */
  messageTitle?: string
  /** 消息内容 */
  messageContent: string
  /** 消息类型（如：Text、Image、File、System） */
  messageType: string
  /** 消息分组（如：Chat、Notification、Alert） */
  messageGroup?: string
  /** 读取状态（0=未读，1=已读） */
  readStatus: number
  /** 读取时间 */
  readTime?: string
  /** 发送时间 */
  sendTime: string
  /** 消息扩展数据（JSON格式） */
  messageExtData?: string
}

/**
 * BroadcastMessage类型（对应后端Hub ReceiveBroadcast事件）
 * 基于 TaktNotificationHub.cs 第259-267行的匿名对象结构
 */
export interface BroadcastMessageDto {
  /** 发送者用户名 */
  fromUserName: string
  /** 消息标题 */
  messageTitle?: string
  /** 消息内容 */
  messageContent: string
  /** 消息类型 */
  messageType: string
  /** 消息分组 */
  messageGroup: string
  /** 发送时间 */
  sendTime: string
}

/**
 * MessageSentEvent类型（对应后端Hub MessageSent事件）
 * 基于 TaktNotificationHub.cs 第199-203行的匿名对象结构
 */
export interface MessageSentEventDto {
  /** 接收者用户名 */
  toUserName: string
  /** 发送时间 */
  sendTime: string
}

/**
 * MessageReadEvent类型（对应后端Hub MessageRead事件）
 * 基于 TaktNotificationHub.cs 第299-303行的匿名对象结构
 */
export interface MessageReadEventDto {
  /** 消息ID */
  messageId: number
  /** 读取时间 */
  readTime: string
}

/**
 * SignalRErrorEvent类型（对应后端Hub Error事件）
 * 基于 TaktNotificationHub.cs 第235、280、310行的匿名对象结构
 */
export interface SignalRErrorEventDto {
  /** 错误消息 */
  message: string
}

/**
 * OnlineMessageEvent类型（对应后端Hub OnlineMessage事件）
 * 基于 TaktNotificationHub.cs 第80-88行的匿名对象结构，与OnlineUserDto相同
 */
export interface OnlineMessageEventDto {
  /** 用户名 */
  userName: string
  /** 用户ID（字符串类型） */
  userId: string
  /** 真实姓名 */
  realName?: string
  /** 连接时间 */
  connectTime: string
  /** 消息内容 */
  message: string
  /** 消息类型 */
  messageType: string
}
