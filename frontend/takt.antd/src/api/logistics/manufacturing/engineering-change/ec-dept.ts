// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/engineering-change
// 文件名称：ec-dept.ts
// 功能描述：EcDept API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange.TaktEcDepts
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EcDept,
  EcDeptQuery,
  EcDeptCreate,
  EcDeptUpdate
} from '@/types/logistics/manufacturing/engineering-change/ec-dept'

// ========================================
// EcDept相关 API（按后端控制器顺序）
// ========================================
const ecDeptUrl = '/api/TaktEcDepts';

/**
 * 获取EcDept列表（分页）
 * 对应后端：GetEcDeptListAsync
 */
export function getEcDeptList(params: EcDeptQuery): Promise<TaktPagedResult<EcDept>> {
  return request({
    url: `${ecDeptUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EcDept
 * 对应后端：GetEcDeptByIdAsync
 */
export function getEcDeptById(id: string): Promise<EcDept> {
  return request({
    url: `${ecDeptUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EcDept选项列表（用于下拉框等）
 * 对应后端：GetEcDeptOptionsAsync
 */
export function getEcDeptOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ecDeptUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EcDept
 * 对应后端：CreateEcDeptAsync
 */
export function createEcDept(data: EcDeptCreate): Promise<EcDept> {
  return request({
    url: ecDeptUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EcDept
 * 对应后端：UpdateEcDeptAsync
 */
export function updateEcDept(id: string, data: EcDeptUpdate): Promise<EcDept> {
  return request({
    url: `${ecDeptUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EcDept（单条）
 * 对应后端：DeleteEcDeptByIdAsync
 */
export function deleteEcDeptById(id: string): Promise<void> {
  return request({
    url: `${ecDeptUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EcDept
 * 对应后端：DeleteEcDeptBatchAsync
 */
export function deleteEcDeptBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ecDeptUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEcDeptTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEcDeptTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ecDeptUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EcDept
 * 对应后端：ImportEcDeptAsync
 */
export function importEcDeptData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ecDeptUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EcDept
 * 对应后端：ExportEcDeptAsync；fileName 仅传名称不含后缀
 */
export function exportEcDeptData(query: EcDeptQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ecDeptUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
