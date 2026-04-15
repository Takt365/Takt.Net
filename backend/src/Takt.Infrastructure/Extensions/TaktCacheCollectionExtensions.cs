// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Extensions
// 文件名称：TaktCacheCollectionExtensions.cs
// 创建时间：2026-01-28
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt缓存服务注册扩展方法
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Takt.Shared.Models;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// Takt缓存服务注册扩展方法
/// </summary>
public static class TaktCacheCollectionExtensions
{
    /// <summary>
    /// 添加Takt缓存服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktCache(this IServiceCollection services, IConfiguration configuration)
    {
        // 注册缓存配置选项
        services.Configure<TaktCacheOptions>(configuration.GetSection("Cache"));

        var cacheOptions = configuration.GetSection("Cache").Get<TaktCacheOptions>() ?? new TaktCacheOptions();

        // 注册内存缓存（如果尚未注册）
        if (!services.Any(s => s.ServiceType == typeof(IMemoryCache)))
        {
            services.AddMemoryCache(options =>
            {
                if (cacheOptions.Memory != null)
                {
                    if (cacheOptions.Memory.CompactionPercentage > 0)
                    {
                        options.CompactionPercentage = cacheOptions.Memory.CompactionPercentage;
                    }
                }
            });
        }

        // 根据配置注册Redis缓存
        if (cacheOptions.Provider.Equals("Redis", StringComparison.OrdinalIgnoreCase) && 
            cacheOptions.Redis.Enabled && 
            !string.IsNullOrEmpty(cacheOptions.Redis.ConnectionString))
        {
            services.AddStackExchangeRedisCache(options =>
            {
                // 构建 Redis 配置选项
                var configOptions = ConfigurationOptions.Parse(cacheOptions.Redis.ConnectionString);
                configOptions.ConnectTimeout = cacheOptions.Redis.ConnectTimeout;
                configOptions.SyncTimeout = cacheOptions.Redis.SyncTimeout;
                configOptions.AllowAdmin = cacheOptions.Redis.AllowAdmin;
                configOptions.Ssl = cacheOptions.Redis.Ssl;
                configOptions.DefaultDatabase = cacheOptions.Redis.DefaultDatabase;

                if (!string.IsNullOrEmpty(cacheOptions.Redis.Password))
                {
                    configOptions.Password = cacheOptions.Redis.Password;
                }

                options.ConfigurationOptions = configOptions;
                options.InstanceName = cacheOptions.Redis.InstanceName;
            });
        }

        return services;
    }
}
