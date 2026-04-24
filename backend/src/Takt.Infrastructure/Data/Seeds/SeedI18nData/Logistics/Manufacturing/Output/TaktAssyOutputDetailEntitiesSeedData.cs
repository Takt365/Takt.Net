// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktAssyOutputDetailEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktAssyOutputDetail 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Manufacturing.Output;

/// <summary>
/// TaktAssyOutputDetail 实体翻译种子数据（自动生成，与 TaktAssyOutputDetail.cs 属性一一对应）
/// </summary>
public class TaktAssyOutputDetailEntitiesSeedData : ITaktSeedData
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
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 0 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllTaktAssyOutputDetailEntityTranslations();

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
    /// 获取所有 TaktAssyOutputDetail 实体名称及字段翻译（自动生成，与 TaktAssyOutputDetail.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktAssyOutputDetailEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.assyoutputdetail（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail._self", TranslationValue = "组立日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail._self", TranslationValue = "组立日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail._self", TranslationValue = "组立日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail._self", TranslationValue = "组立日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail._self", TranslationValue = "组立日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail._self", TranslationValue = "组立日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.assyoutputid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.assyoutputid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.assyoutputid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.assyoutputid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.assyoutputid", TranslationValue = "组立日报ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.assyoutputid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.assyoutputid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.timeperiod
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.prodactualqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.prodactualqty", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.prodactualqty", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.prodactualqty", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.prodactualqty", TranslationValue = "实际生产数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.prodactualqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.prodactualqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.downtimeminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.downtimeminutes", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.downtimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.downtimeminutes", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.downtimeminutes", TranslationValue = "停线时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.downtimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.downtimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.downtimereason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.downtimereason", TranslationValue = "停线原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.downtimereason", TranslationValue = "停线原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.downtimereason", TranslationValue = "停线原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.downtimereason", TranslationValue = "停线原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.downtimereason", TranslationValue = "停线原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.downtimereason", TranslationValue = "停线原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.downtimedescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.downtimedescription", TranslationValue = "停线说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.downtimedescription", TranslationValue = "停线说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.downtimedescription", TranslationValue = "停线说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.downtimedescription", TranslationValue = "停线说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.downtimedescription", TranslationValue = "停线说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.downtimedescription", TranslationValue = "停线说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.unachievedreason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.unachieveddescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.inputminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.inputminutes", TranslationValue = "投入工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.inputminutes", TranslationValue = "投入工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.inputminutes", TranslationValue = "投入工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.inputminutes", TranslationValue = "投入工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.inputminutes", TranslationValue = "投入工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.inputminutes", TranslationValue = "投入工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.prodminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.prodminutes", TranslationValue = "生产工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.prodminutes", TranslationValue = "生产工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.prodminutes", TranslationValue = "生产工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.prodminutes", TranslationValue = "生产工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.prodminutes", TranslationValue = "生产工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.prodminutes", TranslationValue = "生产工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.actualminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.actualminutes", TranslationValue = "实际工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.actualminutes", TranslationValue = "实际工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.actualminutes", TranslationValue = "实际工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.actualminutes", TranslationValue = "实际工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.actualminutes", TranslationValue = "实际工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.actualminutes", TranslationValue = "实际工时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assyoutputdetail.achievementrate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assyoutputdetail.achievementrate", TranslationValue = "达成率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assyoutputdetail.achievementrate", TranslationValue = "达成率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assyoutputdetail.achievementrate", TranslationValue = "达成率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assyoutputdetail.achievementrate", TranslationValue = "达成率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assyoutputdetail.achievementrate", TranslationValue = "达成率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assyoutputdetail.achievementrate", TranslationValue = "达成率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
