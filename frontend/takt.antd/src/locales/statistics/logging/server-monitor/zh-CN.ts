/**
 * 服务器监控模块 · 中文
 * - 仅包含前端 UI 交互文本
 * - 实体字段翻译由后端种子数据 TaktServerMonitorI18nSeedData.cs 统一管理
 * - 前端使用 t('servermonitor.xxx') 直接复用后端翻译键
 */
export default {
  page: {
    title: '服务器监控',
    description: '查看服务器硬件信息和应用运行状态',
    refreshCache: '刷新缓存',
    refreshSuccess: '硬件信息缓存已刷新'
  }
}
