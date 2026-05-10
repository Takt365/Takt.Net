// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/defect
// 文件名称：assy-defect.ts
// 功能描述：AssyDefect API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Defect.TaktAssyDefects
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AssyDefect,
  AssyDefectQuery,
  AssyDefectCreate,
  AssyDefectUpdate,
  AssyDefectStatus
} from '@/types/logistics/manufacturing/defect/assy-defect'

// ========================================
// AssyDefect相关 API（按后端控制器顺序）
// ========================================
const assyDefectUrl = '/api/TaktAssyDefects';

/**
 * 获取AssyDefect列表（分页）
 * 对应后端：GetAssyDefectListAsync
 */
export function getAssyDefectList(params: AssyDefectQuery): Promise<TaktPagedResult<AssyDefect>> {
  return request({
    url: `${assyDefectUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AssyDefect
 * 对应后端：GetAssyDefectByIdAsync
 */
export function getAssyDefectById(id: string): Promise<AssyDefect> {
  return request({
    url: `${assyDefectUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AssyDefect选项列表（用于下拉框等）
 * 对应后端：GetAssyDefectOptionsAsync
 */
export function getAssyDefectOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${assyDefectUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AssyDefect
 * 对应后端：CreateAssyDefectAsync
 */
export function createAssyDefect(data: AssyDefectCreate): Promise<AssyDefect> {
  return request({
    url: assyDefectUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AssyDefect
 * 对应后端：UpdateAssyDefectAsync
 */
export function updateAssyDefect(id: string, data: AssyDefectUpdate): Promise<AssyDefect> {
  return request({
    url: `${assyDefectUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AssyDefect（单条）
 * 对应后端：DeleteAssyDefectByIdAsync
 */
export function deleteAssyDefectById(id: string): Promise<void> {
  return request({
    url: `${assyDefectUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AssyDefect
 * 对应后端：DeleteAssyDefectBatchAsync
 */
export function deleteAssyDefectBatch(ids: string[]): Promise<void> {
  return request({
    url: `${assyDefectUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新AssyDefect状态
 * 对应后端：UpdateAssyDefectStatusAsync
 */
export function updateAssyDefectStatus(data: AssyDefectStatus): Promise<AssyDefectStatus> {
  return request({
    url: `${assyDefectUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAssyDefectTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAssyDefectTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${assyDefectUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AssyDefect
 * 对应后端：ImportAssyDefectAsync
 */
export function importAssyDefectData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${assyDefectUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AssyDefect
 * 对应后端：ExportAssyDefectAsync；fileName 仅传名称不含后缀
 */
export function exportAssyDefectData(query: AssyDefectQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${assyDefectUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
