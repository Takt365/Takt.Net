// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktCompensationBenefitEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktCompensationBenefit 实体字段翻译种子数据（自动生成）
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
/// TaktCompensationBenefit 实体翻译种子数据（自动生成，与 TaktCompensationBenefit.cs 属性一一对应）
/// </summary>
public class TaktCompensationBenefitEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktCompensationBenefitEntityTranslations();

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
    /// 获取所有 TaktCompensationBenefit 实体名称及字段翻译（自动生成，与 TaktCompensationBenefit.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktCompensationBenefitEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.compensationbenefit（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit._self", TranslationValue = "薪酬福利", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit._self", TranslationValue = "薪酬福利", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit._self", TranslationValue = "薪酬福利", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit._self", TranslationValue = "薪酬福利", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit._self", TranslationValue = "薪酬福利", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit._self", TranslationValue = "薪酬福利", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.compensationbenefit.employeeid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.employeeid", TranslationValue = "员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.compensationbenefit.basesalary
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.basesalary", TranslationValue = "基本工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.basesalary", TranslationValue = "基本工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.basesalary", TranslationValue = "基本工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.basesalary", TranslationValue = "基本工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.basesalary", TranslationValue = "基本工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.basesalary", TranslationValue = "基本工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.compensationbenefit.positionallowance
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.positionallowance", TranslationValue = "岗位津贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.positionallowance", TranslationValue = "岗位津贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.positionallowance", TranslationValue = "岗位津贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.positionallowance", TranslationValue = "岗位津贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.positionallowance", TranslationValue = "岗位津贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.positionallowance", TranslationValue = "岗位津贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.compensationbenefit.performancebonus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.performancebonus", TranslationValue = "绩效奖金", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.performancebonus", TranslationValue = "绩效奖金", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.performancebonus", TranslationValue = "绩效奖金", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.performancebonus", TranslationValue = "绩效奖金", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.performancebonus", TranslationValue = "绩效奖金", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.performancebonus", TranslationValue = "绩效奖金", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.compensationbenefit.overtimepay
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.overtimepay", TranslationValue = "加班费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.overtimepay", TranslationValue = "加班费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.overtimepay", TranslationValue = "加班费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.overtimepay", TranslationValue = "加班费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.overtimepay", TranslationValue = "加班费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.overtimepay", TranslationValue = "加班费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.compensationbenefit.transportallowance
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.transportallowance", TranslationValue = "交通补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.transportallowance", TranslationValue = "交通补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.transportallowance", TranslationValue = "交通补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.transportallowance", TranslationValue = "交通补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.transportallowance", TranslationValue = "交通补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.transportallowance", TranslationValue = "交通补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.compensationbenefit.mealallowance
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.mealallowance", TranslationValue = "餐费补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.mealallowance", TranslationValue = "餐费补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.mealallowance", TranslationValue = "餐费补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.mealallowance", TranslationValue = "餐费补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.mealallowance", TranslationValue = "餐费补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.mealallowance", TranslationValue = "餐费补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.compensationbenefit.housingallowance
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.housingallowance", TranslationValue = "住房补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.housingallowance", TranslationValue = "住房补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.housingallowance", TranslationValue = "住房补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.housingallowance", TranslationValue = "住房补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.housingallowance", TranslationValue = "住房补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.housingallowance", TranslationValue = "住房补贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.compensationbenefit.socialsecuritybase
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.socialsecuritybase", TranslationValue = "社保缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.socialsecuritybase", TranslationValue = "社保缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.socialsecuritybase", TranslationValue = "社保缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.socialsecuritybase", TranslationValue = "社保缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.socialsecuritybase", TranslationValue = "社保缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.socialsecuritybase", TranslationValue = "社保缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.compensationbenefit.housingfundbase
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.housingfundbase", TranslationValue = "公积金缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.housingfundbase", TranslationValue = "公积金缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.housingfundbase", TranslationValue = "公积金缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.housingfundbase", TranslationValue = "公积金缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.housingfundbase", TranslationValue = "公积金缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.housingfundbase", TranslationValue = "公积金缴纳基数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.compensationbenefit.otherbenefits
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.otherbenefits", TranslationValue = "其他福利说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.otherbenefits", TranslationValue = "其他福利说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.otherbenefits", TranslationValue = "其他福利说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.otherbenefits", TranslationValue = "其他福利说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.otherbenefits", TranslationValue = "其他福利说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.otherbenefits", TranslationValue = "其他福利说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.compensationbenefit.effectivedate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.effectivedate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.effectivedate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.effectivedate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.effectivedate", TranslationValue = "生效日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.compensationbenefit.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.compensationbenefit.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.compensationbenefit.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.compensationbenefit.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.compensationbenefit.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.compensationbenefit.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.compensationbenefit.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
        };
    }
}
