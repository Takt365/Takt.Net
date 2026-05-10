// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktTenantEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktTenant 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Identity;

/// <summary>
/// TaktTenant 实体翻译种子数据（自动生成，与 TaktTenant.cs 属性一一对应）
/// </summary>
public class TaktTenantEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktTenantEntityTranslations();

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
    /// 获取所有 TaktTenant 实体名称及字段翻译（自动生成，与 TaktTenant.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktTenantEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.tenant（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.tenant._self", TranslationValue = "租户信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.tenant._self", TranslationValue = "租户信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.tenant._self", TranslationValue = "租户信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.tenant._self", TranslationValue = "租户信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.tenant._self", TranslationValue = "租户信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.tenant._self", TranslationValue = "租户信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.tenant.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.tenant.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.tenant.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.tenant.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.tenant.name", TranslationValue = "租户名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.tenant.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.tenant.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.tenant.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.tenant.code", TranslationValue = "租户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.tenant.code", TranslationValue = "租户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.tenant.code", TranslationValue = "租户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.tenant.code", TranslationValue = "租户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.tenant.code", TranslationValue = "租户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.tenant.code", TranslationValue = "租户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.tenant.allowedconfigids
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.tenant.allowedconfigids", TranslationValue = "数据库访问列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.tenant.allowedconfigids", TranslationValue = "数据库访问列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.tenant.allowedconfigids", TranslationValue = "数据库访问列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.tenant.allowedconfigids", TranslationValue = "数据库访问列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.tenant.allowedconfigids", TranslationValue = "数据库访问列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.tenant.allowedconfigids", TranslationValue = "数据库访问列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.tenant.subscriptionstarttime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.tenant.subscriptionstarttime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.tenant.subscriptionstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.tenant.subscriptionstarttime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.tenant.subscriptionstarttime", TranslationValue = "订阅开始时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.tenant.subscriptionstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.tenant.subscriptionstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.tenant.subscriptionendtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.tenant.subscriptionendtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.tenant.subscriptionendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.tenant.subscriptionendtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.tenant.subscriptionendtime", TranslationValue = "订阅结束时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.tenant.subscriptionendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.tenant.subscriptionendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.tenant.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.tenant.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.tenant.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.tenant.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.tenant.status", TranslationValue = "租户状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.tenant.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.tenant.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
        };
    }
}
