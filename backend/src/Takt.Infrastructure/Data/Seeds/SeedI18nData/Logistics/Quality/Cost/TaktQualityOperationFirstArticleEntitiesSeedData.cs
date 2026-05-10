// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktQualityOperationFirstArticleEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktQualityOperationFirstArticle 实体字段翻译种子数据（自动生成）
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
/// TaktQualityOperationFirstArticle 实体翻译种子数据（自动生成，与 TaktQualityOperationFirstArticle.cs 属性一一对应）
/// </summary>
public class TaktQualityOperationFirstArticleEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktQualityOperationFirstArticleEntityTranslations();

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
    /// 获取所有 TaktQualityOperationFirstArticle 实体名称及字段翻译（自动生成，与 TaktQualityOperationFirstArticle.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktQualityOperationFirstArticleEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.qualityoperationfirstarticle（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityoperationfirstarticle._self", TranslationValue = "品质业务初期定期检定费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityoperationfirstarticle._self", TranslationValue = "品质业务初期定期检定费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityoperationfirstarticle._self", TranslationValue = "品质业务初期定期检定费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityoperationfirstarticle._self", TranslationValue = "品质业务初期定期检定费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityoperationfirstarticle._self", TranslationValue = "品质业务初期定期检定费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityoperationfirstarticle._self", TranslationValue = "品质业务初期定期检定费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.qualityoperationfirstarticle.qualityoperationid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityoperationfirstarticle.qualityoperationid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityoperationfirstarticle.qualityoperationid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityoperationfirstarticle.qualityoperationid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityoperationfirstarticle.qualityoperationid", TranslationValue = "品质业务主表ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityoperationfirstarticle.qualityoperationid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityoperationfirstarticle.qualityoperationid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.qualityoperationfirstarticle.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityoperationfirstarticle.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityoperationfirstarticle.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityoperationfirstarticle.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityoperationfirstarticle.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityoperationfirstarticle.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityoperationfirstarticle.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.qualityoperationfirstarticle.qualificationcost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityoperationfirstarticle.qualificationcost", TranslationValue = "初期检定定期检定业务费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityoperationfirstarticle.qualificationcost", TranslationValue = "初期检定定期检定业务费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityoperationfirstarticle.qualificationcost", TranslationValue = "初期检定定期检定业务费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityoperationfirstarticle.qualificationcost", TranslationValue = "初期检定定期检定业务费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityoperationfirstarticle.qualificationcost", TranslationValue = "初期检定定期检定业务费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityoperationfirstarticle.qualificationcost", TranslationValue = "初期检定定期检定业务费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.qualityoperationfirstarticle.worktimeminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityoperationfirstarticle.worktimeminutes", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityoperationfirstarticle.worktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityoperationfirstarticle.worktimeminutes", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityoperationfirstarticle.worktimeminutes", TranslationValue = "检定作业时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityoperationfirstarticle.worktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityoperationfirstarticle.worktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.qualityoperationfirstarticle.otherexpenses
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityoperationfirstarticle.otherexpenses", TranslationValue = "检定其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityoperationfirstarticle.otherexpenses", TranslationValue = "检定其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityoperationfirstarticle.otherexpenses", TranslationValue = "检定其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityoperationfirstarticle.otherexpenses", TranslationValue = "检定其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityoperationfirstarticle.otherexpenses", TranslationValue = "检定其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityoperationfirstarticle.otherexpenses", TranslationValue = "检定其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.qualityoperationfirstarticle.qualificationnote
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityoperationfirstarticle.qualificationnote", TranslationValue = "Remark", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityoperationfirstarticle.qualificationnote", TranslationValue = "備考", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityoperationfirstarticle.qualificationnote", TranslationValue = "비고", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityoperationfirstarticle.qualificationnote", TranslationValue = "检定备注", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityoperationfirstarticle.qualificationnote", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityoperationfirstarticle.qualificationnote", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
        };
    }
}
