// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/file/specific-engine/downloads
// 文件名称：downloads.d.ts
// 创建时间：2026-05-06
// 创建人：Takt365
// 功能描述：下载引擎相关类型定义
// ========================================

/**
 * 文件公开状态变更DTO（对应后端 Takt.Application.Dtos.Routine.Tasks.File.SpecificEngine.TaktFilePublicChangeDto）
 */
export interface FilePublicChange {
  /** 文件ID */
  fileId: string
  /** 是否公开（0=否，1=是） */
  isPublic: number
}

/**
 * 检查权限结果（对应后端匿名类型 { hasPermission }）
 */
export interface FilePermissionCheckResult {
  /** 是否有权访问 */
  hasPermission: boolean
}
