// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data
// 文件名称：TaktSqlSugarDbContext.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt SqlSugar数据库上下文，管理多租户数据库连接
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Takt.Infrastructure.Tenant;
using Takt.Shared.Constants;

namespace Takt.Infrastructure.Data;

/// <summary>
/// Takt SqlSugar数据库上下文
/// </summary>
public class TaktSqlSugarDbContext
{
    private readonly ISqlSugarClient _client;
    private readonly IConfiguration? _configuration;
    private static readonly ConcurrentDictionary<string, bool> _registeredConfigs = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="client">SqlSugar客户端</param>
    /// <param name="configuration">可选；提供时实体分库 ConfigId 与 appsettings 中 dbConfigs 对齐（与仓储持久化 ConfigId 一致）。</param>
    public TaktSqlSugarDbContext(ISqlSugarClient client, IConfiguration? configuration = null)
    {
        _client = client;
        _configuration = configuration;
    }

    /// <summary>
    /// 获取数据库客户端（根据实体类型自动切换数据库）
    /// </summary>
    /// <param name="entityType">实体类型（可选，用于自动路由到对应的数据库）</param>
    /// <returns>SqlSugar客户端</returns>
    public ISqlSugarClient GetClient(Type? entityType = null)
    {
        string configId;
        
        // 如果提供了实体类型，根据实体类型自动获取对应的 ConfigId（多库自动切换）
        if (entityType != null)
        {
            configId = TaktEntityDatabaseMapping.GetPersistenceConfigIdForEntityType(entityType, _configuration);
        }
        // 否则使用租户上下文中的 ConfigId（多租户模式）
        else
        {
            configId = TaktTenantContext.CurrentConfigId ?? TaktAppConstants.DefaultConfigId;
        }
        
        return GetClientByConfigId(configId);
    }

    /// <summary>
    /// 根据 ConfigId 直接获取数据库客户端（用于跨库查询）
    /// </summary>
    /// <param name="configId">数据库配置ID</param>
    /// <returns>SqlSugar客户端</returns>
    /// <exception cref="InvalidOperationException">当无法获取数据库客户端时抛出异常</exception>
    public ISqlSugarClient GetClientByConfigId(string configId)
    {
        if (_client == null)
        {
            throw new InvalidOperationException("SqlSugar客户端未初始化");
        }

        var tenant = _client.AsTenant();
        if (tenant == null)
        {
            throw new InvalidOperationException($"无法获取租户客户端，ConfigId: {configId}");
        }
        
        // 按照官方Demo方式：使用 GetConnectionScope 获取数据库连接
        // 配置已经在初始化时从 dbConfigs 加载，直接切换即可
        var connection = tenant.GetConnectionScope(configId);
        if (connection == null)
        {
            throw new InvalidOperationException($"无法获取数据库连接，ConfigId: {configId}。请检查 appsettings.json 中的 dbConfigs 配置是否包含此 ConfigId");
        }
        
        // 确保映射信息已初始化（通过访问 EntityMaintenance 触发初始化）
        // 这可以避免在调用 Insertable 时出现空引用异常
        try
        {
            _ = connection.EntityMaintenance;
        }
        catch
        {
            // 忽略初始化错误，让后续操作自然失败并抛出更明确的错误
        }
        
        return connection;
    }
}