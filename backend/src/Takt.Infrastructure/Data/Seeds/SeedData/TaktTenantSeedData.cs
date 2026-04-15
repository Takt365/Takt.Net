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

        // 定义租户数据：ConfigId 与库名一一对应 0=Identity, 1=HumanResource, 2=Routine, 3=Building, 4=Accounting, 5=Logistics
        var tenants = new[]
        {
            new { ConfigId = "0", TenantCode = "tenant_0", TenantName = "Identity分库", StartTime = DateTime.Now, EndTime = new DateTime(9999, 12, 31), TenantStatus = 1, IsDeleted = 0 },
            new { ConfigId = "1", TenantCode = "tenant_1", TenantName = "HumanResource分库", StartTime = DateTime.Now, EndTime = new DateTime(9999, 12, 31), TenantStatus = 1, IsDeleted = 0 },
            new { ConfigId = "2", TenantCode = "tenant_2", TenantName = "Routine分库", StartTime = DateTime.Now, EndTime = new DateTime(9999, 12, 31), TenantStatus = 1, IsDeleted = 0 },
            new { ConfigId = "3", TenantCode = "tenant_3", TenantName = "Building分库", StartTime = DateTime.Now, EndTime = new DateTime(9999, 12, 31), TenantStatus = 1, IsDeleted = 0 },
            new { ConfigId = "4", TenantCode = "tenant_4", TenantName = "Accounting分库", StartTime = DateTime.Now, EndTime = new DateTime(9999, 12, 31), TenantStatus = 1, IsDeleted = 0 },
            new { ConfigId = "5", TenantCode = "tenant_5", TenantName = "Logistics分库", StartTime = DateTime.Now, EndTime = new DateTime(9999, 12, 31), TenantStatus = 1, IsDeleted = 0 }
        };

        // 根据 ConfigId 直接通过索引访问（ConfigId 与数组索引一致）
        if (!int.TryParse(configId, out var configIdInt) || configIdInt < 0 || configIdInt >= tenants.Length)
        {
            return (0, 0);
        }

        var tenant = tenants[configIdInt];

        var existing = await tenantRepository.GetAsync(t => t.ConfigId == tenant.ConfigId);
        
        if (existing == null)
        {
            // 不存在则插入
            var newTenant = new TaktTenant
            {
                ConfigId = tenant.ConfigId,
                TenantCode = tenant.TenantCode,
                TenantName = tenant.TenantName,
                StartTime = tenant.StartTime,
                EndTime = tenant.EndTime,
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
            existing.StartTime = tenant.StartTime;
            existing.EndTime = tenant.EndTime;
            existing.TenantStatus = tenant.TenantStatus;
            await tenantRepository.UpdateAsync(existing);
            updateCount++;
        }

        return (insertCount, updateCount);
    }
}
