// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/visiting
// 文件名称：visit-person.ts
// 功能描述：VisitPerson API，对应后端 Takt.WebApi.Controllers.Routine.Business.Visiting.TaktVisitPersons
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  VisitPerson,
  VisitPersonQuery,
  VisitPersonCreate,
  VisitPersonUpdate
} from '@/types/routine/business/visiting/visit-person'

// ========================================
// VisitPerson相关 API（按后端控制器顺序）
// ========================================
const visitPersonUrl = '/api/TaktVisitPersons';

/**
 * 获取VisitPerson列表（分页）
 * 对应后端：GetVisitPersonListAsync
 */
export function getVisitPersonList(params: VisitPersonQuery): Promise<TaktPagedResult<VisitPerson>> {
  return request({
    url: `${visitPersonUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取VisitPerson
 * 对应后端：GetVisitPersonByIdAsync
 */
export function getVisitPersonById(id: string): Promise<VisitPerson> {
  return request({
    url: `${visitPersonUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取VisitPerson选项列表（用于下拉框等）
 * 对应后端：GetVisitPersonOptionsAsync
 */
export function getVisitPersonOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${visitPersonUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建VisitPerson
 * 对应后端：CreateVisitPersonAsync
 */
export function createVisitPerson(data: VisitPersonCreate): Promise<VisitPerson> {
  return request({
    url: visitPersonUrl,
    method: 'post',
    data
  })
}

/**
 * 更新VisitPerson
 * 对应后端：UpdateVisitPersonAsync
 */
export function updateVisitPerson(id: string, data: VisitPersonUpdate): Promise<VisitPerson> {
  return request({
    url: `${visitPersonUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除VisitPerson（单条）
 * 对应后端：DeleteVisitPersonByIdAsync
 */
export function deleteVisitPersonById(id: string): Promise<void> {
  return request({
    url: `${visitPersonUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除VisitPerson
 * 对应后端：DeleteVisitPersonBatchAsync
 */
export function deleteVisitPersonBatch(ids: string[]): Promise<void> {
  return request({
    url: `${visitPersonUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetVisitPersonTemplateAsync；fileName 仅传名称不含后缀
 */
export function getVisitPersonTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${visitPersonUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入VisitPerson
 * 对应后端：ImportVisitPersonAsync
 */
export function importVisitPersonData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${visitPersonUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出VisitPerson
 * 对应后端：ExportVisitPersonAsync；fileName 仅传名称不含后缀
 */
export function exportVisitPersonData(query: VisitPersonQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${visitPersonUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
