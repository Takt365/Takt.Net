import request from '../request'
import type {
  LoginParams,
  LoginResponse,
  UserInfo,
  ExistingSessionItem,
  AlreadyLoggedInElsewhereError
} from '@/types/identity/auth'

// 登录
export function login(params: LoginParams) {
  // OpenIddict token 端点需要 application/x-www-form-urlencoded 格式
  // 将参数转换为 URL-encoded 格式
  const formData = new URLSearchParams()
  formData.append('grant_type', 'password')
  formData.append('client_id', 'takt-web-client') // 使用后端配置的客户端ID
  formData.append('username', params.username)
  // 注意：密码直接发送原始值，不做任何加密处理
  // 后端使用 PBKDF2 算法验证密码（TaktEncryptHelper.VerifyPassword）
  // 密码通过 HTTPS 传输，确保传输安全
  formData.append('password', params.password)
  if (params.force_login === true) {
    formData.append('force_login', 'true')
  }

  // 直接使用 axios 发送请求，绕过 request 拦截器的响应处理
  // 因为 OpenIddict 返回的是 OAuth2 标准格式，不是 { code: 200, data: ... } 格式
  return new Promise<LoginResponse>(async (resolve, reject) => {
    try {
      const axios = (await import('axios')).default
      
      // 从 Cookie 中读取 CSRF Token（与 request.ts 中的逻辑一致）
      const CSRF_TOKEN_COOKIE = 'CSRF-Token'
      const CSRF_TOKEN_HEADER = 'X-CSRF-Token'
      function getCsrfTokenFromCookie(): string | null {
        if (typeof document === 'undefined') {
          return null
        }
        const cookies = document.cookie.split(';')
        for (const cookie of cookies) {
          const [name, value] = cookie.trim().split('=')
          if (name === CSRF_TOKEN_COOKIE) {
            return decodeURIComponent(value)
          }
        }
        return null
      }
      
      // 获取 CSRF Token，如果不存在则先发送 GET 请求获取
      let csrfToken = getCsrfTokenFromCookie()
      if (!csrfToken) {
        logger.warn('[CSRF] CSRF Token 不存在，正在获取...')
        try {
          // 发送 GET 请求以获取 CSRF Token
          await axios.get('/api/TaktAuth/userinfo', {
            baseURL: '',
            withCredentials: true
          })
          // 重新获取 CSRF Token
          csrfToken = getCsrfTokenFromCookie()
        } catch (error: any) {
          // 忽略 401 错误（未登录是正常的），只要请求发送成功，CSRF Token 就会被设置
          if (error.response?.status !== 401) {
            logger.warn('[CSRF] 获取 CSRF Token 失败:', error)
          } else {
            // 401 错误是正常的，重新获取 CSRF Token
            csrfToken = getCsrfTokenFromCookie()
          }
        }
      }
      
      const headers: Record<string, string> = {
        'Content-Type': 'application/x-www-form-urlencoded'
      }
      
      // 添加 CSRF Token 头（POST 请求需要）
      if (csrfToken) {
        headers[CSRF_TOKEN_HEADER] = csrfToken
      } else {
        logger.error('[CSRF] CSRF Token 未找到，请求可能被拒绝')
        reject(new Error('安全验证失败，请刷新页面重试'))
        return
      }
      
      const response = await axios.post<{
        access_token: string
        token_type: string
        expires_in: number
        refresh_token?: string
      }>(
        '/api/TaktAuth/connect/token', // 与后端路由一致：api/TaktAuth/connect/token
        formData.toString(),
        {
          baseURL: '', // 不使用 baseURL，因为 URL 已经包含完整路径
          headers,
          withCredentials: true
        }
      )

      const data = response.data as Record<string, unknown> | null | undefined
      // 已在其他位置登录：必须先于 token 判断。后端返回 200 + error/error_description/existing_sessions，无 access_token
      if (data && String(data.error) === 'already_logged_in_elsewhere') {
        if (import.meta.env.DEV) {
          logger.debug('[Login] 响应数据:', response.data)
          logger.debug('[Login] 已在其他位置登录，等待用户选择强制登录或取消', data.existing_sessions)
        }
        const err = new Error((data.error_description as string) || '当前用户已在其他位置登录，需要发送通知吗？') as AlreadyLoggedInElsewhereError
        err.code = 'already_logged_in_elsewhere'
        err.error_description = (data.error_description as string) || ''
        err.existing_sessions = (Array.isArray(data.existing_sessions) ? data.existing_sessions : []) as ExistingSessionItem[]
        reject(err)
        return
      }

      // 调试：打印响应数据（仅在开发环境）
      if (import.meta.env.DEV) {
        logger.debug('[Login] 响应数据:', response.data)
        logger.debug('[Login] 响应状态:', response.status)
        logger.debug('[Login] 响应头:', response.headers)
      }

      // 将 OAuth2 响应格式转换为 LoginResponse 格式
      const token = (response.data as { access_token?: string } | null | undefined)?.access_token

      if (!token) {
        logger.error('[Login] Token 未找到，响应数据:', response.data)
        reject(new Error('登录失败：未获取到访问令牌'))
        return
      }
      
      // 获取用户信息（需要先设置 token 到请求头）
      // 使用 axios 直接发送请求，手动设置 Authorization 头
      const userInfoResponse = await axios.get<UserInfo>(
        '/api/TaktAuth/userinfo', // 与后端路由一致：api/TaktAuth/userinfo
        {
          baseURL: '', // 不使用 baseURL，因为 URL 已经包含完整路径
          headers: {
            Authorization: `Bearer ${token}`
          },
          withCredentials: true
        }
      )
      
      // 处理响应（可能需要根据实际响应格式调整）
      const userInfo = userInfoResponse.data
      
      resolve({
        token,
        refreshToken: response.data?.refresh_token, // 保存刷新令牌
        tokenType: response.data?.token_type,
        expiresIn: response.data?.expires_in,
        userInfo
      })
    } catch (error: any) {
      // 处理 OpenIddict 错误响应
      if (error.response?.data) {
        const errorData = error.response.data
        const errorMessage = errorData.error_description || errorData.error || '登录失败'
        reject(new Error(errorMessage))
      } else {
        reject(error)
      }
    }
  })
}

