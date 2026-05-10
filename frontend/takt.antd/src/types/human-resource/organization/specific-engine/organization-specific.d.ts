// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/organization/organization-specific
// 文件名称：organization-specific.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：organization-specific相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Dept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptDto）
 */
export interface Dept {
  /** 对应后端字段 deptHead */
  deptHead?: string
  /** 对应后端字段 deptCostCenterName */
  deptCostCenterName?: string
  /** 对应后端字段 userIds */
  userIds?: string[]
  /** 对应后端字段 roleIds */
  roleIds?: string[]
  /** 对应后端字段 delegates */
  delegates?: unknown[]
}

/**
 * DeptAssignUsers类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptAssignUsersDto）
 */
export interface DeptAssignUsers {
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 userIds */
  userIds: string[]
}

/**
 * DeptCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptCreateDto）
 */
export interface DeptCreate {
  /** 对应后端字段 delegates */
  delegates?: unknown[]
  /** 对应后端字段 userIds */
  userIds?: string[]
  /** 对应后端字段 roleIds */
  roleIds?: string[]
}

/**
 * DeptDelegateItem类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptDelegateItemDto）
 */
export interface DeptDelegateItem {
  /** 对应后端字段 id */
  id?: string
  /** 对应后端字段 delegateMode */
  delegateMode: number
  /** 对应后端字段 delegateEmployeeId */
  delegateEmployeeId?: string
  /** 对应后端字段 delegateDeptId */
  delegateDeptId?: string
  /** 对应后端字段 delegatePostId */
  delegatePostId?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * DeptUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptUpdateDto）
 */
export interface DeptUpdate extends DeptCreate {
}

/**
 * EmployeeDept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktEmployeeDeptDto）
 */
export interface EmployeeDept {
  /** 对应后端字段 employeeName */
  employeeName?: string
  /** 对应后端字段 employeeCode */
  employeeCode?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 deptCode */
  deptCode?: string
}

/**
 * EmployeePost类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktEmployeePostDto）
 */
export interface EmployeePost {
  /** 对应后端字段 employeeName */
  employeeName?: string
  /** 对应后端字段 employeeCode */
  employeeCode?: string
  /** 对应后端字段 postName */
  postName?: string
  /** 对应后端字段 postCode */
  postCode?: string
}

/**
 * Post类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostDto）
 */
export interface Post {
  /** 对应后端字段 postId */
  postId: string
  /** 对应后端字段 postName */
  postName: string
  /** 对应后端字段 postCode */
  postCode: string
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 postCategory */
  postCategory: string
  /** 对应后端字段 postLevel */
  postLevel: number
  /** 对应后端字段 postDuty */
  postDuty?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 postStatus */
  postStatus: number
  /** 对应后端字段 postDelegateIds */
  postDelegateIds?: string[]
}

/**
 * PostAssignUsers类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostAssignUsersDto）
 */
export interface PostAssignUsers {
  /** 对应后端字段 postId */
  postId: string
  /** 对应后端字段 userIds */
  userIds: string[]
}

/**
 * PostCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostCreateDto）
 */
export interface PostCreate {
  /** 对应后端字段 postName */
  postName: string
  /** 对应后端字段 postCode */
  postCode: string
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 postCategory */
  postCategory: string
  /** 对应后端字段 postLevel */
  postLevel: number
  /** 对应后端字段 postDuty */
  postDuty?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 postStatus */
  postStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PostUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktPostUpdateDto）
 */
export interface PostUpdate extends PostCreate {
  /** 对应后端字段 postId */
  postId: string
}

/**
 * RoleDept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktRoleDeptDto）
 */
export interface RoleDept {
  /** 对应后端字段 roleDeptId */
  roleDeptId: string
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 deptId */
  deptId: string
}

/**
 * UserDept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktUserDeptDto）
 */
export interface UserDept {
  /** 对应后端字段 userDeptId */
  userDeptId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 deptId */
  deptId: string
}

/**
 * UserPost类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktUserPostDto）
 */
export interface UserPost {
  /** 对应后端字段 userPostId */
  userPostId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 postId */
  postId: string
}
