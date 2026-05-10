// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/organization/user-post
// 文件名称：user-post.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：user-post相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * UserPost类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktUserPostDto）
 */
export interface UserPost extends TaktEntityBase {
  /** 对应后端字段 userPostId */
  userPostId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 postId */
  postId: string
}

/**
 * UserPostQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktUserPostQueryDto）
 */
export interface UserPostQuery extends TaktPagedQuery {
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 postId */
  postId?: string
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
 * UserPostCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktUserPostCreateDto）
 */
export interface UserPostCreate {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 postId */
  postId: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * UserPostUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktUserPostUpdateDto）
 */
export interface UserPostUpdate extends UserPostCreate {
  /** 对应后端字段 userPostId */
  userPostId: string
}
