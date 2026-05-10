// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/organization/post-delegate
// 文件名称：post-delegate.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：post-delegate相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PostDelegate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostDelegateDto）
 */
export interface PostDelegate extends TaktEntityBase {
  /** 对应后端字段 postDelegateId */
  postDelegateId: string
  /** 对应后端字段 postId */
  postId: string
  /** 对应后端字段 delegateMode */
  delegateMode: number
  /** 对应后端字段 delegateEmployeeId */
  delegateEmployeeId?: string
  /** 对应后端字段 delegateDeptId */
  delegateDeptId?: string
  /** 对应后端字段 delegatePostId */
  delegatePostId?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * PostDelegateQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostDelegateQueryDto）
 */
export interface PostDelegateQuery extends TaktPagedQuery {
  /** 对应后端字段 postId */
  postId?: string
  /** 对应后端字段 delegateMode */
  delegateMode?: number
  /** 对应后端字段 delegateEmployeeId */
  delegateEmployeeId?: string
  /** 对应后端字段 delegateDeptId */
  delegateDeptId?: string
  /** 对应后端字段 delegatePostId */
  delegatePostId?: string
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
 * PostDelegateCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostDelegateCreateDto）
 */
export interface PostDelegateCreate {
  /** 对应后端字段 postId */
  postId: string
  /** 对应后端字段 delegateMode */
  delegateMode: number
  /** 对应后端字段 delegateEmployeeId */
  delegateEmployeeId?: string
  /** 对应后端字段 delegateDeptId */
  delegateDeptId?: string
  /** 对应后端字段 delegatePostId */
  delegatePostId?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PostDelegateUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostDelegateUpdateDto）
 */
export interface PostDelegateUpdate extends PostDelegateCreate {
  /** 对应后端字段 postDelegateId */
  postDelegateId: string
}

/**
 * PostDelegateSort类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostDelegateSortDto）
 */
export interface PostDelegateSort {
  /** 对应后端字段 postDelegateId */
  postDelegateId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
