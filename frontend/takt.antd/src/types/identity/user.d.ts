// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/identity/user
// 文件名称：user.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：user相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * User类型（对应后端 Takt.Application.Dtos.Identity.TaktUserDto）
 */
export interface User extends TaktEntityBase {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 nickName */
  nickName: string
  /** 对应后端字段 userType */
  userType: number
  /** 对应后端字段 userEmail */
  userEmail: string
  /** 对应后端字段 userPhone */
  userPhone: string
  /** 对应后端字段 passwordHash */
  passwordHash: string
  /** 对应后端字段 loginCount */
  loginCount: number
  /** 对应后端字段 lockReason */
  lockReason?: string
  /** 对应后端字段 lockTime */
  lockTime?: string
  /** 对应后端字段 lockBy */
  lockBy?: string
  /** 对应后端字段 errorCount */
  errorCount: number
  /** 对应后端字段 errorLimit */
  errorLimit: number
  /** 对应后端字段 userStatus */
  userStatus: number
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 employee */
  employee?: unknown
}

/**
 * UserQuery类型（对应后端 Takt.Application.Dtos.Identity.TaktUserQueryDto）
 */
export interface UserQuery extends TaktPagedQuery {
  /** 对应后端字段 userName */
  userName?: string
  /** 对应后端字段 nickName */
  nickName?: string
  /** 对应后端字段 userType */
  userType?: number
  /** 对应后端字段 userEmail */
  userEmail?: string
  /** 对应后端字段 userPhone */
  userPhone?: string
  /** 对应后端字段 passwordHash */
  passwordHash?: string
  /** 对应后端字段 loginCount */
  loginCount?: number
  /** 对应后端字段 lockReason */
  lockReason?: string
  /** 对应后端字段 lockTime */
  lockTime?: string
  /** 对应后端字段 lockTimeStart */
  lockTimeStart?: string
  /** 对应后端字段 lockTimeEnd */
  lockTimeEnd?: string
  /** 对应后端字段 lockBy */
  lockBy?: string
  /** 对应后端字段 errorCount */
  errorCount?: number
  /** 对应后端字段 errorLimit */
  errorLimit?: number
  /** 对应后端字段 userStatus */
  userStatus?: number
  /** 对应后端字段 employeeId */
  employeeId?: string
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
 * UserCreate类型（对应后端 Takt.Application.Dtos.Identity.TaktUserCreateDto）
 */
export interface UserCreate {
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 nickName */
  nickName: string
  /** 对应后端字段 userType */
  userType: number
  /** 对应后端字段 userEmail */
  userEmail: string
  /** 对应后端字段 userPhone */
  userPhone: string
  /** 对应后端字段 passwordHash */
  passwordHash: string
  /** 对应后端字段 loginCount */
  loginCount: number
  /** 对应后端字段 lockReason */
  lockReason?: string
  /** 对应后端字段 lockTime */
  lockTime?: string
  /** 对应后端字段 lockBy */
  lockBy?: string
  /** 对应后端字段 errorCount */
  errorCount: number
  /** 对应后端字段 errorLimit */
  errorLimit: number
  /** 对应后端字段 userStatus */
  userStatus: number
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * UserUpdate类型（对应后端 Takt.Application.Dtos.Identity.TaktUserUpdateDto）
 */
export interface UserUpdate extends UserCreate {
  /** 对应后端字段 userId */
  userId: string
}

/**
 * UserStatus类型（对应后端 Takt.Application.Dtos.Identity.TaktUserStatusDto）
 */
export interface UserStatus {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userStatus */
  userStatus: number
}
