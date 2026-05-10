// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/identity/user-role
// 文件名称：user-role.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：user-role相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * UserRole类型（对应后端 Takt.Application.Dtos.Identity.TaktUserRoleDto）
 */
export interface UserRole extends TaktEntityBase {
  /** 对应后端字段 userRoleId */
  userRoleId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 roleId */
  roleId: string
}

/**
 * UserRoleQuery类型（对应后端 Takt.Application.Dtos.Identity.TaktUserRoleQueryDto）
 */
export interface UserRoleQuery extends TaktPagedQuery {
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 roleId */
  roleId?: string
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
 * UserRoleCreate类型（对应后端 Takt.Application.Dtos.Identity.TaktUserRoleCreateDto）
 */
export interface UserRoleCreate {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * UserRoleUpdate类型（对应后端 Takt.Application.Dtos.Identity.TaktUserRoleUpdateDto）
 */
export interface UserRoleUpdate extends UserRoleCreate {
  /** 对应后端字段 userRoleId */
  userRoleId: string
}
