// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/files/file
// 文件名称：file.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：file相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * File类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Files.TaktFileDto）
 */
export interface File extends TaktEntityBase {
  /** 对应后端字段 fileId */
  fileId: string
  /** 对应后端字段 fileCode */
  fileCode: string
  /** 对应后端字段 fileName */
  fileName: string
  /** 对应后端字段 fileOriginalName */
  fileOriginalName: string
  /** 对应后端字段 filePath */
  filePath: string
  /** 对应后端字段 fileSize */
  fileSize: number
  /** 对应后端字段 fileType */
  fileType?: string
  /** 对应后端字段 fileExtension */
  fileExtension?: string
  /** 对应后端字段 fileHash */
  fileHash?: string
  /** 对应后端字段 fileCategory */
  fileCategory: number
  /** 对应后端字段 storageType */
  storageType: number
  /** 对应后端字段 storageConfig */
  storageConfig?: string
  /** 对应后端字段 accessUrl */
  accessUrl?: string
  /** 对应后端字段 downloadCount */
  downloadCount: number
  /** 对应后端字段 lastDownloadTime */
  lastDownloadTime?: string
  /** 对应后端字段 fileStatus */
  fileStatus: number
  /** 对应后端字段 isPublic */
  isPublic: number
  /** 对应后端字段 accessPermissionConfig */
  accessPermissionConfig?: string
  /** 对应后端字段 fileDescription */
  fileDescription?: string
  /** 对应后端字段 fileTags */
  fileTags?: string
  /** 对应后端字段 ipAddress */
  ipAddress?: string
  /** 对应后端字段 location */
  location?: string
}

/**
 * FileQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Files.TaktFileQueryDto）
 */
export interface FileQuery extends TaktPagedQuery {
  /** 对应后端字段 fileCode */
  fileCode?: string
  /** 对应后端字段 fileName */
  fileName?: string
  /** 对应后端字段 fileCategory */
  fileCategory?: number
  /** 对应后端字段 storageType */
  storageType?: number
  /** 对应后端字段 fileStatus */
  fileStatus?: number
  /** 对应后端字段 createdAtStart */
  createdAtStart?: string
  /** 对应后端字段 createdAtEnd */
  createdAtEnd?: string
  /** 对应后端字段 fileExtension */
  fileExtension?: string
  /** 对应后端字段 isPublic */
  isPublic?: number
}

/**
 * FileCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Files.TaktFileCreateDto）
 */
export interface FileCreate {
  /** 对应后端字段 fileCode */
  fileCode: string
  /** 对应后端字段 fileName */
  fileName: string
  /** 对应后端字段 fileOriginalName */
  fileOriginalName: string
  /** 对应后端字段 filePath */
  filePath: string
  /** 对应后端字段 fileSize */
  fileSize: number
  /** 对应后端字段 fileType */
  fileType?: string
  /** 对应后端字段 fileExtension */
  fileExtension?: string
  /** 对应后端字段 fileHash */
  fileHash?: string
  /** 对应后端字段 fileCategory */
  fileCategory: number
  /** 对应后端字段 storageType */
  storageType: number
  /** 对应后端字段 storageConfig */
  storageConfig?: string
  /** 对应后端字段 accessUrl */
  accessUrl?: string
  /** 对应后端字段 isPublic */
  isPublic: number
  /** 对应后端字段 accessPermissionConfig */
  accessPermissionConfig?: string
  /** 对应后端字段 fileDescription */
  fileDescription?: string
  /** 对应后端字段 fileTags */
  fileTags?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 ipAddress */
  ipAddress?: string
  /** 对应后端字段 location */
  location?: string
}

/**
 * FileUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Files.TaktFileUpdateDto）
 */
export interface FileUpdate extends FileCreate {
  /** 对应后端字段 fileId */
  fileId: string
}

/**
 * FileStatus类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Files.TaktFileStatusDto）
 */
export interface FileStatus {
  /** 对应后端字段 fileId */
  fileId: string
  /** 对应后端字段 fileStatus */
  fileStatus: number
}

/**
 * FileChange类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Files.TaktFileChangeDto）
 */
export interface FileChange {
  /** 对应后端字段 fileId */
  fileId: string
  /** 对应后端字段 isPublic */
  isPublic: number
}

/**
 * FileIncrementDownloadCount类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Files.TaktFileIncrementDownloadCountDto）
 */
export interface FileIncrementDownloadCount {
  /** 对应后端字段 fileId */
  fileId: string
}
