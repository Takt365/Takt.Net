// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Middleware
// 文件名称：TaktExceptionHandlerMiddleware.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt全局异常处理中间件，统一处理应用程序异常
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Net;
using Newtonsoft.Json;
using Serilog.Events;
using Takt.Domain.Interfaces;
using Takt.Shared.Enums;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Infrastructure.Localization;
using Takt.Shared.Models;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// Takt全局异常处理中间件
/// </summary>
public class TaktExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITaktLocalizer? _localizer;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">下一个中间件</param>
    public TaktExceptionHandlerMiddleware(RequestDelegate next, ITaktLocalizer? localizer = null)
    {
        _next = next;
        _localizer = localizer;
    }

    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>任务</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // 记录 SignalR negotiate 请求（用于调试）
            var path = context.Request.Path.Value ?? string.Empty;
            var method = context.Request.Method;
            
            // 记录所有 SignalR 相关请求（包括 negotiate）
            if (path.Contains("/hubs/", StringComparison.OrdinalIgnoreCase) || 
                path.Contains("/negotiate", StringComparison.OrdinalIgnoreCase))
            {
                TaktLogger.Information("SignalR 请求到达异常处理中间件: {Path}, Method: {Method}, QueryString: {QueryString}", 
                    path, method, context.Request.QueryString.Value ?? string.Empty);
            }
            
            await _next(context);
            
            // 记录 SignalR negotiate 响应状态（用于调试）
            if (path.Contains("/hubs/", StringComparison.OrdinalIgnoreCase) || 
                path.Contains("/negotiate", StringComparison.OrdinalIgnoreCase))
            {
                TaktLogger.Information("SignalR 请求处理完成: {Path}, Method: {Method}, StatusCode: {StatusCode}, ResponseStarted: {ResponseStarted}", 
                    path, method, context.Response.StatusCode, context.Response.HasStarted);
            }
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, _localizer);
        }
    }

    /// <summary>
    /// 处理异常
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <param name="exception">异常</param>
    /// <returns>任务</returns>
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ITaktLocalizer? localizer)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.OK; // API统一返回200，通过Code区分

        TaktApiResult<object> result;
        string logMessage;
        LogEventLevel logLevel;

        // 根据异常类型处理
        switch (exception)
        {
            case TaktLocalizedException localizedEx:
                result = TaktApiResult<object>.Fail(
                    await ResolveLocalizedMessageAsync(localizer, localizedEx.MessageKey, localizedEx.ResourceType, localizedEx.Arguments).ConfigureAwait(false),
                    TaktResultCode.BusinessError);
                logMessage = $"业务异常(本地化键): {localizedEx.MessageKey}";
                logLevel = LogEventLevel.Warning;
                break;

            case TaktBusinessException businessEx:
                // 业务异常（预期异常，记录警告级别）
                var businessMessage = await ResolveBusinessMessageAsync(localizer, businessEx.Message).ConfigureAwait(false);
                if (businessEx.Data.Contains("validationErrors"))
                {
                    result = new TaktApiResult<object>
                    {
                        Code = TaktResultCode.BusinessError,
                        Message = businessMessage,
                        Data = new
                        {
                            validationErrors = businessEx.Data["validationErrors"]
                        }
                    };
                }
                else
                {
                    result = TaktApiResult<object>.Fail(businessMessage, TaktResultCode.BusinessError);
                }
                logMessage = $"业务异常: {businessEx.Message}";
                logLevel = LogEventLevel.Warning;
                break;

            case ArgumentNullException argEx:
                // 参数为空异常（预期异常，记录警告级别）
                var argNullMessage = await ResolveLocalizedMessageAsync(localizer, "validation.argumentNull", "Frontend", argEx.ParamName ?? argEx.Message).ConfigureAwait(false);
                result = TaktApiResult<object>.Fail(argNullMessage, TaktResultCode.ParameterError);
                logMessage = $"参数错误: {argEx.ParamName ?? argEx.Message}";
                logLevel = LogEventLevel.Warning;
                break;

            case ArgumentException argEx:
                // 参数异常（预期异常，记录警告级别）
                var argMessage = await ResolveLocalizedMessageAsync(localizer, "validation.argumentError", "Frontend", argEx.Message).ConfigureAwait(false);
                result = TaktApiResult<object>.Fail(argMessage, TaktResultCode.ParameterError);
                logMessage = $"参数错误: {argEx.Message}";
                logLevel = LogEventLevel.Warning;
                break;

            case UnauthorizedAccessException:
                // 未授权异常（预期异常，记录警告级别）
                var unauthorizedMessage = await ResolveLocalizedMessageAsync(localizer, "validation.unauthorizedAccess", "Frontend").ConfigureAwait(false);
                result = TaktApiResult<object>.Fail(unauthorizedMessage, TaktResultCode.Unauthorized);
                logMessage = unauthorizedMessage;
                logLevel = LogEventLevel.Warning;
                break;

            default:
                // 其他未预期异常（系统错误，记录错误级别）
                var systemErrorMessage = await ResolveLocalizedMessageAsync(localizer, "validation.systemInternalError", "Frontend").ConfigureAwait(false);
                result = TaktApiResult<object>.Fail(systemErrorMessage, TaktResultCode.SystemError);
                logMessage = $"系统异常: {exception.Message}";
                logLevel = LogEventLevel.Error;
                break;
        }

        // 统一记录日志（包含请求信息）
        using (TaktLogger.PushProperty("RequestPath", context.Request.Path))
        using (TaktLogger.PushProperty("RequestMethod", context.Request.Method))
        using (TaktLogger.PushProperty("ExceptionType", exception.GetType().Name))
        {
            if (logLevel == LogEventLevel.Error)
            {
                TaktLogger.Error(exception, "{LogMessage} | 请求路径: {Path} {Method}", 
                    logMessage, context.Request.Path, context.Request.Method);
            }
            else
            {
                TaktLogger.Warning(exception, "{LogMessage} | 请求路径: {Path} {Method}", 
                    logMessage, context.Request.Path, context.Request.Method);
            }
        }

        // 序列化响应（使用 Newtonsoft.Json）
        // 注意：使用 CamelCasePropertyNamesContractResolver 转换为 camelCase，与正常响应保持一致
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None,
            DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };

        var json = JsonConvert.SerializeObject(result, settings);
        await context.Response.WriteAsync(json);
    }

    private static async Task<string> ResolveBusinessMessageAsync(ITaktLocalizer? localizer, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return "validation.businessError";

        if (localizer == null)
            return message;

        var backendText = await localizer.GetStringAsync(message, "Backend").ConfigureAwait(false);
        if (!string.Equals(backendText, message, StringComparison.Ordinal))
            return backendText;

        var frontendText = await localizer.GetStringAsync(message, "Frontend").ConfigureAwait(false);
        if (!string.Equals(frontendText, message, StringComparison.Ordinal))
            return frontendText;

        return message;
    }

    private static async Task<string> ResolveLocalizedMessageAsync(ITaktLocalizer? localizer, string key, string resourceType = "Backend", params object[] arguments)
    {
        if (localizer == null) return key;
        return arguments.Length == 0
            ? await localizer.GetStringAsync(key, resourceType).ConfigureAwait(false)
            : await localizer.GetStringAsync(key, resourceType, arguments).ConfigureAwait(false);
    }
}
