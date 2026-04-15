// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/identity/role-menu
// 文件名称：role-menu.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：角色菜单关联类型定义，对应后端 Takt.Application.Dtos.Identity.TaktRoleMenuDto
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase } from '@/types/common'

/**
 * 角色菜单关联类型（对应后端 Takt.Application.Dtos.Identity.TaktRoleMenuDto）
 */
export interface RoleMenu extends TaktEntityBase {
  /** 角色菜单关联ID（对应后端 RoleMenuId，序列化为string以避免Javascript精度问题） */
  roleMenuId: string
  /** 角色ID（对应后端 RoleId，序列化为string以避免Javascript精度问题） */
  roleId: string
  /** 角色名称（对应后端 RoleName） */
  roleName: string
  /** 角色编码（对应后端 RoleCode） */
  roleCode: string
  /** 菜单ID（对应后端 MenuId，序列化为string以避免Javascript精度问题） */
  menuId: string
  /** 菜单名称（对应后端 MenuName） */
  menuName: string
  /** 菜单编码（对应后端 MenuCode） */
  menuCode: string
  /** 菜单路径（对应后端 Path） */
  path?: string
  /** 菜单类型（0=目录，1=菜单，2=按钮，对应后端 MenuType） */
  menuType: number
}
