// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktCompanySeedData.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：公司种子数据，初始化默认公司（参照 TaktUserSeedData）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Accounting.Financial;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 公司种子数据（实体种子：插入 TaktCompany 业务记录）
/// </summary>
public class TaktCompanySeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序
    /// </summary>
    public int Order => 53;

    /// <summary>
    /// 初始化公司种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var companyRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktCompany>>();
        int insertCount = 0;
        int updateCount = 0;

        // 公司代码统一为 C100、C200…（4 位），种子数据与业务校验一致
        var companies = new[]
        {
            new { CompanyCode = "C100", CompanyName = "节拍数字工厂", ShortName = "TDF", Region = "中国", Province = "广东省", City = "深圳市", LocalCurrency = "CNY", ChartOfAccounts = "takt", CompanyStatus = 0, OrderNum = 0, SocialAccounts = "[{\"type\":\"wechat\",\"account\":\"\"},{\"type\":\"weibo\",\"account\":\"\"}]" }
        };

        foreach (var company in companies)
        {
            var code = company.CompanyCode.Trim();
            if (code.Length != 4)
                throw new InvalidOperationException($"种子数据公司代码必须为4位，当前为：{company.CompanyCode}（长度 {code.Length}）");
            var existing = await companyRepository.GetAsync(c => c.CompanyCode == code && c.IsDeleted == 0);
            if (existing == null)
            {
                await companyRepository.CreateAsync(new TaktCompany
                {
                    CompanyCode = code,
                    CompanyName = company.CompanyName,
                    ShortName = company.ShortName,
                    Region = company.Region,
                    Province = company.Province,
                    City = company.City,
                    LocalCurrency = company.LocalCurrency,
                    ChartOfAccounts = company.ChartOfAccounts,
                    CompanyStatus = company.CompanyStatus,
                    OrderNum = company.OrderNum,
                    SocialAccounts = company.SocialAccounts,
                    ConfigId = configId
                });
                insertCount++;
            }
            else
            {
                existing.CompanyName = company.CompanyName;
                existing.ShortName = company.ShortName;
                existing.Region = company.Region;
                existing.Province = company.Province;
                existing.City = company.City;
                existing.LocalCurrency = company.LocalCurrency;
                existing.ChartOfAccounts = company.ChartOfAccounts;
                existing.CompanyStatus = company.CompanyStatus;
                existing.OrderNum = company.OrderNum;
                existing.SocialAccounts = company.SocialAccounts;
                await companyRepository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
