// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Helpers
// 文件名称：TaktLoggingExclusions.cs
// 创建时间：2025-01-22
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt日志排除配置，统一管理操作日志和差异日志的排除规则
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Infrastructure.Helpers;

/// <summary>
/// Takt日志排除配置
/// 统一管理操作日志（OperLog）和差异日志（AopLog）的排除规则
/// </summary>
public static class TaktLoggingExclusions
{
    /// <summary>
    /// 种子数据操作标志（使用 AsyncLocal 确保线程安全）
    /// 当执行种子数据初始化时，设置为 true，差异日志将跳过记录
    /// </summary>
    private static readonly AsyncLocal<bool> _isSeedingData = new();

    /// <summary>
    /// 排除的操作日志路径（用于 TaktOperLogMiddleware）
    /// 这些路径的操作不会被记录到操作日志中
    /// </summary>
    public static readonly HashSet<string> ExcludedOperLogPaths = new(StringComparer.OrdinalIgnoreCase)
    {
        // 日志管理相关的API，避免循环记录
        "/api/TaktOperLogs",
        "/api/TaktAopLogs",
        
        // 验证码相关的API，不需要记录日志
        "/api/TaktCaptcha",
        
        // Health检查相关的API
        "/api/TaktHealth",
        "/health",
        
        // Swagger、SignalR、静态文件等
        "/swagger",
        "/hubs/",
        "/negotiate",
        "/favicon.ico"
    };

    /// <summary>
    /// 排除的差异日志表名（用于 TaktAutofacModule OnDiffLogEvent）
    /// 这些表的数据变更不会被记录到差异日志中
    /// </summary>
    public static readonly HashSet<string> ExcludedAopLogTableNames = new(StringComparer.OrdinalIgnoreCase)
    {
        // 日志实体自身，避免循环记录
        "takt_logging_aop_log",
        "takt_logging_oper_log",
        "takt_logging_login_log",
        "takt_logging_quartz_log"
    };

    /// <summary>
    /// 判断路径是否应该被排除（不记录操作日志）
    /// </summary>
    /// <param name="path">请求路径</param>
    /// <returns>如果应该排除返回 true，否则返回 false</returns>
    public static bool ShouldExcludeOperLogPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return false;

        // 检查是否包含或匹配排除的路径
        foreach (var excludedPath in ExcludedOperLogPaths)
        {
            // 如果排除路径以 "/" 结尾，使用 StartsWith 检查
            // 否则使用 Contains 检查（适用于部分匹配）
            if (excludedPath.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                if (path.StartsWith(excludedPath, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            else
            {
                if (path.Contains(excludedPath, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 判断表名是否应该被排除（不记录差异日志）
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>如果应该排除返回 true，否则返回 false</returns>
    public static bool ShouldExcludeAopLogTable(string tableName)
    {
        if (string.IsNullOrEmpty(tableName))
            return false;

        return ExcludedAopLogTableNames.Contains(tableName);
    }

    /// <summary>
    /// 判断当前是否在执行种子数据操作（不记录差异日志）
    /// </summary>
    /// <returns>如果正在执行种子数据操作返回 true，否则返回 false</returns>
    public static bool IsSeedingData()
    {
        return _isSeedingData.Value;
    }

    /// <summary>
    /// 设置种子数据操作标志
    /// </summary>
    /// <param name="isSeeding">是否正在执行种子数据操作</param>
    public static void SetSeedingData(bool isSeeding)
    {
        _isSeedingData.Value = isSeeding;
    }

    /// <summary>
    /// 判断是否是 Create、Update、Delete 操作（需要记录操作日志的操作类型）
    /// </summary>
    /// <param name="operMethod">操作方法名</param>
    /// <param name="requestMethod">HTTP请求方法</param>
    /// <returns>如果是 Create、Update、Delete 操作返回 true，否则返回 false</returns>
    public static bool IsCreateUpdateDeleteOperation(string? operMethod, string requestMethod)
    {
        // 如果提供了操作方法名，根据方法名判断
        if (!string.IsNullOrEmpty(operMethod))
        {
            var methodName = operMethod.ToLowerInvariant();
            
            // Create 操作
            if (methodName.StartsWith("create", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("add", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("insert", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("import", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            
            // Update 操作
            if (methodName.StartsWith("update", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("edit", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("modify", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("change", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("reset", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("assign", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("unlock", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            
            // Delete 操作
            if (methodName.StartsWith("delete", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("remove", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        
        // 如果无法从方法名判断，则根据HTTP方法判断（作为后备方案）
        var httpMethod = requestMethod.ToUpperInvariant();
        return httpMethod == "POST" || httpMethod == "PUT" || httpMethod == "PATCH" || httpMethod == "DELETE";
    }
}
