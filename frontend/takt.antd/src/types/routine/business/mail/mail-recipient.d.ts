// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/mail/mail-recipient
// 文件名称：mail-recipient.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：mail-recipient相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * MailRecipient类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailRecipientDto）
 */
export interface MailRecipient extends TaktEntityBase {
  /** 对应后端字段 mailRecipientId */
  mailRecipientId: string
  /** 对应后端字段 mailId */
  mailId: string
  /** 对应后端字段 recipientId */
  recipientId: string
  /** 对应后端字段 recipientName */
  recipientName: string
  /** 对应后端字段 recipientEmail */
  recipientEmail?: string
  /** 对应后端字段 recipientType */
  recipientType: number
  /** 对应后端字段 readStatus */
  readStatus: number
  /** 对应后端字段 readTime */
  readTime?: string
  /** 对应后端字段 isRecipientDeleted */
  isRecipientDeleted: number
  /** 对应后端字段 isStarred */
  isStarred: number
  /** 对应后端字段 isFlagged */
  isFlagged: number
  /** 对应后端字段 mail */
  mail?: unknown
}

/**
 * MailRecipientQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailRecipientQueryDto）
 */
export interface MailRecipientQuery extends TaktPagedQuery {
  /** 对应后端字段 mailId */
  mailId?: string
  /** 对应后端字段 recipientId */
  recipientId?: string
  /** 对应后端字段 recipientName */
  recipientName?: string
  /** 对应后端字段 recipientEmail */
  recipientEmail?: string
  /** 对应后端字段 recipientType */
  recipientType?: number
  /** 对应后端字段 readStatus */
  readStatus?: number
  /** 对应后端字段 readTime */
  readTime?: string
  /** 对应后端字段 readTimeStart */
  readTimeStart?: string
  /** 对应后端字段 readTimeEnd */
  readTimeEnd?: string
  /** 对应后端字段 isRecipientDeleted */
  isRecipientDeleted?: number
  /** 对应后端字段 isStarred */
  isStarred?: number
  /** 对应后端字段 isFlagged */
  isFlagged?: number
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
 * MailRecipientCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailRecipientCreateDto）
 */
export interface MailRecipientCreate {
  /** 对应后端字段 mailId */
  mailId: string
  /** 对应后端字段 recipientId */
  recipientId: string
  /** 对应后端字段 recipientName */
  recipientName: string
  /** 对应后端字段 recipientEmail */
  recipientEmail?: string
  /** 对应后端字段 recipientType */
  recipientType: number
  /** 对应后端字段 readStatus */
  readStatus: number
  /** 对应后端字段 readTime */
  readTime?: string
  /** 对应后端字段 isRecipientDeleted */
  isRecipientDeleted: number
  /** 对应后端字段 isStarred */
  isStarred: number
  /** 对应后端字段 isFlagged */
  isFlagged: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * MailRecipientUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailRecipientUpdateDto）
 */
export interface MailRecipientUpdate extends MailRecipientCreate {
  /** 对应后端字段 mailRecipientId */
  mailRecipientId: string
}

/**
 * MailRecipientReadStatus类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailRecipientReadStatusDto）
 */
export interface MailRecipientReadStatus {
  /** 对应后端字段 mailRecipientId */
  mailRecipientId: string
  /** 对应后端字段 readStatus */
  readStatus: number
}
