// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/tenant/user-tenant
// 文件名称：user-tenant.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：用户租户关联类型定义，对应后端 Takt.Application.Dtos.Tenant.TaktUserTenantDto
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase } from '@/types/common'

/**
 * 用户租户关联类型（对应后端 Takt.Application.Dtos.Tenant.TaktUserTenantDto）
 * 用于获取租户用户列表，即根据租户ID获取该租户下的用户列表
 */
export interface UserTenant extends TaktEntityBase {
  /** 用户租户关联ID（对应后端 UserTenantId，序列化为string以避免Javascript精度问题） */
  userTenantId: string
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 用户名（对应后端 UserName） */
  userName: string
  /** 用户真实姓名（对应后端 RealName） */
  realName: string
  /** 租户ID（对应后端 TenantId，序列化为string以避免Javascript精度问题） */
  tenantId: string
  /** 租户名称（对应后端 TenantName） */
  tenantName: string
  /** 租户编码（对应后端 TenantCode） */
  tenantCode: string
  /** 租户配置ID（对应后端 TenantConfigId） */
  tenantConfigId: string
  /** 租户状态（0=启用，1=禁用，对应后端 TenantStatus） */
  tenantStatus: number
  /** 订阅开始时间（对应后端 StartTime） */
  startTime?: string
  /** 订阅结束时间（对应后端 EndTime） */
  endTime?: string
}
