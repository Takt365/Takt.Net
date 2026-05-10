// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktQualityScrapEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktQualityScrap 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality.Cost;

/// <summary>
/// TaktQualityScrap 实体翻译种子数据（自动生成，与 TaktQualityScrap.cs 属性一一对应）
/// </summary>
public class TaktQualityScrapEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktQualityScrapEntityTranslations();

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
    /// 获取所有 TaktQualityScrap 实体名称及字段翻译（自动生成，与 TaktQualityScrap.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktQualityScrapEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.qualityscrap（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrap._self", TranslationValue = "品质废弃主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrap._self", TranslationValue = "品质废弃主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrap._self", TranslationValue = "品质废弃主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrap._self", TranslationValue = "品质废弃主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrap._self", TranslationValue = "品质废弃主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrap._self", TranslationValue = "品质废弃主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.qualityscrap.plantcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrap.plantcode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrap.plantcode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrap.plantcode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrap.plantcode", TranslationValue = "工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrap.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrap.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.qualityscrap.no
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrap.no", TranslationValue = "废弃单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrap.no", TranslationValue = "废弃单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrap.no", TranslationValue = "废弃单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrap.no", TranslationValue = "废弃单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrap.no", TranslationValue = "废弃单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrap.no", TranslationValue = "废弃单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.qualityscrap.date
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrap.date", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrap.date", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrap.date", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrap.date", TranslationValue = "废弃日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrap.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrap.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.qualityscrap.indirectmanpowercostperminute
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrap.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrap.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrap.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrap.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrap.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrap.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.qualityscrap.model
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrap.model", TranslationValue = "机种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrap.model", TranslationValue = "机种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrap.model", TranslationValue = "机种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrap.model", TranslationValue = "机种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrap.model", TranslationValue = "机种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrap.model", TranslationValue = "机种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.qualityscrap.reason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrap.reason", TranslationValue = "事故内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrap.reason", TranslationValue = "事故内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrap.reason", TranslationValue = "事故内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrap.reason", TranslationValue = "事故内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrap.reason", TranslationValue = "事故内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrap.reason", TranslationValue = "事故内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.qualityscrap.totalscrapquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrap.totalscrapquantity", TranslationValue = "废弃总数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrap.totalscrapquantity", TranslationValue = "废弃总数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrap.totalscrapquantity", TranslationValue = "废弃总数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrap.totalscrapquantity", TranslationValue = "废弃总数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrap.totalscrapquantity", TranslationValue = "废弃总数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrap.totalscrapquantity", TranslationValue = "废弃总数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.qualityscrap.totalscrapcost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrap.totalscrapcost", TranslationValue = "总废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrap.totalscrapcost", TranslationValue = "总废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrap.totalscrapcost", TranslationValue = "总废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrap.totalscrapcost", TranslationValue = "总废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrap.totalscrapcost", TranslationValue = "总废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrap.totalscrapcost", TranslationValue = "总废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.qualityscrap.costcurrency
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrap.costcurrency", TranslationValue = "成本币种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrap.costcurrency", TranslationValue = "成本币种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrap.costcurrency", TranslationValue = "成本币种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrap.costcurrency", TranslationValue = "成本币种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrap.costcurrency", TranslationValue = "成本币种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrap.costcurrency", TranslationValue = "成本币种", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
        };
    }
}
