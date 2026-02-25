// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktNumberingRuleSeedData.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：单据编码规则种子数据，初始化公告编码、通知编码、文件编码规则（ConfigId=4 Routine 库）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.NumberingRules;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 单据编码规则种子数据（公告、通知、文件）
/// </summary>
public class TaktNumberingRuleSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（在字典类型之后、实体翻译之前，便于规则编码可被引用）
    /// </summary>
    public int Order => 98;

    /// <summary>
    /// 初始化编码规则种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID（应为 4=Routine）</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var repository = serviceProvider.GetRequiredService<ITaktRepository<TaktNumberingRule>>();

        int insertCount = 0;
        int updateCount = 0;

        // 格式示例：DTA-A-20260200001（前缀+年月+5位流水号，按月重置）
        var rules = new[]
        {
            new
            {
                RuleCode = "Announcement",
                RuleName = "公告编码",
                DocumentType = "公告",
                CompanyCode = (string?)null,
                DeptCode = (string?)null,
                Prefix = "DTA-A-",
                DateFormat = "yyyyMM",
                SerialLength = 5,
                Suffix = (string?)null,
                ResetCycle = 2,
                OrderNum = 10
            },
            new
            {
                RuleCode = "Notification",
                RuleName = "通知编码",
                DocumentType = "通知",
                CompanyCode = (string?)null,
                DeptCode = (string?)null,
                Prefix = "DTA-N-",
                DateFormat = "yyyyMM",
                SerialLength = 5,
                Suffix = (string?)null,
                ResetCycle = 2,
                OrderNum = 20
            },
            new
            {
                RuleCode = "File",
                RuleName = "文件编码",
                DocumentType = "文件",
                CompanyCode = (string?)null,
                DeptCode = (string?)null,
                Prefix = "DTA-F-",
                DateFormat = "yyyyMM",
                SerialLength = 5,
                Suffix = (string?)null,
                ResetCycle = 2,
                OrderNum = 30
            }
        };

        foreach (var r in rules)
        {
            var existing = await repository.GetAsync(x => x.RuleCode == r.RuleCode && x.IsDeleted == 0);
            if (existing == null)
            {
                var entity = new TaktNumberingRule
                {
                    RuleCode = r.RuleCode,
                    RuleName = r.RuleName,
                    DocumentType = r.DocumentType,
                    CompanyCode = r.CompanyCode,
                    DeptCode = r.DeptCode,
                    Prefix = r.Prefix,
                    DateFormat = r.DateFormat,
                    SerialLength = r.SerialLength,
                    Suffix = r.Suffix,
                    CurrentValue = 0,
                    ResetCycle = r.ResetCycle,
                    OrderNum = r.OrderNum,
                    RuleStatus = 0,
                    ConfigId = configId,
                    IsDeleted = 0
                };
                await repository.CreateAsync(entity);
                insertCount++;
            }
            else
            {
                existing.RuleName = r.RuleName;
                existing.DocumentType = r.DocumentType;
                existing.CompanyCode = r.CompanyCode;
                existing.DeptCode = r.DeptCode;
                existing.Prefix = r.Prefix;
                existing.DateFormat = r.DateFormat;
                existing.SerialLength = r.SerialLength;
                existing.Suffix = r.Suffix;
                existing.ResetCycle = r.ResetCycle;
                existing.OrderNum = r.OrderNum;
                await repository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
