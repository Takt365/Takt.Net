// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/generator
// 文件名称：table.d.ts
// 功能描述：代码生成表配置类型，对应后端 TaktGenTableDto / TaktGenTableQueryDto 等
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'
import type { GenTableColumn } from './table-column'

/** 代码生成表配置（对应后端 TaktGenTableDto，继承 TaktEntityBase） */
export interface GenTable extends TaktEntityBase {
  /** 表ID（对应后端 Id，序列化为 string 以避免精度问题） */
  tableId: string
  /** 数据源（JSON 格式存储，如：Takt_Identity_Dev:0 表示 显示名:ConfigId） */
  dataSource?: string
  tableName?: string
  tableComment?: string
  subTableName?: string
  subTableFkName?: string
  treeCode?: string
  treeParentCode?: string
  treeName?: string
  inDatabase?: number
  genTemplate?: string
  namePrefix?: string
  entityNamespace?: string
  entityClassName?: string
  dtoNamespace?: string
  dtoClassName?: string
  dtoCategory?: string
  serviceNamespace?: string
  iServiceClassName?: string
  serviceClassName?: string
  controllerNamespace?: string
  controllerClassName?: string
  repositoryInterfaceNamespace?: string
  iRepositoryClassName?: string
  repositoryNamespace?: string
  repositoryClassName?: string
  genModuleName?: string
  genBusinessName?: string
  genFunctionName?: string
  genFunction?: string
  genMethod?: number
  isRepository?: number
  genPath?: string
  parentMenuId?: string
  isGenMenu?: number
  isGenTranslation?: number
  sortType?: string
  sortField?: string
  permsPrefix?: string
  frontTemplate?: number
  frontStyle?: number
  btnStyle?: number
  isGenCode?: number
  genCodeCount?: number
  isUseTabs?: number
  tabsFieldCount?: number
  genAuthor?: string
  options?: string
  /** 字段配置列表（主子表关系，与后端 Columns 对应） */
  columns?: GenTableColumn[]
  [key: string]: any
}

/** 代码生成表查询（对应后端 TaktGenTableQueryDto） */
export interface GenTableQuery extends TaktPagedQuery {
  keyWords?: string
  tableName?: string
  entityClassName?: string
  genModuleName?: string
  genBusinessName?: string
}

/** 创建代码生成表（对应后端 TaktGenTableCreateDto） */
export interface GenTableCreate {
  /** 数据源（前面是数据库名称，后面是 ConfigId，如：Takt_Identity_Dev:0） */
  dataSource?: string
  tableName?: string
  tableComment?: string
  subTableName?: string
  subTableFkName?: string
  treeCode?: string
  treeParentCode?: string
  treeName?: string
  inDatabase?: number
  genTemplate?: string
  namePrefix?: string
  entityNamespace?: string
  entityClassName?: string
  dtoNamespace?: string
  dtoClassName?: string
  dtoCategory?: string
  serviceNamespace?: string
  iServiceClassName?: string
  serviceClassName?: string
  controllerNamespace?: string
  controllerClassName?: string
  repositoryInterfaceNamespace?: string
  iRepositoryClassName?: string
  repositoryNamespace?: string
  repositoryClassName?: string
  genModuleName?: string
  genBusinessName?: string
  genFunctionName?: string
  genFunction?: string
  genMethod?: number
  isRepository?: number
  genPath?: string
  parentMenuId?: string
  isGenMenu?: number
  isGenTranslation?: number
  sortType?: string
  sortField?: string
  permsPrefix?: string
  frontTemplate?: number
  frontStyle?: number
  btnStyle?: number
  isGenCode?: number
  genCodeCount?: number
  isUseTabs?: number
  tabsFieldCount?: number
  genAuthor?: string
  options?: string
  configId?: string
  [key: string]: any
}

/** 更新代码生成表（对应后端 TaktGenTableUpdateDto） */
export interface GenTableUpdate extends GenTableCreate {
  tableId: string
}
