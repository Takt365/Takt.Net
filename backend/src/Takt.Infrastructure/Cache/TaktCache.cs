// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Cache
// 文件名称：TaktCache.cs
// 创建时间：2026-01-28
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt缓存服务实现，支持内存缓存和Redis缓存
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.IO.Compression;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Takt.Domain.Interfaces;
using Takt.Shared.Models;

namespace Takt.Infrastructure.Cache;

/// <summary>
/// Takt缓存服务实现
/// </summary>
public class TaktCache : ITaktCache
{
    private readonly IMemoryCache? _memoryCache;
    private readonly IDistributedCache? _distributedCache;
    private readonly TaktCacheOptions _options;
    private readonly ILogger<TaktCache>? _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="memoryCache">内存缓存（可选）</param>
    /// <param name="distributedCache">分布式缓存（可选）</param>
    /// <param name="options">缓存配置选项</param>
    /// <param name="logger">日志记录器（可选）</param>
    public TaktCache(
        IMemoryCache? memoryCache,
        IDistributedCache? distributedCache,
        IOptions<TaktCacheOptions> options,
        ILogger<TaktCache>? logger = null)
    {
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
        _options = options.Value;
        _logger = logger;
    }

    /// <summary>
    /// 获取缓存值
    /// </summary>
    public T? Get<T>(string key) where T : class
    {
        try
        {
            // 多级缓存：先查内存，再查Redis
            if (_options.EnableMultiLevelCache && _memoryCache != null)
            {
                var memoryValue = _memoryCache.Get<T>(key);
                if (memoryValue != null)
                {
                    return memoryValue;
                }
            }

            // 使用配置的提供者
            if (_options.Provider.Equals("Redis", StringComparison.OrdinalIgnoreCase) && _distributedCache != null)
            {
                return GetFromDistributedCache<T>(key);
            }
            else if (_memoryCache != null)
            {
                return _memoryCache.Get<T>(key);
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "获取缓存失败: Key={Key}", key);
            return null;
        }
    }

