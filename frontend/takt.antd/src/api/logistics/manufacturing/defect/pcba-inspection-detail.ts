// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/defect
// 文件名称：pcba-inspection-detail.ts
// 功能描述：PcbaInspectionDetail API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Defect.TaktPcbaInspectionDetails
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PcbaInspectionDetail,
  PcbaInspectionDetailQuery,
  PcbaInspectionDetailCreate,
  PcbaInspectionDetailUpdate
} from '@/types/logistics/manufacturing/defect/pcba-inspection-detail'

// ========================================
// PcbaInspectionDetail相关 API（按后端控制器顺序）
// ========================================
const pcbaInspectionDetailUrl = '/api/TaktPcbaInspectionDetails';

/**
 * 获取PcbaInspectionDetail列表（分页）
 * 对应后端：GetPcbaInspectionDetailListAsync
 */
export function getPcbaInspectionDetailList(params: PcbaInspectionDetailQuery): Promise<TaktPagedResult<PcbaInspectionDetail>> {
  return request({
    url: `${pcbaInspectionDetailUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PcbaInspectionDetail
 * 对应后端：GetPcbaInspectionDetailByIdAsync
 */
export function getPcbaInspectionDetailById(id: string): Promise<PcbaInspectionDetail> {
  return request({
    url: `${pcbaInspectionDetailUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PcbaInspectionDetail选项列表（用于下拉框等）
 * 对应后端：GetPcbaInspectionDetailOptionsAsync
 */
export function getPcbaInspectionDetailOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${pcbaInspectionDetailUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PcbaInspectionDetail
 * 对应后端：CreatePcbaInspectionDetailAsync
 */
export function createPcbaInspectionDetail(data: PcbaInspectionDetailCreate): Promise<PcbaInspectionDetail> {
  return request({
    url: pcbaInspectionDetailUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PcbaInspectionDetail
 * 对应后端：UpdatePcbaInspectionDetailAsync
 */
export function updatePcbaInspectionDetail(id: string, data: PcbaInspectionDetailUpdate): Promise<PcbaInspectionDetail> {
  return request({
    url: `${pcbaInspectionDetailUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PcbaInspectionDetail（单条）
 * 对应后端：DeletePcbaInspectionDetailByIdAsync
 */
export function deletePcbaInspectionDetailById(id: string): Promise<void> {
  return request({
    url: `${pcbaInspectionDetailUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PcbaInspectionDetail
 * 对应后端：DeletePcbaInspectionDetailBatchAsync
 */
export function deletePcbaInspectionDetailBatch(ids: string[]): Promise<void> {
  return request({
    url: `${pcbaInspectionDetailUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPcbaInspectionDetailTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPcbaInspectionDetailTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${pcbaInspectionDetailUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PcbaInspectionDetail
 * 对应后端：ImportPcbaInspectionDetailAsync
 */
export function importPcbaInspectionDetailData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${pcbaInspectionDetailUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PcbaInspectionDetail
 * 对应后端：ExportPcbaInspectionDetailAsync；fileName 仅传名称不含后缀
 */
export function exportPcbaInspectionDetailData(query: PcbaInspectionDetailQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${pcbaInspectionDetailUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
