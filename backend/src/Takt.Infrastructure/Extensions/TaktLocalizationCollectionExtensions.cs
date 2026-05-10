// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Extensions
// 文件名称：TaktLocalizationCollectionExtensions.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：本地化配置扩展方法，用于配置ASP.NET Core本地化服务
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Localization;
using Takt.Infrastructure.Resources;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// 本地化配置扩展方法
/// </summary>
public static class TaktLocalizationCollectionExtensions
{
    /// <summary>
    /// 添加Takt本地化服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="resourceAssembly">资源文件所在程序集，默认使用 Infrastructure 程序集</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktLocalization(
        this IServiceCollection services,
        Assembly? resourceAssembly = null)
    {
        // 使用 Infrastructure 程序集作为默认资源程序集
        var assembly = resourceAssembly ?? typeof(SharedResources).Assembly;
        
        // 添加本地化服务
        services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });

        // 配置请求本地化选项
        services.Configure<RequestLocalizationOptions>(options =>
        {
            // 支持的语言列表（6种语言）
            var supportedCultures = new[]
            {
                new CultureInfo("zh-CN"),  // 简体中文
                new CultureInfo("zh-TW"),  // 繁体中文（台湾）
                new CultureInfo("zh-HK"),  // 繁体中文（香港）
                new CultureInfo("en-US"),  // 英语（美国）
                new CultureInfo("ja-JP"),  // 日语（日本）
                new CultureInfo("ko-KR"),  // 韩语（韩国）
            };

            // 获取操作系统默认语言
            var defaultCulture = GetDefaultCultureFromOperatingSystem();
            options.DefaultRequestCulture = new RequestCulture(defaultCulture);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            // 语言检测提供者（按优先级顺序）
            options.RequestCultureProviders.Clear();
            options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());  // 查询字符串：?culture=zh-CN
            options.RequestCultureProviders.Add(new CookieRequestCultureProvider());       // Cookie
            options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider()); // Accept-Language 头
        });

        // 注册内存缓存（TaktCacheCollectionExtensions 中已注册，这里不需要重复注册）
        // services.AddMemoryCache();

        // 注册 TaktLocalizer（翻译仓储、HttpContextAccessor、MemoryCache）
        services.AddScoped<ITaktLocalizer, TaktLocalizer>();

        return services;
    }

    /// <summary>
    /// 使用Takt本地化
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    public static IApplicationBuilder UseTaktLocalization(this IApplicationBuilder app)
    {
        // 使用自定义中间件实现本地化，避免 UseRequestLocalization 导致的启动阻塞
        app.Use(async (context, next) =>
        {
            var localizationOptions = context.RequestServices
                .GetService<IOptions<RequestLocalizationOptions>>();
            
            if (localizationOptions?.Value != null)
            {
                var options = localizationOptions.Value;
                
                // 按优先级顺序检测语言
                ProviderCultureResult? providerResult = null;
                
                foreach (var provider in options.RequestCultureProviders)
                {
                    providerResult = await provider.DetermineProviderCultureResult(context);
                    if (providerResult?.Cultures != null && providerResult.Cultures.Count > 0)
                    {
                        break;
                    }
                }
                
                RequestCulture? requestCulture = null;
                
                if (providerResult?.Cultures != null && providerResult.Cultures.Count > 0)
                {
                    var culture = providerResult.Cultures[0].Value;
                    var uiCulture = providerResult.UICultures.Count > 0 ? providerResult.UICultures[0].Value : culture;
                    requestCulture = new RequestCulture(culture, uiCulture);
                }
                
                // 如果未检测到语言，使用默认语言
                requestCulture ??= options.DefaultRequestCulture;
                
                if (requestCulture != null)
                {
                    context.Features.Set(requestCulture);
                    CultureInfo.CurrentCulture = requestCulture.Culture;
                    CultureInfo.CurrentUICulture = requestCulture.UICulture;
                }
            }
            
            await next();
        });
        
        return app;
    }

    /// <summary>
    /// 从操作系统获取默认语言
    /// </summary>
    /// <returns>默认语言编码（如：zh-CN）</returns>
    private static string GetDefaultCultureFromOperatingSystem()
    {
        try
        {
            // 使用 TaktServHelper 获取操作系统语言信息
            var osLanguage = TaktServHelper.GetOperatingSystemLanguage();
            
            // 优先使用当前UI文化
            if (!string.IsNullOrEmpty(osLanguage.CurrentUICulture))
            {
                // 检查是否为支持的语言
                var supportedCultures = new[] { "zh-CN", "zh-TW", "zh-HK", "en-US", "ja-JP", "ko-KR" };
                if (supportedCultures.Contains(osLanguage.CurrentUICulture))
                {
                    return osLanguage.CurrentUICulture;
                }
                
                // 尝试匹配中性文化（如：zh-CN -> zh）
                var neutralCulture = osLanguage.CurrentUICulture.Split('-')[0];
                var matchedCulture = supportedCultures.FirstOrDefault(c => c.StartsWith(neutralCulture, StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(matchedCulture))
                {
                    return matchedCulture;
                }
            }
            
            // 其次使用当前文化
            if (!string.IsNullOrEmpty(osLanguage.CurrentCulture))
            {
                var supportedCultures = new[] { "zh-CN", "zh-TW", "zh-HK", "en-US", "ja-JP", "ko-KR" };
                if (supportedCultures.Contains(osLanguage.CurrentCulture))
                {
                    return osLanguage.CurrentCulture;
                }
            }
        }
        catch
        {
            // 如果获取失败，使用备用方案
        }

        // 最后回退到 .NET 默认文化或 "zh-CN"
        try
        {
            var installedCulture = CultureInfo.InstalledUICulture.Name;
            var supportedCultures = new[] { "zh-CN", "zh-TW", "zh-HK", "en-US", "ja-JP", "ko-KR" };
            if (supportedCultures.Contains(installedCulture))
            {
                return installedCulture;
            }
            
            // 尝试匹配中性文化
            var neutralCulture = installedCulture.Split('-')[0];
            var matchedCulture = supportedCultures.FirstOrDefault(c => c.StartsWith(neutralCulture, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(matchedCulture))
            {
                return matchedCulture;
            }
        }
        catch { }

        return "zh-CN"; // 最终回退值
    }
}
