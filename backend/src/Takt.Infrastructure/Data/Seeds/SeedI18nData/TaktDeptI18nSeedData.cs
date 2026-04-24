// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktDeptI18nSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt部门本地化种子数据，为 TaktDeptSeedData 中的部门编码创建翻译数据（ResourceKey = dept.{deptcode}，翻译键全部小写）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData;

/// <summary>
/// Takt部门本地化种子数据
/// </summary>
public class TaktDeptI18nSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（部门本地化应在部门、语言之后初始化）
    /// </summary>
    public int Order => 8;

    /// <summary>
    /// 初始化部门本地化种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();

        int insertCount = 0;
        int updateCount = 0;

        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            var languages = await languageRepository.FindAsync(l =>
                l.LanguageStatus == 0 &&
                l.IsDeleted == 0);

            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllDeptTranslations();

            foreach (var t in allTranslations)
            {
                if (!cultureCodeToLanguageId.TryGetValue(t.CultureCode, out var languageId))
                    continue;

                var existing = await translationRepository.GetAsync(x =>
                    x.ResourceKey == t.ResourceKey &&
                    x.CultureCode == t.CultureCode &&
                    x.IsDeleted == 0);

                if (existing == null)
                {
                    await translationRepository.CreateAsync(new TaktTranslation
                    {
                        LanguageId = languageId,
                        CultureCode = t.CultureCode,
                        ResourceKey = t.ResourceKey,
                        TranslationValue = t.TranslationValue,
                        ResourceType = t.ResourceType,
                        ResourceGroup = t.ResourceGroup,
                        SortOrder = t.SortOrder,
                        IsDeleted = 0
                    });
                    insertCount++;
                }
                else
                {
                    if (existing.TranslationValue != t.TranslationValue ||
                        existing.ResourceType != t.ResourceType ||
                        existing.ResourceGroup != t.ResourceGroup)
                    {
                        existing.LanguageId = languageId;
                        existing.TranslationValue = t.TranslationValue;
                        existing.ResourceType = t.ResourceType;
                        existing.ResourceGroup = t.ResourceGroup;
                        await translationRepository.UpdateAsync(existing);
                        updateCount++;
                    }
                    else if (existing.LanguageId != languageId)
                    {
                        existing.LanguageId = languageId;
                        await translationRepository.UpdateAsync(existing);
                        updateCount++;
                    }
                }
            }
        }
        finally
        {
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取所有部门翻译数据（与 TaktDeptSeedData 的 DeptCode 一一对应，6 种语言）
    /// </summary>
    private static List<TaktTranslation> GetAllDeptTranslations()
    {
        return new List<TaktTranslation>
        {
            // dept.head_office
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.head_office", TranslationValue = "TEAC", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.head_office", TranslationValue = "TEAC", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.head_office", TranslationValue = "TEAC", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.head_office", TranslationValue = "TEAC", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.head_office", TranslationValue = "TEAC", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.head_office", TranslationValue = "TEAC", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0000
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0000", TranslationValue = "DTA", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0000", TranslationValue = "DTA", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0000", TranslationValue = "DTA", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0000", TranslationValue = "DTA", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0000", TranslationValue = "DTA", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0000", TranslationValue = "DTA", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d1000
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d1000", TranslationValue = "General Manager's Room", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d1000", TranslationValue = "総経理室", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d1000", TranslationValue = "총경리실", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d1000", TranslationValue = "总经理室", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d1000", TranslationValue = "總經理室", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d1000", TranslationValue = "總經理室", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0100
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0100", TranslationValue = "General Affairs Dept", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0100", TranslationValue = "総務部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0100", TranslationValue = "총무부", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0100", TranslationValue = "总务部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0100", TranslationValue = "總務部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0100", TranslationValue = "總務部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0110
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0110", TranslationValue = "General Affairs Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0110", TranslationValue = "総務課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0110", TranslationValue = "총무과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0110", TranslationValue = "总务课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0110", TranslationValue = "總務課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0110", TranslationValue = "總務課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0200
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0200", TranslationValue = "Finance Dept", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0200", TranslationValue = "財務部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0200", TranslationValue = "재무부", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0200", TranslationValue = "财务部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0200", TranslationValue = "財務部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0200", TranslationValue = "財務部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0210
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0210", TranslationValue = "Finance Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0210", TranslationValue = "財務課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0210", TranslationValue = "재무과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0210", TranslationValue = "财务课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0210", TranslationValue = "財務課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0210", TranslationValue = "財務課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0300
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0300", TranslationValue = "IT Dept", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0300", TranslationValue = "IT部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0300", TranslationValue = "IT부", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0300", TranslationValue = "IT部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0300", TranslationValue = "IT部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0300", TranslationValue = "IT部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0310
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0310", TranslationValue = "IT Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0310", TranslationValue = "コンピューター課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0310", TranslationValue = "컴퓨터과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0310", TranslationValue = "电脑课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0310", TranslationValue = "電腦課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0310", TranslationValue = "電腦課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0400
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0400", TranslationValue = "Management Dept", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0400", TranslationValue = "管理部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0400", TranslationValue = "관리부", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0400", TranslationValue = "管理部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0400", TranslationValue = "管理部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0400", TranslationValue = "管理部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0410
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0410", TranslationValue = "Customhouse Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0410", TranslationValue = "通関課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0410", TranslationValue = "통관과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0410", TranslationValue = "报关课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0410", TranslationValue = "報關課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0410", TranslationValue = "報關課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0420
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0420", TranslationValue = "Production Management Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0420", TranslationValue = "生管課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0420", TranslationValue = "생관과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0420", TranslationValue = "生管课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0420", TranslationValue = "生管課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0420", TranslationValue = "生管課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0430
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0430", TranslationValue = "Materials Control Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0430", TranslationValue = "部管課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0430", TranslationValue = "부관과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0430", TranslationValue = "部管课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0430", TranslationValue = "部管課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0430", TranslationValue = "部管課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0500
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0500", TranslationValue = "Materials Dept", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0500", TranslationValue = "資材部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0500", TranslationValue = "자재부", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0500", TranslationValue = "资材部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0500", TranslationValue = "資材部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0500", TranslationValue = "資材部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0510
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0510", TranslationValue = "Purchase Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0510", TranslationValue = "購買課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0510", TranslationValue = "구매과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0510", TranslationValue = "采购课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0510", TranslationValue = "採購課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0510", TranslationValue = "採購課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0600
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0600", TranslationValue = "Production Dept", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0600", TranslationValue = "生産部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0600", TranslationValue = "생산부", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0600", TranslationValue = "生产部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0600", TranslationValue = "生產部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0600", TranslationValue = "生產部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0610
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0610", TranslationValue = "Production Section 1", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0610", TranslationValue = "製造1課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0610", TranslationValue = "제조1과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0610", TranslationValue = "制造1课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0610", TranslationValue = "製造1課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0610", TranslationValue = "製造1課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0620
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0620", TranslationValue = "Production Section 2", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0620", TranslationValue = "製造2課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0620", TranslationValue = "제조2과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0620", TranslationValue = "制造2课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0620", TranslationValue = "製造2課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0620", TranslationValue = "製造2課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0621
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0621", TranslationValue = "SMT", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0621", TranslationValue = "SMT", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0621", TranslationValue = "SMT", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0621", TranslationValue = "SMT", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0621", TranslationValue = "SMT", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0621", TranslationValue = "SMT", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0622
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0622", TranslationValue = "Automatic Insertion", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0622", TranslationValue = "自挿入", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0622", TranslationValue = "자동삽입", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0622", TranslationValue = "自插", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0622", TranslationValue = "自插", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0622", TranslationValue = "自插", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0623
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0623", TranslationValue = "Revision", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0623", TranslationValue = "修正", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0623", TranslationValue = "수정", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0623", TranslationValue = "修正", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0623", TranslationValue = "修正", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0623", TranslationValue = "修正", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0624
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0624", TranslationValue = "Manual Insertion", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0624", TranslationValue = "手挿入", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0624", TranslationValue = "수동삽입", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0624", TranslationValue = "手插", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0624", TranslationValue = "手插", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0624", TranslationValue = "手插", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0625
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0625", TranslationValue = "Materials", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0625", TranslationValue = "物料", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0625", TranslationValue = "자재", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0625", TranslationValue = "物料", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0625", TranslationValue = "物料", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0625", TranslationValue = "物料", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0626
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0626", TranslationValue = "PS2 Indirect", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0626", TranslationValue = "製造2課-間接", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0626", TranslationValue = "제조2과-간접", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0626", TranslationValue = "制造2课-间接", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0626", TranslationValue = "製造2課-間接", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0626", TranslationValue = "製造2課-間接", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0630
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0630", TranslationValue = "Production Engineering Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0630", TranslationValue = "製造技術課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0630", TranslationValue = "제조기술과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0630", TranslationValue = "制造技术课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0630", TranslationValue = "製造技術課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0630", TranslationValue = "製造技術課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0700
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0700", TranslationValue = "Engineering Dept", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0700", TranslationValue = "技術部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0700", TranslationValue = "기술부", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0700", TranslationValue = "技术部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0700", TranslationValue = "技術部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0700", TranslationValue = "技術部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0710
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0710", TranslationValue = "Engineering Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0710", TranslationValue = "技術課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0710", TranslationValue = "기술과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0710", TranslationValue = "技术课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0710", TranslationValue = "技術課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0710", TranslationValue = "技術課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0800
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0800", TranslationValue = "Quality Assurance Dept", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0800", TranslationValue = "品質保証部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0800", TranslationValue = "품질보증부", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0800", TranslationValue = "品保部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0800", TranslationValue = "品保部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0800", TranslationValue = "品保部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0810
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0810", TranslationValue = "IQC Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0810", TranslationValue = "受検課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0810", TranslationValue = "수검과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0810", TranslationValue = "受检课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0810", TranslationValue = "受檢課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0810", TranslationValue = "受檢課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0820
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0820", TranslationValue = "QA Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0820", TranslationValue = "品質管理課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0820", TranslationValue = "품질관리과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0820", TranslationValue = "品管课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0820", TranslationValue = "品管課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0820", TranslationValue = "品管課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0900
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0900", TranslationValue = "OEM Dept", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0900", TranslationValue = "OEM部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0900", TranslationValue = "OEM부", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0900", TranslationValue = "OEM部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0900", TranslationValue = "OEM部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0900", TranslationValue = "OEM部", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0910
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0910", TranslationValue = "OEM QA Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0910", TranslationValue = "OEM QA課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0910", TranslationValue = "OEM QA과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0910", TranslationValue = "OEM QA课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0910", TranslationValue = "OEM QA課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0910", TranslationValue = "OEM QA課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },

            // dept.d0920
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "dept.d0920", TranslationValue = "OEM Management Section", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "dept.d0920", TranslationValue = "OEM管理課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "dept.d0920", TranslationValue = "OEM 관리과", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "dept.d0920", TranslationValue = "OEM管理课", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "dept.d0920", TranslationValue = "OEM管理課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "dept.d0920", TranslationValue = "OEM管理課", ResourceType = "Frontend", ResourceGroup = "Dept", SortOrder = 0 }
        };
    }
}
