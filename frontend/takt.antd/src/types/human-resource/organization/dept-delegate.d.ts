// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/organization/dept-delegate
// 文件名称：dept-delegate.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：dept-delegate相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * DeptDelegate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptDelegateDto）
 */
export interface DeptDelegate extends TaktEntityBase {
  /** 对应后端字段 deptDelegateId */
  deptDelegateId: string
  /** 对应后端字段 deptId */
  deptId: string
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
 * DeptDelegateQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptDelegateQueryDto）
 */
export interface DeptDelegateQuery extends TaktPagedQuery {
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 delegateMode */
  delegateMode?: number
  /** 对应后端字段 delegateEmployeeId */
  delegateEmployeeId?: string
  /** 对应后端字段 delegateDeptId */
  delegateDeptId?: string
  /** 对应后端字段 delegatePostId */
  delegatePostId?: string
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
 * DeptDelegateCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptDelegateCreateDto）
 */
export interface DeptDelegateCreate {
  /** 对应后端字段 deptId */
  deptId: string
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
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * DeptDelegateUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptDelegateUpdateDto）
 */
export interface DeptDelegateUpdate extends DeptDelegateCreate {
  /** 对应后端字段 deptDelegateId */
  deptDelegateId: string
}

/**
 * DeptDelegateSort类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktDeptDelegateSortDto）
 */
export interface DeptDelegateSort {
  /** 对应后端字段 deptDelegateId */
  deptDelegateId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
