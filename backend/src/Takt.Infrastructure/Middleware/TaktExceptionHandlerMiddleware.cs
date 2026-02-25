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
using SqlSugar;
using Takt.Infrastructure.Localization;
using Takt.Shared.Enums;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// Takt全局异常处理中间件
/// </summary>
public class TaktExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">下一个中间件</param>
    public TaktExceptionHandlerMiddleware(RequestDelegate next)
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
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// 处理异常
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <param name="exception">异常</param>
    /// <returns>任务</returns>
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.OK; // API统一返回200，通过Code区分

        TaktApiResult<object> result;
        string logMessage;
        LogEventLevel logLevel;

        // 根据异常类型处理
        switch (exception)
        {
            case TaktBusinessException businessEx:
                // 业务异常（预期异常，记录警告级别）
                result = TaktApiResult<object>.Fail(businessEx.Message, TaktResultCode.BusinessError);
                logMessage = $"业务异常: {businessEx.Message}";
                logLevel = LogEventLevel.Warning;
                break;

            case ArgumentNullException argEx:
                // 参数为空异常（预期异常，记录警告级别）
                result = TaktApiResult<object>.Fail($"参数错误: {argEx.ParamName ?? argEx.Message}", TaktResultCode.ParameterError);
                logMessage = $"参数错误: {argEx.ParamName ?? argEx.Message}";
                logLevel = LogEventLevel.Warning;
                break;

            case ArgumentException argEx:
                // 参数异常（预期异常，记录警告级别）
                result = TaktApiResult<object>.Fail($"参数错误: {argEx.Message}", TaktResultCode.ParameterError);
                logMessage = $"参数错误: {argEx.Message}";
                logLevel = LogEventLevel.Warning;
                break;

            case UnauthorizedAccessException:
                // 未授权异常（预期异常，记录警告级别）
                result = TaktApiResult<object>.Fail("未授权访问", TaktResultCode.Unauthorized);
                logMessage = "未授权访问";
                logLevel = LogEventLevel.Warning;
                break;

            case SqlSugarException sqlEx when IsConnectionError(sqlEx.Message):
                result = TaktApiResult<object>.Fail("数据库连接异常，请稍后重试", TaktResultCode.ServiceUnavailable);
                logMessage = "数据库连接异常";
                logLevel = LogEventLevel.Error;
                break;

            case InvalidOperationException invEx when IsConnectionError(invEx.Message):
                result = TaktApiResult<object>.Fail("数据库连接异常，请稍后重试", TaktResultCode.ServiceUnavailable);
                logMessage = "数据库连接异常";
                logLevel = LogEventLevel.Error;
                break;

            default:
                // 其他未预期异常（系统错误，记录错误级别）
                result = TaktApiResult<object>.Fail("系统内部错误，请稍后重试", TaktResultCode.SystemError);
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

    /// <summary>
    /// 判断是否为数据库连接相关异常（用于返回 503 友好提示）
    /// </summary>
    private static bool IsConnectionError(string? message)
    {
        if (string.IsNullOrEmpty(message)) return false;
        var m = message.ToLowerInvariant();
        return m.Contains("连接被关闭") || m.Contains("连接已关闭") || m.Contains("连接的当前状态")
            || m.Contains("beginexecutereader") || m.Contains("connection open error")
            || m.Contains("unable to cast object") || m.Contains("内部连接致命错误");
    }
}
