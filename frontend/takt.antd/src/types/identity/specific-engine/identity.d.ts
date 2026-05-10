// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/identity/specific-engine/identity
// 文件名称：identity.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：identity相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * MenuIsCache类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktMenuIsCacheDto）
 */
export interface MenuIsCache {
  /** 对应后端字段 menuId */
  menuId: string
  /** 对应后端字段 isCache */
  isCache: number
}

/**
 * MenuVisible类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktMenuVisibleDto）
 */
export interface MenuVisible {
  /** 对应后端字段 menuId */
  menuId: string
  /** 对应后端字段 isVisible */
  isVisible: number
}

/**
 * Role类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktRoleDto）
 */
export interface Role {
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 roleName */
  roleName: string
  /** 对应后端字段 roleCode */
  roleCode: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 roleStatus */
  roleStatus: number
}

/**
 * RoleAssignDepts类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktRoleAssignDeptsDto）
 */
export interface RoleAssignDepts {
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 deptIds */
  deptIds: string[]
}

/**
 * RoleAssignMenus类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktRoleAssignMenusDto）
 */
export interface RoleAssignMenus {
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 menuIds */
  menuIds: string[]
}

/**
 * RoleCreate类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktRoleCreateDto）
 */
export interface RoleCreate {
  /** 对应后端字段 roleName */
  roleName: string
  /** 对应后端字段 roleCode */
  roleCode: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 roleStatus */
  roleStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * RoleMenu类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktRoleMenuDto）
 */
export interface RoleMenu {
  /** 对应后端字段 roleMenuId */
  roleMenuId: string
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 menuId */
  menuId: string
}

/**
 * RoleUpdate类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktRoleUpdateDto）
 */
export interface RoleUpdate extends RoleCreate {
  /** 对应后端字段 roleId */
  roleId: string
}

/**
 * TenantQuery类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktTenantQueryDto）
 */
export interface TenantQuery extends TaktPagedQuery {
  /** 对应后端字段 tenantName */
  tenantName?: string
  /** 对应后端字段 tenantCode */
  tenantCode?: string
  /** 对应后端字段 allowedConfigIds */
  allowedConfigIds?: string
  /** 对应后端字段 subscriptionStartTime */
  subscriptionStartTime?: string
  /** 对应后端字段 subscriptionStartTimeStart */
  subscriptionStartTimeStart?: string
  /** 对应后端字段 subscriptionStartTimeEnd */
  subscriptionStartTimeEnd?: string
  /** 对应后端字段 subscriptionEndTime */
  subscriptionEndTime?: string
  /** 对应后端字段 subscriptionEndTimeStart */
  subscriptionEndTimeStart?: string
  /** 对应后端字段 subscriptionEndTimeEnd */
  subscriptionEndTimeEnd?: string
  /** 对应后端字段 tenantStatus */
  tenantStatus?: number
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
 * User类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserDto）
 */
export interface User {
  /** 对应后端字段 userId */
  userId: string
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
  /** 对应后端字段 loginCount */
  loginCount: number
  /** 对应后端字段 lockReason */
  lockReason?: string
  /** 对应后端字段 lockTime */
  lockTime?: string
  /** 对应后端字段 lockBy */
  lockBy?: string
  /** 对应后端字段 errorCount */
  errorCount: number
  /** 对应后端字段 errorLimit */
  errorLimit: number
  /** 对应后端字段 userStatus */
  userStatus: number
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 employee */
  employee?: unknown
}

/**
 * UserAssignDepts类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserAssignDeptsDto）
 */
export interface UserAssignDepts {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 deptIds */
  deptIds: string[]
}

/**
 * UserAssignPosts类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserAssignPostsDto）
 */
export interface UserAssignPosts {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 postIds */
  postIds: string[]
}

/**
 * UserAssignRoles类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserAssignRolesDto）
 */
export interface UserAssignRoles {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 roleIds */
  roleIds: string[]
}

/**
 * UserAssignTenants类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserAssignTenantsDto）
 */
export interface UserAssignTenants {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 tenantIds */
  tenantIds: string[]
}

/**
 * UserAvatarUpdate类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserAvatarUpdateDto）
 */
export interface UserAvatarUpdate {
  /** 对应后端字段 avatar */
  avatar?: string
}

/**
 * UserChangePwd类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserChangePwdDto）
 */
export interface UserChangePwd {
  /** 对应后端字段 oldPassword */
  oldPassword: string
  /** 对应后端字段 newPassword */
  newPassword: string
}

/**
 * UserCreate类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserCreateDto）
 */
export interface UserCreate {
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
  /** 对应后端字段 loginCount */
  loginCount: number
  /** 对应后端字段 lockReason */
  lockReason?: string
  /** 对应后端字段 lockTime */
  lockTime?: string
  /** 对应后端字段 lockBy */
  lockBy?: string
  /** 对应后端字段 errorCount */
  errorCount: number
  /** 对应后端字段 errorLimit */
  errorLimit: number
  /** 对应后端字段 userStatus */
  userStatus: number
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * UserForgotPassword类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserForgotPasswordDto）
 */
export interface UserForgotPassword {
  /** 对应后端字段 userEmail */
  userEmail: string
}

/**
 * UserForgotPasswordResult类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserForgotPasswordResultDto）
 */
export interface UserForgotPasswordResult {
  /** 对应后端字段 success */
  success: boolean
  /** 对应后端字段 code */
  code?: string
}

/**
 * UserResetPwd类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserResetPwdDto）
 */
export interface UserResetPwd {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 newPassword */
  newPassword: string
}

/**
 * UserRole类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserRoleDto）
 */
export interface UserRole {
  /** 对应后端字段 userRoleId */
  userRoleId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 roleId */
  roleId: string
}

/**
 * UserTenant类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserTenantDto）
 */
export interface UserTenant {
  /** 对应后端字段 userTenantId */
  userTenantId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 tenantId */
  tenantId: string
}

/**
 * UserUnlock类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserUnlockDto）
 */
export interface UserUnlock {
  /** 对应后端字段 userStatus */
  userStatus?: number
}

/**
 * UserUpdate类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserUpdateDto）
 */
export interface UserUpdate extends UserCreate {
  /** 对应后端字段 userId */
  userId: string
}
