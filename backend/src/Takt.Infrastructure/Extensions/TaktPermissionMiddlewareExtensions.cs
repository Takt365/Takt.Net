// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Extensions
// 文件名称：TaktPermissionMiddlewareExtensions.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：权限中间件扩展方法，用于注册权限中间件
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using Microsoft.AspNetCore.Builder;
using Takt.Infrastructure.Middleware;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// 权限中间件扩展方法
/// </summary>
public static class TaktPermissionMiddlewareExtensions
{
    /// <summary>
    /// 使用Takt权限中间件
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    /// <remarks>
    /// 权限中间件应该在以下中间件之后使用：
    /// - UseRouting()
    /// - UseAuthentication()
    /// - UseAuthorization()
    /// - UseMiddleware&lt;TaktUserMiddleware&gt;()（用户上下文中间件）
    /// 
    /// 这样可以确保用户信息已经加载到上下文中。
    /// </remarks>
    public static IApplicationBuilder UseTaktPermission(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TaktPermissionMiddleware>();
    }
}
