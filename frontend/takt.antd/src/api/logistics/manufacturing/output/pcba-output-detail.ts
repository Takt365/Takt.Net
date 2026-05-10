// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/output
// 文件名称：pcba-output-detail.ts
// 功能描述：PcbaOutputDetail API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Output.TaktPcbaOutputDetails
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PcbaOutputDetail,
  PcbaOutputDetailQuery,
  PcbaOutputDetailCreate,
  PcbaOutputDetailUpdate
} from '@/types/logistics/manufacturing/output/pcba-output-detail'

// ========================================
// PcbaOutputDetail相关 API（按后端控制器顺序）
// ========================================
const pcbaOutputDetailUrl = '/api/TaktPcbaOutputDetails';

/**
 * 获取PcbaOutputDetail列表（分页）
 * 对应后端：GetPcbaOutputDetailListAsync
 */
export function getPcbaOutputDetailList(params: PcbaOutputDetailQuery): Promise<TaktPagedResult<PcbaOutputDetail>> {
  return request({
    url: `${pcbaOutputDetailUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PcbaOutputDetail
 * 对应后端：GetPcbaOutputDetailByIdAsync
 */
export function getPcbaOutputDetailById(id: string): Promise<PcbaOutputDetail> {
  return request({
    url: `${pcbaOutputDetailUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PcbaOutputDetail选项列表（用于下拉框等）
 * 对应后端：GetPcbaOutputDetailOptionsAsync
 */
export function getPcbaOutputDetailOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${pcbaOutputDetailUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PcbaOutputDetail
 * 对应后端：CreatePcbaOutputDetailAsync
 */
export function createPcbaOutputDetail(data: PcbaOutputDetailCreate): Promise<PcbaOutputDetail> {
  return request({
    url: pcbaOutputDetailUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PcbaOutputDetail
 * 对应后端：UpdatePcbaOutputDetailAsync
 */
export function updatePcbaOutputDetail(id: string, data: PcbaOutputDetailUpdate): Promise<PcbaOutputDetail> {
  return request({
    url: `${pcbaOutputDetailUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PcbaOutputDetail（单条）
 * 对应后端：DeletePcbaOutputDetailByIdAsync
 */
export function deletePcbaOutputDetailById(id: string): Promise<void> {
  return request({
    url: `${pcbaOutputDetailUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PcbaOutputDetail
 * 对应后端：DeletePcbaOutputDetailBatchAsync
 */
export function deletePcbaOutputDetailBatch(ids: string[]): Promise<void> {
  return request({
    url: `${pcbaOutputDetailUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPcbaOutputDetailTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPcbaOutputDetailTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${pcbaOutputDetailUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PcbaOutputDetail
 * 对应后端：ImportPcbaOutputDetailAsync
 */
export function importPcbaOutputDetailData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${pcbaOutputDetailUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PcbaOutputDetail
 * 对应后端：ExportPcbaOutputDetailAsync；fileName 仅传名称不含后缀
 */
export function exportPcbaOutputDetailData(query: PcbaOutputDetailQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${pcbaOutputDetailUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
