// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity/specific-engine
// 文件名称：identity.ts
// 功能描述：身份专用 API，对应后端 Takt.WebApi.Controllers.Identity.SpecificEngine.TaktIdentities
// ========================================

import request from '@/api/request'
import type { TaktTreeSelectOption } from '@/types/common'
import type { MenuTree } from '@/types/identity/menu'
import type {
  TaktUserResetPwdDto,
  TaktUserChangePwdDto,
  TaktUserForgotPasswordDto,
  TaktUserForgotPasswordResultDto,
  TaktUserUnlockDto,
  TaktUserAvatarUpdateDto,
  TaktUserDto,
  TaktUserRoleDto,
  TaktUserDeptDto,
  TaktUserPostDto,
  TaktUserTenantDto
} from '@/types/identity/specific-engine/identity'

// ========================================
// 身份专用 API（按后端控制器顺序）
// ========================================
const identityUrl = 'api/TaktIdentities';

/**
 * 获取菜单树形选项（含按钮 MenuType=2），用于角色分配菜单等需勾选按钮权限的场景。
 * 对应后端：GetMenuTreeOptionsWithButtonAsync
 */
export function getMenuTreeOptionsWithButton(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: `${identityUrl}/menu-tree-options-with-button`,
    method: 'get'
  })
}

/**
 * 获取模块名称用目录树（仅 MenuType=0），用于代码生成中的模块列表。
 * 对应后端：GetMenuDirectoryTreeAsync
 */
export function getMenuDirectoryTree(): Promise<MenuTree[]> {
  return request({
    url: `${identityUrl}/menu-directory-tree`,
    method: 'get'
  })
}

/**
 * 获取当前用户可见的菜单树（按权限过滤）。
 * 对应后端：GetCurrentUserMenuTreeAsync
 */
export function getCurrentUserMenuTree(): Promise<MenuTree[]> {
  return request({
    url: `${identityUrl}/current-user-menu-tree`,
    method: 'get'
  })
}

// ========================================
// 用户管理 API
// ========================================

/**
 * 重置密码（管理员操作）
 * 对应后端：ResetPasswordAsync
 */
export function resetPassword(data: TaktUserResetPwdDto): Promise<void> {
  return request({
    url: `${identityUrl}/reset-password`,
    method: 'post',
    data
  })
}

/**
 * 修改密码（当前用户操作）
 * 对应后端：ChangePasswordAsync
 */
export function changePassword(data: TaktUserChangePwdDto): Promise<void> {
  return request({
    url: `${identityUrl}/change-password`,
    method: 'post',
    data
  })
}

/**
 * 忘记密码（发送密码重置邮件）
 * 对应后端：ForgotPasswordAsync
 */
export function forgotPassword(data: TaktUserForgotPasswordDto): Promise<TaktUserForgotPasswordResultDto> {
  return request({
    url: `${identityUrl}/forgot-password`,
    method: 'post',
    data
  })
}

/**
 * 解锁用户
 * 对应后端：UnlockUserAsync
 */
export function unlockUser(data: TaktUserUnlockDto): Promise<TaktUserDto> {
  return request({
    url: `${identityUrl}/unlock-user`,
    method: 'post',
    data
  })
}

/**
 * 更新头像
 * 对应后端：UpdateAvatarAsync
 */
export function updateAvatar(data: TaktUserAvatarUpdateDto): Promise<TaktUserDto> {
  return request({
    url: `${identityUrl}/update-avatar`,
    method: 'post',
    data
  })
}

/**
 * 获取用户角色列表
 * 对应后端：GetUserRolesAsync
 */
export function getUserRoles(userId: number): Promise<TaktUserRoleDto[]> {
  return request({
    url: `${identityUrl}/user/${userId}/roles`,
    method: 'get'
  })
}

/**
 * 获取用户部门列表
 * 对应后端：GetUserDeptsAsync
 */
export function getUserDepts(userId: number): Promise<TaktUserDeptDto[]> {
  return request({
    url: `${identityUrl}/user/${userId}/depts`,
    method: 'get'
  })
}

/**
 * 获取用户岗位列表
 * 对应后端：GetUserPostsAsync
 */
export function getUserPosts(userId: number): Promise<TaktUserPostDto[]> {
  return request({
    url: `${identityUrl}/user/${userId}/posts`,
    method: 'get'
  })
}

/**
 * 获取用户租户列表
 * 对应后端：GetUserTenantsAsync
 */
export function getUserTenants(userId: number): Promise<TaktUserTenantDto[]> {
  return request({
    url: `${identityUrl}/user/${userId}/tenants`,
    method: 'get'
  })
}

/**
 * 分配用户角色
 * 对应后端：AssignUserRolesAsync
 */
export function assignUserRoles(userId: number, roleIds: number[]): Promise<boolean> {
  return request({
    url: `${identityUrl}/user/${userId}/assign-roles`,
    method: 'post',
    data: roleIds
  })
}

/**
 * 分配用户部门
 * 对应后端：AssignUserDeptsAsync
 */
export function assignUserDepts(userId: number, deptIds: number[]): Promise<boolean> {
  return request({
    url: `${identityUrl}/user/${userId}/assign-depts`,
    method: 'post',
    data: deptIds
  })
}

/**
 * 分配用户岗位
 * 对应后端：AssignUserPostsAsync
 */
export function assignUserPosts(userId: number, postIds: number[]): Promise<boolean> {
  return request({
    url: `${identityUrl}/user/${userId}/assign-posts`,
    method: 'post',
    data: postIds
  })
}

/**
 * 分配用户租户
 * 对应后端：AssignUserTenantsAsync
 */
export function assignUserTenants(userId: number, tenantIds: number[]): Promise<boolean> {
  return request({
    url: `${identityUrl}/user/${userId}/assign-tenants`,
    method: 'post',
    data: tenantIds
  })
}

// ========================================
// 统计分析 API
// ========================================

/**
 * 获取用户总数统计
 * 对应后端：GetUserCountAsync
 */
export function getUserCount(): Promise<number> {
  return request({
    url: `${identityUrl}/stats/user-count`,
    method: 'get'
  })
}

