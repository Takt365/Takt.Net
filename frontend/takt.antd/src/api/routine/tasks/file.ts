// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/routine/file
// 文件名称：file.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：文件相关 API，对应后端 TaktFilesController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  File as TaktFile,
  FileQuery,
  FileCreate,
  FileUpdate,
  FileStatus,
  FileIncrementDownloadCount,
  FileChange
} from '@/types/routine/tasks/file'

// ========================================
// 文件相关 API（按后端控制器顺序）
// ========================================

const fileUrl = '/api/TaktFiles'

/**
 * 获取文件列表（分页）
 * 对应后端：GetListAsync
 */
export function getFileList(params: FileQuery): Promise<TaktPagedResult<TaktFile>> {
  return request({
    url: `${fileUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取文件
 * 对应后端：GetByIdAsync
 */
export function getFileById(id: string): Promise<TaktFile> {
  return request({
    url: `${fileUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 根据文件编码获取文件
 * 对应后端：GetByCodeAsync
 */
export function getByCode(fileCode: string): Promise<TaktFile> {
  return request({
    url: `${fileUrl}/code/${fileCode}`,
    method: 'get'
  })
}

/**
 * 创建文件记录
 * 对应后端：CreateAsync
 */
export function createFile(data: FileCreate): Promise<TaktFile> {
  return request({
    url: fileUrl,
    method: 'post',
    data
  })
}

/**
 * 更新文件记录
 * 对应后端：UpdateAsync
 */
export function updateFile(id: string, data: FileUpdate): Promise<TaktFile> {
  return request({
    url: `${fileUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除文件记录（软删除）
 * 对应后端：DeleteAsync
 */
export function deleteFile(id: string): Promise<void> {
  return request({
    url: `${fileUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除文件记录（软删除）
 * 对应后端：DeleteBatchAsync
 */
export function deleteFileBatch(ids: string[]): Promise<void> {
  return request({
    url: `${fileUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新文件状态
 * 对应后端：UpdateStatusAsync
 */
export function updateFileStatus(data: FileStatus): Promise<TaktFile> {
  return request({
    url: `${fileUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 上传文件
 * 对应后端：UploadAsync
 * @param file 要上传的文件（原生 File 对象）
 * @param fileType 文件类型（0=头像，1=图片，2=文件，默认2）
 * @param targetFileName 目标文件名（可选，如果提供则使用该名称保存，否则使用默认的 fileCode.扩展名）
 * @param onProgress 上传进度回调（可选）
 */
export function upload(
  file: File, 
  fileType: number = 2,
  targetFileName?: string,
  onProgress?: (progressEvent: { percent: number }) => void
): Promise<any> {
  const formData = new FormData()
  formData.append('file', file)
  formData.append('fileType', fileType.toString())
  if (targetFileName) {
    formData.append('targetFileName', targetFileName)
  }
  
  return request({
    url: `${fileUrl}/upload`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    },
    onUploadProgress: onProgress ? (progressEvent: any) => {
      if (progressEvent.total) {
        const percent = Math.round((progressEvent.loaded * 100) / progressEvent.total)
        onProgress({ percent })
      }
    } : undefined
  })
}

/**
 * 下载文件
 * 对应后端：DownloadAsync
 * @param id 文件ID
 * @returns 文件Blob
 */
export function download(id: string): Promise<Blob> {
  return request({
    url: `${fileUrl}/${id}/download`,
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 增加下载次数
 * 对应后端：IncrementDownloadCountAsync
 */
export function incrementDownloadCount(data: FileIncrementDownloadCount): Promise<void> {
  return request({
    url: `${fileUrl}/increment-download-count`,
    method: 'post',
    data
  })
}

/**
 * 切换文件公开/私有状态
 * 对应后端：ChangeIsPublicAsync
 */
export function changeIsPublic(data: FileChange): Promise<TaktFile> {
  return request({
    url: `${fileUrl}/change`,
    method: 'put',
    data
  })
}

/**
 * 导出文件列表
 * 对应后端：ExportAsync
 * @param query 查询参数
 * @param sheetName 工作表名称（可选）
 * @param fileName 文件名（可选）
 */
export function exportFiles(query: FileQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${fileUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
