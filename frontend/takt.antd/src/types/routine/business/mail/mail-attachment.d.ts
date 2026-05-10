// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/mail/mail-attachment
// 文件名称：mail-attachment.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：mail-attachment相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * MailAttachment类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailAttachmentDto）
 */
export interface MailAttachment extends TaktEntityBase {
  /** 对应后端字段 mailAttachmentId */
  mailAttachmentId: string
  /** 对应后端字段 mailId */
  mailId: string
  /** 对应后端字段 fileId */
  fileId: string
  /** 对应后端字段 fileName */
  fileName: string
  /** 对应后端字段 filePath */
  filePath: string
  /** 对应后端字段 fileSize */
  fileSize: number
  /** 对应后端字段 fileType */
  fileType?: string
  /** 对应后端字段 fileExtension */
  fileExtension?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 mail */
  mail?: unknown
}

/**
 * MailAttachmentQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailAttachmentQueryDto）
 */
export interface MailAttachmentQuery extends TaktPagedQuery {
  /** 对应后端字段 mailId */
  mailId?: string
  /** 对应后端字段 fileId */
  fileId?: string
  /** 对应后端字段 fileName */
  fileName?: string
  /** 对应后端字段 filePath */
  filePath?: string
  /** 对应后端字段 fileSize */
  fileSize?: number
  /** 对应后端字段 fileType */
  fileType?: string
  /** 对应后端字段 fileExtension */
  fileExtension?: string
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
 * MailAttachmentCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailAttachmentCreateDto）
 */
export interface MailAttachmentCreate {
  /** 对应后端字段 mailId */
  mailId: string
  /** 对应后端字段 fileId */
  fileId: string
  /** 对应后端字段 fileName */
  fileName: string
  /** 对应后端字段 filePath */
  filePath: string
  /** 对应后端字段 fileSize */
  fileSize: number
  /** 对应后端字段 fileType */
  fileType?: string
  /** 对应后端字段 fileExtension */
  fileExtension?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * MailAttachmentUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailAttachmentUpdateDto）
 */
export interface MailAttachmentUpdate extends MailAttachmentCreate {
  /** 对应后端字段 mailAttachmentId */
  mailAttachmentId: string
}

/**
 * MailAttachmentSort类型（对应后端 Takt.Application.Dtos.Routine.Business.Mail.TaktMailAttachmentSortDto）
 */
export interface MailAttachmentSort {
  /** 对应后端字段 mailAttachmentId */
  mailAttachmentId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
