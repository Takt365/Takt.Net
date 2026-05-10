// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/code/generator/gen-engine
// 文件名称：gen-workflow.ts
// 功能描述：代码生成工作流API，对应后端 TaktGenWorkflowsController
// 路由前缀：api/TaktGenWorkflows
// ========================================

import request from '@/api/request'
import type { TaktApiResult } from '@/types/common'

/** 数据库表信息DTO */
export interface TaktDatabaseTableInfoDto {
  tableName: string
  tableDescription: string
}

/** 导入表请求DTO */
export interface TaktImportTableFromDatabaseRequestDto {
  configId: string
  tableName: string
  tableOverrides?: Record<string, unknown>
}

/** 代码生成表DTO */
export interface TaktGenTableDto {
  id: number
  tableName: string
  entityName: string
  // ... 其他字段
}

/** 初始化表请求DTO */
export interface TaktInitializeTableFromEntityRequestDto {
  configId: string
  entityTypeFullName: string
}

/** 生成代码请求DTO */
export interface TaktGenerateCodeRequestDto {
  templates: Record<string, string>
}

/** 代码生成结果DTO */
export interface TaktCodeGenResultDto {
  fileName: string
  content: string
}

/** 预览代码请求DTO */
export interface TaktPreviewCodeRequestDto {
  templates: Record<string, string>
  pathMappings?: Record<string, string>
  targetBasePath?: string
}

/** 预览结果DTO */
export interface TaktCodeGenPreviewResultDto {
  files: Array<{
    relativePath: string
    content: string
    exists: boolean
  }>
}

// ========================================
// 代码生成工作流API
// ========================================
const genWorkflowUrl = 'api/TaktGenWorkflows'

/**
 * 获取指定数据库配置下的所有数据表列表
 * 对应后端：GetDatabaseTablesAsync
 */
export function getDatabaseTables(configId: string): Promise<TaktApiResult<TaktDatabaseTableInfoDto[]>> {
  return request({
    url: `${genWorkflowUrl}/database/tables`,
    method: 'get',
    params: { configId }
  })
}

/**
 * 从数据库导入表结构到代码生成配置（有表导入）
 * 对应后端：ImportTableFromDatabaseAsync
 */
export function importTableFromDatabase(data: TaktImportTableFromDatabaseRequestDto): Promise<TaktApiResult<TaktGenTableDto>> {
  return request({
    url: `${genWorkflowUrl}/database/import`,
    method: 'post',
    data
  })
}

/**
 * 获取可用于"按实体初始化表"的实体类型全名列表
 * 对应后端：GetAvailableEntityTypesAsync
 */
export function getAvailableEntityTypes(): Promise<TaktApiResult<string[]>> {
  return request({
    url: `${genWorkflowUrl}/entities`,
    method: 'get'
  })
}

/**
 * 根据实体类型初始化数据表（无表流程）
 * 对应后端：InitializeTableFromEntityAsync
 */
export function initializeTableFromEntity(data: TaktInitializeTableFromEntityRequestDto): Promise<TaktApiResult<{ message: string }>> {
  return request({
    url: `${genWorkflowUrl}/entities/initialize`,
    method: 'post',
    data
  })
}

/**
 * 根据表配置和模板生成代码
 * 对应后端：GenerateCodeAsync
 */
export function generateCode(tableId: number, data: TaktGenerateCodeRequestDto): Promise<TaktApiResult<TaktCodeGenResultDto[]>> {
  return request({
    url: `${genWorkflowUrl}/generate/${tableId}`,
    method: 'post',
    data
  })
}

/**
 * 预览生成的代码文件（不落盘）
 * 对应后端：PreviewCodeAsync
 */
export function previewCode(tableId: number, data: TaktPreviewCodeRequestDto): Promise<TaktApiResult<TaktCodeGenPreviewResultDto>> {
  return request({
    url: `${genWorkflowUrl}/preview/${tableId}`,
    method: 'post',
    data
  })
}