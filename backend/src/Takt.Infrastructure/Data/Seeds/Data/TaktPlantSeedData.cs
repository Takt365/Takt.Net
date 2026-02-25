// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPlantSeedData.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：工厂种子数据，初始化默认工厂（参照 TaktUserSeedData）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Logistics.Materials;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 工厂种子数据（实体种子：插入 TaktPlant 业务记录）
/// </summary>
public class TaktPlantSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序
    /// </summary>
    public int Order => 54;

    /// <summary>
    /// 初始化工厂种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var plantRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktPlant>>();
        int insertCount = 0;
        int updateCount = 0;

        // 工厂代码统一为 P100、P200…（4 位），种子数据与业务校验一致
        var plants = new[]
        {
            new { PlantCode = "P100", PlantName = "节拍数字工厂-主工厂", ShortName = "主工厂", Region = "中国", Province = "广东省", City = "深圳市", LocalCurrency = "CNY", ChartOfAccounts = "takt", PlantStatus = 0, OrderNum = 0, SocialAccounts = "[{\"type\":\"wechat\",\"account\":\"\"},{\"type\":\"weibo\",\"account\":\"\"}]" }
        };

        foreach (var plant in plants)
        {
            var code = plant.PlantCode.Trim();
            if (code.Length != 4)
                throw new InvalidOperationException($"种子数据工厂代码必须为4位，当前为：{plant.PlantCode}（长度 {code.Length}）");
            var existing = await plantRepository.GetAsync(p => p.PlantCode == code && p.IsDeleted == 0);
            if (existing == null)
            {
                await plantRepository.CreateAsync(new TaktPlant
                {
                    PlantCode = code,
                    PlantName = plant.PlantName,
                    ShortName = plant.ShortName,
                    Region = plant.Region,
                    Province = plant.Province,
                    City = plant.City,
                    LocalCurrency = plant.LocalCurrency,
                    ChartOfAccounts = plant.ChartOfAccounts,
                    PlantStatus = plant.PlantStatus,
                    OrderNum = plant.OrderNum,
                    SocialAccounts = plant.SocialAccounts,
                    ConfigId = configId
                });
                insertCount++;
            }
            else
            {
                existing.PlantName = plant.PlantName;
                existing.ShortName = plant.ShortName;
                existing.Region = plant.Region;
                existing.Province = plant.Province;
                existing.City = plant.City;
                existing.LocalCurrency = plant.LocalCurrency;
                existing.ChartOfAccounts = plant.ChartOfAccounts;
                existing.PlantStatus = plant.PlantStatus;
                existing.OrderNum = plant.OrderNum;
                existing.SocialAccounts = plant.SocialAccounts;
                await plantRepository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
