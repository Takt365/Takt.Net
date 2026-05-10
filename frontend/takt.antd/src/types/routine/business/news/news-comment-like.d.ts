// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/news/news-comment-like
// 文件名称：news-comment-like.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：news-comment-like相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * NewsCommentLike类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCommentLikeDto）
 */
export interface NewsCommentLike extends TaktEntityBase {
  /** 对应后端字段 newsCommentLikeId */
  newsCommentLikeId: string
  /** 对应后端字段 commentId */
  commentId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 likeTime */
  likeTime: string
  /** 对应后端字段 comment */
  comment?: unknown
}

/**
 * NewsCommentLikeQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCommentLikeQueryDto）
 */
export interface NewsCommentLikeQuery extends TaktPagedQuery {
  /** 对应后端字段 commentId */
  commentId?: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 likeTime */
  likeTime?: string
  /** 对应后端字段 likeTimeStart */
  likeTimeStart?: string
  /** 对应后端字段 likeTimeEnd */
  likeTimeEnd?: string
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
 * NewsCommentLikeCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCommentLikeCreateDto）
 */
export interface NewsCommentLikeCreate {
  /** 对应后端字段 commentId */
  commentId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 likeTime */
  likeTime: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * NewsCommentLikeUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCommentLikeUpdateDto）
 */
export interface NewsCommentLikeUpdate extends NewsCommentLikeCreate {
  /** 对应后端字段 newsCommentLikeId */
  newsCommentLikeId: string
}
