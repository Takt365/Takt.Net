// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/news/news-attachment
// 文件名称：news-attachment.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：news-attachment相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * NewsAttachment类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsAttachmentDto）
 */
export interface NewsAttachment extends TaktEntityBase {
  /** 对应后端字段 newsAttachmentId */
  newsAttachmentId: string
  /** 对应后端字段 newsId */
  newsId: string
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
  /** 对应后端字段 news */
  news?: unknown
}

/**
 * NewsAttachmentQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsAttachmentQueryDto）
 */
export interface NewsAttachmentQuery extends TaktPagedQuery {
  /** 对应后端字段 newsId */
  newsId?: string
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
 * NewsAttachmentCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsAttachmentCreateDto）
 */
export interface NewsAttachmentCreate {
  /** 对应后端字段 newsId */
  newsId: string
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
 * NewsAttachmentUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsAttachmentUpdateDto）
 */
export interface NewsAttachmentUpdate extends NewsAttachmentCreate {
  /** 对应后端字段 newsAttachmentId */
  newsAttachmentId: string
}

/**
 * NewsAttachmentSort类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsAttachmentSortDto）
 */
export interface NewsAttachmentSort {
  /** 对应后端字段 newsAttachmentId */
  newsAttachmentId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
