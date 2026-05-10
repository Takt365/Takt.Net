// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial
// 文件名称：countersign-form.ts
// 功能描述：CountersignForm API，对应后端 Takt.WebApi.Controllers.Accounting.Financial.TaktCountersignForms
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CountersignForm,
  CountersignFormQuery,
  CountersignFormCreate,
  CountersignFormUpdate
} from '@/types/accounting/financial/countersign-form'

// ========================================
// CountersignForm相关 API（按后端控制器顺序）
// ========================================
const countersignFormUrl = '/api/TaktCountersignForms';

/**
 * 获取CountersignForm列表（分页）
 * 对应后端：GetCountersignFormListAsync
 */
export function getCountersignFormList(params: CountersignFormQuery): Promise<TaktPagedResult<CountersignForm>> {
  return request({
    url: `${countersignFormUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CountersignForm
 * 对应后端：GetCountersignFormByIdAsync
 */
export function getCountersignFormById(id: string): Promise<CountersignForm> {
  return request({
    url: `${countersignFormUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CountersignForm选项列表（用于下拉框等）
 * 对应后端：GetCountersignFormOptionsAsync
 */
export function getCountersignFormOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${countersignFormUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建CountersignForm
 * 对应后端：CreateCountersignFormAsync
 */
export function createCountersignForm(data: CountersignFormCreate): Promise<CountersignForm> {
  return request({
    url: countersignFormUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CountersignForm
 * 对应后端：UpdateCountersignFormAsync
 */
export function updateCountersignForm(id: string, data: CountersignFormUpdate): Promise<CountersignForm> {
  return request({
    url: `${countersignFormUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CountersignForm（单条）
 * 对应后端：DeleteCountersignFormByIdAsync
 */
export function deleteCountersignFormById(id: string): Promise<void> {
  return request({
    url: `${countersignFormUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CountersignForm
 * 对应后端：DeleteCountersignFormBatchAsync
 */
export function deleteCountersignFormBatch(ids: string[]): Promise<void> {
  return request({
    url: `${countersignFormUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCountersignFormTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCountersignFormTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${countersignFormUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CountersignForm
 * 对应后端：ImportCountersignFormAsync
 */
export function importCountersignFormData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${countersignFormUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CountersignForm
 * 对应后端：ExportCountersignFormAsync；fileName 仅传名称不含后缀
 */
export function exportCountersignFormData(query: CountersignFormQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${countersignFormUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
