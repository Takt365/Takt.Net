/**
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 命名空间：@/views/dashboard/data-board
 * 文件名称：config.ts
 * 功能描述：数据看板模块默认配置与 localStorage 存储 key
 * 版权信息：Copyright (c) 2025 Takt. All rights reserved.
 */
import type { DataBoardModuleItem, DataBoardModuleMeta } from '@/types/dashboard/dataBoard'

export const DATA_BOARD_STORAGE_KEY = 'takt-data-board-modules'

/** 可添加的统计模块类型列表（6 个数据仪表 + 自定义） */
export const DATA_BOARD_AVAILABLE_MODULES: DataBoardModuleMeta[] = [
  { key: 'accounting', titleKey: 'dashboard.dataBoard.modules.accounting', defaultSpan: 12 },
  { key: 'logistics', titleKey: 'dashboard.dataBoard.modules.logistics', defaultSpan: 12 },
  { key: 'workflow', titleKey: 'dashboard.dataBoard.modules.workflow', defaultSpan: 12 },
  { key: 'humanResource', titleKey: 'dashboard.dataBoard.modules.humanResource', defaultSpan: 12 },
  { key: 'routine', titleKey: 'dashboard.dataBoard.modules.routine', defaultSpan: 12 },
  { key: 'onlineUsers', titleKey: 'dashboard.dataBoard.modules.onlineUsers', defaultSpan: 12 },
  { key: 'custom', titleKey: 'dashboard.dataBoard.modules.custom', defaultSpan: 24 }
]

/** 默认数据看板模块（首次进入或未持久化时）：6 个数据仪表，两列布局 */
export function getDefaultDataBoardModules(): DataBoardModuleItem[] {
  return [
    { id: 'accounting-1', moduleKey: 'accounting', span: 12 },
    { id: 'logistics-1', moduleKey: 'logistics', span: 12 },
    { id: 'workflow-1', moduleKey: 'workflow', span: 12 },
    { id: 'humanResource-1', moduleKey: 'humanResource', span: 12 },
    { id: 'routine-1', moduleKey: 'routine', span: 12 },
    { id: 'onlineUsers-1', moduleKey: 'onlineUsers', span: 12 }
  ]
}

export function generateDataBoardModuleId(moduleKey: string): string {
  return `${moduleKey}-${Date.now()}`
}
