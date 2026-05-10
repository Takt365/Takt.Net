// ========================================
// 项目名称:节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间:@/utils/eventBusMonitor
// 文件名称:eventBusMonitor.ts
// 创建时间:2026-05-10
// 创建人:Takt365
// 功能描述:EventBus 性能监控和调试工具
//
// 功能:
// 1. 事件触发频率统计
// 2. 事件处理性能监控
// 3. 事件订阅关系可视化
// 4. 异常事件追踪
//
// 版权信息:Copyright (c) 2025 Takt  All rights reserved.
// 免责声明:此软件使用 MIT License,作者不承担任何使用风险。
// ========================================

import { eventBus, type Events } from './eventBus'
import { logger } from '@/utils/logger'

/**
 * 事件性能数据
 */
interface EventPerformanceData {
  eventName: string
  triggerCount: number
  avgExecutionTime: number
  maxExecutionTime: number
  minExecutionTime: number
  lastTriggerTime: number
  errors: number
}

/**
 * EventBus 性能监控器
 */
class EventBusMonitor {
  private performanceMap: Map<string, EventPerformanceData> = new Map()
  private isEnabled: boolean = false
  private eventHistory: Array<{
    eventName: string
    timestamp: number
    executionTime: number
    success: boolean
    error?: string | undefined
  }> = []
  private maxHistorySize: number = 1000

  /**
   * 启用监控
   */
  enable(): void {
    if (this.isEnabled) return
    
    this.isEnabled = true
    logger.info('[EventBus Monitor] 性能监控已启用')

    // 注册中间件
    eventBus.use((type, event) => {
      if (!this.isEnabled) return
      
      const eventName = String(type)
      const startTime = performance.now()
      
      // 记录事件触发
      this.recordEventTrigger(eventName)
      
      // 使用 setTimeout 异步记录性能数据
      setTimeout(() => {
        const executionTime = performance.now() - startTime
        this.recordEventPerformance(eventName, executionTime, true)
      }, 0)
    })
  }

  /**
   * 禁用监控
   */
  disable(): void {
    this.isEnabled = false
    logger.info('[EventBus Monitor] 性能监控已禁用')
  }

  /**
   * 记录事件触发
   */
  private recordEventTrigger(eventName: string): void {
    if (!this.performanceMap.has(eventName)) {
      this.performanceMap.set(eventName, {
        eventName,
        triggerCount: 0,
        avgExecutionTime: 0,
        maxExecutionTime: 0,
        minExecutionTime: Infinity,
        lastTriggerTime: 0,
        errors: 0
      })
    }

    const data = this.performanceMap.get(eventName)!
    data.triggerCount++
    data.lastTriggerTime = Date.now()
  }

  /**
   * 记录事件性能
   */
  private recordEventPerformance(
    eventName: string,
    executionTime: number,
    success: boolean,
    error?: string
  ): void {
    const data = this.performanceMap.get(eventName)
    if (!data) return

    // 更新性能统计
    data.avgExecutionTime = (data.avgExecutionTime * (data.triggerCount - 1) + executionTime) / data.triggerCount
    data.maxExecutionTime = Math.max(data.maxExecutionTime, executionTime)
    data.minExecutionTime = Math.min(data.minExecutionTime, executionTime)

    if (!success) {
      data.errors++
    }

    // 记录到历史
    this.eventHistory.push({
      eventName,
      timestamp: Date.now(),
      executionTime,
      success,
      error
    })

    // 限制历史记录大小
    if (this.eventHistory.length > this.maxHistorySize) {
      this.eventHistory = this.eventHistory.slice(-this.maxHistorySize)
    }
  }

  /**
   * 获取所有事件的性能数据
   */
  getAllPerformanceData(): EventPerformanceData[] {
    return Array.from(this.performanceMap.values())
  }

  /**
   * 获取特定事件的性能数据
   */
  getEventPerformance(eventName: string): EventPerformanceData | undefined {
    return this.performanceMap.get(eventName)
  }

