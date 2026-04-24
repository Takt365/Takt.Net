// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/signal-r/message
// 文件名称：message.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：message相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Message类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.TaktMessageDto）
 */
export interface Message extends TaktEntityBase {
  /** 对应后端字段 messageId */
  messageId: string
  /** 对应后端字段 fromUserName */
  fromUserName: string
  /** 对应后端字段 fromUserId */
  fromUserId?: string
  /** 对应后端字段 toUserName */
  toUserName: string
  /** 对应后端字段 toUserId */
  toUserId?: string
  /** 对应后端字段 messageTitle */
  messageTitle?: string
  /** 对应后端字段 messageContent */
  messageContent: string
  /** 对应后端字段 messageType */
  messageType: string
  /** 对应后端字段 messageGroup */
  messageGroup?: string
  /** 对应后端字段 readStatus */
  readStatus: number
  /** 对应后端字段 readTime */
  readTime?: string
  /** 对应后端字段 sendTime */
  sendTime: string
  /** 对应后端字段 messageExtData */
  messageExtData?: string
}

/**
 * MessageQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.TaktMessageQueryDto）
 */
export interface MessageQuery extends TaktPagedQuery {
  /** 对应后端字段 fromUserName */
  fromUserName?: string
  /** 对应后端字段 toUserName */
  toUserName?: string
  /** 对应后端字段 messageType */
  messageType?: string
  /** 对应后端字段 messageGroup */
  messageGroup?: string
  /** 对应后端字段 readStatus */
  readStatus?: number
  /** 对应后端字段 sendTimeStart */
  sendTimeStart?: string
  /** 对应后端字段 sendTimeEnd */
  sendTimeEnd?: string
}

/**
 * MessageCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.TaktMessageCreateDto）
 */
export interface MessageCreate {
  /** 对应后端字段 fromUserName */
  fromUserName: string
  /** 对应后端字段 fromUserId */
  fromUserId?: string
  /** 对应后端字段 toUserName */
  toUserName: string
  /** 对应后端字段 toUserId */
  toUserId?: string
  /** 对应后端字段 messageTitle */
  messageTitle?: string
  /** 对应后端字段 messageContent */
  messageContent: string
  /** 对应后端字段 messageType */
  messageType: string
  /** 对应后端字段 messageGroup */
  messageGroup?: string
  /** 对应后端字段 readStatus */
  readStatus: number
  /** 对应后端字段 readTime */
  readTime?: string
  /** 对应后端字段 sendTime */
  sendTime?: string
  /** 对应后端字段 messageExtData */
  messageExtData?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * MessageUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.TaktMessageUpdateDto）
 */
export interface MessageUpdate extends MessageCreate {
  /** 对应后端字段 messageId */
  messageId: string
}

/**
 * MessageRead类型（对应后端 Takt.Application.Dtos.Routine.Tasks.SignalR.TaktMessageReadDto）
 */
export interface MessageRead {
  /** 对应后端字段 messageId */
  messageId: string
}
