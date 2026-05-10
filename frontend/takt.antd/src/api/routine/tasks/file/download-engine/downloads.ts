// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/file/downloads
// 文件名称：downloads.ts
// 创建时间：2026-05-06
// 创建人：Takt365
// 功能描述：下载引擎 API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.File.DownloadEngine.TaktDownloads
// ========================================

import request from '@/api/request'
import type { File } from '@/types/routine/tasks/file/file'
import type { FilePublicChange, FilePermissionCheckResult } from '@/types/routine/tasks/file/specific-engine/downloads'

// ========================================
// 下载引擎相关 API
// ========================================
const downloadsUrl = '/api/TaktDownloads'

/**
 * 获取文件下载信息（自动更新下载统计和位置）
 * 对应后端：GetDownloadableFileAsync
 */
export function getDownloadableFile(fileId: string, location?: string): Promise<File> {
  return request({
    url: `${downloadsUrl}/${fileId}`,
    method: 'get',
    params: location ? { location } : undefined
  })
}

/**
 * 变更文件公开状态
 * 对应后端：ChangePublicStatusAsync
 */
export function changeFilePublicStatus(data: FilePublicChange): Promise<void> {
  return request({
    url: `${downloadsUrl}/change-public-status`,
    method: 'post',
    data
  })
}

/**
 * 更新文件标签
 * 对应后端：UpdateFileTagsAsync
 */
export function updateFileTags(fileId: string, fileTags?: string): Promise<void> {
  return request({
    url: `${downloadsUrl}/${fileId}/tags`,
    method: 'put',
    data: fileTags
  })
}

/**
 * 更新文件访问权限配置
 * 对应后端：UpdateAccessPermissionConfigAsync
 */
export function updateFilePermissionConfig(fileId: string, accessPermissionConfig?: string): Promise<void> {
  return request({
    url: `${downloadsUrl}/${fileId}/permission-config`,
    method: 'put',
    data: accessPermissionConfig
  })
}

/**
 * 检查文件访问权限
 * 对应后端：CheckFileAccessPermissionAsync
 */
export function checkFilePermission(fileId: string, userId?: string): Promise<FilePermissionCheckResult> {
  return request({
    url: `${downloadsUrl}/${fileId}/check-permission`,
    method: 'get',
    params: userId ? { userId } : undefined
  })
}