/**
 * 获取用户信息
 * 对应后端：GetUserInfoAsync
 */
export function getUserInfo(): Promise<UserInfo> {
  return request<UserInfo>({
    url: '/api/TaktAuth/userinfo', // 与后端路由一致：api/TaktAuth/userinfo
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

  return new Promise<LoginResponse>(async (resolve, reject) => {
    try {
      const axios = (await import('axios')).default
      
      // 从 Cookie 中读取 CSRF Token
      const CSRF_TOKEN_COOKIE = 'CSRF-Token'
      const CSRF_TOKEN_HEADER = 'X-CSRF-Token'
      function getCsrfTokenFromCookie(): string | null {
        if (typeof document === 'undefined') {
          return null
        }
        const cookies = document.cookie.split(';')
        for (const cookie of cookies) {
          const [name, value] = cookie.trim().split('=')
          if (name === CSRF_TOKEN_COOKIE) {
            return decodeURIComponent(value)
          }
        }
        return null
      }
      
      const csrfToken = getCsrfTokenFromCookie()
      const headers: Record<string, string> = {
        'Content-Type': 'application/x-www-form-urlencoded'
      }
      
      if (csrfToken) {
        headers[CSRF_TOKEN_HEADER] = csrfToken
      }
      
      const response = await axios.post<{
        access_token: string
        token_type: string
        expires_in: number
        refresh_token?: string
      }>(
        '/api/TaktAuth/connect/token',
        formData.toString(),
        {
          baseURL: '',
          headers,
          withCredentials: true
        }
      )

      const token = response.data?.access_token
      if (!token) {
        reject(new Error('刷新令牌失败：未获取到访问令牌'))
        return
      }

      // 获取用户信息
      const userInfoResponse = await axios.get<UserInfo>(
        '/api/TaktAuth/userinfo',
        {
          baseURL: '',
          headers: {
            Authorization: `Bearer ${token}`
          },
          withCredentials: true
        }
      )

      resolve({
        token,
        refreshToken: response.data?.refresh_token || refreshToken, // 如果返回新的 refreshToken 则使用新的，否则使用旧的
        tokenType: response.data?.token_type,
        expiresIn: response.data?.expires_in,
        userInfo: userInfoResponse.data
      })
    } catch (error: any) {
      if (error.response?.data) {
        const errorData = error.response.data
        const errorMessage = errorData.error_description || errorData.error || '刷新令牌失败'
        reject(new Error(errorMessage))
      } else {
        reject(error)
      }
    }
  })
}

// 登出
export function logout(refreshToken: string) {
  return request({
    url: '/api/TaktAuth/logout', // 与后端路由一致：api/TaktAuth/logout
    method: 'post',
    data: refreshToken // 后端需要接收字符串类型的 refreshToken
  })
}
