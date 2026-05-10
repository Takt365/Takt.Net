// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktQualityScrapItemEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktQualityScrapItem 实体字段翻译种子数据（自动生成）
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
/// TaktQualityScrapItem 实体翻译种子数据（自动生成，与 TaktQualityScrapItem.cs 属性一一对应）
/// </summary>
public class TaktQualityScrapItemEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktQualityScrapItemEntityTranslations();

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
    /// 获取所有 TaktQualityScrapItem 实体名称及字段翻译（自动生成，与 TaktQualityScrapItem.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktQualityScrapItemEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.qualityscrapitem（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem._self", TranslationValue = "品质废弃明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem._self", TranslationValue = "品质废弃明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem._self", TranslationValue = "品质废弃明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem._self", TranslationValue = "品质废弃明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem._self", TranslationValue = "品质废弃明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem._self", TranslationValue = "品质废弃明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.qualityscrapitem.qualityscrapid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.qualityscrapid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.qualityscrapid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.qualityscrapid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.qualityscrapid", TranslationValue = "品质废弃主表ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.qualityscrapid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.qualityscrapid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.qualityscrapitem.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.qualityscrapitem.materialcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.qualityscrapitem.materialname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.materialname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.materialname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.materialname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.materialname", TranslationValue = "物料名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.qualityscrapitem.scrapcost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.scrapcost", TranslationValue = "废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.scrapcost", TranslationValue = "废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.scrapcost", TranslationValue = "废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.scrapcost", TranslationValue = "废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.scrapcost", TranslationValue = "废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.scrapcost", TranslationValue = "废弃费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.qualityscrapitem.scrapsize
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.scrapsize", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.scrapsize", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.scrapsize", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.scrapsize", TranslationValue = "废弃数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.scrapsize", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.scrapsize", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.qualityscrapitem.partprice
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.partprice", TranslationValue = "零件单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.partprice", TranslationValue = "零件单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.partprice", TranslationValue = "零件单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.partprice", TranslationValue = "零件单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.partprice", TranslationValue = "零件单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.partprice", TranslationValue = "零件单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.qualityscrapitem.scrapreasoncost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.scrapreasoncost", TranslationValue = "废弃处理费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.scrapreasoncost", TranslationValue = "废弃处理费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.scrapreasoncost", TranslationValue = "废弃处理费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.scrapreasoncost", TranslationValue = "废弃处理费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.scrapreasoncost", TranslationValue = "废弃处理费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.scrapreasoncost", TranslationValue = "废弃处理费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.qualityscrapitem.freightcharges
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.freightcharges", TranslationValue = "运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.freightcharges", TranslationValue = "运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.freightcharges", TranslationValue = "运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.freightcharges", TranslationValue = "运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.freightcharges", TranslationValue = "运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.freightcharges", TranslationValue = "运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.qualityscrapitem.otherexpenses
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.qualityscrapitem.reasonworktimeminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.reasonworktimeminutes", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.reasonworktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.reasonworktimeminutes", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.reasonworktimeminutes", TranslationValue = "处理作业时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.reasonworktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.reasonworktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.qualityscrapitem.tax
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.tax", TranslationValue = "关税", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.tax", TranslationValue = "关税", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.tax", TranslationValue = "关税", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.tax", TranslationValue = "关税", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.tax", TranslationValue = "关税", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.tax", TranslationValue = "关税", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.qualityscrapitem.reasonotherexpenses
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.reasonotherexpenses", TranslationValue = "处理发生其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.reasonotherexpenses", TranslationValue = "处理发生其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.reasonotherexpenses", TranslationValue = "处理发生其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.reasonotherexpenses", TranslationValue = "处理发生其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.reasonotherexpenses", TranslationValue = "处理发生其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.reasonotherexpenses", TranslationValue = "处理发生其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.qualityscrapitem.scrapnote
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityscrapitem.scrapnote", TranslationValue = "Remark", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityscrapitem.scrapnote", TranslationValue = "備考", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityscrapitem.scrapnote", TranslationValue = "비고", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityscrapitem.scrapnote", TranslationValue = "废弃备注", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityscrapitem.scrapnote", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityscrapitem.scrapnote", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
        };
    }
}
