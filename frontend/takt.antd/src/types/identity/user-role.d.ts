// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/identity/user-role
// 文件名称：user-role.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：用户角色关联类型定义，对应后端 Takt.Application.Dtos.Identity.TaktUserRoleDto
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase } from '@/types/common'

/**
 * 用户角色关联类型（对应后端 Takt.Application.Dtos.Identity.TaktUserRoleDto）
 */
export interface UserRole extends TaktEntityBase {
  /** 用户角色关联ID（对应后端 UserRoleId，序列化为string以避免Javascript精度问题） */
  userRoleId: string
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 用户名（对应后端 UserName） */
  userName: string
  /** 用户真实姓名（对应后端 RealName） */
  realName: string
  /** 角色ID（对应后端 RoleId，序列化为string以避免Javascript精度问题） */
  roleId: string
  /** 角色名称（对应后端 RoleName） */
  roleName: string
  /** 角色编码（对应后端 RoleCode） */
  roleCode: string
}
