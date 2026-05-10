// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Cache
// 文件名称：TaktCacheController.cs
// 创建时间：2026-01-28
// 功能描述：缓存管理控制器，提供缓存信息查询、键存在检查、移除、统计等接口
// ========================================

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Takt.Application.Dtos.Routine.Tasks.Cache;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.Tasks.Cache;

/// <summary>
/// 缓存管理控制器
/// </summary>
[Route("api/[controller]", Name = "缓存管理")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:cache", "缓存管理")]
public class TaktCachesController : TaktControllerBase
{
    private readonly ITaktCache _cache;
    private readonly TaktCacheOptions _cacheOptions;
    private readonly IMemoryCache? _memoryCache;

    public TaktCachesController(
        ITaktCache cache,
        IOptions<TaktCacheOptions> cacheOptions,
        IMemoryCache? memoryCache = null,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _cache = cache;
        _cacheOptions = cacheOptions.Value;
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// 获取缓存配置信息（不包含敏感信息）
    /// </summary>
    [HttpGet("info")]
    [TaktPermission("routine:tasks:cache:list", "查询缓存信息")]
    public ActionResult<TaktCacheInfoDto> GetInfo()
    {
        var dto = new TaktCacheInfoDto
        {
            Provider = string.IsNullOrEmpty(_cacheOptions.Provider) ? "Memory" : _cacheOptions.Provider,
            DefaultExpirationMinutes = _cacheOptions.DefaultExpirationMinutes,
            EnableSlidingExpiration = _cacheOptions.EnableSlidingExpiration,
            EnableMultiLevelCache = _cacheOptions.EnableMultiLevelCache,
            RedisEnabled = _cacheOptions.Redis?.Enabled ?? false,
            RedisInstanceName = _cacheOptions.Redis?.InstanceName ?? string.Empty
        };
        return Ok(dto);
    }

    /// <summary>
    /// 获取缓存统计信息（仅 Memory 提供者支持：总项数、命中/未命中、命中率、估算大小）
    /// </summary>
    [HttpGet("statistics")]
    [TaktPermission("routine:tasks:cache:list", "查询缓存统计")]
    public ActionResult<TaktCacheStatisticsDto> GetStatistics()
    {
        var provider = string.IsNullOrEmpty(_cacheOptions.Provider) ? "Memory" : _cacheOptions.Provider;
        if (!provider.Equals("Memory", StringComparison.OrdinalIgnoreCase) || _memoryCache == null)
        {
            return Ok(new TaktCacheStatisticsDto { Supported = false, Message = GetLocalizedString("validation.cacheStatsMemoryOnly", "Frontend", provider) });
        }

        // GetCurrentStatistics() 在 .NET 7+ 的 MemoryCache 实现类上，接口可能无此方法，故按实现类型调用
        var stats = (_memoryCache as MemoryCache)?.GetCurrentStatistics();
        if (stats == null)
        {
            return Ok(new TaktCacheStatisticsDto { Supported = true, Message = GetLocalizedString("validation.cacheStatsDisabledOrEmpty", "Frontend") });
        }

        var totalRequests = stats.TotalHits + stats.TotalMisses;
        var hitRate = totalRequests > 0 ? (double)stats.TotalHits / totalRequests : (double?)null;
        var dto = new TaktCacheStatisticsDto
        {
            Supported = true,
            TotalHits = stats.TotalHits,
            TotalMisses = stats.TotalMisses,
            CurrentEntryCount = stats.CurrentEntryCount,
            CurrentEstimatedSizeBytes = stats.CurrentEstimatedSize,
            HitRate = hitRate
        };
        return Ok(dto);
    }

    /// <summary>
    /// 检查指定键是否存在
    /// </summary>
    /// <param name="key">缓存键</param>
    [HttpGet("exists")]
    [TaktPermission("routine:tasks:cache:query", "检查缓存键")]
    public async Task<ActionResult<TaktCacheExistsDto>> ExistsAsync([FromQuery] string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return BadRequest(GetLocalizedString("validation.cacheKeyRequired", "Frontend"));
        var exists = await _cache.ExistsAsync(key);
        return Ok(new TaktCacheExistsDto { Key = key, Exists = exists });
    }

    /// <summary>
    /// 移除指定缓存键
    /// </summary>
    /// <param name="key">缓存键</param>
    [HttpDelete]
    [TaktPermission("routine:tasks:cache:delete", "移除缓存")]
    public async Task<ActionResult> RemoveAsync([FromQuery] string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return BadRequest(GetLocalizedString("validation.cacheKeyRequired", "Frontend"));
        await _cache.RemoveAsync(key);
        return Ok(GetLocalizedString("validation.cacheRemoveSuccess", "Frontend"));
    }
}
