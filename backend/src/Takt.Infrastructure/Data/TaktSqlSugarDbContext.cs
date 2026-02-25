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
using Takt.Infrastructure.Tenant;
using Takt.Shared.Constants;

namespace Takt.Infrastructure.Data;

/// <summary>
/// Takt SqlSugar数据库上下文
/// </summary>
public class TaktSqlSugarDbContext
{
    private readonly ISqlSugarClient _client;
    private static readonly ConcurrentDictionary<string, bool> _registeredConfigs = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="client">SqlSugar客户端</param>
    public TaktSqlSugarDbContext(ISqlSugarClient client)
    {
        _client = client;
    }

    /// <summary>
    /// 获取数据库客户端（按实体类型选择库，租户禁用/启用均为多库）
    /// </summary>
    /// <param name="entityType">实体类型（可选，用于自动路由到对应的数据库）</param>
    /// <returns>SqlSugar客户端</returns>
    /// <remarks>
    /// 租户禁用与启用均使用多库（0～5 按实体命名空间映射）。有 entityType 时按映射取 ConfigId，否则用 CurrentConfigId 或主库。
    /// 租户启用时由中间件/仓储设置 CurrentConfigId 并做按租户过滤；禁用时不按租户过滤。
    /// </remarks>
    public ISqlSugarClient GetClient(Type? entityType = null)
    {
        string configId;

        if (entityType != null)
        {
            var entityNamespace = entityType.Namespace ?? string.Empty;
            configId = TaktEntityDatabaseMapping.GetConfigIdByEntityNamespace(entityNamespace);
        }
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