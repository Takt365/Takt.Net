// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/signal-r
// 文件名称：online.ts
// 功能描述：Online API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.SignalR.TaktOnlines
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Online,
  OnlineQuery,
  OnlineCreate,
  OnlineUpdate,
  OnlineStatus
} from '@/types/routine/tasks/signal-r/online'

// ========================================
// Online相关 API（按后端控制器顺序）
// ========================================
const onlineUrl = '/api/TaktOnlines';

/**
 * 获取Online列表（分页）
 * 对应后端：GetOnlineListAsync
 */
export function getOnlineList(params: OnlineQuery): Promise<TaktPagedResult<Online>> {
  return request({
    url: `${onlineUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Online
 * 对应后端：GetOnlineByIdAsync
 */
export function getOnlineById(id: string): Promise<Online> {
  return request({
    url: `${onlineUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Online选项列表（用于下拉框等）
 * 对应后端：GetOnlineOptionsAsync
 */
export function getOnlineOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${onlineUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Online
 * 对应后端：CreateOnlineAsync
 */
export function createOnline(data: OnlineCreate): Promise<Online> {
  return request({
    url: onlineUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Online
 * 对应后端：UpdateOnlineAsync
 */
export function updateOnline(id: string, data: OnlineUpdate): Promise<Online> {
  return request({
    url: `${onlineUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Online（单条）
 * 对应后端：DeleteOnlineByIdAsync
 */
export function deleteOnlineById(id: string): Promise<void> {
  return request({
    url: `${onlineUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Online
 * 对应后端：DeleteOnlineBatchAsync
 */
export function deleteOnlineBatch(ids: string[]): Promise<void> {
  return request({
    url: `${onlineUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Online状态
 * 对应后端：UpdateOnlineStatusAsync
 */
export function updateOnlineStatus(data: OnlineStatus): Promise<OnlineStatus> {
  return request({
    url: `${onlineUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetOnlineTemplateAsync；fileName 仅传名称不含后缀
 */
export function getOnlineTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${onlineUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Online
 * 对应后端：ImportOnlineAsync
 */
export function importOnlineData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${onlineUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Online
 * 对应后端：ExportOnlineAsync；fileName 仅传名称不含后缀
 */
export function exportOnlineData(query: OnlineQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${onlineUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
