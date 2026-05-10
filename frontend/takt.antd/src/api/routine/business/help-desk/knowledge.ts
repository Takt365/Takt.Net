// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/help-desk
// 文件名称：knowledge.ts
// 功能描述：Knowledge API，对应后端 Takt.WebApi.Controllers.Routine.Business.HelpDesk.TaktKnowledges
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Knowledge,
  KnowledgeQuery,
  KnowledgeCreate,
  KnowledgeUpdate,
  KnowledgeStatus,
  KnowledgeSort
} from '@/types/routine/business/help-desk/knowledge'

// ========================================
// Knowledge相关 API（按后端控制器顺序）
// ========================================
const knowledgeUrl = '/api/TaktKnowledges';

/**
 * 获取Knowledge列表（分页）
 * 对应后端：GetKnowledgeListAsync
 */
export function getKnowledgeList(params: KnowledgeQuery): Promise<TaktPagedResult<Knowledge>> {
  return request({
    url: `${knowledgeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Knowledge
 * 对应后端：GetKnowledgeByIdAsync
 */
export function getKnowledgeById(id: string): Promise<Knowledge> {
  return request({
    url: `${knowledgeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Knowledge选项列表（用于下拉框等）
 * 对应后端：GetKnowledgeOptionsAsync
 */
export function getKnowledgeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${knowledgeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Knowledge
 * 对应后端：CreateKnowledgeAsync
 */
export function createKnowledge(data: KnowledgeCreate): Promise<Knowledge> {
  return request({
    url: knowledgeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Knowledge
 * 对应后端：UpdateKnowledgeAsync
 */
export function updateKnowledge(id: string, data: KnowledgeUpdate): Promise<Knowledge> {
  return request({
    url: `${knowledgeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Knowledge（单条）
 * 对应后端：DeleteKnowledgeByIdAsync
 */
export function deleteKnowledgeById(id: string): Promise<void> {
  return request({
    url: `${knowledgeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Knowledge
 * 对应后端：DeleteKnowledgeBatchAsync
 */
export function deleteKnowledgeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${knowledgeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Knowledge状态
 * 对应后端：UpdateKnowledgeStatusAsync
 */
export function updateKnowledgeStatus(data: KnowledgeStatus): Promise<KnowledgeStatus> {
  return request({
    url: `${knowledgeUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新Knowledge排序
 * 对应后端：UpdateKnowledgeSortAsync
 */
export function updateKnowledgeSort(data: KnowledgeSort): Promise<KnowledgeSort> {
  return request({
    url: `${knowledgeUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetKnowledgeTemplateAsync；fileName 仅传名称不含后缀
 */
export function getKnowledgeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${knowledgeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Knowledge
 * 对应后端：ImportKnowledgeAsync
 */
export function importKnowledgeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${knowledgeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Knowledge
 * 对应后端：ExportKnowledgeAsync；fileName 仅传名称不含后缀
 */
export function exportKnowledgeData(query: KnowledgeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${knowledgeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
