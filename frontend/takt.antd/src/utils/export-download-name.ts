// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/utils
// 文件名称：export-download-name.ts
// 功能描述：按服务端响应头还原下载文件名。是否导出为 zip（如 TaktExcelHelper 超行数分批）仅由后端决定，前端不做格式推断或用户选择。
// ========================================

/**
 * 从 Content-Disposition 解析文件名（支持 filename、filename* RFC5987）。
 */
export function parseContentDispositionFileName(contentDisposition: string | undefined | null): string | null {
  if (!contentDisposition || typeof contentDisposition !== 'string') return null
  const trimmed = contentDisposition.trim()
  if (!trimmed) return null

  const star = /filename\*=(?:UTF-8''|utf-8'')([^;\s]+)/i.exec(trimmed)
  if (star?.[1]) {
    const raw = star[1].replace(/^["']|["']$/g, '').trim()
    try {
      return decodeURIComponent(raw)
    } catch {
      return raw
    }
  }

  const quoted = /filename\s*=\s*"([^"]+)"/i.exec(trimmed)
  if (quoted?.[1]) return sanitizeExportFileSegment(quoted[1])

  const unquoted = /filename\s*=\s*([^;\s]+)/i.exec(trimmed)
  if (unquoted?.[1]) return sanitizeExportFileSegment(unquoted[1].replace(/^["']|["']$/g, ''))

  return null
}

function sanitizeExportFileSegment(name: string): string {
  const s = name.trim()
  if (!s) return s
  return s.replace(/[/\\?%*:|"<>]/g, '_')
}

function stripKnownExportExt(base: string): string {
  return base.replace(/\.(xlsx|xls|zip)$/i, '')
}

function ensureExt(base: string, ext: string): string {
  return stripKnownExportExt(base) + ext
}

/**
 * 优先使用服务端 Content-Disposition 中的文件名；无则按 Content-Type 为 fallbackBase 补扩展名（与后端返回一致，非用户选择）。
 */
export function resolveExportDownloadFileName(options: {
  contentDisposition?: string | null
  contentType?: string | null
  /** 不含扩展名或任意；方法内会去掉末尾 .xlsx/.zip 再拼接 */
  fallbackBase: string
}): string {
  const fromHeader = parseContentDispositionFileName(options.contentDisposition ?? undefined)
  if (fromHeader && fromHeader.length > 0) return fromHeader

  const ct = (options.contentType || '').toLowerCase()
  if (ct.includes('application/zip')) return ensureExt(options.fallbackBase, '.zip')
  if (
    ct.includes('spreadsheetml') ||
    ct.includes('application/vnd.ms-excel') ||
    ct.includes('officedocument.spreadsheetml')
  ) {
    return ensureExt(options.fallbackBase, '.xlsx')
  }
  return ensureExt(options.fallbackBase, '.xlsx')
}
