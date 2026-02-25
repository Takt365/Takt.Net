// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data
// 文件名称：TaktTableInitializer.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt数据表初始化器，负责数据库和表结构创建
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using System.Reflection;
using Microsoft.Extensions.Logging;
using Takt.Domain.Entities;

namespace Takt.Infrastructure.Data;

/// <summary>
/// Takt数据表初始化器
/// </summary>
public class TaktTableInitializer
{
    private readonly ISqlSugarClient _client;
    private readonly ILogger? _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="client">SqlSugar客户端</param>
    /// <param name="logger">日志记录器（可选）</param>
    public TaktTableInitializer(ISqlSugarClient client, ILogger? logger = null)
    {
        _client = client;
        _logger = logger;
    }

    /// <summary>
    /// 获取所有继承自TaktEntityBase的实体类型（搜索所有已加载的程序集）
    /// </summary>
    /// <returns>实体类型数组</returns>
    private static Type[] GetAllEntityTypes()
    {
        var entityBaseType = typeof(TaktEntityBase);
        var entityTypes = new List<Type>();

        // 搜索当前AppDomain中所有已加载的程序集（确保能找到所有实体类型）
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName != null && 
                       (a.FullName.StartsWith("Takt.", StringComparison.OrdinalIgnoreCase) ||
                        a.GetName().Name?.StartsWith("Takt.", StringComparison.OrdinalIgnoreCase) == true))
            .ToList();

        foreach (var assembly in assemblies)
        {
            try
            {
                var types = assembly.GetTypes()
                    .Where(t =>
                        t.IsClass &&
                        !t.IsAbstract &&
                        !t.IsGenericType &&
                        t.IsSubclassOf(typeof(TaktEntityBase)))
                    .ToList();
                entityTypes.AddRange(types);
            }
            catch (ReflectionTypeLoadException ex)
            {
                // 忽略加载失败的类型，只处理成功加载的类型
                var loadedTypes = ex.Types
                    .Where(t => t != null)
                    .Cast<Type>()
                    .Where(t =>
                        t.IsClass &&
                        !t.IsAbstract &&
                        !t.IsGenericType &&
                        t.IsSubclassOf(typeof(TaktEntityBase)))
                    .ToList();
                entityTypes.AddRange(loadedTypes);
            }
            catch (Exception)
            {
                // 忽略无法加载的程序集
                continue;
            }
        }

