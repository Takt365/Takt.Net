// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/defect
// 文件名称：pcba-repair-detail.ts
// 功能描述：PcbaRepairDetail API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Defect.TaktPcbaRepairDetails
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PcbaRepairDetail,
  PcbaRepairDetailQuery,
  PcbaRepairDetailCreate,
  PcbaRepairDetailUpdate
} from '@/types/logistics/manufacturing/defect/pcba-repair-detail'

// ========================================
// PcbaRepairDetail相关 API（按后端控制器顺序）
// ========================================
const pcbaRepairDetailUrl = '/api/TaktPcbaRepairDetails';

/**
 * 获取PcbaRepairDetail列表（分页）
 * 对应后端：GetPcbaRepairDetailListAsync
 */
export function getPcbaRepairDetailList(params: PcbaRepairDetailQuery): Promise<TaktPagedResult<PcbaRepairDetail>> {
  return request({
    url: `${pcbaRepairDetailUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PcbaRepairDetail
 * 对应后端：GetPcbaRepairDetailByIdAsync
 */
export function getPcbaRepairDetailById(id: string): Promise<PcbaRepairDetail> {
  return request({
    url: `${pcbaRepairDetailUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PcbaRepairDetail选项列表（用于下拉框等）
 * 对应后端：GetPcbaRepairDetailOptionsAsync
 */
export function getPcbaRepairDetailOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${pcbaRepairDetailUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PcbaRepairDetail
 * 对应后端：CreatePcbaRepairDetailAsync
 */
export function createPcbaRepairDetail(data: PcbaRepairDetailCreate): Promise<PcbaRepairDetail> {
  return request({
    url: pcbaRepairDetailUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PcbaRepairDetail
 * 对应后端：UpdatePcbaRepairDetailAsync
 */
export function updatePcbaRepairDetail(id: string, data: PcbaRepairDetailUpdate): Promise<PcbaRepairDetail> {
  return request({
    url: `${pcbaRepairDetailUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PcbaRepairDetail（单条）
 * 对应后端：DeletePcbaRepairDetailByIdAsync
 */
export function deletePcbaRepairDetailById(id: string): Promise<void> {
  return request({
    url: `${pcbaRepairDetailUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PcbaRepairDetail
 * 对应后端：DeletePcbaRepairDetailBatchAsync
 */
export function deletePcbaRepairDetailBatch(ids: string[]): Promise<void> {
  return request({
    url: `${pcbaRepairDetailUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPcbaRepairDetailTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPcbaRepairDetailTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${pcbaRepairDetailUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PcbaRepairDetail
 * 对应后端：ImportPcbaRepairDetailAsync
 */
export function importPcbaRepairDetailData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${pcbaRepairDetailUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PcbaRepairDetail
 * 对应后端：ExportPcbaRepairDetailAsync；fileName 仅传名称不含后缀
 */
export function exportPcbaRepairDetailData(query: PcbaRepairDetailQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${pcbaRepairDetailUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
