// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/organization/user-dept
// 文件名称：user-dept.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：user-dept相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * UserDept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktUserDeptDto）
 */
export interface UserDept extends TaktEntityBase {
  /** 对应后端字段 userDeptId */
  userDeptId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 deptId */
  deptId: string
}

/**
 * UserDeptQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktUserDeptQueryDto）
 */
export interface UserDeptQuery extends TaktPagedQuery {
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 deptId */
  deptId?: string
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
 * UserDeptCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktUserDeptCreateDto）
 */
export interface UserDeptCreate {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * UserDeptUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktUserDeptUpdateDto）
 */
export interface UserDeptUpdate extends UserDeptCreate {
  /** 对应后端字段 userDeptId */
  userDeptId: string
}
