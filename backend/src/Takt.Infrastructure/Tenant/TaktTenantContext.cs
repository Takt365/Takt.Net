// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Tenant
// 文件名称：TaktTenantContext.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt租户上下文，存储当前请求的租户信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Domain.Entities.Identity;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.Tenant;

/// <summary>
/// Takt租户上下文
/// </summary>
public class TaktTenantContext
{
    private static readonly AsyncLocal<string?> _currentConfigId = new();
    private static readonly AsyncLocal<string?> _currentConnectionString = new();
    private static readonly AsyncLocal<string?> _defaultConnectionString = new();
    private static readonly AsyncLocal<TaktTenant?> _currentTenant = new();
    private static readonly AsyncLocal<bool> _isTenantEnabled = new();

    /// <summary>
    /// 当前 ConfigId（数据库连接标识）
    /// </summary>
    public static string? CurrentConfigId
    {
        get => _currentConfigId.Value;
        set => _currentConfigId.Value = value;
    }

    /// <summary>
    /// 当前连接字符串
    /// </summary>
    public static string? CurrentConnectionString
    {
        get => _currentConnectionString.Value;
        set => _currentConnectionString.Value = value;
    }

    /// <summary>
    /// 默认连接字符串
    /// </summary>
    public static string? DefaultConnectionString
    {
        get => _defaultConnectionString.Value;
        set => _defaultConnectionString.Value = value;
    }

    /// <summary>
    /// 当前租户实体（完整数据）
    /// </summary>
    public static TaktTenant? CurrentTenant
    {
        get => _currentTenant.Value;
        set
        {
            var oldTenant = _currentTenant.Value;
            _currentTenant.Value = value;
            
            // 输出日志：租户上下文变更
            if (value != null && (oldTenant == null || oldTenant.Id != value.Id))
            {
                TaktLogger.Information("租户上下文已设置: TenantId: {TenantId}, TenantCode: {TenantCode}, TenantName: {TenantName}, ConfigId: {ConfigId}", 
                    value.Id, value.TenantCode, value.TenantName, value.ConfigId);
            }
            else if (value == null && oldTenant != null)
            {
                TaktLogger.Information("租户上下文已清除: TenantId: {TenantId}, TenantCode: {TenantCode}", 
                    oldTenant.Id, oldTenant.TenantCode);
            }
        }
    }

    /// <summary>
    /// 租户功能是否启用（基于配置的 Tenant.Enabled）
    /// </summary>
    public static bool IsTenantEnabled
    {
        get => _isTenantEnabled.Value;
        set => _isTenantEnabled.Value = value;
    }

    /// <summary>
    /// 是否使用多库模式（租户启用时为多库模式，未启用时为单库模式）
    /// </summary>
    public static bool IsMultiDatabaseMode => IsTenantEnabled;
}