// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/routine/file
// 文件名称：file.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：文件相关类型定义，对应后端 Takt.Application.Dtos.Routine.Files.TaktFileDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 文件类型（对应后端 Takt.Application.Dtos.Routine.Files.TaktFileDto）
 */
export interface File extends TaktEntityBase {
  /** 文件ID（对应后端 FileId，序列化为string以避免Javascript精度问题） */
  fileId: string
  /** 文件编码（唯一索引） */
  fileCode: string
  /** 文件名称 */
  fileName: string
  /** 文件原始名称（上传时的原始文件名） */
  fileOriginalName: string
  /** 文件路径（相对路径或完整路径） */
  filePath: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件类型（MIME类型） */
  fileType?: string
  /** 文件扩展名 */
  fileExtension?: string
  /** 文件哈希值（MD5或SHA256，用于去重和校验） */
  fileHash?: string
  /** 文件分类（0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他） */
  fileCategory: number
  /** 存储方式（0=本地存储，1=OSS对象存储，2=FTP，3=其他） */
  storageType: number
  /** 存储配置（JSON格式，存储OSS配置、FTP配置等） */
  storageConfig?: string
  /** 访问地址（文件的访问URL地址） */
  accessUrl?: string
  /** 下载次数 */
  downloadCount: number
  /** 最后下载时间 */
  lastDownloadTime?: string
  /** 文件状态（0=正常，1=已锁定，2=已归档，3=已删除） */
  fileStatus: number
  /** 是否公开（0=私有，1=公开） */
  isPublic: number
  /** 访问权限配置（JSON格式，存储用户ID列表、部门ID列表或角色ID列表） */
  accessPermissionConfig?: string
  /** 文件描述 */
  fileDescription?: string
  /** 文件标签（多个标签用逗号分隔） */
  fileTags?: string
  /** IP地址（上传或访问文件的IP地址） */
  ipAddress?: string
  /** 位置（IP地址对应的地理位置信息） */
  location?: string
}

/**
 * 文件查询类型（对应后端 Takt.Application.Dtos.Routine.Files.TaktFileQueryDto）
 */
export interface FileQuery extends TaktPagedQuery {
  /** 关键词（在文件编码、文件名称、文件原始名称中模糊查询） */
  keyWords?: string
  /** 文件编码 */
  fileCode?: string
  /** 文件名称（模糊查询） */
  fileName?: string
  /** 文件分类（0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他） */
  fileCategory?: number
  /** 存储方式（0=本地存储，1=OSS对象存储，2=FTP，3=其他） */
  storageType?: number
  /** 文件状态（0=正常，1=已锁定，2=已归档，3=已删除） */
  fileStatus?: number
  /** 创建开始时间（上传开始时间） */
  createTimeStart?: string
  /** 创建结束时间（上传结束时间） */
  createTimeEnd?: string
  /** 文件扩展名 */
  fileExtension?: string
  /** 是否公开（0=私有，1=公开） */
  isPublic?: number
}

/**
 * 创建文件类型（对应后端 Takt.Application.Dtos.Routine.Files.TaktFileCreateDto）
 */
export interface FileCreate {
  /** 文件编码（唯一索引） */
  fileCode: string
  /** 文件名称 */
  fileName: string
  /** 文件原始名称（上传时的原始文件名） */
  fileOriginalName: string
  /** 文件路径（相对路径或完整路径） */
  filePath: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件类型（MIME类型） */
  fileType?: string
  /** 文件扩展名 */
  fileExtension?: string
  /** 文件哈希值（MD5或SHA256，用于去重和校验） */
  fileHash?: string
  /** 文件分类（0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他） */
  fileCategory: number
  /** 存储方式（0=本地存储，1=OSS对象存储，2=FTP，3=其他） */
  storageType: number
  /** 存储配置（JSON格式，存储OSS配置、FTP配置等） */
  storageConfig?: string
  /** 访问地址（文件的访问URL地址） */
  accessUrl?: string
  /** 是否公开（0=私有，1=公开） */
  isPublic: number
  /** 访问权限配置（JSON格式，存储用户ID列表、部门ID列表或角色ID列表） */
  accessPermissionConfig?: string
  /** 文件描述 */
  fileDescription?: string
  /** 文件标签（多个标签用逗号分隔） */
  fileTags?: string
  /** 备注 */
  remark?: string
  /** IP地址（上传或访问文件的IP地址） */
  ipAddress?: string
  /** 位置（IP地址对应的地理位置信息） */
  location?: string
}

/**
 * 更新文件类型（对应后端 Takt.Application.Dtos.Routine.Files.TaktFileUpdateDto）
 */
export interface FileUpdate extends FileCreate {
  /** 文件ID（对应后端 FileId，序列化为string以避免Javascript精度问题） */
  fileId: string
  /** 文件状态（0=正常，1=已锁定，2=已归档，3=已删除） */
  fileStatus?: number
}

/**
 * 文件状态类型（对应后端 Takt.Application.Dtos.Routine.Files.TaktFileStatusDto）
 */
export interface FileStatus {
  /** 文件ID（对应后端 FileId，序列化为string以避免Javascript精度问题） */
  fileId: string
  /** 文件状态（0=正常，1=已锁定，2=已归档，3=已删除） */
  fileStatus: number
}

/**
 * 增加下载次数类型（对应后端 Takt.Application.Dtos.Routine.Files.TaktFileIncrementDownloadCountDto）
 */
export interface FileIncrementDownloadCount {
  /** 文件ID（对应后端 FileId，序列化为string以避免Javascript精度问题） */
  fileId: string
}

/**
 * 文件切换类型（对应后端 Takt.Application.Dtos.Routine.Files.TaktFileChangeDto）
 */
export interface FileChange {
  /** 文件ID（对应后端 FileId，序列化为string以避免Javascript精度问题） */
  fileId: string
  /** 是否公开（0=私有，1=公开） */
  isPublic: number
}
