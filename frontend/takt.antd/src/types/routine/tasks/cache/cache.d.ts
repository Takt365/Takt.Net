// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/cache
// 文件名称：cache.d.ts
// 创建时间：2026-01-28
// 功能描述：缓存管理相关类型定义（对应后端 TaktCacheDtos）
// ========================================

/**
 * 缓存配置信息 DTO（不含敏感信息）
 */
export interface TaktCacheInfoDto {
  /** 缓存提供者 */
  provider: string
  /** 默认过期时间（分钟） */
  defaultExpirationMinutes: number
  /** 是否启用滑动过期 */
  enableSlidingExpiration: boolean
  /** 是否启用多级缓存 */
  enableMultiLevelCache: boolean
  /** 是否启用 Redis */
  redisEnabled: boolean
  /** Redis 实例名称 */
  redisInstanceName: string
}

/**
 * 缓存键存在检查结果 DTO
 */
export interface TaktCacheExistsDto {
  /** 缓存键 */
  key: string
  /** 是否存在 */
  exists: boolean
}

/**
 * 缓存统计信息 DTO（来自 MemoryCacheStatistics，仅 Memory 支持）
 */
export interface TaktCacheStatisticsDto {
  /** 是否支持统计（仅 Memory 支持） */
  supported: boolean
  /** 说明（不支持或未启用时） */
  message?: string
  /** 命中次数 */
  totalHits: number
  /** 未命中次数 */
  totalMisses: number
  /** 当前缓存项数量 */
  currentEntryCount: number
  /** 当前估算大小（字节） */
  currentEstimatedSizeBytes?: number
  /** 命中率（0~1），无请求时为 null */
  hitRate?: number
}
