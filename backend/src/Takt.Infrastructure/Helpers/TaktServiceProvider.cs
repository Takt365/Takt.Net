// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Helpers
// 文件名称：TaktServiceProvider.cs
// 创建时间：2025-01-22
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt全局服务提供者，用于在事件回调中获取服务（避免循环依赖）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;

namespace Takt.Infrastructure.Helpers;

/// <summary>
/// Takt全局服务提供者
/// </summary>
public static class TaktServiceProvider
{
    private static IServiceProvider? _serviceProvider;

    /// <summary>
    /// 设置服务提供者
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    public static void SetServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 获取服务提供者
    /// </summary>
    public static IServiceProvider? ServiceProvider => _serviceProvider;
}
