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
  /** 对应后端字段 roleName */
  roleName: string
  /** 对应后端字段 roleCode */
  roleCode: string
  /** 对应后端字段 deptId */
  deptId: string
  /** 对应后端字段 deptName */
  deptName: string
  /** 对应后端字段 deptCode */
  deptCode: string
}
