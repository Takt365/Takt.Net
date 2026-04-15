/**
 * 工作台模块类型与配置
 */

/** 工作台单个模块配置（用于布局持久化） */
export interface WorkspaceModuleItem {
  /** 唯一 id */
  id: string
  /** 模块类型 key，对应注册的模块组件 */
  moduleKey: WorkspaceModuleKey
  /** 栅格占位 1-24，默认 24 */
  span?: number
  /** 自定义模块的标题（仅 moduleKey 为 custom 时使用） */
  customTitle?: string
  /** 自定义模块的内容（仅 moduleKey 为 custom 时使用） */
  customContent?: string
}

/** 可选的模块类型 */
export type WorkspaceModuleKey =
  | 'welcome'
  | 'shortcut'
  | 'todo'
  | 'notice'
  | 'custom'

/** 可选模块元信息（用于“添加模块”列表） */
export interface WorkspaceModuleMeta {
  key: WorkspaceModuleKey
  /** 多语言 key，如 dashboard.workspace.modules.welcome */
  titleKey: string
  /** 默认栅格占位 */
  defaultSpan: number
}
