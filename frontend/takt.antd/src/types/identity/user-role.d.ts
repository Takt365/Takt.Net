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
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 realName */
  realName: string
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 roleName */
  roleName: string
  /** 对应后端字段 roleCode */
  roleCode: string
}
