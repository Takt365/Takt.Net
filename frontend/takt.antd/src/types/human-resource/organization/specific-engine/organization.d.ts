// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/organization/specific-engine/organization
// 文件名称：organization.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：organization相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Dept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktDeptDto）
 */
export interface Dept {
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 deptName */
  deptName: string
  /** 对应后端字段 deptCode */
  deptCode: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 deptHeadId */
  deptHeadId: string
  /** 对应后端字段 costCenterCode */
  costCenterCode?: string
  /** 对应后端字段 deptType */
  deptType: number
  /** 对应后端字段 deptPhone */
  deptPhone?: string
  /** 对应后端字段 deptMail */
  deptMail?: string
  /** 对应后端字段 deptAddr */
  deptAddr?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 deptStatus */
  deptStatus: number
  /** 对应后端字段 deptDelegates */
  deptDelegates?: unknown[]
}

/**
 * DeptAssignUsers类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktDeptAssignUsersDto）
 */
export interface DeptAssignUsers {
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 userIds */
  userIds: string[]
}

/**
 * DeptCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktDeptCreateDto）
 */
export interface DeptCreate {
  /** 对应后端字段 deptName */
  deptName: string
  /** 对应后端字段 deptCode */
  deptCode: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 deptHeadId */
  deptHeadId: string
  /** 对应后端字段 costCenterCode */
  costCenterCode?: string
  /** 对应后端字段 deptType */
  deptType: number
  /** 对应后端字段 deptPhone */
  deptPhone?: string
  /** 对应后端字段 deptMail */
  deptMail?: string
  /** 对应后端字段 deptAddr */
  deptAddr?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 deptStatus */
  deptStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 deptDelegates */
  deptDelegates?: unknown[]
}

/**
 * DeptDelegateItem类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktDeptDelegateItemDto）
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
 * DeptUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktDeptUpdateDto）
 */
export interface DeptUpdate extends DeptCreate {
  /** 对应后端字段 deptId */
  deptId: string
}

/**
 * EmployeeDept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktEmployeeDeptDto）
 */
export interface EmployeeDept {
  /** 对应后端字段 employeeDeptId */
  employeeDeptId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 deptId */
  deptId: string
}

/**
 * EmployeePost类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktEmployeePostDto）
 */
export interface EmployeePost {
  /** 对应后端字段 employeePostId */
  employeePostId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 postId */
  postId: string
}

/**
 * Post类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktPostDto）
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
  /** 对应后端字段 postDelegates */
  postDelegates?: unknown[]
}

/**
 * PostAssignUsers类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktPostAssignUsersDto）
 */
export interface PostAssignUsers {
  /** 对应后端字段 postId */
  postId: string
  /** 对应后端字段 userIds */
  userIds: string[]
}

/**
 * PostCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktPostCreateDto）
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
  /** 对应后端字段 postDelegates */
  postDelegates?: unknown[]
}

/**
 * PostUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktPostUpdateDto）
 */
export interface PostUpdate extends PostCreate {
  /** 对应后端字段 postId */
  postId: string
}

/**
 * RoleDept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktRoleDeptDto）
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
 * UserDept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktUserDeptDto）
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
 * UserPost类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.SpecificEngine.TaktUserPostDto）
 */
export interface UserPost {
  /** 对应后端字段 userPostId */
  userPostId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 postId */
  postId: string
}
