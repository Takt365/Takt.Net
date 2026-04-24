// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/organization/dept
// 文件名称：dept.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：dept相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Dept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptDto）
 */
export interface Dept extends TaktEntityBase {
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
  /** 对应后端字段 deptHead */
  deptHead?: string
  /** 对应后端字段 costCenterCode */
  costCenterCode?: string
  /** 对应后端字段 deptCostCenterName */
  deptCostCenterName?: string
  /** 对应后端字段 delegates */
  delegates?: unknown[]
  /** 对应后端字段 deptType */
  deptType: number
  /** 对应后端字段 deptPhone */
  deptPhone?: string
  /** 对应后端字段 deptMail */
  deptMail?: string
  /** 对应后端字段 deptAddr */
  deptAddr?: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 deptStatus */
  deptStatus: number
  /** 对应后端字段 userIds */
  userIds?: string[]
  /** 对应后端字段 roleIds */
  roleIds?: string[]
}

/**
 * DeptQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptQueryDto）
 */
export interface DeptQuery extends TaktPagedQuery {
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 deptCode */
  deptCode?: string
  /** 对应后端字段 parentId */
  parentId?: string
  /** 对应后端字段 deptStatus */
  deptStatus?: number
}

/**
 * DeptCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptCreateDto）
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
  /** 对应后端字段 delegates */
  delegates?: unknown[]
  /** 对应后端字段 deptType */
  deptType: number
  /** 对应后端字段 deptPhone */
  deptPhone?: string
  /** 对应后端字段 deptMail */
  deptMail?: string
  /** 对应后端字段 deptAddr */
  deptAddr?: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 dataScope */
  dataScope: number
  /** 对应后端字段 customScope */
  customScope?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 userIds */
  userIds?: string[]
  /** 对应后端字段 roleIds */
  roleIds?: string[]
}

/**
 * DeptUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptUpdateDto）
 */
export interface DeptUpdate extends DeptCreate {
  /** 对应后端字段 deptId */
  deptId: string
}

/**
 * DeptStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptStatusDto）
 */
export interface DeptStatus {
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 deptStatus */
  deptStatus: number
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
  /** 对应后端字段 orderNum */
  orderNum: number
}

/**
 * DeptTree类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptTreeDto）
 */
export interface DeptTree extends Dept {
  /** 对应后端字段 children */
  children: unknown[]
}
