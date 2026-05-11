// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/complaint
// 文件名称：customer-satisfaction-survey.ts
// 功能描述：CustomerSatisfactionSurvey API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveys
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CustomerSatisfactionSurvey,
  CustomerSatisfactionSurveyQuery,
  CustomerSatisfactionSurveyCreate,
  CustomerSatisfactionSurveyUpdate,
  CustomerSatisfactionSurveySort
} from '@/types/logistics/quality/complaint/customer-satisfaction-survey'

// ========================================
// CustomerSatisfactionSurvey相关 API（按后端控制器顺序）
// ========================================
const customerSatisfactionSurveyUrl = '/api/TaktCustomerSatisfactionSurveys';

/**
 * 获取CustomerSatisfactionSurvey列表（分页）
 * 对应后端：GetCustomerSatisfactionSurveyListAsync
 */
export function getCustomerSatisfactionSurveyList(params: CustomerSatisfactionSurveyQuery): Promise<TaktPagedResult<CustomerSatisfactionSurvey>> {
  return request({
    url: `${customerSatisfactionSurveyUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取CustomerSatisfactionSurvey
 * 对应后端：GetCustomerSatisfactionSurveyByIdAsync
 */
export function getCustomerSatisfactionSurveyById(id: string): Promise<CustomerSatisfactionSurvey> {
  return request({
    url: `${customerSatisfactionSurveyUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取CustomerSatisfactionSurvey选项列表（用于下拉框等）
 * 对应后端：GetCustomerSatisfactionSurveyOptionsAsync
 */
export function getCustomerSatisfactionSurveyOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${customerSatisfactionSurveyUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建CustomerSatisfactionSurvey
 * 对应后端：CreateCustomerSatisfactionSurveyAsync
 */
export function createCustomerSatisfactionSurvey(data: CustomerSatisfactionSurveyCreate): Promise<CustomerSatisfactionSurvey> {
  return request({
    url: customerSatisfactionSurveyUrl,
    method: 'post',
    data
  })
}

/**
 * 更新CustomerSatisfactionSurvey
 * 对应后端：UpdateCustomerSatisfactionSurveyAsync
 */
export function updateCustomerSatisfactionSurvey(id: string, data: CustomerSatisfactionSurveyUpdate): Promise<CustomerSatisfactionSurvey> {
  return request({
    url: `${customerSatisfactionSurveyUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除CustomerSatisfactionSurvey（单条）
 * 对应后端：DeleteCustomerSatisfactionSurveyByIdAsync
 */
export function deleteCustomerSatisfactionSurveyById(id: string): Promise<void> {
  return request({
    url: `${customerSatisfactionSurveyUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除CustomerSatisfactionSurvey
 * 对应后端：DeleteCustomerSatisfactionSurveyBatchAsync
 */
export function deleteCustomerSatisfactionSurveyBatch(ids: string[]): Promise<void> {
  return request({
    url: `${customerSatisfactionSurveyUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新CustomerSatisfactionSurvey排序
 * 对应后端：UpdateCustomerSatisfactionSurveySortAsync
 */
export function updateCustomerSatisfactionSurveySort(data: CustomerSatisfactionSurveySort): Promise<CustomerSatisfactionSurveySort> {
  return request({
    url: `${customerSatisfactionSurveyUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCustomerSatisfactionSurveyTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCustomerSatisfactionSurveyTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${customerSatisfactionSurveyUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入CustomerSatisfactionSurvey
 * 对应后端：ImportCustomerSatisfactionSurveyAsync
 */
export function importCustomerSatisfactionSurveyData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${customerSatisfactionSurveyUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出CustomerSatisfactionSurvey
 * 对应后端：ExportCustomerSatisfactionSurveyAsync；fileName 仅传名称不含后缀
 */
export function exportCustomerSatisfactionSurveyData(query: CustomerSatisfactionSurveyQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${customerSatisfactionSurveyUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
