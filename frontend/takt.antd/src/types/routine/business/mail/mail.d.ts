// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/mail/mail
// 文件名称：mail.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：mail相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Mail类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailDto）
 */
export interface Mail extends TaktEntityBase {
  /** 对应后端字段 mailId */
  mailId: string
  /** 对应后端字段 mailCode */
  mailCode: string
  /** 对应后端字段 mailSubject */
  mailSubject: string
  /** 对应后端字段 mailContent */
  mailContent: string
  /** 对应后端字段 mailType */
  mailType: number
  /** 对应后端字段 senderId */
  senderId: string
  /** 对应后端字段 senderName */
  senderName: string
  /** 对应后端字段 senderEmail */
  senderEmail?: string
  /** 对应后端字段 recipientList */
  recipientList: string
  /** 对应后端字段 ccList */
  ccList?: string
  /** 对应后端字段 bccList */
  bccList?: string
  /** 对应后端字段 isImportant */
  isImportant: number
  /** 对应后端字段 isUrgent */
  isUrgent: number
  /** 对应后端字段 isReadReceiptRequired */
  isReadReceiptRequired: number
  /** 对应后端字段 isReadReceiptSent */
  isReadReceiptSent: number
  /** 对应后端字段 sendTime */
  sendTime?: string
  /** 对应后端字段 scheduledSendTime */
  scheduledSendTime?: string
  /** 对应后端字段 mailStatus */
  mailStatus: number
  /** 对应后端字段 sendFailureReason */
  sendFailureReason?: string
  /** 对应后端字段 attachmentCount */
  attachmentCount: number
  /** 对应后端字段 attachments */
  attachments?: unknown[]
  /** 对应后端字段 recipients */
  recipients?: unknown[]
}

/**
 * MailQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailQueryDto）
 */
export interface MailQuery extends TaktPagedQuery {
  /** 对应后端字段 mailCode */
  mailCode?: string
  /** 对应后端字段 mailSubject */
  mailSubject?: string
  /** 对应后端字段 mailContent */
  mailContent?: string
  /** 对应后端字段 mailType */
  mailType?: number
  /** 对应后端字段 senderId */
  senderId?: string
  /** 对应后端字段 senderName */
  senderName?: string
  /** 对应后端字段 senderEmail */
  senderEmail?: string
  /** 对应后端字段 recipientList */
  recipientList?: string
  /** 对应后端字段 ccList */
  ccList?: string
  /** 对应后端字段 bccList */
  bccList?: string
  /** 对应后端字段 isImportant */
  isImportant?: number
  /** 对应后端字段 isUrgent */
  isUrgent?: number
  /** 对应后端字段 isReadReceiptRequired */
  isReadReceiptRequired?: number
  /** 对应后端字段 isReadReceiptSent */
  isReadReceiptSent?: number
  /** 对应后端字段 sendTime */
  sendTime?: string
  /** 对应后端字段 sendTimeStart */
  sendTimeStart?: string
  /** 对应后端字段 sendTimeEnd */
  sendTimeEnd?: string
  /** 对应后端字段 scheduledSendTime */
  scheduledSendTime?: string
  /** 对应后端字段 scheduledSendTimeStart */
  scheduledSendTimeStart?: string
  /** 对应后端字段 scheduledSendTimeEnd */
  scheduledSendTimeEnd?: string
  /** 对应后端字段 mailStatus */
  mailStatus?: number
  /** 对应后端字段 sendFailureReason */
  sendFailureReason?: string
  /** 对应后端字段 attachmentCount */
  attachmentCount?: number
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
 * MailCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailCreateDto）
 */
export interface MailCreate {
  /** 对应后端字段 mailCode */
  mailCode: string
  /** 对应后端字段 mailSubject */
  mailSubject: string
  /** 对应后端字段 mailContent */
  mailContent: string
  /** 对应后端字段 mailType */
  mailType: number
  /** 对应后端字段 senderId */
  senderId: string
  /** 对应后端字段 senderName */
  senderName: string
  /** 对应后端字段 senderEmail */
  senderEmail?: string
  /** 对应后端字段 recipientList */
  recipientList: string
  /** 对应后端字段 ccList */
  ccList?: string
  /** 对应后端字段 bccList */
  bccList?: string
  /** 对应后端字段 isImportant */
  isImportant: number
  /** 对应后端字段 isUrgent */
  isUrgent: number
  /** 对应后端字段 isReadReceiptRequired */
  isReadReceiptRequired: number
  /** 对应后端字段 isReadReceiptSent */
  isReadReceiptSent: number
  /** 对应后端字段 sendTime */
  sendTime?: string
  /** 对应后端字段 scheduledSendTime */
  scheduledSendTime?: string
  /** 对应后端字段 mailStatus */
  mailStatus: number
  /** 对应后端字段 sendFailureReason */
  sendFailureReason?: string
  /** 对应后端字段 attachmentCount */
  attachmentCount: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 attachments */
  attachments?: unknown[]
  /** 对应后端字段 recipients */
  recipients?: unknown[]
}

/**
 * MailUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailUpdateDto）
 */
export interface MailUpdate extends MailCreate {
  /** 对应后端字段 mailId */
  mailId: string
}

/**
 * MailStatus类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailStatusDto）
 */
export interface MailStatus {
  /** 对应后端字段 mailId */
  mailId: string
  /** 对应后端字段 mailStatus */
  mailStatus: number
}
