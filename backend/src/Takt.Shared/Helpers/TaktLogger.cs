// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Helpers
// 文件名称：TaktLogger.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt统一日志帮助类，使用Serilog进行日志记录
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Helpers;

/// <summary>
/// Takt统一日志帮助类
/// </summary>
/// <remarks>
/// 符合 Serilog 规范：
/// - 支持所有日志级别（Verbose, Debug, Information, Warning, Error, Fatal）
/// - 支持结构化日志（使用消息模板和参数）
/// - 支持异常日志记录
/// - 支持日志上下文属性
/// </remarks>
public static class TaktLogger
{
    /// <summary>
    /// 记录详细调试信息（Verbose级别）
    /// </summary>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Verbose(string messageTemplate, params object[]? propertyValues)
    {
        Log.Verbose(messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录详细调试信息（Verbose级别）
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Verbose(Exception exception, string messageTemplate, params object[]? propertyValues)
    {
        Log.Verbose(exception, messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录调试信息（Debug级别）
    /// </summary>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Debug(string messageTemplate, params object[]? propertyValues)
    {
        Log.Debug(messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录调试信息（Debug级别）
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Debug(Exception exception, string messageTemplate, params object[]? propertyValues)
    {
        Log.Debug(exception, messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录信息（Information级别）
    /// </summary>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Information(string messageTemplate, params object[]? propertyValues)
    {
        Log.Information(messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录信息（Information级别）
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Information(Exception exception, string messageTemplate, params object[]? propertyValues)
    {
        Log.Information(exception, messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录警告（Warning级别）
    /// </summary>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Warning(string messageTemplate, params object[]? propertyValues)
    {
        Log.Warning(messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录警告（Warning级别）
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Warning(Exception exception, string messageTemplate, params object[]? propertyValues)
    {
        Log.Warning(exception, messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录错误（Error级别）
    /// </summary>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Error(string messageTemplate, params object[]? propertyValues)
    {
        Log.Error(messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录错误（Error级别）
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Error(Exception exception, string messageTemplate, params object[]? propertyValues)
    {
        Log.Error(exception, messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录致命错误（Fatal级别）
    /// </summary>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Fatal(string messageTemplate, params object[]? propertyValues)
    {
        Log.Fatal(messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录致命错误（Fatal级别）
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    public static void Fatal(Exception exception, string messageTemplate, params object[]? propertyValues)
    {
        Log.Fatal(exception, messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 使用属性记录日志
    /// </summary>
    /// <param name="propertyName">属性名</param>
    /// <param name="value">属性值</param>
    /// <returns>可释放的上下文</returns>
    public static IDisposable PushProperty(string propertyName, object? value)
    {
        return LogContext.PushProperty(propertyName, value);
    }

    /// <summary>
    /// 使用多个属性记录日志
    /// </summary>
    /// <param name="properties">属性字典</param>
    /// <returns>可释放的上下文</returns>
    /// <remarks>
    /// 使用嵌套的 PushProperty 来确保属性在同一个上下文中
    /// </remarks>
    public static IDisposable PushProperties(Dictionary<string, object?> properties)
    {
        if (properties == null || properties.Count == 0)
        {
            return new EmptyDisposable();
        }

        IDisposable? result = null;
        foreach (var property in properties)
        {
            if (result == null)
            {
                result = LogContext.PushProperty(property.Key, property.Value);
            }
            else
            {
                // 嵌套 PushProperty 确保所有属性在同一个上下文中
                result = new CompositeDisposable(
                    result,
                    LogContext.PushProperty(property.Key, property.Value)
                );
            }
        }
        return result ?? new EmptyDisposable();
    }

    /// <summary>
    /// 检查是否启用指定级别
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <returns>是否启用</returns>
    public static bool IsEnabled(LogEventLevel level)
    {
        return Log.IsEnabled(level);
    }

    /// <summary>
    /// 复合释放器（用于嵌套的 PushProperty）
    /// </summary>
    private class CompositeDisposable : IDisposable
    {
        private readonly IDisposable _first;
        private readonly IDisposable _second;

        public CompositeDisposable(IDisposable first, IDisposable second)
        {
            _first = first;
            _second = second;
        }

        public void Dispose()
        {
            // 按相反顺序释放（后进先出）
            _second?.Dispose();
            _first?.Dispose();
        }
    }

    /// <summary>
    /// 空释放器（用于无属性时返回）
    /// </summary>
    private class EmptyDisposable : IDisposable
    {
        public void Dispose()
        {
            // 无操作
        }
    }
}
