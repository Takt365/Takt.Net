// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/organization/post
// 文件名称：post.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：post相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Post类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostDto）
 */
export interface Post extends TaktEntityBase {
  /** 对应后端字段 postId */
  postId: string
  /** 对应后端字段 postName */
  postName: string
  /** 对应后端字段 postCode */
  postCode: string
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 postCategory */
  postCategory?: string
  /** 对应后端字段 postLevel */
  postLevel: number
  /** 对应后端字段 postDuty */
  postDuty?: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 postStatus */
  postStatus: number
  /** 对应后端字段 userIds */
  userIds?: string[]
}

/**
 * PostQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostQueryDto）
 */
export interface PostQuery extends TaktPagedQuery {
  /** 对应后端字段 postName */
  postName?: string
  /** 对应后端字段 postCode */
  postCode?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 postStatus */
  postStatus?: number
}

/**
 * PostCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostCreateDto）
 */
export interface PostCreate {
  /** 对应后端字段 postName */
  postName: string
  /** 对应后端字段 postCode */
  postCode: string
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 postCategory */
  postCategory?: string
  /** 对应后端字段 postLevel */
  postLevel: number
  /** 对应后端字段 postDuty */
  postDuty?: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 userIds */
  userIds?: string[]
}

/**
 * PostUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostUpdateDto）
 */
export interface PostUpdate extends PostCreate {
  /** 对应后端字段 postId */
  postId: string
}

/**
 * PostStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostStatusDto）
 */
export interface PostStatus {
  /** 对应后端字段 postId */
  postId: string
  /** 对应后端字段 postStatus */
  postStatus: number
}

/**
 * PostAssignUsers类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostAssignUsersDto）
 */
export interface PostAssignUsers {
  /** 对应后端字段 postId */
  postId: string
  /** 对应后端字段 userIds */
  userIds: string[]
}
