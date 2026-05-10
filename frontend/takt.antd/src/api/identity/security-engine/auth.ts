// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity/security-engine
// 文件名称：auth.ts
// 功能描述：认证API，使用 OpenIddict 标准 OAuth2/OIDC 端点
// 路由前缀：api/TaktAuths/connect/token
// ========================================

import axios from 'axios'
import request from '@/api/request'

/** 登录请求DTO */
export interface TaktLoginDto {
  username: string
  password: string
}

/** 刷新令牌请求DTO */
export interface TaktRefreshTokenDto {
  refreshToken: string
}

/** 用户信息DTO */
export interface TaktUserInfoDto {
  userId: string
  userName: string
  nickName: string
  realName: string
  avatar: string
  roles: string[]
  permissions: string[]
  userType: number
  employeeId: string
  // 假日信息
  holidayToday?: boolean
  holidayName?: string
  holidayGreeting?: string
  holidayQuote?: string
  holidayTheme?: string
}

/** 登录响应DTO */
export interface TaktLoginResponseDto {
  token: string
  refreshToken: string
  expiresIn: number
  tokenType: string
  userInfo?: TaktUserInfoDto
}

// ========================================
// 认证API - 使用 OpenIddict 标准端点
// ========================================

/**
 * 用户登录
 * 使用 OpenIddict 标准密码流程 (Resource Owner Password Credentials Grant)
 * 对应后端：TaktAuthsController.ExchangeAsync()
 */
export async function login(data: TaktLoginDto): Promise<TaktLoginResponseDto> {
  const params = new URLSearchParams()
  params.append('grant_type', 'password')
  params.append('client_id', 'takt-web-client')
  params.append('username', data.username)
  params.append('password', data.password)
  params.append('scope', 'openid profile email roles offline_access')
  
  const response = await axios.post('/api/TaktAuths/connect/token', params, {
    headers: { 
      'Content-Type': 'application/x-www-form-urlencoded'
    }
  })
  
  // OpenIddict 返回格式转换为前端标准格式
  return {
    token: response.data.access_token,
    refreshToken: response.data.refresh_token,
    expiresIn: response.data.expires_in,
    tokenType: response.data.token_type
  }
}

/**
 * 刷新访问令牌
 * 使用 OpenIddict 标准刷新令牌流程 (Refresh Token Grant)
 * 对应后端：TaktAuthsController.ExchangeAsync()
 */
export async function refreshToken(data: TaktRefreshTokenDto): Promise<TaktLoginResponseDto> {
  const params = new URLSearchParams()
  params.append('grant_type', 'refresh_token')
  params.append('client_id', 'takt-web-client')
  params.append('refresh_token', data.refreshToken)
  params.append('scope', 'openid profile email roles offline_access')
  
  const response = await axios.post('/api/TaktAuths/connect/token', params, {
    headers: { 
      'Content-Type': 'application/x-www-form-urlencoded'
    }
  })
  
  return {
    token: response.data.access_token,
    refreshToken: response.data.refresh_token,
    expiresIn: response.data.expires_in,
    tokenType: response.data.token_type
  }
}

/**
 * 用户登出
 * 对应后端：TaktAuthsController.LogoutAsync()
 * 注意：登出是业务逻辑端点，用于清理登录日志和在线记录
 */
export function logout(refreshToken: string): Promise<string> {
  return request({
    url: '/api/TaktAuths/logout',
    method: 'post',
    data: refreshToken
  })
}

/**
 * 获取当前用户信息
 * 对应后端：TaktAuthsController.GetUserInfoAsync()
 * 需要认证
 */
export function getUserInfo(): Promise<TaktUserInfoDto> {
  return request({
    url: '/api/TaktAuths/userinfo',
    method: 'get'
  })
}