// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/identity/role
// 文件名称：role.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：role相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Role类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleDto）
 */
export interface Role extends TaktEntityBase {
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 roleName */
  roleName: string
  /** 对应后端字段 roleCode */
  roleCode: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 roleStatus */
  roleStatus: number
  /** 对应后端字段 menuIds */
  menuIds?: string[]
  /** 对应后端字段 userIds */
  userIds?: string[]
  /** 对应后端字段 deptIds */
  deptIds?: string[]
}

/**
 * RoleQuery类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleQueryDto）
 */
export interface RoleQuery extends TaktPagedQuery {
  /** 对应后端字段 roleName */
  roleName?: string
  /** 对应后端字段 roleCode */
  roleCode?: string
  /** 对应后端字段 roleStatus */
  roleStatus?: number
}

/**
 * RoleCreate类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleCreateDto）
 */
export interface RoleCreate {
  /** 对应后端字段 roleName */
  roleName: string
  /** 对应后端字段 roleCode */
  roleCode: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 menuIds */
  menuIds?: string[]
  /** 对应后端字段 userIds */
  userIds?: string[]
  /** 对应后端字段 deptIds */
  deptIds?: string[]
}

/**
 * RoleUpdate类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleUpdateDto）
 */
export interface RoleUpdate extends RoleCreate {
  /** 对应后端字段 roleId */
  roleId: string
}

/**
 * RoleStatus类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleStatusDto）
 */
export interface RoleStatus {
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 roleStatus */
  roleStatus: number
}

/**
 * RoleAssignDepts类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleAssignDeptsDto）
 */
export interface RoleAssignDepts {
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 deptIds */
  deptIds: string[]
}

/**
 * RoleAssignMenus类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleAssignMenusDto）
 */
export interface RoleAssignMenus {
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 menuIds */
  menuIds: string[]
}
