// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/identity/role
// 文件名称：role.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：角色相关类型定义，对应后端 Takt.Application.Dtos.Identity.TaktRoleDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 角色类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleDto）
 */
export interface Role extends TaktEntityBase {
  /** 角色ID（对应后端 RoleId，序列化为string以避免Javascript精度问题） */
  roleId: string
  /** 角色名称 */
  roleName: string
  /** 角色编码 */
  roleCode: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围） */
  dataScope: number
  /** 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔） */
  customScope?: string
  /** 角色状态（1=启用，0=禁用） */
  roleStatus: number
  /** 菜单ID列表 */
  menuIds?: string[]
  /** 用户ID列表 */
  userIds?: string[]
  /** 部门ID列表 */
  deptIds?: string[]
}

/**
 * 角色查询类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleQueryDto）
 */
export interface RoleQuery extends TaktPagedQuery {
  /** 关键词（在角色名称、角色编码中模糊查询） */
  keyWords?: string
  /** 角色名称 */
  roleName?: string
  /** 角色编码 */
  roleCode?: string
  /** 角色状态（1=启用，0=禁用） */
  roleStatus?: number
}

/**
 * 创建角色类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleCreateDto）
 */
export interface RoleCreate {
  /** 角色名称 */
  roleName: string
  /** 角色编码 */
  roleCode: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围） */
  dataScope: number
  /** 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔） */
  customScope?: string
  /** 备注 */
  remark?: string
  /** 菜单ID列表 */
  menuIds?: string[]
  /** 用户ID列表 */
  userIds?: string[]
  /** 部门ID列表 */
  deptIds?: string[]
}

/**
 * 更新角色类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleUpdateDto）
 */
export interface RoleUpdate extends RoleCreate {
  /** 角色ID（对应后端 RoleId，序列化为string以避免Javascript精度问题） */
  roleId: string
}

/**
 * 角色状态类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleStatusDto）
 */
export interface RoleStatus {
  /** 角色ID（对应后端 RoleId，序列化为string以避免Javascript精度问题） */
  roleId: string
  /** 角色状态（1=启用，0=禁用） */
  roleStatus: number
}
