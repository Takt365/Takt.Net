// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktKnowledgeChangeLogEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktKnowledgeChangeLog 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Routine.Business.HelpDesk;

/// <summary>
/// TaktKnowledgeChangeLog 实体翻译种子数据（自动生成，与 TaktKnowledgeChangeLog.cs 属性一一对应）
/// </summary>
public class TaktKnowledgeChangeLogEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktKnowledgeChangeLogEntityTranslations();

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
    /// 获取所有 TaktKnowledgeChangeLog 实体名称及字段翻译（自动生成，与 TaktKnowledgeChangeLog.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktKnowledgeChangeLogEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.knowledgechangelog（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.knowledgechangelog._self", TranslationValue = "知识库变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.knowledgechangelog._self", TranslationValue = "知识库变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.knowledgechangelog._self", TranslationValue = "知识库变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.knowledgechangelog._self", TranslationValue = "知识库变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.knowledgechangelog._self", TranslationValue = "知识库变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.knowledgechangelog._self", TranslationValue = "知识库变更日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.knowledgechangelog.knowledgeid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.knowledgechangelog.knowledgeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.knowledgechangelog.knowledgeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.knowledgechangelog.knowledgeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.knowledgechangelog.knowledgeid", TranslationValue = "知识ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.knowledgechangelog.knowledgeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.knowledgechangelog.knowledgeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.knowledgechangelog.knowledgetitle
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.knowledgechangelog.knowledgetitle", TranslationValue = "知识标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.knowledgechangelog.knowledgetitle", TranslationValue = "知识标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.knowledgechangelog.knowledgetitle", TranslationValue = "知识标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.knowledgechangelog.knowledgetitle", TranslationValue = "知识标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.knowledgechangelog.knowledgetitle", TranslationValue = "知识标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.knowledgechangelog.knowledgetitle", TranslationValue = "知识标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.knowledgechangelog.changetype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.knowledgechangelog.changetype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.knowledgechangelog.changetype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.knowledgechangelog.changetype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.knowledgechangelog.changetype", TranslationValue = "变更类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.knowledgechangelog.changetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.knowledgechangelog.changetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.knowledgechangelog.changesummary
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.knowledgechangelog.changesummary", TranslationValue = "修改内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.knowledgechangelog.changesummary", TranslationValue = "修改内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.knowledgechangelog.changesummary", TranslationValue = "修改内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.knowledgechangelog.changesummary", TranslationValue = "修改内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.knowledgechangelog.changesummary", TranslationValue = "修改内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.knowledgechangelog.changesummary", TranslationValue = "修改内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.knowledgechangelog.changefield
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.knowledgechangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.knowledgechangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.knowledgechangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.knowledgechangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.knowledgechangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.knowledgechangelog.changefield", TranslationValue = "变更字段名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.knowledgechangelog.oldvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.knowledgechangelog.oldvalue", TranslationValue = "原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.knowledgechangelog.oldvalue", TranslationValue = "原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.knowledgechangelog.oldvalue", TranslationValue = "原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.knowledgechangelog.oldvalue", TranslationValue = "原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.knowledgechangelog.oldvalue", TranslationValue = "原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.knowledgechangelog.oldvalue", TranslationValue = "原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.knowledgechangelog.newvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.knowledgechangelog.newvalue", TranslationValue = "新值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.knowledgechangelog.newvalue", TranslationValue = "新值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.knowledgechangelog.newvalue", TranslationValue = "新值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.knowledgechangelog.newvalue", TranslationValue = "新值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.knowledgechangelog.newvalue", TranslationValue = "新值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.knowledgechangelog.newvalue", TranslationValue = "新值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.knowledgechangelog.changereason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.knowledgechangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.knowledgechangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.knowledgechangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.knowledgechangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.knowledgechangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.knowledgechangelog.changereason", TranslationValue = "变更原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.knowledgechangelog.versionatchange
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.knowledgechangelog.versionatchange", TranslationValue = "变更时版本号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.knowledgechangelog.versionatchange", TranslationValue = "变更时版本号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.knowledgechangelog.versionatchange", TranslationValue = "变更时版本号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.knowledgechangelog.versionatchange", TranslationValue = "变更时版本号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.knowledgechangelog.versionatchange", TranslationValue = "变更时版本号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.knowledgechangelog.versionatchange", TranslationValue = "变更时版本号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
