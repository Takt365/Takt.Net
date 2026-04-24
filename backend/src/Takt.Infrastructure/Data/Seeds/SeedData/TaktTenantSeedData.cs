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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

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

        // 定义租户数据：只初始化系统管理租户（tenant_0），允许访问所有数据库
        var tenants = new[]
        {
            new { TenantCode = "tenant_0", TenantName = "系统管理", AllowedConfigIds = "[1,2,3,4,5]", SubscriptionStartTime = DateTime.Now, SubscriptionEndTime = new DateTime(9999, 12, 31), TenantStatus = 1, IsDeleted = 0 }
        };

        // 只初始化系统管理租户
        var tenant = tenants[0];

        var existing = await tenantRepository.GetAsync(t => t.TenantCode == tenant.TenantCode);
        
        if (existing == null)
        {
            // 不存在则插入
            var newTenant = new TaktTenant
            {
                TenantCode = tenant.TenantCode,
                TenantName = tenant.TenantName,
                AllowedConfigIds = tenant.AllowedConfigIds,
                SubscriptionStartTime = tenant.SubscriptionStartTime,
                SubscriptionEndTime = tenant.SubscriptionEndTime,
                TenantStatus = tenant.TenantStatus,
                IsDeleted = tenant.IsDeleted
            };
            await tenantRepository.CreateAsync(newTenant);
            insertCount++;
        }
        else
        {
            // 存在则更新
            existing.TenantCode = tenant.TenantCode;
            existing.TenantName = tenant.TenantName;
            existing.AllowedConfigIds = tenant.AllowedConfigIds;
            existing.SubscriptionStartTime = tenant.SubscriptionStartTime;
            existing.SubscriptionEndTime = tenant.SubscriptionEndTime;
            existing.TenantStatus = tenant.TenantStatus;
            await tenantRepository.UpdateAsync(existing);
            updateCount++;
        }

        return (insertCount, updateCount);
    }
}
