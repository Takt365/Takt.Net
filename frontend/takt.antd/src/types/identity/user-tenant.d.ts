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
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 realName */
  realName: string
  /** 对应后端字段 tenantId */
  tenantId: string
  /** 对应后端字段 tenantName */
  tenantName: string
  /** 对应后端字段 tenantCode */
  tenantCode: string
  /** 对应后端字段 tenantConfigId */
  tenantConfigId: string
  /** 对应后端字段 tenantStatus */
  tenantStatus: number
  /** 对应后端字段 startTime */
  startTime?: string
  /** 对应后端字段 endTime */
  endTime?: string
}
