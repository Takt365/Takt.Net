// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Extensions
// 文件名称：TaktSignalRCollectionExtensions.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：SignalR配置扩展方法，用于配置SignalR服务
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// SignalR配置扩展方法
/// </summary>
public static class TaktSignalRCollectionExtensions
{
    /// <summary>
    /// 添加SignalR服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configure">可选的SignalR配置委托</param>
    /// <returns>SignalR服务构建器</returns>
    public static ISignalRServerBuilder AddTaktSignalR(this IServiceCollection services, Action<HubOptions>? configure = null)
    {
        var signalRBuilder = services.AddSignalR(options =>
        {
            // 默认配置
            options.EnableDetailedErrors = true;
            options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            options.KeepAliveInterval = TimeSpan.FromSeconds(15);
            
            // 自定义配置
            configure?.Invoke(options);
        });

        return signalRBuilder;
    }

    /// <summary>
    /// 映射指定的SignalR Hub（使用WebApplication.MapHub，适用于.NET 6+）
    /// </summary>
    /// <typeparam name="THub">Hub类型</typeparam>
    /// <param name="app">Web应用程序</param>
    /// <param name="pattern">Hub路由模式</param>
    /// <returns>Web应用程序</returns>
    public static WebApplication MapTaktSignalRHub<THub>(this WebApplication app, string pattern) 
        where THub : Hub
    {
        app.MapHub<THub>(pattern);
        return app;
    }
}
