import request from '@/api/request'
import type { LoginParams, LoginResponse, UserInfo } from '@/types/identity/auth'

const authUrl = '/api/TaktAuth'

interface OAuthTokenResponse {
  access_token: string
  token_type: string
  expires_in: number
  refresh_token?: string
}

interface OAuthErrorPayload {
  error?: string
  error_description?: string
}

function getCsrfTokenFromCookie(): string | null {
  const CSRF_TOKEN_COOKIE = 'CSRF-Token'
  if (typeof document === 'undefined') return null
  const cookies = document.cookie.split(';')
  for (const cookie of cookies) {
    const [name, value] = cookie.trim().split('=')
    if (name === CSRF_TOKEN_COOKIE) {
      return decodeURIComponent(value)
    }
  }
  return null
}

function getErrorMessage(error: unknown, fallback: string): string {
  if (typeof error === 'object' && error !== null && 'response' in error) {
    const response = (error as { response?: { data?: OAuthErrorPayload } }).response
    const errorData = response?.data
    if (errorData?.error_description) return errorData.error_description
    if (errorData?.error) return errorData.error
  }
  if (error instanceof Error && error.message) return error.message
  return fallback
}

function createErrorWithCause(message: string, cause: unknown): Error {
  const err = new Error(message)
  ;(err as Error & { cause?: unknown }).cause = cause
  return err
}

async function ensureCsrfToken(axios: typeof import('axios').default): Promise<string> {
  // 获取 CSRF Token，如果不存在则先发送 GET 请求获取
  let csrfToken = getCsrfTokenFromCookie()
  if (!csrfToken) {
    logger.warn('[CSRF] CSRF Token 不存在，正在获取...')
    try {
      await axios.get(`${authUrl}/userinfo`, {
        baseURL: '',
        withCredentials: true
      })
    } catch (error: unknown) {
      const status = (error as { response?: { status?: number } })?.response?.status
      if (status !== 401) {
        logger.warn('[CSRF] 获取 CSRF Token 失败:', error)
      }
    }
    csrfToken = getCsrfTokenFromCookie()
  }

  if (!csrfToken) {
    throw new Error('安全验证失败，请刷新页面重试')
  }
  return csrfToken
}

// 登录
export async function login(params: LoginParams): Promise<LoginResponse> {
  // OpenIddict token 端点需要 application/x-www-form-urlencoded 格式
  const formData = new URLSearchParams()
  formData.append('grant_type', 'password')
  formData.append('client_id', 'takt-web-client')
  formData.append('username', params.username)
  formData.append('password', params.password)

  try {
    const axios = (await import('axios')).default
    const CSRF_TOKEN_HEADER = 'X-CSRF-Token'
    const csrfToken = await ensureCsrfToken(axios)
    const headers: Record<string, string> = {
      'Content-Type': 'application/x-www-form-urlencoded',
      [CSRF_TOKEN_HEADER]: csrfToken
    }

    const response = await axios.post<OAuthTokenResponse>(
      `${authUrl}/connect/token`,
      formData.toString(),
      {
        baseURL: '',
        headers,
        withCredentials: true
      }
    )

    if (import.meta.env.DEV) {
      logger.debug('[Login] 响应数据:', response.data)
      logger.debug('[Login] 响应状态:', response.status)
      logger.debug('[Login] 响应头:', response.headers)
    }

    const token = response.data?.access_token
    if (!token) {
      logger.error('[Login] Token 未找到，响应数据:', response.data)
      throw new Error('登录失败：未获取到访问令牌')
    }

    const userInfoResponse = await axios.get<UserInfo>(
      `${authUrl}/userinfo`,
      {
        baseURL: '',
        headers: {
          Authorization: `Bearer ${token}`
        },
        withCredentials: true
      }
    )

    return {
      token,
      refreshToken: response.data?.refresh_token,
      tokenType: response.data?.token_type,
      expiresIn: response.data?.expires_in,
      userInfo: userInfoResponse.data
    }
  } catch (error: unknown) {
    throw createErrorWithCause(getErrorMessage(error, '登录失败'), error)
  }
}

/**
 * 获取用户信息
 * 对应后端：GetUserInfoAsync
 */
export function getUserInfo(): Promise<UserInfo> {
  return request<UserInfo>({
    url: `${authUrl}/userinfo`,
    method: 'get'
  }) as unknown as Promise<UserInfo>
}

/**
 * 刷新访问令牌
 * 对应后端：RefreshTokenAsync
 */
export function refreshToken(refreshToken: string): Promise<LoginResponse> {
  // 使用 OpenIddict 的刷新令牌流程
  const formData = new URLSearchParams()
  formData.append('grant_type', 'refresh_token')
  formData.append('client_id', 'takt-web-client')
  formData.append('refresh_token', refreshToken)

  return (async () => {
    try {
      const axios = (await import('axios')).default
      const csrfToken = getCsrfTokenFromCookie()
      const headers: Record<string, string> = {
        'Content-Type': 'application/x-www-form-urlencoded'
      }

      if (csrfToken) {
        headers['X-CSRF-Token'] = csrfToken
      }

      const response = await axios.post<OAuthTokenResponse>(
        `${authUrl}/connect/token`,
        formData.toString(),
        {
          baseURL: '',
          headers,
          withCredentials: true
        }
      )

      const token = response.data?.access_token
      if (!token) {
        throw new Error('刷新令牌失败：未获取到访问令牌')
      }

      const userInfoResponse = await axios.get<UserInfo>(
        `${authUrl}/userinfo`,
        {
          baseURL: '',
          headers: {
            Authorization: `Bearer ${token}`
          },
          withCredentials: true
        }
      )

      return {
        token,
        refreshToken: response.data?.refresh_token || refreshToken,
        tokenType: response.data?.token_type,
        expiresIn: response.data?.expires_in,
        userInfo: userInfoResponse.data
      }
    } catch (error: unknown) {
      throw createErrorWithCause(getErrorMessage(error, '刷新令牌失败'), error)
    }
  })()
}

// 登出
export function logout(refreshToken: string) {
  return request({
    url: `${authUrl}/logout`,
    method: 'post',
    data: refreshToken // 后端需要接收字符串类型的 refreshToken
  })
}
