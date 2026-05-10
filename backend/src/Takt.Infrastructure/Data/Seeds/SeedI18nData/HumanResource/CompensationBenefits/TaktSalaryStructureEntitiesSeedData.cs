// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktSalaryStructureEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktSalaryStructure 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.HumanResource.CompensationBenefits;

/// <summary>
/// TaktSalaryStructure 实体翻译种子数据（自动生成，与 TaktSalaryStructure.cs 属性一一对应）
/// </summary>
public class TaktSalaryStructureEntitiesSeedData : ITaktSeedData
{
    public int Order => 999;

    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();

        int insertCount = 0;
        int updateCount = 0;
        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 1 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllTaktSalaryStructureEntityTranslations();

            foreach (var translation in allTranslations)
            {
                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId))
                    continue;

                var existing = await translationRepository.GetAsync(t =>
                    t.ResourceKey == translation.ResourceKey &&
                    t.CultureCode == translation.CultureCode &&
                    t.IsDeleted == 0);

                if (existing == null)
                {
                    await translationRepository.CreateAsync(new TaktTranslation
                    {
                        LanguageId = languageId,
                        CultureCode = translation.CultureCode,
                        ResourceKey = translation.ResourceKey,
                        TranslationValue = translation.TranslationValue,
                        ResourceType = translation.ResourceType,
                        ResourceGroup = translation.ResourceGroup,
                        SortOrder = translation.SortOrder,
                        IsDeleted = 0
                    });
                    insertCount++;
                }
                else if (existing.TranslationValue != translation.TranslationValue)
                {
                    existing.LanguageId = languageId;
                    existing.TranslationValue = translation.TranslationValue;
                    existing.ResourceType = translation.ResourceType;
                    existing.ResourceGroup = translation.ResourceGroup;
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
        finally
        {
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取所有 TaktSalaryStructure 实体名称及字段翻译（自动生成，与 TaktSalaryStructure.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktSalaryStructureEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.salarystructure（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure._self", TranslationValue = "薪资结构", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure._self", TranslationValue = "薪资结构", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure._self", TranslationValue = "薪资结构", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure._self", TranslationValue = "薪资结构", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure._self", TranslationValue = "薪资结构", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure._self", TranslationValue = "薪资结构", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.salarystructure.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.code", TranslationValue = "薪资结构编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.code", TranslationValue = "薪资结构编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.code", TranslationValue = "薪资结构编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.code", TranslationValue = "薪资结构编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.code", TranslationValue = "薪资结构编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.code", TranslationValue = "薪资结构编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.salarystructure.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.name", TranslationValue = "薪资结构名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.salarystructure.salarylevel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.salarylevel", TranslationValue = "薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.salarylevel", TranslationValue = "薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.salarylevel", TranslationValue = "薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.salarylevel", TranslationValue = "薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.salarylevel", TranslationValue = "薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.salarylevel", TranslationValue = "薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.salarystructure.salarygrade
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.salarygrade", TranslationValue = "薪资档次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.salarygrade", TranslationValue = "薪资档次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.salarygrade", TranslationValue = "薪资档次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.salarygrade", TranslationValue = "薪资档次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.salarygrade", TranslationValue = "薪资档次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.salarygrade", TranslationValue = "薪资档次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.salarystructure.minsalary
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.minsalary", TranslationValue = "最低薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.minsalary", TranslationValue = "最低薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.minsalary", TranslationValue = "最低薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.minsalary", TranslationValue = "最低薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.minsalary", TranslationValue = "最低薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.minsalary", TranslationValue = "最低薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.salarystructure.midsalary
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.midsalary", TranslationValue = "中位薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.midsalary", TranslationValue = "中位薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.midsalary", TranslationValue = "中位薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.midsalary", TranslationValue = "中位薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.midsalary", TranslationValue = "中位薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.midsalary", TranslationValue = "中位薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.salarystructure.maxsalary
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.maxsalary", TranslationValue = "最高薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.maxsalary", TranslationValue = "最高薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.maxsalary", TranslationValue = "最高薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.maxsalary", TranslationValue = "最高薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.maxsalary", TranslationValue = "最高薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.maxsalary", TranslationValue = "最高薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.salarystructure.applicabledepartment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.salarystructure.applicableposition
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.salarystructure.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.description", TranslationValue = "说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.description", TranslationValue = "说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.description", TranslationValue = "说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.description", TranslationValue = "说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.description", TranslationValue = "说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.description", TranslationValue = "说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.salarystructure.effectivedate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.effectivedate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.effectivedate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.effectivedate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.effectivedate", TranslationValue = "生效日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.salarystructure.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salarystructure.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salarystructure.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salarystructure.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salarystructure.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salarystructure.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salarystructure.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
        };
    }
}
