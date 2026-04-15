// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/generator
// 文件名称：table-column.ts
// 功能描述：代码生成字段配置 API，对应后端 TaktGenTableColumnsController
// ========================================

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  GenTableColumn,
  GenTableColumnQuery,
  GenTableColumnCreate,
  GenTableColumnUpdate
} from '@/types/generator/table-column'

export type { GenTableColumn, GenTableColumnQuery, GenTableColumnCreate, GenTableColumnUpdate } from '@/types/generator/table-column'

const tableColumnUrl = '/api/TaktGenTableColumns'

/** 获取代码生成字段配置列表（分页），对应后端 GetListAsync */
export function getGenTableColumnList(params: GenTableColumnQuery): Promise<TaktPagedResult<GenTableColumn>> {
  return request({
    url: `${tableColumnUrl}/list`,
    method: 'get',
    params
  })
}

/** 根据 ID 获取代码生成字段配置，对应后端 GetByIdAsync */
export function getGenTableColumnById(id: string): Promise<GenTableColumn> {
  return request({
    url: `${tableColumnUrl}/${id}`,
    method: 'get'
  })
}

/** 根据表ID获取字段配置列表，对应后端 GetListByTableIdAsync */
export function getColumnsByTableId(tableId: string): Promise<GenTableColumn[]> {
  return request({
    url: `${tableColumnUrl}/table/${tableId}`,
    method: 'get'
  })
}

/** 创建代码生成字段配置，对应后端 CreateAsync */
export function createGenTableColumn(data: GenTableColumnCreate): Promise<GenTableColumn> {
  return request({
    url: tableColumnUrl,
    method: 'post',
    data
  })
}

/** 批量创建代码生成字段配置，对应后端 CreateBatchAsync */
export function createGenTableColumnBatch(data: GenTableColumnCreate[]): Promise<GenTableColumn[]> {
  return request({
    url: `${tableColumnUrl}/batch`,
    method: 'post',
    data
  })
}

/** 更新代码生成字段配置，对应后端 UpdateAsync */
export function updateGenTableColumn(id: string, data: GenTableColumnUpdate): Promise<GenTableColumn> {
  return request({
    url: `${tableColumnUrl}/${id}`,
    method: 'put',
    data
  })
}

/** 删除代码生成字段配置，对应后端 DeleteAsync */
export function deleteGenTableColumn(id: string): Promise<void> {
  return request({
    url: `${tableColumnUrl}/${id}`,
    method: 'delete'
  })
}

/** 根据表ID删除所有字段配置，对应后端 DeleteByTableIdAsync */
export function deleteGenTableColumnsByTableId(tableId: string): Promise<void> {
  return request({
    url: `${tableColumnUrl}/table/${tableId}`,
    method: 'delete'
  })
}
