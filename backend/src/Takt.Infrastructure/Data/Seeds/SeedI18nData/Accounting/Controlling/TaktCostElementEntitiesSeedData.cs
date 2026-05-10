// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktCostElementEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktCostElement 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Accounting.Controlling;

/// <summary>
/// TaktCostElement 实体翻译种子数据（自动生成，与 TaktCostElement.cs 属性一一对应）
/// </summary>
public class TaktCostElementEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktCostElementEntityTranslations();

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
    /// 获取所有 TaktCostElement 实体名称及字段翻译（自动生成，与 TaktCostElement.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktCostElementEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.costelement（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement._self", TranslationValue = "成本要素", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement._self", TranslationValue = "成本要素", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement._self", TranslationValue = "成本要素", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement._self", TranslationValue = "成本要素", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement._self", TranslationValue = "成本要素", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement._self", TranslationValue = "成本要素", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.costelement.companycode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement.companycode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement.companycode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement.companycode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.costelement.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement.code", TranslationValue = "成本要素编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement.code", TranslationValue = "成本要素编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement.code", TranslationValue = "成本要素编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement.code", TranslationValue = "成本要素编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement.code", TranslationValue = "成本要素编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement.code", TranslationValue = "成本要素编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.costelement.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement.name", TranslationValue = "成本要素名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.costelement.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement.type", TranslationValue = "成本要素类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.costelement.category
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement.category", TranslationValue = "成本要素类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement.category", TranslationValue = "成本要素类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement.category", TranslationValue = "成本要素类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement.category", TranslationValue = "成本要素类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement.category", TranslationValue = "成本要素类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement.category", TranslationValue = "成本要素类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.costelement.parentid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement.parentid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement.parentid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement.parentid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement.parentid", TranslationValue = "父级ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement.parentid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement.parentid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.costelement.level
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement.level", TranslationValue = "成本要素层级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement.level", TranslationValue = "成本要素层级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement.level", TranslationValue = "成本要素层级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement.level", TranslationValue = "成本要素层级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement.level", TranslationValue = "成本要素层级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement.level", TranslationValue = "成本要素层级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.costelement.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement.status", TranslationValue = "成本要素状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.costelement.validfrom
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement.validfrom", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement.validfrom", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement.validfrom", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement.validfrom", TranslationValue = "生效日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement.validfrom", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement.validfrom", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.costelement.validto
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement.validto", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement.validto", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement.validto", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement.validto", TranslationValue = "失效日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement.validto", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement.validto", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.costelement.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.costelement.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.costelement.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.costelement.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.costelement.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.costelement.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.costelement.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
        };
    }
}
