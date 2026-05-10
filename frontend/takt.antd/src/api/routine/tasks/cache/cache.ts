// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/cache
// 文件名称：cache.ts
// 功能描述：缓存管理 API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.Cache.TaktCachesController
// ========================================

import request from '@/api/request'
import type { TaktCacheInfoDto, TaktCacheExistsDto, TaktCacheStatisticsDto } from '@/types/routine/tasks/cache/cache'

// 重新导出类型，方便视图文件使用
export type { TaktCacheInfoDto, TaktCacheExistsDto, TaktCacheStatisticsDto }

// ========================================
// Cache 相关 API（特殊管理接口）
// ========================================
const cacheUrl = 'api/TaktCaches'

/**
 * 获取缓存配置信息（不包含敏感信息）
 * 对应后端：GetInfo
 */
export function getCacheInfo(): Promise<TaktCacheInfoDto> {
  return request({
    url: `${cacheUrl}/info`,
    method: 'get'
  })
}

/**
 * 获取缓存统计信息（仅 Memory 提供者支持：总项数、命中/未命中、命中率、估算大小）
 * 对应后端：GetStatistics
 */
export function getCacheStatistics(): Promise<TaktCacheStatisticsDto> {
  return request({
    url: `${cacheUrl}/statistics`,
    method: 'get'
  })
}

/**
 * 检查指定键是否存在
 * 对应后端：ExistsAsync
 */
export function existsCacheKey(key: string): Promise<TaktCacheExistsDto> {
  return request({
    url: `${cacheUrl}/exists`,
    method: 'get',
    params: { key }
  })
}

/**
 * 移除指定缓存键
 * 对应后端：RemoveAsync
 */
export function removeCacheKey(key: string): Promise<string> {
  return request({
    url: cacheUrl,
    method: 'delete',
    params: { key }
  })
}
