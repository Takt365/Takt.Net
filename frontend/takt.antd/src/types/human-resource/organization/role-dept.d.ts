// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/organization/role-dept
// 文件名称：role-dept.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：role-dept相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * RoleDept类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktRoleDeptDto）
 */
export interface RoleDept extends TaktEntityBase {
  /** 对应后端字段 roleDeptId */
  roleDeptId: string
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 deptId */
  deptId: string
}

/**
 * RoleDeptQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktRoleDeptQueryDto）
 */
export interface RoleDeptQuery extends TaktPagedQuery {
  /** 对应后端字段 roleId */
  roleId?: string
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
 * RoleDeptCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktRoleDeptCreateDto）
 */
export interface RoleDeptCreate {
  /** 对应后端字段 roleId */
  roleId: string
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * RoleDeptUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktRoleDeptUpdateDto）
 */
export interface RoleDeptUpdate extends RoleDeptCreate {
  /** 对应后端字段 roleDeptId */
  roleDeptId: string
}
