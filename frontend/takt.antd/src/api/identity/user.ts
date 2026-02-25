// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/identity/user
// 文件名称：user.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：用户相关 API，对应后端 TaktUsersController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  User,
  UserQuery,
  UserCreate,
  UserUpdate,
  UserStatus,
  UserResetPwd,
  UserChangePwd,
  UserUnlock,
  UserAvatarUpdate,
  UserForgotPassword
} from '@/types/identity/user'
import type { UserRole } from '@/types/identity/user-role'
import type { UserDept } from '@/types/humanresource/organization/user-dept'
import type { UserPost } from '@/types/humanresource/organization/user-post'
import type { UserTenant } from '@/types/identity/user-tenant'

// ========================================
// 用户相关 API（按后端控制器顺序）
// ========================================

/**
 * 获取用户列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: UserQuery): Promise<TaktPagedResult<User>> {
  return request({
    url: '/api/TaktUsers/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取用户
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<User> {
  return request({
    url: `/api/TaktUsers/${id}`,
    method: 'get'
  })
}

/**
 * 获取用户选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktUsers/options',
    method: 'get'
  })
}

/**
 * 创建用户
 * 对应后端：CreateAsync
 */
export function create(data: UserCreate): Promise<User> {
  return request({
    url: '/api/TaktUsers',
    method: 'post',
    data
  })
}

/**
 * 更新用户
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: UserUpdate): Promise<User> {
  return request({
    url: `/api/TaktUsers/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除用户
 * 对应后端：DeleteAsync
 */
export function deleteUser(id: string): Promise<void> {
  return request({
    url: `/api/TaktUsers/${id}`,
    method: 'delete'
  })
}

/**
 * 更新用户状态
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: UserStatus): Promise<User> {
  return request({
    url: '/api/TaktUsers/status',
    method: 'put',
    data
  })
}

/**
 * 重置密码
 * 对应后端：ResetPasswordAsync
 */
export function resetPassword(data: UserResetPwd): Promise<void> {
  return request({
    url: '/api/TaktUsers/reset-password',
    method: 'put',
    data
  })
}

/**
 * 修改密码
 * 对应后端：ChangePasswordAsync
 */
export function changePassword(data: UserChangePwd): Promise<void> {
  return request({
    url: '/api/TaktUsers/change-password',
    method: 'put',
    data
  })
}

/**
 * 忘记密码（发送密码重置邮件）
 * 对应后端：ForgotPasswordAsync，返回 success 表示该邮箱已注册且已发送重置邮件
 */
export function forgotPassword(data: UserForgotPassword): Promise<{ success: boolean; code?: string }> {
  return request({
    url: '/api/TaktUsers/forgot-password',
    method: 'post',
    data
  })
}

/**
 * 解锁用户
 * 对应后端：UnlockAsync
 */
export function unlock(data: UserUnlock): Promise<User> {
  return request({
    url: '/api/TaktUsers/unlock',
    method: 'put',
    data
  })
}

/**
 * 更新头像
 * 对应后端：UpdateAvatarAsync
 */
export function updateAvatar(data: UserAvatarUpdate): Promise<User> {
  return request({
    url: '/api/TaktUsers/avatar',
    method: 'put',
    data
  })
}

/**
 * 获取用户角色列表
 * 对应后端：GetUserRoleIdsAsync
 */
export function getUserRoleIds(userId: string): Promise<UserRole[]> {
  return request({
    url: `/api/TaktUsers/${userId}/roles`,
    method: 'get'
  })
}

/**
 * 获取用户部门列表
 * 对应后端：GetUserDeptIdsAsync
 */
export function getUserDeptIds(userId: string): Promise<UserDept[]> {
  return request({
    url: `/api/TaktUsers/${userId}/depts`,
    method: 'get'
  })
}

/**
 * 获取用户岗位列表
 * 对应后端：GetUserPostIdsAsync
 */
export function getUserPostIds(userId: string): Promise<UserPost[]> {
  return request({
    url: `/api/TaktUsers/${userId}/posts`,
    method: 'get'
  })
}

/**
 * 分配用户角色
 * 对应后端：AssignUserRolesAsync
 */
export function assignUserRoles(userId: string, roleIds: string[]): Promise<{ success: boolean }> {
  return request({
    url: `/api/TaktUsers/${userId}/roles`,
    method: 'put',
    data: roleIds
  })
}

/**
 * 分配用户部门
 * 对应后端：AssignUserDeptsAsync
 */
export function assignUserDepts(userId: string, deptIds: string[]): Promise<{ success: boolean }> {
  return request({
    url: `/api/TaktUsers/${userId}/depts`,
    method: 'put',
    data: deptIds
  })
}

/**
 * 分配用户岗位
 * 对应后端：AssignUserPostsAsync
 */
export function assignUserPosts(userId: string, postIds: string[]): Promise<{ success: boolean }> {
  return request({
    url: `/api/TaktUsers/${userId}/posts`,
    method: 'put',
    data: postIds
  })
}

/**
 * 获取用户租户列表
 * 对应后端：GetUserTenantIdsAsync
 */
export function getUserTenantIds(userId: string): Promise<UserTenant[]> {
  return request({
    url: `/api/TaktUsers/${userId}/tenants`,
    method: 'get'
  })
}

/**
 * 分配用户租户
 * 对应后端：AssignUserTenantsAsync
 */
export function assignUserTenants(userId: string, tenantIds: string[]): Promise<{ success: boolean }> {
  return request({
    url: `/api/TaktUsers/${userId}/tenants`,
    method: 'put',
    data: tenantIds
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktUsers/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入用户
 * 对应后端：ImportAsync
 */
export function importUsers(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktUsers/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出用户
 * 对应后端：ExportAsync；fileName 仅传名称不含后缀
 */
export function exportUsers(query: UserQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktUsers/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
