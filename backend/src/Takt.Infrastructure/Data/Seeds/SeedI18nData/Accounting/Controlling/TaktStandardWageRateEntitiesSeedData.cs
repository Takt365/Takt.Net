// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktStandardWageRateEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktStandardWageRate 实体字段翻译种子数据（自动生成）
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
/// TaktStandardWageRate 实体翻译种子数据（自动生成，与 TaktStandardWageRate.cs 属性一一对应）
/// </summary>
public class TaktStandardWageRateEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktStandardWageRateEntityTranslations();

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
    /// 获取所有 TaktStandardWageRate 实体名称及字段翻译（自动生成，与 TaktStandardWageRate.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktStandardWageRateEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.standardwagerate（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate._self", TranslationValue = "标准工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate._self", TranslationValue = "标准工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate._self", TranslationValue = "标准工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate._self", TranslationValue = "标准工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate._self", TranslationValue = "标准工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate._self", TranslationValue = "标准工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.companycode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.yearmonth
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "年月", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "年月", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "年月", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "年月", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "年月", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "年月", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.workingdays
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "工作天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "工作天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "工作天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "工作天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "工作天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "工作天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.salesamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "销售额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "销售额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "销售额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "销售额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "销售额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "销售额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.directlaborcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "直接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "直接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "直接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "直接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "直接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "直接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.directlaborwage
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "直接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "直接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "直接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "直接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "直接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "直接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.directovertimehours
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "直接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "直接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "直接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "直接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "直接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "直接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.directovertimetotal
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "直接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "直接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "直接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "直接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "直接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "直接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.directwagerate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "直接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "直接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "直接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "直接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "直接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "直接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.indirectlaborcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "间接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "间接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "间接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "间接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "间接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "间接人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.indirectlaborwage
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "间接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "间接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "间接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "间接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "间接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "间接工资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.indirectovertimehours
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "间接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "间接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "间接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "间接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "间接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "间接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.indirectovertimetotal
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "间接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "间接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "间接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "间接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "间接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "间接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.indirectwagerate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "间接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "间接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "间接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "间接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "间接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "间接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.standardwagerate.relatedplant
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
