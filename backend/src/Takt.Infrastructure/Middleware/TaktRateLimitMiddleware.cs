// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Middleware
// 文件名称：TaktRateLimitMiddleware.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt速率限制中间件，控制请求频率，防止DoS攻击
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Collections.Concurrent;
using System.Text;
using Newtonsoft.Json;
using Serilog;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// 速率限制记录
/// </summary>
internal class RateLimitRecord
{
    public int RequestCount { get; set; }
    public DateTime ResetTime { get; set; }
}

/// <summary>
/// Takt速率限制中间件
/// </summary>
public class TaktRateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _maxRequests;
    private readonly TimeSpan _timeWindow;
    private static readonly ConcurrentDictionary<string, RateLimitRecord> _rateLimitStore = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">下一个中间件</param>
    /// <param name="maxRequests">时间窗口内最大请求数（默认100）</param>
    /// <param name="timeWindowSeconds">时间窗口（秒，默认60）</param>
    public TaktRateLimitMiddleware(RequestDelegate next, int maxRequests = 100, int timeWindowSeconds = 60)
    {
        _next = next;
        _maxRequests = maxRequests;
        _timeWindow = TimeSpan.FromSeconds(timeWindowSeconds);
    }

    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>任务</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // 检查是否需要跳过速率限制（SignalR negotiation、健康检查、静态资源等）
        if (ShouldSkipRateLimit(context))
        {
            await _next(context);
            return;
        }

        // 获取客户端标识（IP地址）
        var clientId = GetClientId(context);

        // 检查速率限制（先检查当前计数，如果允许再增加）
        var (isAllowed, record) = CheckAndIncrement(clientId);
        
        if (!isAllowed)
        {
            Log.Warning("速率限制：客户端 {ClientId} 超过请求频率限制，请求路径 {Path}, 当前计数: {Count}",
                clientId, context.Request.Path, record.RequestCount);

            context.Response.StatusCode = 429; // Too Many Requests
            context.Response.ContentType = "application/json";
            context.Response.Headers.Append("Retry-After", _timeWindow.TotalSeconds.ToString());

            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = $"请求过于频繁，请稍后再试。限制：{_maxRequests} 次/{_timeWindow.TotalSeconds} 秒"
                }),
                Encoding.UTF8);
            return;
        }

        // 添加速率限制响应头
        var remaining = Math.Max(0, _maxRequests - record.RequestCount);
        context.Response.Headers.Append("X-RateLimit-Limit", _maxRequests.ToString());
        context.Response.Headers.Append("X-RateLimit-Remaining", remaining.ToString());
        context.Response.Headers.Append("X-RateLimit-Reset", record.ResetTime.ToUnixTimeSeconds().ToString());

        await _next(context);
    }

    /// <summary>
    /// 检查是否应该跳过速率限制
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>是否跳过</returns>
    private static bool ShouldSkipRateLimit(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        
        // SignalR negotiation 和 hub 连接应该跳过速率限制
        if (path.Contains("/hubs/", StringComparison.OrdinalIgnoreCase) ||
            path.Contains("/negotiate", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        
        // 健康检查接口跳过速率限制
        if (path.Contains("/TaktHealth", StringComparison.OrdinalIgnoreCase) ||
            path.Contains("/health", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        
        // 静态资源跳过速率限制
        if (path.StartsWith("/_framework/", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/css/", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/js/", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/images/", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/fonts/", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// 获取客户端标识
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>客户端标识</returns>
    private static string GetClientId(HttpContext context)
    {
        // 优先使用X-Forwarded-For头（适用于反向代理）
        var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            return forwardedFor.Split(',')[0].Trim();
        }

        // 使用RemoteIpAddress
        return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }

    /// <summary>
    /// 检查速率限制并增加计数（如果允许）
    /// </summary>
    /// <param name="clientId">客户端标识</param>
    /// <returns>是否允许和更新后的记录</returns>
    private (bool IsAllowed, RateLimitRecord Record) CheckAndIncrement(string clientId)
    {
        var now = DateTime.UtcNow;
        var shouldIncrement = true;

        // 使用 AddOrUpdate 原子性地检查和更新计数
        var record = _rateLimitStore.AddOrUpdate(
            clientId,
            _ => new RateLimitRecord
            {
                RequestCount = 1, // 新客户端，首次请求
                ResetTime = now.Add(_timeWindow)
            },
            (key, existing) =>
            {
                // 如果时间窗口已过期，重置计数
                if (now >= existing.ResetTime)
                {
                    return new RateLimitRecord
                    {
                        RequestCount = 1, // 重置后，首次请求
                        ResetTime = now.Add(_timeWindow)
                    };
                }

                // 检查是否已经达到限制（在增加计数之前）
                // 如果已经达到限制，不增加计数，保持原样
                if (existing.RequestCount >= _maxRequests)
                {
                    shouldIncrement = false;
                    return existing; // 返回原记录，不增加计数
                }

                // 如果未达到限制，增加请求计数
                existing.RequestCount++;
                return existing;
            });

        // 检查是否超过限制
        if (!shouldIncrement || record.RequestCount > _maxRequests)
        {
            return (false, record);
        }

        return (true, record);
    }
}

/// <summary>
/// DateTime扩展方法
/// </summary>
internal static class DateTimeExtensions
{
    /// <summary>
    /// 转换为Unix时间戳（秒）
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>Unix时间戳</returns>
    public static long ToUnixTimeSeconds(this DateTime dateTime)
    {
        return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
    }
}
