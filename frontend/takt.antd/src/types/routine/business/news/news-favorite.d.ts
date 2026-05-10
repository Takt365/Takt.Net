// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/news/news-favorite
// 文件名称：news-favorite.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：news-favorite相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * NewsFavorite类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsFavoriteDto）
 */
export interface NewsFavorite extends TaktEntityBase {
  /** 对应后端字段 newsFavoriteId */
  newsFavoriteId: string
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 favoriteTime */
  favoriteTime: string
  /** 对应后端字段 favoriteTags */
  favoriteTags?: string
  /** 对应后端字段 news */
  news?: unknown
}

/**
 * NewsFavoriteQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsFavoriteQueryDto）
 */
export interface NewsFavoriteQuery extends TaktPagedQuery {
  /** 对应后端字段 newsId */
  newsId?: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 favoriteTime */
  favoriteTime?: string
  /** 对应后端字段 favoriteTimeStart */
  favoriteTimeStart?: string
  /** 对应后端字段 favoriteTimeEnd */
  favoriteTimeEnd?: string
  /** 对应后端字段 favoriteTags */
  favoriteTags?: string
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
 * NewsFavoriteCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsFavoriteCreateDto）
 */
export interface NewsFavoriteCreate {
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 favoriteTime */
  favoriteTime: string
  /** 对应后端字段 favoriteTags */
  favoriteTags?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * NewsFavoriteUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsFavoriteUpdateDto）
 */
export interface NewsFavoriteUpdate extends NewsFavoriteCreate {
  /** 对应后端字段 newsFavoriteId */
  newsFavoriteId: string
}
