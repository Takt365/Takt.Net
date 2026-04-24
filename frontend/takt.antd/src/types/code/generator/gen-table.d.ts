// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/code/generator/gen-table
// 文件名称：gen-table.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：gen-table相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * GenTable类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktGenTableDto）
 */
export interface GenTable extends TaktEntityBase {
  /** 对应后端字段 tableId */
  tableId: string
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
  /** 对应后端字段 genTemplate */
  genTemplate: string
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
  /** 对应后端字段 dtoCategory */
  dtoCategory?: string
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
  /** 对应后端字段 repositoryInterfaceNamespace */
  repositoryInterfaceNamespace?: string
  /** 对应后端字段 iRepositoryClassName */
  iRepositoryClassName?: string
  /** 对应后端字段 repositoryNamespace */
  repositoryNamespace?: string
  /** 对应后端字段 repositoryClassName */
  repositoryClassName?: string
  /** 对应后端字段 genModuleName */
  genModuleName?: string
  /** 对应后端字段 genBusinessName */
  genBusinessName: string
  /** 对应后端字段 genFunctionName */
  genFunctionName?: string
  /** 对应后端字段 genFunction */
  genFunction?: string
  /** 对应后端字段 genMethod */
  genMethod: number
  /** 对应后端字段 isRepository */
  isRepository: number
  /** 对应后端字段 genPath */
  genPath: string
  /** 对应后端字段 parentMenuId */
  parentMenuId: string
  /** 对应后端字段 isGenMenu */
  isGenMenu: number
  /** 对应后端字段 isGenTranslation */
  isGenTranslation: number
  /** 对应后端字段 sortType */
  sortType: string
  /** 对应后端字段 sortField */
  sortField: string
  /** 对应后端字段 permsPrefix */
  permsPrefix: string
  /** 对应后端字段 buttonGroup */
  buttonGroup?: string
  /** 对应后端字段 frontTemplate */
  frontTemplate: number
  /** 对应后端字段 frontStyle */
  frontStyle: number
  /** 对应后端字段 btnStyle */
  btnStyle: number
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
  /** 对应后端字段 options */
  options?: string
  /** 对应后端字段 columns */
  columns?: unknown[]
}

/**
 * GenTableQuery类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktGenTableQueryDto）
 */
export interface GenTableQuery extends TaktPagedQuery {
  /** 对应后端字段 tableName */
  tableName?: string
  /** 对应后端字段 entityClassName */
  entityClassName?: string
  /** 对应后端字段 genModuleName */
  genModuleName?: string
  /** 对应后端字段 genBusinessName */
  genBusinessName?: string
}

/**
 * GenTableCreate类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktGenTableCreateDto）
 */
export interface GenTableCreate {
  /** 对应后端字段 configId */
  configId: string
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
  /** 对应后端字段 genTemplate */
  genTemplate: string
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
  /** 对应后端字段 dtoCategory */
  dtoCategory?: string
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
  /** 对应后端字段 repositoryInterfaceNamespace */
  repositoryInterfaceNamespace?: string
  /** 对应后端字段 iRepositoryClassName */
  iRepositoryClassName?: string
  /** 对应后端字段 repositoryNamespace */
  repositoryNamespace?: string
  /** 对应后端字段 repositoryClassName */
  repositoryClassName?: string
  /** 对应后端字段 genModuleName */
  genModuleName?: string
  /** 对应后端字段 genBusinessName */
  genBusinessName: string
  /** 对应后端字段 genFunctionName */
  genFunctionName?: string
  /** 对应后端字段 genFunction */
  genFunction?: string
  /** 对应后端字段 genMethod */
  genMethod: number
  /** 对应后端字段 isRepository */
  isRepository: number
  /** 对应后端字段 genPath */
  genPath: string
  /** 对应后端字段 parentMenuId */
  parentMenuId: string
  /** 对应后端字段 isGenMenu */
  isGenMenu: number
  /** 对应后端字段 isGenTranslation */
  isGenTranslation: number
  /** 对应后端字段 sortType */
  sortType: string
  /** 对应后端字段 sortField */
  sortField: string
  /** 对应后端字段 permsPrefix */
  permsPrefix: string
  /** 对应后端字段 buttonGroup */
  buttonGroup?: string
  /** 对应后端字段 frontTemplate */
  frontTemplate: number
  /** 对应后端字段 frontStyle */
  frontStyle: number
  /** 对应后端字段 btnStyle */
  btnStyle: number
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
  /** 对应后端字段 options */
  options?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 columns */
  columns?: unknown[]
}

/**
 * GenTableUpdate类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktGenTableUpdateDto）
 */
export interface GenTableUpdate extends GenTableCreate {
  /** 对应后端字段 tableId */
  tableId: string
}
