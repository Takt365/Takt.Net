// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Middleware
// 文件名称：TaktCsrfProtectionMiddleware.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt CSRF防护中间件，防止跨站请求伪造攻击
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Text;
using Newtonsoft.Json;
using Serilog;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// Takt CSRF防护中间件
/// </summary>
public class TaktCsrfProtectionMiddleware
{
    private readonly RequestDelegate _next;
    private const string CsrfTokenHeader = "X-CSRF-Token";
    private const string CsrfTokenCookie = "CSRF-Token";

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">下一个中间件</param>
    public TaktCsrfProtectionMiddleware(RequestDelegate next)
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
        // 检查是否需要跳过CSRF验证（SignalR negotiation、健康检查等）
        if (ShouldSkipCsrfProtection(context))
        {
            await _next(context);
            return;
        }

        // 对于GET请求，生成并设置CSRF Token
        if (context.Request.Method == "GET")
        {
            var token = GenerateCsrfToken();
            context.Response.Cookies.Append(CsrfTokenCookie, token, new CookieOptions
            {
                HttpOnly = false, // 允许JavaScript读取
                Secure = context.Request.IsHttps,
                SameSite = SameSiteMode.Strict,
                Path = "/"
            });
        }
        // 对于需要保护的HTTP方法（POST, PUT, DELETE, PATCH），验证CSRF Token
        else if (IsProtectedMethod(context.Request.Method))
        {
            var headerToken = context.Request.Headers[CsrfTokenHeader].FirstOrDefault();
            var cookieToken = context.Request.Cookies[CsrfTokenCookie];

            if (string.IsNullOrEmpty(headerToken) || string.IsNullOrEmpty(cookieToken) || headerToken != cookieToken)
            {
                Log.Warning("CSRF攻击检测：请求来自 {RemoteIpAddress}，路径 {Path}，方法 {Method}",
                    context.Connection.RemoteIpAddress, context.Request.Path, context.Request.Method);

                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new
                    {
                        success = false,
                        message = "CSRF token验证失败，请求被拒绝"
                    }),
                    Encoding.UTF8);
                return;
            }
        }

        await _next(context);
    }

    /// <summary>
    /// 检查是否应该跳过CSRF保护
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>是否跳过</returns>
    private static bool ShouldSkipCsrfProtection(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        
        // SignalR negotiation 和 hub 连接应该跳过CSRF保护
        if (path.Contains("/hubs/", StringComparison.OrdinalIgnoreCase) ||
            path.Contains("/negotiate", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        
        // Swagger UI 和 Swagger JSON 文档应该跳过CSRF保护
        if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        
        // 健康检查接口跳过CSRF保护
        if (path.Contains("/TaktHealth", StringComparison.OrdinalIgnoreCase) ||
            path.Contains("/health", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// 判断是否为需要保护的HTTP方法
    /// </summary>
    /// <param name="method">HTTP方法</param>
    /// <returns>是否需要保护</returns>
    private static bool IsProtectedMethod(string method)
    {
        return method == "POST" || method == "PUT" || method == "DELETE" || method == "PATCH";
    }

    /// <summary>
    /// 生成CSRF Token
    /// </summary>
    /// <returns>CSRF Token</returns>
    private static string GenerateCsrfToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");
    }
}
