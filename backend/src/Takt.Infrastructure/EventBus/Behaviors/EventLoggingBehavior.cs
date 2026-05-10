// ========================================
// 项目名称:节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间:Takt.Infrastructure.EventBus.Behaviors
// 文件名称:EventLoggingBehavior.cs
// 创建时间:2026-05-10
// 创建人:Takt365
// 功能描述:事件日志行为管道 - 自动记录所有事件的处理日志
// 
// 版权信息:Copyright (c) 2025 Takt  All rights reserved.
// 免责声明:此软件使用 MIT License,作者不承担任何使用风险。
// ========================================

using MediatR;
using Takt.Shared.Helpers;
using System.Diagnostics;

namespace Takt.Infrastructure.EventBus.Behaviors;

/// <summary>
/// 事件日志行为管道
/// </summary>
/// <typeparam name="TRequest">事件类型</typeparam>
/// <typeparam name="TResponse">响应类型</typeparam>
public class EventLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    /// <summary>
    /// 执行事件处理并记录日志
    /// </summary>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var eventName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // 记录事件开始处理
            TaktLogger.Debug("[Event Start] 开始处理事件: {EventName}", eventName);

            // 执行实际的事件处理
            var response = await next();

            stopwatch.Stop();

            // 记录事件处理成功
            TaktLogger.Debug(
                "[Event Success] 事件处理完成: {EventName}, 耗时: {ElapsedMs}ms",
                eventName,
                stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            // 记录事件处理异常
            TaktLogger.Error(
                ex,
                "[Event Error] 事件处理失败: {EventName}, 耗时: {ElapsedMs}ms, 错误: {ErrorMessage}",
                eventName,
                stopwatch.ElapsedMilliseconds,
                ex.Message);

            throw;
        }
    }
}
