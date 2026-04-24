// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/identity/user
// 文件名称：user.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：user相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * User类型（对应后端 Takt.Application.Dtos.Identity.TaktUserDto）
 */
export interface User extends TaktEntityBase {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 nickName */
  nickName: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 userType */
  userType: number
  /** 对应后端字段 userEmail */
  userEmail: string
  /** 对应后端字段 userPhone */
  userPhone: string
  /** 对应后端字段 loginCount */
  loginCount: number
  /** 对应后端字段 lockReason */
  lockReason?: string
  /** 对应后端字段 lockTime */
  lockTime?: string
  /** 对应后端字段 lockBy */
  lockBy?: string
  /** 对应后端字段 errorLimit */
  errorLimit: number
  /** 对应后端字段 userStatus */
  userStatus: number
  /** 对应后端字段 roleIds */
  roleIds?: string[]
  /** 对应后端字段 deptIds */
  deptIds?: string[]
  /** 对应后端字段 postIds */
  postIds?: string[]
  /** 对应后端字段 tenantIds */
  tenantIds?: string[]
}

/**
 * UserQuery类型（对应后端 Takt.Application.Dtos.Identity.TaktUserQueryDto）
 */
export interface UserQuery extends TaktPagedQuery {
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 userEmail */
  userEmail?: string
  /** 对应后端字段 userPhone */
  userPhone?: string
  /** 对应后端字段 userStatus */
  userStatus?: number
}

/**
 * UserCreate类型（对应后端 Takt.Application.Dtos.Identity.TaktUserCreateDto）
 */
export interface UserCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 nickName */
  nickName: string
  /** 对应后端字段 userType */
  userType: number
  /** 对应后端字段 userEmail */
  userEmail: string
  /** 对应后端字段 userPhone */
  userPhone: string
  /** 对应后端字段 passwordHash */
  passwordHash: string
  /** 对应后端字段 userStatus */
  userStatus: number
  /** 对应后端字段 roleIds */
  roleIds?: string[]
  /** 对应后端字段 deptIds */
  deptIds?: string[]
  /** 对应后端字段 postIds */
  postIds?: string[]
  /** 对应后端字段 tenantIds */
  tenantIds?: string[]
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * UserUpdate类型（对应后端 Takt.Application.Dtos.Identity.TaktUserUpdateDto）
 */
export interface UserUpdate extends UserCreate {
  /** 对应后端字段 userId */
  userId: string
}

/**
 * UserStatus类型（对应后端 Takt.Application.Dtos.Identity.TaktUserStatusDto）
 */
export interface UserStatus {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userStatus */
  userStatus: number
}

/**
 * UserAssignDepts类型（对应后端 Takt.Application.Dtos.Identity.TaktUserAssignDeptsDto）
 */
export interface UserAssignDepts {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 deptIds */
  deptIds: string[]
}

/**
 * UserAssignPosts类型（对应后端 Takt.Application.Dtos.Identity.TaktUserAssignPostsDto）
 */
export interface UserAssignPosts {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 postIds */
  postIds: string[]
}

/**
 * UserAssignRoles类型（对应后端 Takt.Application.Dtos.Identity.TaktUserAssignRolesDto）
 */
export interface UserAssignRoles {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 roleIds */
  roleIds: string[]
}

/**
 * UserAssignTenants类型（对应后端 Takt.Application.Dtos.Identity.TaktUserAssignTenantsDto）
 */
export interface UserAssignTenants {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 tenantIds */
  tenantIds: string[]
}

/**
 * UserAvatarUpdate类型（对应后端 Takt.Application.Dtos.Identity.TaktUserAvatarUpdateDto）
 */
export interface UserAvatarUpdate {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 avatar */
  avatar?: string
}

/**
 * UserChangePwd类型（对应后端 Takt.Application.Dtos.Identity.TaktUserChangePwdDto）
 */
export interface UserChangePwd {
  /** 对应后端字段 oldPassword */
  oldPassword: string
  /** 对应后端字段 newPassword */
  newPassword: string
}

/**
 * UserForgotPassword类型（对应后端 Takt.Application.Dtos.Identity.TaktUserForgotPasswordDto）
 */
export interface UserForgotPassword {
  /** 对应后端字段 userEmail */
  userEmail: string
}

/**
 * UserForgotPasswordResult类型（对应后端 Takt.Application.Dtos.Identity.TaktUserForgotPasswordResultDto）
 */
export interface UserForgotPasswordResult {
  /** 对应后端字段 success */
  success: boolean
  /** 对应后端字段 code */
  code?: string
}

/**
 * UserResetPwd类型（对应后端 Takt.Application.Dtos.Identity.TaktUserResetPwdDto）
 */
export interface UserResetPwd {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 newPassword */
  newPassword: string
}

/**
 * UserUnlock类型（对应后端 Takt.Application.Dtos.Identity.TaktUserUnlockDto）
 */
export interface UserUnlock {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userStatus */
  userStatus: number
}
