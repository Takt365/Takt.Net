// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktIqcOrderChangeLogEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktIqcOrderChangeLog 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality.Operation;

/// <summary>
/// TaktIqcOrderChangeLog 实体翻译种子数据（自动生成，与 TaktIqcOrderChangeLog.cs 属性一一对应）
/// </summary>
public class TaktIqcOrderChangeLogEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktIqcOrderChangeLogEntityTranslations();

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
    /// 获取所有 TaktIqcOrderChangeLog 实体名称及字段翻译（自动生成，与 TaktIqcOrderChangeLog.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktIqcOrderChangeLogEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.iqcorderchangelog（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorderchangelog._self", TranslationValue = "进货检验单变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorderchangelog._self", TranslationValue = "进货检验单变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorderchangelog._self", TranslationValue = "进货检验单变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorderchangelog._self", TranslationValue = "进货检验单变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorderchangelog._self", TranslationValue = "进货检验单变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorderchangelog._self", TranslationValue = "进货检验单变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.iqcorderchangelog.iqcorderid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorderchangelog.iqcorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorderchangelog.iqcorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorderchangelog.iqcorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorderchangelog.iqcorderid", TranslationValue = "IQC检验单ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorderchangelog.iqcorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorderchangelog.iqcorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.iqcorderchangelog.changefields
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorderchangelog.changefields", TranslationValue = "变更字段列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorderchangelog.changefields", TranslationValue = "变更字段列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorderchangelog.changefields", TranslationValue = "变更字段列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorderchangelog.changefields", TranslationValue = "变更字段列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorderchangelog.changefields", TranslationValue = "变更字段列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorderchangelog.changefields", TranslationValue = "变更字段列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.iqcorderchangelog.changetype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorderchangelog.changetype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorderchangelog.changetype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorderchangelog.changetype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorderchangelog.changetype", TranslationValue = "变更类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorderchangelog.changetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorderchangelog.changetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.iqcorderchangelog.changereason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorderchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorderchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorderchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorderchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorderchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorderchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.iqcorderchangelog.changeby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorderchangelog.changeby", TranslationValue = "变更人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorderchangelog.changeby", TranslationValue = "变更人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorderchangelog.changeby", TranslationValue = "变更人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorderchangelog.changeby", TranslationValue = "变更人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorderchangelog.changeby", TranslationValue = "变更人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorderchangelog.changeby", TranslationValue = "变更人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.iqcorderchangelog.changetime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorderchangelog.changetime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorderchangelog.changetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorderchangelog.changetime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorderchangelog.changetime", TranslationValue = "变更时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorderchangelog.changetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorderchangelog.changetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
        };
    }
}
