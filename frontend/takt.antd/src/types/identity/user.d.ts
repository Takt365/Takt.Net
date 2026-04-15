// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/identity/user
// 文件名称：user.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：用户相关类型定义，对应后端 Takt.Application.Dtos.Identity.TaktUserDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 用户类型（对应后端 Takt.Application.Dtos.Identity.TaktUserDto）
 */
export interface User extends TaktEntityBase {
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 关联员工ID（必填，通过员工选项列表选择） */
  employeeId: string
  /** 用户名（登录名，通过选择员工后带出或手动填写） */
  userName: string
  /** 昵称（用户表展示用，与员工档案姓名独立；旧数据可能缺省） */
  nickName?: string
  /** 用户类型（0=普通用户，1=管理员，2=超级管理员） */
  userType: number
  /** 用户邮箱 */
  userEmail: string
  /** 用户手机号 */
  userPhone: string
  /** 登录次数 */
  loginCount: number
  /** 锁定原因 */
  lockReason?: string
  /** 锁定时间 */
  lockTime?: string
  /** 锁定人（用户名） */
  lockBy?: string
  /** 错误次数限制（登录错误次数上限，超过则锁定） */
  errorLimit: number
  /** 用户状态（1=启用，0=禁用，2=锁定） */
  userStatus: number
  /** 角色ID列表 */
  roleIds?: string[]
  /** 部门ID列表 */
  deptIds?: string[]
  /** 岗位ID列表 */
  postIds?: string[]
  /** 租户ID列表 */
  tenantIds?: string[]
}

/**
 * 用户查询类型（对应后端 Takt.Application.Dtos.Identity.TaktUserQueryDto）
 */
export interface UserQuery extends TaktPagedQuery {
  /** 关键词（在用户名、昵称、邮箱、手机号中模糊查询） */
  keyWords?: string
  /** 用户名 */
  userName?: string
  /** 用户邮箱 */
  userEmail?: string
  /** 用户手机号 */
  userPhone?: string
  /** 用户状态（1=启用，0=禁用，2=锁定） */
  userStatus?: number
}

/**
 * 创建用户类型（对应后端 Takt.Application.Dtos.Identity.TaktUserCreateDto）
 */
export interface UserCreate {
  /** 关联员工ID（必填，通过员工选项列表选择） */
  employeeId: string
  /** 用户名（登录名，选择员工后可带出员工编码或手动填写） */
  userName: string
  /** 昵称（可选，默认空串） */
  nickName?: string
  /** 用户类型（0=普通用户，1=管理员，2=超级管理员） */
  userType: number
  /** 用户邮箱 */
  userEmail: string
  /** 用户手机号 */
  userPhone: string
  /** 密码哈希 */
  passwordHash: string
  /** 用户状态（1=启用，0=禁用，2=锁定，默认值为1=启用） */
  userStatus?: number
  /** 角色ID列表 */
  roleIds?: string[]
  /** 部门ID列表 */
  deptIds?: string[]
  /** 岗位ID列表 */
  postIds?: string[]
  /** 租户ID列表 */
  tenantIds?: string[]
  /** 备注 */
  remark?: string
}

/**
 * 用户维护弹窗中与 `a-form` 绑定的字段（对应 `views/identity/user/components/user-form.vue` 的 `formState`）。
 * `password` 为明文录入；父组件提交新增时映射为 `UserCreate.passwordHash`。
 */
export type UserFormModel = Pick<
  UserCreate,
  'employeeId' | 'userName' | 'nickName' | 'userType' | 'userEmail' | 'userPhone' | 'userStatus' | 'remark'
> & {
  password: string
}

/**
 * 用户维护弹窗中权限分配标签页的模型（对应 `user-form.vue` 的 `permissionState`）。
 */
export interface UserFormPermissionModel {
  roleIds: string[]
  deptIds: string[]
  postIds: string[]
  tenantIds: string[]
}

/**
 * `user-form.vue` 的 `getValues()` 返回值（基础表单字段 + 权限 ID 列表）。
 */
export type UserFormValues = UserFormModel & UserFormPermissionModel

/**
 * 更新用户类型（对应后端 Takt.Application.Dtos.Identity.TaktUserUpdateDto）
 */
export interface UserUpdate extends UserCreate {
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
}

/**
 * 用户状态类型（对应后端 Takt.Application.Dtos.Identity.TaktUserStatusDto）
 */
export interface UserStatus {
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 用户状态（1=启用，0=禁用，2=锁定） */
  userStatus: number
}

/**
 * 重置密码类型（对应后端 Takt.Application.Dtos.Identity.TaktUserResetPwdDto）
 * 注意：重置密码功能只使用配置中的默认密码（PasswordPolicy:DefaultPassword），
 * 前端不需要传递 newPassword 参数，后端会忽略该参数并从配置中读取默认密码
 */
export interface UserResetPwd {
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 新密码（可选，重置密码功能不使用此参数，后端会从配置中读取默认密码） */
  newPassword?: string
}

/**
 * 修改密码类型（对应后端 Takt.Application.Dtos.Identity.TaktUserChangePwdDto）
 */
export interface UserChangePwd {
  /** 旧密码 */
  oldPassword: string
  /** 新密码 */
  newPassword: string
}

/**
 * 修改密码弹窗表单模型（`user-change-password.vue` 的 `formState`）。
 * `confirmPassword` 仅前端校验；`getValues()` 返回 `UserChangePwd`，不含确认字段。
 */
export type UserChangePasswordFormModel = UserChangePwd & {
  /** 确认新密码（须与 newPassword 一致） */
  confirmPassword: string
}

/**
 * 忘记密码类型（对应后端 Takt.Application.Dtos.Identity.TaktUserForgotPasswordDto）
 */
export interface UserForgotPassword {
  /** 用户邮箱 */
  userEmail: string
}

/**
 * 解锁用户类型（对应后端 Takt.Application.Dtos.Identity.TaktUserUnlockDto）
 */
export interface UserUnlock {
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 用户状态（1=启用，0=禁用，2=锁定，解锁时设置为1） */
  userStatus: number
}

/**
 * 用户头像更新类型（对应后端 Takt.Application.Dtos.Identity.TaktUserAvatarUpdateDto）
 */
export interface UserAvatarUpdate {
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 头像 */
  avatar?: string
}

/**
 * 用户分配角色类型（对应后端 Takt.Application.Dtos.Identity.TaktUserAssignRolesDto）
 */
export interface UserAssignRoles {
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 角色ID列表 */
  roleIds: string[]
}

/**
 * 用户分配部门类型（对应后端 Takt.Application.Dtos.Identity.TaktUserAssignDeptsDto）
 */
export interface UserAssignDepts {
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 部门ID列表 */
  deptIds: string[]
}

/**
 * 用户分配岗位类型（对应后端 Takt.Application.Dtos.Identity.TaktUserAssignPostsDto）
 */
export interface UserAssignPosts {
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 岗位ID列表 */
  postIds: string[]
}

/**
 * 用户分配租户类型（对应后端 Takt.Application.Dtos.Identity.TaktUserAssignTenantsDto）
 */
export interface UserAssignTenants {
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 租户ID列表 */
  tenantIds: string[]
}
