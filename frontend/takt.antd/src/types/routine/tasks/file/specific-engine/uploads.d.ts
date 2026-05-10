// ========================================
// 项目名称:节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间:@/types/routine/tasks/file/specific-engine/uploads
// 文件名称:uploads.d.ts
// 创建时间:2026-05-10
// 创建人:Takt365
// 功能描述:上传引擎相关类型定义
// ========================================

/**
 * 文件上传结果DTO(对应后端 Takt.Application.Dtos.Routine.Tasks.File.SpecificEngine.FileUploadResultDto)
 */
export interface FileUploadResult {
  /** 文件编码 */
  fileCode: string
  /** 文件名称 */
  fileName: string
  /** 文件原始名称 */
  fileOriginalName: string
  /** 文件路径(相对路径) */
  filePath: string
  /** 文件大小(字节) */
  fileSize: number
  /** 文件类型(MIME类型) */
  fileType?: string
  /** 文件扩展名 */
  fileExtension?: string
  /** 文件哈希值 */
  fileHash?: string
  /** 文件分类(0=文档,1=图片,2=视频,3=音频,4=压缩包,5=其他) */
  fileCategory: number
}

/**
 * 文件上传类型枚举(对应后端 Takt.Shared.Constants.TaktFileConstants.FileUploadType)
 */
export enum FileUploadType {
  /** 头像 */
  Avatar = 0,
  /** 图片 */
  Image = 1,
  /** 文件 */
  File = 2
}
