// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/code/generator/code-generator-specific
// 文件名称：code-generator-specific.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：code-generator-specific相关类型定义（自动生成）
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
 * GenerateCodeRequest类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktGenerateCodeRequestDto）
 */
export interface GenerateCodeRequest {
}

/**
 * GenTable类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktGenTableDto）
 */
export interface GenTable {
  /** 对应后端字段 genTableId */
  genTableId: string
  /** 对应后端字段 dataSource */
  dataSource?: string
  /** 对应后端字段 tableName */
  tableName: string
  /** 对应后端字段 tableComment */
  tableComment?: string
  /** 对应后端字段 subTableName */
  subTableName?: string
  /** 对应后端字段 subTableFkName */
  subTableFkName?: string
  /** 对应后端字段 treeCode */
  treeCode?: string
  /** 对应后端字段 treeParentCode */
  treeParentCode?: string
  /** 对应后端字段 treeName */
  treeName?: string
  /** 对应后端字段 inDatabase */
  inDatabase: number
  /** 对应后端字段 genTemplateCategory */
  genTemplateCategory: string
  /** 对应后端字段 genModuleName */
  genModuleName?: string
  /** 对应后端字段 genBusinessName */
  genBusinessName: string
  /** 对应后端字段 genFunctionName */
  genFunctionName?: string
  /** 对应后端字段 permsPrefix */
  permsPrefix: string
  /** 对应后端字段 menuButtonGroup */
  menuButtonGroup?: string
  /** 对应后端字段 namePrefix */
  namePrefix?: string
  /** 对应后端字段 entityNamespace */
  entityNamespace?: string
  /** 对应后端字段 entityClassName */
  entityClassName: string
  /** 对应后端字段 dtoNamespace */
  dtoNamespace?: string
  /** 对应后端字段 dtoClassName */
  dtoClassName?: string
  /** 对应后端字段 serviceNamespace */
  serviceNamespace?: string
  /** 对应后端字段 iServiceClassName */
  iServiceClassName?: string
  /** 对应后端字段 serviceClassName */
  serviceClassName?: string
  /** 对应后端字段 controllerNamespace */
  controllerNamespace?: string
  /** 对应后端字段 controllerClassName */
  controllerClassName?: string
  /** 对应后端字段 isRepository */
  isRepository: number
  /** 对应后端字段 repositoryInterfaceNamespace */
  repositoryInterfaceNamespace?: string
  /** 对应后端字段 iRepositoryClassName */
  iRepositoryClassName?: string
  /** 对应后端字段 repositoryNamespace */
  repositoryNamespace?: string
  /** 对应后端字段 repositoryClassName */
  repositoryClassName?: string
  /** 对应后端字段 genFunction */
  genFunction?: string
  /** 对应后端字段 genMethod */
  genMethod: number
  /** 对应后端字段 genPath */
  genPath: string
  /** 对应后端字段 isGenMenu */
  isGenMenu: number
  /** 对应后端字段 parentMenuId */
  parentMenuId: string
  /** 对应后端字段 isGenTranslation */
  isGenTranslation: number
  /** 对应后端字段 sortField */
  sortField: string
  /** 对应后端字段 sortType */
  sortType: string
  /** 对应后端字段 frontUi */
  frontUi: number
  /** 对应后端字段 frontFormLayout */
  frontFormLayout: number
  /** 对应后端字段 frontBtnStyle */
  frontBtnStyle: number
  /** 对应后端字段 isGenCode */
  isGenCode: number
  /** 对应后端字段 genCodeCount */
  genCodeCount: number
  /** 对应后端字段 isUseTabs */
  isUseTabs: number
  /** 对应后端字段 tabsFieldCount */
  tabsFieldCount: number
  /** 对应后端字段 genAuthor */
  genAuthor: string
  /** 对应后端字段 otherGenOptions */
  otherGenOptions?: string
  /** 对应后端字段 columnIds */
  columnIds?: string[]
}

/**
 * GenTableCreate类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktGenTableCreateDto）
 */
