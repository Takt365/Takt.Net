// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.Cache
// 文件名称：TaktCacheDtos.cs
// 创建时间：2026-01-28
// 功能描述：缓存管理相关DTO定义
// ========================================

namespace Takt.Application.Dtos.Routine.Tasks.Cache;

/// <summary>
/// 缓存配置信息 DTO（不含敏感信息）
/// </summary>
public class TaktCacheInfoDto
{
    public string Provider { get; set; } = string.Empty;
    public int DefaultExpirationMinutes { get; set; }
    public bool EnableSlidingExpiration { get; set; }
    public bool EnableMultiLevelCache { get; set; }
    public bool RedisEnabled { get; set; }
    public string RedisInstanceName { get; set; } = string.Empty;
}

/// <summary>
/// 缓存键存在检查结果 DTO
/// </summary>
public class TaktCacheExistsDto
{
    public string Key { get; set; } = string.Empty;
    public bool Exists { get; set; }
}

/// <summary>
/// 缓存统计信息 DTO（来自 MemoryCacheStatistics，仅 Memory 支持）
/// </summary>
public class TaktCacheStatisticsDto
{
    /// <summary>是否支持统计（仅 Memory 支持）</summary>
    public bool Supported { get; set; }
    /// <summary>说明（不支持或未启用时）</summary>
    public string? Message { get; set; }
    /// <summary>命中次数</summary>
    public long TotalHits { get; set; }
    /// <summary>未命中次数</summary>
    public long TotalMisses { get; set; }
    /// <summary>当前缓存项数量</summary>
    public long CurrentEntryCount { get; set; }
    /// <summary>当前估算大小（字节）</summary>
    public long? CurrentEstimatedSizeBytes { get; set; }
    /// <summary>命中率（0~1），无请求时为 null</summary>
    public double? HitRate { get; set; }
}
