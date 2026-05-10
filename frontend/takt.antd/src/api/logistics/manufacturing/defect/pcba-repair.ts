// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/defect
// 文件名称：pcba-repair.ts
// 功能描述：PcbaRepair API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Defect.TaktPcbaRepairs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PcbaRepair,
  PcbaRepairQuery,
  PcbaRepairCreate,
  PcbaRepairUpdate,
  PcbaRepairStatus
} from '@/types/logistics/manufacturing/defect/pcba-repair'

// ========================================
// PcbaRepair相关 API（按后端控制器顺序）
// ========================================
const pcbaRepairUrl = '/api/TaktPcbaRepairs';

/**
 * 获取PcbaRepair列表（分页）
 * 对应后端：GetPcbaRepairListAsync
 */
export function getPcbaRepairList(params: PcbaRepairQuery): Promise<TaktPagedResult<PcbaRepair>> {
  return request({
    url: `${pcbaRepairUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PcbaRepair
 * 对应后端：GetPcbaRepairByIdAsync
 */
export function getPcbaRepairById(id: string): Promise<PcbaRepair> {
  return request({
    url: `${pcbaRepairUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PcbaRepair选项列表（用于下拉框等）
 * 对应后端：GetPcbaRepairOptionsAsync
 */
export function getPcbaRepairOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${pcbaRepairUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PcbaRepair
 * 对应后端：CreatePcbaRepairAsync
 */
export function createPcbaRepair(data: PcbaRepairCreate): Promise<PcbaRepair> {
  return request({
    url: pcbaRepairUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PcbaRepair
 * 对应后端：UpdatePcbaRepairAsync
 */
export function updatePcbaRepair(id: string, data: PcbaRepairUpdate): Promise<PcbaRepair> {
  return request({
    url: `${pcbaRepairUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PcbaRepair（单条）
 * 对应后端：DeletePcbaRepairByIdAsync
 */
export function deletePcbaRepairById(id: string): Promise<void> {
  return request({
    url: `${pcbaRepairUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PcbaRepair
 * 对应后端：DeletePcbaRepairBatchAsync
 */
export function deletePcbaRepairBatch(ids: string[]): Promise<void> {
  return request({
    url: `${pcbaRepairUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新PcbaRepair状态
 * 对应后端：UpdatePcbaRepairStatusAsync
 */
export function updatePcbaRepairStatus(data: PcbaRepairStatus): Promise<PcbaRepairStatus> {
  return request({
    url: `${pcbaRepairUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPcbaRepairTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPcbaRepairTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${pcbaRepairUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PcbaRepair
 * 对应后端：ImportPcbaRepairAsync
 */
export function importPcbaRepairData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${pcbaRepairUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PcbaRepair
 * 对应后端：ExportPcbaRepairAsync；fileName 仅传名称不含后缀
 */
export function exportPcbaRepairData(query: PcbaRepairQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${pcbaRepairUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
