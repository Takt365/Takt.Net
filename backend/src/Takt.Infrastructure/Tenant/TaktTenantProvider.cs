// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Tenant
// 文件名称：TaktTenantProvider.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt租户提供者，从配置中获取租户连接字符串
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Data;
using Takt.Shared.Constants;

namespace Takt.Infrastructure.Tenant;

/// <summary>
/// Takt租户提供者
/// </summary>
public class TaktTenantProvider : ITaktTenantContext
{
    private readonly IConfiguration _configuration;
    private readonly TaktSqlSugarDbContext _dbContext;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="dbContext">数据库上下文（用于直接查询数据库，避免循环依赖）</param>
    public TaktTenantProvider(IConfiguration configuration, TaktSqlSugarDbContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }

    /// <summary>
    /// 获取当前租户实体（完整数据）
    /// </summary>
    /// <returns>租户实体，如果未设置则返回null</returns>
    public TaktTenant? GetCurrentTenant()
    {
        return TaktTenantContext.CurrentTenant;
    }

    /// <summary>
    /// 获取当前租户实体（完整数据，异步）
    /// </summary>
    /// <returns>租户实体，如果未设置则返回null</returns>
    public async Task<TaktTenant?> GetCurrentTenantAsync()
    {
        // 优先从上下文获取
        if (TaktTenantContext.CurrentTenant != null)
        {
            return TaktTenantContext.CurrentTenant;
        }

        // 获取当前 ConfigId
        var configId = GetCurrentConfigId();
        if (string.IsNullOrEmpty(configId))
        {
            return null;
        }

        // 从数据库查询租户信息（通过 ConfigId，直接使用数据库客户端，避免循环依赖）
        var db = _dbContext.GetClient(typeof(TaktTenant));
        var tenant = await db.Queryable<TaktTenant>()
            .Where(t => t.ConfigId == configId && t.IsDeleted == 0)
            .FirstAsync();
        
        if (tenant != null)
        {
            // 缓存到上下文
            TaktTenantContext.CurrentTenant = tenant;
        }

        return tenant;
    }

    /// <summary>
    /// 获取当前 ConfigId（数据库连接标识）
    /// </summary>
    /// <returns>ConfigId，如果未设置则返回null</returns>
    public string? GetCurrentConfigId()
    {
        return TaktTenantContext.CurrentConfigId ?? TaktTenantContext.CurrentTenant?.ConfigId;
    }

    /// <summary>
    /// 获取当前连接字符串
    /// </summary>
    /// <returns>连接字符串，如果未设置则返回null</returns>
    public string? GetCurrentConnectionString()
    {
        return TaktTenantContext.CurrentConnectionString;
    }

    /// <summary>
    /// 获取默认连接字符串（用于判断是否使用单库模式）
    /// </summary>
    /// <returns>默认连接字符串，如果未设置则返回null</returns>
    public string? GetDefaultConnectionString()
    {
        return TaktTenantContext.DefaultConnectionString;
    }

    /// <summary>
    /// 是否使用多库模式（当前连接字符串与默认不同）
    /// </summary>
    /// <returns>如果使用多库模式返回true，否则返回false</returns>
    public bool IsMultiDatabaseMode()
    {
        return TaktTenantContext.IsMultiDatabaseMode;
    }

    /// <summary>
    /// 获取连接字符串（异步）
    /// </summary>
    /// <param name="configId">ConfigId</param>
    /// <returns>连接字符串</returns>
    public Task<string?> GetConnectionStringAsync(string configId)
    {
        // 从 dbConfigs 配置节点获取配置
        var dbConfigsSection = _configuration.GetSection("dbConfigs");
        foreach (var dbConfig in dbConfigsSection.GetChildren())
        {
            if (dbConfig["ConfigId"] == configId)
            {
                var conn = dbConfig["Conn"];
                if (!string.IsNullOrEmpty(conn))
                {
                    return Task.FromResult<string?>(conn);
                }
            }
        }

        // 如果没有找到，返回主库连接字符串
        var tenantSection = _configuration.GetSection("Tenant");
        var mainDb = tenantSection["DefaultConfigId"] ?? "0";
        return GetConnectionStringAsync(mainDb);
    }

    /// <summary>
    /// 获取当前租户的开始时间
    /// </summary>
    /// <returns>开始时间，如果未设置则返回null</returns>
    public DateTime? GetCurrentStartTime()
    {
        return TaktTenantContext.CurrentTenant?.SubscriptionStartTime;
    }

    /// <summary>
    /// 获取当前租户的结束时间
    /// </summary>
    /// <returns>结束时间，如果未设置则返回null</returns>
    public DateTime? GetCurrentEndTime()
    {
        return TaktTenantContext.CurrentTenant?.SubscriptionEndTime;
    }

    /// <summary>
    /// 获取当前租户的状态（0=启用，1=禁用）
    /// </summary>
    /// <returns>租户状态，如果未设置则返回null</returns>
    public int? GetCurrentTenantStatus()
    {
        return TaktTenantContext.CurrentTenant?.TenantStatus;
    }
}