// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/document
// 文件名称：document.ts
// 功能描述：文控中心文档 API，对应后端 TaktDocumentsController
// ========================================

import request from '../request'
import type { TaktPagedResult } from '@/types/common'
import type {
  Document,
  DocumentQuery,
  DocumentCreate,
  DocumentUpdate,
  DocumentStatus
} from '@/types/routine/document'
import type { TaktSelectOption } from '@/types/common'

const BASE = '/api/TaktDocuments'

/** 获取文档列表（分页） */
export function getDocumentList(params: DocumentQuery): Promise<TaktPagedResult<Document>> {
  return request({
    url: `${BASE}/list`,
    method: 'get',
    params
  })
}

/** 根据ID获取文档 */
export function getDocumentById(id: string): Promise<Document> {
  return request({
    url: `${BASE}/${id}`,
    method: 'get'
  })
}

/** 获取文档选项列表（用于下拉框等） */
export function getDocumentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${BASE}/options`,
    method: 'get'
  })
}

/** 创建文档 */
export function createDocument(data: DocumentCreate): Promise<Document> {
  return request({
    url: BASE,
    method: 'post',
    data
  })
}

/** 更新文档 */
export function updateDocument(id: string, data: DocumentUpdate): Promise<Document> {
  return request({
    url: `${BASE}/${id}`,
    method: 'put',
    data
  })
}

/** 删除文档 */
export function deleteDocument(id: string): Promise<void> {
  return request({
    url: `${BASE}/${id}`,
    method: 'delete'
  })
}

/** 批量删除文档 */
export function deleteDocumentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${BASE}/batch`,
    method: 'delete',
    data: ids.map(id => Number(id))
  })
}

/** 更新文档状态 */
export function updateDocumentStatus(data: DocumentStatus): Promise<Document> {
  return request({
    url: `${BASE}/status`,
    method: 'put',
    data
  })
}
