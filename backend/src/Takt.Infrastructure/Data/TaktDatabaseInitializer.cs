// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data
// 文件名称：TaktDatabaseInitializer.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt数据库初始化器，负责协调表结构和种子数据初始化
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.Logging;
using Takt.Infrastructure.Data.Seeds;
using Takt.Infrastructure.Helpers;

namespace Takt.Infrastructure.Data;

/// <summary>
/// Takt数据库初始化器
/// </summary>
public class TaktDatabaseInitializer
{
    private readonly ISqlSugarClient _client;
    private readonly IEnumerable<ITaktSeedData> _seedDataProviders;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _configId;
    private readonly ILogger? _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="client">SqlSugar客户端</param>
    /// <param name="seedDataProviders">种子数据提供者集合</param>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <param name="logger">日志记录器（可选）</param>
    public TaktDatabaseInitializer(ISqlSugarClient client, IEnumerable<ITaktSeedData> seedDataProviders, IServiceProvider serviceProvider, string configId, ILogger? logger = null)
    {
        _client = client;
        _seedDataProviders = seedDataProviders;
        _serviceProvider = serviceProvider;
        _configId = configId;
        _logger = logger;
    }

    /// <summary>
    /// 初始化数据库（创建数据库和表）
    /// </summary>
    /// <returns>任务</returns>
    public Task InitializeTablesAsync()
    {
        var tableInitializer = new TaktTableInitializer(_client, _logger);
        // 传入当前 ConfigId，只初始化属于该数据库的实体表
        return tableInitializer.InitializeAsync(_configId);
    }

    /// <summary>
    /// 初始化种子数据（根据业务领域自动分配到对应数据库）
    /// </summary>
    /// <returns>返回总记录数统计（总插入数, 总更新数）</returns>
    public async Task<(int TotalInsertCount, int TotalUpdateCount)> SeedDataAsync()
    {
        // 按执行顺序排序种子数据提供者
        var orderedSeedDataProviders = _seedDataProviders
            .OrderBy(s => s.Order)
            .ToList();

        // 根据业务领域过滤种子数据提供者，只初始化属于当前数据库的种子数据（支持一种子多库，如 TaktRbacSeedData）
        var filteredSeedDataProviders = orderedSeedDataProviders
            .Where(s =>
            {
                var seedDataClassName = s.GetType().Name;
                var configIds = TaktEntityDatabaseMapping.GetConfigIdsForSeedDataClassName(seedDataClassName);
                return configIds.Contains(_configId);
            })
            .ToList();

        _logger?.LogInformation("根据业务领域过滤后，ConfigId={ConfigId} 需要初始化 {SeedDataCount} 个种子数据提供者", 
            _configId, filteredSeedDataProviders.Count);

        int totalInsertCount = 0;
        int totalUpdateCount = 0;

        // 设置种子数据操作标志，排除差异日志记录
        TaktLoggingExclusions.SetSeedingData(true);
        try
        {
            // 依次执行种子数据初始化
            foreach (var seedDataProvider in filteredSeedDataProviders)
            {
                try
                {
                    var (insertCount, updateCount) = await seedDataProvider.SeedAsync(_serviceProvider, _configId);
                    totalInsertCount += insertCount;
                    totalUpdateCount += updateCount;

                    var seedDataName = seedDataProvider.GetType().Name;
                    _logger?.LogInformation("种子数据初始化完成: {SeedDataName}, 插入: {InsertCount}, 更新: {UpdateCount}", 
                        seedDataName, insertCount, updateCount);
                }
                catch (Exception ex)
                {
                    var seedDataName = seedDataProvider.GetType().Name;
                    _logger?.LogError(ex, "种子数据初始化失败: {SeedDataName}", seedDataName);
                    throw;
                }
            }
        }
        finally
        {
            // 恢复标志，确保即使发生异常也能正确恢复
            TaktLoggingExclusions.SetSeedingData(false);
        }

        _logger?.LogInformation("种子数据初始化完成 ConfigId={ConfigId}，总计 - 插入: {TotalInsertCount}, 更新: {TotalUpdateCount}", 
            _configId, totalInsertCount, totalUpdateCount);

        return (totalInsertCount, totalUpdateCount);
    }
}