export interface GenTableCreate {
  /** 对应后端字段 dataSource */
  dataSource?: string
  /** 对应后端字段 tableName */
  tableName: string
  /** 对应后端字段 tableComment */
  tableComment?: string
  /** 对应后端字段 subTableName */
  subTableName?: string
  /** 对应后端字段 subTableFkName */
  subTableFkName?: string
  /** 对应后端字段 treeCode */
  treeCode?: string
  /** 对应后端字段 treeParentCode */
  treeParentCode?: string
  /** 对应后端字段 treeName */
  treeName?: string
  /** 对应后端字段 inDatabase */
  inDatabase: number
  /** 对应后端字段 genTemplateCategory */
  genTemplateCategory: string
  /** 对应后端字段 genModuleName */
  genModuleName?: string
  /** 对应后端字段 genBusinessName */
  genBusinessName: string
  /** 对应后端字段 genFunctionName */
  genFunctionName?: string
  /** 对应后端字段 permsPrefix */
  permsPrefix: string
  /** 对应后端字段 menuButtonGroup */
  menuButtonGroup?: string
  /** 对应后端字段 namePrefix */
  namePrefix?: string
  /** 对应后端字段 entityNamespace */
  entityNamespace?: string
  /** 对应后端字段 entityClassName */
  entityClassName: string
  /** 对应后端字段 dtoNamespace */
  dtoNamespace?: string
  /** 对应后端字段 dtoClassName */
  dtoClassName?: string
  /** 对应后端字段 serviceNamespace */
  serviceNamespace?: string
  /** 对应后端字段 iServiceClassName */
  iServiceClassName?: string
  /** 对应后端字段 serviceClassName */
  serviceClassName?: string
  /** 对应后端字段 controllerNamespace */
  controllerNamespace?: string
  /** 对应后端字段 controllerClassName */
  controllerClassName?: string
  /** 对应后端字段 isRepository */
  isRepository: number
  /** 对应后端字段 repositoryInterfaceNamespace */
  repositoryInterfaceNamespace?: string
  /** 对应后端字段 iRepositoryClassName */
  iRepositoryClassName?: string
  /** 对应后端字段 repositoryNamespace */
  repositoryNamespace?: string
  /** 对应后端字段 repositoryClassName */
  repositoryClassName?: string
  /** 对应后端字段 genFunction */
  genFunction?: string
  /** 对应后端字段 genMethod */
  genMethod: number
  /** 对应后端字段 genPath */
  genPath: string
  /** 对应后端字段 isGenMenu */
  isGenMenu: number
  /** 对应后端字段 parentMenuId */
  parentMenuId: string
  /** 对应后端字段 isGenTranslation */
  isGenTranslation: number
  /** 对应后端字段 sortField */
  sortField: string
  /** 对应后端字段 sortType */
  sortType: string
  /** 对应后端字段 frontUi */
  frontUi: number
  /** 对应后端字段 frontFormLayout */
  frontFormLayout: number
  /** 对应后端字段 frontBtnStyle */
  frontBtnStyle: number
  /** 对应后端字段 isGenCode */
  isGenCode: number
  /** 对应后端字段 genCodeCount */
  genCodeCount: number
  /** 对应后端字段 isUseTabs */
  isUseTabs: number
  /** 对应后端字段 tabsFieldCount */
  tabsFieldCount: number
  /** 对应后端字段 genAuthor */
  genAuthor: string
  /** 对应后端字段 otherGenOptions */
  otherGenOptions?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ImportTableFromDatabaseRequest类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktImportTableFromDatabaseRequestDto）
 */
export interface ImportTableFromDatabaseRequest {
  /** 对应后端字段 configId */
  configId: string
  /** 对应后端字段 tableName */
  tableName: string
  /** 对应后端字段 tableOverrides */
  tableOverrides?: unknown
}

/**
 * InitializeTableFromEntityRequest类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktInitializeTableFromEntityRequestDto）
 */
export interface InitializeTableFromEntityRequest {
  /** 对应后端字段 configId */
  configId: string
  /** 对应后端字段 entityTypeFullName */
  entityTypeFullName: string
}

/**
 * IsGenCode类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktIsGenCodeDto）
 */
export interface IsGenCode {
  /** 对应后端字段 genTableId */
  genTableId: string
  /** 对应后端字段 isGenCode */
  isGenCode: number
}

/**
 * PreviewCodeRequest类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktPreviewCodeRequestDto）
 */
export interface PreviewCodeRequest {
  /** 对应后端字段 targetBasePath */
  targetBasePath?: string
}
