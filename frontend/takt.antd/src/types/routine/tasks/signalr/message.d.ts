// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/routine/signalr/message
// 文件名称：message.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：在线消息类型定义，对应后端 Takt.Application.Dtos.Routine.SignalR.TaktMessageDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 在线消息（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktMessageDto）
 */
export interface Message {
  /** 消息ID（对应后端 MessageId，序列化为string以避免Javascript精度问题） */
  messageId: string
  /** 租户配置ID（对应后端 ConfigId） */
  configId: string
  /** 发送者用户名（对应后端 FromUserName） */
  fromUserName: string
  /** 发送者用户ID（对应后端 FromUserId，序列化为string以避免Javascript精度问题） */
  fromUserId?: string
  /** 接收者用户名（对应后端 ToUserName） */
  toUserName: string
  /** 接收者用户ID（对应后端 ToUserId，序列化为string以避免Javascript精度问题） */
  toUserId?: string
  /** 消息标题（对应后端 MessageTitle） */
  messageTitle?: string
  /** 消息内容（对应后端 MessageContent） */
  messageContent: string
  /** 消息类型（如：Text=文本，Image=图片，File=文件，System=系统消息，对应后端 MessageType） */
  messageType: string
  /** 消息分组（用于消息分类，如：Chat=聊天，Notification=通知，Alert=提醒，对应后端 MessageGroup） */
  messageGroup?: string
  /** 读取状态（0=未读，1=已读，对应后端 ReadStatus） */
  readStatus: number
  /** 读取时间（对应后端 ReadTime） */
  readTime?: string
  /** 发送时间（对应后端 SendTime） */
  sendTime: string
  /** 消息扩展数据（JSON格式，用于存储附件、图片URL等额外信息，对应后端 MessageExtData） */
  messageExtData?: string
  /** 创建时间（对应后端 CreateTime） */
  createTime: string
}

/**
 * 在线消息查询（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktMessageQueryDto）
 */
export interface MessageQuery extends TaktPagedQuery {
  /** 关键词（在发送者用户名、接收者用户名、消息标题、消息内容中模糊查询，对应后端 KeyWords） */
  keyWords?: string
  /** 发送者用户名（对应后端 FromUserName） */
  fromUserName?: string
  /** 接收者用户名（对应后端 ToUserName） */
  toUserName?: string
  /** 消息类型（对应后端 MessageType） */
  messageType?: string
  /** 消息分组（对应后端 MessageGroup） */
  messageGroup?: string
  /** 读取状态（0=未读，1=已读，对应后端 ReadStatus） */
  readStatus?: number
  /** 发送时间开始（对应后端 SendTimeStart） */
  sendTimeStart?: string
  /** 发送时间结束（对应后端 SendTimeEnd） */
  sendTimeEnd?: string
}

/**
 * 创建在线消息（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktMessageCreateDto）
 */
export interface MessageCreate {
  /** 发送者用户名（对应后端 FromUserName） */
  fromUserName: string
  /** 发送者用户ID（对应后端 FromUserId，序列化为string以避免Javascript精度问题） */
  fromUserId?: string
  /** 接收者用户名（对应后端 ToUserName） */
  toUserName: string
  /** 接收者用户ID（对应后端 ToUserId，序列化为string以避免Javascript精度问题） */
  toUserId?: string
  /** 消息标题（对应后端 MessageTitle） */
  messageTitle?: string
  /** 消息内容（对应后端 MessageContent） */
  messageContent: string
  /** 消息类型（如：Text=文本，Image=图片，File=文件，System=系统消息，对应后端 MessageType） */
  messageType: string
  /** 消息分组（用于消息分类，如：Chat=聊天，Notification=通知，Alert=提醒，对应后端 MessageGroup） */
  messageGroup?: string
  /** 读取状态（0=未读，1=已读，对应后端 ReadStatus） */
  readStatus: number
  /** 读取时间（对应后端 ReadTime） */
  readTime?: string
  /** 发送时间（对应后端 SendTime） */
  sendTime?: string
  /** 消息扩展数据（JSON格式，用于存储附件、图片URL等额外信息，对应后端 MessageExtData） */
  messageExtData?: string
}

/**
 * 更新在线消息（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktMessageUpdateDto）
 */
export interface MessageUpdate extends MessageCreate {
  /** 消息ID（对应后端 MessageId，序列化为string以避免Javascript精度问题） */
  messageId: string
}

/**
 * 消息已读（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktMessageReadDto）
 */
export interface MessageRead {
  /** 消息ID（对应后端 MessageId，序列化为string以避免Javascript精度问题） */
  messageId: string
}

/**
 * 在线消息导出（对应后端 Takt.Application.Dtos.Routine.SignalR.TaktMessageExportDto）
 */
export interface MessageExport {
  /** 发送者用户名（对应后端 FromUserName） */
  fromUserName: string
  /** 接收者用户名（对应后端 ToUserName） */
  toUserName: string
  /** 消息标题（对应后端 MessageTitle） */
  messageTitle: string
  /** 消息内容（对应后端 MessageContent） */
  messageContent: string
  /** 消息类型（字符串格式，用于Excel显示，对应后端 MessageType） */
  messageType: string
  /** 消息分组（对应后端 MessageGroup） */
  messageGroup: string
  /** 读取状态（字符串格式，用于Excel显示，对应后端 ReadStatus） */
  readStatus: string
  /** 读取时间（对应后端 ReadTime） */
  readTime?: string
  /** 发送时间（对应后端 SendTime） */
  sendTime: string
}
