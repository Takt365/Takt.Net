// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：axios 扩展
// 文件名称：axios-config.d.ts
// 功能描述：AxiosRequestConfig 扩展（与 request.ts 中 blob 元数据约定一致）
// ========================================

import 'axios'

declare module 'axios' {
  interface AxiosRequestConfig {
    /**
     * 为 true 且 responseType 为 blob 时，成功响应为
     * { blob, contentDisposition?, contentType? }，便于解析 Content-Disposition 文件名。
     */
    blobWithHeaders?: boolean
  }
}
