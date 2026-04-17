import type { IdS } from '@/types/internal/openapi-pick'

export type UserInfo = IdS<'Takt.Application.Dtos.Identity.TaktUserInfoDto'>

/** OAuth 密码模式 + userinfo 组合结果（非单一 swagger schema）。 */
export interface LoginResponse {
  token: string
  refreshToken?: string
  tokenType?: string
  expiresIn?: number
  userInfo: UserInfo
}

/** 登录表单与 OpenAPI TaktLoginDto 字段名不完全一致（username），此处为前端入参约定。 */
export interface LoginParams {
  username: string
  password: string
  rememberMe?: boolean
}
