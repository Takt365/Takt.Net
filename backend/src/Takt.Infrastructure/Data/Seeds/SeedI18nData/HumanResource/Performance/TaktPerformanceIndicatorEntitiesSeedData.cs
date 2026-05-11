// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPerformanceIndicatorEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktPerformanceIndicator 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.HumanResource.Performance;

/// <summary>
/// TaktPerformanceIndicator 实体翻译种子数据（自动生成，与 TaktPerformanceIndicator.cs 属性一一对应）
/// </summary>
public class TaktPerformanceIndicatorEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktPerformanceIndicatorEntityTranslations();

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
    /// 获取所有 TaktPerformanceIndicator 实体名称及字段翻译（自动生成，与 TaktPerformanceIndicator.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktPerformanceIndicatorEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.performanceindicator（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator._self", TranslationValue = "绩效指标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator._self", TranslationValue = "绩效指标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator._self", TranslationValue = "绩效指标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator._self", TranslationValue = "绩效指标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator._self", TranslationValue = "绩效指标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator._self", TranslationValue = "绩效指标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.performanceindicator.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.code", TranslationValue = "指标编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.code", TranslationValue = "指标编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.code", TranslationValue = "指标编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.code", TranslationValue = "指标编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.code", TranslationValue = "指标编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.code", TranslationValue = "指标编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.performanceindicator.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.name", TranslationValue = "指标名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.performanceindicator.category
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.category", TranslationValue = "指标类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.category", TranslationValue = "指标类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.category", TranslationValue = "指标类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.category", TranslationValue = "指标类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.category", TranslationValue = "指标类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.category", TranslationValue = "指标类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.performanceindicator.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.type", TranslationValue = "指标类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.performanceindicator.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.description", TranslationValue = "指标说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.description", TranslationValue = "指标说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.description", TranslationValue = "指标说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.description", TranslationValue = "指标说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.description", TranslationValue = "指标说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.description", TranslationValue = "指标说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.performanceindicator.scoringcriteria
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.scoringcriteria", TranslationValue = "评分标准说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.scoringcriteria", TranslationValue = "评分标准说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.scoringcriteria", TranslationValue = "评分标准说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.scoringcriteria", TranslationValue = "评分标准说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.scoringcriteria", TranslationValue = "评分标准说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.scoringcriteria", TranslationValue = "评分标准说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.performanceindicator.targetvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.targetvalue", TranslationValue = "目标值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.targetvalue", TranslationValue = "目标值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.targetvalue", TranslationValue = "目标值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.targetvalue", TranslationValue = "目标值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.targetvalue", TranslationValue = "目标值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.targetvalue", TranslationValue = "目标值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.performanceindicator.minimumvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.minimumvalue", TranslationValue = "最低要求值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.minimumvalue", TranslationValue = "最低要求值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.minimumvalue", TranslationValue = "最低要求值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.minimumvalue", TranslationValue = "最低要求值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.minimumvalue", TranslationValue = "最低要求值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.minimumvalue", TranslationValue = "最低要求值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.performanceindicator.excellentvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.excellentvalue", TranslationValue = "卓越值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.excellentvalue", TranslationValue = "卓越值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.excellentvalue", TranslationValue = "卓越值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.excellentvalue", TranslationValue = "卓越值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.excellentvalue", TranslationValue = "卓越值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.excellentvalue", TranslationValue = "卓越值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.performanceindicator.standardweight
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.standardweight", TranslationValue = "标准权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.standardweight", TranslationValue = "标准权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.standardweight", TranslationValue = "标准权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.standardweight", TranslationValue = "标准权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.standardweight", TranslationValue = "标准权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.standardweight", TranslationValue = "标准权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.performanceindicator.datasource
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.datasource", TranslationValue = "数据来源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.datasource", TranslationValue = "数据来源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.datasource", TranslationValue = "数据来源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.datasource", TranslationValue = "数据来源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.datasource", TranslationValue = "数据来源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.datasource", TranslationValue = "数据来源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.performanceindicator.evaluationcycle
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.evaluationcycle", TranslationValue = "考核周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.evaluationcycle", TranslationValue = "考核周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.evaluationcycle", TranslationValue = "考核周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.evaluationcycle", TranslationValue = "考核周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.evaluationcycle", TranslationValue = "考核周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.evaluationcycle", TranslationValue = "考核周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.performanceindicator.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.performanceindicator.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceindicator.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceindicator.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceindicator.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceindicator.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceindicator.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceindicator.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
        };
    }
}
