// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Middleware
// 文件名称：TaktSignalRTokenMiddleware.cs
// 创建时间：2025-01-17
// 创建人：Takt365(Cursor AI)
// 功能描述：SignalR Token 中间件，从查询参数中提取 access_token 并添加到 Authorization header
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Shared.Helpers;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// SignalR Token 中间件
/// 用于从查询参数中提取 access_token 并添加到 Authorization header
/// 注意：SignalR WebSocket 不能使用 Authorization header，需要通过查询参数传递 token
/// </summary>
public class TaktSignalRTokenMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">下一个中间件</param>
    public TaktSignalRTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>任务</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        
        // 只处理 SignalR hub 路径
        if (path.Contains("/hubs/", StringComparison.OrdinalIgnoreCase) || 
            path.Contains("/negotiate", StringComparison.OrdinalIgnoreCase))
        {
            var existingAuth = context.Request.Headers["Authorization"].ToString();
            TaktLogger.Information("SignalR Token 中间件: 路径 {Path}, 现有 Authorization: {HasAuth}, 查询参数 access_token: {HasQueryToken}", 
                path, !string.IsNullOrEmpty(existingAuth), context.Request.Query.ContainsKey("access_token"));
            
            // 如果 Authorization header 已存在，则不需要从查询参数读取
            if (string.IsNullOrEmpty(existingAuth))
            {
                // 尝试从查询参数 access_token 中提取 token
                if (context.Request.Query.TryGetValue("access_token", out var accessTokenValues) && 
                    accessTokenValues.Count > 0 && !string.IsNullOrEmpty(accessTokenValues[0]))
                {
                    var accessToken = accessTokenValues[0];
                    // 将 token 添加到 Authorization header（Bearer 格式）
                    context.Request.Headers["Authorization"] = $"Bearer {accessToken}";
                    TaktLogger.Information("SignalR Token 中间件: 从查询参数 access_token 中提取 token 并添加到 Authorization header，路径: {Path}", path);
                }
                else
                {
                    TaktLogger.Warning("SignalR Token 中间件: 路径 {Path} 未找到 access_token 查询参数", path);
                }
            }
            else
            {
                TaktLogger.Information("SignalR Token 中间件: 路径 {Path} 已存在 Authorization header，跳过查询参数提取", path);
            }
        }

        await _next(context);
    }
}
