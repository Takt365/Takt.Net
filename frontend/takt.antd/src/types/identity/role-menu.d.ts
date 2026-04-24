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
  /** 对应后端字段 roleName */
  roleName: string
  /** 对应后端字段 roleCode */
  roleCode: string
  /** 对应后端字段 menuId */
  menuId: string
  /** 对应后端字段 menuName */
  menuName: string
  /** 对应后端字段 menuCode */
  menuCode: string
  /** 对应后端字段 path */
  path?: string
  /** 对应后端字段 menuType */
  menuType: number
}