    /// <summary>
    /// 异步获取缓存值
    /// </summary>
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        try
        {
            // 多级缓存：先查内存，再查Redis
            if (_options.EnableMultiLevelCache && _memoryCache != null)
            {
                var memoryValue = _memoryCache.Get<T>(key);
                if (memoryValue != null)
                {
                    return memoryValue;
                }
            }

            // 使用配置的提供者
            if (_options.Provider.Equals("Redis", StringComparison.OrdinalIgnoreCase) && _distributedCache != null)
            {
                return await GetFromDistributedCacheAsync<T>(key, cancellationToken);
            }
            else if (_memoryCache != null)
            {
                return await Task.FromResult(_memoryCache.Get<T>(key));
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "获取缓存失败: Key={Key}", key);
            return null;
        }
    }

    /// <summary>
    /// 设置缓存值
    /// </summary>
    public void Set<T>(string key, T value, int? expirationMinutes = null, bool? slidingExpiration = null) where T : class
    {
        try
        {
            var expiration = expirationMinutes ?? _options.DefaultExpirationMinutes;
            var useSliding = slidingExpiration ?? _options.EnableSlidingExpiration;

            // 使用配置的提供者
            if (_options.Provider.Equals("Redis", StringComparison.OrdinalIgnoreCase) && _distributedCache != null)
            {
                SetToDistributedCache(key, value, expiration, useSliding);
            }
            else if (_memoryCache != null)
            {
                SetToMemoryCache(key, value, expiration, useSliding);
            }

            // 多级缓存：同时写入内存和Redis
            if (_options.EnableMultiLevelCache)
            {
                if (_memoryCache != null)
                {
                    SetToMemoryCache(key, value, expiration, useSliding);
                }
                if (_distributedCache != null)
                {
                    SetToDistributedCache(key, value, expiration, useSliding);
                }
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "设置缓存失败: Key={Key}", key);
        }
    }

    /// <summary>
    /// 异步设置缓存值
    /// </summary>
    public async Task SetAsync<T>(string key, T value, int? expirationMinutes = null, bool? slidingExpiration = null, CancellationToken cancellationToken = default) where T : class
    {
        try
        {
            var expiration = expirationMinutes ?? _options.DefaultExpirationMinutes;
            var useSliding = slidingExpiration ?? _options.EnableSlidingExpiration;

            // 使用配置的提供者
            if (_options.Provider.Equals("Redis", StringComparison.OrdinalIgnoreCase) && _distributedCache != null)
            {
                await SetToDistributedCacheAsync(key, value, expiration, useSliding, cancellationToken);
            }
            else if (_memoryCache != null)
            {
                SetToMemoryCache(key, value, expiration, useSliding);
            }

            // 多级缓存：同时写入内存和Redis
            if (_options.EnableMultiLevelCache)
            {
                if (_memoryCache != null)
                {
                    SetToMemoryCache(key, value, expiration, useSliding);
                }
                if (_distributedCache != null)
                {
                    await SetToDistributedCacheAsync(key, value, expiration, useSliding, cancellationToken);
                }
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "设置缓存失败: Key={Key}", key);
        }
    }

    /// <summary>
    /// 移除缓存
    /// </summary>
    public void Remove(string key)
    {
        try
        {
            if (_memoryCache != null)
            {
                _memoryCache.Remove(key);
            }

            if (_distributedCache != null)
            {
                _distributedCache.Remove(key);
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "移除缓存失败: Key={Key}", key);
        }
    }

    /// <summary>
    /// 异步移除缓存
    /// </summary>
    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            if (_memoryCache != null)
            {
                _memoryCache.Remove(key);
            }

            if (_distributedCache != null)
            {
                await _distributedCache.RemoveAsync(key, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "移除缓存失败: Key={Key}", key);
        }
    }

    /// <summary>
    /// 检查缓存是否存在
    /// </summary>
    public bool Exists(string key)
    {
        try
        {
            if (_options.Provider.Equals("Redis", StringComparison.OrdinalIgnoreCase) && _distributedCache != null)
            {
                var value = _distributedCache.Get(key);
                return value != null;
            }
            else if (_memoryCache != null)
            {
                return _memoryCache.TryGetValue(key, out _);
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "检查缓存存在性失败: Key={Key}", key);
            return false;
        }
    }

    /// <summary>
    /// 异步检查缓存是否存在
    /// </summary>
    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            if (_options.Provider.Equals("Redis", StringComparison.OrdinalIgnoreCase) && _distributedCache != null)
            {
                var value = await _distributedCache.GetAsync(key, cancellationToken);
                return value != null;
            }
            else if (_memoryCache != null)
            {
                return await Task.FromResult(_memoryCache.TryGetValue(key, out _));
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "检查缓存存在性失败: Key={Key}", key);
            return false;
        }
    }

    /// <summary>
    /// 获取或设置缓存值
    /// </summary>
#pragma warning disable CS8766 // 与接口 ITaktCache 的 T? 返回类型可空性一致，实现可返回 null
    public T? GetOrSet<T>(string key, Func<T> factory, int? expirationMinutes = null, bool? slidingExpiration = null) where T : class
#pragma warning restore CS8766
    {
        var value = Get<T>(key);
        if (value != null)
        {
            return value;
        }

        value = factory();
        if (value != null)
        {
            Set(key, value, expirationMinutes, slidingExpiration);
        }

        return value;
    }

    /// <summary>
    /// 异步获取或设置缓存值
    /// </summary>
#pragma warning disable CS8613 // 与接口 ITaktCache 的 Task<T?> 返回类型可空性一致，实现可返回 null
    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, int? expirationMinutes = null, bool? slidingExpiration = null, CancellationToken cancellationToken = default) where T : class
