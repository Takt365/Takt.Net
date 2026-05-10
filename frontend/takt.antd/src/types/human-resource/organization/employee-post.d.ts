// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/organization/employee-post
// 文件名称：employee-post.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-post相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeePost类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktEmployeePostDto）
 */
export interface EmployeePost extends TaktEntityBase {
  /** 对应后端字段 employeePostId */
  employeePostId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 postId */
  postId: string
}

/**
 * EmployeePostQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktEmployeePostQueryDto）
 */
export interface EmployeePostQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 postId */
  postId?: string
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
 * EmployeePostCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktEmployeePostCreateDto）
 */
export interface EmployeePostCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 postId */
  postId: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EmployeePostUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Organization.TaktEmployeePostUpdateDto）
 */
export interface EmployeePostUpdate extends EmployeePostCreate {
  /** 对应后端字段 employeePostId */
  employeePostId: string
}
