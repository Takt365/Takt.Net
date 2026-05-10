// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/identity/user-tenant
// 文件名称：user-tenant.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：user-tenant相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * UserTenant类型（对应后端 Takt.Application.Dtos.Identity.TaktUserTenantDto）
 */
export interface UserTenant extends TaktEntityBase {
  /** 对应后端字段 userTenantId */
  userTenantId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 tenantId */
  tenantId: string
}

/**
 * UserTenantQuery类型（对应后端 Takt.Application.Dtos.Identity.TaktUserTenantQueryDto）
 */
export interface UserTenantQuery extends TaktPagedQuery {
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 tenantId */
  tenantId?: string
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
 * UserTenantCreate类型（对应后端 Takt.Application.Dtos.Identity.TaktUserTenantCreateDto）
 */
export interface UserTenantCreate {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 tenantId */
  tenantId: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * UserTenantUpdate类型（对应后端 Takt.Application.Dtos.Identity.TaktUserTenantUpdateDto）
 */
export interface UserTenantUpdate extends UserTenantCreate {
  /** 对应后端字段 userTenantId */
  userTenantId: string
}