        return entityTypes.Distinct().ToArray();
    }

    /// <summary>
    /// 初始化数据库和表结构（根据业务领域自动分配到对应数据库）
    /// </summary>
    /// <param name="targetConfigId">目标数据库 ConfigId，如果为 null 则根据实体命名空间自动分配</param>
    /// <returns>任务</returns>
    public Task InitializeAsync(string? targetConfigId = null)
    {
        // 创建数据库（如果不存在）
        _logger?.LogInformation("正在创建数据库（如果不存在）...");
        _client.DbMaintenance.CreateDatabase();
        _logger?.LogInformation("数据库创建完成");

        // 自动搜索所有实体类型
        var entityTypes = GetAllEntityTypes();
        _logger?.LogInformation("发现 {EntityCount} 个实体类型，开始初始化表结构...", entityTypes.Length);

        // 根据业务领域过滤实体类型
        var filteredEntityTypes = entityTypes;
        if (!string.IsNullOrEmpty(targetConfigId))
        {
            // 先记录所有实体的映射情况（用于调试）
            var entityMappingInfo = entityTypes
                .Select(et =>
                {
                    var entityNamespace = et.Namespace ?? string.Empty;
                    var mappedConfigId = TaktEntityDatabaseMapping.GetConfigIdByEntityNamespace(entityNamespace);
                    // 调试：检查工作流、生成器、人力资源实体（仅在开发环境或需要详细调试时启用）
                    // 注意：实际匹配逻辑使用 ".workflow"（没有末尾点），因为命名空间末尾没有点
                    // 此调试日志已禁用，因为映射逻辑已正确工作
                    // if (_logger != null && (entityNamespace.Contains("Workflow", StringComparison.OrdinalIgnoreCase) || 
                    //                         entityNamespace.Contains("Generator", StringComparison.OrdinalIgnoreCase) ||
                    //                         entityNamespace.Contains("HumanResource", StringComparison.OrdinalIgnoreCase)))
                    // {
                    //     var nsLower = entityNamespace.ToLowerInvariant();
                    //     var hasWorkflow = nsLower.Contains(".workflow");
                    //     var hasGenerator = nsLower.Contains(".generator");
                    //     var hasHumanResource = nsLower.Contains(".humanresource");
                    //     _logger.LogWarning("实体映射调试: {EntityName}, 命名空间=[{Namespace}], 小写=[{NsLower}], 包含.workflow={HasWorkflow}, 包含.generator={HasGenerator}, 包含.humanresource={HasHumanResource}, 映射到ConfigId={MappedConfigId}", 
                    //         et.Name, entityNamespace, nsLower, hasWorkflow, hasGenerator, hasHumanResource, mappedConfigId);
                    // }
                    return new { Entity = et, Namespace = entityNamespace, MappedConfigId = mappedConfigId };
                })
                .ToList();

            // 按ConfigId分组统计
            var groupedByConfigId = entityMappingInfo
                .GroupBy(x => x.MappedConfigId)
                .Select(g => new { ConfigId = g.Key, Count = g.Count(), Entities = g.Select(x => x.Entity.Name).ToList() })
                .ToList();

            _logger?.LogInformation("实体映射统计: 总计 {TotalCount} 个实体", entityTypes.Length);
            foreach (var group in groupedByConfigId.OrderBy(g => g.ConfigId))
            {
                _logger?.LogInformation("  ConfigId={ConfigId}: {Count} 个实体", group.ConfigId, group.Count);
                if (group.ConfigId == targetConfigId && group.Count > 0)
                {
                    _logger?.LogInformation("    匹配的实体: {Entities}", string.Join(", ", group.Entities.Take(10)));
                }
            }

            filteredEntityTypes = entityTypes
                .Where(et =>
                {
                    var entityNamespace = et.Namespace ?? string.Empty;
                    var mappedConfigId = TaktEntityDatabaseMapping.GetConfigIdByEntityNamespace(entityNamespace);
                    return mappedConfigId == targetConfigId;
                })
                .ToArray();
            
            _logger?.LogInformation("根据业务领域过滤后，ConfigId={ConfigId} 需要初始化 {EntityCount} 个实体类型", 
                targetConfigId, filteredEntityTypes.Length);
        }

        // 创建所有表（SqlSugar 自动处理依赖）
        // 三者逻辑：创建 = 表初始前不存在且 InitTables 未抛异常；更新 = 表初始前已存在且 InitTables 后仍可见；失败 = InitTables 抛异常 或 表曾存在但 InitTables 后不可见
        int createdCount = 0;
        int updatedCount = 0;
        int failedCount = 0;
        foreach (var entityType in filteredEntityTypes)
        {
            try
            {
                var tableName = _client.EntityMaintenance.GetTableName(entityType);
                var existsBefore = _client.DbMaintenance.IsAnyTable(tableName);

                bool initThrew = false;
                try
                {
                    _client.CodeFirst.InitTables(entityType);
                }
                catch (Exception exInner)
                {
                    initThrew = true;
                    _logger?.LogError(exInner, "建表失败: {TableName} ({EntityType})", tableName, entityType.Name);
                    failedCount++;
                }

                if (initThrew)
                    continue;

                var existsAfter = _client.DbMaintenance.IsAnyTable(tableName);
                if (existsBefore)
                {
                    if (existsAfter)
                    {
                        _logger?.LogInformation("表已存在，已更新: {TableName} ({EntityType})", tableName, entityType.Name);
                        updatedCount++;
                    }
                    else
                    {
                        _logger?.LogWarning("建表失败: {TableName} ({EntityType})（表曾存在，初始化后不可见）", tableName, entityType.Name);
                        failedCount++;
                    }
                }
                else
                {
                    _logger?.LogInformation("表创建成功: {TableName} ({EntityType})", tableName, entityType.Name);
                    createdCount++;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "表初始化失败: {EntityType}", entityType.Name);
                failedCount++;
                throw;
            }
        }

        _logger?.LogInformation("表结构初始化完成，创建: {CreatedCount}, 更新: {UpdatedCount}, 失败: {FailedCount}, 总计: {TotalCount}",
            createdCount, updatedCount, failedCount, filteredEntityTypes.Length);
        
        return Task.CompletedTask;
    }
}
