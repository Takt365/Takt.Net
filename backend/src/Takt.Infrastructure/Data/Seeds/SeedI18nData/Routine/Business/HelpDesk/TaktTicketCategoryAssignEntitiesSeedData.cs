// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktTicketCategoryAssignEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktTicketCategoryAssign 实体字段翻译种子数据（自动生成）
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
/// TaktTicketCategoryAssign 实体翻译种子数据（自动生成，与 TaktTicketCategoryAssign.cs 属性一一对应）
/// </summary>
public class TaktTicketCategoryAssignEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktTicketCategoryAssignEntityTranslations();

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
    /// 获取所有 TaktTicketCategoryAssign 实体名称及字段翻译（自动生成，与 TaktTicketCategoryAssign.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktTicketCategoryAssignEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.ticketcategoryassign（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ticketcategoryassign._self", TranslationValue = "工单分类默认处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ticketcategoryassign._self", TranslationValue = "工单分类默认处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ticketcategoryassign._self", TranslationValue = "工单分类默认处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ticketcategoryassign._self", TranslationValue = "工单分类默认处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ticketcategoryassign._self", TranslationValue = "工单分类默认处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ticketcategoryassign._self", TranslationValue = "工单分类默认处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.ticketcategoryassign.categorycode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ticketcategoryassign.categorycode", TranslationValue = "分类编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ticketcategoryassign.categorycode", TranslationValue = "分类编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ticketcategoryassign.categorycode", TranslationValue = "分类编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ticketcategoryassign.categorycode", TranslationValue = "分类编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ticketcategoryassign.categorycode", TranslationValue = "分类编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ticketcategoryassign.categorycode", TranslationValue = "分类编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.ticketcategoryassign.eeid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ticketcategoryassign.eeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ticketcategoryassign.eeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ticketcategoryassign.eeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ticketcategoryassign.eeid", TranslationValue = "默认处理人ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ticketcategoryassign.eeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ticketcategoryassign.eeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.ticketcategoryassign.eename
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ticketcategoryassign.eename", TranslationValue = "默认处理人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ticketcategoryassign.eename", TranslationValue = "默认处理人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ticketcategoryassign.eename", TranslationValue = "默认处理人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ticketcategoryassign.eename", TranslationValue = "默认处理人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ticketcategoryassign.eename", TranslationValue = "默认处理人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ticketcategoryassign.eename", TranslationValue = "默认处理人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.ticketcategoryassign.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ticketcategoryassign.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ticketcategoryassign.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ticketcategoryassign.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ticketcategoryassign.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ticketcategoryassign.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ticketcategoryassign.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
        };
    }
}
