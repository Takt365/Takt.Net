// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/identity/auth
// 文件名称：auth.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：认证相关类型定义，对应后端 Takt.Application.Dtos.Identity.TaktAuthDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/**
 * 登录参数（对应后端 Takt.Application.Dtos.Identity.TaktLoginDto）
 */
export interface LoginParams {
  /** 用户名（对应后端 UserName） */
  username: string
  /** 密码（对应后端 Password） */
  password: string
  /** 记住我（对应后端 RememberMe） */
  rememberMe?: boolean
  /** 强制登录：已达设备上限时踢掉已有会话并通知对方（对应后端 force_login） */
  force_login?: boolean
}

/** 已在别处登录时后端返回的已有会话项（对应 TaktExistingSessionDto） */
export interface ExistingSessionItem {
  connectLocation?: string
  connectIp?: string
  deviceType?: string
  browserType?: string
  connectTime?: string
}

/** 登录接口返回“已在其他位置登录”时的结构化错误，供登录页弹窗与强制登录重试 */
export interface AlreadyLoggedInElsewhereError extends Error {
  code: 'already_logged_in_elsewhere'
  error_description: string
  existing_sessions: ExistingSessionItem[]
}

/**
 * 登录响应（对应后端 Takt.Application.Dtos.Identity.TaktLoginResponseDto）
 */
export interface LoginResponse {
  /** 访问令牌（对应后端 AccessToken） */
  token: string
  /** 刷新令牌（对应后端 RefreshToken） */
  refreshToken?: string
  /** 令牌类型（对应后端 TokenType，通常为 "Bearer"） */
  tokenType?: string
  /** 过期时间（秒，对应后端 ExpiresIn） */
  expiresIn?: number
  /** 用户信息（对应后端 UserInfo） */
  userInfo: UserInfo
}

/**
 * 用户信息（对应后端 Takt.Application.Dtos.Identity.TaktUserInfoDto）
 */
export interface UserInfo {
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 用户名（对应后端 UserName） */
  userName: string
  /** 昵称（对应后端 NickName，用户表） */
  nickName?: string
  /** 真实姓名（对应后端 RealName，通常来自关联员工档案） */
  realName: string
  /** 兼容旧 JSON 字段名，含义同 nickName（优先使用 nickName） */
  nickname?: string
  /** 头像（对应后端 Avatar） */
  avatar: string
  /** 角色列表（对应后端 Roles） */
  roles: string[]
  /** 权限列表（对应后端 Permissions） */
  permissions: string[]
  /** 用户类型（对应后端 UserType：0=普通用户，1=管理员，2=超级管理员） */
  userType?: number
  /** 关联员工 ID（对应后端 EmployeeId，发起流程时用于默认申请人） */
  employeeId?: string
  /** 今日是否为假日（access_token 为 JWE 时由 userinfo 返回） */
  holidayToday?: boolean
  /** 假日名称 */
  holidayName?: string
  /** 假日问候语（简短，用于问候语行） */
  holidayGreeting?: string
  /** 假日引用/诗句（用于引用区展示） */
  holidayQuote?: string
  /** 假日主题（对应 themeColorMap 的 key） */
  holidayTheme?: string
}

/**
 * 刷新令牌参数（对应后端 Takt.Application.Dtos.Identity.TaktRefreshTokenDto）
 */
export interface RefreshTokenParams {
  /** 刷新令牌（对应后端 RefreshToken） */
  refreshToken: string
}