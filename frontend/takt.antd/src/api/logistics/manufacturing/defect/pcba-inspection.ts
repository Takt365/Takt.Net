// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/defect
// 文件名称：pcba-inspection.ts
// 功能描述：PcbaInspection API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Defect.TaktPcbaInspections
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PcbaInspection,
  PcbaInspectionQuery,
  PcbaInspectionCreate,
  PcbaInspectionUpdate,
  PcbaInspectionStatus
} from '@/types/logistics/manufacturing/defect/pcba-inspection'

// ========================================
// PcbaInspection相关 API（按后端控制器顺序）
// ========================================
const pcbaInspectionUrl = '/api/TaktPcbaInspections';

/**
 * 获取PcbaInspection列表（分页）
 * 对应后端：GetPcbaInspectionListAsync
 */
export function getPcbaInspectionList(params: PcbaInspectionQuery): Promise<TaktPagedResult<PcbaInspection>> {
  return request({
    url: `${pcbaInspectionUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PcbaInspection
 * 对应后端：GetPcbaInspectionByIdAsync
 */
export function getPcbaInspectionById(id: string): Promise<PcbaInspection> {
  return request({
    url: `${pcbaInspectionUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PcbaInspection选项列表（用于下拉框等）
 * 对应后端：GetPcbaInspectionOptionsAsync
 */
export function getPcbaInspectionOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${pcbaInspectionUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PcbaInspection
 * 对应后端：CreatePcbaInspectionAsync
 */
export function createPcbaInspection(data: PcbaInspectionCreate): Promise<PcbaInspection> {
  return request({
    url: pcbaInspectionUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PcbaInspection
 * 对应后端：UpdatePcbaInspectionAsync
 */
export function updatePcbaInspection(id: string, data: PcbaInspectionUpdate): Promise<PcbaInspection> {
  return request({
    url: `${pcbaInspectionUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PcbaInspection（单条）
 * 对应后端：DeletePcbaInspectionByIdAsync
 */
export function deletePcbaInspectionById(id: string): Promise<void> {
  return request({
    url: `${pcbaInspectionUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PcbaInspection
 * 对应后端：DeletePcbaInspectionBatchAsync
 */
export function deletePcbaInspectionBatch(ids: string[]): Promise<void> {
  return request({
    url: `${pcbaInspectionUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新PcbaInspection状态
 * 对应后端：UpdatePcbaInspectionStatusAsync
 */
export function updatePcbaInspectionStatus(data: PcbaInspectionStatus): Promise<PcbaInspectionStatus> {
  return request({
    url: `${pcbaInspectionUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPcbaInspectionTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPcbaInspectionTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${pcbaInspectionUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PcbaInspection
 * 对应后端：ImportPcbaInspectionAsync
 */
export function importPcbaInspectionData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${pcbaInspectionUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PcbaInspection
 * 对应后端：ExportPcbaInspectionAsync；fileName 仅传名称不含后缀
 */
export function exportPcbaInspectionData(query: PcbaInspectionQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${pcbaInspectionUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
