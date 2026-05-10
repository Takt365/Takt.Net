// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/file/upload-engine
// 文件名称：upload.ts
// 功能描述：文件上传API，对应后端 TaktUploadsController
// 路由前缀：api/TaktUploads
// ========================================

import request from '@/api/request'

/** 文件上传类型枚举 */
export enum FileUploadType {
  Avatar = 0,
  Document = 1,
  Image = 2,
  Video = 3,
  Other = 99
}

/** 文件上传结果 */
export interface FileUploadResult {
  fileName: string
  filePath: string
  fileSize: number
  mimeType: string
  url: string
}

// ========================================
// 文件上传API
// ========================================
const uploadUrl = '/api/TaktUploads'

/**
 * 上传文件
 * 对应后端：UploadAsync
 * @param file 文件对象
 * @param fileType 文件类型
 * @param targetFileName 目标文件名（可选）
 */
export function uploadFile(
  file: File,
  fileType: FileUploadType,
  targetFileName?: string
): Promise<FileUploadResult> {
  const formData = new FormData()
  formData.append('file', file)
  formData.append('fileType', fileType.toString())
  if (targetFileName) {
    formData.append('targetFileName', targetFileName)
  }
  return request({
    url: `${uploadUrl}/upload`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 批量上传文件
 * 对应后端：UploadBatchAsync
 * @param files 文件列表
 * @param fileType 文件类型
 */
export function uploadFileBatch(
  files: File[],
  fileType: FileUploadType
): Promise<FileUploadResult[]> {
  const formData = new FormData()
  files.forEach((file, index) => {
    formData.append(`files[${index}]`, file)
  })
  formData.append('fileType', fileType.toString())
  return request({
    url: `${uploadUrl}/upload/batch`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 删除文件
 * 对应后端：DeleteAsync
 * @param filePath 文件路径（相对路径）
 */
export function deleteFile(filePath: string): Promise<{ message: string }> {
  return request({
    url: `${uploadUrl}/delete`,
    method: 'delete',
    params: { filePath }
  })
}