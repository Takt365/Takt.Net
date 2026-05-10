// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/file/file
// 文件名称：file.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：file相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * File类型（对应后端 Takt.Application.Dtos.Routine.Tasks.File.TaktFileDto）
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
 * FileQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.File.TaktFileQueryDto）
 */
export interface FileQuery extends TaktPagedQuery {
  /** 对应后端字段 fileCode */
  fileCode?: string
  /** 对应后端字段 fileName */
  fileName?: string
  /** 对应后端字段 fileOriginalName */
  fileOriginalName?: string
  /** 对应后端字段 filePath */
  filePath?: string
  /** 对应后端字段 fileSize */
  fileSize?: number
  /** 对应后端字段 fileType */
  fileType?: string
  /** 对应后端字段 fileExtension */
  fileExtension?: string
  /** 对应后端字段 fileHash */
  fileHash?: string
  /** 对应后端字段 fileCategory */
  fileCategory?: number
  /** 对应后端字段 storageType */
  storageType?: number
  /** 对应后端字段 storageConfig */
  storageConfig?: string
  /** 对应后端字段 accessUrl */
  accessUrl?: string
  /** 对应后端字段 downloadCount */
  downloadCount?: number
  /** 对应后端字段 lastDownloadTime */
  lastDownloadTime?: string
  /** 对应后端字段 lastDownloadTimeStart */
  lastDownloadTimeStart?: string
  /** 对应后端字段 lastDownloadTimeEnd */
  lastDownloadTimeEnd?: string
  /** 对应后端字段 fileStatus */
  fileStatus?: number
  /** 对应后端字段 isPublic */
  isPublic?: number
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
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 createdById */
  createdById?: string
  /** 对应后端字段 createdBy */
  createdBy?: string
  /** 对应后端字段 createdAt */
  createdAt?: string
  /** 对应后端字段 createdAtStart */
  createdAtStart?: string
  /** 对应后端字段 createdAtEnd */
  createdAtEnd?: string
}

/**
 * FileCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.File.TaktFileCreateDto）
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
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * FileUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.File.TaktFileUpdateDto）
 */
export interface FileUpdate extends FileCreate {
  /** 对应后端字段 fileId */
  fileId: string
}

/**
 * FileStatus类型（对应后端 Takt.Application.Dtos.Routine.Tasks.File.TaktFileStatusDto）
 */
export interface FileStatus {
  /** 对应后端字段 fileId */
  fileId: string
  /** 对应后端字段 fileStatus */
  fileStatus: number
}

// ========================================
// 上传引擎类型定义
// ========================================

/**
 * 文件上传结果DTO（对应后端 Takt.Application.Dtos.Routine.Tasks.File.FileUploadResultDto）
 */
export interface FileUploadResult {
  /** 文件编码 */
  fileCode: string
  /** 文件名称 */
  fileName: string
  /** 文件原始名称 */
  fileOriginalName: string
  /** 文件路径（相对路径） */
  filePath: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件类型（MIME类型） */
  fileType?: string
  /** 文件扩展名 */
  fileExtension?: string
  /** 文件哈希值 */
  fileHash?: string
  /** 文件分类（0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他） */
  fileCategory: number
}

/**
 * 文件上传类型枚举（对应后端 TaktFileConstants.FileUploadType）
 */
export enum FileUploadType {
  /** 头像 */
  Avatar = 0,
  /** 图片 */
  Image = 1,
  /** 文件 */
  File = 2
}

// ========================================
// 下载引擎类型定义
// ========================================

/**
 * 文件下载计数增量DTO（对应后端 Takt.Application.Dtos.Routine.Tasks.File.TaktFileIncrementDownloadCountDto）
 */
export interface FileIncrementDownloadCount {
  /** 文件ID */
  fileId: string
}

/**
 * 文件公开状态变更DTO（对应后端 Takt.Application.Dtos.Routine.Tasks.File.TaktFilePublicChangeDto）
 */
export interface FilePublicChange {
  /** 文件ID */
  fileId: string
  /** 是否公开（0=否，1=是） */
  isPublic: number
}

/**
 * 文件访问权限检查结果
 */
export interface FileAccessPermissionCheck {
  /** 是否有权访问 */
  hasPermission: boolean
}
