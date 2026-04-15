/**
 * 数据看板统计模块类型与配置（与工作台模块模式一致）
 */

/** 数据看板单个模块配置（用于布局持久化） */
export interface DataBoardModuleItem {
  id: string
  moduleKey: DataBoardModuleKey
  span?: number
}

/** 可选的统计模块类型 */
export type DataBoardModuleKey =
  | 'overview'
  | 'change'
  | 'online'
  | 'sales'
  | 'production'
  | 'custom'

/** 可选模块元信息（用于“添加模块”列表） */
export interface DataBoardModuleMeta {
  key: DataBoardModuleKey
  titleKey: string
  defaultSpan: number
}