#pragma warning restore CS8613
    {
        var value = await GetAsync<T>(key, cancellationToken);
        if (value != null)
        {
            return value;
        }

        value = await factory();
        if (value != null)
        {
            await SetAsync(key, value, expirationMinutes, slidingExpiration, cancellationToken);
        }

        return value;
    }

    #region 私有方法

    private T? GetFromDistributedCache<T>(string key) where T : class
    {
        if (_distributedCache == null) return null;

        var fullKey = GetFullKey(key);
        var bytes = _distributedCache.Get(fullKey);
        if (bytes == null || bytes.Length == 0)
        {
            return null;
        }

        var json = DecompressIfNeeded(bytes);
        return JsonConvert.DeserializeObject<T>(json);
    }

    private async Task<T?> GetFromDistributedCacheAsync<T>(string key, CancellationToken cancellationToken) where T : class
    {
        if (_distributedCache == null) return null;

        var fullKey = GetFullKey(key);
        var bytes = await _distributedCache.GetAsync(fullKey, cancellationToken);
        if (bytes == null || bytes.Length == 0)
        {
            return null;
        }

        var json = DecompressIfNeeded(bytes);
        return JsonConvert.DeserializeObject<T>(json);
    }

    private void SetToDistributedCache<T>(string key, T value, int expirationMinutes, bool slidingExpiration) where T : class
    {
        if (_distributedCache == null) return;

        var fullKey = GetFullKey(key);
        var json = JsonConvert.SerializeObject(value);
        var bytes = Encoding.UTF8.GetBytes(json);
        bytes = CompressIfNeeded(bytes);

        var options = new DistributedCacheEntryOptions();
        if (slidingExpiration)
        {
            options.SlidingExpiration = TimeSpan.FromMinutes(expirationMinutes);
        }
        else
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationMinutes);
        }

        _distributedCache.Set(fullKey, bytes, options);
    }

    private async Task SetToDistributedCacheAsync<T>(string key, T value, int expirationMinutes, bool slidingExpiration, CancellationToken cancellationToken) where T : class
    {
        if (_distributedCache == null) return;

        var fullKey = GetFullKey(key);
        var json = JsonConvert.SerializeObject(value);
        var bytes = Encoding.UTF8.GetBytes(json);
        bytes = CompressIfNeeded(bytes);

        var options = new DistributedCacheEntryOptions();
        if (slidingExpiration)
        {
            options.SlidingExpiration = TimeSpan.FromMinutes(expirationMinutes);
        }
        else
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationMinutes);
        }

        await _distributedCache.SetAsync(fullKey, bytes, options, cancellationToken);
    }

    private void SetToMemoryCache<T>(string key, T value, int expirationMinutes, bool slidingExpiration) where T : class
    {
        if (_memoryCache == null) return;

        var options = new MemoryCacheEntryOptions();
        if (slidingExpiration)
        {
            options.SlidingExpiration = TimeSpan.FromMinutes(expirationMinutes);
        }
        else
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationMinutes);
        }

        _memoryCache.Set(key, value, options);
    }

    private string GetFullKey(string key)
    {
        if (string.IsNullOrEmpty(_options.Redis.InstanceName))
        {
            return key;
        }

        return $"{_options.Redis.InstanceName}:{key}";
    }

    private byte[] CompressIfNeeded(byte[] data)
    {
        if (!_options.Redis.EnableCompression || data.Length < _options.Redis.CompressionThreshold)
        {
            return data;
        }

        // 使用 GZip 压缩
        using var output = new MemoryStream();
        using (var gzip = new GZipStream(output, CompressionLevel.Fastest))
        {
            gzip.Write(data, 0, data.Length);
        }
        return output.ToArray();
    }

    private string DecompressIfNeeded(byte[] data)
    {
        // 检查是否是压缩数据（简单检查：如果前两个字节是 GZip 魔数）
        if (data.Length >= 2 && data[0] == 0x1f && data[1] == 0x8b)
        {
            using var input = new MemoryStream(data);
            using var gzip = new GZipStream(input, CompressionMode.Decompress);
            using var output = new MemoryStream();
            gzip.CopyTo(output);
            return Encoding.UTF8.GetString(output.ToArray());
        }

        return Encoding.UTF8.GetString(data);
    }

    #endregion
}
