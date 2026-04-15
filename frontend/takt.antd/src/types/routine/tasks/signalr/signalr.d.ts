// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/routine/signalr/signalr
// 文件名称：signalr.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：SignalR 类型定义
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/**
 * 在线用户信息
 */
export interface OnlineUser {
  /** 用户名 */
  userName: string
  /** 用户ID */
  userId?: number
  /** 连接时间 */
  connectTime: string
  /** 最后活跃时间 */
  lastActiveTime?: string
}

/**
 * 用户连接事件数据
 */
export interface UserConnectedEvent {
  /** 用户名 */
  userName: string
  /** 用户ID */
  userId?: number
  /** 连接时间 */
  connectTime: string
}

/**
 * 用户断开连接事件数据
 */
export interface UserDisconnectedEvent {
  /** 用户名 */
  userName: string
  /** 断开时间 */
  disconnectTime: string
}

/**
 * 消息数据
 */
export interface SignalRMessage {
  /** 发送者用户名 */
  fromUserName: string
  /** 发送者用户ID */
  fromUserId?: number
  /** 接收者用户名 */
  toUserName: string
  /** 接收者用户ID */
  toUserId?: number
  /** 消息标题 */
  messageTitle?: string
  /** 消息内容 */
  messageContent: string
  /** 消息类型（Text、Image、File、System） */
  messageType: string
  /** 消息分组（Chat、Notification、Alert） */
  messageGroup: string
  /** 读取状态（0=未读，1=已读） */
  readStatus: number
  /** 发送时间 */
  sendTime: string
  /** 消息扩展数据（JSON格式） */
  messageExtData?: string
}

/**
 * 广播消息数据
 */
export interface BroadcastMessage {
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
 * 消息已发送事件数据
 */
export interface MessageSentEvent {
  /** 接收者用户名 */
  toUserName: string
  /** 发送时间 */
  sendTime: string
}

/**
 * 消息已读事件数据
 */
export interface MessageReadEvent {
  /** 消息ID */
  messageId: number
  /** 读取时间 */
  readTime: string
}

/**
 * 错误事件数据
 */
export interface SignalRErrorEvent {
  /** 错误消息 */
  message: string
}

/**
 * 上线消息事件数据
 */
export interface OnlineMessageEvent {
  /** 消息内容 */
  message: string
  /** 消息类型 */
  messageType: string
  /** 用户名 */
  userName: string
  /** 用户ID */
  userId?: number
  /** 真实姓名 */
  realName?: string
  /** 连接时间 */
  connectTime: string
  /** 连接IP（仅连接 Hub） */
  connectIp?: string
  /** 设备类型（仅连接 Hub） */
  deviceType?: string
}
