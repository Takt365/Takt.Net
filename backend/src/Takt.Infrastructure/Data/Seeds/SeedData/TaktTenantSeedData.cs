// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktTenantSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt租户种子数据，初始化默认租户数据
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt租户种子数据
/// </summary>
public class TaktTenantSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（租户应该最先初始化，因为其他数据可能依赖租户）
    /// </summary>
    public int Order => 1;

    /// <summary>
    /// 初始化租户种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var tenantRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTenant>>();

        int insertCount = 0;
        int updateCount = 0;
        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            // 租户数据存储在Identity库（ConfigId="0"）
            TaktTenantContext.CurrentConfigId = "0";

            // 初始化演示租户数据
            var demoTenants = GetDemoTenants();
            foreach (var demoTenant in demoTenants)
            {
                var (i, u) = await CreateOrUpdateTenantFromEntityAsync(tenantRepository, demoTenant);
                insertCount += i; updateCount += u;
            }
        }
        finally
        {
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 从实体对象创建或更新租户
    /// </summary>
    private static async Task<(int InsertCount, int UpdateCount)> CreateOrUpdateTenantFromEntityAsync(
        ITaktRepository<TaktTenant> tenantRepository,
        TaktTenant demoTenant)
    {
        var existing = await tenantRepository.GetAsync(t => t.TenantCode == demoTenant.TenantCode && t.IsDeleted == 0);
        if (existing == null)
        {
            await tenantRepository.CreateAsync(demoTenant);
            TaktLogger.Information("创建租户: {TenantCode} - {TenantName}, 允许访问库: {AllowedConfigIds}",
                demoTenant.TenantCode, demoTenant.TenantName, demoTenant.AllowedConfigIds);
            return (1, 0);
        }
        else if (existing.AllowedConfigIds != demoTenant.AllowedConfigIds)
        {
            existing.AllowedConfigIds = demoTenant.AllowedConfigIds;
            existing.UpdatedAt = DateTime.UtcNow;
            await tenantRepository.UpdateAsync(existing);
            TaktLogger.Information("更新租户: {TenantCode} - {TenantName}, 新允许访问库: {AllowedConfigIds}",
                demoTenant.TenantCode, demoTenant.TenantName, demoTenant.AllowedConfigIds);
            return (0, 1);
        }
        return (0, 0);
    }

    /// <summary>
    /// 获取演示租户数据
    /// 注意：主库（ConfigId="0,1,2"）始终可访问（Identity、HR、日常库），不在AllowedConfigIds中
    /// AllowedConfigIds 仅控制租户可以访问哪些业务库（3-5：生成器、财务、物流）
    /// </summary>
    private static List<TaktTenant> GetDemoTenants() => new()
    {
        // 租户develop（开发）：可以访问所有业务库 3, 4, 5（生成器、财务、物流）
        // 主库（ConfigId="0,1,2"）始终可访问（Identity、HR、日常）
        new TaktTenant
        {
            Id = 0,
            TenantCode = "develop",
            TenantName = "开发租户（Develop）",
            AllowedConfigIds = "[\"3\",\"4\",\"5\"]",
            ConfigId = "0",  // 租户实体存储在Identity库
            SubscriptionStartTime = DateTime.Now,
            SubscriptionEndTime = new DateTime(9999, 12, 31),
            TenantStatus = 1,  // 1=启用
            CreatedAt = DateTime.UtcNow,
            IsDeleted = 0
        },

        // 租户DTA：可以访问业务库 3, 5（生成器、物流），不能访问财务库（4）
        // 主库（ConfigId="0,1,2"）始终可访问（Identity、HR、日常）
        new TaktTenant
        {
            Id = 0,
            TenantCode = "DTA",
            TenantName = "DTA租户",
            AllowedConfigIds = "[\"3\",\"5\"]",
            ConfigId = "0",  // 租户实体存储在Identity库
            SubscriptionStartTime = DateTime.Now,
            SubscriptionEndTime = new DateTime(9999, 12, 31),
            TenantStatus = 1,  // 1=启用
            CreatedAt = DateTime.UtcNow,
            IsDeleted = 0
        },

        // 租户TCJ：可以访问业务库 5（物流），不能访问生成器（3）和财务（4）
        // 主库（ConfigId="0,1,2"）始终可访问（Identity、HR、日常）
        new TaktTenant
        {
            Id = 0,
            TenantCode = "TCJ",
            TenantName = "TCJ租户",
            AllowedConfigIds = "[\"5\"]",
            ConfigId = "0",  // 租户实体存储在Identity库
            SubscriptionStartTime = DateTime.Now,
            SubscriptionEndTime = new DateTime(9999, 12, 31),
            TenantStatus = 1,  // 1=启用
            CreatedAt = DateTime.UtcNow,
            IsDeleted = 0
        }
    };
}
