// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/file
// 文件名称：file.ts
// 功能描述：File API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.File.TaktFiles
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  File,
  FileQuery,
  FileCreate,
  FileUpdate,
  FileStatus
} from '@/types/routine/tasks/file/file'

// ========================================
// File相关 API（按后端控制器顺序）
// ========================================
const fileUrl = '/api/TaktFiles';

/**
 * 获取File列表（分页）
 * 对应后端：GetFileListAsync
 */
export function getFileList(params: FileQuery): Promise<TaktPagedResult<File>> {
  return request({
    url: `${fileUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取File
 * 对应后端：GetFileByIdAsync
 */
export function getFileById(id: string): Promise<File> {
  return request({
    url: `${fileUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取File选项列表（用于下拉框等）
 * 对应后端：GetFileOptionsAsync
 */
export function getFileOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${fileUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建File
 * 对应后端：CreateFileAsync
 */
export function createFile(data: FileCreate): Promise<File> {
  return request({
    url: fileUrl,
    method: 'post',
    data
  })
}

/**
 * 更新File
 * 对应后端：UpdateFileAsync
 */
export function updateFile(id: string, data: FileUpdate): Promise<File> {
  return request({
    url: `${fileUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除File（单条）
 * 对应后端：DeleteFileByIdAsync
 */
export function deleteFileById(id: string): Promise<void> {
  return request({
    url: `${fileUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除File
 * 对应后端：DeleteFileBatchAsync
 */
export function deleteFileBatch(ids: string[]): Promise<void> {
  return request({
    url: `${fileUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新File状态
 * 对应后端：UpdateFileStatusAsync
 */
export function updateFileStatus(data: FileStatus): Promise<FileStatus> {
  return request({
    url: `${fileUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetFileTemplateAsync；fileName 仅传名称不含后缀
 */
export function getFileTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${fileUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入File
 * 对应后端：ImportFileAsync
 */
export function importFileData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${fileUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出File
 * 对应后端：ExportFileAsync；fileName 仅传名称不含后缀
 */
export function exportFileData(query: FileQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${fileUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
