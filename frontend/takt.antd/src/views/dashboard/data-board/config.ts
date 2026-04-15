/**
 * 数据看板统计模块默认配置与存储 key（与工作台一致的可添加模块方式）
 */
import type { DataBoardModuleItem, DataBoardModuleMeta } from '@/types/dashboard/data-board'

export const DATA_BOARD_STORAGE_KEY = 'takt-data-board-modules'

/** 可添加的统计模块类型列表 */
export const DATA_BOARD_AVAILABLE_MODULES: DataBoardModuleMeta[] = [
  { key: 'overview', titleKey: 'dashboard.data-board.modules.overview', defaultSpan: 12 },
  { key: 'change', titleKey: 'dashboard.data-board.modules.change', defaultSpan: 12 },
  { key: 'online', titleKey: 'dashboard.data-board.modules.online', defaultSpan: 12 },
  { key: 'sales', titleKey: 'dashboard.data-board.modules.sales', defaultSpan: 12 },
  { key: 'production', titleKey: 'dashboard.data-board.modules.production', defaultSpan: 12 },
  { key: 'custom', titleKey: 'dashboard.data-board.modules.custom', defaultSpan: 24 }
]

/** 默认数据看板模块（首次进入或未持久化时） */
export function getDefaultDataBoardModules(): DataBoardModuleItem[] {
  return [
    { id: 'overview-1', moduleKey: 'overview', span: 24 }
  ]
}

export function generateModuleId(moduleKey: string): string {
  return `${moduleKey}-${Date.now()}`
}
