// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktReportSchemeEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktReportScheme 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Statistics.Report;

/// <summary>
/// TaktReportScheme 实体翻译种子数据（自动生成，与 TaktReportScheme.cs 属性一一对应）
/// </summary>
public class TaktReportSchemeEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktReportSchemeEntityTranslations();

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
    /// 获取所有 TaktReportScheme 实体名称及字段翻译（自动生成，与 TaktReportScheme.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktReportSchemeEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.reportscheme（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme._self", TranslationValue = "报表方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme._self", TranslationValue = "报表方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme._self", TranslationValue = "报表方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme._self", TranslationValue = "报表方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme._self", TranslationValue = "报表方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme._self", TranslationValue = "报表方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.reportscheme.reportcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.reportcode", TranslationValue = "报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.reportcode", TranslationValue = "报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.reportcode", TranslationValue = "报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.reportcode", TranslationValue = "报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.reportcode", TranslationValue = "报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.reportcode", TranslationValue = "报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.reportscheme.reportname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.reportname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.reportname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.reportname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.reportname", TranslationValue = "报表名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.reportname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.reportname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.reportscheme.reportcategory
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.reportcategory", TranslationValue = "报表类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.reportcategory", TranslationValue = "报表类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.reportcategory", TranslationValue = "报表类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.reportcategory", TranslationValue = "报表类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.reportcategory", TranslationValue = "报表类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.reportcategory", TranslationValue = "报表类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.reportscheme.applicationmodule
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.applicationmodule", TranslationValue = "应用模块", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.applicationmodule", TranslationValue = "应用模块", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.applicationmodule", TranslationValue = "应用模块", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.applicationmodule", TranslationValue = "应用模块", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.applicationmodule", TranslationValue = "应用模块", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.applicationmodule", TranslationValue = "应用模块", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.reportscheme.reportdescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.reportdescription", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.reportdescription", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.reportdescription", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.reportdescription", TranslationValue = "报表描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.reportdescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.reportdescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.reportscheme.selectionscreenconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.selectionscreenconfig", TranslationValue = "选择屏幕配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.selectionscreenconfig", TranslationValue = "选择屏幕配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.selectionscreenconfig", TranslationValue = "选择屏幕配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.selectionscreenconfig", TranslationValue = "选择屏幕配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.selectionscreenconfig", TranslationValue = "选择屏幕配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.selectionscreenconfig", TranslationValue = "选择屏幕配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.reportscheme.datasourcetype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.datasourcetype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.datasourcetype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.datasourcetype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.datasourcetype", TranslationValue = "数据源类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.datasourcetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.datasourcetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.reportscheme.datasourcename
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.datasourcename", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.datasourcename", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.datasourcename", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.datasourcename", TranslationValue = "数据源名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.datasourcename", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.datasourcename", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.reportscheme.sqlquery
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.sqlquery", TranslationValue = "SQL查询语句", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.sqlquery", TranslationValue = "SQL查询语句", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.sqlquery", TranslationValue = "SQL查询语句", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.sqlquery", TranslationValue = "SQL查询语句", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.sqlquery", TranslationValue = "SQL查询语句", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.sqlquery", TranslationValue = "SQL查询语句", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.reportscheme.outputtype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.outputtype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.outputtype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.outputtype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.outputtype", TranslationValue = "输出类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.outputtype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.outputtype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.reportscheme.alvcolumnconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.alvcolumnconfig", TranslationValue = "ALV列配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.alvcolumnconfig", TranslationValue = "ALV列配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.alvcolumnconfig", TranslationValue = "ALV列配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.alvcolumnconfig", TranslationValue = "ALV列配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.alvcolumnconfig", TranslationValue = "ALV列配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.alvcolumnconfig", TranslationValue = "ALV列配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.reportscheme.defaultlayoutvariant
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.defaultlayoutvariant", TranslationValue = "默认布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.defaultlayoutvariant", TranslationValue = "默认布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.defaultlayoutvariant", TranslationValue = "默认布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.defaultlayoutvariant", TranslationValue = "默认布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.defaultlayoutvariant", TranslationValue = "默认布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.defaultlayoutvariant", TranslationValue = "默认布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.reportscheme.supportlayoutvariant
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.supportlayoutvariant", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.supportlayoutvariant", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.supportlayoutvariant", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.supportlayoutvariant", TranslationValue = "是否支持布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.supportlayoutvariant", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.supportlayoutvariant", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.reportscheme.subtotalfields
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.subtotalfields", TranslationValue = "小计字段配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.subtotalfields", TranslationValue = "小计字段配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.subtotalfields", TranslationValue = "小计字段配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.subtotalfields", TranslationValue = "小计字段配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.subtotalfields", TranslationValue = "小计字段配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.subtotalfields", TranslationValue = "小计字段配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.reportscheme.sortfields
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.sortfields", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.sortfields", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.sortfields", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.sortfields", TranslationValue = "排序字段配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.sortfields", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.sortfields", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.reportscheme.filterconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.reportscheme.supporttotal
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.supporttotal", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.supporttotal", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.supporttotal", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.supporttotal", TranslationValue = "是否支持总计", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.supporttotal", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.supporttotal", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.reportscheme.supportsubtotal
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.supportsubtotal", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.supportsubtotal", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.supportsubtotal", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.supportsubtotal", TranslationValue = "是否支持小计", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.supportsubtotal", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.supportsubtotal", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.reportscheme.supportaggregation
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.supportaggregation", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.supportaggregation", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.supportaggregation", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.supportaggregation", TranslationValue = "是否支持汇总", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.supportaggregation", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.supportaggregation", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.reportscheme.supportdrilldown
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.supportdrilldown", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.supportdrilldown", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.supportdrilldown", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.supportdrilldown", TranslationValue = "是否支持钻取", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.supportdrilldown", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.supportdrilldown", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.reportscheme.drilldownreportcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.drilldownreportcode", TranslationValue = "钻取目标报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.drilldownreportcode", TranslationValue = "钻取目标报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.drilldownreportcode", TranslationValue = "钻取目标报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.drilldownreportcode", TranslationValue = "钻取目标报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.drilldownreportcode", TranslationValue = "钻取目标报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.drilldownreportcode", TranslationValue = "钻取目标报表编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.reportscheme.supportbackground
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.supportbackground", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.supportbackground", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.supportbackground", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.supportbackground", TranslationValue = "是否支持后台执行", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.supportbackground", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.supportbackground", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.reportscheme.supportvariantsave
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.supportvariantsave", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.supportvariantsave", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.supportvariantsave", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.supportvariantsave", TranslationValue = "是否支持变式保存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.supportvariantsave", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.supportvariantsave", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },

            // entity.reportscheme.defaultpagesize
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.defaultpagesize", TranslationValue = "默认页大小", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.defaultpagesize", TranslationValue = "默认页大小", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.defaultpagesize", TranslationValue = "默认页大小", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.defaultpagesize", TranslationValue = "默认页大小", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.defaultpagesize", TranslationValue = "默认页大小", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.defaultpagesize", TranslationValue = "默认页大小", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },

            // entity.reportscheme.maxrowcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.maxrowcount", TranslationValue = "最大数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.maxrowcount", TranslationValue = "最大数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.maxrowcount", TranslationValue = "最大数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.maxrowcount", TranslationValue = "最大数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.maxrowcount", TranslationValue = "最大数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.maxrowcount", TranslationValue = "最大数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },

            // entity.reportscheme.isexportable
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.isexportable", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.isexportable", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.isexportable", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.isexportable", TranslationValue = "是否支持导出", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.isexportable", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.isexportable", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },

            // entity.reportscheme.exportformats
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.exportformats", TranslationValue = "导出格式配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.exportformats", TranslationValue = "导出格式配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.exportformats", TranslationValue = "导出格式配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.exportformats", TranslationValue = "导出格式配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.exportformats", TranslationValue = "导出格式配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.exportformats", TranslationValue = "导出格式配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },

            // entity.reportscheme.isprintable
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.isprintable", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.isprintable", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.isprintable", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.isprintable", TranslationValue = "是否支持打印", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.isprintable", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.isprintable", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },

            // entity.reportscheme.printtemplate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.printtemplate", TranslationValue = "打印模板", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.printtemplate", TranslationValue = "打印模板", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.printtemplate", TranslationValue = "打印模板", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.printtemplate", TranslationValue = "打印模板", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.printtemplate", TranslationValue = "打印模板", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.printtemplate", TranslationValue = "打印模板", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },

            // entity.reportscheme.applicableplantcodes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.applicableplantcodes", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 32 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.applicableplantcodes", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 32 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.applicableplantcodes", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 32 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.applicableplantcodes", TranslationValue = "适用工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 32 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.applicableplantcodes", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 32 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.applicableplantcodes", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 32 },

            // entity.reportscheme.applicablecompanycodes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.applicablecompanycodes", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 33 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.applicablecompanycodes", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 33 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.applicablecompanycodes", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 33 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.applicablecompanycodes", TranslationValue = "适用公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 33 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.applicablecompanycodes", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 33 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.applicablecompanycodes", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 33 },

            // entity.reportscheme.applicabledepartment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 34 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 34 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 34 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 34 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 34 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 34 },

            // entity.reportscheme.applicableroles
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.applicableroles", TranslationValue = "适用角色", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 35 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.applicableroles", TranslationValue = "适用角色", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 35 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.applicableroles", TranslationValue = "适用角色", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 35 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.applicableroles", TranslationValue = "适用角色", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 35 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.applicableroles", TranslationValue = "适用角色", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 35 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.applicableroles", TranslationValue = "适用角色", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 35 },

            // entity.reportscheme.developmentclass
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.developmentclass", TranslationValue = "开发类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 36 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.developmentclass", TranslationValue = "开发类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 36 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.developmentclass", TranslationValue = "开发类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 36 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.developmentclass", TranslationValue = "开发类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 36 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.developmentclass", TranslationValue = "开发类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 36 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.developmentclass", TranslationValue = "开发类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 36 },

            // entity.reportscheme.author
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.author", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 37 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.author", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 37 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.author", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 37 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.author", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 37 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.author", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 37 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.author", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 37 },

            // entity.reportscheme.version
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.version", TranslationValue = "版本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 38 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.version", TranslationValue = "版本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 38 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.version", TranslationValue = "版本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 38 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.version", TranslationValue = "版本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 38 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.version", TranslationValue = "版本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 38 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.version", TranslationValue = "版本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 38 },

            // entity.reportscheme.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 39 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 39 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 39 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 39 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 39 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 39 },

            // entity.reportscheme.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportscheme.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 40 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportscheme.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 40 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportscheme.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 40 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportscheme.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 40 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportscheme.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 40 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportscheme.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 40 },
        };
    }
}
