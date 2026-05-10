// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/news/news-comment
// 文件名称：news-comment.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：news-comment相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * NewsComment类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCommentDto）
 */
export interface NewsComment extends TaktEntityBase {
  /** 对应后端字段 newsCommentId */
  newsCommentId: string
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 userAvatar */
  userAvatar?: string
  /** 对应后端字段 replyToUserId */
  replyToUserId?: string
  /** 对应后端字段 replyToUserName */
  replyToUserName?: string
  /** 对应后端字段 commentContent */
  commentContent: string
  /** 对应后端字段 commentTime */
  commentTime: string
  /** 对应后端字段 likeCount */
  likeCount: number
  /** 对应后端字段 replyCount */
  replyCount: number
  /** 对应后端字段 commentLevel */
  commentLevel: number
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 approvalStatus */
  approvalStatus: number
  /** 对应后端字段 commentStatus */
  commentStatus: number
  /** 对应后端字段 news */
  news?: unknown
  /** 对应后端字段 likes */
  likes?: unknown[]
}

/**
 * NewsCommentTree类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCommentTreeDto）
 */
export interface NewsCommentTree extends NewsComment {
  /** 对应后端字段 children */
  children: NewsCommentTree[]
}

/**
 * NewsCommentQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCommentQueryDto）
 */
export interface NewsCommentQuery extends TaktPagedQuery {
  /** 对应后端字段 newsId */
  newsId?: string
  /** 对应后端字段 parentId */
  parentId?: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 userAvatar */
  userAvatar?: string
  /** 对应后端字段 replyToUserId */
  replyToUserId?: string
  /** 对应后端字段 replyToUserName */
  replyToUserName?: string
  /** 对应后端字段 commentContent */
  commentContent?: string
  /** 对应后端字段 commentTime */
  commentTime?: string
  /** 对应后端字段 commentTimeStart */
  commentTimeStart?: string
  /** 对应后端字段 commentTimeEnd */
  commentTimeEnd?: string
  /** 对应后端字段 likeCount */
  likeCount?: number
  /** 对应后端字段 replyCount */
  replyCount?: number
  /** 对应后端字段 commentLevel */
  commentLevel?: number
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 approvalStatus */
  approvalStatus?: number
  /** 对应后端字段 commentStatus */
  commentStatus?: number
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
 * NewsCommentCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCommentCreateDto）
 */
export interface NewsCommentCreate {
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 userAvatar */
  userAvatar?: string
  /** 对应后端字段 replyToUserId */
  replyToUserId?: string
  /** 对应后端字段 replyToUserName */
  replyToUserName?: string
  /** 对应后端字段 commentContent */
  commentContent: string
  /** 对应后端字段 commentTime */
  commentTime: string
  /** 对应后端字段 likeCount */
  likeCount: number
  /** 对应后端字段 replyCount */
  replyCount: number
  /** 对应后端字段 commentLevel */
  commentLevel: number
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 approvalStatus */
  approvalStatus: number
  /** 对应后端字段 commentStatus */
  commentStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 likes */
  likes?: unknown[]
}

/**
 * NewsCommentUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCommentUpdateDto）
 */
export interface NewsCommentUpdate extends NewsCommentCreate {
  /** 对应后端字段 newsCommentId */
  newsCommentId: string
}

/**
 * NewsCommentApprovalStatus类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCommentApprovalStatusDto）
 */
export interface NewsCommentApprovalStatus {
  /** 对应后端字段 newsCommentId */
  newsCommentId: string
  /** 对应后端字段 approvalStatus */
  approvalStatus: number
}

/**
 * NewsCommentCommentStatus类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCommentCommentStatusDto）
 */
export interface NewsCommentCommentStatus {
  /** 对应后端字段 newsCommentId */
  newsCommentId: string
  /** 对应后端字段 commentStatus */
  commentStatus: number
}
