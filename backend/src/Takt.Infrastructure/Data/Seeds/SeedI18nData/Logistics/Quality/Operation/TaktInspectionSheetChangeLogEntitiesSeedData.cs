// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktInspectionSheetChangeLogEntitiesSeedData.cs
// 创建时间：2026-05-07
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktInspectionSheetChangeLog 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality;

/// <summary>
/// TaktInspectionSheetChangeLog 实体翻译种子数据（自动生成，与 TaktInspectionSheetChangeLog.cs 属性一一对应）
/// </summary>
public class TaktInspectionSheetChangeLogEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktInspectionSheetChangeLogEntityTranslations();

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
    /// 获取所有 TaktInspectionSheetChangeLog 实体名称及字段翻译（自动生成，与 TaktInspectionSheetChangeLog.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktInspectionSheetChangeLogEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.inspectionsheetchangelog（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetchangelog._self", TranslationValue = "检验单变更日志", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetchangelog._self", TranslationValue = "检验单变更日志", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetchangelog._self", TranslationValue = "检验单变更日志", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetchangelog._self", TranslationValue = "检验单变更日志", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetchangelog._self", TranslationValue = "检验单变更日志", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetchangelog._self", TranslationValue = "检验单变更日志", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.inspectionsheetchangelog.sheetid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetchangelog.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetchangelog.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetchangelog.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetchangelog.sheetid", TranslationValue = "检验单ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetchangelog.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetchangelog.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.inspectionsheetchangelog.changefield
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetchangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetchangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetchangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetchangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetchangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetchangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.inspectionsheetchangelog.changefielddescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetchangelog.changefielddescription", TranslationValue = "Description", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetchangelog.changefielddescription", TranslationValue = "説明", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetchangelog.changefielddescription", TranslationValue = "설명", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetchangelog.changefielddescription", TranslationValue = "变更字段描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetchangelog.changefielddescription", TranslationValue = "描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetchangelog.changefielddescription", TranslationValue = "描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.inspectionsheetchangelog.oldvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetchangelog.oldvalue", TranslationValue = "原值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetchangelog.oldvalue", TranslationValue = "原值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetchangelog.oldvalue", TranslationValue = "原值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetchangelog.oldvalue", TranslationValue = "原值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetchangelog.oldvalue", TranslationValue = "原值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetchangelog.oldvalue", TranslationValue = "原值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.inspectionsheetchangelog.newvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetchangelog.newvalue", TranslationValue = "新值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetchangelog.newvalue", TranslationValue = "新值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetchangelog.newvalue", TranslationValue = "新值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetchangelog.newvalue", TranslationValue = "新值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetchangelog.newvalue", TranslationValue = "新值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetchangelog.newvalue", TranslationValue = "新值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.inspectionsheetchangelog.changetype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetchangelog.changetype", TranslationValue = "Type", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetchangelog.changetype", TranslationValue = "タイプ", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetchangelog.changetype", TranslationValue = "유형", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetchangelog.changetype", TranslationValue = "变更类型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetchangelog.changetype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetchangelog.changetype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.inspectionsheetchangelog.changereason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetchangelog.changereason", TranslationValue = "变更原因", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.inspectionsheetchangelog.changeuserid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetchangelog.changeuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetchangelog.changeuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetchangelog.changeuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetchangelog.changeuserid", TranslationValue = "变更人ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetchangelog.changeuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetchangelog.changeuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.inspectionsheetchangelog.changeusername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetchangelog.changeusername", TranslationValue = "变更人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetchangelog.changeusername", TranslationValue = "变更人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetchangelog.changeusername", TranslationValue = "变更人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetchangelog.changeusername", TranslationValue = "变更人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetchangelog.changeusername", TranslationValue = "变更人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetchangelog.changeusername", TranslationValue = "变更人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.inspectionsheetchangelog.changetime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetchangelog.changetime", TranslationValue = "Time", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetchangelog.changetime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetchangelog.changetime", TranslationValue = "시간", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetchangelog.changetime", TranslationValue = "变更时间", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetchangelog.changetime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetchangelog.changetime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
        };
    }
}
