// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/news/news-share
// 文件名称：news-share.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：news-share相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * NewsShare类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsShareDto）
 */
export interface NewsShare extends TaktEntityBase {
  /** 对应后端字段 newsShareId */
  newsShareId: string
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 sharePlatform */
  sharePlatform: number
  /** 对应后端字段 shareTime */
  shareTime: string
  /** 对应后端字段 shareRemark */
  shareRemark?: string
  /** 对应后端字段 news */
  news?: unknown
}

/**
 * NewsShareQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsShareQueryDto）
 */
export interface NewsShareQuery extends TaktPagedQuery {
  /** 对应后端字段 newsId */
  newsId?: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 sharePlatform */
  sharePlatform?: number
  /** 对应后端字段 shareTime */
  shareTime?: string
  /** 对应后端字段 shareTimeStart */
  shareTimeStart?: string
  /** 对应后端字段 shareTimeEnd */
  shareTimeEnd?: string
  /** 对应后端字段 shareRemark */
  shareRemark?: string
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
 * NewsShareCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsShareCreateDto）
 */
export interface NewsShareCreate {
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 sharePlatform */
  sharePlatform: number
  /** 对应后端字段 shareTime */
  shareTime: string
  /** 对应后端字段 shareRemark */
  shareRemark?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * NewsShareUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsShareUpdateDto）
 */
export interface NewsShareUpdate extends NewsShareCreate {
  /** 对应后端字段 newsShareId */
  newsShareId: string
}
