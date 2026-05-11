// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktTaxRuleEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktTaxRule 实体字段翻译种子数据（自动生成）
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
/// TaktTaxRule 实体翻译种子数据（自动生成，与 TaktTaxRule.cs 属性一一对应）
/// </summary>
public class TaktTaxRuleEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktTaxRuleEntityTranslations();

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
    /// 获取所有 TaktTaxRule 实体名称及字段翻译（自动生成，与 TaktTaxRule.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktTaxRuleEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.taxrule（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule._self", TranslationValue = "税务规则", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule._self", TranslationValue = "税务规则", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule._self", TranslationValue = "税务规则", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule._self", TranslationValue = "税务规则", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule._self", TranslationValue = "税务规则", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule._self", TranslationValue = "税务规则", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.taxrule.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.code", TranslationValue = "规则编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.code", TranslationValue = "规则编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.code", TranslationValue = "规则编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.code", TranslationValue = "规则编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.code", TranslationValue = "规则编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.code", TranslationValue = "规则编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.taxrule.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.name", TranslationValue = "规则名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.taxrule.taxyear
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.taxyear", TranslationValue = "税务年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.taxyear", TranslationValue = "税务年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.taxyear", TranslationValue = "税务年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.taxyear", TranslationValue = "税务年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.taxyear", TranslationValue = "税务年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.taxyear", TranslationValue = "税务年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.taxrule.taxthreshold
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.taxthreshold", TranslationValue = "税收起征点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.taxthreshold", TranslationValue = "税收起征点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.taxthreshold", TranslationValue = "税收起征点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.taxthreshold", TranslationValue = "税收起征点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.taxthreshold", TranslationValue = "税收起征点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.taxthreshold", TranslationValue = "税收起征点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.taxrule.taxableincomemin
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.taxableincomemin", TranslationValue = "应纳税所得额下限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.taxableincomemin", TranslationValue = "应纳税所得额下限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.taxableincomemin", TranslationValue = "应纳税所得额下限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.taxableincomemin", TranslationValue = "应纳税所得额下限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.taxableincomemin", TranslationValue = "应纳税所得额下限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.taxableincomemin", TranslationValue = "应纳税所得额下限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.taxrule.taxableincomemax
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.taxableincomemax", TranslationValue = "应纳税所得额上限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.taxableincomemax", TranslationValue = "应纳税所得额上限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.taxableincomemax", TranslationValue = "应纳税所得额上限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.taxableincomemax", TranslationValue = "应纳税所得额上限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.taxableincomemax", TranslationValue = "应纳税所得额上限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.taxableincomemax", TranslationValue = "应纳税所得额上限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.taxrule.taxrate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.taxrate", TranslationValue = "税率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.taxrate", TranslationValue = "税率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.taxrate", TranslationValue = "税率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.taxrate", TranslationValue = "税率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.taxrate", TranslationValue = "税率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.taxrate", TranslationValue = "税率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.taxrule.quickdeduction
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.quickdeduction", TranslationValue = "速算扣除数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.quickdeduction", TranslationValue = "速算扣除数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.quickdeduction", TranslationValue = "速算扣除数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.quickdeduction", TranslationValue = "速算扣除数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.quickdeduction", TranslationValue = "速算扣除数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.quickdeduction", TranslationValue = "速算扣除数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.taxrule.specialdeductionstandard
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.specialdeductionstandard", TranslationValue = "专项扣除标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.specialdeductionstandard", TranslationValue = "专项扣除标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.specialdeductionstandard", TranslationValue = "专项扣除标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.specialdeductionstandard", TranslationValue = "专项扣除标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.specialdeductionstandard", TranslationValue = "专项扣除标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.specialdeductionstandard", TranslationValue = "专项扣除标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.taxrule.socialsecuritydeductionrate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.socialsecuritydeductionrate", TranslationValue = "社保扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.socialsecuritydeductionrate", TranslationValue = "社保扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.socialsecuritydeductionrate", TranslationValue = "社保扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.socialsecuritydeductionrate", TranslationValue = "社保扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.socialsecuritydeductionrate", TranslationValue = "社保扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.socialsecuritydeductionrate", TranslationValue = "社保扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.taxrule.housingfunddeductionrate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.housingfunddeductionrate", TranslationValue = "公积金扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.housingfunddeductionrate", TranslationValue = "公积金扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.housingfunddeductionrate", TranslationValue = "公积金扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.housingfunddeductionrate", TranslationValue = "公积金扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.housingfunddeductionrate", TranslationValue = "公积金扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.housingfunddeductionrate", TranslationValue = "公积金扣除比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.taxrule.calculationformula
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.calculationformula", TranslationValue = "计算公式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.calculationformula", TranslationValue = "计算公式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.calculationformula", TranslationValue = "计算公式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.calculationformula", TranslationValue = "计算公式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.calculationformula", TranslationValue = "计算公式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.calculationformula", TranslationValue = "计算公式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.taxrule.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.description", TranslationValue = "规则说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.description", TranslationValue = "规则说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.description", TranslationValue = "规则说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.description", TranslationValue = "规则说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.description", TranslationValue = "规则说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.description", TranslationValue = "规则说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.taxrule.effectivedate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.effectivedate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.effectivedate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.effectivedate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.effectivedate", TranslationValue = "生效日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.taxrule.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.taxrule.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.taxrule.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.taxrule.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.taxrule.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.taxrule.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.taxrule.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
        };
    }
}
