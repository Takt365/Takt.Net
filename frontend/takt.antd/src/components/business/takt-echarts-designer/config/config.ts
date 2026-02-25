// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/components/business/takt-echarts-designer/config
// 文件名称：config.ts
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：ECharts 设计器默认高度、图表类型与预设 option
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { EChartsOption } from 'echarts'

/** 设计器画布默认高度（px） */
export const DEFAULT_CHART_HEIGHT = 400

/** 右侧选项面板默认宽度（px） */
export const OPTION_PANEL_WIDTH = 320

/** 图表类型与预设 option 的 key */
export const CHART_TYPES = [
  { value: 'bar', labelKey: 'components.echarts-designer.chartType.bar' },
  { value: 'line', labelKey: 'components.echarts-designer.chartType.line' },
  { value: 'pie', labelKey: 'components.echarts-designer.chartType.pie' },
  { value: 'scatter', labelKey: 'components.echarts-designer.chartType.scatter' }
] as const

export type ChartTypeValue = (typeof CHART_TYPES)[number]['value']

/** 获取各图表类型的默认 option（用于设计器初始或切换类型） */
export function getPresetOption(type: ChartTypeValue): EChartsOption {
  switch (type) {
    case 'bar':
      return {
        title: { text: '柱状图示例', left: 'center' },
        tooltip: { trigger: 'axis' },
        xAxis: { type: 'category', data: ['A', 'B', 'C', 'D', 'E'] },
        yAxis: { type: 'value' },
        series: [{ name: '销量', type: 'bar', data: [120, 200, 150, 80, 70] }]
      }
    case 'line':
      return {
        title: { text: '折线图示例', left: 'center' },
        tooltip: { trigger: 'axis' },
        xAxis: { type: 'category', data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri'] },
        yAxis: { type: 'value' },
        series: [{ name: '数据', type: 'line', data: [150, 230, 224, 218, 135] }]
      }
    case 'pie':
      return {
        title: { text: '饼图示例', left: 'center' },
        tooltip: { trigger: 'item' },
        series: [{
          name: '占比',
          type: 'pie',
          radius: '50%',
          data: [
            { value: 1048, name: 'A' },
            { value: 735, name: 'B' },
            { value: 580, name: 'C' },
            { value: 484, name: 'D' },
            { value: 300, name: 'E' }
          ]
        }]
      }
    case 'scatter':
      return {
        title: { text: '散点图示例', left: 'center' },
        tooltip: { trigger: 'item' },
        xAxis: { type: 'value' },
        yAxis: { type: 'value' },
        series: [{
          name: '散点',
          type: 'scatter',
          data: [[10, 20], [30, 40], [50, 60], [70, 80], [90, 100]]
        }]
      }
    default:
      return getPresetOption('bar')
  }
}

/** 空 option，仅保留基础结构，用于「清空」后由用户从 JSON 编辑 */
export const EMPTY_OPTION: EChartsOption = {
  title: { text: '', left: 'center' },
  tooltip: {},
  series: []
}
