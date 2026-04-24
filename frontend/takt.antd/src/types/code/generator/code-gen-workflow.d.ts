// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/code/generator/code-gen-workflow
// 文件名称：code-gen-workflow.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：code-gen-workflow相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CodeGenPreviewFile类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktCodeGenPreviewFileDto）
 */
export interface CodeGenPreviewFile {
  /** 对应后端字段 path */
  path: string
  /** 对应后端字段 content */
  content: string
  /** 对应后端字段 isExisting */
  isExisting: boolean
}

/**
 * CodeGenPreviewResult类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktCodeGenPreviewResultDto）
 */
export interface CodeGenPreviewResult {
  /** 对应后端字段 isValid */
  isValid: boolean
  /** 对应后端字段 previewFiles */
  previewFiles: unknown[]
  /** 对应后端字段 validationIssues */
  validationIssues: unknown[]
}

/**
 * CodeGenPreviewValidationIssue类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktCodeGenPreviewValidationIssueDto）
 */
export interface CodeGenPreviewValidationIssue {
  /** 对应后端字段 templateKey */
  templateKey: string
  /** 对应后端字段 targetPath */
  targetPath?: string
  /** 对应后端字段 message */
  message: string
}

/**
 * CodeGenResult类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktCodeGenResultDto）
 */
export interface CodeGenResult {
  /** 对应后端字段 fileName */
  fileName: string
  /** 对应后端字段 content */
  content: string
}

/**
 * DatabaseTableInfo类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktDatabaseTableInfoDto）
 */
export interface DatabaseTableInfo {
  /** 对应后端字段 tableName */
  tableName: string
  /** 对应后端字段 tableComment */
  tableComment?: string
}

/**
 * ImportTableRequest类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktImportTableRequestDto）
 */
export interface ImportTableRequest {
  /** 对应后端字段 configId */
  configId: string
  /** 对应后端字段 tableName */
  tableName: string
  /** 对应后端字段 tableOverrides */
  tableOverrides?: unknown
}
