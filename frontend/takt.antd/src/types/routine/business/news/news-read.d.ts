// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/news/news-read
// 文件名称：news-read.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：news-read相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * NewsRead类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsReadDto）
 */
export interface NewsRead extends TaktEntityBase {
  /** 对应后端字段 newsReadId */
  newsReadId: string
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 readTime */
  readTime: string
  /** 对应后端字段 readDuration */
  readDuration: number
  /** 对应后端字段 news */
  news?: unknown
}

/**
 * NewsReadQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsReadQueryDto）
 */
export interface NewsReadQuery extends TaktPagedQuery {
  /** 对应后端字段 newsId */
  newsId?: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 readTime */
  readTime?: string
  /** 对应后端字段 readTimeStart */
  readTimeStart?: string
  /** 对应后端字段 readTimeEnd */
  readTimeEnd?: string
  /** 对应后端字段 readDuration */
  readDuration?: number
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
 * NewsReadCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsReadCreateDto）
 */
export interface NewsReadCreate {
  /** 对应后端字段 newsId */
  newsId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 readTime */
  readTime: string
  /** 对应后端字段 readDuration */
  readDuration: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * NewsReadUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.News.TaktNewsReadUpdateDto）
 */
export interface NewsReadUpdate extends NewsReadCreate {
  /** 对应后端字段 newsReadId */
  newsReadId: string
}
