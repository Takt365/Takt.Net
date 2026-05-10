// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/identity/role-menu
// 文件名称：role-menu.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：role-menu相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * RoleMenu类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleMenuDto）
 */
export interface RoleMenu extends TaktEntityBase {
  /** 对应后端字段 roleMenuId */
  roleMenuId: string
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 menuId */
  menuId: string
}

/**
 * RoleMenuQuery类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleMenuQueryDto）
 */
export interface RoleMenuQuery extends TaktPagedQuery {
  /** 对应后端字段 roleId */
  roleId?: string
  /** 对应后端字段 menuId */
  menuId?: string
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
 * RoleMenuCreate类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleMenuCreateDto）
 */
export interface RoleMenuCreate {
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 menuId */
  menuId: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * RoleMenuUpdate类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleMenuUpdateDto）
 */
export interface RoleMenuUpdate extends RoleMenuCreate {
  /** 对应后端字段 roleMenuId */
  roleMenuId: string
}
