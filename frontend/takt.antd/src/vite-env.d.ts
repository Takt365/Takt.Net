/// <reference types="vite/client" />

/**
 * 环境变量类型定义
 * 为 Vite 环境变量提供 TypeScript 类型支持
 */
interface ImportMetaEnv {
  // 应用基础配置
  readonly VITE_APP_TITLE: string
  readonly VITE_APP_ENV: 'development' | 'production' | 'test'
  
  // API 配置
  readonly VITE_API_BASE_URL: string
  readonly VITE_API_TIMEOUT: string
  readonly VITE_API_TARGET: string
  
  // 开发服务器配置
  readonly VITE_DEV_SERVER_PORT: string
  readonly VITE_DEV_SERVER_HOST: string
  readonly VITE_DEV_SERVER_HTTPS: string
  
  // Mock 配置
  readonly VITE_USE_MOCK: string
  
  // 构建配置
  readonly VITE_BUILD_SOURCEMAP: string
  readonly VITE_BUILD_COMPRESS: string
}

interface ImportMeta {
  readonly env: ImportMetaEnv
}

declare module 'virtual:app-info' {
  const s: string
  export default s
}

declare module '@form-create/antd-designer/locale/zh-cn.js' {
  const locale: Record<string, unknown>
  export default locale
}
declare module '@form-create/antd-designer/locale/en.js' {
  const locale: Record<string, unknown>
  export default locale
}
