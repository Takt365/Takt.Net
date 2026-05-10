// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/identity/specific-engine/auth
// 文件名称：auth.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：auth相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Login类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktLoginDto）
 */
export interface Login {
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 password */
  password: string
  /** 对应后端字段 rememberMe */
  rememberMe: boolean
}

/**
 * LoginResponse类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktLoginResponseDto）
 */
export interface LoginResponse {
  /** 对应后端字段 accessToken */
  accessToken: string
  /** 对应后端字段 refreshToken */
  refreshToken: string
  /** 对应后端字段 tokenType */
  tokenType: string
  /** 对应后端字段 expiresIn */
  expiresIn: number
  /** 对应后端字段 userInfo */
  userInfo?: unknown
}

/**
 * RefreshToken类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktRefreshTokenDto）
 */
export interface RefreshToken {
  /** 对应后端字段 refreshToken */
  refreshToken: string
}

/**
 * UserInfo类型（对应后端 Takt.Application.Dtos.Identity.SpecificEngine.TaktUserInfoDto）
 */
export interface UserInfo {
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 nickName */
  nickName: string
  /** 对应后端字段 realName */
  realName: string
  /** 对应后端字段 avatar */
  avatar: string
  /** 对应后端字段 roles */
  roles: string[]
  /** 对应后端字段 permissions */
  permissions: string[]
  /** 对应后端字段 userType */
  userType: number
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 holidayToday */
  holidayToday?: boolean
  /** 对应后端字段 holidayName */
  holidayName?: string
  /** 对应后端字段 holidayGreeting */
  holidayGreeting?: string
  /** 对应后端字段 holidayQuote */
  holidayQuote?: string
  /** 对应后端字段 holidayTheme */
  holidayTheme?: string
}
