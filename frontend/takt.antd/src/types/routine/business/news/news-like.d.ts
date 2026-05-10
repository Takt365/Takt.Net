// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/news/news-like
// 文件名称：news-like.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：news-like相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * NewsLike类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsLikeDto）
 */
export interface NewsLike extends TaktEntityBase {
  /** 对应后端字段 newsLikeId */
  newsLikeId: string
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 likeTime */
  likeTime: string
  /** 对应后端字段 news */
  news?: unknown
}

/**
 * NewsLikeQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsLikeQueryDto）
 */
export interface NewsLikeQuery extends TaktPagedQuery {
  /** 对应后端字段 newsId */
  newsId?: string
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
 * NewsLikeCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsLikeCreateDto）
 */
export interface NewsLikeCreate {
  /** 对应后端字段 newsId */
  newsId: string
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
 * NewsLikeUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsLikeUpdateDto）
 */
export interface NewsLikeUpdate extends NewsLikeCreate {
  /** 对应后端字段 newsLikeId */
  newsLikeId: string
}