  /**
   * 获取最频繁触发的事件 TOP N
   */
  getTopFrequentEvents(limit: number = 10): EventPerformanceData[] {
    return Array.from(this.performanceMap.values())
      .sort((a, b) => b.triggerCount - a.triggerCount)
      .slice(0, limit)
  }

  /**
   * 获取最慢的事件 TOP N
   */
  getSlowestEvents(limit: number = 10): EventPerformanceData[] {
    return Array.from(this.performanceMap.values())
      .sort((a, b) => b.avgExecutionTime - a.avgExecutionTime)
      .slice(0, limit)
  }

  /**
   * 获取事件历史
   */
  getEventHistory(limit: number = 50): typeof this.eventHistory {
    return this.eventHistory.slice(-limit)
  }

  /**
   * 获取监控统计摘要
   */
  getSummary(): {
    totalEvents: number
    totalTriggers: number
    avgExecutionTime: number
    errorRate: number
    monitoredEvents: number
  } {
    let totalTriggers = 0
    let totalExecutionTime = 0
    let totalErrors = 0

    for (const data of this.performanceMap.values()) {
      totalTriggers += data.triggerCount
      totalExecutionTime += data.avgExecutionTime * data.triggerCount
      totalErrors += data.errors
    }

    return {
      totalEvents: this.performanceMap.size,
      totalTriggers,
      avgExecutionTime: totalTriggers > 0 ? totalExecutionTime / totalTriggers : 0,
      errorRate: totalTriggers > 0 ? totalErrors / totalTriggers : 0,
      monitoredEvents: this.performanceMap.size
    }
  }

  /**
   * 清空监控数据
   */
  clear(): void {
    this.performanceMap.clear()
    this.eventHistory = []
    logger.info('[EventBus Monitor] 监控数据已清空')
  }

  /**
   * 导出性能报告
   */
  exportReport(): string {
    const summary = this.getSummary()
    const topFrequent = this.getTopFrequentEvents(5)
    const slowest = this.getSlowestEvents(5)

    const report = `
=== EventBus 性能报告 ===
生成时间: ${new Date().toLocaleString()}

【总体统计】
- 监控事件数: ${summary.totalEvents}
- 总触发次数: ${summary.totalTriggers}
- 平均执行时间: ${summary.avgExecutionTime.toFixed(2)}ms
- 错误率: ${(summary.errorRate * 100).toFixed(2)}%

【最频繁触发的事件 TOP 5】
${topFrequent.map(e => `  - ${e.eventName}: ${e.triggerCount} 次, 平均 ${e.avgExecutionTime.toFixed(2)}ms`).join('\n')}

【最慢的事件 TOP 5】
${slowest.map(e => `  - ${e.eventName}: 平均 ${e.avgExecutionTime.toFixed(2)}ms, 触发 ${e.triggerCount} 次`).join('\n')}
========================
    `.trim()

    return report
  }

  /**
   * 打印性能报告到控制台
   */
  printReport(): void {
    const report = this.exportReport()
    logger.info(report)
  }
}

/**
 * 创建全局监控实例
 */
export const eventBusMonitor = new EventBusMonitor()

/**
 * 组合式函数:在组件中使用事件监控
 */
export function useEventBusMonitor() {
  return {
    /**
     * 启用监控
     */
    enable: () => eventBusMonitor.enable(),
    
    /**
     * 禁用监控
     */
    disable: () => eventBusMonitor.disable(),
    
    /**
     * 获取性能数据
     */
    getPerformance: () => eventBusMonitor.getAllPerformanceData(),
    
    /**
     * 获取统计摘要
     */
    getSummary: () => eventBusMonitor.getSummary(),
    
    /**
     * 打印报告
     */
    printReport: () => eventBusMonitor.printReport(),
    
    /**
     * 清空数据
     */
    clear: () => eventBusMonitor.clear()
  }
}
