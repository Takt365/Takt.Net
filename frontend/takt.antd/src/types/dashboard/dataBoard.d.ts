/**
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 命名空间：@/types/dashboard/dataBoard
 * 文件名称：dataBoard.d.ts
 * 功能描述：数据看板统计模块类型与配置（ModuleItem / ModuleKey / ModuleMeta）
 * 版权信息：Copyright (c) 2025 Takt. All rights reserved.
 */

/** 数据看板单个模块配置（用于布局持久化） */
export interface DataBoardModuleItem {
  /** 唯一 id */
  id: string
  /** 模块类型 key，对应注册的模块组件 */
  moduleKey: DataBoardModuleKey
  /** 栅格占位 1-24，默认 24 */
  span?: number
  /** 自定义模块标题（仅 moduleKey 为 custom 时使用） */
  customTitle?: string
  /** 自定义模块内容（仅 moduleKey 为 custom 时使用） */
  customContent?: string
}

/** 可选的统计模块类型（6 个数据仪表 + 自定义） */
export type DataBoardModuleKey =
  | 'accounting'   // 会计核算
  | 'logistics'    // 后勤管理
  | 'workflow'     // 工作流程
  | 'humanResource' // 人力资源
  | 'routine'      // 日常事务
  | 'onlineUsers'  // 在线用户
  | 'custom'

/** 可选模块元信息（用于「添加统计模块」列表） */
export interface DataBoardModuleMeta {
  key: DataBoardModuleKey
  /** 多语言 key，如 dashboard.dataBoard.modules.overview */
  titleKey: string
  /** 默认栅格占位 */
  defaultSpan: number
}
