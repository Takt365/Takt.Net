// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/news/news
// 文件名称：news.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：news相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * News类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsDto）
 */
export interface News extends TaktEntityBase {
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 newsCode */
  newsCode: string
  /** 对应后端字段 newsCategory */
  newsCategory: number
  /** 对应后端字段 newsTitle */
  newsTitle: string
  /** 对应后端字段 newsSummary */
  newsSummary?: string
  /** 对应后端字段 newsContent */
  newsContent: string
  /** 对应后端字段 newsCoverImage */
  newsCoverImage?: string
  /** 对应后端字段 isTop */
  isTop: number
  /** 对应后端字段 isRecommended */
  isRecommended: number
  /** 对应后端字段 effectiveTime */
  effectiveTime?: string
  /** 对应后端字段 expireTime */
  expireTime?: string
  /** 对应后端字段 readCount */
  readCount: number
  /** 对应后端字段 likeCount */
  likeCount: number
  /** 对应后端字段 commentCount */
  commentCount: number
  /** 对应后端字段 favoriteCount */
  favoriteCount: number
  /** 对应后端字段 shareCount */
  shareCount: number
  /** 对应后端字段 attachmentCount */
  attachmentCount: number
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 publisherId */
  publisherId: string
  /** 对应后端字段 publisherName */
  publisherName: string
  /** 对应后端字段 publishTime */
  publishTime?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 newsStatus */
  newsStatus: number
  /** 对应后端字段 attachments */
  attachments?: unknown[]
  /** 对应后端字段 comments */
  comments?: unknown[]
  /** 对应后端字段 likes */
  likes?: unknown[]
  /** 对应后端字段 reads */
  reads?: unknown[]
  /** 对应后端字段 favorites */
  favorites?: unknown[]
  /** 对应后端字段 shares */
  shares?: unknown[]
}

/**
 * NewsQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsQueryDto）
 */
export interface NewsQuery extends TaktPagedQuery {
  /** 对应后端字段 newsCode */
  newsCode?: string
  /** 对应后端字段 newsCategory */
  newsCategory?: number
  /** 对应后端字段 newsTitle */
  newsTitle?: string
  /** 对应后端字段 newsSummary */
  newsSummary?: string
  /** 对应后端字段 newsContent */
  newsContent?: string
  /** 对应后端字段 newsCoverImage */
  newsCoverImage?: string
  /** 对应后端字段 isTop */
  isTop?: number
  /** 对应后端字段 isRecommended */
  isRecommended?: number
  /** 对应后端字段 effectiveTime */
  effectiveTime?: string
  /** 对应后端字段 effectiveTimeStart */
  effectiveTimeStart?: string
  /** 对应后端字段 effectiveTimeEnd */
  effectiveTimeEnd?: string
  /** 对应后端字段 expireTime */
  expireTime?: string
  /** 对应后端字段 expireTimeStart */
  expireTimeStart?: string
  /** 对应后端字段 expireTimeEnd */
  expireTimeEnd?: string
  /** 对应后端字段 readCount */
  readCount?: number
  /** 对应后端字段 likeCount */
  likeCount?: number
  /** 对应后端字段 commentCount */
  commentCount?: number
  /** 对应后端字段 favoriteCount */
  favoriteCount?: number
  /** 对应后端字段 shareCount */
  shareCount?: number
  /** 对应后端字段 attachmentCount */
  attachmentCount?: number
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 publisherId */
  publisherId?: string
  /** 对应后端字段 publisherName */
  publisherName?: string
  /** 对应后端字段 publishTime */
  publishTime?: string
  /** 对应后端字段 publishTimeStart */
  publishTimeStart?: string
  /** 对应后端字段 publishTimeEnd */
  publishTimeEnd?: string
  /** 对应后端字段 newsStatus */
  newsStatus?: number
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
 * NewsCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsCreateDto）
 */
export interface NewsCreate {
  /** 对应后端字段 newsCode */
  newsCode: string
  /** 对应后端字段 newsCategory */
  newsCategory: number
  /** 对应后端字段 newsTitle */
  newsTitle: string
  /** 对应后端字段 newsSummary */
  newsSummary?: string
  /** 对应后端字段 newsContent */
  newsContent: string
  /** 对应后端字段 newsCoverImage */
  newsCoverImage?: string
  /** 对应后端字段 isTop */
  isTop: number
  /** 对应后端字段 isRecommended */
  isRecommended: number
  /** 对应后端字段 effectiveTime */
  effectiveTime?: string
  /** 对应后端字段 expireTime */
  expireTime?: string
  /** 对应后端字段 readCount */
  readCount: number
  /** 对应后端字段 likeCount */
  likeCount: number
  /** 对应后端字段 commentCount */
  commentCount: number
  /** 对应后端字段 favoriteCount */
  favoriteCount: number
  /** 对应后端字段 shareCount */
  shareCount: number
  /** 对应后端字段 attachmentCount */
  attachmentCount: number
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 publisherId */
  publisherId: string
  /** 对应后端字段 publisherName */
  publisherName: string
  /** 对应后端字段 publishTime */
  publishTime?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 newsStatus */
  newsStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 attachments */
  attachments?: unknown[]
  /** 对应后端字段 comments */
  comments?: unknown[]
  /** 对应后端字段 likes */
  likes?: unknown[]
  /** 对应后端字段 reads */
  reads?: unknown[]
  /** 对应后端字段 favorites */
  favorites?: unknown[]
  /** 对应后端字段 shares */
  shares?: unknown[]
}

/**
 * NewsUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsUpdateDto）
 */
export interface NewsUpdate extends NewsCreate {
  /** 对应后端字段 newsId */
  newsId: string
}

/**
 * NewsStatus类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsStatusDto）
 */
export interface NewsStatus {
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 newsStatus */
  newsStatus: number
}

/**
 * NewsSort类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsSortDto）
 */
export interface NewsSort {
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
